<div align="center">

![GameFrameX Logo](https://download.alianblank.com/gameframex/gameframex_logo_320.png)

# GameFrameX Unity 패키지

[![Version](https://img.shields.io/badge/version-1.3.6-blue.svg)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity](https://img.shields.io/badge/Unity-2019.4+-green.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT+Apache%202.0-orange.svg)](LICENSE.md)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

**인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현**

[📖 문서](https://gameframex.doc.alianblank.com) • [🚀 빠른 시작](#🚀-빠른-시작) • [💬 QQ 그룹: 467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)

---

🌐 **언어**: [English](./README.md) | [繁體中文](./README.zh-TW.md) | [简体中文](./README.zh-CN.md) | [日本語](./README.ja.md) | [한국어](./README.ko.md)

---

</div>

## 📑 목차

- [🏗️ 프로젝트 개요](#🏗️-프로젝트-개요)
- [📂 아키텍처](#📂-아키텍처)
  - [Runtime 모듈 상세](#📦-runtime-모듈)
  - [Plugins 모듈 상세](#🔌-plugins-모듈)
  - [Editor 모듈 상세](#🛠️-editor-모듈)
- [🚀 빠른 시작](#🚀-빠른-시작)
- [💡 사용 예시](#💡-사용-예시)
  - [Runtime 사용 예시](#runtime-사용-예시)
  - [Editor 도구](#editor-도구)
- [🎯 플랫폼 지원](#🎯-플랫폼-지원)
- [📚 문서 및 자료](#📚-문서-및-자료)
- [🤝 커뮤니티 및 지원](#🤝-커뮤니티-및-지원)
- [� 변경 로그](#🔄-변경-로그)
- [�📄 라이선스](#📄-라이선스)
- [👨‍💻 작성자](#👨‍💻-작성자)

---

## 🏗️ 프로젝트 개요

GameFrameX는 인디펜던트 게임 개발자를 위해 설계된 최신 Unity 게임 프레임워크로, 게임 개발을 위한 완전한 엔드투엔드 솔루션을 제공합니다. 이 프레임워크는 **3층 모듈식 아키텍처** 설계를 따르며, 풍부한 내장 게임 개발 도구와 컴포넌트를 제공하여 개발자가 고품질 게임 프로젝트를 빠르게 구축할 수 있도록 지원합니다.

### 🎯 주요 기능

- 🏗️ **3층 아키텍처** - Runtime, Plugins, Editor의 명확한 분리
- 🔧 **풍부한 도구 세트** - 내장 개발 지원 및 에디터 확장 기능
- 📦 **오브젝트 풀 관리** - 효율적인 메모리 관리 및 오브젝트 재사용
- 🎨 **확장 메서드 라이브러리** - 광범위한 Unity 엔진 확장 기능
- 🛠️ **유틸리티 클래스** - 암호화, 압축, 네트워킹 등
- 📱 **멀티 플랫폼 지원** - PC, 모바일, WebGL 등
- 🔥 **핫픽스 지원** - 내장 HybridCLR 핫픽스 솔루션
- 🎮 **미니게임 지원** - 여러 미니게임 플랫폼 간 원클릭 전환

### 📋 시스템 요구사항

- **Unity 버전**: 2019.4 이상
- **플랫폼 지원**: Windows, macOS, Linux, iOS, Android, WebGL
- **.NET 버전**: .NET Standard 2.0+

---

## 📂 아키텍처

GameFrameX는 각 모듈이 각각의 역할을を持ち 함께 작동하는 명확한 3층 모듈식 아키텍처 설계를 사용합니다.

### 📦 Runtime 모듈

게임 런타임에 필요한 모든 기능을 제공하는 핵심 런타임 코드입니다.

```
Runtime/
├── Base/                          # 프레임워크 핵심 기반
│   ├── DataStruct/               # 데이터 구조체
│   │   └── TypeNamePair.cs       # 타입명 쌍
│   ├── EventPool/                # 이벤트 풀 시스템
│   │   ├── BaseEventArgs.cs      # 기본 이벤트 인자
│   │   ├── EventPool.EventNode.cs # 이벤트 노드
│   │   ├── EventPool.cs          # 이벤트 풀 코어
│   │   └── EventPoolMode.cs      # 이벤트 풀 모드
│   ├── Log/                      # 로깅 시스템
│   │   ├── GameFrameworkLog.ILogHelper.cs # 로그 인터페이스
│   │   ├── GameFrameworkLog.cs   # 로그 코어
│   │   └── GameFrameworkLogLevel.cs # 로그 레벨
│   ├── ReferencePool/            # 참조 풀 시스템
│   │   ├── IReference.cs        # 참조 인터페이스
│   │   ├── ReferencePool.ReferenceCollection.cs # 참조 컬렉션
│   │   ├── ReferencePool.cs      # 참조 풀 코어
│   │   └── ReferencePoolInfo.cs   # 참조 풀 정보
│   ├── TaskPool/                 # 태스크 풀 시스템
│   │   ├── ITaskAgent.cs        # 태스크 에이전트 인터페이스
│   │   ├── StartTaskStatus.cs    # 태스크 상태
│   │   ├── TaskBase.cs          # 태스크 베이스
│   │   ├── TaskInfo.cs          # 태스크 정보
│   │   ├── TaskPool.cs          # 태스크 풀 코어
│   │   └── TaskStatus.cs        # 태스크 상태
│   ├── Variable/                 # 변수 시스템
│   │   ├── GenericVariable.cs    # 제네릭 변수
│   │   └── Variable.cs          # 변수 베이스
│   ├── Version/                  # 버전 관리
│   │   ├── Version.IVersionHelper.cs # 버전 인터페이스
│   │   └── Version.cs           # 버전 코어
│   ├── BaseComponent.cs          # 기본 컴포넌트
│   ├── GameEntry.cs             # 게임 엔트리
│   ├── GameFrameworkComponent.cs # 프레임워크 컴포넌트
│   ├── GameFrameworkEntry.cs    # 프레임워크 엔트리
│   ├── GameFrameworkEventArgs.cs # 프레임워크 이벤트 인자
│   ├── GameFrameworkException.cs # 프레임워크 예외
│   ├── GameFrameworkGuard.cs    # 프레임워크 가드
│   ├── GameFrameworkLinkedList.cs # 연결 리스트
│   ├── GameFrameworkLinkedListRange.cs # 연결 리스트 범위
│   ├── GameFrameworkModule.cs   # 프레임워크 모듈
│   ├── GameFrameworkMonoSingleton.cs # Mono 싱글톤
│   ├── GameFrameworkMultiDictionary.cs # 멀티값 딕셔너리
│   ├── GameFrameworkSerializer.cs # 직렬화
│   ├── GameFrameworkSingleton.cs # 싱글톤 베이스
│   ├── ObjectDontDestroyOnLoad.cs # 씬 영속 오브젝트
│   └── ShutdownType.cs           # 종료 유형
├── Extension/                     # 확장 메서드 라이브러리
│   ├── Extension/                # 공통 확장
│   │   ├── BidirectionalDictionary.cs # 양방향 딕셔너리
│   │   ├── BinaryExtension.cs      # 바이너리 확장
│   │   ├── BufferExtension.cs      # 버퍼 확장
│   │   ├── CollectionExtensions.cs # 컬렉션 확장
│   │   ├── DateTimeExtensions.cs   # DateTime 확장
│   │   ├── ObjectExtension.cs      # 오브젝트 확장
│   │   ├── SpanExtension.cs        # Span 확장
│   │   ├── StringExtensions.cs     # 문자열 확장
│   │   ├── ThreadLocalRandom.cs    # 스레드 로컬 랜덤
│   │   └── TypeExtensions.cs       # 타입 확장
│   ├── SequenceReader/            # 시퀀스 리더
│   │   ├── SequenceReader.cs       # 시퀀스 리더 코어
│   │   └── SequenceReaderExtensions.cs # 시퀀스 리더 확장
│   ├── UnityEngage.GameObject/    # GameObject 확장
│   │   └── UnityEngage.GameObjectExtension.cs # GameObject 확장
│   └── UnityEngine/               # Unity 타입 확장
│       ├── Transform/             # Transform 확장
│       ├── Vector2/               # Vector2 확장
│       ├── Vector3/               # Vector3 확장
│       └── Vector4/               # Vector4 확장
├── Helper/                        # 헬퍼 클래스
│   ├── ApplicationHelper.cs      # 애플리케이션 헬퍼
│   ├── CameraHelper.cs          # 카메라 헬퍼
│   ├── DefaultCompressionHelper.cs # 기본 압축 헬퍼
│   ├── DefaultLogHelper.cs       # 기본 로그 헬퍼
│   ├── DefaultTextHelper.cs      # 기본 텍스트 헬퍼
│   ├── DefaultVersionHelper.cs   # 기본 버전 헬퍼
│   ├── DistinctHelper.cs         # 고유값 헬퍼
│   ├── DoTweenHelper.cs          # DoTween 애니메이션 헬퍼
│   ├── FileHelper.cs            # 파일 헬퍼
│   ├── GameObjectHelper.cs      # GameObject 헬퍼
│   ├── Helper.cs                # 헬퍼 베이스 클래스
│   ├── MathHelper.cs            # 수학 헬퍼
│   ├── NetworkHelper.cs         # 네트워크 헬퍼
│   ├── NewtonsoftJsonHelper.cs  # Newtonsoft JSON 헬퍼
│   ├── ObjectHelper.cs          # 오브젝트 헬퍼
│   ├── PathHelper.cs            # 경로 헬퍼
│   ├── PositionHelper.cs        # 위치 헬퍼
│   ├── RandomHelper.cs          # 랜덤 헬퍼
│   ├── TimerHelper/             # 타이머 헬퍼
│   │   ├── TimerHelper.cs       # 타이머 코어
│   │   ├── TimerHelper.Current.cs # 현재 시간
│   │   ├── TimerHelper.Day.cs   # 일 계산
│   │   ├── TimerHelper.Difference.cs # 시간 차이
│   │   ├── TimerHelper.Month.cs  # 월 계산
│   │   ├── TimerHelper.Range.cs  # 시간 범위
│   │   ├── TimerHelper.TimeOffset.cs # 시간 오프셋
│   │   ├── TimerHelper.Timestamp.cs # 타임스탬프
│   │   ├── TimerHelper.Week.cs   # 주 계산
│   │   └── TimerHelper.Year.cs   # 년 계산
│   ├── UnityRendererHelper.cs   # UnityRenderer 헬퍼
│   └── ZipHelper.cs             # ZIP 압축 헬퍼
├── ObjectPool/                    # 오브젝트 풀 시스템
│   ├── IObjectPool.cs           # 오브젝트 풀 인터페이스
│   ├── ObjectBase.cs            # 오브젝트 풀 베이스
│   └── ObjectPoolComponent.cs   # 오브젝트 풀 컴포넌트
├── Property/                      # 프로퍼티 시스템
│   └── BindableProperty.cs      # 바인딩 가능 프로퍼티
├── ReferencePool/                # 참조 풀 시스템
│   └── ReferencePoolComponent.cs # 참조 풀 컴포넌트
└── Utility/                       # 유틸리티 클래스
    ├── Log.cs                     # 로그 유틸리티
    ├── Utility.Assembly.cs        # 어셈블리 유틸리티
    ├── Utility.Asset.Path.cs      # 에셋 경로 유틸리티
    ├── Utility.Compression/      # 압축
    │   ├── ICompressionHelper.cs  # 압축 인터페이스
    │   └── Utility.Compression.cs # 압축 코어
    ├── Utility.Const/            # 상수
    │   └── FileNameSuffix.cs      # 파일명 접미사
    ├── Utility.Converter.cs       # 타입 변환기
    ├── Utility.Encryption/        # 암호화
    │   ├── Utility.Encryption.cs  # 암호화 코어
    │   ├── Aes.cs                # AES 암호화
    │   ├── Rsa.cs                # RSA 암호화
    │   └── Dsa.cs                # DSA 암호화
    ├── Utility.File.cs           # 파일 작업
    ├── Utility.Hash/             # 해시 계산
    │   ├── HMACSha256.cs         # HMAC SHA256
    │   ├── Md5.cs                # MD5
    │   ├── Sha1.cs               # SHA1
    │   ├── MurmurHash3.cs        # MurmurHash3
    │   └── XxHash.cs             # XxHash
    ├── Utility.IdGenerator.cs     # ID 생성기
    ├── Utility.Json/             # JSON 직렬화
    │   ├── IJsonHelper.cs        # JSON 인터페이스
    │   └── Utility.Json.cs       # JSON 코어
    ├── Utility.Marshal.cs         # Marshal 유틸리티
    ├── Utility.Net.cs            # 네트워크 유틸리티
    ├── Utility.Object.cs          # 오브젝트 유틸리티
    ├── Utility.Path.cs           # 경로 처리
    ├── Utility.Random.cs         # 랜덤 번호
    ├── Utility.Text/             # 텍스트 처리
    │   ├── ITextHelper.cs        # 텍스트 인터페이스
    │   └── Utility.Text.cs       # 텍스트 코어
    ├── Utility.Verifier/          # 검증
    │   ├── Verifier.cs           # 검증 코어
    │   ├── Crc32.cs             # CRC32
    │   └── Crc64.cs             # CRC64
    ├── Utility.cs                # 유틸리티 엔트리
    └── XString.cs                # 효율적인 문자열
```

#### Runtime 서브모듈

| 서브모듈 | 설명 | 주요 기능 |
|------------|-------------|---------------|
| **Base** | 프레임워크 코어 | 컴포넌트 관리, 이벤트 풀, 로깅, 참조 풀, 태스크 풀, 변수 시스템, 라이프사이클 관리, 싱글톤 패턴 |
| **Extension** | 확장 라이브러리 | 공통 확장(문자열, 컬렉션, DateTime), Unity 타입 확장(Transform, Vector), 시퀀스 리더, GameObject 확장 |
| **Helper** | 헬퍼 클래스 | 애플리케이션, 카메라, 파일, 경로, 수학, 랜덤, 타이머, 네트워크, JSON, 렌더링, 위치 등 |
| **ObjectPool** | 오브젝트 풀 시스템 | 오브젝트 재사용, 메모리 최적화, 성능 향상 |
| **Property** | 프로퍼티 시스템 | 바인딩 가능 프로퍼티, 데이터 바인딩, MVVM 지원 |
| **ReferencePool** | 참조 풀 시스템 | 참조 타입 관리, GC 최적화 |
| **Utility** | 유틸리티 클래스 | 암호화(AES/RSA/DSA), 압축, 해시(MD5/SHA1/HMAC), CRC, JSON, 파일 작업, ID 생성, 타입 변환, 텍스트 처리, 로깅 |

### 🔌 Plugins 모듈

네이티브 플랫폼 플러그인 및 서드파티 의존성입니다.

```
Plugins/
├── iOS/                          # iOS 네이티브 플러그인
│   └── GameFrameX/
│       ├── GameFrameX.mm                    # 핵심 기능
│       └── GameFrameXTrackingAuthorization.mm # 권한 추적
├── ICSharpCode.SharpZipLib.dll   # ZIP 압축 라이브러리
├── Microsoft.NET.StringTools.dll  # 문자열 도구
├── System.Buffers.dll            # 메모리 버퍼
├── System.IO.Pipelines.dll       # IO 파이프라인
├── System.Memory.dll            # 메모리 관리
└── System.Runtime.CompilerServices.Unsafe.dll # 런타임 지원
```

#### Plugins 서브모듈

| 서브모듈 | 설명 | 의존성 |
|------------|-------------|--------------|
| **iOS Plugin** | iOS 네이티브 기능 | GameFrameX.mm |
| **Compression Library** | ZIP 파일 압축/압축 해제 | SharpZipLib |
| **Memory Management** | 효율적인 메모리 작업 | StringTools, Memory, Buffers |
| **Runtime Support** | .NET 런타임 확장 | CompilerServices.Unsafe |

### 🛠️ Editor 모듈

개발 효율성을 높이기 위한 에디터 도구 및 확장 기능입니다.

```
Editor/
├── BuildHotfix/                  # 핫픽스 빌드 도구
│   ├── BuildHotfixHelper.cs     # 빌드 헬퍼
│   ├── HotFixAssemblyDefinitionHelper.cs # 핫픽스 어셈블리
│   └── HotFixEditorCompilerHelper.cs # 에디터 컴파일러
├── BuildProduct/                 # 프로덕트 빌드 어시스턴트
│   ├── BuildProductHelper.cs    # 빌드 헬퍼
│   ├── BuildPostProcessHelper.cs # 빌드 후 처리
│   ├── IBuilderPreHookHandler.cs # 사전 빌드 후크
│   └── IBuilderPostHookHandler.cs # 사후 빌드 후크
├── BuildWebGLTools/             # WebGL 빌드 도구
│   └── BuildWebGLToolsWithHybridCLR.cs # HybridCLR WebGL 빌드
├── Cropping/                     # 이미지 크롭 도구
│   └── CroppingWindow.cs        # 크롭 윈도우
├── Inspector/                    # 커스텀 인스펙터 패널
│   ├── BaseComponentInspector.cs # 기본 컴포넌트 인스펙터
│   ├── ObjectPoolComponentInspector.cs # 오브젝트 풀 인스펙터
│   └── ReferencePoolComponentInspector.cs # 참조 풀 인스펙터
├── InspectorLockShortcut/        # 인스펙터 잠금
│   └── InspectorLockShortcut.cs # 키보드 단축키 잠금
├── MiniGame/                      # 미니게임 플랫폼 지원 (21개 플랫폼) ⭐
│   ├── MiniGameDefineSymbolHelper.cs # 기본 정의 심볼 관리자
│   ├── DomesticMiniGames/          # 국내 미니게임
│   │   ├── MiniGameDefineSymbolHelper.WeChat.cs # WeChat
│   │   ├── MiniGameDefineSymbolHelper.Alipay.cs # Alipay
│   │   ├── MiniGameDefineSymbolHelper.DouYin.cs # DouYin
│   │   ├── MiniGameDefineSymbolHelper.KuaiShou.cs # KuaiShou
│   │   ├── MiniGameDefineSymbolHelper.Baidu.cs # Baidu
│   │   ├── MiniGameDefineSymbolHelper.JingDong.cs # JingDong
│   │   ├── MiniGameDefineSymbolHelper.Meituan.cs # Meituan
│   │   ├── MiniGameDefineSymbolHelper.Taobao.cs # Taobao
│   │   └── MiniGameDefineSymbolHelper.Bilibili.cs # Bilibili
│   ├── InternationalMiniGames/     # 국제 미니게임
│   │   ├── MiniGameDefineSymbolHelper.CrazyGames.cs # CrazyGames
│   │   ├── MiniGameDefineSymbolHelper.Discord.cs # Discord
│   │   ├── MiniGameDefineSymbolHelper.Facebook.cs # Facebook
│   │   ├── MiniGameDefineSymbolHelper.GooglePlay.cs # Google Play
│   │   ├── MiniGameDefineSymbolHelper.Poki.cs # Poki
│   │   ├── MiniGameDefineSymbolHelper.TikTok.cs # TikTok
│   │   └── MiniGameDefineSymbolHelper.YouTube.cs # YouTube
│   ├── DeviceOEMs/                # 기기 제조사 미니게임
│   │   ├── MiniGameDefineSymbolHelper.Huawei.cs # Huawei
│   │   ├── MiniGameDefineSymbolHelper.OPPO.cs # OPPO
│   │   ├── MiniGameDefineSymbolHelper.Vivo.cs # vivo
│   │   └── MiniGameDefineSymbolHelper.Xiaomi.cs # Xiaomi
│   └── GamePlatforms/             # 게임 플랫폼
│       └── MiniGameDefineSymbolHelper.TapTap.cs # TapTap
├── PackageManager/               # 패키지 관리자 윈도우
│   ├── PackageManagerWindow.cs   # 패키지 관리자 윈도우
│   └── PackagesManifest.cs     # 패키지 매니페스트
├── UpdatePackages/               # 패키지 업데이트 도구
│   └── UpdateAllPackageHelper.cs # 일괄 업데이트
├── Welcome/                      # 시작 윈도우
│   └── WelcomeWindow.cs         # 시작 인터페이스
└── Misc/                         # 기타 도구
    ├── HelperInfo.cs            # 헬퍼 정보
    ├── LogRedirection.cs        # 로그 리다이렉션
    ├── ScriptingDefineSymbols.cs # 정의 심볼 관리자
    ├── Type.cs                  # 타입 유틸리티
    └── OpenFolder.cs            # 폴더 열기
```

#### Editor 서브모듈

| 서브모듈 | 설명 | 주요 기능 |
|------------|-------------|---------------|
| **BuildHotfix** | 핫픽스 빌드 | HybridCLR 핫픽스 어셈블리 빌드 및 관리 |
| **BuildProduct** | 프로덕트 빌드 | 빌드 프로세스 자동화, 사전/사후 후크 |
| **BuildWebGLTools** | WebGL 빌드 | WebGL 플랫폼 전용 빌드 도구 |
| **Cropping** | 이미지 크롭 | 비주얼 이미지 크롭 도구 |
| **Inspector** | 커스텀 인스펙터 | 오브젝트 풀, 참조 풀 비주얼 모니터링 |
| **InspectorLockShortcut** | 인스펙터 잠금 | 인스펙터 패널 잠금용 키보드 단축키 |
| **MiniGame** | 미니게임 지원 | 21개 미니게임 플랫폼 원클릭 전환 (국내, 국제, 기기 제조사, 게임 플랫폼 분류) |
| **PackageManager** | 패키지 관리 | 비주얼 패키지 관리 인터페이스 |
| **UpdatePackages** | 패키지 업데이트 | 프로젝트 의존성 일괄 업데이트 |
| **Welcome** | 시작 인터페이스 | 신규 사용자 가이드 및 빠른 액세스 |
| **Misc** | 기타 | 로깅, 정의 심볼, 타입 등 |

---

## 🚀 빠른 시작

### 설치

#### 방법 1: Unity Package Manager (권장)

1. Unity 에디터를 엽니다
2. `Window` → `Package Manager`로 이동합니다
3. 왼쪽 상단의 `+` 버튼을 클릭합니다
4. `Add package from git URL`을 선택합니다
5. 다음을 입력합니다: `https://github.com/GameFrameX/com.gameframex.unity.git`

#### 방법 2: 수동 다운로드

1. 최신[릴리스](https://github.com/GameFrameX/com.gameframex.unity/releases)를 다운로드합니다
2. 프로젝트의 `Packages` 디렉토리에 압축을 풉니다

### 기본 사용법

```csharp
using GameFrameX.Runtime;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 오브젝트 풀 컴포넌트 가져오기
        var objectPool = GameEntry.GetComponent<ObjectPoolComponent>();
        
        // 참조 풀 컴포넌트 가져오기
        var referencePool = GameEntry.GetComponent<ReferencePoolComponent>();
        
        // 확장 메서드 사용
        transform.SetPositionX(10f);
        gameObject.SetActiveOptimized(true);
    }
}
```

---

## 💡 사용 예시

### Runtime 사용 예시

#### 🎯 오브젝트 풀 시스템

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

#### 📝 확장 메서드

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

#### 🔐 유틸리티 클래스

```csharp
// 파일 작업
Utility.File.WriteAllBytes("path/to/file", data);
byte[] content = Utility.File.ReadAllBytes("path/to/file");

// AES 암호화/복호화
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// 해시 계산
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");

// JSON 직렬화
var json = Utility.Json.ToJson(myObject);
var obj = Utility.Json.FromJson<MyClass>(json);
```

#### 📡 이벤트 시스템

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

### Editor 도구

#### 🎮 미니게임 플랫폼 지원

Unity 메뉴에서 미니게임 플랫폼 간에 쉽게 전환:

```
GameFrameX/
├── Scripting Define Symbols/
│   ├── Domestic Mini Games(국내 미니게임)/
│   │   ├── Enable WeChat Mini Game
│   │   ├── Enable Alipay Mini Game
│   │   ├── Enable DouYin Mini Game
│   │   ├── Enable KuaiShou Mini Game
│   │   ├── Enable Baidu Mini Game
│   │   ├── Enable JingDong Mini Game
│   │   ├── Enable Meituan Mini Game
│   │   ├── Enable Taobao Mini Game
│   │   └── Enable Bilibili Mini Game
│   ├── International Mini Games(국제 미니게임)/
│   │   ├── Enable Discord Mini Game
│   │   ├── Enable YouTube Mini Game
│   │   ├── Enable Facebook Mini Game
│   │   ├── Enable Google Play Mini Game
│   │   ├── Enable TikTok Mini Game
│   │   ├── Enable CrazyGames Mini Game
│   │   └── Enable Poki Mini Game
│   ├── Device OEMs(기기 제조사)/
│   │   ├── Enable Huawei Mini Game
│   │   ├── Enable OPPO Mini Game
│   │   ├── Enable Vivo Mini Game
│   │   └── Enable Xiaomi Mini Game
│   └── Game Platforms(게임 플랫폼)/
│       └── Enable TapTap Mini Game
```

#### 🏗️ 빌드 도구

```
GameFrameX/
├── Build Hotfix
├── Build Product
└── Build WebGL With HybridCLR
```

#### 📦 패키지 관리

```
GameFrameX/
├── Package Manager
└── Update All Packages
```

---

## 🎯 플랫폼 지원

### 운영 체제

| 플랫폼 | 상태 | 지원 버전 |
|----------|--------|-------------------|
| Windows | ✅ 지원 | Unity 2019.4+ |
| macOS | ✅ 지원 | Unity 2019.4+ |
| Linux | ✅ 지원 | Unity 2019.4+ |
| iOS | ✅ 지원 | Unity 2019.4+ |
| Android | ✅ 지원 | Unity 2019.4+ |
| WebGL | ✅ 지원 | Unity 2019.4+ |

### 미니게임 플랫폼 지원

GameFrameX는 원클릭 미니게임 플랫폼 지원을 제공하며, 전 세계 **21개의 주요 미니게임 플랫폼**을 지원합니다:

#### 🇨🇳 국내 미니게임 (9)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
|----------|---------------|--------|---------------|
| WeChat 미니게임 | `ENABLE_WECHAT_MINI_GAME` / `WEIXINMINIGAME` | 🇨🇳 중국 | 2000 |
| Alipay 미니게임 | `ENABLE_ALIPAY_MINI_GAME` / `ALIPAYMINIGAME` | 🇨🇳 중국 | 2400 |
| DouYin 미니게임 | `ENABLE_DOUYIN_MINI_GAME` / `DOUYINMINIGAME` | 🇨🇳 중국 | 2100 |
| KuaiShou 미니게임 | `ENABLE_KUAISHOU_MINI_GAME` / `KUAISHOUMINIGAME` | 🇨🇳 중국 | 2200 |
| Baidu 미니게임 | `ENABLE_BAIDU_MINI_GAME` / `BAIDUMINIGAME` | 🇨🇳 중국 | 2300 |
| JD 미니게임 | `ENABLE_JINGDONG_MINI_GAME` / `JINGDONGMINIGAME` | 🇨🇳 중국 | 2500 |
| Taobao 미니프로그램 | `ENABLE_TAOBAO_MINI_GAME` / `TAOBAOMINIGAME` | 🇨🇳 중국 | 2600 |
| Meituan 미니게임 | `ENABLE_MEITUAN_MINI_GAME` / `MEITUANMINIGAME` | 🇨🇳 중국 | 2800 |
| Bilibili 미니게임 | `ENABLE_BILIBILI_MINI_GAME` / `BILIBILIMINIGAME` | 🇨🇳 중국 | 2900 |

#### 🌍 국제 미니게임 (7)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
|----------|---------------|--------|---------------|
| Discord | `ENABLE_DISCORD_MINI_GAME` / `DISCORDMINIGAME` | 🌍 글로벌 | 2700 |
| YouTube | `ENABLE_YOUTUBE_MINI_GAME` / `YOUTUBEMINIGAME` | 🌍 글로벌 | 2800 |
| Facebook | `ENABLE_FACEBOOK_MINI_GAME` / `FACEBOOKMINIGAME` | 🌍 글로벌 | 2900 |
| Google Play | `ENABLE_GOOGLEPLAY_MINI_GAME` / `GOOGLEPLAYMINIGAME` | 🌍 글로벌 | 3000 |
| TikTok | `ENABLE_TIKTOK_MINI_GAME` / `TIKTOKMINIGAME` | 🌍 글로벌 | 3500 |
| CrazyGames | `ENABLE_CRAZYGAMES_MINI_GAME` / `CRAZYGAMESMINIGAME` | 🌍 글로벌 | 3600 |
| Poki | `ENABLE_POKI_MINI_GAME` / `POKIMINIGAME` | 🌍 글로벌 | 3700 |

#### 📱 기기 제조사 (4)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
|----------|---------------|--------|---------------|
| Huawei 미니게임 | `ENABLE_HUAWEI_MINI_GAME` / `HUAWEIMINIGAME` | 🇨🇳 중국 | 3400 |
| OPPO 미니게임 | `ENABLE_OPPO_MINI_GAME` / `OPPOSMINIGAME` | 🇨🇳 중국 | 3200 |
| vivo 미니게임 | `ENABLE_VIVO_MINI_GAME` / `VIVOMINIGAME` | 🇨🇳 중국 | 3100 |
| Xiaomi 미니게임 | `ENABLE_XIAOMI_MINI_GAME` / `XIAOMIMINIGAME` | 🇨🇳 중국 | 3300 |

#### 🎮 게임 플랫폼 (1)

| 플랫폼 | 정의 심볼 | 지역 | 메뉴 우선순위 |
|----------|---------------|--------|---------------|
| TapTap 미니게임 | `ENABLE_TAPTAP_MINI_GAME` / `TAPTAPMINIGAME` | 🇨🇳 중국 | 2700 |

#### 정의 심볼 상세

- **통합 정의**: `ENABLE_WEBGL_MINI_GAME` - 모든 미니게임 플랫폼에서 공유
- **플랫폼 정의**: 각 플랫폼의 조건부 컴파일을 위한 독립 정의
- **상호 배제 메커니즘**: 하나의 미니게임 플랫폼을 활성화하면 다른 플랫폼이 자동으로 비활성화됩니다
- **메뉴 경로**: `GameFrameX/Scripting Define Symbols/[카테고리]/Enable [Platform] Mini Game`

---

## 📚 문서 및 자료

- 📖 **전체 문서**: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🎯 **API 레퍼런스**: [API 문서](https://gameframex.doc.alianblank.com/api)
- 📝 **예제 프로젝트**: [예제 저장소](https://github.com/GameFrameX/Examples)
- 🎬 **비디오 튜토리얼**: [YouTube 채널](https://youtube.com/gameframex)

---

## 🤝 커뮤니티 및 지원

- 💬 **QQ 그룹**: [467608841](https://qm.qq.com/cgi-bin/qm/qr?k=sYFd1nv6m2KZIWFLorZ5pBR0AE5ZhbuL&jump_from=webapi&authKey=oCu+uoL3n35fT5SEt7iLgGtROPxh31n/rHUxRlp0w1f+j38W4tKBuWyRH3KEdwHN)
- 🐛 **이슈 트래커**: [GitHub Issues](https://github.com/GameFrameX/com.gameframex.unity/issues)
- 💡 **기능 요청**: [GitHub Discussions](https://github.com/GameFrameX/com.gameframex.unity/discussions)

---

## 🔄 변경 로그

### v1.3.6 (2025-05-28)
- 🐛 중복 GUID 문제 수정
- ✨ Meituan, Bilibili 미니게임 플랫폼 지원 추가
- ✨ 추가 확장 메서드 추가
- 📚 완전한 모듈 구조를 포함한 README 문서 강화
- 🔧 오브젝트 풀 성능 최적화
- 📚 문서 개선

전체 변경 로그 보기: [CHANGELOG.md](CHANGELOG.md)

---

## 📄 라이선스

이 프로젝트는 **MIT 라이선스** 및 **Apache 라이선스 2.0** 이중 라이선스로 배포됩니다.

전체 라이선스 텍스트 보기: [LICENSE.md](LICENSE.md)

---

## 👨‍💻 작성자

**Blank**

- 🌐 웹사이트: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🐙 GitHub: [@GameFrameX](https://github.com/GameFrameX)

---

<div align="center">

**이 프로젝트가 도움이 되셨다면, ⭐ 별을 주세요!**

[⬆ 맨 위로](#gameframex-unity-패키지)

</div>
