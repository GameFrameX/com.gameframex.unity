using System;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 热更新 AOT 元数据保留生成文件的校验执行器，供 CI 或测试单独调用。
    /// </summary>
    /// <remarks>
    /// Validator runner for hot-update AOT metadata preservation generated files, intended for standalone invocation from CI or tests.
    /// </remarks>
    internal static class AotPreserveValidationRunner
    {
        /// <summary>
        /// 校验生成文件是否齐全且最新，存在错误时抛出异常。
        /// </summary>
        /// <remarks>
        /// Validates whether the generated files are complete and up to date, throwing an exception when errors exist.
        /// </remarks>
        /// <exception cref="InvalidOperationException">当生成文件缺失或过期时抛出，消息聚合全部错误 / Thrown when generated files are missing or out of date, with all errors aggregated in the message</exception>
        public static void ValidateGeneratedFiles()
        {
            var errors = AotPreserveGenerator.ValidateGeneratedFiles();
            if (errors.Length > 0)
            {
                throw new InvalidOperationException(string.Join("\n", errors));
            }
        }
    }
}
