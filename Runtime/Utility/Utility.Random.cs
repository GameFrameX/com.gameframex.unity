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

using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 随机相关的实用函数。
        /// </summary>
        [Preserve]
        public static class Random
        {
            private static System.Random s_Random = new System.Random((int)DateTime.UtcNow.Ticks);

            /// <summary>
            /// 设置随机数种子。
            /// </summary>
            /// <param name="seed">随机数种子。</param>
            [Preserve]
            public static void SetSeed(int seed)
            {
                s_Random = new System.Random(seed);
            }

            /// <summary>
            /// 返回非负随机数。
            /// </summary>
            /// <returns>返回一个大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数。</returns>
            [Preserve]
            public static int GetRandom()
            {
                return s_Random.Next();
            }

            /// <summary>
            /// 返回一个小于所指定最大值的非负随机数。
            /// </summary>
            /// <param name="maxValue">要生成的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于零。</param>
            /// <returns>返回一个大于等于零且小于 maxValue 的 32 位带符号整数。如果 maxValue 等于零，则返回 maxValue。</returns>
            [Preserve]
            public static int GetRandom(int maxValue)
            {
                return s_Random.Next(maxValue);
            }

            /// <summary>
            /// 返回一个指定范围内的随机数。
            /// </summary>
            /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
            /// <param name="maxValue">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。</param>
            /// <returns>返回一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数。如果 minValue 等于 maxValue，则返回 minValue。</returns>
            [Preserve]
            public static int GetRandom(int minValue, int maxValue)
            {
                return s_Random.Next(minValue, maxValue);
            }

            /// <summary>
            /// 返回一个介于 0.0 和 1.0 之间的随机数。
            /// </summary>
            /// <returns>返回一个大于等于 0.0 并且小于 1.0 的双精度浮点数。</returns>
            [Preserve]
            public static double GetRandomDouble()
            {
                return s_Random.NextDouble();
            }

            /// <summary>
            /// 用随机数填充指定字节数组的元素。
            /// </summary>
            /// <param name="buffer">包含随机数的字节数组。</param>
            [Preserve]
            public static void GetRandomBytes(byte[] buffer)
            {
                s_Random.NextBytes(buffer);
            }
        }
    }
}