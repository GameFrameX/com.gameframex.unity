<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity Package

[![Version](https://img.shields.io/badge/version-1.3.6-blue.svg)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity](https://img.shields.io/badge/Unity-2019.4+-green.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT+Apache%202.0-orange.svg)](LICENSE.md)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

**独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使**

[📖 文档](https://gameframex.doc.alianblank.com) • [🚀 快速开始](#快速开始) • [💬 QQ群: 467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)

---

🌐 **语言**: [English](./README.md) | [繁體中文](./README.zh-TW.md) | [中文](./README.zh-CN.md) | [日本語](./README.ja.md) | [한국어](./README.ko.md)

---

## 📑 目录导航

- [🏗️ 项目简介](#🏗️-项目简介)
- [📂 架构概览](#📂-架构概览)
  - [Runtime 模块](#runtime-模块)
  - [Plugins 模块](#plugins-模块)
  - [Editor 模块](#editor-模块)
- [🚀 快速开始](#🚀-快速开始)
- [💡 使用示例](#💡-使用示例)
  - [Runtime 使用示例](#runtime-使用示例)
  - [Editor 工具使用](#editor-工具使用)
- [🎯 平台支持](#🎯-平台支持)
- [📚 文档与资源](#📚-文档与资源)
- [🤝 社区与支持](#🤝-社区与支持)
- [📄 开源协议](#📄-开源协议)

</div>

---

## 🏗️ 项目简介

GameFrameX 是一个专为独立游戏开发者设计的现代化 Unity 游戏框架，提供完整的前后端一体化解决方案。框架采用**三层模块化架构**设计，内置丰富的游戏开发工具和组件，帮助开发者快速构建高质量的游戏项目。

### 🎯 核心特性

- 🏗️ **三层架构** - Runtime（运行时）、Plugins（插件）、Editor（编辑器）清晰分层
- 🔧 **丰富工具集** - 内置多种开发辅助工具和编辑器扩展
- 📦 **对象池管理** - 高效的内存管理和对象复用机制
- 🎨 **扩展方法库** - 丰富的 Unity 引擎扩展方法
- 🛠️ **实用工具类** - 涵盖加密、压缩、网络等常用功能
- 📱 **多平台支持** - 支持 PC、移动端、WebGL 等多平台部署
- 🔥 **热更新支持** - 内置 HybridCLR 热更新解决方案
- 🎮 **小游戏适配** - 一键切换多平台小游戏环境

### 📋 系统要求

- **Unity 版本**: 2019.4 或更高版本
- **平台支持**: Windows, macOS, Linux, iOS, Android, WebGL
- **.NET 版本**: .NET Standard 2.0+

---

## 📂 架构概览

GameFrameX 采用清晰的三层架构设计，每个模块各司其职，协同工作。

### 📦 Runtime 模块

运行时核心代码，提供游戏运行时所需的所有功能。

```
Runtime/
├── Base/                          # 框架核心基础
│   ├── DataStruct/               # 数据结构
│   │   └── TypeNamePair.cs       # 类型名称对
│   ├── EventPool/                # 事件池系统
│   │   ├── BaseEventArgs.cs      # 事件参数基类
│   │   ├── EventPool.EventNode.cs # 事件节点
│   │   ├── EventPool.cs          # 事件池核心
│   │   └── EventPoolMode.cs      # 事件池模式
│   ├── Log/                      # 日志系统
│   │   ├── GameFrameworkLog.ILogHelper.cs # 日志接口
│   │   ├── GameFrameworkLog.cs   # 日志核心
│   │   └── GameFrameworkLogLevel.cs # 日志级别
│   ├── ReferencePool/            # 引用池系统
│   │   ├── IReference.cs        # 引用接口
│   │   ├── ReferencePool.ReferenceCollection.cs # 引用集合
│   │   ├── ReferencePool.cs      # 引用池核心
│   │   └── ReferencePoolInfo.cs   # 引用池信息
│   ├── TaskPool/                 # 任务池系统
│   │   ├── ITaskAgent.cs        # 任务代理接口
│   │   ├── StartTaskStatus.cs    # 任务状态
│   │   ├── TaskBase.cs          # 任务基类
│   │   ├── TaskInfo.cs          # 任务信息
│   │   ├── TaskPool.cs          # 任务池核心
│   │   └── TaskStatus.cs        # 任务状态
│   ├── Variable/                 # 变量系统
│   │   ├── GenericVariable.cs    # 泛型变量
│   │   └── Variable.cs          # 变量基类
│   ├── Version/                  # 版本管理
│   │   ├── Version.IVersionHelper.cs # 版本接口
│   │   └── Version.cs           # 版本核心
│   ├── BaseComponent.cs          # 基础组件
│   ├── GameEntry.cs             # 游戏入口
│   ├── GameFrameworkComponent.cs # 框架组件
│   ├── GameFrameworkEntry.cs    # 框架入口
│   ├── GameFrameworkEventArgs.cs # 框架事件参数
│   ├── GameFrameworkException.cs # 框架异常
│   ├── GameFrameworkGuard.cs    # 框架守卫
│   ├── GameFrameworkLinkedList.cs # 链表
│   ├── GameFrameworkLinkedListRange.cs # 链表范围
│   ├── GameFrameworkModule.cs   # 框架模块
│   ├── GameFrameworkMonoSingleton.cs # Mono单例
│   ├── GameFrameworkMultiDictionary.cs # 多值字典
│   ├── GameFrameworkSerializer.cs # 序列化器
│   ├── GameFrameworkSingleton.cs # 单例基类
│   ├── ObjectDontDestroyOnLoad.cs # 场景常驻对象
│   └── ShutdownType.cs           # 关闭类型
├── Extension/                     # 扩展方法库
│   ├── Extension/                # 通用扩展
│   │   ├── BidirectionalDictionary.cs # 双向字典
│   │   ├── BinaryExtension.cs      # 二进制扩展
│   │   ├── BufferExtension.cs      # 缓冲区扩展
│   │   ├── CollectionExtensions.cs # 集合扩展
│   │   ├── DateTimeExtensions.cs   # DateTime 扩展
│   │   ├── ObjectExtension.cs      # 对象扩展
│   │   ├── SpanExtension.cs        # Span 扩展
│   │   ├── StringExtensions.cs     # 字符串扩展
│   │   ├── ThreadLocalRandom.cs    # 线程本地随机
│   │   └── TypeExtensions.cs       # 类型扩展
│   ├── SequenceReader/            # 序列读取器
│   │   ├── SequenceReader.cs       # 序列读取器核心
│   │   └── SequenceReaderExtensions.cs # 序列读取器扩展
│   ├── UnityEngage.GameObject/    # GameObject 扩展
│   │   └── UnityEngage.GameObjectExtension.cs # GameObject 扩展
│   └── UnityEngine/               # Unity 类型扩展
│       ├── Transform/             # Transform 扩展
│       ├── Vector2/               # Vector2 扩展
│       ├── Vector3/               # Vector3 扩展
│       └── Vector4/               # Vector4 扩展
├── Helper/                        # 助手工具类
│   ├── ApplicationHelper.cs      # 应用助手
│   ├── CameraHelper.cs          # 相机助手
│   ├── DefaultCompressionHelper.cs # 默认压缩助手
│   ├── DefaultLogHelper.cs       # 默认日志助手
│   ├── DefaultTextHelper.cs      # 默认文本助手
│   ├── DefaultVersionHelper.cs   # 默认版本助手
│   ├── DistinctHelper.cs         # 去重助手
│   ├── DoTweenHelper.cs          # DoTween 动画助手
│   ├── FileHelper.cs            # 文件助手
│   ├── GameObjectHelper.cs      # 游戏对象助手
│   ├── Helper.cs                # 助手基类
│   ├── MathHelper.cs            # 数学助手
│   ├── NetworkHelper.cs         # 网络助手
│   ├── NewtonsoftJsonHelper.cs  # Newtonsoft JSON 助手
│   ├── ObjectHelper.cs          # 对象助手
│   ├── PathHelper.cs            # 路径助手
│   ├── PositionHelper.cs        # 位置助手
│   ├── RandomHelper.cs          # 随机助手
│   ├── TimerHelper/             # 计时器助手
│   │   ├── TimerHelper.cs       # 计时器核心
│   │   ├── TimerHelper.Current.cs # 当前时间
│   │   ├── TimerHelper.Day.cs   # 日期计算
│   │   ├── TimerHelper.Difference.cs # 时间差计算
│   │   ├── TimerHelper.Month.cs  # 月份计算
│   │   ├── TimerHelper.Range.cs  # 时间范围
│   │   ├── TimerHelper.TimeOffset.cs # 时间偏移
│   │   ├── TimerHelper.Timestamp.cs # 时间戳
│   │   ├── TimerHelper.Week.cs   # 周计算
│   │   └── TimerHelper.Year.cs   # 年份计算
│   ├── UnityRendererHelper.cs   # Unity 渲染助手
│   └── ZipHelper.cs             # ZIP 压缩助手
├── ObjectPool/                    # 对象池系统
│   ├── IObjectPool.cs           # 对象池接口
│   ├── ObjectBase.cs            # 对象池基类
│   └── ObjectPoolComponent.cs   # 对象池组件
├── Property/                      # 属性系统
│   └── BindableProperty.cs      # 可绑定属性
├── ReferencePool/                # 引用池系统
│   └── ReferencePoolComponent.cs # 引用池组件
└── Utility/                       # 实用工具类
    ├── Log.cs                     # 日志工具
    ├── Utility.Assembly.cs        # 程序集工具
    ├── Utility.Asset.Path.cs      # 资源路径工具
    ├── Utility.Compression/      # 压缩解压
    │   ├── ICompressionHelper.cs  # 压缩接口
    │   └── Utility.Compression.cs # 压缩核心
    ├── Utility.Const/            # 常量定义
    │   └── FileNameSuffix.cs      # 文件名后缀
    ├── Utility.Converter.cs       # 类型转换
    ├── Utility.Encryption/        # 加密解密
    │   ├── Utility.Encryption.cs  # 加密核心
    │   ├── Aes.cs                # AES 加密
    │   ├── Rsa.cs                # RSA 加密
    │   └── Dsa.cs                # DSA 加密
    ├── Utility.File.cs           # 文件操作
    ├── Utility.Hash/             # 哈希计算
    │   ├── HMACSha256.cs         # HMAC SHA256
    │   ├── Md5.cs                # MD5
    │   ├── Sha1.cs               # SHA1
    │   ├── MurmurHash3.cs        # MurmurHash3
    │   └── XxHash.cs             # XxHash
    ├── Utility.IdGenerator.cs     # ID 生成器
    ├── Utility.Json/             # JSON 序列化
    │   ├── IJsonHelper.cs        # JSON 接口
    │   └── Utility.Json.cs       # JSON 核心
    ├── Utility.Marshal.cs         # 序列化工具
    ├── Utility.Net.cs            # 网络工具
    ├── Utility.Object.cs          # 对象工具
    ├── Utility.Path.cs           # 路径处理
    ├── Utility.Random.cs         # 随机数
    ├── Utility.Text/             # 文本处理
    │   ├── ITextHelper.cs        # 文本接口
    │   └── Utility.Text.cs       # 文本核心
    ├── Utility.Verifier/          # 校验工具
    │   ├── Verifier.cs           # 校验核心
    │   ├── Crc32.cs             # CRC32
    │   └── Crc64.cs             # CRC64
    ├── Utility.cs                # 工具类入口
    └── XString.cs                # 高效字符串
```

#### Runtime 子模块详解

| 子模块 | 描述 | 主要功能 |
|--------|------|----------|
| **Base** | 框架核心基础 | 组件管理、事件池、日志系统、引用池、任务池、变量系统、生命周期管理、单例模式 |
| **Extension** | 扩展方法库 | 通用扩展（字符串、集合、日期）、Unity类型扩展（Transform、Vector）、序列读取器、GameObject扩展 |
| **Helper** | 助手工具类 | 应用、相机、文件、路径、数学、随机、计时器、网络、JSON、渲染、位置等全方位辅助功能 |
| **ObjectPool** | 对象池系统 | 对象复用、内存优化、性能提升 |
| **Property** | 属性系统 | 可绑定属性、数据监听、MVVM 支持 |
| **ReferencePool** | 引用池系统 | 引用类型对象管理、GC 优化 |
| **Utility** | 实用工具类 | 加密解密（AES/RSA/DSA）、压缩解压、哈希计算（MD5/SHA1/HMAC）、CRC校验、JSON序列化、文件操作、ID生成、类型转换、文本处理、日志等 |

### 🔌 Plugins 模块

原生平台插件和第三方依赖库。

```
Plugins/
├── iOS/                          # iOS 原生插件
│   └── GameFrameX/
│       ├── GameFrameX.mm                    # 核心功能
│       └── GameFrameXTrackingAuthorization.mm # 权限追踪
├── ICSharpCode.SharpZipLib.dll   # ZIP 压缩库
├── Microsoft.NET.StringTools.dll  # 字符串工具
├── System.Buffers.dll            # 内存缓冲
├── System.IO.Pipelines.dll       # IO 管道
├── System.Memory.dll            # 内存管理
└── System.Runtime.CompilerServices.Unsafe.dll # 运行时支持
```

#### Plugins 子模块详解

| 子模块 | 描述 | 依赖库 |
|--------|------|--------|
| **iOS 插件** | iOS 原生平台功能 | GameFrameX.mm |
| **压缩库** | ZIP 文件压缩/解压 | SharpZipLib |
| **内存管理** | 高效内存操作 | StringTools, Memory, Buffers |
| **运行时支持** | .NET 运行时扩展 | CompilerServices.Unsafe |

### 🛠️ Editor 模块

编辑器工具和扩展，提升开发效率。

```
Editor/
├── BuildHotfix/                  # 热更新构建工具
│   ├── BuildHotfixHelper.cs     # 构建帮助类
│   ├── HotFixAssemblyDefinitionHelper.cs # 热更新程序集
│   └── HotFixEditorCompilerHelper.cs # 编辑器编译
├── BuildProduct/                 # 产品构建助手
│   ├── BuildProductHelper.cs    # 构建帮助类
│   ├── BuildPostProcessHelper.cs # 构建后处理
│   ├── IBuilderPreHookHandler.cs # 构建前钩子
│   └── IBuilderPostHookHandler.cs # 构建后钩子
├── BuildWebGLTools/             # WebGL 构建工具
│   └── BuildWebGLToolsWithHybridCLR.cs # HybridCLR WebGL 构建
├── Cropping/                     # 图片裁剪工具
│   └── CroppingWindow.cs        # 裁剪窗口
├── Inspector/                    # 自定义检视面板
│   ├── BaseComponentInspector.cs # 基础组件检视
│   ├── ObjectPoolComponentInspector.cs # 对象池检视
│   └── ReferencePoolComponentInspector.cs # 引用池检视
├── InspectorLockShortcut/        # 检视面板锁定
│   └── InspectorLockShortcut.cs # 快捷键锁定
├── MiniGame/                      # 小游戏平台适配（21个平台） ⭐
│   ├── MiniGameDefineSymbolHelper.cs # 基础宏定义管理
│   ├── MiniGameDefineSymbolHelper.WeChat.cs # 微信
│   ├── MiniGameDefineSymbolHelper.Alipay.cs # 支付宝
│   ├── MiniGameDefineSymbolHelper.DouYin.cs # 抖音
│   ├── MiniGameDefineSymbolHelper.KuaiShou.cs # 快手
│   ├── MiniGameDefineSymbolHelper.Baidu.cs # 百度
│   ├── MiniGameDefineSymbolHelper.TapTap.cs # TapTap
│   ├── MiniGameDefineSymbolHelper.Meituan.cs # 美团
│   ├── MiniGameDefineSymbolHelper.Bilibili.cs # Bilibili
│   ├── MiniGameDefineSymbolHelper.JingDong.cs # 京东
│   ├── MiniGameDefineSymbolHelper.Taobao.cs # 淘宝
│   ├── MiniGameDefineSymbolHelper.Vivo.cs # vivo
│   ├── MiniGameDefineSymbolHelper.OPPO.cs # OPPO
│   ├── MiniGameDefineSymbolHelper.Xiaomi.cs # 小米
│   ├── MiniGameDefineSymbolHelper.Huawei.cs # 华为
│   ├── MiniGameDefineSymbolHelper.Discord.cs # Discord
│   ├── MiniGameDefineSymbolHelper.YouTube.cs # YouTube
│   ├── MiniGameDefineSymbolHelper.Facebook.cs # Facebook
│   ├── MiniGameDefineSymbolHelper.GooglePlay.cs # Google Play
│   ├── MiniGameDefineSymbolHelper.TikTok.cs # TikTok
│   ├── MiniGameDefineSymbolHelper.CrazyGames.cs # CrazyGames
│   └── MiniGameDefineSymbolHelper.Poki.cs # Poki
├── PackageManager/               # 包管理器窗口
│   ├── PackageManagerWindow.cs   # 包管理窗口
│   └── PackagesManifest.cs     # 包清单管理
├── UpdatePackages/               # 包更新工具
│   └── UpdateAllPackageHelper.cs # 批量更新
├── Welcome/                      # 欢迎窗口
│   └── WelcomeWindow.cs         # 欢迎界面
└── Misc/                         # 杂项工具
    ├── HelperInfo.cs            # 助手信息
    ├── LogRedirection.cs        # 日志重定向
    ├── ScriptingDefineSymbols.cs # 宏定义管理
    ├── Type.cs                  # 类型工具
    └── OpenFolder.cs            # 文件夹打开
```

#### Editor 子模块详解

| 子模块 | 描述 | 主要功能 |
|--------|------|----------|
| **BuildHotfix** | 热更新构建 | HybridCLR 热更新程序集构建和管理 |
| **BuildProduct** | 产品构建 | 构建流程自动化、前后置处理钩子 |
| **BuildWebGLTools** | WebGL 构建 | WebGL 平台专用构建工具 |
| **Cropping** | 图片裁剪 | 可视化图片裁剪工具 |
| **Inspector** | 自定义检视面板 | 对象池、引用池可视化监控 |
| **InspectorLockShortcut** | 面板锁定 | 快捷键锁定 Inspector 面板 |
| **MiniGame** | 小游戏适配 | 一键切换多平台小游戏环境（8个平台） |
| **PackageManager** | 包管理 | 可视化包管理界面 |
| **UpdatePackages** | 包更新 | 批量更新项目依赖 |
| **Welcome** | 欢迎界面 | 新用户引导和快速入口 |
| **Misc** | 杂项工具 | 日志、宏定义、类型等辅助工具 |

---

## 🚀 快速开始

### 安装方式

#### 方式一：Unity Package Manager（推荐）

1. 打开 Unity 编辑器
2. 打开 `Window` → `Package Manager`
3. 点击左上角的 `+` 按钮
4. 选择 `Add package from git URL`
5. 输入: `https://github.com/GameFrameX/com.gameframex.unity.git`

#### 方式二：手动下载

1. 下载最新的 [Release](https://github.com/GameFrameX/com.gameframex.unity/releases)
2. 解压到项目的 `Packages` 目录下

### 基础使用

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 获取对象池组件
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();
        
        // 获取引用池组件
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();
        
        // 使用扩展方法
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

---

## 💡 使用示例

### Runtime 使用示例

#### 🎯 对象池系统

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

#### 📝 扩展方法使用

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

#### 🔐 实用工具类

```csharp
// 文件操作
Utility.File.WriteAllBytes("path/to/file", data);
byte[] content = Utility.File.ReadAllBytes("path/to/file");

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

#### 📡 事件系统

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

### Editor 工具使用

#### 🎮 小游戏平台适配

在 Unity 菜单中可以快速切换小游戏平台：

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Enable WeChat Mini Game (开启[微信小游戏]适配)
│   ├── Enable Alipay Mini Game (开启[支付宝小游戏]适配)
│   ├── Enable DouYin Mini Game (开启[抖音小游戏]适配)
│   ├── Enable KuaiShou Mini Game (开启[快手小游戏]适配)
│   ├── Enable Baidu Mini Game (开启[百度小游戏]适配)
│   ├── Enable TapTap Mini Game (开启[TapTap小游戏]适配)
│   ├── Enable Meituan Mini Game (开启[美团小游戏]适配)
│   └── Enable Bilibili Mini Game (开启[Bilibili小游戏]适配)
```

#### 🏗️ 构建工具

```
GameFrameX/
├── Build Hotfix (构建热更新)
├── Build Product (构建产品)
└── Build WebGL With HybridCLR (WebGL热更新构建)
```

#### 📦 包管理

```
GameFrameX/
├── Package Manager (包管理器)
└── Update All Packages (更新所有包)
```

---

## 🎯 平台支持

### 操作系统平台

| 平台 | 状态 | 支持版本 |
|------|------|----------|
| Windows | ✅ 已支持 | Unity 2019.4+ |
| macOS | ✅ 已支持 | Unity 2019.4+ |
| Linux | ✅ 已支持 | Unity 2019.4+ |
| iOS | ✅ 已支持 | Unity 2019.4+ |
| Android | ✅ 已支持 | Unity 2019.4+ |
| WebGL | ✅ 已支持 | Unity 2019.4+ |

### 小游戏平台适配

GameFrameX 提供一键切换的小游戏平台适配功能，支持**全球21个主流小游戏平台**：

#### 🇨🇳 中国大陆平台（14个）

| 平台 | 宏定义 | 地区 | 菜单优先级 |
|------|--------|------|-----------|
| 微信小程序 | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 🇨🇳 中国 | 2000 |
| 抖音小程序 | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 🇨🇳 中国 | 2100 |
| 快手小程序 | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 🇨🇳 中国 | 2200 |
| 百度小程序 | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 🇨🇳 中国 | 2300 |
| 支付宝小程序 | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 🇨🇳 中国 | 2400 |
| 京东小程序 | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 🇨🇳 中国 | 2500 |
| 淘宝小程序 | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 🇨🇳 中国 | 2600 |
| TapTap 小游戏 | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 🇨🇳 中国 | 2700 |
| 美团小程序 | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 🇨🇳 中国 | 2800 |
| Bilibili 小程序 | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 🇨🇳 中国 | 2900 |
| vivo 小游戏 | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 🇨🇳 中国 | 3100 |
| OPPO 小游戏 | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 🇨🇳 中国 | 3200 |
| 小米小游戏 | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 🇨🇳 中国 | 3300 |
| 华为小游戏 | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 🇨🇳 中国 | 3400 |

#### 🌍 海外平台（7个）

| 平台 | 宏定义 | 地区 | 菜单优先级 |
|------|--------|------|-----------|
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 🌍 全球 | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 🌍 全球 | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 🌍 全球 | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 🌍 全球 | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 🌍 全球 | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 🌍 全球 | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 🌍 全球 | 3700 |

#### 宏定义说明

- **统一宏定义**: `ENABLE_WEBGL_MINI_GAME` - 所有小游戏平台共享
- **平台宏定义**: 各平台独立宏定义，用于条件编译
- **互斥机制**: 开启任一小游戏平台会自动关闭其他平台
- **菜单位置**: `GameFrameX/Scripting Define Symbols/Enable [平台名称] Mini Game`

---

## 📚 文档与资源

- 📖 **完整文档**: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🎯 **API 参考**: [API Documentation](https://gameframex.doc.alianblank.com/api)
- 📝 **示例项目**: [Examples Repository](https://github.com/GameFrameX/Examples)
- 🎬 **视频教程**: [YouTube 频道](https://youtube.com/gameframex)

---

## 🤝 社区与支持

- 💬 **QQ 讨论群**: [467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)
- 🐛 **问题反馈**: [GitHub Issues](https://github.com/GameFrameX/com.gameframex.unity/issues)
- 💡 **功能建议**: [GitHub Discussions](https://github.com/GameFrameX/com.gameframex.unity/discussions)

---

## 🔄 更新日志

### v1.4.0 (2025-04-16)
- ✨ 新增13个小游戏平台支持（京东、淘宝、Discord、YouTube、Facebook、Google Play、vivo、OPPO、小米、华为、TikTok、CrazyGames、Poki）
- ✨ 平台总数从8个扩展到21个，覆盖全球主流小游戏平台
- 📚 更新所有语言版本的 README 文档，同步最新平台支持信息
- 🐛 优化平台宏定义管理，提升互斥机制稳定性

### v1.3.6 (2025-05-28)
- 🐛 修复文件 GUID 重复的问题
- ✨ 新增美团、Bilibili 小游戏平台适配
- ✨ 新增更多扩展方法
- 📚 完善 README 文档结构，补全所有模块和文件的完整说明
- 🔧 优化对象池性能
- 📚 完善文档说明

查看完整更新日志: [CHANGELOG.md](CHANGELOG.md)

---

## 📄 开源协议

本项目采用 **MIT License** 与 **Apache License 2.0** 双许可证分发。

完整许可证文本请参见: [LICENSE.md](LICENSE.md)

---

## 👨‍💻 作者信息

**Blank**

- 🌐 Website: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🐙 GitHub: [@GameFrameX](https://github.com/GameFrameX)

---

<div align="center">

**如果这个项目对你有帮助，请给我们一个 ⭐ Star！**

[⬆ 回到顶部](#gameframex-unity-package)

</div>
