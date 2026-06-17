using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 构建前置处理器，在打包前校验热更新 AOT 元数据保留生成文件是否齐全且最新。
    /// </summary>
    /// <remarks>
    /// Build preprocessor that validates whether the hot-update AOT metadata preservation generated files are complete and up to date before building.
    /// </remarks>
    internal sealed class AotPreserveBuildPreprocessor : IPreprocessBuildWithReport
    {
        /// <summary>
        /// 获取构建回调执行顺序，本处理器需最先执行。
        /// </summary>
        /// <remarks>
        /// Gets the build callback execution order; this processor must run first.
        /// </remarks>
        /// <value>回调执行顺序 / Callback execution order</value>
        public int callbackOrder
        {
            get { return 0; }
        }

        /// <summary>
        /// 在构建开始前校验生成文件，若缺失或过期则中断构建。
        /// </summary>
        /// <remarks>
        /// Validates the generated files before the build starts and aborts the build if they are missing or out of date.
        /// </remarks>
        /// <param name="report">当前构建报告 / Current build report</param>
        /// <exception cref="BuildFailedException">当生成文件缺失或过期时抛出，提示运行菜单生成命令 / Thrown when generated files are missing or out of date, prompting to run the menu generation command</exception>
        public void OnPreprocessBuild(BuildReport report)
        {
            var errors = AotPreserveGenerator.ValidateGeneratedFiles();
            if (errors.Length > 0)
            {
                throw new BuildFailedException("Hotfix AOT preserve generated files are missing or out of date. Run '" + AotPreserveConstants.MenuPath + "'.\n" + string.Join("\n", errors));
            }
        }
    }
}
