<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity 패키지

[![License](https://img.shields.io/badge/license-blue.svg)](LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

[![Discord](https://img.shields.io/badge/-5865F2?logo=discord&logoColor=white)](https://discord.gg/VDWUjWMDw9)
[![GitHub](https://img.shields.io/badge/-181717?logo=github&logoColor=white)](https://github.com/GameFrameX/gameframex)
[![Bilibili](https://img.shields.io/badge/-00A1D6?logo=bilibili&logoColor=white)](https://www.bilibili.com/video/BV1yrpeepEn7)
[![Gitee](https://img.shields.io/badge/-C71D23?logo=gitee&logoColor=white)](https://gitee.com/GameFrameX/gameframex)

**인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현**

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#quick-start) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

<br />

</div>

---

## 프로젝트 개요

GameFrameX는 독립 게임 개발자를 위해 설계된 모던 Unity 게임 프레임워크로, 게임 개발을 위한 완전한 엔드 투 엔드 솔루션을 제공합니다. 3계층 모듈러 아키텍처를 채택하고 풍부한 내장 도구와 컴포넌트를 갖추고 있어, 개발자가 더 빠르게 고품질 게임 프로젝트를 출시할 수 있도록 돕습니다.

`com.gameframex.unity`는 코어 패키지로, 런타임 커널(EventPool, ReferencePool, TaskPool, ObjectPool, Variable / Property 시스템, 확장 라이브러리, Helper, 유틸리티 클래스), 에디터 도구 체인(BuildProduct / BuildHotfix 파이프라인, WebGL + HybridCLR 도구, 미니게임 플랫폼 대응, Welcome 윈도우, Inspector 강화, Inspector 잠금 단축키, 크로핑, Scripting Define Symbol 관리자), iOS 네이티브 브리지 플러그인, 그리고 번들된 .NET 라이브러리(SharpZipLib, System.Memory / Buffers / Pipelines)를 포함합니다. 이는 다른 모든 GameFrameX Unity 패키지가 의존하는 기반 패키지입니다.

### 기능

- 3계층 아키텍처: Runtime / Plugins / Editor의 명확한 책임 분리.
- 풍부한 런타임 툴킷: EventPool, ReferencePool, TaskPool, ObjectPool, Variable / Property, 확장 라이브러리, Helper 클래스.
- 에디터 도구 체인: BuildProduct / BuildHotfix 파이프라인, BuildWebGLTools + HybridCLR, 21개 미니게임 플랫폼 대응, Welcome 윈도우, Inspector 강화.
- 유틸리티 계층: 암호화(AES / RSA / DSA), 압축(ZIP), 해시(MD5 / SHA1 / HMAC / CRC), JSON, 파일 / 경로 / 네트워크 헬퍼, ID 생성기, 텍스트 처리.
- 핫픽스 지원: 표준 빌드 플로우에 통합된 HybridCLR 핫픽스 빌드 파이프라인.
- 멀티 플랫폼: Windows, macOS, Linux, iOS, Android, WebGL, 그리고 전 세계 21개 미니게임 플랫폼.

### 시스템 요구사항

- Unity: 2019.4 이상
- 플랫폼: Windows, macOS, Linux, iOS, Android, WebGL
- .NET: .NET Standard 2.0+

---

## 빠른 시작

### 설치

다음 방법 중 하나를 선택하세요:

1. Unity 프로젝트의 `Packages/manifest.json`을 편집하고 `scopedRegistries` 섹션을 추가합니다:
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

   `scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex.unity`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

2. `manifest.json`의 dependencies에 추가:
   ```json
   {
     "com.gameframex.unity": "https://github.com/GameFrameX/com.gameframex.unity.git"
   }
   ```
3. Unity의 `Package Manager`에서 `Git URL`을 사용하여 추가합니다, 주소: `https://github.com/GameFrameX/com.gameframex.unity.git`
4. 저장소를 다운로드하여 Unity 프로젝트의 `Packages` 디렉터리에 배치하면 자동으로 로드됩니다.

### 기본 사용법

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 프레임워크 엔트리에서 컴포넌트 가져오기
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

        // 레퍼런스 풀 컴포넌트 가져오기
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();

        // 엔진 타입의 확장 메서드 사용
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

### 호스트 프레임워크 통합 (Headless 3행 계약)

GameFrameX는 순수 C# 커널로 다른 Unity 프레임워크나 커스텀 호스트에 임베드할 수 있습니다: 런타임에 `[GameFrameX]` 노드를 생성하지 않고, GameFrameX 비즈니스 컴포넌트를 부착하지 않으며, 등록된 모듈은 호스트가 구동합니다.

```csharp
using GameFrameX.Runtime;

// 1. 시작: 첫 번째 씬 로드 전에 한 번만 호출. 반복 시작은 단일 인스턴스 가드에 의해 거부됨
GameFrameworkStartup.Headless();

// 2. 구동: 호스트 메인 루프에서 매 프레임 전달
//    (또는 UnityGameFrameworkDriver 컴포넌트를 부착해 두면 대신 구동해 줌)
GameFrameworkEntry.Update(deltaTime, unscaledDeltaTime);

// 3. 종료: 호스트 종료 시 반드시 호출. 호출하지 않으면 정적 모듈이 누수됨
//    (UnityGameFrameworkDriver는 Destroy 시 자동으로 호출)
GameFrameworkEntry.Shutdown();
```

로그 라우팅 - `ILogHelper`를 구현해 프레임워크 로그를 호스트 자체 로그 시스템에 파이프합니다:

```csharp
public sealed class HostLogBridge : GameFrameworkLog.ILogHelper
{
    public void Log(GameFrameworkLogLevel level, object message)
    {
        MyLogger.Write(level, message.ToString()); // 호스트 자체 로그 시스템
    }
}

GameFrameworkLog.SetLogHelper(new HostLogBridge());
```

제약과 동작:

- 단일 인스턴스: 실행마다 GameFrameX 인스턴스는 하나뿐. 세션 실행 중 `Headless()`를 다시 호출하면 예외가 던져지고, `Shutdown()` 이후에는 재진입 가능.
- 3모드 상호 배타: Headless 계약이 활성화되면 씬의 GFX 수동 진입과 자동 패키지 모드 Host는 모두 거부되고 진단이 출력됨. Headless 모드에서는 GameFrameX 컴포넌트 부착 금지.
- 저메모리: 호스트가 자체 저메모리 / 일시 정지 이벤트를 받으면 `GameFrameworkEntry.NotifyLowMemory()`로 전달. Unity 경로(BaseComponent, UnityGameFrameworkDriver)는 자동으로 배선되어 동작도 동일.

### 모듈 Options 시작 구성

각 모듈은 `XxxOptions` 시작 구성을 제공하며 (필드 기본값은 기존 Inspector 시리얼라이즈 기본값과 일치), 모듈이 처음 인스턴스화되기 전에 `GameFrameXOptions.Configure`로 한 번 주입됩니다. 컴포넌트의 시리얼라이즈 파라미터 필드는 제거되어 편집 시 구성 진입점이 코드로 통일되었습니다.

```csharp
using GameFrameX.Runtime;

// 3모드 공통 주입 시점: Manual / AutoPackage은 첫 씬 로드 전(BeforeSceneLoad)
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
static void ConfigureFramework()
{
    // 차분 필드만 쓰고 나머지는 패키지 기본값 사용
    GameFrameXOptions.Configure(new DownloadOptions { Timeout = 60f });
}

// Headless: 호스트는 GameFrameworkStartup.Headless() 호출 직전에 Configure 호출
// (자동 패키지 모드 Host는 AfterSceneLoad에서 생성되므로 BeforeSceneLoad Configure보다 늦음)
```

- 우선순위: `Configure`로 주입된 값 > Options 필드 기본값. 모듈 첫 인스턴스화 시 한 번만 소비됨.
- 타이밍: 시작기 전량 인스턴스화 완료 후 `Configure`를 호출하면 `GameFrameworkException`이 던져짐 - 타이밍이 틀려도 조용히 실패하지 않음.
- 분류: 필드는 기본적으로 실시간형 (런타임에 모듈 공개 API로 변경 가능). 시작기형 필드(에이전트 수, Helper 타입 등)는 `[GameFrameXStartupOnly]`로 표시되며 런타임 변경은 Warning과 함께 거부됨.
- 미소비 진단: `Reset` / `Shutdown` 시 "등록되었지만 어떤 모듈에서도 소비되지 않은" Options에 대해 Warning이 출력됨 (보통 미설치 패키지나 구조적으로 비활성화된 컴포넌트가 원인).

팩토리 클로저로 Helper 구현을 오버라이드 (Custom 참조가 우선되며 기존 Inspector의 타입명 드롭다운을 대체. UI 백엔드 패키지 FairyGUI / UGUI는 백엔드 레지스트리를 통해 기본 구현을 자동 등록):

```csharp
GameFrameXOptions.Configure(new UIOptions
{
    UIFormHelperFactory = () => new MyUIFormHelper(),
});
```

라이브 뷰: 컴포넌트는 얇은 진입점(순수 포워딩)으로 남고 Inspector는 파라미터를 갖지 않음. Play 중에는 모듈 디버그 뷰에서 유효 구성을 확인할 수 있으며 런타임 조정은 항상 `GetModule<T>()` / `GameApp.X` 모듈 API를 통해 이루어짐.

---

## 사용 예시

### 오브젝트 풀 시스템

```csharp
// 오브젝트 풀 컴포넌트 가져오기
var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();

// 오브젝트 풀 생성
objectPool.CreatePool<MyObject>("MyObjectPool", 10, 100);

// 풀에서 오브젝트 생성
var obj = objectPool.Spawn<MyObject>("MyObjectPool");

// 오브젝트를 풀로 반환
objectPool.Unspawn(obj);

// 오브젝트 풀 파괴
objectPool.DestroyPool("MyObjectPool");
```

### 확장 메서드

```csharp
// Transform 확장
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// Vector3 확장
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);

// GameObject 확장
gameObject.SetActiveOptimized(true);
gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
```

### 유틸리티 클래스

```csharp
// 파일 작업
FileHelper.WriteAllBytes("path/to/file", data);
byte[] content = FileHelper.ReadAllBytes("path/to/file");
string size = FileHelper.GetBytesSize(1024 * 1024); // "1MB"

// AES 암호화 / 복호화
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// 해시 계산
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSON 직렬화
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

### 이벤트 시스템

```csharp
// 이벤트 인자 정의
public class PlayerDeadEventArgs : BaseEventArgs
{
    public int PlayerId { get; set; }
    public float Damage { get; set; }
}

// 이벤트 구독
GameEntry.Event.Subscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);

// 이벤트 발생
GameEntry.Event.Fire(this, PlayerDeadEventArgs.Create(playerId, damage));

// 이벤트 구독 취소
GameEntry.Event.Unsubscribe(PlayerDeadEventArgs.EventId, OnPlayerDead);
```

### 에디터 도구

#### 미니게임 플랫폼 대응

Unity 메뉴에서 미니게임 플랫폼 간 빠른 전환:

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Domestic Mini Games(국내 미니게임)/
│   │   ├── Enable WeChat Mini Game (WeChat 미니게임 활성화)
│   │   ├── Enable Alipay Mini Game (Alipay 미니게임 활성화)
│   │   ├── Enable DouYin Mini Game (DouYin 미니게임 활성화)
│   │   ├── Enable KuaiShou Mini Game (KuaiShou 미니게임 활성화)
│   │   ├── Enable Baidu Mini Game (Baidu 미니게임 활성화)
│   │   ├── Enable JingDong Mini Game (JD 미니게임 활성화)
│   │   ├── Enable Meituan Mini Game (Meituan 미니게임 활성화)
│   │   ├── Enable Taobao Mini Game (Taobao 미니게임 활성화)
│   │   └── Enable Bilibili Mini Game (Bilibili 미니게임 활성화)
│   ├── International Mini Games(국제 미니게임)/
│   │   ├── Enable Discord Mini Game
│   │   ├── Enable YouTube Mini Game
│   │   ├── Enable Facebook Mini Game
│   │   ├── Enable Google Play Mini Game
│   │   ├── Enable TikTok Mini Game
│   │   ├── Enable CrazyGames Mini Game
│   │   └── Enable Poki Mini Game
│   ├── Device OEMs(기기 제조사)/
│   │   ├── Enable Huawei Mini Game (Huawei 미니게임 활성화)
│   │   ├── Enable OPPO Mini Game (OPPO 미니게임 활성화)
│   │   ├── Enable Vivo Mini Game (vivo 미니게임 활성화)
│   │   └── Enable Xiaomi Mini Game (Xiaomi 미니게임 활성화)
│   └── Game Platforms(게임 플랫폼)/
│       └── Enable TapTap Mini Game (TapTap 미니게임 활성화)
```

#### 빌드 도구

```
GameFrameX/
├── Build Hotfix (핫픽스 빌드)
├── Build Product (프로덕트 빌드)
└── Build WebGL With HybridCLR (WebGL 핫픽스 빌드)
```

---

## 아키텍처

GameFrameX는 명확한 3계층 아키텍처를 채택해 각 모듈이 자기 책임을 갖고 함께 동작합니다.

### Runtime

런타임의 핵심 코드로, 게임 런타임에 필요한 모든 기능을 제공합니다.

```
Runtime/
├── Base/                          프레임워크의 핵심 토대
│   ├── DataStruct/                데이터 구조
│   ├── EventPool/                 이벤트 풀
│   ├── Log/                       로깅 시스템
│   ├── ReferencePool/             레퍼런스 풀
│   ├── TaskPool/                  태스크 풀
│   ├── Variable/                  변수 시스템
│   ├── Version/                   버전 관리
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
├── Extension/                     확장 메서드 라이브러리
│   ├── Extension/                 공용 확장
│   ├── SequenceReader/            Sequence Reader
│   ├── UnityEngine.GameObject/    GameObject 확장
│   └── UnityEngine/               Unity 타입 확장 (Transform, Vector2/3/4)
├── Helper/                        Helper 클래스 (Application, Camera, File, Path,
│                                  Math, Random, Timer, Network, JSON, Renderer,
│                                  Zip, DoTween, 기간 분할 Timer 등)
├── ObjectPool/                    오브젝트 풀
├── Property/                      프로퍼티 시스템 (BindableProperty)
├── ReferencePool/                 레퍼런스 풀
└── Utility/                       유틸리티 (Encryption, Compression, Hash, JSON,
                                   Marshal, Text, Verifier, Const, IdGenerator)
```

### Plugins

네이티브 플랫폼 플러그인과 번들된 서드파티 .NET 라이브러리.

```
Plugins/
├── iOS/                           iOS 네이티브 브리지 (GameFrameX.mm, TrackingAuthorization)
├── ICSharpCode.SharpZipLib.dll    ZIP 압축 라이브러리
├── Microsoft.NET.StringTools.dll  문자열 도구
├── System.Buffers.dll             메모리 버퍼
├── System.IO.Pipelines.dll        IO 파이프라인
├── System.Memory.dll              메모리 관리
└── System.Runtime.CompilerServices.Unsafe.dll   런타임 지원
```

### Editor

개발 효율을 높이는 에디터 도구와 확장.

```
Editor/
├── BuildHotfix/                   핫픽스 빌드 도구 (HybridCLR)
├── BuildProduct/                  프로덕트 빌드 (Pre / Post Hook 포함)
├── BuildWebGLTools/               WebGL 빌드 최적화 (HybridCLR)
├── Cropping/                      이미지 크로핑 도구
├── Inspector/                     Base / ObjectPool / ReferencePool 커스텀 Inspector
├── InspectorLockShortcut/         Inspector 패널 잠금 단축키
├── MiniGame/                      미니게임 대응 (21 플랫폼)
│   ├── DomesticMiniGames/         WeChat, Alipay, DouYin, KuaiShou, Baidu, JD, Meituan, Taobao, Bilibili
│   ├── InternationalMiniGames/    Discord, YouTube, Facebook, Google Play, TikTok, CrazyGames, Poki
│   ├── DeviceOEMs/                Huawei, OPPO, Vivo, Xiaomi
│   └── GamePlatforms/             TapTap
├── Welcome/                       Welcome 윈도우
└── Misc/                          로그 리다이렉션, Scripting Define Symbols, 타입 유틸리티, 폴더 열기
```

---

## 플랫폼 지원

### 운영 체제

| 플랫폼 | 상태 | 지원 버전 |
| ------ | ---- | --------- |
| Windows | 지원 | Unity 2019.4+ |
| macOS | 지원 | Unity 2019.4+ |
| Linux | 지원 | Unity 2019.4+ |
| iOS | 지원 | Unity 2019.4+ |
| Android | 지원 | Unity 2019.4+ |
| WebGL | 지원 | Unity 2019.4+ |

### 미니게임 플랫폼 대응

GameFrameX는 원클릭 미니게임 플랫폼 전환을 제공하며, 전 세계 21개 주요 미니게임 플랫폼을 커버합니다.

#### 국내 미니게임 (9)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
| ------ | --------- | ---- | ------------- |
| WeChat 미니게임 | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 중국 | 2000 |
| Alipay 미니게임 | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 중국 | 2400 |
| DouYin 미니게임 | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 중국 | 2100 |
| KuaiShou 미니게임 | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 중국 | 2200 |
| Baidu 미니게임 | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 중국 | 2300 |
| JD 미니게임 | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 중국 | 2500 |
| Taobao 미니프로그램 | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 중국 | 2600 |
| Meituan 미니게임 | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 중국 | 2800 |
| Bilibili 미니게임 | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 중국 | 2900 |

#### 국제 미니게임 (7)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
| ------ | --------- | ---- | ------------- |
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 글로벌 | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 글로벌 | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 글로벌 | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 글로벌 | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 글로벌 | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 글로벌 | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 글로벌 | 3700 |

#### 기기 제조사 (4)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
| ------ | --------- | ---- | ------------- |
| Huawei 미니게임 | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 중국 | 3400 |
| OPPO 미니게임 | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 중국 | 3200 |
| Vivo 미니게임 | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 중국 | 3100 |
| Xiaomi 미니게임 | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 중국 | 3300 |

#### 게임 플랫폼 (1)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
| ------ | --------- | ---- | ------------- |
| TapTap 미니게임 | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 중국 | 2700 |

#### 정의 심볼 설명

- 통합 정의 심볼: `ENABLE_WEBGL_MINI_GAME`은 모든 미니게임 플랫폼이 공유합니다.
- 플랫폼 정의 심볼: 각 플랫폼마다 독립적인 정의 심볼을 가지며 조건부 컴파일에 사용됩니다.
- 상호 배타 메커니즘: 한 미니게임 플랫폼을 활성화하면 다른 플랫폼은 자동으로 비활성화됩니다.
- 메뉴 위치: `GameFrameX/Scripting Define Symbols/[카테고리]/Enable [플랫폼 이름] Mini Game`.

---

## 의존성

`package.json`에 선언됨:

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

개발 도구: `semantic-release`, `@semantic-release/changelog`, `@semantic-release/git`.

---

## 문서 및 자료

- 문서: https://gameframex.doc.alianblank.com
- API 참조: https://gameframex.doc.alianblank.com/api
- 예제 프로젝트: https://github.com/GameFrameX/Examples
- 영상 튜토리얼: https://www.youtube.com/channel/UCD9QhSFJ5xZkn5NTSV-DVAw

---

## 커뮤니티 및 지원

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

## 변경 로그

전체 릴리스 기록은 [CHANGELOG.md](CHANGELOG.md)를 참조하세요.

---

## 라이선스

전체 라이선스 전문은 [LICENSE.md](LICENSE.md)를 참조하세요.

---

<div align="center">

이 프로젝트가 도움이 되었다면 Star를 부탁드립니다.

[맨 위로](#gameframex-unity-패키지)

</div>
