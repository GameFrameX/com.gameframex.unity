<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity Package

[![License](https://img.shields.io/badge/license-blue.svg)](LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

[![Discord](https://img.shields.io/badge/-5865F2?logo=discord&logoColor=white)](https://discord.gg/VDWUJWMDw9)
[![GitHub](https://img.shields.io/badge/-181717?logo=github&logoColor=white)](https://github.com/GameFrameX/gameframex)
[![Bilibili](https://img.shields.io/badge/-00A1D6?logo=bilibili&logoColor=white)](https://www.bilibili.com/video/BV1yrpeepEn7)
[![Gitee](https://img.shields.io/badge/-C71D23?logo=gitee&logoColor=white)](https://gitee.com/GameFrameX/gameframex)

**All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams**

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

<br />

</div>

---

## Project Overview

GameFrameX is a modern Unity game framework designed for independent game developers, providing a complete end-to-end solution for game development. The framework follows a three-layer modular architecture and ships with rich built-in tools and components, helping developers ship high-quality game projects faster.

`com.gameframex.unity` is the core package — it contains the runtime kernels (event pool, reference pool, task pool, object pool, variable / property systems, extensions, helpers, utilities), the editor toolchain (build pipeline with hotfix, WebGL + HybridCLR tools, mini-game platform adaptation, welcome window, inspector enhancements, inspector lock shortcut, cropping, scripting define symbol manager), native iOS bridge plugin and bundled .NET libraries (SharpZipLib, System.Memory / Buffers / Pipelines). It is the foundation every other GameFrameX Unity package depends on.

### Features

- Three-layer architecture: Runtime / Plugins / Editor with clear responsibilities.
- Rich runtime toolkit: EventPool, ReferencePool, TaskPool, ObjectPool, Variable / Property, extensions and Helper classes.
- Editor toolchain: BuildProduct / BuildHotfix pipeline, BuildWebGLTools with HybridCLR, mini-game adaptation for 21 platforms, welcome window, inspector enhancements.
- Utility layer: encryption (AES / RSA / DSA), compression (ZIP), hash (MD5 / SHA1 / HMAC / CRC), JSON, file / path / network helpers, ID generator, text processing.
- Hotfix-ready: HybridCLR hotfix build pipeline integrated with the standard build process.
- Multi-platform: Windows, macOS, Linux, iOS, Android, WebGL, plus 21 mini-game platforms worldwide.

### System Requirements

- Unity: 2019.4 or higher
- Platforms: Windows, macOS, Linux, iOS, Android, WebGL
- .NET: .NET Standard 2.0+

---

## Quick Start

### Installation

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex.unity"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity": "2.5.4"
     }
   }
   ```

   `scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex.unity` will be fetched from it.

2. Add to `manifest.json` dependencies:
   ```json
   {
     "com.gameframex.unity": "https://github.com/GameFrameX/com.gameframex.unity.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/GameFrameX/com.gameframex.unity.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.

> When installed via options 2-4, the editor automatically detects and completes a missing GameFrameX scoped registry entry in `manifest.json` on load (a backup is written to `manifest.json.gameframex.bak` first); it skips when a third-party registry such as OpenUPM already covers the scope, and never modifies the manifest in `-batchmode` (CI) sessions.
> The OpenUPM registry (`package.openupm.com`) is registered by default as well (empty scopes; an existing entry with the same URL is left untouched).

### Basic Usage

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Get a component from the framework entry
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

        // Get a reference pool component
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();

        // Use extension methods on engine types
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

### Zero-Config Startup (Auto Injection)

Installing this package is the only startup step. `GameFrameXRuntimeBootstrap` marks itself with `RuntimeInitializeOnLoadMethod(BeforeSceneLoad)`, so the framework injects itself before the first scene loads; at `AfterSceneLoad` it creates the hidden `[GameFrameX]` host node and mounts every GameFrameX component on it. No scene edits, no prefab, no bootstrap script, and no code — any scene boots the framework unchanged.

Hand-placed GameFrameX components in a scene (BaseComponent and its sibling components) are detected by the bootstrap, then **suppress themselves**: they do not register, do not initialize, and are not driven, and each one logs a `[GameFrameX]`-prefixed warning. Migration: delete the hand-placed GameFrameX nodes from your scenes and let auto injection own startup.

Module parameters are injected through `GameFrameXOptions.Configure<T>(options)` from a `RuntimeInitializeOnLoadMethod(BeforeSceneLoad)` method — see the next section.

### Module Options Startup Configuration

Each module ships an `XxxOptions` startup configuration (field defaults match the former Inspector serialized defaults), injected once via `GameFrameXOptions.Configure` before the module is first instantiated; serialized parameter fields on components have been removed and edit-time configuration now lives in code.

```csharp
using GameFrameX.Runtime;

// The one injection point: before the first scene load (BeforeSceneLoad)
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
static void ConfigureFramework()
{
    // Set only the deltas; everything else uses package defaults
    GameFrameXOptions.Configure(new DownloadOptions { Timeout = 60f });
}
```

- Priority: `Configure`-injected values > Options field defaults; consumed once when the module is first instantiated.
- Timing: after startup full instantiation, any `Configure` throws a `GameFrameworkException` — a mistimed configuration never fails silently. The auto host is created at `AfterSceneLoad`, still after this `BeforeSceneLoad` call.
- Tiers: fields are real-time by default (mutable at runtime via module APIs); startup-only fields (agent counts, Helper types, …) are marked `[GameFrameXStartupOnly]` — runtime changes are rejected with a Warning.
- Unconsumed diagnostics: on `Reset` / `Shutdown`, options registered but never consumed by any module produce a Warning (usually an uninstalled package or a structurally disabled component).

#### Options Contributors (silent discovery, since C28)

Prefer zero startup boilerplate? A plain class implementing `IGameFrameXOptionsContributor` is silently discovered by the runtime scanner and applied automatically — no attributes, no bootstrap, no registration code:

```csharp
using GameFrameX.Runtime;
using UnityEngine.Scripting;

// Contributor classes only Configure; keep them side-effect free with a public parameterless constructor
[Preserve]
public sealed class MyGameOptionsContributor : IGameFrameXOptionsContributor
{
    public void ConfigureOptions(GameFrameXOptionsContributorContext context)
    {
        context.Configure(new DownloadOptions { Timeout = 60f });
    }
}
```

- Manual-first: explicit `GameFrameXOptions.Configure` always wins — already-configured types are skipped per type by contributors with a `[GameFrameX]` diagnostic, regardless of execution order inside the BeforeSceneLoad phase.
- Timing: applied before the earliest module instantiation, from the single `BeforeSceneLoad` entry point, once per domain.
- Stripping guard: under IL2CPP / mini-game stripping, implementations must be kept with `[Preserve]` (as above) or added to the cropping list, otherwise the type may be stripped silently. Likewise, classes implementing `IGameFrameXRuntimeOverrideProvider` no longer need manual `RegisterProvider` — scanning is enough (types without a public parameterless constructor are skipped with a Warning).


#### Late-loaded assemblies (incremental registration, since C29)

Assemblies loaded after startup (hot-update / runtime loading) register their manager descriptors automatically on `AssemblyLoad` — a late `IXxxManager` implementation resolves via `GetModule<IXxxManager>()` with zero manual calls, including the naming-convention fallback, because `Utility.Assembly` now live-fetches the AppDomain instead of a startup snapshot. Every incremental registration logs an `[GameFrameX]` diagnostic with the assembly name and descriptor count; Editor / Tests assemblies are skipped by the same gate as the full scan, and re-registering the same assembly name (reload after hot-update unload) is idempotent.

- Scope is manager descriptors only: `GameFrameworkComponent`s inside late assemblies are NOT auto-mounted — mount timing is scene-state bound, so add them manually with `AddComponent` when needed.
- Options contributors inside late assemblies are rejected loudly by the configure-timing guard (modules are already instantiated) — there is deliberately no semi-silent channel for them.
- Subscription lifecycle: re-established (unsubscribe + subscribe) at `SubsystemRegistration` so repeated Play with Domain Reload disabled never double-subscribes; deliberately NOT unsubscribed at `GameFrameworkEntry.Shutdown` — after a shutdown clears the type maps, newly loaded assemblies must still be able to register (Restart semantics).

Override Helper implementations with factory closures (custom references win, replacing the former Inspector type-name dropdown; UI backend packages FairyGUI / UGUI self-register defaults via the backend registry):

```csharp
GameFrameXOptions.Configure(new UIOptions
{
    UIFormHelperFactory = () => new MyUIFormHelper(),
});
```

Live view: components remain thin entries (pure forwarding) and the Inspector no longer carries parameters; during Play you can inspect the effective configuration in the module debug view, and runtime adjustments always go through the `GetModule<T>()` / `GameApp.X` module APIs.

---

## Usage Examples

### Object Pool System

```csharp
// Get object pool component
var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

// Create object pool
objectPool.CreatePool<MyObject>("MyObjectPool", 10, 100);

// Spawn object from pool
var obj = objectPool.Spawn<MyObject>("MyObjectPool");

// Return object to pool
objectPool.Unspawn(obj);

// Destroy object pool
objectPool.DestroyPool("MyObjectPool");
```

### Extension Methods

```csharp
// Transform extensions
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// Vector3 extensions
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);

// GameObject extensions
gameObject.SetActiveOptimized(true);
gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
```

### Utility Classes

```csharp
// File operations
FileHelper.WriteAllBytes("path/to/file", data);
byte[] content = FileHelper.ReadAllBytes("path/to/file");
string size = FileHelper.GetBytesSize(1024 * 1024); // "1MB"

// AES encryption / decryption
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// Hash calculation
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSON serialization
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

### Event System

```csharp
// Define event args
public class PlayerDeadEventArgs : BaseEventArgs
{
    public int PlayerId { get; set; }
    public float Damage { get; set; }
}

// Subscribe to event
GameEntry.Event.Subscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);

// Fire event
GameEntry.Event.Fire(this, PlayerDeadEventArgs.Create(playerId, damage));

// Unsubscribe from event
GameEntry.Event.Unsubscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);
```

### Editor Tools

#### Mini Game Platform Adaptation

Switch between mini-game platforms from the Unity menu:

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Domestic Mini Games/
│   │   ├── Enable WeChat Mini Game
│   │   ├── Enable Alipay Mini Game
│   │   ├── Enable DouYin Mini Game
│   │   ├── Enable KuaiShou Mini Game
│   │   ├── Enable Baidu Mini Game
│   │   ├── Enable JingDong Mini Game
│   │   ├── Enable Meituan Mini Game
│   │   ├── Enable Taobao Mini Game
│   │   └── Enable Bilibili Mini Game
│   ├── International Mini Games/
│   │   ├── Enable Discord Mini Game
│   │   ├── Enable YouTube Mini Game
│   │   ├── Enable Facebook Mini Game
│   │   ├── Enable Google Play Mini Game
│   │   ├── Enable TikTok Mini Game
│   │   ├── Enable CrazyGames Mini Game
│   │   └── Enable Poki Mini Game
│   ├── Device OEMs/
│   │   ├── Enable Huawei Mini Game
│   │   ├── Enable OPPO Mini Game
│   │   ├── Enable Vivo Mini Game
│   │   └── Enable Xiaomi Mini Game
│   └── Game Platforms/
│       └── Enable TapTap Mini Game
```

#### Build Tools

```
GameFrameX/
├── Build Hotfix
├── Build Product
└── Build WebGL With HybridCLR
```

---

## Architecture

GameFrameX uses a three-layer architecture; each module has its own responsibility and they work together.

### Runtime

Core runtime code providing all functionality needed at game runtime.

```
Runtime/
├── Base/                          Framework Core Foundation
│   ├── DataStruct/                Data Structures
│   ├── EventPool/                 Event Pool System
│   ├── Log/                       Logging System
│   ├── ReferencePool/             Reference Pool System
│   ├── TaskPool/                  Task Pool System
│   ├── Variable/                  Variable System
│   ├── Version/                   Version Management
│   ├── BaseComponent.cs
│   ├── GameEntry.cs
│   ├── GameFrameworkComponent.cs
│   ├── GameFrameworkEntry.cs
│   ├── GameFrameworkEventArgs.cs
│   ├── GameFrameworkException.cs
│   ├── GameFrameworkLinkedList.cs
│   ├── GameFrameworkMultiDictionary.cs
│   ├── GameFrameworkMonoSingleton.cs
│   ├── GameFrameworkSerializer.cs
│   ├── GameFrameworkSingleton.cs
│   ├── IGameFrameworkModule.cs
│   └── ObjectDontDestroyOnLoad.cs
├── Extension/                     Extension Methods Library
│   ├── Extension/                 Common extensions
│   ├── SequenceReader/            Sequence Reader
│   ├── UnityEngine.GameObject/    GameObject extension
│   └── UnityEngine/               Unity type extensions (Transform, Vector2/3/4)
├── Helper/                        Helper classes (Application, Camera, File, Path,
│                                  Math, Random, Timer, Network, JSON, Renderer,
│                                  Zip, DoTween, Timer split by period, etc.)
├── ObjectPool/                    Object Pool System
├── Property/                      Property System (BindableProperty)
├── ReferencePool/                 Reference Pool System
└── Utility/                       Utility classes (Encryption, Compression, Hash,
                                   JSON, Marshal, Text, Verifier, Const, IdGenerator)
```

### Plugins

Native platform plugins and bundled third-party .NET libraries.

```
Plugins/
├── iOS/                           iOS native bridge (GameFrameX.mm, TrackingAuthorization)
├── ICSharpCode.SharpZipLib.dll    ZIP compression library
├── Microsoft.NET.StringTools.dll   String tools
├── System.Buffers.dll             Memory buffers
├── System.IO.Pipelines.dll        IO pipelines
├── System.Memory.dll              Memory management
└── System.Runtime.CompilerServices.Unsafe.dll   Runtime support
```

### Editor

Editor tools and extensions for development efficiency.

```
Editor/
├── BuildHotfix/                    Hotfix build tools (HybridCLR)
├── BuildProduct/                   Product build with pre / post hooks
├── BuildWebGLTools/                WebGL build optimization (HybridCLR)
├── Cropping/                       Image cropping tool
├── Inspector/                      Custom inspectors for Base / ObjectPool / ReferencePool
├── InspectorLockShortcut/          Inspector panel lock shortcut
├── MiniGame/                       Mini-game adaptation (21 platforms)
│   ├── DomesticMiniGames/          WeChat, Alipay, DouYin, KuaiShou, Baidu, JD, Meituan, Taobao, Bilibili
│   ├── InternationalMiniGames/     Discord, YouTube, Facebook, Google Play, TikTok, CrazyGames, Poki
│   ├── DeviceOEMs/                 Huawei, OPPO, Vivo, Xiaomi
│   └── GamePlatforms/              TapTap
├── Welcome/                        Welcome window
└── Misc/                           Logging redirection, scripting define symbols, type utility, open folder
```

---

## Platform Support

### Operating Systems

| Platform | Status | Supported Version |
| -------- | ------ | ----------------- |
| Windows  | Supported | Unity 2019.4+ |
| macOS    | Supported | Unity 2019.4+ |
| Linux    | Supported | Unity 2019.4+ |
| iOS      | Supported | Unity 2019.4+ |
| Android  | Supported | Unity 2019.4+ |
| WebGL    | Supported | Unity 2019.4+ |

### Mini Game Platform Adaptation

GameFrameX provides one-click mini-game platform adaptation for 21 mainstream platforms worldwide.

#### Domestic Mini Games (9)

| Platform | Define Symbol | Region | Menu Priority |
| -------- | ------------- | ------ | ------------- |
| WeChat Mini Game | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | China | 2000 |
| Alipay Mini Game | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | China | 2400 |
| DouYin Mini Game | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | China | 2100 |
| KuaiShou Mini Game | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | China | 2200 |
| Baidu Mini Game | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | China | 2300 |
| JD Mini Game | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | China | 2500 |
| Taobao Mini Program | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | China | 2600 |
| Meituan Mini Game | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | China | 2800 |
| Bilibili Mini Game | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | China | 2900 |

#### International Mini Games (7)

| Platform | Define Symbol | Region | Menu Priority |
| -------- | ------------- | ------ | ------------- |
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | Global | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | Global | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | Global | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | Global | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | Global | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | Global | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | Global | 3700 |

#### Device OEMs (4)

| Platform | Define Symbol | Region | Menu Priority |
| -------- | ------------- | ------ | ------------- |
| Huawei Mini Game | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | China | 3400 |
| OPPO Mini Game | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | China | 3200 |
| Vivo Mini Game | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | China | 3100 |
| Xiaomi Mini Game | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | China | 3300 |

#### Game Platforms (1)

| Platform | Define Symbol | Region | Menu Priority |
| -------- | ------------- | ------ | ------------- |
| TapTap Mini Game | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | China | 2700 |

#### Define Symbol Details

- Unified define: `ENABLE_WEBGL_MINI_GAME` shared by all mini-game platforms.
- Platform defines: each platform has independent defines for conditional compilation.
- Mutex mechanism: enabling one mini-game platform automatically disables others.
- Menu path: `GameFrameX/Scripting Define Symbols/[Category]/Enable [Platform] Mini Game`.

---

## Dependencies

Declared in `package.json`:

- `com.gameframex.unity.litjson` 1.2.2
- `com.gameframex.unity.readassets` 1.2.1
- `com.gameframex.unity.cysharp.unitask` 2.6.2
- `com.unity.modules.androidjni` 1.0.0
- `com.unity.modules.animation` 1.0.0
- `com.unity.modules.assetbundle` 1.0.0
- `com.unity.modules.audio` 1.0.0
- `com.unity.modules.imgui` 1.0.0
- `com.unity.modules.jsonserialize` 1.0.0
- `com.unity.modules.unitywebrequest` 1.0.0

Dev tools: `semantic-release`, `@semantic-release/changelog`, `@semantic-release/git`.

---

## Documentation & Resources

- Documentation: https://gameframex.doc.alianblank.com
- API Reference: https://gameframex.doc.alianblank.com/api
- Example Projects: https://github.com/GameFrameX/Examples
- Video Tutorials: https://www.youtube.com/channel/UCD9QhSFJ5xZkn5NTSV-DVAw

---

## Community & Support

[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/GameFrameX/gameframex)
[![Discord](https://img.shields.io/badge/Discord-5865F2?style=for-the-badge&logo=discord&logoColor=white)](https://discord.gg/VDWUjWMDw9)
[<img src="https://cdn.jsdelivr.net/npm/devicon@2/icons/linkedin/linkedin-original.svg" height="28" alt="LinkedIn" />](https://www.linkedin.com/in/alianblank)
[![Reddit](https://img.shields.io/badge/Reddit-FF4500?style=for-the-badge&logo=reddit&logoColor=white)](https://www.reddit.com/r/GameFrameX/)
[![X](https://img.shields.io/badge/X-000000?style=for-the-badge&logo=x&logoColor=white)](https://x.com/alian_blank)
[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/channel/UCD9QhSFJ5xZkn5NTSV-DVAw)
[![Bluesky](https://img.shields.io/badge/Bluesky-0285FF?style=for-the-badge&logo=bluesky&logoColor=white)](https://bsky.app/profile/alianblank.bsky.social)
[![Bilibili](https://img.shields.io/badge/Bilibili-00A1D6?style=for-the-badge&logo=bilibili&logoColor=white)](https://www.bilibili.com/video/BV1yrpeepEn7)
[![Gitee](https://img.shields.io/badge/Gitee-C71D23?style=for-the-badge&logo=gitee&logoColor=white)](https://gitee.com/GameFrameX/gameframex)
![QQ](https://img.shields.io/badge/QQ-467608841%2F233840761-EB1923?style=for-the-badge&logo=qq&logoColor=white)

- GitHub Issues: https://github.com/GameFrameX/com.gameframex.unity/issues
- GitHub Discussions: https://github.com/GameFrameX/com.gameframex.unity/discussions

---

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for the full release history.

---

## License

See [LICENSE.md](LICENSE.md) for the full license text.

---

<div align="center">

If this project helps you, please give it a Star.

[Back to Top](#gameframex-unity-package)

</div>
