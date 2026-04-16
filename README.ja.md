<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity パッケージ

[![Version](https://img.shields.io/badge/version-1.3.6-blue.svg)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity](https://img.shields.io/badge/Unity-2019.4+-green.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT+Apache%202.0-orange.svg)](LICENSE.md)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

**インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援**

[📖 ドキュメント](https://gameframex.doc.alianblank.com) • [🚀 クイックスタート](#🚀-クイックスタート) • [💬 QQグループ: 467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)

---

🌐 **言語**: [English](./README.md) | [繁體中文](./README.zh-TW.md) | [简体中文](./README.zh-CN.md) | [日本語](./README.ja.md) | [한국어](./README.ko.md)

---

</div>

## 📑 目次

- [🏗️ プロジェクト概要](#🏗️-プロジェクト概要)
- [📂 アーキテクチャ](#📂-アーキテクチャ)
  - [Runtime モジュールの詳細](#📦-runtime-モジュール)
  - [Plugins モジュールの詳細](#🔌-plugins-モジュール)
  - [Editor モジュールの詳細](#🛠️-editor-モジュール)
- [🚀 クイックスタート](#🚀-クイックスタート)
- [💡 使用例](#💡-使用例)
  - [Runtime 使用例](#runtime-使用例)
  - [Editor ツール](#editor-ツール)
- [🎯 プラットフォーム対応](#🎯-プラットフォーム対応)
- [📚 ドキュメントとリソース](#📚-ドキュメントとリソース)
- [🤝 コミュニティとサポート](#🤝-コミュニティとサポート)
- [� 変更履歴](#🔄-変更履歴)
- [�📄 ライセンス](#📄-ライセンス)
- [👨‍💻 作者](#👨‍💻-作者)

---

## 🏗️ プロジェクト概要

GameFrameX は、インディpendentゲーム開発者向けに設計された最新のUnityゲームフレームワークであり、ゲーム開発のための完全なエンドツーエンドソリューションを提供します。このフレームワークは**三層モジュラーアーキテクチャ**設計を採用しており、豊富な組み込みゲーム開発ツールとコンポーネントを提供し、開発者が高品質なゲームプロジェクトを迅速に構築できるよう支援します。

### 🎯 主な機能

- 🏗️ **三層アーキテクチャ** - Runtime、Plugins、Editorの明確な分離
- 🔧 **豊富なツールセット** - 組み込みの開発支援とエディタ拡張機能
- 📦 **オブジェクトプール管理** - 効率的なメモリ管理とオブジェクトの再利用
- 🎨 **拡張メソッドライブラリ** - Unityエンジンの広範な拡張機能
- 🛠️ **ユーティリティクラス** - 暗号化、圧縮、ネットワークなど
- 📱 **マルチプラットフォーム対応** - PC、モバイル、WebGLなど
- 🔥 **ホットフィックス対応** - 組み込みのHybridCLRホットフィックスソリューション
- 🎮 **ミニゲーム対応** - 複数のミニゲームプラットフォームへのワンクリック切り替え

### 📋 システム要件

- **Unityバージョン**: 2019.4以上
- **プラットフォーム対応**: Windows、macOS、Linux、iOS、Android、WebGL
- **.NETバージョン**: .NET Standard 2.0+

---

## 📂 アーキテクチャ

GameFrameXは、各モジュールが各自の責任を持ち、協調して動作する明確な三層アーキテクチャ設計を使用しています。

### 📦 Runtime モジュール

ゲームのランタイムに必要なすべての機能を提供するコアランタイムコードです。

```
Runtime/
├── Base/                          # フレームワークコア基盤
│   ├── DataStruct/               # データ構造体
│   │   └── TypeNamePair.cs       # 型名ペア
│   ├── EventPool/                # イベントプールシステム
│   │   ├── BaseEventArgs.cs      # ベースイベント引数
│   │   ├── EventPool.EventNode.cs # イベントノード
│   │   ├── EventPool.cs          # イベントプールコア
│   │   └── EventPoolMode.cs      # イベントプールモード
│   ├── Log/                      # ログシステム
│   │   ├── GameFrameworkLog.ILogHelper.cs # ログインターフェース
│   │   ├── GameFrameworkLog.cs   # ログコア
│   │   └── GameFrameworkLogLevel.cs # ログレベル
│   ├── ReferencePool/            # 参照プールシステム
│   │   ├── IReference.cs        # 参照インターフェース
│   │   ├── ReferencePool.ReferenceCollection.cs # 参照コレクション
│   │   ├── ReferencePool.cs      # 参照プールコア
│   │   └── ReferencePoolInfo.cs   # 参照プール情報
│   ├── TaskPool/                 # タスクプールシステム
│   │   ├── ITaskAgent.cs        # タスクエージェントインターフェース
│   │   ├── StartTaskStatus.cs    # タスクステータス
│   │   ├── TaskBase.cs          # タスクベース
│   │   ├── TaskInfo.cs          # タスク情報
│   │   ├── TaskPool.cs          # タスクプールコア
│   │   └── TaskStatus.cs        # タスクステータス
│   ├── Variable/                 # 変数システム
│   │   ├── GenericVariable.cs    # ジェネリック変数
│   │   └── Variable.cs          # 変数ベース
│   ├── Version/                  # バージョン管理
│   │   ├── Version.IVersionHelper.cs # バージョンインターフェース
│   │   └── Version.cs           # バージョンコア
│   ├── BaseComponent.cs          # ベースコンポーネント
│   ├── GameEntry.cs             # ゲームエントリー
│   ├── GameFrameworkComponent.cs # フレームワークコンポーネント
│   ├── GameFrameworkEntry.cs    # フレームワークエントリー
│   ├── GameFrameworkEventArgs.cs # フレームワークイベント引数
│   ├── GameFrameworkException.cs # フレームワーク例外
│   ├── GameFrameworkGuard.cs    # フレームワークガード
│   ├── GameFrameworkLinkedList.cs # 連結リスト
│   ├── GameFrameworkLinkedListRange.cs # 連結リスト範囲
│   ├── GameFrameworkModule.cs   # フレームワークモジュール
│   ├── GameFrameworkMonoSingleton.cs # Monoシングルトン
│   ├── GameFrameworkMultiDictionary.cs # マルチ値辞書
│   ├── GameFrameworkSerializer.cs # シリアライザー
│   ├── GameFrameworkSingleton.cs # シングルトンベース
│   ├── ObjectDontDestroyOnLoad.cs # シーン永続オブジェクト
│   └── ShutdownType.cs           # シャットダウンタイプ
├── Extension/                     # 拡張メソッドライブラリ
│   ├── Extension/                # 共通拡張
│   │   ├── BidirectionalDictionary.cs # 双方向辞書
│   │   ├── BinaryExtension.cs      # バイナリ拡張
│   │   ├── BufferExtension.cs      # バッファ拡張
│   │   ├── CollectionExtensions.cs # コレクション拡張
│   │   ├── DateTimeExtensions.cs   # DateTime拡張
│   │   ├── ObjectExtension.cs      # オブジェクト拡張
│   │   ├── SpanExtension.cs        # Span拡張
│   │   ├── StringExtensions.cs     # 文字列拡張
│   │   ├── ThreadLocalRandom.cs    # スレッドローカル乱数
│   │   └── TypeExtensions.cs       # 型拡張
│   ├── SequenceReader/            # シーケンスリーダー
│   │   ├── SequenceReader.cs       # シーケンスリーダーコア
│   │   └── SequenceReaderExtensions.cs # シーケンスリーダー拡張
│   ├── UnityEngage.GameObject/    # GameObject拡張
│   │   └── UnityEngage.GameObjectExtension.cs # GameObject拡張
│   └── UnityEngine/               # Unity型拡張
│       ├── Transform/             # Transform拡張
│       ├── Vector2/               # Vector2拡張
│       ├── Vector3/               # Vector3拡張
│       └── Vector4/               # Vector4拡張
├── Helper/                        # ヘルパークラス
│   ├── ApplicationHelper.cs      # アプリケーションヘルパー
│   ├── CameraHelper.cs          # カメラヘルパー
│   ├── DefaultCompressionHelper.cs # デフォルト圧縮ヘルパー
│   ├── DefaultLogHelper.cs       # デフォルトログヘルパー
│   ├── DefaultTextHelper.cs      # デフォルトテキストヘルパー
│   ├── DefaultVersionHelper.cs   # デフォルトバージョンヘルパー
│   ├── DistinctHelper.cs         # 重複除去ヘルパー
│   ├── DoTweenHelper.cs          # DoTweenアニメーションヘルパー
│   ├── FileHelper.cs            # ファイルヘルパー
│   ├── GameObjectHelper.cs      # GameObjectヘルパー
│   ├── Helper.cs                # ヘルパーベースクラス
│   ├── MathHelper.cs            # 数学ヘルパー
│   ├── NetworkHelper.cs         # ネットワークヘルパー
│   ├── NewtonsoftJsonHelper.cs  # Newtonsoft JSONヘルパー
│   ├── ObjectHelper.cs          # オブジェクトヘルパー
│   ├── PathHelper.cs            # パスヘルパー
│   ├── PositionHelper.cs        # 位置ヘルパー
│   ├── RandomHelper.cs          # 乱数ヘルパー
│   ├── TimerHelper/             # タイマーヘルパー
│   │   ├── TimerHelper.cs       # タイマーコア
│   │   ├── TimerHelper.Current.cs # 現在時刻
│   │   ├── TimerHelper.Day.cs   # 日計算
│   │   ├── TimerHelper.Difference.cs # 時間差
│   │   ├── TimerHelper.Month.cs  # 月計算
│   │   ├── TimerHelper.Range.cs  # 時間範囲
│   │   ├── TimerHelper.TimeOffset.cs # タイムオフセット
│   │   ├── TimerHelper.Timestamp.cs # タイムスタンプ
│   │   ├── TimerHelper.Week.cs   # 週計算
│   │   └── TimerHelper.Year.cs   # 年計算
│   ├── UnityRendererHelper.cs   # UnityRendererヘルパー
│   └── ZipHelper.cs             # ZIP圧縮ヘルパー
├── ObjectPool/                    # オブジェクトプールシステム
│   ├── IObjectPool.cs           # オブジェクトプールインターフェース
│   ├── ObjectBase.cs            # オブジェクトプールベース
│   └── ObjectPoolComponent.cs   # オブジェクトプールコンポーネント
├── Property/                      # プロパティシステム
│   └── BindableProperty.cs      # バインディング可能プロパティ
├── ReferencePool/                # 参照プールシステム
│   └── ReferencePoolComponent.cs # 参照プールコンポーネント
└── Utility/                       # ユーティリティクラス
    ├── Log.cs                     # ログユーティリティ
    ├── Utility.Assembly.cs        # アセンブリユーティリティ
    ├── Utility.Asset.Path.cs      # アセットパスユーティリティ
    ├── Utility.Compression/      # 圧縮
    │   ├── ICompressionHelper.cs  # 圧縮インターフェース
    │   └── Utility.Compression.cs # 圧縮コア
    ├── Utility.Const/            # 定数
    │   └── FileNameSuffix.cs      # ファイル名サフィックス
    ├── Utility.Converter.cs       # 型コンバーター
    ├── Utility.Encryption/        # 暗号化
    │   ├── Utility.Encryption.cs  # 暗号化コア
    │   ├── Aes.cs                # AES暗号化
    │   ├── Rsa.cs                # RSA暗号化
    │   └── Dsa.cs                # DSA暗号化
    ├── Utility.File.cs           # ファイル操作
    ├── Utility.Hash/             # ハッシュ計算
    │   ├── HMACSha256.cs         # HMAC SHA256
    │   ├── Md5.cs                # MD5
    │   ├── Sha1.cs               # SHA1
    │   ├── MurmurHash3.cs        # MurmurHash3
    │   └── XxHash.cs             # XxHash
    ├── Utility.IdGenerator.cs     # IDジェネレーター
    ├── Utility.Json/             # JSONシリアライズ
    │   ├── IJsonHelper.cs        # JSONインターフェース
    │   └── Utility.Json.cs       # JSONコア
    ├── Utility.Marshal.cs         # Marshalユーティリティ
    ├── Utility.Net.cs            # ネットワークユーティリティ
    ├── Utility.Object.cs          # オブジェクトユーティリティ
    ├── Utility.Path.cs           # パス処理
    ├── Utility.Random.cs         # 乱数
    ├── Utility.Text/             # テキスト処理
    │   ├── ITextHelper.cs        # テキストインターフェース
    │   └── Utility.Text.cs       # テキストコア
    ├── Utility.Verifier/          # 検証
    │   ├── Verifier.cs           # 検証コア
    │   ├── Crc32.cs             # CRC32
    │   └── Crc64.cs             # CRC64
    ├── Utility.cs                # ユーティリティエントリー
    └── XString.cs                # 高効率文字列
```

#### Runtime サブモジュール

| サブモジュール | 説明 | 主な機能 |
|------------|-------------|---------------|
| **Base** | フレームワークコア | コンポーネント管理、イベントプール、ログ、参照プール、タスクプール、変数システム、ライフサイクル管理、シングルトンパターン |
| **Extension** | 拡張ライブラリ | 共通拡張（文字列、コレクション、日時）、Unity型拡張（Transform、Vector）、シーケンスリーダー、GameObject拡張 |
| **Helper** | ヘルパークラス | アプリケーション、カメラ、ファイル、パス、数学、乱数、タイマー、ネットワーク、JSON、レンダリング、位置など |
| **ObjectPool** | オブジェクトプールシステム | オブジェクト再利用、メモリ最適化、パフォーマンス向上 |
| **Property** | プロパティシステム | バインディング可能プロパティ、データバインディング、MVVMサポート |
| **ReferencePool** | 参照プールシステム | 参照型管理、GC最適化 |
| **Utility** | ユーティリティクラス | 暗号化（AES/RSA/DSA）、圧縮、ハッシュ（MD5/SHA1/HMAC）、CRC、JSON、ファイル操作、ID生成、型変換、テキスト処理、ログ |

### 🔌 Plugins モジュール

ネイティブプラットフォームプラグインとサードパーティ依存関係です。

```
Plugins/
├── iOS/                          # iOSネイティブプラグイン
│   └── GameFrameX/
│       ├── GameFrameX.mm                    # コア機能
│       └── GameFrameXTrackingAuthorization.mm # 許可トラッキング
├── ICSharpCode.SharpZipLib.dll   # ZIP圧縮ライブラリ
├── Microsoft.NET.StringTools.dll  # 文字列ツール
├── System.Buffers.dll            # メモリバッファ
├── System.IO.Pipelines.dll       # IOパイプライン
├── System.Memory.dll            # メモリ管理
└── System.Runtime.CompilerServices.Unsafe.dll # ランタイムサポート
```

#### Plugins サブモジュール

| サブモジュール | 説明 | 依存関係 |
|------------|-------------|--------------|
| **iOS Plugin** | iOSネイティブ機能 | GameFrameX.mm |
| **Compression Library** | ZIPファイル圧縮/解凍 | SharpZipLib |
| **Memory Management** | 効率的なメモリ操作 | StringTools, Memory, Buffers |
| **Runtime Support** | .NETランタイム拡張 | CompilerServices.Unsafe |

### 🛠️ Editor モジュール

開発効率を向上させるためのエディタツールと拡張機能です。

```
Editor/
├── BuildHotfix/                  # ホットフィックスビルドツール
│   ├── BuildHotfixHelper.cs     # ビルドヘルパー
│   ├── HotFixAssemblyDefinitionHelper.cs # ホットフィックスアセンブリ
│   └── HotFixEditorCompilerHelper.cs # エディタコンパイラ
├── BuildProduct/                 # プロダクトビルドアシスタント
│   ├── BuildProductHelper.cs    # ビルドヘルパー
│   ├── BuildPostProcessHelper.cs # ポストビルド処理
│   ├── IBuilderPreHookHandler.cs # プレビルドフック
│   └── IBuilderPostHookHandler.cs # ポストビルドフック
├── BuildWebGLTools/             # WebGLビルドツール
│   └── BuildWebGLToolsWithHybridCLR.cs # HybridCLR WebGLビルド
├── Cropping/                     # 画像トリミングツール
│   └── CroppingWindow.cs        # トリミングウィンドウ
├── Inspector/                    # カスタムインスペクタパネル
│   ├── BaseComponentInspector.cs # ベースコンポーネントインスペクタ
│   ├── ObjectPoolComponentInspector.cs # オブジェクトプールインスペクタ
│   └── ReferencePoolComponentInspector.cs # 参照プールインスペクタ
├── InspectorLockShortcut/        # インスペクタロック
│   └── InspectorLockShortcut.cs # キーボードショートカットロック
├── MiniGame/                      # ミニゲームプラットフォーム対応（21プラットフォーム） ⭐
│   ├── MiniGameDefineSymbolHelper.cs # ベース定義シンボルマネージャー
│   ├── DomesticMiniGames/          # 国内ミニゲーム
│   │   ├── MiniGameDefineSymbolHelper.WeChat.cs # WeChat
│   │   ├── MiniGameDefineSymbolHelper.Alipay.cs # Alipay
│   │   ├── MiniGameDefineSymbolHelper.DouYin.cs # DouYin
│   │   ├── MiniGameDefineSymbolHelper.KuaiShou.cs # KuaiShou
│   │   ├── MiniGameDefineSymbolHelper.Baidu.cs # Baidu
│   │   ├── MiniGameDefineSymbolHelper.JingDong.cs # JingDong
│   │   ├── MiniGameDefineSymbolHelper.Meituan.cs # Meituan
│   │   ├── MiniGameDefineSymbolHelper.Taobao.cs # Taobao
│   │   └── MiniGameDefineSymbolHelper.Bilibili.cs # Bilibili
│   ├── InternationalMiniGames/     # 国際ミニゲーム
│   │   ├── MiniGameDefineSymbolHelper.CrazyGames.cs # CrazyGames
│   │   ├── MiniGameDefineSymbolHelper.Discord.cs # Discord
│   │   ├── MiniGameDefineSymbolHelper.Facebook.cs # Facebook
│   │   ├── MiniGameDefineSymbolHelper.GooglePlay.cs # Google Play
│   │   ├── MiniGameDefineSymbolHelper.Poki.cs # Poki
│   │   ├── MiniGameDefineSymbolHelper.TikTok.cs # TikTok
│   │   └── MiniGameDefineSymbolHelper.YouTube.cs # YouTube
│   ├── DeviceOEMs/                # デバイスOEMミニゲーム
│   │   ├── MiniGameDefineSymbolHelper.Huawei.cs # Huawei
│   │   ├── MiniGameDefineSymbolHelper.OPPO.cs # OPPO
│   │   ├── MiniGameDefineSymbolHelper.Vivo.cs # vivo
│   │   └── MiniGameDefineSymbolHelper.Xiaomi.cs # Xiaomi
│   └── GamePlatforms/             # ゲームプラットフォーム
│       └── MiniGameDefineSymbolHelper.TapTap.cs # TapTap
├── PackageManager/               # パッケージマネージャーウィンドウ
│   ├── PackageManagerWindow.cs   # パッケージマネージャーウィンドウ
│   └── PackagesManifest.cs     # パッケージマニフェスト
├── UpdatePackages/               # パッケージ更新ツール
│   └── UpdateAllPackageHelper.cs # 一括更新
├── Welcome/                      # ウェルカムウィンドウ
│   └── WelcomeWindow.cs         # ウェルカムインターフェース
└── Misc/                         # その他のツール
    ├── HelperInfo.cs            # ヘルパー情報
    ├── LogRedirection.cs        # ログリダイレクション
    ├── ScriptingDefineSymbols.cs # 定義シンボルマネージャー
    ├── Type.cs                  # 型ユーティリティ
    └── OpenFolder.cs            # フォルダを開く
```

#### Editor サブモジュール

| サブモジュール | 説明 | 主な機能 |
|------------|-------------|---------------|
| **BuildHotfix** | ホットフィックスビルド | HybridCLRホットフィックスアセンブリのビルド与管理 |
| **BuildProduct** | プロダクトビルド | ビルドプロセスの自動化、プレ/ポストフック |
| **BuildWebGLTools** | WebGLビルド | WebGLプラットフォーム固有のビルドツール |
| **Cropping** | 画像トリミング | ビジュアル画像トリミングツール |
| **Inspector** | カスタムインスペクタ | オブジェクトプール、参照プールのビジュアルモニタリング |
| **InspectorLockShortcut** | インスペクタロック | インスペクタパネルのロック用キーボードショートカット |
| **MiniGame** | ミニゲーム対応 | 21のミニゲームプラットフォームへのワンクリック切り替え（国内、国際、デバイスOEM、ゲームプラットフォームに分類） |
| **PackageManager** | パッケージ管理 | ビジュアルパッケージ管理インターフェース |
| **UpdatePackages** | パッケージ更新 | プロジェクト依存関係の一括更新 |
| **Welcome** | ウェルカムインターフェース | 新規ユーザーガイドとクイックアクセス |
| **Misc** | その他 | ログ、定義シンボル、型など |

---

## 🚀 クイックスタート

### インストール

#### 方法1: Unity Package Manager（推奨）

1. Unityエディタを開く
2. `Window` → `Package Manager` に移動
3. 左上の `+` ボタンをクリック
4. `Add package from git URL` を選択
5. 次を入力: `https://github.com/GameFrameX/com.gameframex.unity.git`

#### 方法2: 手動ダウンロード

1. 最新の[リリース](https://github.com/GameFrameX/com.gameframex.unity/releases)をダウンロード
2. プロジェクトの `Packages` ディレクトリに展開

### 基本的な使用方法

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // オブジェクトプールコンポーネントを取得
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();
        
        // 参照プールコンポーネントを取得
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();
        
        // 拡張メソッドを使用
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

---

## 💡 使用例

### Runtime 使用例

#### 🎯 オブジェクトプールシステム

```csharp
// オブジェクトプールコンポーネントを取得
var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

// オブジェクトプールを作成
objectPool.CreatePool<MyObject>("MyObjectPool", 10, 100);

// プールからオブジェクトを生成
var obj = objectPool.Spawn<MyObject>("MyObjectPool");

// オブジェクトをプールに返却
objectPool.Unspawn(obj);

// オブジェクトプールを破棄
objectPool.DestroyPool("MyObjectPool");
```

#### 📝 拡張メソッド

```csharp
// Transform拡張
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// Vector3拡張
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);

// GameObject拡張
gameObject.SetActiveOptimized(true);
gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
```

#### 🔐 ユーティリティクラス

```csharp
// ファイル操作
Utility.File.WriteAllBytes("path/to/file", data);
byte[] content = Utility.File.ReadAllBytes("path/to/file");

// AES暗号化/復号化
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// ハッシュ計算
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSONシリアライズ
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

#### 📡 イベントシステム

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

// イベントの購読解除
GameEntry.Event.Unsubscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);
```

### Editor ツール

#### 🎮 ミニゲームプラットフォーム対応

Unityメニューのミニゲームプラットフォーム間を簡単に切り替え:

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Domestic Mini Games(国内ミニゲーム)/
│   │   ├── Enable WeChat Mini Game
│   │   ├── Enable Alipay Mini Game
│   │   ├── Enable DouYin Mini Game
│   │   ├── Enable KuaiShou Mini Game
│   │   ├── Enable Baidu Mini Game
│   │   ├── Enable JingDong Mini Game
│   │   ├── Enable Meituan Mini Game
│   │   ├── Enable Taobao Mini Game
│   │   └── Enable Bilibili Mini Game
│   ├── International Mini Games(国際ミニゲーム)/
│   │   ├── Enable Discord Mini Game
│   │   ├── Enable YouTube Mini Game
│   │   ├── Enable Facebook Mini Game
│   │   ├── Enable Google Play Mini Game
│   │   ├── Enable TikTok Mini Game
│   │   ├── Enable CrazyGames Mini Game
│   │   └── Enable Poki Mini Game
│   ├── Device OEMs(デバイスOEM)/
│   │   ├── Enable Huawei Mini Game
│   │   ├── Enable OPPO Mini Game
│   │   ├── Enable Vivo Mini Game
│   │   └── Enable Xiaomi Mini Game
│   └── Game Platforms(ゲームプラットフォーム)/
│       └── Enable TapTap Mini Game
```

#### 🏗️ ビルドツール

```
GameFrameX/
├── Build Hotfix
├── Build Product
└── Build WebGL With HybridCLR
```

#### 📦 パッケージ管理

```
GameFrameX/
├── Package Manager
└── Update All Packages
```

---

## 🎯 プラットフォーム対応

### オペレーティングシステム

| プラットフォーム | ステータス | 対応バージョン |
|----------|--------|-------------------|
| Windows | ✅ 対応 | Unity 2019.4以上 |
| macOS | ✅ 対応 | Unity 2019.4以上 |
| Linux | ✅ 対応 | Unity 2019.4以上 |
| iOS | ✅ 対応 | Unity 2019.4以上 |
| Android | ✅ 対応 | Unity 2019.4以上 |
| WebGL | ✅ 対応 | Unity 2019.4以上 |

### ミニゲームプラットフォーム対応

GameFrameXはワンクリックでミニゲームプラットフォームに対応し、世界の各国の**21の主流ミニゲームプラットフォーム**をサポートしています:

#### 🇨🇳 国内ミニゲーム（9）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
|----------|---------------|--------|---------------|
| WeChat ミニゲーム | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 🇨🇳 中国 | 2000 |
| Alipay ミニゲーム | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 🇨🇳 中国 | 2400 |
| DouYin ミニゲーム | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 🇨🇳 中国 | 2100 |
| KuaiShou ミニゲーム | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 🇨🇳 中国 | 2200 |
| Baidu ミニゲーム | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 🇨🇳 中国 | 2300 |
| JD ミニゲーム | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 🇨🇳 中国 | 2500 |
| Taobao ミニプログラム | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 🇨🇳 中国 | 2600 |
| Meituan ミニゲーム | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 🇨🇳 中国 | 2800 |
| Bilibili ミニゲーム | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 🇨🇳 中国 | 2900 |

#### 🌍 国際ミニゲーム（7）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
|----------|---------------|--------|---------------|
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 🌍 グローバル | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 🌍 グローバル | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 🌍 グローバル | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 🌍 グローバル | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 🌍 グローバル | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 🌍 グローバル | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 🌍 グローバル | 3700 |

#### 📱 デバイスOEM（4）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
|----------|---------------|--------|---------------|
| Huawei ミニゲーム | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 🇨🇳 中国 | 3400 |
| OPPO ミニゲーム | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 🇨🇳 中国 | 3200 |
| vivo ミニゲーム | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 🇨🇳 中国 | 3100 |
| Xiaomi ミニゲーム | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 🇨🇳 中国 | 3300 |

#### 🎮 ゲームプラットフォーム（1）

| プラットフォーム | 定義シンボル | 地域 | メニュー優先度 |
|----------|---------------|--------|---------------|
| TapTap ミニゲーム | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 🇨🇳 中国 | 2700 |

#### 定義シンボルの詳細

- **統合定義**: `ENABLE_WEBGL_MINI_GAME` - すべてのミニゲームプラットフォームで共有
- **プラットフォーム定義**: 各プラットフォームの条件付きコンパイル用の独立定義
- **相互排他メカニズム**: 1つのミニゲームプラットフォームを有効にすると、他のプラットフォームが自動的に無効になります
- **メニュースパス**: `GameFrameX/Scripting Define Symbols/[カテゴリ]/Enable [Platform] Mini Game`

---

## 📚 ドキュメントとリソース

- 📖 **完全なドキュメント**: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🎯 **APIリファレンス**: [APIドキュメント](https://gameframex.doc.alianblank.com/api)
- 📝 **サンプルプロジェクト**: [サンプルレポジトリ](https://github.com/GameFrameX/Examples)
- 🎬 **ビデオチュートリアル**: [YouTubeチャンネル](https://youtube.com/gameframex)

---

## 🤝 コミュニティとサポート

- 💬 **QQグループ**: [467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)
- 🐛 **イシュートラッカー**: [GitHub Issues](https://github.com/GameFrameX/com.gameframex.unity/issues)
- 💡 **機能リクエスト**: [GitHub Discussions](https://github.com/GameFrameX/com.gameframex.unity/discussions)

---

## 🔄 変更履歴

### v1.3.6 (2025-05-28)
- 🐛 重複GUIDの問題を修正
- ✨ Meituan、Bilibiliミニゲームプラットフォーム対応を追加
- ✨ 追加の拡張メソッドを追加
- 📚 完全なモジュール構造を含むREADMEドキュメントを強化
- 🔧 オブジェクトプールのパフォーマンスを最適化
- 📚 ドキュメントを改善

完全な変更履歴: [CHANGELOG.md](CHANGELOG.md)

---

## 📄 ライセンス

このプロジェクトは**MIT License**と**Apache License 2.0**の二重ライセンスで配布されています。

完全なライセンステキスト: [LICENSE.md](LICENSE.md)

---

## 👨‍💻 作者

**Blank**

- 🌐 ウェブサイト: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🐙 GitHub: [@GameFrameX](https://github.com/GameFrameX)

---

<div align="center">

**このプロジェクトが役立ったら、⭐ をください！**

[⬆ トップへ戻る](#gameframex-unity-パッケージ)

</div>
