using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 提供热更新 AOT 保留的默认设置。
    /// </summary>
    /// <remarks>
    /// Provides default settings for hotfix AOT preservation.
    /// </remarks>
    internal sealed class DefaultAotPreserveSettingsProvider : IAotPreserveSettingsProvider
    {
        /// <summary>
        /// 获取需要保留的 Unity 类型数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of Unity types to preserve.
        /// </remarks>
        public Type[] PreserveTypes
        {
            get { return Array.Empty<Type>(); }
        }

        /// <summary>
        /// 获取需要生成引用包装代码的类型数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of types that require reference wrapper code generation.
        /// </remarks>
        public Type[] WrapTypes
        {
            get
            {
                return new[]
                {
                    typeof(object),
                    typeof(Delegate),
                    typeof(string),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(float),
                    typeof(double),
                    typeof(Enum),
                    typeof(Type),
                    typeof(IEnumerator),
                    typeof(UnityEngine.Object),
                    typeof(Application),
                    typeof(Time),
                    typeof(Screen),
                    typeof(SleepTimeout),
                    typeof(Input),
                    typeof(Resources),
                    typeof(Physics),
                    typeof(RenderSettings),
                    typeof(QualitySettings),
                    typeof(GL),
                    typeof(Graphics),
                    typeof(Component),
                    typeof(Transform),
                    typeof(Material),
                    typeof(Light),
                    typeof(Rigidbody),
                    typeof(Camera),
                    typeof(AudioSource),
                    typeof(Behaviour),
                    typeof(MonoBehaviour),
                    typeof(GameObject),
                    typeof(Collider),
                    typeof(Texture),
                    typeof(Texture2D),
                    typeof(Shader),
                    typeof(Renderer),
                    typeof(CameraClearFlags),
                    typeof(AudioClip),
                    typeof(AssetBundle),
                    typeof(ParticleSystem),
                    typeof(AsyncOperation),
                    typeof(LightType),
                    typeof(Animator),
                    typeof(KeyCode),
                    typeof(SkinnedMeshRenderer),
                    typeof(Space),
                    typeof(MeshRenderer),
                    typeof(BoxCollider),
                    typeof(MeshCollider),
                    typeof(SphereCollider),
                    typeof(CharacterController),
                    typeof(CapsuleCollider),
                    typeof(Animation),
                    typeof(AnimationClip),
                    typeof(AnimationState),
                    typeof(AnimationBlendMode),
                    typeof(QueueMode),
                    typeof(PlayMode),
                    typeof(WrapMode),
                    typeof(SkinWeights),
                    typeof(RenderTexture),
                    typeof(Vector2),
                    typeof(Vector3),
                    typeof(Vector4),
                    typeof(Quaternion),
                    typeof(Color),
                    typeof(Bounds),
                    typeof(Ray),
                    typeof(RaycastHit),
                    typeof(LayerMask),
                    typeof(Touch),
                };
            }
        }

        /// <summary>
        /// 获取需要保留的成员名称数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of member names to preserve.
        /// </remarks>
        public string[] PreserveMembers
        {
            get { return Array.Empty<string>(); }
        }

        /// <summary>
        /// 获取需要保留的泛型类型数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of generic types to preserve.
        /// </remarks>
        public Type[] PreserveGenericTypes
        {
            get
            {
                return new[]
                {
                    typeof(Action),
                    typeof(Action<int>),
                    typeof(Action<long>),
                    typeof(Action<object>),
                    typeof(Action<string>),
                    typeof(Action<int, object>),
                    typeof(Action<object, object>),
                    typeof(UnityAction),
                    typeof(Predicate<int>),
                    typeof(Comparison<int>),
                    typeof(Func<int, int>),
                    typeof(Func<object>),
                    typeof(Func<string, object>),
                    typeof(List<int>),
                    typeof(List<long>),
                    typeof(List<string>),
                    typeof(List<object>),
                    typeof(List<Vector2>),
                    typeof(List<Vector3>),
                    typeof(List<Vector4>),
                    typeof(Dictionary<int, int>),
                    typeof(Dictionary<int, object>),
                    typeof(Dictionary<long, object>),
                    typeof(Dictionary<string, object>),
                    typeof(Dictionary<object, object>),
                    typeof(KeyValuePair<int, object>),
                    typeof(KeyValuePair<long, object>),
                    typeof(KeyValuePair<string, object>),
                    typeof(KeyValuePair<object, object>),
                    typeof(HashSet<int>),
                    typeof(HashSet<object>),
                };
            }
        }

        /// <summary>
        /// 获取类型白名单数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of type whitelist patterns.
        /// </remarks>
        public string[] TypeWhiteList
        {
            get { return Array.Empty<string>(); }
        }

        /// <summary>
        /// 获取类型黑名单数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of type blacklist patterns.
        /// </remarks>
        public string[] TypeBlackList
        {
            get
            {
                return new[]
                {
                    "UnityEditor.*",
                    "*.Editor*",
                };
            }
        }

        /// <summary>
        /// 获取成员白名单数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of member whitelist patterns.
        /// </remarks>
        public string[] MemberWhiteList
        {
            get { return Array.Empty<string>(); }
        }

        /// <summary>
        /// 获取成员黑名单数组。
        /// </summary>
        /// <remarks>
        /// Gets the array of member blacklist patterns.
        /// </remarks>
        public string[] MemberBlackList
        {
            get
            {
                return new[]
                {
                    "*.Editor*",
                    "*.Obsolete*",
                };
            }
        }
    }
}
