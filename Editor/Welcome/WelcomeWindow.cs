// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    public class WelcomeWindow : EditorWindow
    {
        private static readonly string FirstRunKey = "GameFrameX_WelcomeWindow_Shown";
        private static readonly string ShowOnStartupKey = "GameFrameX_ShowOnStartup";

        private bool showOnStartup = true;
        private Vector2 scrollPosition;

        [MenuItem("GameFrameX/Welcome Window", false, 99999999)]
        public static void ShowWindow()
        {
            var window = GetWindow<WelcomeWindow>("GameFrameX 欢迎界面");
            window.minSize = new Vector2(640, 800);
            window.maxSize = new Vector2(640, 800);
            window.ShowModalUtility();
        }

        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            EditorApplication.delayCall += ShowWelcomeWindowOnFirstRun;
        }

        /// <summary>
        /// 在第一次运行时显示欢迎窗口
        /// </summary>
        static void ShowWelcomeWindowOnFirstRun()
        {
            // 检查是否已经显示过欢迎窗口
            if (!SessionState.GetBool(FirstRunKey, false))
            {
                // 设置标记，表示已经显示过
                SessionState.SetBool(FirstRunKey, true);

                // 检查用户设置是否应该显示
                if (EditorPrefs.GetBool(ShowOnStartupKey, true))
                {
                    var window = GetWindow<WelcomeWindow>("GameFrameX 欢迎界面");
                    window.minSize = new Vector2(640, 800);
                    window.maxSize = new Vector2(640, 800);
                    window.ShowModalUtility();
                }
            }
        }

        private void OnEnable()
        {
            showOnStartup = EditorPrefs.GetBool(ShowOnStartupKey, true);
            // 加载Logo纹理
            LoadLogoTexture();
        }

        private Texture2D _logoTexture;

        private void LoadLogoTexture()
        {
            // 方式1: 从Resources文件夹加载（如果Logo放在Resources文件夹中）
            _logoTexture = Resources.Load<Texture2D>("gameframex_logo");
        }

        private void OnGUI()
        {
            // 头部标题
            GUILayout.Space(10);
            // Logo和标题区域
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (_logoTexture != null)
            {
                // 显示Logo
                GUILayout.Label(_logoTexture, GUILayout.Width(80), GUILayout.Height(80));
                GUILayout.Space(10);
            }

            // 标题垂直排列
            GUILayout.BeginVertical();
            GUIStyle titleStyle = new GUIStyle(EditorStyles.largeLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 20,
                fontStyle = FontStyle.Bold
            };

            GUIStyle subTitleStyle = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 12
            };

            GUILayout.Label("欢迎使用 GameFrameX", titleStyle);
            GUILayout.Label("独立游戏前后端一体化解决方案,独立游戏开发者的圆梦大使", subTitleStyle);
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            // 内容区域
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));

            // 视频教程部分
            DrawSection("视频教程", "观看我们的入门视频教程", "https://space.bilibili.com/101356690/lists/3861146?type=season");

            // 文档链接部分
            DrawSection("官方文档", "查看详细的开发文档", "https://gameframex.doc.alianblank.com/");

            // 常见问题部分
            DrawSection("常见问题", "查看常见问题及解决方案", "https://gameframex.doc.alianblank.com/faq");

            // 配置表部分
            DrawSection("配置表", "查看配置表的规范和要求相关内容", "https://gameframex.doc.alianblank.com/config/");

            // 通信协议部分
            DrawSection("通信协议", "查看通信协议的规范和要求相关内容", "https://gameframex.doc.alianblank.com/protobuf/note.html");

            // UI 系统部分
            DrawSection("UI 系统", "查看 UI 系统相关内容", "https://gameframex.doc.alianblank.com/unity/component/ui.html");

            //  BUG 反馈部分
            DrawSection("BUG 反馈", "反馈 BUG", "https://github.com/GameFrameX/GameFrameX/issues/new");

            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();

            // 底部复选框
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            showOnStartup = EditorGUILayout.ToggleLeft("下次启动时显示此窗口", showOnStartup, GUILayout.Width(200));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
        }

        private void DrawSection(string title, string description, string url)
        {
            GUIStyle sectionStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(10, 10, 10, 10),
                margin = new RectOffset(10, 10, 5, 5)
            };

            GUILayout.BeginVertical(sectionStyle);

            GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14
            };

            GUILayout.Label(title, headerStyle);
            GUILayout.Label(description, EditorStyles.label);

            GUILayout.Space(5);

            if (GUILayout.Button("立即访问"))
            {
                Application.OpenURL(url);
            }

            GUILayout.EndVertical();

            GUILayout.Space(10);
        }

        private void OnDisable()
        {
            // 保存用户的设置
            EditorPrefs.SetBool(ShowOnStartupKey, showOnStartup);
        }
    }
}