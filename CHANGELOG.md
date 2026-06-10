## [2.0.1](https://github.com/gameframex/com.gameframex.unity/compare/2.0.0...2.0.1) (2026-06-10)


### Bug Fixes

* **event:** 取消订阅时 handler 不存在改为警告而非抛异常 ([5900647](https://github.com/gameframex/com.gameframex.unity/commit/5900647cd8a15ec6699c2e94c66404699b57ea4f))

# [2.0.0](https://github.com/gameframex/com.gameframex.unity/compare/1.12.0...2.0.0) (2026-06-05)


### Bug Fixes

* **test:** 使用完全限定命名空间避免 SequenceReader 冲突 ([631c07d](https://github.com/gameframex/com.gameframex.unity/commit/631c07d652496e3f9585294ae1914bec9ae60a0c))


### chore

* 标记下一版本为破坏性更新 ([c326147](https://github.com/gameframex/com.gameframex.unity/commit/c3261476889c8ba826b229f8125833c405b4b0f4))


### BREAKING CHANGES

* 下个版本将包含多项公共 API 行为变更，升级前请阅读 CHANGELOG。

# [1.12.0](https://github.com/gameframex/com.gameframex.unity/compare/1.11.2...1.12.0) (2026-06-03)


### Bug Fixes

* **base:** EventPool 在抛出无处理器异常前先释放事件引用，避免引用池泄漏 ([ae10bd0](https://github.com/gameframex/com.gameframex.unity/commit/ae10bd05d5d49986ce0c02cbec98726eb0e93eb4))
* **base:** GameFrameworkMonoSingleton 使用 double-checked locking 保证线程安全并修正游戏对象命名 ([f847898](https://github.com/gameframex/com.gameframex.unity/commit/f8478988df7d96b3bb94050ed62731a9f5652e13))
* **base:** GenericVariable SetValue 增加类型检查防止无效类型转换，并统一字段命名为下划线前缀 ([a7beeb0](https://github.com/gameframex/com.gameframex.unity/commit/a7beeb0776a8d84c868f0b927145ff7dfcd9091f))
* **base:** 修复 Variable.SetValue 对值类型传入 null 时崩溃 ([28e89d0](https://github.com/gameframex/com.gameframex.unity/commit/28e89d08c1ec2d6bf69da74c21190b80a5c4daf7))
* **base:** 修复组件类型查找不支持派生类型的问题 ([a9322a4](https://github.com/gameframex/com.gameframex.unity/commit/a9322a4d0f9fef845aab73b0809d9a8d10844165))
* **buffer:** 修复 Span 缓冲区读写越界检查逻辑，增加负偏移检测 ([82a1849](https://github.com/gameframex/com.gameframex.unity/commit/82a1849c8df9816319f91cbab7b34d38d0d72350))
* **buffer:** 修复缓冲区读写越界检查逻辑，增加负偏移检测 ([175a0eb](https://github.com/gameframex/com.gameframex.unity/commit/175a0ebbd3ffc0a54f7c18a21628828eb911ebfe))
* **extension:** BidirectionalDictionary TryAdd 增加反向字典重复值检查防止双向映射不一致 ([8e63297](https://github.com/gameframex/com.gameframex.unity/commit/8e6329787bf2127de7fd12ed10ca09b31816b73f))
* **extension:** GameObjectExtension 预分配缓存列表容量并确保使用前 Clear ([11373e5](https://github.com/gameframex/com.gameframex.unity/commit/11373e5c7a97cb8100c2ecab02b43aa1ab7c9b9c))
* **extension:** StringExtensions CreateAsDirectory 使用 IsNullOrWhiteSpace 过滤空白输入并移除冗余递归调用 ([0521398](https://github.com/gameframex/com.gameframex.unity/commit/052139890af28f50d34fe7d50a9e1e114102c5bf))
* **extensions:** 修复双向字典、日期、字符串、类型扩展的 bug ([5120bb1](https://github.com/gameframex/com.gameframex.unity/commit/5120bb169cdb0bda7b1371eba22473e37f067598))
* **extension:** ThreadLocalRandom SetSeed 时重建 ThreadLocal 实例使种子生效 ([86c6d42](https://github.com/gameframex/com.gameframex.unity/commit/86c6d42bf28cad92a1b077cbc1fb743c82de0b3c))
* **helper:** DefaultCompressionHelper 添加 using 确保 GZipInputStream 释放并统一字段命名 ([179c87b](https://github.com/gameframex/com.gameframex.unity/commit/179c87be8d53b6a0d7c7bc376b137e6e885e260f))
* **helper:** PathHelper 移除静态 StringBuilder 避免线程安全问题 ([3348898](https://github.com/gameframex/com.gameframex.unity/commit/33488983696a14b9743a25eb8bd6b08f99aa935c))
* **helper:** PositionHelper 添加除零防护避免向量 y 分量为零时崩溃 ([5cdcd16](https://github.com/gameframex/com.gameframex.unity/commit/5cdcd16f6e062f99c9b10d7e737522e40a483849))
* **helper:** ZipHelper 移除静态 Crc32 实例避免线程安全问题 ([2e48290](https://github.com/gameframex/com.gameframex.unity/commit/2e482909e796f52ff4de1787d3f66330f746bc24))
* **objectpool:** 空值防护及释放过滤不修改入参列表 ([a06c28d](https://github.com/gameframex/com.gameframex.unity/commit/a06c28d31c850befb2242c08a875295ee99ac2df))
* **runtime:** BinaryExtension 使用 ThreadStatic 保证线程安全 ([b848a36](https://github.com/gameframex/com.gameframex.unity/commit/b848a36ca79b19340250491e07a26d868bd569dd))
* **runtime:** CollectionExtensions.Join 修复末尾多余分隔符 ([435d176](https://github.com/gameframex/com.gameframex.unity/commit/435d17660fe36b24630dd74bb93dd5cf63fe324b))
* **runtime:** RandomHelper.Next 使用 NextDouble 提高精度 ([01678e1](https://github.com/gameframex/com.gameframex.unity/commit/01678e126330da6b97b1b90ee60f11be8538ddbe))
* **time:** Month.WithTimeZone 时间戳方法统一使用 DateTimeToSecondsWithTimeZone ([921a9a1](https://github.com/gameframex/com.gameframex.unity/commit/921a9a1a926f81a5523833a526533473cb4ebc8d))
* **time:** UnixTimeSeconds/UnixTimeMilliseconds 去除 TimeOffset 污染，返回纯净 UTC 时间戳 ([0f84181](https://github.com/gameframex/com.gameframex.unity/commit/0f84181ceb8adb2ba50b2ad5888b7dc0bb22df1b))
* **utility:** Marshal 非托管内存指针添加 lock 保护线程安全 ([b2ccb0d](https://github.com/gameframex/com.gameframex.unity/commit/b2ccb0dc3d480b5237e4bcaddc571d76844666d4))
* **utility:** MD5 移除静态实例避免线程安全问题 ([68ff188](https://github.com/gameframex/com.gameframex.unity/commit/68ff18873e34bffc9b55dd3b52eeda5f2e94f799))
* **utility:** RandomUtility 使用 ThreadLocal 保证线程安全并修复 SetSeed 失效问题 ([75fef41](https://github.com/gameframex/com.gameframex.unity/commit/75fef4121864999a2816f73807c45d966fdb181e))
* **utility:** 多处修复线程安全、加密安全及异常处理 ([97549d9](https://github.com/gameframex/com.gameframex.unity/commit/97549d9b0f5ee6d4ba351ee1bcf76f143e2e82c2))
* **utility:** 移除 Verifier 静态 CRC 实例避免线程安全问题 ([1fb41a7](https://github.com/gameframex/com.gameframex.unity/commit/1fb41a724a21977785cdf269a775722042680327))


### Features

* **time:** 为 TimerHelper 公开成员添加 [Preserve] 防裁剪标签 ([97a9b40](https://github.com/gameframex/com.gameframex.unity/commit/97a9b40e9bf09c9efcd9da09ce163e9253a7ea09))


### Performance Improvements

* **editor:** CroppingWindow 移除冗余 ToLower 调用 ([19ce200](https://github.com/gameframex/com.gameframex.unity/commit/19ce200cf5c0290145941476b8c9e2f25e535bf3))

## [1.11.2](https://github.com/gameframex/com.gameframex.unity/compare/1.11.1...1.11.2) (2026-05-29)


### Bug Fixes

* **Runtime:** 移除 BetterStreamingAssets 程序集引用 ([08a135c](https://github.com/gameframex/com.gameframex.unity/commit/08a135c6ebb20d169c15efe007ee26b88ffadb1d))

## [1.11.1](https://github.com/gameframex/com.gameframex.unity/compare/1.11.0...1.11.1) (2026-05-28)


### Bug Fixes

* **ci:** 统一 .github 工作流配置 ([7e41cb3](https://github.com/gameframex/com.gameframex.unity/commit/7e41cb34abeab503a743a4528d72000fe07c8914))

# [1.11.0](https://github.com/gameframex/com.gameframex.unity/compare/1.10.1...1.11.0) (2026-05-25)


### Features

* **PackageManager:** 添加 scoped registry 配置与迁移功能 ([d12075b](https://github.com/gameframex/com.gameframex.unity/commit/d12075b6b6cd7fa0b059e18f7f6b2b599577313b))

## [1.10.1](https://github.com/gameframex/com.gameframex.unity/compare/1.10.0...1.10.1) (2026-05-24)


### Bug Fixes

* **MiniGame:** 调整 UNITY_WEBGL 编译指令作用域 ([e9b0603](https://github.com/gameframex/com.gameframex.unity/commit/e9b0603c29ecbf4add8c888dee8a309a373c468d)), closes [#if](https://github.com/gameframex/com.gameframex.unity/issues/if)

# [1.10.0](https://github.com/gameframex/com.gameframex.unity/compare/1.9.0...1.10.0) (2026-04-16)


### Features

* **Editor:** 添加 Discord 平台小游戏宏定义管理支持 ([14b1121](https://github.com/gameframex/com.gameframex.unity/commit/14b112199d83a83f45a2525a080200225512b1ed))
* **Editor:** 添加 Facebook 小游戏平台宏定义支持 ([234ecef](https://github.com/gameframex/com.gameframex.unity/commit/234ecefeb534d9bd539c120314c44fe096bbbfd1))
* **Editor:** 添加 Poki 小游戏平台宏定义配置支持 ([931248b](https://github.com/gameframex/com.gameframex.unity/commit/931248bb5f6c55feb820db85d6cf80e0176cbbf4))
* **Editor:** 添加 TikTok 小游戏宏定义管理支持 ([f512956](https://github.com/gameframex/com.gameframex.unity/commit/f512956d1da8ddd38e0ca5851429f7db07456adc))
* **Editor:** 添加Bilibili小游戏适配宏定义支持 ([a911ac4](https://github.com/gameframex/com.gameframex.unity/commit/a911ac4b76a0507a4ae2d5b3a540a08a405e59e2))
* **Editor:** 添加vivo小游戏适配宏定义辅助工具 ([8fc9c27](https://github.com/gameframex/com.gameframex.unity/commit/8fc9c27f010c83e04273631a06ba6c6498e18e30))
* **Editor:** 添加京东小游戏宏定义支持 ([f6b6058](https://github.com/gameframex/com.gameframex.unity/commit/f6b605816314d598a5a7b87b948c6303a9da80ed))
* **Editor:** 添加华为小游戏宏定义配置支持 ([47db689](https://github.com/gameframex/com.gameframex.unity/commit/47db68958763584938dbb1776f802670e9f43bd1))
* **Editor:** 添加小米小游戏适配宏定义工具 ([978f34d](https://github.com/gameframex/com.gameframex.unity/commit/978f34df49a5364fb9ae97d114e887507f55bab6))
* **Editor:** 添加淘宝小程序宏定义支持 ([d4fcf1d](https://github.com/gameframex/com.gameframex.unity/commit/d4fcf1d7d36918f7c77965255b5263e201e49467))
* **Editor:** 添加美团和哔哩哔哩小游戏平台定义符号支持 ([365335d](https://github.com/gameframex/com.gameframex.unity/commit/365335db72327f2a16c4e8a57e7830900d4e1942))
* 添加 OPPO 小游戏适配宏定义助手 ([e73ab82](https://github.com/gameframex/com.gameframex.unity/commit/e73ab828f2f0ca847eee26ba1fe9a6d2bd65f21c))
* 添加多个新平台的小游戏脚本定义符号 ([e250751](https://github.com/gameframex/com.gameframex.unity/commit/e25075160809737694eafcbe3e6a12e1111eff67))
* **编辑器:** 添加 CrazyGames 小游戏平台宏定义管理功能 ([c0414a0](https://github.com/gameframex/com.gameframex.unity/commit/c0414a03956261934fa9ad8ba3c8030c85e35133))
* **编辑器:** 添加 Google Play 游戏平台宏定义助手 ([210bc17](https://github.com/gameframex/com.gameframex.unity/commit/210bc17fb2f82b62f8394c4846fa7009c26d423e))
* **编辑器:** 添加 YouTube 小游戏平台宏定义支持 ([aa42bd3](https://github.com/gameframex/com.gameframex.unity/commit/aa42bd34342787fd4c7ee866e876c7668f32c12a))
* **编辑器:** 添加美团小游戏宏定义助手 ([3d5ae9f](https://github.com/gameframex/com.gameframex.unity/commit/3d5ae9f85a5b44b91dd983b0307d6540a9fd2cc0))

# [1.9.0](https://github.com/gameframex/com.gameframex.unity/compare/1.8.0...1.9.0) (2026-04-10)


### Features

* **editor:** 添加小游戏统一宏定义管理 ([5fa11fe](https://github.com/gameframex/com.gameframex.unity/commit/5fa11fead31f3958edf658d96e8292e345e4e9ba))

# [1.8.0](https://github.com/gameframex/com.gameframex.unity/compare/1.7.3...1.8.0) (2026-04-10)


### Features

* **editor:** 新增多个小游戏平台宏定义助手并重构代码结构 ([e83b90e](https://github.com/gameframex/com.gameframex.unity/commit/e83b90e2621de39aa21ed3d18ac2374a778b2709))

## [1.7.3](https://github.com/gameframex/com.gameframex.unity/compare/1.7.2...1.7.3) (2026-04-07)


### Bug Fixes

* **extension:** 重命名StringExtensions类为StringExtension ([336d219](https://github.com/gameframex/com.gameframex.unity/commit/336d219089582f06719d3e5f28226108c830af47))

## [1.7.2](https://github.com/gameframex/com.gameframex.unity/compare/1.7.1...1.7.2) (2026-04-07)


### Bug Fixes

* **editor:** 将欢迎窗口的logo加载方式从Resources改为AssetDatabase ([57e09ff](https://github.com/gameframex/com.gameframex.unity/commit/57e09ff12838e61cc0fb700955bcbb06a74fe2b6))

## [1.7.1](https://github.com/gameframex/com.gameframex.unity/compare/1.7.0...1.7.1) (2026-04-01)


### Bug Fixes

* 修复日志方法参数错误并更正类型名称 ([8c76328](https://github.com/gameframex/com.gameframex.unity/commit/8c76328c3ad084b7a9911dbf9866c6427a93d3a8))

# [1.7.0](https://github.com/gameframex/com.gameframex.unity/compare/1.6.3...1.7.0) (2026-03-31)


### Bug Fixes

* **Extension:** 为字符串扩展方法添加空值检查 ([b8b1517](https://github.com/gameframex/com.gameframex.unity/commit/b8b151779aaf8efecbe773825d55602bfbfd19f3))
* **Extension:** 修正集合扩展方法中的拼写错误和线程安全问题 ([3f92a96](https://github.com/gameframex/com.gameframex.unity/commit/3f92a960a0b4b572711ce381f180f1e8ad38c696))


### Features

* **编辑器:** 扩展小游戏宏定义支持并重构菜单项 ([acd450e](https://github.com/gameframex/com.gameframex.unity/commit/acd450e53a4e1ffa1888674fa91dc76d0456a9f4))
* **随机数:** 添加设置种子和生成64位随机数的方法 ([e39cfa5](https://github.com/gameframex/com.gameframex.unity/commit/e39cfa52494cda5abbf8d3f58bbb6aadd6d7a1a0))

## [1.6.3](https://github.com/gameframex/com.gameframex.unity/compare/1.6.2...1.6.3) (2026-03-04)


### Bug Fixes

* **构建热更新:** 修复未找到HybridCLR数据目录时的错误 ([a44c0c5](https://github.com/gameframex/com.gameframex.unity/commit/a44c0c548c95f2e2dff2b2d8521c98f62fe93b2c))

## [1.6.2](https://github.com/gameframex/com.gameframex.unity/compare/1.6.1...1.6.2) (2026-03-04)


### Bug Fixes

* 在生成AOT代码前先清理旧文件 ([8e923a0](https://github.com/gameframex/com.gameframex.unity/commit/8e923a08253e3f61ea18587d47d8825147420002))

## [1.6.1](https://github.com/gameframex/com.gameframex.unity/compare/1.6.0...1.6.1) (2026-01-29)


### Bug Fixes

* 修复 GetBytesSize 方法中大数值单位转换错误 ([098e376](https://github.com/gameframex/com.gameframex.unity/commit/098e37639cc5428d970b4e81f82edf6a1d238232))

# [1.6.0](https://github.com/gameframex/com.gameframex.unity/compare/1.5.1...1.6.0) (2025-12-23)


### Bug Fixes

* 修正热更新DLL文件名大小写不一致问题 ([54593b9](https://github.com/gameframex/com.gameframex.unity/commit/54593b9166a5223e6b418d11a4a2df64002a0f34))


### Features

* **editor:** 添加欢迎窗口及资源文件 ([e51a267](https://github.com/gameframex/com.gameframex.unity/commit/e51a26790817143d55dd83540073a6c37d99cc5d))
* **GameObject:** 添加销毁组件的方法 ([23c9e36](https://github.com/gameframex/com.gameframex.unity/commit/23c9e361fc080bae484c3c0b13d26e7a17f8115b))
* **Vector扩展:** 添加Vector2/3/4之间的相互转换方法 ([c4024df](https://github.com/gameframex/com.gameframex.unity/commit/c4024dff50b2ab3d56aea8e49a9aaa2883760418))
* **平台支持:** 添加微信和抖音小游戏平台支持 ([2619bd9](https://github.com/gameframex/com.gameframex.unity/commit/2619bd9076e3583c7243f8630e4374361200699e))

# Changelog

## [1.5.1](https://github.com/GameFrameX/com.gameframex.unity/tree/1.5.1) (2025-10-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.5.0...1.5.1)

## [1.5.0](https://github.com/GameFrameX/com.gameframex.unity/tree/1.5.0) (2025-10-23)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.4.0...1.5.0)

## [1.4.0](https://github.com/GameFrameX/com.gameframex.unity/tree/1.4.0) (2025-09-18)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.9...1.4.0)

## [1.3.9](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.9) (2025-08-12)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.8...1.3.9)

## [1.3.8](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.8) (2025-07-08)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.7...1.3.8)

## [1.3.7](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.7) (2025-06-01)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.6...1.3.7)

## [1.3.6](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.6) (2025-05-28)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.5...1.3.6)

## [1.3.5](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.5) (2025-05-21)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.4...1.3.5)

## [1.3.4](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.4) (2025-05-21)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.3...1.3.4)

**Closed issues:**

- 文件GUID重复的问题 [\#1](https://github.com/GameFrameX/com.gameframex.unity/issues/1)

## [1.3.3](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.3) (2025-05-19)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.2...1.3.3)

## [1.3.2](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.2) (2025-04-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.3.0...1.3.2)

## [1.3.0](https://github.com/GameFrameX/com.gameframex.unity/tree/1.3.0) (2025-02-07)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.2.1...1.3.0)

## [1.2.1](https://github.com/GameFrameX/com.gameframex.unity/tree/1.2.1) (2025-02-07)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.2.0...1.2.1)

## [1.2.0](https://github.com/GameFrameX/com.gameframex.unity/tree/1.2.0) (2025-02-05)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.1.1...1.2.0)

## [1.1.1](https://github.com/GameFrameX/com.gameframex.unity/tree/1.1.1) (2025-01-02)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.1.0...1.1.1)

## [1.1.0](https://github.com/GameFrameX/com.gameframex.unity/tree/1.1.0) (2024-12-25)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.11...1.1.0)

## [1.0.11](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.11) (2024-11-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.10...1.0.11)

## [1.0.10](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.10) (2024-11-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.9...1.0.10)

## [1.0.9](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.9) (2024-09-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.8...1.0.9)

## [1.0.8](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.8) (2024-09-23)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.7...1.0.8)

## [1.0.7](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.7) (2024-09-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.6...1.0.7)

## [1.0.6](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.6) (2024-09-06)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.5...1.0.6)

## [1.0.5](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.5) (2024-09-05)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.4...1.0.5)

## [1.0.4](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.4) (2024-08-19)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/1.0.3...1.0.4)

## [1.0.3](https://github.com/GameFrameX/com.gameframex.unity/tree/1.0.3) (2024-07-30)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity/compare/793f226b9bb27ea14d01bd33505638231c45d3d8...1.0.3)



\* *This Changelog was automatically generated by [github_changelog_generator](https://github.com/github-changelog-generator/github-changelog-generator)*
