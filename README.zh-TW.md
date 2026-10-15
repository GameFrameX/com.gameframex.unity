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

**獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使**

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#quick-start) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

<br />

</div>

---

## 項目簡介

GameFrameX 是專為獨立遊戲開發者設計的現代化 Unity 遊戲框架，提供完整的前後端一體化解決方案。框架採用三層模組化架構，內建豐富的遊戲開發工具與元件，幫助開發者快速交付高品質的遊戲專案。

`com.gameframex.unity` 是核心包，包含運行時核心（事件池、引用池、任務池、物件池、變數 / 屬性系統、擴充函式庫、Helper、工具類別）、編輯器工具鏈（建置管線與熱更新、WebGL + HybridCLR 工具、小遊戲平台適配、歡迎視窗、Inspector 增強、Inspector 鎖定快捷鍵、裁切工具、Scripting Define Symbol 管理器）、iOS 原生橋接外掛以及內建的 .NET 函式庫（SharpZipLib、System.Memory / Buffers / Pipelines）。它是所有其它 GameFrameX Unity 包的基礎依賴。

### 功能特性

- 三層架構：Runtime / Plugins / Editor 職責清晰、解耦良好。
- 運行時工具集：EventPool、ReferencePool、TaskPool、ObjectPool、Variable / Property、擴充函式庫、Helper 類別。
- 編輯器工具鏈：BuildProduct / BuildHotfix 建置管線、BuildWebGLTools + HybridCLR、21 個小遊戲平台適配、歡迎視窗、Inspector 增強。
- 工具層：加密（AES / RSA / DSA）、壓縮（ZIP）、雜湊（MD5 / SHA1 / HMAC / CRC）、JSON、檔案 / 路徑 / 網路輔助、ID 產生器、文字處理。
- 支援熱更新：標準建置流程中整合 HybridCLR 熱更新建置管線。
- 多平台：Windows、macOS、Linux、iOS、Android、WebGL，以及全球 21 個小遊戲平台。

### 系統需求

- Unity：2019.4 及以上
- 平台：Windows、macOS、Linux、iOS、Android、WebGL
- .NET：.NET Standard 2.0+

---

## 快速開始

### 安裝

選擇以下任一方式：

1. 編輯 Unity 專案的 `Packages/manifest.json`，新增 `scopedRegistries` 區段：
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

   `scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex.unity` 開頭的套件才會從這個註冊表取得。

2. 直接在 `manifest.json` 的 `dependencies` 節點下新增以下內容：
   ```json
   {
     "com.gameframex.unity": "https://github.com/GameFrameX/com.gameframex.unity.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式新增套件，網址為：`https://github.com/GameFrameX/com.gameframex.unity.git`
4. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。

> 透過方式 2–4 安裝時，編輯器載入後會自動偵測並補全 `manifest.json` 中缺失的 GameFrameX scoped registry 設定（寫入前備份為 `manifest.json.gameframex.bak`）；已有第三方 registry（如 OpenUPM）覆蓋該 scope 時跳過，`-batchmode`（CI）環境下不自動變更。
> 同時會預設註冊 OpenUPM registry（`package.openupm.com`，scopes 留空，不變更已存在的同 URL 條目設定）。

### 基礎使用

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 從框架入口取得元件
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

        // 取得引用池元件
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();

        // 使用引擎類型的擴充方法
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

### 零配置啟動（自動注入）

安裝本套件就是唯一的啟動步驟。`GameFrameXRuntimeBootstrap` 以 `RuntimeInitializeOnLoadMethod(BeforeSceneLoad)` 標記自身，框架在首場景載入前自動注入；`AfterSceneLoad` 時建立隱藏的 `[GameFrameX]` Host 節點，並在其上自動掛載全部 GameFrameX 元件。無需改場景、無需預製體、無需 Boot 腳本、無需寫程式碼——任意場景零改動即可啟動。

場景中手動擺放的 GameFrameX 元件（BaseComponent 及其兄弟元件）會被自動偵測並**自我抑制**：不註冊、不初始化、不驅動，並各自輸出帶 `[GameFrameX]` 前綴的警告。遷移做法：刪掉場景中手動擺放的 GFX 節點，把啟動權交給自動注入。

模組參數透過 `RuntimeInitializeOnLoadMethod(BeforeSceneLoad)` 中的 `GameFrameXOptions.Configure<T>(options)` 注入——見下一節。

### 模組 Options 啟動配置

各模組提供 `XxxOptions` 啟動配置（欄位預設值同原 Inspector 序列化預設），經 `GameFrameXOptions.Configure` 在模組首次實例化前一次性注入；元件上的序列化參數欄位已移除，編輯期配置入口統一收斂到程式碼。

```csharp
using GameFrameX.Runtime;

// 唯一注入時機：首場景載入前（BeforeSceneLoad）
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
static void ConfigureFramework()
{
    // 只寫差異欄位，其餘用套件預設值
    GameFrameXOptions.Configure(new DownloadOptions { Timeout = 60f });
}
```

- 優先級：`Configure` 注入值 > Options 欄位預設值；模組首次實例化時一次性消費。
- 時機：啟動期全量實例化完成後任何 `Configure` 都會拋出 `GameFrameworkException`——配置時機錯誤不會靜默。自動 Host 在 `AfterSceneLoad` 建立，仍晚於此處 `BeforeSceneLoad` 的呼叫。
- 分檔：欄位預設為即時型（運行期經模組公開 API 修改）；啟動期型欄位（代理數量、Helper 類型等）以 `[GameFrameXStartupOnly]` 標註，運行期修改會被拒絕並輸出 Warning。
- 未消費診斷：`Reset` / `Shutdown` 時對「已註冊但從未被任何模組消費」的 Options 輸出 Warning（成因通常是套件未安裝或元件被結構性停用）。

#### Options 貢獻者（靜默發現，C28 起）

不想寫任何啟動樣板？實作 `IGameFrameXOptionsContributor` 的普通類別會被執行時掃描器靜默發現並自動套用——無屬性、無 Bootstrap、無註冊程式碼：

```csharp
using GameFrameX.Runtime;

// 貢獻者類別只做 Configure，不承載初始化副作用；保持 public 無參建構子
[UnityEngine.Scripting.Preserve]
public sealed class MyGameOptionsContributor : IGameFrameXOptionsContributor
{
    public void ConfigureOptions(GameFrameXOptionsContributorContext context)
    {
        context.Configure(new DownloadOptions { Timeout = 60f });
    }
}
```

- 手動優先：遊戲側明確 `GameFrameXOptions.Configure` 永遠勝出——已 Configure 的類型貢獻者按類型粒度跳過並輸出 `[GameFrameX]` 診斷，與 BeforeSceneLoad 階段內執行順序無關。
- 套用時機：最早模組實例化之前，唯一入口 `BeforeSceneLoad`，每 domain 一次。
- 防裁剪：IL2CPP / 小遊戲裁剪環境下，實作類別需加 `[UnityEngine.Scripting.Preserve]`（如上）或納入裁剪保護清單，否則類型可能被裁掉導致靜默失效。同理，實作 `IGameFrameXRuntimeOverrideProvider` 的類別也不再需要手動 `RegisterProvider`，掃描即生效（缺 public 無參建構子會輸出 Warning 後跳過）。

#### 遲載入組件增量註冊（C29 起）

啟動後載入的組件（熱更 / 執行期載入）在 `AssemblyLoad` 時自動註冊其中的 Manager 描述符——遲載入的 `IXxxManager` 實作零手動呼叫即可 `GetModule<IXxxManager>()` 命中（含命名慣例兜底路徑，因為 `Utility.Assembly` 由啟動快照改為活取 AppDomain）。每次增量註冊輸出帶組件名與描述符數的 `[GameFrameX]` 診斷；Editor / Tests 組件與全量掃描同門跳過，同名組件重複註冊（熱更卸載後再載入）冪等去重。

- 僅增量註冊 Manager 描述符：遲載入組件內的 `GameFrameworkComponent` **不自動補掛**——掛載時機與場景狀態強相關，需要時手動 `AddComponent`。
- 遲載入組件內的 Options 貢獻者會被 Configure 時機守衛響亮拒絕（模組已實例化）——刻意不提供半靜默通道。
- 訂閱生命週期：`SubsystemRegistration` 先退訂再訂（關閉 Domain Reload 的重複 Play 不雙訂閱）；`GameFrameworkEntry.Shutdown` 後**刻意不退訂**——Shutdown 清空映射後新載入組件仍應能註冊（Restart 語義）。


用工廠閉包覆寫 Helper 實作（Custom 引用優先，取代原 Inspector 的類型名稱下拉；UI 後端套件 FairyGUI / UGUI 經註冊表自動註冊預設實作）：

```csharp
GameFrameXOptions.Configure(new UIOptions
{
    UIFormHelperFactory = () => new MyUIFormHelper(),
});
```

活視圖：元件保留為薄入口（純轉發），Inspector 不再承載參數；Play 期可在模組除錯視圖中查看當前生效配置，運行期調整一律走 `GetModule<T>()` / `GameApp.X` 模組 API。

---

## 使用示例

### 物件池系統

```csharp
// 取得物件池元件
var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

// 建立物件池
objectPool.CreatePool<MyObject>("MyObjectPool", 10, 100);

// 從池中取得物件
var obj = objectPool.Spawn<MyObject>("MyObjectPool");

// 歸還物件到池中
objectPool.Unspawn(obj);

// 銷毀物件池
objectPool.DestroyPool("MyObjectPool");
```

### 擴充方法

```csharp
// Transform 擴充
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// Vector3 擴充
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);

// GameObject 擴充
gameObject.SetActiveOptimized(true);
gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
```

### 實用工具類別

```csharp
// 檔案操作
FileHelper.WriteAllBytes("path/to/file", data);
byte[] content = FileHelper.ReadAllBytes("path/to/file");
string size = FileHelper.GetBytesSize(1024 * 1024); // "1MB"

// AES 加密解密
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// 雜湊計算
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSON 序列化
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

### 事件系統

```csharp
// 定義事件參數
public class PlayerDeadEventArgs : BaseEventArgs
{
    public int PlayerId { get; set; }
    public float Damage { get; set; }
}

// 訂閱事件
GameEntry.Event.Subscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);

// 觸發事件
GameEntry.Event.Fire(this, PlayerDeadEventArgs.Create(playerId, damage));

// 取消訂閱
GameEntry.Event.Unsubscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);
```

### 編輯器工具

#### 小遊戲平台適配

在 Unity 選單中可一鍵切換小遊戲平台：

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

#### 建置工具

```
GameFrameX/
├── Build Hotfix (建置熱更新)
├── Build Product (建置產品)
└── Build WebGL With HybridCLR (WebGL熱更新建置)
```

---

## 架構概覽

GameFrameX 採用清晰的三層架構，每個模組各司其職、協同運作。

### 運行時（Runtime）

運行時核心程式碼，提供遊戲運行期所需的所有功能。

```
Runtime/
├── Base/                          框架核心基礎
│   ├── DataStruct/                資料結構
│   ├── EventPool/                 事件池系統
│   ├── Log/                       日誌系統
│   ├── ReferencePool/             引用池系統
│   ├── TaskPool/                  工作池系統
│   ├── Variable/                  變數系統
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
├── Extension/                     擴充函式庫
│   ├── Extension/                 通用擴充
│   ├── SequenceReader/            Sequence Reader
│   ├── UnityEngine.GameObject/    GameObject 擴充
│   └── UnityEngine/               Unity 類型擴充（Transform、Vector2/3/4）
├── Helper/                        Helper 類別（Application、Camera、File、Path、
│                                  Math、Random、Timer、Network、JSON、Renderer、
│                                  Zip、DoTween、按週期拆分的 Timer 等）
├── ObjectPool/                    物件池系統
├── Property/                      屬性系統（BindableProperty）
├── ReferencePool/                 引用池系統
└── Utility/                       工具類別（Encryption、Compression、Hash、JSON、
                                   Marshal、Text、Verifier、Const、IdGenerator）
```

### 插件層（Plugins）

原生平台外掛與內建的第三方 .NET 函式庫。

```
Plugins/
├── iOS/                           iOS 原生橋接（GameFrameX.mm、TrackingAuthorization）
├── ICSharpCode.SharpZipLib.dll    ZIP 壓縮函式庫
├── Microsoft.NET.StringTools.dll  字串工具
├── System.Buffers.dll             記憶體緩衝
├── System.IO.Pipelines.dll        IO 管線
├── System.Memory.dll              記憶體管理
└── System.Runtime.CompilerServices.Unsafe.dll   執行階段支援
```

### 編輯器（Editor）

編輯器工具與擴充，提升開發效率。

```
Editor/
├── BuildHotfix/                   熱更新建置工具（HybridCLR）
├── BuildProduct/                  產品建置（帶 Pre/Post Hook）
├── BuildWebGLTools/               WebGL 建置最佳化（HybridCLR）
├── Cropping/                      影像裁切工具
├── Inspector/                     Base / ObjectPool / ReferencePool 自訂 Inspector
├── InspectorLockShortcut/         Inspector 面板鎖定快捷鍵
├── MiniGame/                      小遊戲平台適配（21 個平台）
│   ├── DomesticMiniGames/         微信、支付寶、抖音、快手、百度、京東、美團、淘寶、Bilibili
│   ├── InternationalMiniGames/    Discord、YouTube、Facebook、Google Play、TikTok、CrazyGames、Poki
│   ├── DeviceOEMs/                華為、OPPO、Vivo、小米
│   └── GamePlatforms/             TapTap
├── Welcome/                       歡迎視窗
└── Misc/                          日誌轉向、Scripting Define Symbols、類型工具、開啟資料夾
```

---

## 平台支援

### 作業系統

| 平台 | 狀態 | 支援版本 |
|------|------|----------|
| Windows | 支援 | Unity 2019.4+ |
| macOS | 支援 | Unity 2019.4+ |
| Linux | 支援 | Unity 2019.4+ |
| iOS | 支援 | Unity 2019.4+ |
| Android | 支援 | Unity 2019.4+ |
| WebGL | 支援 | Unity 2019.4+ |

### 小遊戲平台適配

GameFrameX 提供一鍵切換的小遊戲平台適配，覆蓋全球 21 個主流小遊戲平台。

#### 國內小遊戲（9 個）

| 平台 | 巨集定義 | 地區 | 選單優先級 |
|------|---------|------|-----------|
| 微信小程式 | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 中國 | 2000 |
| 支付寶小程式 | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 中國 | 2400 |
| 抖音小程式 | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 中國 | 2100 |
| 快手小程式 | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 中國 | 2200 |
| 百度小程式 | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 中國 | 2300 |
| 京東小程式 | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 中國 | 2500 |
| 淘寶小程式 | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 中國 | 2600 |
| 美團小程式 | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 中國 | 2800 |
| Bilibili 小程式 | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 中國 | 2900 |

#### 國際小遊戲（7 個）

| 平台 | 巨集定義 | 地區 | 選單優先級 |
|------|---------|------|-----------|
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 全球 | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 全球 | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 全球 | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 全球 | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 全球 | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 全球 | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 全球 | 3700 |

#### 設備廠商（4 個）

| 平台 | 巨集定義 | 地區 | 選單優先級 |
|------|---------|------|-----------|
| 華為小遊戲 | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 中國 | 3400 |
| OPPO 小遊戲 | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 中國 | 3200 |
| vivo 小遊戲 | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 中國 | 3100 |
| 小米小遊戲 | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 中國 | 3300 |

#### 遊戲平台（1 個）

| 平台 | 巨集定義 | 地區 | 選單優先級 |
|------|---------|------|-----------|
| TapTap 小遊戲 | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 中國 | 2700 |

#### 巨集定義說明

- 統一巨集定義：`ENABLE_WEBGL_MINI_GAME`，所有小遊戲平台共用。
- 平台巨集定義：各平台獨立巨集定義，用於條件編譯。
- 互斥機制：開啟任一小遊戲平台會自動關閉其他平台。
- 選單位置：`GameFrameX/Scripting Define Symbols/[分類]/Enable [平台名稱] Mini Game`。

---

## 依賴

`package.json` 中宣告：

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

## 文檔與資源

- 文檔：https://gameframex.doc.alianblank.com
- API 參考：https://gameframex.doc.alianblank.com/api
- 範例專案：https://github.com/GameFrameX/Examples
- 影片教學：https://www.youtube.com/channel/UCD9QhSFJ5xZkn5NTSV-DVAw

---

## 社區與支援

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

- 問題回報：https://github.com/GameFrameX/com.gameframex.unity/issues
- 功能建議：https://github.com/GameFrameX/com.gameframex.unity/discussions

---

## 更新日誌

完整版本歷史請參閱 [CHANGELOG.md](CHANGELOG.md)。

---

## 開源協議

完整授權條款請參閱 [LICENSE.md](LICENSE.md)。

---

<div align="center">

如果這個專案對你有幫助，請給我們一個 Star。

[回到頂部](#gameframex-unity-包)

</div>
