using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 标记物体对象为不可销毁
    /// </summary>
    /// <remarks>
    /// Marks a GameObject to persist across scene loads. Objects with this component will not be destroyed
    /// when loading a new scene. This is useful for singleton patterns, managers, and other persistent objects.
    /// </remarks>
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