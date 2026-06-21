namespace GameFrameX.Editor
{
    /// <summary>
    /// HybridCLR 热更新 AOT 元数据保留生成相关的常量定义，包含生成路径、文件名前缀与菜单路径。
    /// </summary>
    /// <remarks>
    /// Constants for HybridCLR hot-update AOT metadata preservation generation, including generated paths, file name prefixes and menu paths.
    /// </remarks>
    internal static class AotPreserveConstants
    {
        /// <summary>
        /// 生成文件的根目录相对路径。
        /// </summary>
        /// <remarks>
        /// Root directory relative path of generated files.
        /// </remarks>
        public const string GeneratedDirectory = "Assets/Generated/AotPreserve";

        /// <summary>
        /// 生成的 link.xml 文件相对路径，用于托管代码 stripping 时的类型保留。
        /// </summary>
        /// <remarks>
        /// Relative path of the generated link.xml file used for type preservation during managed code stripping.
        /// </remarks>
        public const string LinkXmlPath = GeneratedDirectory + "/link.xml";

        /// <summary>
        /// 生成的引用汇总代码文件相对路径。
        /// </summary>
        /// <remarks>
        /// Relative path of the generated aggregated reference code file.
        /// </remarks>
        public const string ReferencesPath = "Assets/Generated/AotPreserveReferences.cs";

        /// <summary>
        /// 按类型拆分的引用代码文件名前缀。
        /// </summary>
        /// <remarks>
        /// File name prefix of per-type split reference code files.
        /// </remarks>
        public const string ReferenceTypeFilePrefix = "AotPreserveReference_";

        /// <summary>
        /// 生成代码所属程序集定义文件的相对路径。
        /// </summary>
        /// <remarks>
        /// Relative path of the assembly definition file that the generated code belongs to.
        /// </remarks>
        public const string GeneratedAsmdefPath = "Assets/Generated/AotPreserve.Generated.asmdef";

        /// <summary>
        /// 生成报告 Markdown 文件的相对路径。
        /// </summary>
        /// <remarks>
        /// Relative path of the generation report Markdown file.
        /// </remarks>
        public const string ReportPath = GeneratedDirectory + "/aot_preserve_report.md";

        /// <summary>
        /// Unity 菜单项路径，用于触发生成命令。
        /// </summary>
        /// <remarks>
        /// Unity menu item path used to trigger the generation command.
        /// </remarks>
        public const string MenuPath = "GameFrameX/HybridCLR Generate AOT Preserve";
    }
}
