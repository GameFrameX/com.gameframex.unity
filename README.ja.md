<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_640.png)

# GameFrameX Unity Package

[![Version](https://img.shields.io/badge/version-1.3.6-blue.svg)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity](https://img.shields.io/badge/Unity-2019.4+-green.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT+Apache%202.0-orange.svg)](LICENSE.md)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

**インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援**

[📖 Documentation](https://gameframex.doc.alianblank.com) • [🚀 Quick Start](#quick-start) • [💬 QQ Group: 467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)

---

🌐 **言語**: [English](./README.md) | [繁體中文](./README.zh-TW.md) | [简体中文](./README.zh-CN.md) | [日本語](./README.ja.md) | [한국어](./README.ko.md)

---

## 📑 目次

- [🏗️ プロジェクト概要](#🏗️-プロジェクト概要)
- [📂 アーキテクチャ](#📂-アーキテクチャ)
  - [Runtime モジュールの詳細](#-runtime-モジュール)
  - [Plugins モジュールの詳細](#-plugins-モジュール)
  - [Editor モジュールの詳細](#-editor-モジュール)
- [🚀 クイックスタート](#🚀-クイックスタート)
- [💡 使用例](#💡-使用例)
  - [Runtime 使用例](#runtime-使用例)
  - [Editor ツール](#editor-ツール)
- [🎯 プラットフォーム対応](#🎯-プラットフォーム対応)
- [📚 ドキュメントとリソース](#📚-ドキュメントとリソース)
- [🤝 コミュニティとサポート](#🤝-コミュニティとサポート)
- [📄 ライセンス](#📄-ライセンス)

</div>

---

## 🏗️ Project Overview

GameFrameX is a modern Unity game framework designed for independent game developers, providing a complete end-to-end solution for game development. The framework follows a **three-layer modular architecture** design with rich built-in game development tools and components, helping developers quickly build high-quality game projects.

### 🎯 Key Features

- 🏗️ **Three-Layer Architecture** - Clear separation: Runtime, Plugins, and Editor
- 🔧 **Rich Toolset** - Built-in development aids and editor extensions
- 📦 **Object Pool Management** - Efficient memory management and object reuse
- 🎨 **Extension Methods Library** - Extensive Unity engine extensions
- 🛠️ **Utility Classes** - Encryption, compression, networking, and more
- 📱 **Multi-Platform Support** - PC, Mobile, WebGL and more
- 🔥 **Hotfix Support** - Built-in HybridCLR hotfix solution
- 🎮 **Mini Game Adaptation** - One-click switch between multiple mini game platforms

### 📋 System Requirements

- **Unity Version**: 2019.4 or higher
- **Platform Support**: Windows, macOS, Linux, iOS, Android, WebGL
- **.NET Version**: .NET Standard 2.0+

---

## 📂 Architecture

GameFrameX uses a clear three-layer architecture design, with each module having its own responsibilities and working together.

### 📦 Runtime Module

Core runtime code providing all functionality needed for game runtime.

```
Runtime/
├── Base/                          # Framework Core Foundation
│   ├── DataStruct/               # Data Structures
│   │   └── TypeNamePair.cs       # Type Name Pair
│   ├── EventPool/                # Event Pool System
│   │   ├── BaseEventArgs.cs      # Base Event Args
│   │   ├── EventPool.EventNode.cs # Event Node
│   │   ├── EventPool.cs          # Event Pool Core
│   │   └── EventPoolMode.cs      # Event Pool Mode
│   ├── Log/                      # Logging System
│   │   ├── GameFrameworkLog.ILogHelper.cs # Log Interface
│   │   ├── GameFrameworkLog.cs   # Log Core
│   │   └── GameFrameworkLogLevel.cs # Log Level
│   ├── ReferencePool/            # Reference Pool System
│   │   ├── IReference.cs        # Reference Interface
│   │   ├── ReferencePool.ReferenceCollection.cs # Reference Collection
│   │   ├── ReferencePool.cs      # Reference Pool Core
│   │   └── ReferencePoolInfo.cs   # Reference Pool Info
│   ├── TaskPool/                 # Task Pool System
│   │   ├── ITaskAgent.cs        # Task Agent Interface
│   │   ├── StartTaskStatus.cs    # Task Status
│   │   ├── TaskBase.cs          # Task Base
│   │   ├── TaskInfo.cs          # Task Info
│   │   ├── TaskPool.cs          # Task Pool Core
│   │   └── TaskStatus.cs        # Task Status
│   ├── Variable/                 # Variable System
│   │   ├── GenericVariable.cs    # Generic Variable
│   │   └── Variable.cs          # Variable Base
│   ├── Version/                  # Version Management
│   │   ├── Version.IVersionHelper.cs # Version Interface
│   │   └── Version.cs           # Version Core
│   ├── BaseComponent.cs          # Base Component
│   ├── GameEntry.cs             # Game Entry
│   ├── GameFrameworkComponent.cs # Framework Component
│   ├── GameFrameworkEntry.cs    # Framework Entry
│   ├── GameFrameworkEventArgs.cs # Framework Event Args
│   ├── GameFrameworkException.cs # Framework Exception
│   ├── GameFrameworkGuard.cs    # Framework Guard
│   ├── GameFrameworkLinkedList.cs # Linked List
│   ├── GameFrameworkLinkedListRange.cs # Linked List Range
│   ├── GameFrameworkModule.cs   # Framework Module
│   ├── GameFrameworkMonoSingleton.cs # Mono Singleton
│   ├── GameFrameworkMultiDictionary.cs # Multi-Value Dictionary
│   ├── GameFrameworkSerializer.cs # Serializer
│   ├── GameFrameworkSingleton.cs # Singleton Base
│   ├── ObjectDontDestroyOnLoad.cs # Scene Persistent Object
│   └── ShutdownType.cs           # Shutdown Type
├── Extension/                     # Extension Methods Library
│   ├── Extension/                # Common Extensions
│   │   ├── BidirectionalDictionary.cs # Bidirectional Dictionary
│   │   ├── BinaryExtension.cs      # Binary Extension
│   │   ├── BufferExtension.cs      # Buffer Extension
│   │   ├── CollectionExtensions.cs # Collection Extensions
│   │   ├── DateTimeExtensions.cs   # DateTime Extension
│   │   ├── ObjectExtension.cs      # Object Extension
│   │   ├── SpanExtension.cs        # Span Extension
│   │   ├── StringExtensions.cs     # String Extensions
│   │   ├── ThreadLocalRandom.cs    # Thread Local Random
│   │   └── TypeExtensions.cs       # Type Extensions
│   ├── SequenceReader/            # Sequence Reader
│   │   ├── SequenceReader.cs       # Sequence Reader Core
│   │   └── SequenceReaderExtensions.cs # Sequence Reader Extensions
│   ├── UnityEngage.GameObject/    # GameObject Extension
│   │   └── UnityEngage.GameObjectExtension.cs # GameObject Extension
│   └── UnityEngine/               # Unity Type Extensions
│       ├── Transform/             # Transform Extension
│       ├── Vector2/               # Vector2 Extension
│       ├── Vector3/               # Vector3 Extension
│       └── Vector4/               # Vector4 Extension
├── Helper/                        # Helper Classes
│   ├── ApplicationHelper.cs      # Application Helper
│   ├── CameraHelper.cs          # Camera Helper
│   ├── DefaultCompressionHelper.cs # Default Compression Helper
│   ├── DefaultLogHelper.cs       # Default Log Helper
│   ├── DefaultTextHelper.cs      # Default Text Helper
│   ├── DefaultVersionHelper.cs   # Default Version Helper
│   ├── DistinctHelper.cs         # Distinct Helper
│   ├── DoTweenHelper.cs          # DoTween Animation Helper
│   ├── FileHelper.cs            # File Helper
│   ├── GameObjectHelper.cs      # GameObject Helper
│   ├── Helper.cs                # Helper Base Class
│   ├── MathHelper.cs            # Math Helper
│   ├── NetworkHelper.cs         # Network Helper
│   ├── NewtonsoftJsonHelper.cs  # Newtonsoft JSON Helper
│   ├── ObjectHelper.cs          # Object Helper
│   ├── PathHelper.cs            # Path Helper
│   ├── PositionHelper.cs        # Position Helper
│   ├── RandomHelper.cs          # Random Helper
│   ├── TimerHelper/             # Timer Helper
│   │   ├── TimerHelper.cs       # Timer Core
│   │   ├── TimerHelper.Current.cs # Current Time
│   │   ├── TimerHelper.Day.cs   # Day Calculation
│   │   ├── TimerHelper.Difference.cs # Time Difference
│   │   ├── TimerHelper.Month.cs  # Month Calculation
│   │   ├── TimerHelper.Range.cs  # Time Range
│   │   ├── TimerHelper.TimeOffset.cs # Time Offset
│   │   ├── TimerHelper.Timestamp.cs # Timestamp
│   │   ├── TimerHelper.Week.cs   # Week Calculation
│   │   └── TimerHelper.Year.cs   # Year Calculation
│   ├── UnityRendererHelper.cs   # Unity Renderer Helper
│   └── ZipHelper.cs             # ZIP Compression Helper
├── ObjectPool/                    # Object Pool System
│   ├── IObjectPool.cs           # Object Pool Interface
│   ├── ObjectBase.cs            # Object Pool Base
│   └── ObjectPoolComponent.cs   # Object Pool Component
├── Property/                      # Property System
│   └── BindableProperty.cs      # Bindable Property
├── ReferencePool/                # Reference Pool System
│   └── ReferencePoolComponent.cs # Reference Pool Component
└── Utility/                       # Utility Classes
    ├── Log.cs                     # Log Utility
    ├── Utility.Assembly.cs        # Assembly Utility
    ├── Utility.Asset.Path.cs      # Asset Path Utility
    ├── Utility.Compression/      # Compression
    │   ├── ICompressionHelper.cs  # Compression Interface
    │   └── Utility.Compression.cs # Compression Core
    ├── Utility.Const/            # Constants
    │   └── FileNameSuffix.cs      # File Name Suffix
    ├── Utility.Converter.cs       # Type Converter
    ├── Utility.Encryption/        # Encryption
    │   ├── Utility.Encryption.cs  # Encryption Core
    │   ├── Aes.cs                # AES Encryption
    │   ├── Rsa.cs                # RSA Encryption
    │   └── Dsa.cs                # DSA Encryption
    ├── Utility.File.cs           # File Operations
    ├── Utility.Hash/             # Hash Calculation
    │   ├── HMACSha256.cs         # HMAC SHA256
    │   ├── Md5.cs                # MD5
    │   ├── Sha1.cs               # SHA1
    │   ├── MurmurHash3.cs        # MurmurHash3
    │   └── XxHash.cs             # XxHash
    ├── Utility.IdGenerator.cs     # ID Generator
    ├── Utility.Json/             # JSON Serialization
    │   ├── IJsonHelper.cs        # JSON Interface
    │   └── Utility.Json.cs       # JSON Core
    ├── Utility.Marshal.cs         # Marshal Utility
    ├── Utility.Net.cs            # Network Utility
    ├── Utility.Object.cs          # Object Utility
    ├── Utility.Path.cs           # Path Handling
    ├── Utility.Random.cs         # Random Number
    ├── Utility.Text/             # Text Processing
    │   ├── ITextHelper.cs        # Text Interface
    │   └── Utility.Text.cs       # Text Core
    ├── Utility.Verifier/          # Verifier
    │   ├── Verifier.cs           # Verifier Core
    │   ├── Crc32.cs             # CRC32
    │   └── Crc64.cs             # CRC64
    ├── Utility.cs                # Utility Entry
    └── XString.cs                # Efficient String
```

#### Runtime Sub-Modules

| Sub-Module | Description | Main Features |
|------------|-------------|---------------|
| **Base** | Framework Core | Component management, event pool, logging, reference pool, task pool, variable system, lifecycle management, singleton pattern |
| **Extension** | Extension Library | Common extensions (string, collection, datetime), Unity type extensions (Transform, Vector), sequence reader, GameObject extensions |
| **Helper** | Helper Classes | Application, camera, file, path, math, random, timer, network, JSON, rendering, position and more |
| **ObjectPool** | Object Pool System | Object reuse, memory optimization, performance improvement |
| **Property** | Property System | Bindable properties, data binding, MVVM support |
| **ReferencePool** | Reference Pool System | Reference type management, GC optimization |
| **Utility** | Utility Classes | Encryption (AES/RSA/DSA), compression, hash (MD5/SHA1/HMAC), CRC, JSON, file operations, ID generation, type conversion, text processing, logging |

### 🔌 Plugins Module

Native platform plugins and third-party dependencies.

```
Plugins/
├── iOS/                          # iOS Native Plugin
│   └── GameFrameX/
│       ├── GameFrameX.mm                    # Core Functionality
│       └── GameFrameXTrackingAuthorization.mm # Permission Tracking
├── ICSharpCode.SharpZipLib.dll   # ZIP Compression Library
├── Microsoft.NET.StringTools.dll  # String Tools
├── System.Buffers.dll            # Memory Buffer
├── System.IO.Pipelines.dll       # IO Pipeline
├── System.Memory.dll            # Memory Management
└── System.Runtime.CompilerServices.Unsafe.dll # Runtime Support
```

#### Plugins Sub-Modules

| Sub-Module | Description | Dependencies |
|------------|-------------|--------------|
| **iOS Plugin** | iOS native functionality | GameFrameX.mm |
| **Compression Library** | ZIP file compression/decompression | SharpZipLib |
| **Memory Management** | Efficient memory operations | StringTools, Memory, Buffers |
| **Runtime Support** | .NET runtime extensions | CompilerServices.Unsafe |

### 🛠️ Editor Module

Editor tools and extensions for improved development efficiency.

```
Editor/
├── BuildHotfix/                  # Hotfix Build Tools
│   ├── BuildHotfixHelper.cs     # Build Helper
│   ├── HotFixAssemblyDefinitionHelper.cs # Hotfix Assembly
│   └── HotFixEditorCompilerHelper.cs # Editor Compiler
├── BuildProduct/                 # Product Build Assistant
│   ├── BuildProductHelper.cs    # Build Helper
│   ├── BuildPostProcessHelper.cs # Post Build Processing
│   ├── IBuilderPreHookHandler.cs # Pre-Build Hook
│   └── IBuilderPostHookHandler.cs # Post-Build Hook
├── BuildWebGLTools/             # WebGL Build Tools
│   └── BuildWebGLToolsWithHybridCLR.cs # HybridCLR WebGL Build
├── Cropping/                     # Image Cropping Tool
│   └── CroppingWindow.cs        # Cropping Window
├── Inspector/                    # Custom Inspector Panels
│   ├── BaseComponentInspector.cs # Base Component Inspector
│   ├── ObjectPoolComponentInspector.cs # Object Pool Inspector
│   └── ReferencePoolComponentInspector.cs # Reference Pool Inspector
├── InspectorLockShortcut/        # Inspector Lock
│   └── InspectorLockShortcut.cs # Keyboard Shortcut Lock
├── MiniGame/                      # Mini Game Platform Adaptation ⭐
│   ├── MiniGameDefineSymbolHelper.cs # Base Define Symbol Manager
│   ├── MiniGameDefineSymbolHelper.WeChat.cs # WeChat
│   ├── MiniGameDefineSymbolHelper.Alipay.cs # Alipay
│   ├── MiniGameDefineSymbolHelper.DouYin.cs # DouYin
│   ├── MiniGameDefineSymbolHelper.KuaiShou.cs # KuaiShou
│   ├── MiniGameDefineSymbolHelper.Baidu.cs # Baidu
│   ├── MiniGameDefineSymbolHelper.TapTap.cs # TapTap
│   ├── MiniGameDefineSymbolHelper.Meituan.cs # Meituan
│   └── MiniGameDefineSymbolHelper.Bilibili.cs # Bilibili
├── PackageManager/               # Package Manager Window
│   ├── PackageManagerWindow.cs   # Package Manager Window
│   └── PackagesManifest.cs     # Package Manifest
├── UpdatePackages/               # Package Update Tools
│   └── UpdateAllPackageHelper.cs # Batch Update
├── Welcome/                      # Welcome Window
│   └── WelcomeWindow.cs         # Welcome Interface
└── Misc/                         # Miscellaneous Tools
    ├── HelperInfo.cs            # Helper Info
    ├── LogRedirection.cs        # Log Redirection
    ├── ScriptingDefineSymbols.cs # Define Symbol Manager
    ├── Type.cs                  # Type Utility
    └── OpenFolder.cs            # Open Folder
```

#### Editor Sub-Modules

| Sub-Module | Description | Main Features |
|------------|-------------|---------------|
| **BuildHotfix** | Hotfix Build | HybridCLR hotfix assembly build and management |
| **BuildProduct** | Product Build | Build process automation, pre/post hooks |
| **BuildWebGLTools** | WebGL Build | WebGL platform specific build tools |
| **Cropping** | Image Cropping | Visual image cropping tool |
| **Inspector** | Custom Inspectors | Object pool, reference pool visual monitoring |
| **InspectorLockShortcut** | Inspector Lock | Keyboard shortcut for locking Inspector panel |
| **MiniGame** | Mini Game Adaptation | One-click switch between 8 mini game platforms |
| **PackageManager** | Package Management | Visual package management interface |
| **UpdatePackages** | Package Update | Batch update project dependencies |
| **Welcome** | Welcome Interface | New user guide and quick access |
| **Misc** | Miscellaneous | Logging, define symbols, types and more |

---

## 🚀 Quick Start

### Installation

#### Method 1: Unity Package Manager (Recommended)

1. Open Unity Editor
2. Go to `Window` → `Package Manager`
3. Click the `+` button in the top-left corner
4. Select `Add package from git URL`
5. Enter: `https://github.com/GameFrameX/com.gameframex.unity.git`

#### Method 2: Manual Download

1. Download the latest [Release](https://github.com/GameFrameX/com.gameframex.unity/releases)
2. Extract to your project's `Packages` directory

### Basic Usage

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Get object pool component
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();
        
        // Get reference pool component
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();
        
        // Use extension methods
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

---

## 💡 Usage Examples

### Runtime Usage Examples

#### 🎯 Object Pool System

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

#### 📝 Extension Methods

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

#### 🔐 Utility Classes

```csharp
// File operations
Utility.File.WriteAllBytes("path/to/file", data);
byte[] content = Utility.File.ReadAllBytes("path/to/file");

// AES encryption/decryption
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// Hash calculation
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSON serialization
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

#### 📡 Event System

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

#### 🎮 Mini Game Platform Adaptation

Quickly switch between mini game platforms in Unity menu:

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Enable WeChat Mini Game
│   ├── Enable Alipay Mini Game
│   ├── Enable DouYin Mini Game
│   ├── Enable KuaiShou Mini Game
│   ├── Enable Baidu Mini Game
│   ├── Enable TapTap Mini Game
│   ├── Enable Meituan Mini Game
│   └── Enable Bilibili Mini Game
```

#### 🏗️ Build Tools

```
GameFrameX/
├── Build Hotfix
├── Build Product
└── Build WebGL With HybridCLR
```

#### 📦 Package Management

```
GameFrameX/
├── Package Manager
└── Update All Packages
```

---

## 🎯 Platform Support

### Operating Systems

| Platform | Status | Supported Version |
|----------|--------|-------------------|
| Windows | ✅ Supported | Unity 2019.4+ |
| macOS | ✅ Supported | Unity 2019.4+ |
| Linux | ✅ Supported | Unity 2019.4+ |
| iOS | ✅ Supported | Unity 2019.4+ |
| Android | ✅ Supported | Unity 2019.4+ |
| WebGL | ✅ Supported | Unity 2019.4+ |

### Mini Game Platform Adaptation

GameFrameX provides one-click mini game platform adaptation, supporting **21 mainstream mini game platforms** worldwide:

#### 🇨🇳 China Mainland Platforms (14)

| Platform | Define Symbol | Region | Menu Priority |
|----------|---------------|--------|---------------|
| WeChat Mini Game | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 🇨🇳 China | 2000 |
| DouYin Mini Game | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 🇨🇳 China | 2100 |
| KuaiShou Mini Game | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 🇨🇳 China | 2200 |
| Baidu Mini Game | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 🇨🇳 China | 2300 |
| Alipay Mini Game | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 🇨🇳 China | 2400 |
| JD Mini Game | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 🇨🇳 China | 2500 |
| Taobao Mini Program | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 🇨🇳 China | 2600 |
| TapTap Mini Game | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 🇨🇳 China | 2700 |
| Meituan Mini Game | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 🇨🇳 China | 2800 |
| Bilibili Mini Game | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 🇨🇳 China | 2900 |
| vivo Mini Game | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 🇨🇳 China | 3100 |
| OPPO Mini Game | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 🇨🇳 China | 3200 |
| Xiaomi Mini Game | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 🇨🇳 China | 3300 |
| Huawei Mini Game | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 🇨🇳 China | 3400 |

#### 🌍 Global Platforms (7)

| Platform | Define Symbol | Region | Menu Priority |
|----------|---------------|--------|---------------|
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 🌍 Global | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 🌍 Global | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 🌍 Global | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 🌍 Global | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 🌍 Global | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 🌍 Global | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 🌍 Global | 3700 |

#### Define Symbol Details

- **Unified Define**: `ENABLE_WEBGL_MINI_GAME` - Shared by all mini game platforms
- **Platform Defines**: Independent defines for conditional compilation per platform
- **Mutex Mechanism**: Enabling one mini game platform automatically disables others
- **Menu Path**: `GameFrameX/Scripting Define Symbols/Enable [Platform] Mini Game`

---

## 📚 Documentation & Resources

- 📖 **Full Documentation**: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🎯 **API Reference**: [API Documentation](https://gameframex.doc.alianblank.com/api)
- 📝 **Example Projects**: [Examples Repository](https://github.com/GameFrameX/Examples)
- 🎬 **Video Tutorials**: [YouTube Channel](https://youtube.com/gameframex)

---

## 🤝 Community & Support

- 💬 **QQ Group**: [467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)
- 🐛 **Issue Tracker**: [GitHub Issues](https://github.com/GameFrameX/com.gameframex.unity/issues)
- 💡 **Feature Requests**: [GitHub Discussions](https://github.com/GameFrameX/com.gameframex.unity/discussions)

---

## 🔄 Changelog

### v1.3.6 (2025-05-28)
- 🐛 Fixed duplicate GUID issues
- ✨ Added Meituan, Bilibili mini game platform adaptation
- ✨ Added more extension methods
- 📚 Enhanced README documentation with complete module structure
- 🔧 Optimized object pool performance
- 📚 Improved documentation

View full changelog: [CHANGELOG.md](CHANGELOG.md)

---

## 📄 License

This project is distributed under **MIT License** and **Apache License 2.0** dual licensing.

See full license text: [LICENSE.md](LICENSE.md)

---

## 👨‍💻 Author

**Blank**

- 🌐 Website: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🐙 GitHub: [@GameFrameX](https://github.com/GameFrameX)

---

<div align="center">

**If this project helps you, please give us a ⭐ Star!**

[⬆ Back to Top](#gameframex-unity-package)

</div>
