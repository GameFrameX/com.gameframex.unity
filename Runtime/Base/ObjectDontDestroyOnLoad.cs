using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 标记物体对象为不可销毁
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class ObjectDontDestroyOnLoad : MonoBehaviour
    {
        [UnityEngine.Scripting.Preserve]
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}