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

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架日志类。
    /// </summary>
    /// <remarks>
    /// Game framework log class.
    /// Provides static methods for logging messages at different severity levels (Debug, Info, Warning, Error, Fatal).
    /// Requires an ILogHelper implementation to be set via SetLogHelper before logging can occur.
    /// </remarks>
    public static partial class GameFrameworkLog
    {
        private static ILogHelper _logHelper = null;

        /// <summary>
        /// 设置游戏框架日志辅助器。
        /// </summary>
        /// <remarks>
        /// Sets the game framework log helper.
        /// The log helper is responsible for the actual log output implementation.
        /// </remarks>
        /// <param name="logHelper">要设置的游戏框架日志辅助器 / The log helper to set.</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetLogHelper(ILogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug(object message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, message);
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug(string message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, message);
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T">日志参数的类型 / The type of the log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg">日志参数 / Log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// 打印调试级别日志，用于记录调试类日志信息。
        /// </summary>
        /// <remarks>
        /// Logs a debug level message, used for recording debugging information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <typeparam name="T16">日志参数 16 的类型 / The type of the sixteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        /// <param name="arg16">日志参数 16 / The sixteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info(object message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, message);
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info(string message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, message);
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T">日志参数的类型 / The type of the log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg">日志参数 / Log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// 打印信息级别日志，用于记录程序正常运行日志信息。
        /// </summary>
        /// <remarks>
        /// Logs an info level message, used for recording normal program execution information.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <typeparam name="T16">日志参数 16 的类型 / The type of the sixteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        /// <param name="arg16">日志参数 16 / The sixteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning(object message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, message);
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning(string message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, message);
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T">日志参数的类型 / The type of the log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg">日志参数 / Log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs a warning level message, recommended when a local functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <typeparam name="T16">日志参数 16 的类型 / The type of the sixteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        /// <param name="arg16">日志参数 16 / The sixteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error(object message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, message);
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error(string message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, message);
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T">日志参数的类型 / The type of the log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg">日志参数 / Log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。
        /// </summary>
        /// <remarks>
        /// Logs an error level message, recommended when a functional logic error occurs but does not yet cause the game to crash or throw.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <typeparam name="T16">日志参数 16 的类型 / The type of the sixteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        /// <param name="arg16">日志参数 16 / The sixteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal(object message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, message);
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <param name="message">日志内容 / Log message content.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal(string message)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, message);
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T">日志参数的类型 / The type of the log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg">日志参数 / Log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T>(string format, T arg)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。
        /// </summary>
        /// <remarks>
        /// Logs a fatal level message, recommended when a severe error occurs that may cause the game to crash or throw; in this case, try restarting the process or rebuilding the game framework.
        /// </remarks>
        /// <typeparam name="T1">日志参数 1 的类型 / The type of the first log argument.</typeparam>
        /// <typeparam name="T2">日志参数 2 的类型 / The type of the second log argument.</typeparam>
        /// <typeparam name="T3">日志参数 3 的类型 / The type of the third log argument.</typeparam>
        /// <typeparam name="T4">日志参数 4 的类型 / The type of the fourth log argument.</typeparam>
        /// <typeparam name="T5">日志参数 5 的类型 / The type of the fifth log argument.</typeparam>
        /// <typeparam name="T6">日志参数 6 的类型 / The type of the sixth log argument.</typeparam>
        /// <typeparam name="T7">日志参数 7 的类型 / The type of the seventh log argument.</typeparam>
        /// <typeparam name="T8">日志参数 8 的类型 / The type of the eighth log argument.</typeparam>
        /// <typeparam name="T9">日志参数 9 的类型 / The type of the ninth log argument.</typeparam>
        /// <typeparam name="T10">日志参数 10 的类型 / The type of the tenth log argument.</typeparam>
        /// <typeparam name="T11">日志参数 11 的类型 / The type of the eleventh log argument.</typeparam>
        /// <typeparam name="T12">日志参数 12 的类型 / The type of the twelfth log argument.</typeparam>
        /// <typeparam name="T13">日志参数 13 的类型 / The type of the thirteenth log argument.</typeparam>
        /// <typeparam name="T14">日志参数 14 的类型 / The type of the fourteenth log argument.</typeparam>
        /// <typeparam name="T15">日志参数 15 的类型 / The type of the fifteenth log argument.</typeparam>
        /// <typeparam name="T16">日志参数 16 的类型 / The type of the sixteenth log argument.</typeparam>
        /// <param name="format">日志格式 / Log format string.</param>
        /// <param name="arg1">日志参数 1 / The first log argument.</param>
        /// <param name="arg2">日志参数 2 / The second log argument.</param>
        /// <param name="arg3">日志参数 3 / The third log argument.</param>
        /// <param name="arg4">日志参数 4 / The fourth log argument.</param>
        /// <param name="arg5">日志参数 5 / The fifth log argument.</param>
        /// <param name="arg6">日志参数 6 / The sixth log argument.</param>
        /// <param name="arg7">日志参数 7 / The seventh log argument.</param>
        /// <param name="arg8">日志参数 8 / The eighth log argument.</param>
        /// <param name="arg9">日志参数 9 / The ninth log argument.</param>
        /// <param name="arg10">日志参数 10 / The tenth log argument.</param>
        /// <param name="arg11">日志参数 11 / The eleventh log argument.</param>
        /// <param name="arg12">日志参数 12 / The twelfth log argument.</param>
        /// <param name="arg13">日志参数 13 / The thirteenth log argument.</param>
        /// <param name="arg14">日志参数 14 / The fourteenth log argument.</param>
        /// <param name="arg15">日志参数 15 / The fifteenth log argument.</param>
        /// <param name="arg16">日志参数 16 / The sixteenth log argument.</param>
        [UnityEngine.Scripting.Preserve]
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (_logHelper == null)
            {
                return;
            }

            _logHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }
    }
}
