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

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 字符相关的实用函数。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static partial class Text
        {
            private static ITextHelper s_TextHelper = null;

            /// <summary>
            /// 设置字符辅助器。
            /// </summary>
            /// <param name="textHelper">要设置的字符辅助器。</param>
            [UnityEngine.Scripting.Preserve]
            public static void SetTextHelper(ITextHelper textHelper)
            {
                s_TextHelper = textHelper;
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T">字符串参数的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="args">参数列表。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format(string format, params object[] args)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, args);
                }

                return s_TextHelper.Format(format, args);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T">字符串参数的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg">字符串参数。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T>(string format, T arg)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg);
                }

                return s_TextHelper.Format(format, arg);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2>(string format, T1 arg1, T2 arg2)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2);
                }

                return s_TextHelper.Format(format, arg1, arg2);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <param name="arg10">字符串参数 10。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
            /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <param name="arg10">字符串参数 10。</param>
            /// <param name="arg11">字符串参数 11。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
            /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
            /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <param name="arg10">字符串参数 10。</param>
            /// <param name="arg11">字符串参数 11。</param>
            /// <param name="arg12">字符串参数 12。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
            /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
            /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
            /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <param name="arg10">字符串参数 10。</param>
            /// <param name="arg11">字符串参数 11。</param>
            /// <param name="arg12">字符串参数 12。</param>
            /// <param name="arg13">字符串参数 13。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
            /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
            /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
            /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
            /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <param name="arg10">字符串参数 10。</param>
            /// <param name="arg11">字符串参数 11。</param>
            /// <param name="arg12">字符串参数 12。</param>
            /// <param name="arg13">字符串参数 13。</param>
            /// <param name="arg14">字符串参数 14。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
            /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
            /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
            /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
            /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
            /// <typeparam name="T15">字符串参数 15 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <param name="arg10">字符串参数 10。</param>
            /// <param name="arg11">字符串参数 11。</param>
            /// <param name="arg12">字符串参数 12。</param>
            /// <param name="arg13">字符串参数 13。</param>
            /// <param name="arg14">字符串参数 14。</param>
            /// <param name="arg15">字符串参数 15。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            }

            /// <summary>
            /// 获取格式化字符串。
            /// </summary>
            /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
            /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
            /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
            /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
            /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
            /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
            /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
            /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
            /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
            /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
            /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
            /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
            /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
            /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
            /// <typeparam name="T15">字符串参数 15 的类型。</typeparam>
            /// <typeparam name="T16">字符串参数 16 的类型。</typeparam>
            /// <param name="format">字符串格式。</param>
            /// <param name="arg1">字符串参数 1。</param>
            /// <param name="arg2">字符串参数 2。</param>
            /// <param name="arg3">字符串参数 3。</param>
            /// <param name="arg4">字符串参数 4。</param>
            /// <param name="arg5">字符串参数 5。</param>
            /// <param name="arg6">字符串参数 6。</param>
            /// <param name="arg7">字符串参数 7。</param>
            /// <param name="arg8">字符串参数 8。</param>
            /// <param name="arg9">字符串参数 9。</param>
            /// <param name="arg10">字符串参数 10。</param>
            /// <param name="arg11">字符串参数 11。</param>
            /// <param name="arg12">字符串参数 12。</param>
            /// <param name="arg13">字符串参数 13。</param>
            /// <param name="arg14">字符串参数 14。</param>
            /// <param name="arg15">字符串参数 15。</param>
            /// <param name="arg16">字符串参数 16。</param>
            /// <returns>格式化后的字符串。</returns>
            [UnityEngine.Scripting.Preserve]
            public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            {
                if (format == null)
                {
                    throw new GameFrameworkException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            }
        }
    }
}