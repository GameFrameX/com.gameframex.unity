using UnityEngine;

namespace Base
{
    /// <summary>
    /// 游戏框架单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GameFrameworkMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        protected GameFrameworkMonoSingleton()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
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