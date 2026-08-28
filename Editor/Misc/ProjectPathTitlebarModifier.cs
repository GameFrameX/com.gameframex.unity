using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    public class ProjectPathTitlebarModifier
    {
        private const int Delay = 2000;

        [InitializeOnLoadMethod]
        private static void Init()
        {
            _ = ModifyTitleAsync();
        }

        private static async Task ModifyTitleAsync()
        {
            await Task.Delay(Delay);
            ModifyTitle();
        }

        private static void ModifyTitle()
        {
            var tEditorApplication = typeof(EditorApplication);
            var tApplicationTitleDescriptor = tEditorApplication.Assembly.GetTypes()
                .FirstOrDefault(x => x.FullName == "UnityEditor.ApplicationTitleDescriptor");
            if (tApplicationTitleDescriptor == null)
            {
                // Unity 内部类型不存在时放弃修改, 避免版本升级后初始化报错
                return;
            }

            var eiUpdateMainWindowTitle =
                tEditorApplication.GetEvent("updateMainWindowTitle", BindingFlags.Static | BindingFlags.NonPublic);
            var miUpdateMainWindowTitle =
                tEditorApplication.GetMethod("UpdateMainWindowTitle", BindingFlags.Static | BindingFlags.NonPublic);

            var delegateType = typeof(Action<>).MakeGenericType(tApplicationTitleDescriptor);
            var methodInfo = ((Action<object>) UpdateWindowTitle).Method;
            var del = Delegate.CreateDelegate(delegateType, null, methodInfo);

            eiUpdateMainWindowTitle.GetAddMethod(true).Invoke(null, new object[] {del});
            miUpdateMainWindowTitle.Invoke(null, new object[0]);
            eiUpdateMainWindowTitle.GetRemoveMethod(true).Invoke(null, new object[] {del});
        }

        private static void UpdateWindowTitle(object desc)
        {
            var fieldInfo = typeof(EditorApplication).Assembly.GetTypes()
                .FirstOrDefault(x => x.FullName == "UnityEditor.ApplicationTitleDescriptor")
                .GetField("title", BindingFlags.Instance | BindingFlags.Public);
            if (fieldInfo == null)
            {
                return;
            }

            var str = fieldInfo.GetValue(desc) as string;
            fieldInfo.SetValue(desc, $"{str} {Application.dataPath.Replace("/Assets", "")}");
        }
    }
}
