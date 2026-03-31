using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架单例
    /// </summary>
    /// <remarks>
    /// Game framework MonoBehaviour singleton base class that automatically creates and persists the instance across scenes.
    /// </remarks>
    /// <typeparam name="T">单例类型（必须继承自MonoBehaviour）/ The type of the singleton class (must inherit from MonoBehaviour)</typeparam>
    [UnityEngine.Scripting.Preserve]
    public abstract class GameFrameworkMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        [UnityEngine.Scripting.Preserve]
        protected GameFrameworkMonoSingleton()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        /// <remarks>
        /// Gets the singleton instance. If not found, it will search for existing instance, create a new GameObject with the component, and mark it as DontDestroyOnLoad during runtime.
        /// </remarks>
        /// <value>单例实例 / The singleton instance</value>
        [UnityEngine.Scripting.Preserve]
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)Object.FindObjectOfType(typeof(T));
                }

                if (_instance == null)
                {
                    var insObj = new GameObject();
                    _instance = insObj.AddComponent<T>();
                    _instance.name = "[Singleton]" + typeof(T);

                    if (Application.isPlaying)
                    {
                        Object.DontDestroyOnLoad(insObj);
                    }
                }

                return _instance;
            }
        }
    }
}