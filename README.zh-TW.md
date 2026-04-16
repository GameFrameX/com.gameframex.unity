<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity Package

[![Version](https://img.shields.io/badge/version-1.3.6-blue.svg)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity](https://img.shields.io/badge/Unity-2019.4+-green.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT+Apache%202.0-orange.svg)](LICENSE.md)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

**獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使**

[📖 文檔](https://gameframex.doc.alianblank.com) • [🚀 快速开始](#快速开始) • [💬 QQ群: 467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)

---

🌐 **語言**: [English](./README.md) | [繁體中文](./README.zh-TW.md) | [中文](./README.zh-CN.md) | [日本語](./README.ja.md) | [한국어](./README.ko.md)

---

</div>

## 📑 目錄導航

- [🏗️ 項目簡介](#🏗️-項目簡介)
- [📂 架構概覽](#📂-架構概覽)
  - [Runtime 模組](#runtime-模組)
  - [Plugins 模組](#plugins-模組)
  - [Editor 模組](#editor-模組)
- [🚀 快速開始](#🚀-快速開始)
- [💡 使用範例](#💡-使用範例)
  - [Runtime 使用範例](#runtime-使用範例)
  - [Editor 工具使用](#editor-工具使用)
- [🎯 平台支援](#🎯-平台支援)
- [📚 文檔與資源](#📚-文檔與資源)
- [🤝 社區與支援](#🤝-社區與支援)
- [📄 開源協議](#📄-開源協議)

---

## 🏗️ 項目簡介

GameFrameX 是一個專為獨立遊戲開發者設計的現代化 Unity 遊戲框架，提供完整的前後端一體化解決方案。框架採用**三層模組化架構**設計，內置豐富的遊戲開發工具和組件，幫助開發者快速構建高質量的遊戲項目。

### 🎯 核心特性

- 🏗️ **三層架構** - Runtime（運行時）、Plugins（插件）、Editor（編輯器）清晰分層
- 🔧 **豐富工具集** - 內置多種開發輔助工具和編輯器擴展
- 📦 **物件池管理** - 高效的記憶體管理和物件復用機制
- 🎨 **擴展方法庫** - 豐富的 Unity 引擎擴展方法
- 🛠️ **實用工具類** - 涵蓋加密、壓縮、網路等常用功能
- 📱 **多平台支援** - 支援 PC、移動端、WebGL 等多平台部署
- 🔥 **熱更新支援** - 內置 HybridCLR 熱更新解決方案
- 🎮 **小遊戲適配** - 一鍵切換多平台小遊戲環境

### 📋 系統需求

- **Unity 版本**: 2019.4 或更高版本
- **平台支援**: Windows, macOS, Linux, iOS, Android, WebGL
- **.NET 版本**: .NET Standard 2.0+

---

## 📂 架構概覽

GameFrameX 採用清晰的三層架構設計，每個模組各司其職，協同工作。

### 📦 Runtime 模組

運行時核心代碼，提供遊戲運行時所需的所有功能。

```
Runtime/
├── Base/                          # 框架核心基礎
│   ├── DataStruct/               # 數據结构
│   │   └── TypeNamePair.cs       # 類型名称对
│   ├── EventPool/                # 事件池系統
│   │   ├── BaseEventArgs.cs      # 事件參數基类
│   │   ├── EventPool.EventNode.cs # 事件节点
│   │   ├── EventPool.cs          # 事件池核心
│   │   └── EventPoolMode.cs      # 事件池模式
│   ├── Log/                      # 日志系統
│   │   ├── GameFrameworkLog.ILogHelper.cs # 日志介面
│   │   ├── GameFrameworkLog.cs   # 日志核心
│   │   └── GameFrameworkLogLevel.cs # 日志级别
│   ├── ReferencePool/            # 引用池系統
│   │   ├── IReference.cs        # 引用介面
│   │   ├── ReferencePool.ReferenceCollection.cs # 引用集合
│   │   ├── ReferencePool.cs      # 引用池核心
│   │   └── ReferencePoolInfo.cs   # 引用池信息
│   ├── TaskPool/                 # 任務池系統
│   │   ├── ITaskAgent.cs        # 任务代理介面
│   │   ├── StartTaskStatus.cs    # 任务状态
│   │   ├── TaskBase.cs          # 任务基类
│   │   ├── TaskInfo.cs          # 任务信息
│   │   ├── TaskPool.cs          # 任務池核心
│   │   └── TaskStatus.cs        # 任务状态
│   ├── Variable/                 # 变量系統
│   │   ├── GenericVariable.cs    # 泛型变量
│   │   └── Variable.cs          # 变量基类
│   ├── Version/                  # 版本管理
│   │   ├── Version.IVersionHelper.cs # 版本介面
│   │   └── Version.cs           # 版本核心
│   ├── BaseComponent.cs          # 基礎組件
│   ├── GameEntry.cs             # 遊戲入口
│   ├── GameFrameworkComponent.cs # 框架組件
│   ├── GameFrameworkEntry.cs    # 框架入口
│   ├── GameFrameworkEventArgs.cs # 框架事件參數
│   ├── GameFrameworkException.cs # 框架异常
│   ├── GameFrameworkGuard.cs    # 框架守卫
│   ├── GameFrameworkLinkedList.cs # 链表
│   ├── GameFrameworkLinkedListRange.cs # 链表范围
│   ├── GameFrameworkModule.cs   # 框架模組
│   ├── GameFrameworkMonoSingleton.cs # Mono單例
│   ├── GameFrameworkMultiDictionary.cs # 多值字典
│   ├── GameFrameworkSerializer.cs # 序列化器
│   ├── GameFrameworkSingleton.cs # 單例基类
│   ├── ObjectDontDestroyOnLoad.cs # 场景常駐物件
│   └── ShutdownType.cs           # 关闭類型
├── Extension/                     # 擴展方法库
│   ├── Extension/                # 通用擴展
│   │   ├── BidirectionalDictionary.cs # 双向字典
│   │   ├── BinaryExtension.cs      # 二进制擴展
│   │   ├── BufferExtension.cs      # 缓冲区擴展
│   │   ├── CollectionExtensions.cs # 集合擴展
│   │   ├── DateTimeExtensions.cs   # DateTime 擴展
│   │   ├── ObjectExtension.cs      # 物件擴展
│   │   ├── SpanExtension.cs        # Span 擴展
│   │   ├── StringExtensions.cs     # 字符串擴展
│   │   ├── ThreadLocalRandom.cs    # 執行緒本地随机
│   │   └── TypeExtensions.cs       # 類型擴展
│   ├── SequenceReader/            # 序列读取器
│   │   ├── SequenceReader.cs       # 序列读取器核心
│   │   └── SequenceReaderExtensions.cs # 序列读取器擴展
│   ├── UnityEngage.GameObject/    # GameObject 擴展
│   │   └── UnityEngage.GameObjectExtension.cs # GameObject 擴展
│   └── UnityEngine/               # Unity 類型擴展
│       ├── Transform/             # Transform 擴展
│       ├── Vector2/               # Vector2 擴展
│       ├── Vector3/               # Vector3 擴展
│       └── Vector4/               # Vector4 擴展
├── Helper/                        # 助手工具类
│   ├── ApplicationHelper.cs      # 应用助手
│   ├── CameraHelper.cs          # 相机助手
│   ├── DefaultCompressionHelper.cs # 默认壓縮助手
│   ├── DefaultLogHelper.cs       # 默认日志助手
│   ├── DefaultTextHelper.cs      # 默认文本助手
│   ├── DefaultVersionHelper.cs   # 默认版本助手
│   ├── DistinctHelper.cs         # 去重助手
│   ├── DoTweenHelper.cs          # DoTween 动画助手
│   ├── FileHelper.cs            # 文件助手
│   ├── GameObjectHelper.cs      # 遊戲物件助手
│   ├── Helper.cs                # 助手基类
│   ├── MathHelper.cs            # 数学助手
│   ├── NetworkHelper.cs         # 網路助手
│   ├── NewtonsoftJsonHelper.cs  # Newtonsoft JSON 助手
│   ├── ObjectHelper.cs          # 物件助手
│   ├── PathHelper.cs            # 路徑助手
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
│   └── ZipHelper.cs             # ZIP 壓縮助手
├── ObjectPool/                    # 物件池系統
│   ├── IObjectPool.cs           # 物件池介面
│   ├── ObjectBase.cs            # 物件池基类
│   └── ObjectPoolComponent.cs   # 物件池組件
├── Property/                      # 屬性系統
│   └── BindableProperty.cs      # 可绑定屬性
├── ReferencePool/                # 引用池系統
│   └── ReferencePoolComponent.cs # 引用池組件
└── Utility/                       # 实用工具类
    ├── Log.cs                     # 日志工具
    ├── Utility.Assembly.cs        # 程序集工具
    ├── Utility.Asset.Path.cs      # 資源路徑工具
    ├── Utility.Compression/      # 壓縮解壓
    │   ├── ICompressionHelper.cs  # 壓縮介面
    │   └── Utility.Compression.cs # 壓縮核心
    ├── Utility.Const/            # 常量定义
    │   └── FileNameSuffix.cs      # 文件名后缀
    ├── Utility.Converter.cs       # 類型转换
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
    │   ├── IJsonHelper.cs        # JSON 介面
    │   └── Utility.Json.cs       # JSON 核心
    ├── Utility.Marshal.cs         # 序列化工具
    ├── Utility.Net.cs            # 網路工具
    ├── Utility.Object.cs          # 物件工具
    ├── Utility.Path.cs           # 路徑處理
    ├── Utility.Random.cs         # 随机数
    ├── Utility.Text/             # 文本處理
    │   ├── ITextHelper.cs        # 文本介面
    │   └── Utility.Text.cs       # 文本核心
    ├── Utility.Verifier/          # 校驗工具
    │   ├── Verifier.cs           # 校驗核心
    │   ├── Crc32.cs             # CRC32
    │   └── Crc64.cs             # CRC64
    ├── Utility.cs                # 工具类入口
    └── XString.cs                # 高效字符串
```

#### Runtime 子模組詳解

| 子模組 | 描述 | 主要功能 |
|--------|------|----------|
| **Base** | 框架核心基礎 | 組件管理、事件池、日誌系統、引用池、任務池、變量系統、生命周期管理、單例模式 |
| **Extension** | 擴展方法庫 | 通用擴展（字符串、集合、日期）、Unity類型擴展（Transform、Vector）、序列讀取器、GameObject擴展 |
| **Helper** | 助手工具類 | 應用、相機、文件、路徑、數學、隨機、計時器、網路、JSON、渲染、位置等全方位輔助功能 |
| **ObjectPool** | 物件池系統 | 物件復用、記憶體優化、效能提升 |
| **Property** | 屬性系統 | 可綁定屬性、數據監聽、MVVM 支援 |
| **ReferencePool** | 引用池系統 | 引用類型物件管理、GC 優化 |
| **Utility** | 實用工具類 | 加密解密（AES/RSA/DSA）、壓縮解壓、雜湊計算（MD5/SHA1/HMAC）、CRC校驗、JSON序列化、文件操作、ID生成、類型轉換、文本處理、日誌等 |

### 🔌 Plugins 模組

原生平台插件和第三方依賴庫。

```
Plugins/
├── iOS/                          # iOS 原生插件
│   └── GameFrameX/
│       ├── GameFrameX.mm                    # 核心功能
│       └── GameFrameXTrackingAuthorization.mm # 权限追踪
├── ICSharpCode.SharpZipLib.dll   # ZIP 壓縮库
├── Microsoft.NET.StringTools.dll  # 字符串工具
├── System.Buffers.dll            # 記憶體缓冲
├── System.IO.Pipelines.dll       # IO 管道
├── System.Memory.dll            # 記憶體管理
└── System.Runtime.CompilerServices.Unsafe.dll # 運行時支援
```

#### Plugins 子模組詳解

| 子模組 | 描述 | 依賴庫 |
|--------|------|--------|
| **iOS 插件** | iOS 原生平台功能 | GameFrameX.mm |
| **壓縮庫** | ZIP 文件壓縮/解壓 | SharpZipLib |
| **記憶體管理** | 高效記憶體操作 | StringTools, Memory, Buffers |
| **運行時支援** | .NET 運行時擴展 | CompilerServices.Unsafe |

### 🛠️ Editor 模組

編輯器工具和擴展，提升開發效率。

```
Editor/
├── BuildHotfix/                  # 熱更新構建工具
│   ├── BuildHotfixHelper.cs     # 構建幫助类
│   ├── HotFixAssemblyDefinitionHelper.cs # 熱更新程序集
│   └── HotFixEditorCompilerHelper.cs # 編輯器編譯
├── BuildProduct/                 # 产品構建助手
│   ├── BuildProductHelper.cs    # 構建幫助类
│   ├── BuildPostProcessHelper.cs # 構建后處理
│   ├── IBuilderPreHookHandler.cs # 構建前鉤子
│   └── IBuilderPostHookHandler.cs # 構建后鉤子
├── BuildWebGLTools/             # WebGL 構建工具
│   └── BuildWebGLToolsWithHybridCLR.cs # HybridCLR WebGL 構建
├── Cropping/                     # 图片裁剪工具
│   └── CroppingWindow.cs        # 裁剪窗口
├── Inspector/                    # 自定义檢視面板
│   ├── BaseComponentInspector.cs # 基礎組件檢視
│   ├── ObjectPoolComponentInspector.cs # 物件池檢視
│   └── ReferencePoolComponentInspector.cs # 引用池檢視
├── InspectorLockShortcut/        # 檢視面板锁定
│   └── InspectorLockShortcut.cs # 快捷键锁定
├── MiniGame/                      # 小遊戲平台適配（21個平台） ⭐
│   ├── MiniGameDefineSymbolHelper.cs # 基礎宏定義管理
│   ├── DomesticMiniGames/          # 國內小遊戲
│   │   ├── MiniGameDefineSymbolHelper.WeChat.cs # 微信
│   │   ├── MiniGameDefineSymbolHelper.Alipay.cs # 支付寶
│   │   ├── MiniGameDefineSymbolHelper.DouYin.cs # 抖音
│   │   ├── MiniGameDefineSymbolHelper.KuaiShou.cs # 快手
│   │   ├── MiniGameDefineSymbolHelper.Baidu.cs # 百度
│   │   ├── MiniGameDefineSymbolHelper.JingDong.cs # 京東
│   │   ├── MiniGameDefineSymbolHelper.Meituan.cs # 美團
│   │   ├── MiniGameDefineSymbolHelper.Taobao.cs # 淘寶
│   │   └── MiniGameDefineSymbolHelper.Bilibili.cs # Bilibili
│   ├── InternationalMiniGames/     # 國際小遊戲
│   │   ├── MiniGameDefineSymbolHelper.CrazyGames.cs # CrazyGames
│   │   ├── MiniGameDefineSymbolHelper.Discord.cs # Discord
│   │   ├── MiniGameDefineSymbolHelper.Facebook.cs # Facebook
│   │   ├── MiniGameDefineSymbolHelper.GooglePlay.cs # Google Play
│   │   ├── MiniGameDefineSymbolHelper.Poki.cs # Poki
│   │   ├── MiniGameDefineSymbolHelper.TikTok.cs # TikTok
│   │   └── MiniGameDefineSymbolHelper.YouTube.cs # YouTube
│   ├── DeviceOEMs/                # 設備廠商小遊戲
│   │   ├── MiniGameDefineSymbolHelper.Huawei.cs # 華為
│   │   ├── MiniGameDefineSymbolHelper.OPPO.cs # OPPO
│   │   ├── MiniGameDefineSymbolHelper.Vivo.cs # vivo
│   │   └── MiniGameDefineSymbolHelper.Xiaomi.cs # 小米
│   └── GamePlatforms/             # 遊戲平台
│       └── MiniGameDefineSymbolHelper.TapTap.cs # TapTap
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
    ├── Type.cs                  # 類型工具
    └── OpenFolder.cs            # 文件夹打开
```

#### Editor 子模組详解

| 子模組 | 描述 | 主要功能 |
|--------|------|----------|
| **BuildHotfix** | 熱更新構建 | HybridCLR 熱更新程序集構建和管理 |
| **BuildProduct** | 产品構建 | 構建流程自动化、前后置處理鉤子 |
| **BuildWebGLTools** | WebGL 構建 | WebGL 平台专用構建工具 |
| **Cropping** | 图片裁剪 | 可视化图片裁剪工具 |
| **Inspector** | 自定义檢視面板 | 物件池、引用池可视化监控 |
| **InspectorLockShortcut** | 面板锁定 | 快捷键锁定 Inspector 面板 |
| **MiniGame** | 小遊戲適配 | 一鍵切換21個小遊戲平台（按國內、國際、設備廠商、遊戲平台分類） |
| **PackageManager** | 包管理 | 可视化包管理界面 |
| **UpdatePackages** | 包更新 | 批量更新項目依赖 |
| **Welcome** | 欢迎界面 | 新用户引导和快速入口 |
| **Misc** | 杂项工具 | 日志、宏定义、類型等辅助工具 |

---

## 🚀 快速开始

### 安裝方式

#### 方式一：Unity Package Manager（推薦）

1. 打开 Unity 編輯器
2. 打开 `Window` → `Package Manager`
3. 点击左上角的 `+` 按鈕
4. 选择 `Add package from git URL`
5. 輸入: `https://github.com/GameFrameX/com.gameframex.unity.git`

#### 方式二：手动下载

1. 下载最新的 [Release](https://github.com/GameFrameX/com.gameframex.unity/releases)
2. 解壓到項目的 `Packages` 目錄下

### 基礎使用

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 获取物件池組件
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();
        
        // 获取引用池組件
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();
        
        // 使用擴展方法
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

---

## 💡 使用示例

### Runtime 使用示例

#### 🎯 物件池系統

```csharp
// 获取物件池組件
var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

// 创建物件池
objectPool.CreatePool<MyObject>("MyObjectPool", 10, 100);

// 从池中获取物件
var obj = objectPool.Spawn<MyObject>("MyObjectPool");

// 归还物件到池中
objectPool.Unspawn(obj);

// 銷毀物件池
objectPool.DestroyPool("MyObjectPool");
```

#### 📝 擴展方法使用

```csharp
// Transform 擴展
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// Vector3 擴展
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);

// GameObject 擴展
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

#### 📡 事件系統

```csharp
// 定义事件參數
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

#### 🎮 小遊戲平台適配

在 Unity 選單中可以快速切換小遊戲平台：

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Domestic Mini Games(國內小遊戲)/
│   │   ├── Enable WeChat Mini Game (開啟[微信小遊戲]適配)
│   │   ├── Enable Alipay Mini Game (開啟[支付寶小遊戲]適配)
│   │   ├── Enable DouYin Mini Game (開啟[抖音小遊戲]適配)
│   │   ├── Enable KuaiShou Mini Game (開啟[快手小遊戲]適配)
│   │   ├── Enable Baidu Mini Game (開啟[百度小遊戲]適配)
│   │   ├── Enable JingDong Mini Game (開啟[京東小遊戲]適配)
│   │   ├── Enable Meituan Mini Game (開啟[美團小遊戲]適配)
│   │   ├── Enable Taobao Mini Game (開啟[淘寶小遊戲]適配)
│   │   └── Enable Bilibili Mini Game (開啟[Bilibili小遊戲]適配)
│   ├── International Mini Games(國際小遊戲)/
│   │   ├── Enable Discord Mini Game
│   │   ├── Enable YouTube Mini Game
│   │   ├── Enable Facebook Mini Game
│   │   ├── Enable Google Play Mini Game
│   │   ├── Enable TikTok Mini Game
│   │   ├── Enable CrazyGames Mini Game
│   │   └── Enable Poki Mini Game
│   ├── Device OEMs(設備廠商)/
│   │   ├── Enable Huawei Mini Game (開啟[華為小遊戲]適配)
│   │   ├── Enable OPPO Mini Game (開啟[OPPO小遊戲]適配)
│   │   ├── Enable Vivo Mini Game (開啟[vivo小遊戲]適配)
│   │   └── Enable Xiaomi Mini Game (開啟[小米小遊戲]適配)
│   └── Game Platforms(遊戲平台)/
│       └── Enable TapTap Mini Game (開啟[TapTap小遊戲]適配)
```

#### 🏗️ 構建工具

```
GameFrameX/
├── Build Hotfix (構建熱更新)
├── Build Product (構建产品)
└── Build WebGL With HybridCLR (WebGL熱更新構建)
```

#### 📦 包管理

```
GameFrameX/
├── Package Manager (包管理器)
└── Update All Packages (更新所有包)
```

---

## 🎯 平台支援

### 操作系統平台

| 平台 | 状态 | 支援版本 |
|------|------|----------|
| Windows | ✅ 已支援 | Unity 2019.4+ |
| macOS | ✅ 已支援 | Unity 2019.4+ |
| Linux | ✅ 已支援 | Unity 2019.4+ |
| iOS | ✅ 已支援 | Unity 2019.4+ |
| Android | ✅ 已支援 | Unity 2019.4+ |
| WebGL | ✅ 已支援 | Unity 2019.4+ |

### 小遊戲平台適配

GameFrameX 提供一鍵切換的小遊戲平台適配功能，支援**全球21個主流小遊戲平台**：

#### 🇨🇳 國內小遊戲（9個）

| 平台 | 宏定義 | 地區 | 選單優先級 |
|------|--------|------|-----------|
| 微信小程式 | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 🇨🇳 中國 | 2000 |
| 支付寶小程式 | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 🇨🇳 中國 | 2400 |
| 抖音小程式 | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 🇨🇳 中國 | 2100 |
| 快手小程式 | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 🇨🇳 中國 | 2200 |
| 百度小程式 | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 🇨🇳 中國 | 2300 |
| 京東小程式 | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 🇨🇳 中國 | 2500 |
| 淘寶小程式 | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 🇨🇳 中國 | 2600 |
| 美團小程式 | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 🇨🇳 中國 | 2800 |
| Bilibili 小程式 | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 🇨🇳 中國 | 2900 |

#### 🌍 國際小遊戲（7個）

| 平台 | 宏定義 | 地區 | 選單優先級 |
|------|--------|------|-----------|
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 🌍 全球 | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 🌍 全球 | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 🌍 全球 | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 🌍 全球 | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 🌍 全球 | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 🌍 全球 | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 🌍 全球 | 3700 |

#### 📱 設備廠商（4個）

| 平台 | 宏定義 | 地區 | 選單優先級 |
|------|--------|------|-----------|
| 華為小遊戲 | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 🇨🇳 中國 | 3400 |
| OPPO 小遊戲 | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 🇨🇳 中國 | 3200 |
| vivo 小遊戲 | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 🇨🇳 中國 | 3100 |
| 小米小遊戲 | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 🇨🇳 中國 | 3300 |

#### 🎮 遊戲平台（1個）

| 平台 | 宏定義 | 地區 | 選單優先級 |
|------|--------|------|-----------|
| TapTap 小遊戲 | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 🇨🇳 中國 | 2700 |

#### 宏定義說明

- **統一宏定義**: `ENABLE_WEBGL_MINI_GAME` - 所有小遊戲平台共享
- **平台宏定義**: 各平台獨立宏定義，用于條件編譯
- **互斥機制**: 開啟任一小遊戲平台會自動關閉其他平台
- **選單位置**: `GameFrameX/Scripting Define Symbols/[分類]/Enable [平台名稱] Mini Game`

---

## 📚 文檔与資源

- 📖 **完整文檔**: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🎯 **API 参考**: [API Documentation](https://gameframex.doc.alianblank.com/api)
- 📝 **示例項目**: [Examples Repository](https://github.com/GameFrameX/Examples)
- 🎬 **视频教程**: [YouTube 频道](https://youtube.com/gameframex)

---

## 🤝 社區与支援

- 💬 **QQ 讨论群**: [467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)
- 🐛 **问题反馈**: [GitHub Issues](https://github.com/GameFrameX/com.gameframex.unity/issues)
- 💡 **功能建議**: [GitHub Discussions](https://github.com/GameFrameX/com.gameframex.unity/discussions)

---

## 🔄 更新日志

### v1.3.6 (2025-05-28)
- 🐛 修復文件 GUID 重复的问题
- ✨ 新增美团、Bilibili 小遊戲平台適配
- ✨ 新增更多擴展方法
- 📚 完善 README 文檔结构，补全所有模組和文件的完整說明
- 🔧 優化物件池效能
- 📚 完善文檔說明

查看完整更新日志: [CHANGELOG.md](CHANGELOG.md)

---

## 📄 开源協議

本項目採用 **MIT License** 与 **Apache License 2.0** 双许可证分发。

完整许可证文本请参见: [LICENSE.md](LICENSE.md)

---

## 👨‍💻 作者信息

**Blank**

- 🌐 Website: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🐙 GitHub: [@GameFrameX](https://github.com/GameFrameX)

---

<div align="center">

**如果这个項目对你有幫助，请给我们一個 ⭐ Star！**

[⬆ 回到顶部](#gameframex-unity-package)

</div>
