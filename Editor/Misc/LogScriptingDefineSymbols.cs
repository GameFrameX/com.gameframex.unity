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

namespace GameFrameX.Editor
{
    /// <summary>
    /// 日志脚本宏定义。
    /// </summary>
    public static class LogScriptingDefineSymbols
    {
        private const string EnableLogScriptingDefineSymbol = "ENABLE_LOG";
        private const string EnableDebugAndAboveLogScriptingDefineSymbol = "ENABLE_DEBUG_AND_ABOVE_LOG";
        private const string EnableInfoAndAboveLogScriptingDefineSymbol = "ENABLE_INFO_AND_ABOVE_LOG";
        private const string EnableWarningAndAboveLogScriptingDefineSymbol = "ENABLE_WARNING_AND_ABOVE_LOG";
        private const string EnableErrorAndAboveLogScriptingDefineSymbol = "ENABLE_ERROR_AND_ABOVE_LOG";
        private const string EnableFatalAndAboveLogScriptingDefineSymbol = "ENABLE_FATAL_AND_ABOVE_LOG";
        private const string EnableDebugLogScriptingDefineSymbol = "ENABLE_DEBUG_LOG";
        private const string EnableInfoLogScriptingDefineSymbol = "ENABLE_INFO_LOG";
        private const string EnableWarningLogScriptingDefineSymbol = "ENABLE_WARNING_LOG";
        private const string EnableErrorLogScriptingDefineSymbol = "ENABLE_ERROR_LOG";
        private const string EnableFatalLogScriptingDefineSymbol = "ENABLE_FATAL_LOG";

        private static readonly string[] AboveLogScriptingDefineSymbols = new string[]
        {
            EnableDebugAndAboveLogScriptingDefineSymbol,
            EnableInfoAndAboveLogScriptingDefineSymbol,
            EnableWarningAndAboveLogScriptingDefineSymbol,
            EnableErrorAndAboveLogScriptingDefineSymbol,
            EnableFatalAndAboveLogScriptingDefineSymbol
        };

        private static readonly string[] SpecifyLogScriptingDefineSymbols = new string[]
        {
            EnableDebugLogScriptingDefineSymbol,
            EnableInfoLogScriptingDefineSymbol,
            EnableWarningLogScriptingDefineSymbol,
            EnableErrorLogScriptingDefineSymbol,
            EnableFatalLogScriptingDefineSymbol
        };

        /// <summary>
        /// 禁用所有日志脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable All Logs(禁用所有日志脚本宏定义)", false, 30)]
        public static void DisableAllLogs()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableLogScriptingDefineSymbol);

            foreach (string specifyLogScriptingDefineSymbol in SpecifyLogScriptingDefineSymbols)
            {
                ScriptingDefineSymbols.RemoveScriptingDefineSymbol(specifyLogScriptingDefineSymbol);
            }

            foreach (string aboveLogScriptingDefineSymbol in AboveLogScriptingDefineSymbols)
            {
                ScriptingDefineSymbols.RemoveScriptingDefineSymbol(aboveLogScriptingDefineSymbol);
            }
        }

        /// <summary>
        /// 开启所有日志脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable All Logs(开启所有日志脚本宏定义)", false, 31)]
        public static void EnableAllLogs()
        {
            DisableAllLogs();
            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启调试及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Debug And Above Logs(开启调试及以上级别的日志脚本宏定义)", false, 32)]
        public static void EnableDebugAndAboveLogs()
        {
            SetAboveLogScriptingDefineSymbol(EnableDebugAndAboveLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启信息及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Info And Above Logs(开启信息及以上级别的日志脚本宏定义)", false, 33)]
        public static void EnableInfoAndAboveLogs()
        {
            SetAboveLogScriptingDefineSymbol(EnableInfoAndAboveLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启警告及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Warning And Above Logs(开启警告及以上级别的日志脚本宏定义)", false, 34)]
        public static void EnableWarningAndAboveLogs()
        {
            SetAboveLogScriptingDefineSymbol(EnableWarningAndAboveLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启错误及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Error And Above Logs(开启错误及以上级别的日志脚本宏定义)", false, 35)]
        public static void EnableErrorAndAboveLogs()
        {
            SetAboveLogScriptingDefineSymbol(EnableErrorAndAboveLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启严重错误及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Fatal And Above Logs(开启严重错误及以上级别的日志脚本宏定义)", false, 36)]
        public static void EnableFatalAndAboveLogs()
        {
            SetAboveLogScriptingDefineSymbol(EnableFatalAndAboveLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 设置日志脚本宏定义。
        /// </summary>
        /// <param name="aboveLogScriptingDefineSymbol">要设置的日志脚本宏定义。</param>
        public static void SetAboveLogScriptingDefineSymbol(string aboveLogScriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(aboveLogScriptingDefineSymbol))
            {
                return;
            }

            foreach (string i in AboveLogScriptingDefineSymbols)
            {
                if (i == aboveLogScriptingDefineSymbol)
                {
                    DisableAllLogs();
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(aboveLogScriptingDefineSymbol);
                    return;
                }
            }
        }

        /// <summary>
        /// 设置日志脚本宏定义。
        /// </summary>
        /// <param name="specifyLogScriptingDefineSymbols">要设置的日志脚本宏定义。</param>
        public static void SetSpecifyLogScriptingDefineSymbols(string[] specifyLogScriptingDefineSymbols)
        {
            if (specifyLogScriptingDefineSymbols == null || specifyLogScriptingDefineSymbols.Length <= 0)
            {
                return;
            }

            bool removed = false;
            foreach (string specifyLogScriptingDefineSymbol in specifyLogScriptingDefineSymbols)
            {
                if (string.IsNullOrEmpty(specifyLogScriptingDefineSymbol))
                {
                    continue;
                }

                foreach (string i in SpecifyLogScriptingDefineSymbols)
                {
                    if (i == specifyLogScriptingDefineSymbol)
                    {
                        if (!removed)
                        {
                            removed = true;
                            DisableAllLogs();
                        }

                        ScriptingDefineSymbols.AddScriptingDefineSymbol(specifyLogScriptingDefineSymbol);
                        break;
                    }
                }
            }
        }
    }
}