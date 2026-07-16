// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
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
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository:  https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using GameFrameX.LitJSON.Runtime;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// LitJSON 函数集辅助器。
    /// </summary>
    /// <remarks>
    /// Optional JSON helper using LitJSON.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public class LitJsonHelper : Utility.Json.IJsonHelper
    {
        /// <summary>
        /// 初始化 LitJSON 函数集辅助器的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the LitJSON helper.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public LitJsonHelper()
        {
        }

        /// <summary>
        /// 将对象序列化为 JSON 字符串。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns>序列化后的 JSON 字符串。</returns>
        [UnityEngine.Scripting.Preserve]
        public string ToJson(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            return JsonMapper.ToJson(obj, false);
        }

        /// <summary>
        /// 将 JSON 字符串反序列化为对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">要反序列化的 JSON 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        [UnityEngine.Scripting.Preserve]
        public T ToObject<T>(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            if (json.Length == 0)
            {
                return default;
            }

            try
            {
                return JsonMapper.ToObject<T>(json);
            }
            catch (JsonException exception)
            {
                throw new GameFrameworkException(exception.Message, exception);
            }
        }

        /// <summary>
        /// 将 JSON 字符串反序列化为对象。
        /// </summary>
        /// <param name="objectType">对象类型。</param>
        /// <param name="json">要反序列化的 JSON 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        [UnityEngine.Scripting.Preserve]
        public object ToObject(Type objectType, string json)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            if (json.Length == 0)
            {
                return null;
            }

            try
            {
                return JsonMapper.ToObject(json, objectType);
            }
            catch (JsonException exception)
            {
                throw new GameFrameworkException(exception.Message, exception);
            }
        }
    }
}
