<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity パッケージ

[![License](https://img.shields.io/badge/license-blue.svg)](LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

[![Discord](https://img.shields.io/badge/-5865F2?logo=discord&logoColor=white)](https://discord.gg/VDWUjWMDw9)
[![GitHub](https://img.shields.io/badge/-181717?logo=github&logoColor=white)](https://github.com/GameFrameX/gameframex)
[![Bilibili](https://img.shields.io/badge/-00A1D6?logo=bilibili&logoColor=white)](https://www.bilibili.com/video/BV1yrpeepEn7)
[![Gitee](https://img.shields.io/badge/-C71D23?logo=gitee&logoColor=white)](https://gitee.com/GameFrameX/gameframex)

**インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援**

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#quick-start) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

<br />

</div>

---

## プロジェクト概要

GameFrameX は独立系ゲーム開発者向けに設計されたモダンな Unity ゲームフレームワークで、エンドツーエンドのゲーム開発ソリューションを提供します。三層モジュラーアーキテクチャを採用しており、豊富な組み込みツールとコンポーネントにより、高品質なゲームプロジェクトを迅速にリリースできます。

`com.gameframex.unity` はコアパッケージで、ランタイムカーネル（EventPool、ReferencePool、TaskPool、ObjectPool、Variable / Property システム、拡張ライブラリ、Helper、ユーティリティクラス）、エディタツールチェーン（BuildProduct / BuildHotfix パイプライン、WebGL + HybridCLR ツール、ミニゲームプラットフォーム適応、Welcome ウィンドウ、Inspector 強化、Inspector ロックショートカット、クロッピング、Scripting Define Symbol マネージャー）、iOS ネイティブブリッジプラグイン、ならびにバンドルされた .NET ライブラリ（SharpZipLib、System.Memory / Buffers / Pipelines）を含みます。他のすべての GameFrameX Unity パッケージが依存する基盤パッケージです。

### 機能

- 三層アーキテクチャ：Runtime / Plugins / Editor が明確な責務分離で協調動作。
- 豊富なランタイムツール：EventPool、ReferencePool、TaskPool、ObjectPool、Variable / Property、拡張ライブラリ、Helper クラス。
- エディタツールチェーン：BuildProduct / BuildHotfix パイプライン、BuildWebGLTools + HybridCLR、21 プラットフォームのミニゲーム対応、Welcome ウィンドウ、Inspector 強化。
- ユーティリティ層：暗号化（AES / RSA / DSA）、圧縮（ZIP）、ハッシュ（MD5 / SHA1 / HMAC / CRC）、JSON、ファイル / パス / ネットワークヘルパー、ID ジェネレーター、テキスト処理。
- ホットフィックス対応：標準ビルドフローに統合された HybridCLR ホットフィックスビルドパイプライン。
- マルチプラットフォーム：Windows、macOS、Linux、iOS、Android、WebGL に加え、世界 21 のミニゲームプラットフォーム。

### システム要件

- Unity：2019.4 以降
- プラットフォーム：Windows、macOS、Linux、iOS、Android、WebGL
- .NET：.NET Standard 2.0+

---

## クイックスタート

### インストール

以下のいずれかの方法を選択してください：

1. Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加します：
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

   `scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex.unity` で始まるパッケージのみがこのレジストリから取得されます。

2. `manifest.json` の dependencies に追加：
   ```json
   {
     "com.gameframex.unity": "https://github.com/GameFrameX/com.gameframex.unity.git"
   }
   ```
3. Unity の `Package Manager` で `Git URL` を使用して追加します、URL：`https://github.com/GameFrameX/com.gameframex.unity.git`
4. リポジトリをダウンロードして Unity プロジェクトの `Packages` ディレクトリに配置します、自動的に読み込まれます。

### 基本的な使用方法

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // フレームワークエントリからコンポーネントを取得
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

        // リファレンスプールコンポーネントを取得
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();

        // エンジン型の拡張メソッドを使用
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

### ホストフレームワーク統合（Headless 3 行契約）

GameFrameX は純粋な C# カーネルとして他の Unity フレームワークやカスタムホストに組み込めます：実行時に `[GameFrameX]` ノードを作成せず、GameFrameX の業務コンポーネントもアタッチせず、登録されたモジュールはホストが駆動します。

```csharp
using GameFrameX.Runtime;

// 1. 起動：最初のシーンロード前に 1 回呼び出す。重複起動はシングルインスタンスガードにより拒否される
GameFrameworkStartup.Headless();

// 2. 駆動：ホストのメインループから毎フレーム転送する
//    （または UnityGameFrameworkDriver コンポーネントをアタッチすれば代わりに駆動してくれる）
GameFrameworkEntry.Update(deltaTime, unscaledDeltaTime);

// 3. 終了：ホスト終了時に必ず呼び出すこと。さもないと静的モジュールがリークする
//    （UnityGameFrameworkDriver は Destroy 時に自動で呼び出す）
GameFrameworkEntry.Shutdown();
```

ログルーティング — `ILogHelper` を実装してフレームワークのログをホスト自身のログシステムにパイプします：

```csharp
public sealed class HostLogBridge : GameFrameworkLog.ILogHelper
{
    public void Log(GameFrameworkLogLevel level, object message)
    {
        MyLogger.Write(level, message.ToString()); // ホスト自身のログシステム
    }
}

GameFrameworkLog.SetLogHelper(new HostLogBridge());
```

制約と挙動：

- 単一インスタンス：実行ごとに GameFrameX インスタンスは 1 つだけ。セッション実行中に `Headless()` を再度呼ぶと例外がスローされ、`Shutdown()` 後は再入可能。
- 三モード相互排他：Headless 契約が有効な間は、シーン内の GFX 手動入口と自動パッケージモード Host は両方とも拒否され診断が出力される。Headless モードでは GameFrameX コンポーネントのアタッチは禁止。
- メモリ不足：ホスト自身のメモリ不足 / 一時停止イベントを `GameFrameworkEntry.NotifyLowMemory()` で転送する。Unity 経路（BaseComponent、UnityGameFrameworkDriver）は自動で配線され、挙動も同じ。

### モジュール Options 起動構成

各モジュールは `XxxOptions` 起動構成を提供します（フィールドのデフォルトは従来の Inspector シリアライズデフォルトと一致）。モジュールが最初にインスタンス化される前に `GameFrameXOptions.Configure` で 1 回注入します。コンポーネントのシリアライズパラメータフィールドは削除され、編集時の構成エントリはコードに統一されました。

```csharp
using GameFrameX.Runtime;

// 三モード共通の注入タイミング：Manual / AutoPackage は最初のシーンロード前（BeforeSceneLoad）
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
static void ConfigureFramework()
{
    // 差分フィールドだけ書き、残りはパッケージのデフォルトを使用
    GameFrameXOptions.Configure(new DownloadOptions { Timeout = 60f });
}

// Headless：ホストは GameFrameworkStartup.Headless() の直前に Configure を呼ぶ
//（自動パッケージモード Host は AfterSceneLoad で生成されるため、BeforeSceneLoad の Configure より遅い）
```

- 優先度：`Configure` で注入された値 > Options フィールドのデフォルト。モジュールの初回インスタンス化時に一度だけ消費される。
- タイミング：起動期の全量インスタンス化完了後に `Configure` を呼ぶと `GameFrameworkException` がスローされる — タイミングを誤っても静かに失敗することはない。
- 分類：フィールドはデフォルトでリアルタイム型（実行時にモジュールの公開 API で変更可能）。起動期型フィールド（エージェント数、Helper 型など）は `[GameFrameXStartupOnly]` でマークされ、実行時の変更は Warning で拒否される。
- 未消費診断：`Reset` / `Shutdown` 時に「登録されたがどのモジュールにも消費されなかった」 Options について Warning が出力される（通常は未インストールのパッケージまたは構造的に無効化されたコンポーネント）。

Helper 実装をファクトリクロージャでオーバーライドします（Custom 参照が優先され、従来の Inspector の型名ドロップダウンを置き換え。UI バックエンドパッケージ FairyGUI / UGUI はバックエンドレジストリ経由でデフォルトを自動登録）：

```csharp
GameFrameXOptions.Configure(new UIOptions
{
    UIFormHelperFactory = () => new MyUIFormHelper(),
});
```

ライブビュー：コンポーネントは薄いエントリ（純粋な転送）のままで、Inspector はパラメータを持たない。Play 中はモジュールのデバッグビューで有効な構成を確認でき、実行時の調整は必ず `GetModule<T>()` / `GameApp.X` モジュール API 経由で行う。

---

## 使用例

### オブジェクトプールシステム

```csharp
// オブジェクトプールコンポーネントを取得
var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

// オブジェクトプールを作成
objectPool.CreatePool<MyObject>("MyObjectPool", 10, 100);

// プールからオブジェクトを取得
var obj = objectPool.Spawn<MyObject>("MyObjectPool");

// オブジェクトをプールに戻す
objectPool.Unspawn(obj);

// オブジェクトプールを破棄
objectPool.DestroyPool("MyObjectPool");
```

### 拡張メソッド

```csharp
// Transform の拡張
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// Vector3 の拡張
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);

// GameObject の拡張
gameObject.SetActiveOptimized(true);
gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
```

### ユーティリティクラス

```csharp
// ファイル操作
FileHelper.WriteAllBytes("path/to/file", data);
byte[] content = FileHelper.ReadAllBytes("path/to/file");
string size = FileHelper.GetBytesSize(1024 * 1024); // "1MB"

// AES 暗号化 / 復号化
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// ハッシュ計算
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSON シリアライズ
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

### イベントシステム

```csharp
// イベント引数を定義
public class PlayerDeadEventArgs : BaseEventArgs
{
    public int PlayerId { get; set; }
    public float Damage { get; set; }
}

// イベントを購読
GameEntry.Event.Subscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);

// イベントを発火
GameEntry.Event.Fire(this, PlayerDeadEventArgs.Create(playerId, damage));

// 購読を解除
GameEntry.Event.Unsubscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);
```

### エディタツール

#### ミニゲームプラットフォーム適応

Unity メニューからミニゲームプラットフォームを素早く切り替え：

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Domestic Mini Games(国内ミニゲーム)/
│   │   ├── Enable WeChat Mini Game (WeChatミニゲームを有効化)
│   │   ├── Enable Alipay Mini Game (Alipayミニゲームを有効化)
│   │   ├── Enable DouYin Mini Game (DouYinミニゲームを有効化)
│   │   ├── Enable KuaiShou Mini Game (KuaiShouミニゲームを有効化)
│   │   ├── Enable Baidu Mini Game (Baiduミニゲームを有効化)
│   │   ├── Enable JingDong Mini Game (JDミニゲームを有効化)
│   │   ├── Enable Meituan Mini Game (Meituanミニゲームを有効化)
│   │   ├── Enable Taobao Mini Game (Taobaoミニゲームを有効化)
│   │   └── Enable Bilibili Mini Game (Bilibiliミニゲームを有効化)
│   ├── International Mini Games(国際ミニゲーム)/
│   │   ├── Enable Discord Mini Game
│   │   ├── Enable YouTube Mini Game
│   │   ├── Enable Facebook Mini Game
│   │   ├── Enable Google Play Mini Game
│   │   ├── Enable TikTok Mini Game
│   │   ├── Enable CrazyGames Mini Game
│   │   └── Enable Poki Mini Game
│   ├── Device OEMs(デバイスOEM)/
│   │   ├── Enable Huawei Mini Game (Huaweiミニゲームを有効化)
│   │   ├── Enable OPPO Mini Game (OPPOミニゲームを有効化)
│   │   ├── Enable Vivo Mini Game (vivoミニゲームを有効化)
│   │   └── Enable Xiaomi Mini Game (Xiaomiミニゲームを有効化)
│   └── Game Platforms(ゲームプラットフォーム)/
│       └── Enable TapTap Mini Game (TapTapミニゲームを有効化)
```

#### ビルドツール

```
GameFrameX/
├── Build Hotfix (ホットフィックスをビルド)
├── Build Product (プロダクトをビルド)
└── Build WebGL With HybridCLR (WebGLホットフィックスビルド)
```

---

## アーキテクチャ

GameFrameX は明確な三層アーキテクチャを採用しており、各モジュールがそれぞれの責務を持ち協調動作します。

### Runtime

ランタイムのコアコード。ゲームの実行時に必要なすべての機能を提供します。

```
Runtime/
├── Base/                          フレームワークの基礎
│   ├── DataStruct/                データ構造
│   ├── EventPool/                 イベントプール
│   ├── Log/                       ロギング
│   ├── ReferencePool/             リファレンスプール
│   ├── TaskPool/                  タスクプール
│   ├── Variable/                  バリアブルシステム
│   ├── Version/                   バージョン管理
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
│   ├── GameFrameworkStartup.cs
│   ├── IGameFrameworkModule.cs
│   ├── ObjectDontDestroyOnLoad.cs
│   └── UnityGameFrameworkDriver.cs
├── Extension/                     拡張メソッドライブラリ
│   ├── Extension/                 汎用拡張
│   ├── SequenceReader/            Sequence Reader
│   ├── UnityEngine.GameObject/    GameObject 拡張
│   └── UnityEngine/               Unity 型の拡張（Transform、Vector2/3/4）
├── Helper/                        Helper クラス（Application、Camera、File、Path、
│                                  Math、Random、Timer、Network、JSON、Renderer、
│                                  Zip、DoTween、期間分割の Timer など）
├── ObjectPool/                    オブジェクトプール
├── Property/                      プロパティシステム（BindableProperty）
├── ReferencePool/                 リファレンスプール
└── Utility/                       ユーティリティ（Encryption、Compression、Hash、JSON、
                                   Marshal、Text、Verifier、Const、IdGenerator）
```

### Plugins

ネイティブプラットフォームプラグインとバンドルされたサードパーティ製 .NET ライブラリ。

```
Plugins/
├── iOS/                           iOS ネイティブブリッジ（GameFrameX.mm、TrackingAuthorization）
├── ICSharpCode.SharpZipLib.dll    ZIP 圧縮ライブラリ
├── Microsoft.NET.StringTools.dll  文字列ツール
├── System.Buffers.dll             メモリバッファ
├── System.IO.Pipelines.dll        IO パイプライン
├── System.Memory.dll              メモリ管理
└── System.Runtime.CompilerServices.Unsafe.dll   ランタイムサポート
```

### Editor

開発効率を高めるためのエディタツールと拡張。

```
Editor/
├── BuildHotfix/                   ホットフィックスビルドツール（HybridCLR）
├── BuildProduct/                  プロダクトビルド（Pre / Post Hook 付き）
├── BuildWebGLTools/               WebGL ビルド最適化（HybridCLR）
├── Cropping/                      画像クロッピングツール
├── Inspector/                     Base / ObjectPool / ReferencePool のカスタム Inspector
├── InspectorLockShortcut/         Inspector パネルのロックショートカット
├── MiniGame/                      ミニゲーム適応（21 プラットフォーム）
│   ├── DomesticMiniGames/         WeChat、Alipay、DouYin、KuaiShou、Baidu、JD、Meituan、Taobao、Bilibili
│   ├── InternationalMiniGames/    Discord、YouTube、Facebook、Google Play、TikTok、CrazyGames、Poki
│   ├── DeviceOEMs/                Huawei、OPPO、Vivo、Xiaomi
│   └── GamePlatforms/             TapTap
├── Welcome/                       Welcome ウィンドウ
└── Misc/                          ログリダイレクト、Scripting Define Symbols、型ユーティリティ、フォルダを開く
```

---

## プラットフォーム対応

### オペレーティングシステム

| プラットフォーム | ステータス | 対応バージョン |
| ---------------- | ---------- | -------------- |
| Windows          | 対応       | Unity 2019.4+  |
| macOS            | 対応       | Unity 2019.4+  |
| Linux            | 対応       | Unity 2019.4+  |
| iOS              | 対応       | Unity 2019.4+  |
| Android          | 対応       | Unity 2019.4+  |
| WebGL            | 対応       | Unity 2019.4+  |

### ミニゲームプラットフォーム適応

GameFrameX はワンクリックでミニゲームプラットフォームを切り替えられ、世界 21 の主流ミニゲームプラットフォームをカバーします。

#### 国内ミニゲーム（9）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
| ---------------- | ------------ | ---- | -------------- |
| WeChat ミニゲーム | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 中国 | 2000 |
| Alipay ミニゲーム | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 中国 | 2400 |
| DouYin ミニゲーム | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 中国 | 2100 |
| KuaiShou ミニゲーム | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 中国 | 2200 |
| Baidu ミニゲーム | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 中国 | 2300 |
| JD ミニゲーム | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 中国 | 2500 |
| Taobao ミニプログラム | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 中国 | 2600 |
| Meituan ミニゲーム | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 中国 | 2800 |
| Bilibili ミニゲーム | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 中国 | 2900 |

#### 国際ミニゲーム（7）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
| ---------------- | ------------ | ---- | -------------- |
| Discord          | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME`       | グローバル | 2700 |
| YouTube          | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME`       | グローバル | 2800 |
| Facebook         | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME`     | グローバル | 2900 |
| Google Play      | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | グローバル | 3000 |
| TikTok           | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME`         | グローバル | 3500 |
| CrazyGames       | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | グローバル | 3600 |
| Poki             | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME`             | グローバル | 3700 |

#### デバイス OEM（4）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
| ---------------- | ------------ | ---- | -------------- |
| Huawei ミニゲーム | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 中国 | 3400 |
| OPPO ミニゲーム   | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME`    | 中国 | 3200 |
| Vivo ミニゲーム   | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME`     | 中国 | 3100 |
| Xiaomi ミニゲーム | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 中国 | 3300 |

#### ゲームプラットフォーム（1）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
| ---------------- | ------------ | ---- | -------------- |
| TapTap ミニゲーム | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 中国 | 2700 |

#### 定義シンボルの詳細

- 共通定義シンボル：`ENABLE_WEBGL_MINI_GAME` がすべてのミニゲームプラットフォームで共有されます。
- プラットフォーム定義シンボル：各プラットフォームが独立した定義シンボルを持ち、条件コンパイルに使用します。
- 相互排他：あるミニゲームプラットフォームを有効にすると他は自動的に無効になります。
- メニュー位置：`GameFrameX/Scripting Define Symbols/[カテゴリ]/Enable [プラットフォーム名] Mini Game`。

---

## 依存関係

`package.json` で宣言：

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

開発ツール：`semantic-release`、`@semantic-release/changelog`、`@semantic-release/git`。

---

## ドキュメントとリソース

- ドキュメント：https://gameframex.doc.alianblank.com
- API リファレンス：https://gameframex.doc.alianblank.com/api
- サンプルプロジェクト：https://github.com/GameFrameX/Examples
- 動画チュートリアル：https://www.youtube.com/channel/UCD9QhSFJ5xZkn5NTSV-DVAw

---

## コミュニティとサポート

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

- GitHub Issues：https://github.com/GameFrameX/com.gameframex.unity/issues
- GitHub Discussions：https://github.com/GameFrameX/com.gameframex.unity/discussions

---

## 変更履歴

完全なリリース履歴は [CHANGELOG.md](CHANGELOG.md) を参照してください。

---

## ライセンス

完全なライセンス条文は [LICENSE.md](LICENSE.md) を参照してください。

---

<div align="center">

このプロジェクトが役に立ったら、Star をお願いします。

[トップへ戻る](#gameframex-unity-パッケージ)

</div>
