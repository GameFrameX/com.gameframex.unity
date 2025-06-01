<div align="center">

# GameFrameX Unity Package

[![Version](https://img.shields.io/badge/version-1.3.6-blue.svg)](https://github.com/GameFrameX/com.gameframex.unity)
[![Unity](https://img.shields.io/badge/Unity-2019.4+-green.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/license-MIT-orange.svg)](LICENSE.md)
[![Documentation](https://img.shields.io/badge/docs-gameframex.doc.alianblank.com-brightgreen.svg)](https://gameframex.doc.alianblank.com)

**独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使**

[📖 文档](https://gameframex.doc.alianblank.com) • [🚀 快速开始](#快速开始) • [💬 QQ群: 216332935](https://qm.qq.com/cgi-bin/qm/qr?k=xxx)

</div>

---

## ✨ 项目简介

GameFrameX 是一个专为独立游戏开发者设计的现代化Unity游戏框架，提供完整的前后端一体化解决方案。框架采用模块化架构设计，内置丰富的游戏开发工具和组件，帮助开发者快速构建高质量的游戏项目。

### 🎯 核心特性

- 🏗️ **模块化架构** - 基于组件系统的可扩展框架设计
- 🔧 **丰富工具集** - 内置多种开发辅助工具和编辑器扩展
- 📦 **对象池管理** - 高效的内存管理和对象复用机制
- 🎨 **扩展方法库** - 丰富的Unity引擎扩展方法
- 🛠️ **实用工具类** - 涵盖加密、压缩、网络等常用功能
- 📱 **多平台支持** - 支持PC、移动端、WebGL等多平台部署
- 🔥 **热更新支持** - 内置HybridCLR热更新解决方案

## 📋 系统要求

- **Unity版本**: 2019.4 或更高版本
- **平台支持**: Windows, macOS, Linux, iOS, Android, WebGL
- **.NET版本**: .NET Standard 2.0+

## 🚀 快速开始

### 安装方式

#### 方式一：Unity Package Manager (推荐)

1. 打开Unity编辑器
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

## 🏗️ 架构概览

### 核心模块

| 模块 | 描述 | 主要功能 |
|------|------|----------|
| **Base** | 框架核心基础 | 组件管理、事件系统、生命周期管理 |
| **ObjectPool** | 对象池系统 | 对象复用、内存优化、性能提升 |
| **ReferencePool** | 引用池系统 | 引用类型对象管理、GC优化 |
| **Helper** | 工具助手类 | 文件操作、网络请求、数学计算等 |
| **Extension** | 扩展方法库 | Unity引擎类型扩展、便捷操作 |
| **Utility** | 实用工具类 | 加密解密、压缩解压、哈希计算 |

### 编辑器工具

| 工具 | 功能描述 |
|------|----------|
| **BuildHotfix** | 热更新构建工具 |
| **BuildProduct** | 产品构建助手 |
| **PackageManager** | 包管理器窗口 |
| **Cropping** | 图片裁剪工具 |
| **Inspector** | 自定义检视面板 |

## 🔧 主要功能

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
```

### 扩展方法使用

```csharp
// Transform扩展
transform.SetPositionX(10f);
transform.SetLocalScaleXYZ(2f, 2f, 2f);
transform.ResetTransformation();

// GameObject扩展
gameObject.SetActiveOptimized(true);
gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));

// Vector扩展
Vector3 pos = transform.position;
pos = pos.WithX(5f).WithY(10f);
```

### 实用工具类

```csharp
// 文件操作
Utility.File.WriteAllBytes("path/to/file", data);
byte[] content = Utility.File.ReadAllBytes("path/to/file");

// 加密解密
string encrypted = Utility.Encryption.Aes.Encrypt("plaintext", "key");
string decrypted = Utility.Encryption.Aes.Decrypt(encrypted, "key");

// 哈希计算
string md5 = Utility.Hash.Md5.ComputeHash("input");
string sha1 = Utility.Hash.Sha1.ComputeHash("input");
```

## 📚 文档与资源

- 📖 **完整文档**: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🎯 **API参考**: [API Documentation](https://gameframex.doc.alianblank.com/api)
- 📝 **示例项目**: [Examples Repository](https://github.com/GameFrameX/Examples)
- 🎬 **视频教程**: [YouTube频道](https://youtube.com/gameframex)

## 🤝 社区与支持

- 💬 **QQ讨论群**: 216332935
- 🐛 **问题反馈**: [GitHub Issues](https://github.com/GameFrameX/com.gameframex.unity/issues)
- 💡 **功能建议**: [GitHub Discussions](https://github.com/GameFrameX/com.gameframex.unity/discussions)
- 📧 **邮件联系**: alianblank@outlook.com

## 🔄 更新日志

### v1.3.6 (2025-05-28)
- 🐛 修复文件GUID重复的问题
- ✨ 新增更多扩展方法
- 🔧 优化对象池性能
- 📚 完善文档说明

查看完整更新日志: [CHANGELOG.md](CHANGELOG.md)

## 📄 开源协议

本项目采用 [MIT License](LICENSE.md) 开源协议。

## 👨‍💻 作者信息

**Blank**
- 📧 Email: alianblank@outlook.com
- 🌐 Website: [https://gameframex.doc.alianblank.com](https://gameframex.doc.alianblank.com)
- 🐙 GitHub: [@GameFrameX](https://github.com/GameFrameX)

---

<div align="center">

**如果这个项目对你有帮助，请给我们一个 ⭐ Star！**

[⬆ 回到顶部](#gameframex-unity-package)

</div>
