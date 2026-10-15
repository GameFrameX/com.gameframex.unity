<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity 包

[![License](https://img.shields.io/badge/license-blue.svg)](LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

[![Discord](https://img.shields.io/badge/-5865F2?logo=discord&logoColor=white)](https://discord.gg/VDWUjWMDw9)
[![GitHub](https://img.shields.io/badge/-181717?logo=github&logoColor=white)](https://github.com/GameFrameX/gameframex)
[![Bilibili](https://img.shields.io/badge/-00A1D6?logo=bilibili&logoColor=white)](https://www.bilibili.com/video/BV1yrpeepEn7)
[![Gitee](https://img.shields.io/badge/-C71D23?logo=gitee&logoColor=white)](https://gitee.com/GameFrameX/gameframex)

**独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使**

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#quick-start) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

<br />

</div>

---

## 项目简介

GameFrameX 是专为独立游戏开发者设计的现代化 Unity 游戏框架，提供完整的前后端一体化解决方案。框架采用三层模块化架构，内置丰富的游戏开发工具与组件，帮助开发者快速交付高质量的游戏项目。

`com.gameframex.unity` 是核心包，包含运行时内核（事件池、引用池、任务池、对象池、变量 / 属性系统、扩展库、Helper、工具类）、编辑器工具链（构建管线与热更新、WebGL + HybridCLR 工具、小游戏平台适配、欢迎窗口、Inspector 增强、Inspector 锁定快捷键、裁剪工具、Scripting Define Symbol 管理器）、iOS 原生桥接插件以及附带的 .NET 库（SharpZipLib、System.Memory / Buffers / Pipelines）。它是所有其它 GameFrameX Unity 包的基础依赖。

### 功能特性

- 三层架构：Runtime / Plugins / Editor 职责清晰、解耦良好。
- 运行时工具集：EventPool、ReferencePool、TaskPool、ObjectPool、Variable / Property、扩展库、Helper 类。
- 编辑器工具链：BuildProduct / BuildHotfix 构建管线、BuildWebGLTools + HybridCLR、21 个小游戏平台适配、欢迎窗口、Inspector 增强。
- 工具层：加密（AES / RSA / DSA）、压缩（ZIP）、哈希（MD5 / SHA1 / HMAC / CRC）、JSON、文件 / 路径 / 网络助手、ID 生成器、文本处理。
- 支持热更新：标准构建流程中集成 HybridCLR 热更新构建管线。
- 多平台：Windows、macOS、Linux、iOS、Android、WebGL，以及全球 21 个小游戏平台。

### 系统要求

- Unity：2019.4 及以上
- 平台：Windows、macOS、Linux、iOS、Android、WebGL
- .NET：.NET Standard 2.0+

---

## 快速开始

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
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
       "com.gameframex.unity": "3.0.0"
     }
   }
   ```

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex.unity` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
     "com.gameframex.unity": "https://github.com/GameFrameX/com.gameframex.unity.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/GameFrameX/com.gameframex.unity.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。

> 通过方式 2–4 安装时，编辑器加载后会自动检测并补全 `manifest.json` 中缺失的 GameFrameX scoped registry 配置（写前备份为 `manifest.json.gameframex.bak`）；已有第三方 registry（如 OpenUPM）覆盖该 scope 时跳过，`-batchmode`（CI）环境下不自动改动。
> 同时会默认注册 OpenUPM registry（`package.openupm.com`，scopes 留空，不改动已存在的同 URL 条目配置）。

### 基础使用

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 从框架入口获取组件
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

        // 获取引用池组件
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();

        // 使用引擎类型的扩展方法
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

### 零配置启动（自动注入）

安装本包即是唯一的启动步骤。`GameFrameXRuntimeBootstrap` 以 `RuntimeInitializeOnLoadMethod(BeforeSceneLoad)` 标记自身，框架在首场景加载前自动注入；`AfterSceneLoad` 时创建隐藏的 `[GameFrameX]` Host 节点，并在其上自动挂载全部 GameFrameX 组件。无需改场景、无需预制体、无需 Boot 脚本、无需写代码——任意场景零改动即可启动。

场景里手工摆放的 GameFrameX 组件（BaseComponent 及其兄弟组件）会被自动检测到并**自我抑制**：不注册、不初始化、不驱动，并各自输出带 `[GameFrameX]` 前缀的告警。迁移做法：删掉场景里手工摆放的 GFX 节点，把启动权交给自动注入。

模块参数通过 `RuntimeInitializeOnLoadMethod(BeforeSceneLoad)` 中的 `GameFrameXOptions.Configure<T>(options)` 注入——见下一节。

### 模块 Options 启动配置

各模块提供 `XxxOptions` 启动配置（字段默认值同原 Inspector 序列化默认），经 `GameFrameXOptions.Configure` 在模块首次实例化前一次性注入；组件上的序列化参数字段已移除，编辑期配置入口统一收敛到代码。

```csharp
using GameFrameX.Runtime;

// 唯一注入时机：首场景加载前（BeforeSceneLoad）
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
static void ConfigureFramework()
{
    // 只写差异字段，其余用包默认值
    GameFrameXOptions.Configure(new DownloadOptions { Timeout = 60f });
}
```

- 优先级：`Configure` 注入值 > Options 字段默认值；模块首次实例化时一次性消费。
- 时机：启动期全量实例化完成后任何 `Configure` 都会抛出 `GameFrameworkException`——配置时机错误不会静默。自动 Host 在 `AfterSceneLoad` 创建，仍晚于此处 `BeforeSceneLoad` 的调用。
- 分档：字段默认为实时型（运行期经模块公开 API 修改）；启动期型字段（代理数量、Helper 类型等）以 `[GameFrameXStartupOnly]` 标注，运行期修改会被拒绝并输出 Warning。
- 未消费诊断：`Reset` / `Shutdown` 时对"已注册但从未被任何模块消费"的 Options 输出 Warning（成因通常是包未安装或组件被结构性禁用）。

#### Options 贡献者（静默发现，C28 起）

不想写任何启动样板？实现 `IGameFrameXOptionsContributor` 的普通类会被运行时扫描器静默发现并自动应用——无属性、无 Bootstrap、无注册代码：

```csharp
using GameFrameX.Runtime;

// 贡献者类只做 Configure，不承载初始化副作用；保持 public 无参构造
[UnityEngine.Scripting.Preserve]
public sealed class MyGameOptionsContributor : IGameFrameXOptionsContributor
{
    public void ConfigureOptions(GameFrameXOptionsContributorContext context)
    {
        context.Configure(new DownloadOptions { Timeout = 60f });
    }
}
```

- 手动优先：游戏侧显式 `GameFrameXOptions.Configure` 永远胜出——已 Configure 的类型贡献者按类型粒度跳过并输出 `[GameFrameX]` 诊断，与 BeforeSceneLoad 阶段内执行顺序无关。
- 应用时机：最早模块实例化之前，唯一入口 `BeforeSceneLoad`，每 domain 一次。
- 防裁剪：IL2CPP / 小游戏裁剪环境下，实现类需加 `[UnityEngine.Scripting.Preserve]`（如上）或纳入裁剪保护清单，否则类型可能被裁掉导致静默失效。
- 同理，实现 `IGameFrameXRuntimeOverrideProvider` 的类也不再需要手动 `RegisterProvider`，扫描即生效（缺 public 无参构造会输出 Warning 后跳过）。

#### 迟加载程序集增量注册（C29 起）

启动后加载的程序集（热更 / 运行期加载）在 `AssemblyLoad` 时自动注册其中的 Manager 描述符——迟加载的 `IXxxManager` 实现零手动调用即可 `GetModule<IXxxManager>()` 命中（含命名约定兜底路径，因为 `Utility.Assembly` 由启动快照改为活取 AppDomain）。每次增量注册输出带程序集名与描述符数的 `[GameFrameX]` 诊断；Editor / Tests 程序集与全量扫描同门跳过，同名程序集重复注册（热更卸载后再加载）幂等去重。

- 仅增量注册 Manager 描述符：迟加载程序集内的 `GameFrameworkComponent` **不自动补挂**——挂载时机与场景状态强相关，需要时手动 `AddComponent`。
- 迟加载程序集内的 Options 贡献者会被 Configure 时机守卫响亮拒绝（模块已实例化）——刻意不提供半静默通道。
- 订阅生命周期：`SubsystemRegistration` 先退订再订（关闭 Domain Reload 的重复 Play 不双订阅）；`GameFrameworkEntry.Shutdown` 后**刻意不退订**——Shutdown 清空映射后新加载程序集仍应能注册（Restart 语义）。


用工厂闭包覆盖 Helper 实现（Custom 引用优先，替代原 Inspector 的类型名下拉；UI 后端包 FairyGUI / UGUI 经注册表自注册默认实现）：

```csharp
GameFrameXOptions.Configure(new UIOptions
{
    UIFormHelperFactory = () => new MyUIFormHelper(),
});
```

活视图：组件保留为薄入口（纯转发），Inspector 不再承载参数；Play 期可在模块调试视图中查看当前生效配置，运行期调整一律走 `GetModule<T>()` / `GameApp.X` 模块 API。

---

## 使用示例

### 对象池系统

```csharp
// 获取对象池组件
var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

// 创建对象池
objectPool.CreatePool<MyObject>("MyObjectPool", 10, 100);

// 从池中获取对象
var obj = objectPool.Spawn<MyObject>("MyObjectPool");

// 归还对象到池中
objectPool.Unspawn(obj);

// 销毁对象池
objectPool.DestroyPool("MyObjectPool");
```

### 扩展方法

```csharp
// Transform 扩展
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// Vector3 扩展
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);

// GameObject 扩展
gameObject.SetActiveOptimized(true);
gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
```

### 实用工具类

```csharp
// 文件操作
FileHelper.WriteAllBytes("path/to/file", data);
byte[] content = FileHelper.ReadAllBytes("path/to/file");
string size = FileHelper.GetBytesSize(1024 * 1024); // "1MB"

// AES 加密解密
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// 哈希计算
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSON 序列化
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

### 事件系统

```csharp
// 定义事件参数
public class PlayerDeadEventArgs : BaseEventArgs
{
    public int PlayerId { get; set; }
    public float Damage { get; set; }
}

// 订阅事件
GameEntry.Event.Subscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);

// 发送事件
GameEntry.Event.Fire(this, PlayerDeadEventArgs.Create(playerId, damage));

// 取消订阅
GameEntry.Event.Unsubscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);
```

### 编辑器工具

#### 小游戏平台适配

在 Unity 菜单中可一键切换小游戏平台：

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Domestic Mini Games(国内小游戏)/
│   │   ├── Enable WeChat Mini Game (开启[微信小游戏]适配)
│   │   ├── Enable Alipay Mini Game (开启[支付宝小游戏]适配)
│   │   ├── Enable DouYin Mini Game (开启[抖音小游戏]适配)
│   │   ├── Enable KuaiShou Mini Game (开启[快手小游戏]适配)
│   │   ├── Enable Baidu Mini Game (开启[百度小游戏]适配)
│   │   ├── Enable JingDong Mini Game (开启[京东小游戏]适配)
│   │   ├── Enable Meituan Mini Game (开启[美团小游戏]适配)
│   │   ├── Enable Taobao Mini Game (开启[淘宝小游戏]适配)
│   │   └── Enable Bilibili Mini Game (开启[Bilibili小游戏]适配)
│   ├── International Mini Games(国际小游戏)/
│   │   ├── Enable Discord Mini Game
│   │   ├── Enable YouTube Mini Game
│   │   ├── Enable Facebook Mini Game
│   │   ├── Enable Google Play Mini Game
│   │   ├── Enable TikTok Mini Game
│   │   ├── Enable CrazyGames Mini Game
│   │   └── Enable Poki Mini Game
│   ├── Device OEMs(设备厂商)/
│   │   ├── Enable Huawei Mini Game (开启[华为小游戏]适配)
│   │   ├── Enable OPPO Mini Game (开启[OPPO小游戏]适配)
│   │   ├── Enable Vivo Mini Game (开启[vivo小游戏]适配)
│   │   └── Enable Xiaomi Mini Game (开启[小米小游戏]适配)
│   └── Game Platforms(游戏平台)/
│       └── Enable TapTap Mini Game (开启[TapTap小游戏]适配)
```

#### 构建工具

```
GameFrameX/
├── Build Hotfix (构建热更新)
├── Build Product (构建产品)
└── Build WebGL With HybridCLR (WebGL热更新构建)
```

---

## 架构概览

GameFrameX 采用清晰的三层架构，每个模块各司其职、协同工作。

### 运行时（Runtime）

运行时核心代码，提供游戏运行期所需的所有功能。

```
Runtime/
├── Base/                          框架核心基础
│   ├── DataStruct/                数据结构
│   ├── EventPool/                 事件池系统
│   ├── Log/                       日志系统
│   ├── ReferencePool/             引用池系统
│   ├── TaskPool/                  任务池系统
│   ├── Variable/                  变量系统
│   ├── Version/                   版本管理
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
├── Extension/                     扩展方法库
│   ├── Extension/                 通用扩展
│   ├── SequenceReader/            Sequence Reader
│   ├── UnityEngine.GameObject/    GameObject 扩展
│   └── UnityEngine/               Unity 类型扩展（Transform、Vector2/3/4）
├── Helper/                        Helper 类（Application、Camera、File、Path、
│                                  Math、Random、Timer、Network、JSON、Renderer、
│                                  Zip、DoTween、按周期拆分的 Timer 等）
├── ObjectPool/                    对象池系统
├── Property/                      属性系统（BindableProperty）
├── ReferencePool/                 引用池系统
└── Utility/                       工具类（Encryption、Compression、Hash、JSON、
                                   Marshal、Text、Verifier、Const、IdGenerator）
```

### 插件层（Plugins）

原生平台插件与内置的第三方 .NET 库。

```
Plugins/
├── iOS/                           iOS 原生桥接（GameFrameX.mm、TrackingAuthorization）
├── ICSharpCode.SharpZipLib.dll    ZIP 压缩库
├── Microsoft.NET.StringTools.dll  字符串工具
├── System.Buffers.dll             内存缓冲
├── System.IO.Pipelines.dll        IO 管线
├── System.Memory.dll              内存管理
└── System.Runtime.CompilerServices.Unsafe.dll   运行时支持
```

### 编辑器（Editor）

编辑器工具与扩展，提升开发效率。

```
Editor/
├── BuildHotfix/                   热更新构建工具（HybridCLR）
├── BuildProduct/                  产品构建（带 Pre/Post Hook）
├── BuildWebGLTools/               WebGL 构建优化（HybridCLR）
├── Cropping/                      图像裁剪工具
├── Inspector/                     Base / ObjectPool / ReferencePool 自定义 Inspector
├── InspectorLockShortcut/         Inspector 面板锁定快捷键
├── MiniGame/                      小游戏平台适配（21 个平台）
│   ├── DomesticMiniGames/         微信、支付宝、抖音、快手、百度、京东、美团、淘宝、Bilibili
│   ├── InternationalMiniGames/    Discord、YouTube、Facebook、Google Play、TikTok、CrazyGames、Poki
│   ├── DeviceOEMs/                华为、OPPO、Vivo、小米
│   └── GamePlatforms/             TapTap
├── Welcome/                       欢迎窗口
└── Misc/                          日志重定向、Scripting Define Symbols、类型工具、打开文件夹
```

---

## 平台支持

### 操作系统

| 平台 | 状态 | 支持版本 |
|------|------|----------|
| Windows | 支持 | Unity 2019.4+ |
| macOS | 支持 | Unity 2019.4+ |
| Linux | 支持 | Unity 2019.4+ |
| iOS | 支持 | Unity 2019.4+ |
| Android | 支持 | Unity 2019.4+ |
| WebGL | 支持 | Unity 2019.4+ |

### 小游戏平台适配

GameFrameX 提供一键切换的小游戏平台适配，覆盖全球 21 个主流小游戏平台。

#### 国内小游戏（9 个）

| 平台 | 宏定义 | 地区 | 菜单优先级 |
|------|--------|------|-----------|
| 微信小程序 | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 中国 | 2000 |
| 支付宝小程序 | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 中国 | 2400 |
| 抖音小程序 | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 中国 | 2100 |
| 快手小程序 | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 中国 | 2200 |
| 百度小程序 | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 中国 | 2300 |
| 京东小程序 | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 中国 | 2500 |
| 淘宝小程序 | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 中国 | 2600 |
| 美团小程序 | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 中国 | 2800 |
| Bilibili 小程序 | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 中国 | 2900 |

#### 国际小游戏（7 个）

| 平台 | 宏定义 | 地区 | 菜单优先级 |
|------|--------|------|-----------|
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 全球 | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 全球 | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 全球 | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 全球 | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 全球 | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 全球 | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 全球 | 3700 |

#### 设备厂商（4 个）

| 平台 | 宏定义 | 地区 | 菜单优先级 |
|------|--------|------|-----------|
| 华为小游戏 | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 中国 | 3400 |
| OPPO 小游戏 | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 中国 | 3200 |
| vivo 小游戏 | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 中国 | 3100 |
| 小米小游戏 | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 中国 | 3300 |

#### 游戏平台（1 个）

| 平台 | 宏定义 | 地区 | 菜单优先级 |
|------|--------|------|-----------|
| TapTap 小游戏 | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 中国 | 2700 |

#### 宏定义说明

- 统一宏定义：`ENABLE_WEBGL_MINI_GAME`，所有小游戏平台共享。
- 平台宏定义：各平台独立宏定义，用于条件编译。
- 互斥机制：开启任一小游戏平台会自动关闭其他平台。
- 菜单位置：`GameFrameX/Scripting Define Symbols/[分类]/Enable [平台名称] Mini Game`。

---

## 依赖

`package.json` 中声明：

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

Dev 工具：`semantic-release`、`@semantic-release/changelog`、`@semantic-release/git`。

---

## 文档与资源

- 文档：https://gameframex.doc.alianblank.com
- API 参考：https://gameframex.doc.alianblank.com/api
- 示例项目：https://github.com/GameFrameX/Examples
- 视频教程：https://www.youtube.com/channel/UCD9QhSFJ5xZkn5NTSV-DVAw

---

## 社区与支持

![QQ](https://img.shields.io/badge/QQ-467608841%2F233840761-EB1923?style=for-the-badge&logo=qq&logoColor=white)
[![Bilibili](https://img.shields.io/badge/Bilibili-00A1D6?style=for-the-badge&logo=bilibili&logoColor=white)](https://www.bilibili.com/video/BV1yrpeepEn7)
[![Gitee](https://img.shields.io/badge/Gitee-C71D23?style=for-the-badge&logo=gitee&logoColor=white)](https://gitee.com/GameFrameX/gameframex)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/GameFrameX/gameframex)
[![Discord](https://img.shields.io/badge/Discord-5865F2?style=for-the-badge&logo=discord&logoColor=white)](https://discord.gg/VDWUjWMDw9)
[<img src="https://cdn.jsdelivr.net/npm/devicon@2/icons/linkedin/linkedin-original.svg" height="28" alt="LinkedIn" />](https://www.linkedin.com/in/alianblank)
[![Reddit](https://img.shields.io/badge/Reddit-FF4500?style=for-the-badge&logo=reddit&logoColor=white)](https://www.reddit.com/r/GameFrameX/)
[![X](https://img.shields.io/badge/X-000000?style=for-the-badge&logo=x&logoColor=white)](https://x.com/alian_blank)
[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/channel/UCD9QhSFJ5xZkn5NTSV-DVAw)
[![Bluesky](https://img.shields.io/badge/Bluesky-0285FF?style=for-the-badge&logo=bluesky&logoColor=white)](https://bsky.app/profile/alianblank.bsky.social)

- 问题反馈：https://github.com/GameFrameX/com.gameframex.unity/issues
- 功能建议：https://github.com/GameFrameX/com.gameframex.unity/discussions

---

## 更新日志

完整版本历史请参见 [CHANGELOG.md](CHANGELOG.md)。

---

## 开源协议

完整许可证文本请参见 [LICENSE.md](LICENSE.md)。

---

<div align="center">

如果这个项目对你有帮助，请给我们一个 Star。

[回到顶部](#gameframex-unity-包)

</div>
