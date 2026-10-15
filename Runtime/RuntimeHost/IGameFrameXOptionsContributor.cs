
namespace GameFrameX.Runtime
{
    /// <summary>
    /// Options 贡献者契约：实现类由运行时扫描器静默发现并自动应用（引用即注册，零启动样板）。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 发现条件（与 Manager 发现同门）：class、非抽象、非泛型定义、public 无参构造；Editor / Tests 程序集跳过。
    /// 应用时机：最早实例化点之前——<see cref="GameFrameXRuntimeBootstrap"/> 的 BeforeSceneLoad 幂等调用
    /// <c>GameFrameXRuntimeContributorHost.EnsureApplied()</c>。
    /// </para>
    /// <para>
    /// 手动优先：游戏侧显式 <c>GameFrameXOptions.Configure</c> 永远胜出——贡献者对已 Configure 的 Options 类型按类型粒度跳过并输出诊断，
    /// 与 BeforeSceneLoad 阶段内的执行顺序无关。贡献者类只应做 Configure，不承载初始化副作用（发现即实例化，构造函数保持无参且无副作用）。
    /// IL2CPP / 小游戏裁剪环境下实现类需自行 <see cref="UnityEngine.Scripting.Preserve"/> 或纳入裁剪保护清单，否则类型可能被裁掉导致静默失效。
    /// </para>
    /// <para>
    /// English: Contract for silent Options contributors. Implementations are discovered by the runtime scanner and applied
    /// automatically before the earliest module instantiation, so game code needs no startup boilerplate.
    /// Manual <c>GameFrameXOptions.Configure</c> always wins: contributors skip already-configured Options types (per type, with
    /// diagnostics), regardless of execution order within the BeforeSceneLoad phase. Contributor classes must stay side-effect free
    /// and keep a public parameterless constructor; under IL2CPP stripping, implementations should be kept via <c>[UnityEngine.Scripting.Preserve]</c>.
    /// </para>
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public interface IGameFrameXOptionsContributor
    {
        /// <summary>
        /// 向上下文写入本贡献者负责的 Options 默认值（已手动 Configure 的类型会被跳过并诊断）。
        /// </summary>
        /// <param name="context">贡献者写入上下文。 / The contributor write context.</param>
        /// <remarks>
        /// English: Writes this contributor's Options defaults into the context; types already configured manually are skipped
        /// with a diagnostic to preserve the manual-first semantics.
        /// </remarks>
        void ConfigureOptions(GameFrameXOptionsContributorContext context);
    }
}
