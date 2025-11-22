// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================
//
// using System;
// using System.Collections.Generic;
//
// namespace GameFrameX.Runtime
// {
//     /// <summary>
//     /// ZString - 高性能零分配字符串处理类
//     /// <para>
//     /// 该类通过对象池技术和内存操作优化，实现了字符串的零GC分配和高效处理。
//     /// 主要特性包括：
//     /// </para>
//     /// <list type="bullet">
//     /// <item><description>对象池管理：通过多级缓存机制复用字符串对象，减少GC压力</description></item>
//     /// <item><description>内存优化：使用unsafe代码和结构体数组实现高效的内存拷贝操作</description></item>
//     /// <item><description>块作用域管理：支持using语句块，自动管理字符串生命周期</description></item>
//     /// <item><description>深拷贝/浅拷贝：支持两种拷贝模式，满足不同性能需求</description></item>
//     /// <item><description>字符串池：提供字符串驻留功能，减少重复字符串的内存占用</description></item>
//     /// </list>
//     /// <para>
//     /// 使用示例：
//     /// <code>
//     /// using (ZString.Block()) {
//     ///     ZString str1 = "Hello";
//     ///     ZString str2 = 123;
//     ///     ZString result = str1 + " " + str2 + " World!";
//     ///     Debug.Log(result.ToString()); // 输出: Hello 123 World!
//     /// } // 退出using块时自动释放所有ZString对象
//     /// </code>
//     /// </para>
//     /// <remarks>
//     /// 所有ZString操作必须在ZString.Block()作用域内进行，否则会抛出InvalidOperationException。
//     /// 该类设计用于高频字符串操作场景，如游戏开发中的日志、UI文本处理等。
//     /// </remarks>
//     /// </summary>
//     public sealed class XString
//     {
//         /// <summary>
//         /// 8192字节结构体 - 用于大块内存拷贝优化
//         /// 通过结构体嵌套实现高效的8192字节内存块操作
//         /// </summary>
//         struct Byte8192
//         {
//             Byte4096 a1;
//             Byte4096 a2;
//         }
//
//         /// <summary>
//         /// 4096字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte4096
//         {
//             Byte2048 a1;
//             Byte2048 a2;
//         }
//
//         /// <summary>
//         /// 2048字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte2048
//         {
//             Byte1024 a1;
//             Byte1024 a2;
//         }
//
//         /// <summary>
//         /// 1024字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte1024
//         {
//             Byte512 a1;
//             Byte512 a2;
//         }
//
//         /// <summary>
//         /// 512字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte512
//         {
//             Byte256 a1;
//             Byte256 a2;
//         }
//
//         /// <summary>
//         /// 256字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte256
//         {
//             Byte128 a1;
//             Byte128 a2;
//         }
//
//         /// <summary>
//         /// 128字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte128
//         {
//             Byte64 a1;
//             Byte64 a2;
//         }
//
//         /// <summary>
//         /// 64字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte64
//         {
//             Byte32 a1;
//             Byte32 a2;
//         }
//
//         /// <summary>
//         /// 32字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte32
//         {
//             Byte16 a1;
//             Byte16 a2;
//         }
//
//         /// <summary>
//         /// 16字节结构体 - 内存拷贝优化用
//         /// </summary>
//         struct Byte16
//         {
//             Byte8 a1;
//             Byte8 a2;
//         }
//
//         /// <summary>
//         /// 8字节结构体 - 内存拷贝优化用，对应一个long类型
//         /// </summary>
//         struct Byte8
//         {
//             long a1;
//         }
//
//         /// <summary>
//         /// 4字节结构体 - 内存拷贝优化用，对应一个int类型
//         /// </summary>
//         struct Byte4
//         {
//             int a1;
//         }
//
//         /// <summary>
//         /// 2字节结构体 - 内存拷贝优化用，对应一个short类型
//         /// </summary>
//         struct Byte2
//         {
//             short a;
//         }
//
//         /// <summary>
//         /// 1字节结构体 - 内存拷贝优化用，对应一个byte类型
//         /// </summary>
//         struct Byte1
//         {
//             byte a;
//         }
//
//         /// <summary>
//         /// 核心缓存数组 - 存储固定长度字符串的深拷贝对象池
//         /// 索引对应字符串长度，每个位置存储对应长度的ZString队列
//         /// </summary>
//         static Queue<XString>[] g_cache;
//
//         /// <summary>
//         /// 次级缓存字典 - 存储超出核心缓存容量范围的字符串对象池
//         /// Key为字符串长度，Value为对应长度的ZString队列
//         /// </summary>
//         static Dictionary<int, Queue<XString>> g_secCache;
//
//         /// <summary>
//         /// 浅拷贝缓存栈 - 存储浅拷贝模式的ZString对象
//         /// 浅拷贝直接引用原字符串，不进行内容复制，性能更高
//         /// </summary>
//         static Stack<XString> g_shallowCache;
//
//         /// <summary>
//         /// ZString块缓存栈 - 存储可复用的zstring_block对象
//         /// 用于块作用域管理，避免频繁创建和销毁block对象
//         /// </summary>
//         static Stack<ZStringBlock> g_blocks;
//
//         /// <summary>
//         /// 当前打开的ZString块栈 - 管理嵌套的using块
//         /// 支持ZString.Block()的嵌套调用，维护当前活动块的层次结构
//         /// </summary>
//         static Stack<ZStringBlock> g_open_blocks;
//
//         /// <summary>
//         /// 字符串驻留表 - 存储已经驻留的字符串
//         /// Key为字符串的哈希码，Value为驻留后的字符串实例
//         /// 通过字符串驻留避免相同内容的重复字符串对象
//         /// </summary>
//         static Dictionary<int, string> g_intern_table;
//
//         /// <summary>
//         /// 当前活动块 - 指向当前正在使用的zstring_block
//         /// 所有新创建的ZString都会自动添加到当前块的管理范围
//         /// </summary>
//         public static ZStringBlock g_current_block;
//
//         /// <summary>
//         /// 查找结果缓存 - 用于字符串替换操作时记录所有匹配位置
//         /// 在Replace操作中临时存储所有需要替换的子串起始索引
//         /// </summary>
//         static List<int> g_finds;
//
//         /// <summary>
//         /// 格式化参数缓存 - 存储字符串格式化时的参数数组
//         /// 在Format操作中临时存储格式化参数，避免重复分配数组
//         /// </summary>
//         static XString[] g_format_args;
//
//         /// <summary>
//         /// 初始块容量 - 块缓存栈的默认容量
//         /// 控制同时可用的zstring_block对象数量
//         /// </summary>
//         const int INITIAL_BLOCK_CAPACITY = 32;
//
//         /// <summary>
//         /// 初始缓存容量 - 核心缓存数组的默认大小
//         /// 定义支持的最大固定字符串长度范围（0-127）
//         /// </summary>
//         const int INITIAL_CACHE_CAPACITY = 128;
//
//         /// <summary>
//         /// 初始栈容量 - 每个字符串缓存队列的默认容量
//         /// 每个长度对应的ZString对象池中默认存储的对象数量
//         /// </summary>
//         const int INITIAL_STACK_CAPACITY = 48;
//
//         /// <summary>
//         /// 初始驻留容量 - 字符串驻留表的默认容量
//         /// 驻留表初始可容纳的字符串对象数量
//         /// </summary>
//         const int INITIAL_INTERN_CAPACITY = 256;
//
//         /// <summary>
//         /// 初始开放容量 - 同时打开的块数量上限
//         /// 支持的嵌套using块的最大深度
//         /// </summary>
//         const int INITIAL_OPEN_CAPACITY = 5;
//
//         /// <summary>
//         /// 初始浅拷贝容量 - 浅拷贝缓存栈的默认容量
//         /// 浅拷贝模式可用的最大ZString对象数量
//         /// </summary>
//         const int INITIAL_SHALLOW_CAPACITY = 100;
//
//         /// <summary>
//         /// 新分配填充字符 - 用于初始化新分配字符串内存的填充字符
//         /// 使用'X'字符填充新字符串的内存空间，用于调试和内存清理
//         /// </summary>
//         const char NEW_ALLOC_CHAR = 'X';
//
//         /// <summary>
//         /// 浅拷贝标记 - 指示当前ZString是否为浅拷贝模式
//         /// true：浅拷贝，直接引用原字符串；false：深拷贝，独立复制字符串内容
//         /// </summary>
//         private bool isShallow = false;
//
//         /// <summary>
//         /// 字符串值 - 存储ZString的实际字符串内容
//         /// 使用[NonSerialized]标记避免序列化，提高性能
//         /// </summary>
//         [NonSerialized] string _value;
//
//         /// <summary>
//         /// 销毁标记 - 指示ZString是否已被释放回对象池
//         /// true：已释放；false：正常使用中。防止重复释放或使用已释放对象
//         /// </summary>
//         [NonSerialized] bool _disposed;
//
//         /// <summary>
//         /// 默认构造函数 - 私有构造，禁止外部直接实例化
//         /// ZString必须通过对象池获取，确保正确的内存管理和生命周期控制
//         /// </summary>
//         /// <exception cref="NotSupportedException">总是抛出，禁止直接构造</exception>
//         private XString()
//         {
//             throw new NotSupportedException("不允许直接构造ZString，请使用ZString.Block()作用域内的隐式转换或静态方法。");
//         }
//
//         /// <summary>
//         /// 指定长度构造函数 - 创建指定长度的新ZString实例
//         /// 根据字符串长度从相应的对象池中获取可用实例，提高内存使用效率
//         /// </summary>
//         /// <param name="length">字符串长度</param>
//         /// <remarks>
//         /// 新创建的字符串使用填充字符NEW_ALLOC_CHAR('X')初始化所有位置
//         /// 此构造函数仅内部使用，用于对象池的预分配和创建新实例
//         /// </remarks>
//         private XString(int length)
//         {
//             _value = new string(NEW_ALLOC_CHAR, length);
//         }
//
//         /// <summary>
//         /// 浅拷贝构造函数 - 创建浅拷贝模式的ZString实例
//         /// 浅拷贝直接引用源字符串，不进行内容复制，提供更高的性能
//         /// </summary>
//         /// <param name="value">要引用的源字符串</param>
//         /// <param name="shallow">是否为浅拷贝模式，必须为true</param>
//         /// <exception cref="NotSupportedException">当shallow为false时抛出</exception>
//         /// <remarks>
//         /// 浅拷贝模式适用于只读场景，特别是从外部string转换为ZString时
//         /// 不会复制字符串内容，仅保存引用，内存使用更高效
//         /// </remarks>
//         private XString(string value, bool shallow)
//         {
//             if (!shallow)
//             {
//                 throw new NotSupportedException("此构造函数仅支持浅拷贝模式（shallow=true）。");
//             }
//
//             _value = value;
//             isShallow = true;
//         }
//
//         static XString()
//         {
//             Initialize(INITIAL_CACHE_CAPACITY,
//                        INITIAL_STACK_CAPACITY,
//                        INITIAL_BLOCK_CAPACITY,
//                        INITIAL_INTERN_CAPACITY,
//                        INITIAL_OPEN_CAPACITY,
//                        INITIAL_SHALLOW_CAPACITY
//             );
//
//             g_finds = new List<int>(10);
//             g_format_args = new XString[10];
//         }
//
//         /// <summary>
//         /// 释放ZString实例 - 将对象归还到相应的对象池中
//         /// 根据是否为浅拷贝模式，选择不同的缓存路径
//         /// </summary>
//         /// <exception cref="ObjectDisposedException">当对象已被释放时抛出</exception>
//         /// <remarks>
//         /// 释放过程：
//         /// 1. 检查对象是否已被释放，防止重复释放
//         /// 2. 根据isShallow标记选择缓存路径：
//         ///    - 浅拷贝：归入g_shallowCache
//         ///    - 深拷贝：根据字符串长度归入g_cache或g_secCache
//         /// 3. 标记对象为已释放状态
//         /// </remarks>
//         private void dispose()
//         {
//             if (_disposed)
//             {
//                 throw new ObjectDisposedException(this.ToString());
//             }
//
//             if (isShallow) // 深浅拷贝走不同缓存
//             {
//                 g_shallowCache.Push(this);
//             }
//             else
//             {
//                 Queue<XString> stack;
//                 if (g_cache.Length > Length)
//                 {
//                     stack = g_cache[Length]; // 取出value length长度的栈，将自身push进去
//                 }
//                 else
//                 {
//                     stack = g_secCache[Length];
//                 }
//
//                 stack.Enqueue(this);
//             }
//
//             //memcpy(_value, NEW_ALLOC_CHAR); // 内存拷贝至value，暂时注释以提升性能
//             _disposed = true;
//         }
//
//         /// <summary>
//         /// 从string创建深拷贝ZString - 复制字符串内容到新的ZString实例
//         /// </summary>
//         /// <param name="value">源字符串，可以为null</param>
//         /// <returns>深拷贝的ZString实例，如果输入为null则返回null</returns>
//         /// <remarks>
//         /// 深拷贝特点：
//         /// - 完全复制字符串内容，创建独立的数据副本
//         /// - 修改源字符串不会影响ZString内容
//         /// - 适用于需要对字符串进行修改的场景
//         /// </remarks>
//         private static XString Get(string value)
//         {
//             if (value == null)
//             {
//                 return null;
//             }
// #if DBG
//             if (Log != null)
//             {
//                 Log("Getting: " + value);
//             }
//
// #endif
//             var result = get(value.Length);
//             memcpy(dst: result, src: value); // 内存拷贝
//             return result;
//         }
//
//         /// <summary>
//         /// 从string创建浅拷贝ZString - 直接引用源字符串而不复制内容
//         /// </summary>
//         /// <param name="value">源字符串，不能为null</param>
//         /// <returns>浅拷贝的ZString实例</returns>
//         /// <exception cref="InvalidOperationException">当不在ZString.Block()作用域内时抛出</exception>
//         /// <remarks>
//         /// 浅拷贝特点：
//         /// - 直接引用源字符串，不进行内存复制，性能更高
//         /// - 节省内存空间，特别适用于大字符串
//         /// - 适用于只读操作，避免修改原始数据
//         /// - 从string隐式转换到ZString时默认使用浅拷贝
//         /// </remarks>
//         private static XString GetShallow(string value)
//         {
//             if (g_current_block == null)
//             {
//                 throw new InvalidOperationException("ZString 操作必须在一个ZString.Block()块中。");
//             }
//
//             XString result;
//             if (g_shallowCache.Count == 0)
//             {
//                 result = new XString(value, true);
//             }
//             else
//             {
//                 result = g_shallowCache.Pop();
//                 result._value = value;
//             }
//
//             result._disposed = false;
//             g_current_block.Push(result); // ZString推入块所在栈
//             return result;
//         }
//
//         /// <summary>
//         /// 字符串驻留 - 将字符串添加到驻留表中以实现字符串去重
//         /// </summary>
//         /// <param name="value">要驻留的字符串</param>
//         /// <returns>驻留后的字符串引用，如果已存在则返回已有引用</returns>
//         /// <remarks>
//         /// 驻留机制：
//         /// - 使用字符串哈希码作为key进行快速查找
//         /// - 如果字符串已存在，直接返回已驻留的引用
//         /// - 如果字符串不存在，创建新实例并添加到驻留表
//         /// - 通过驻留可以减少相同内容字符串的内存占用
//         /// </remarks>
//         private static string __intern(string value)
//         {
//             int hash = value.GetHashCode();
//             if (g_intern_table.ContainsKey(hash))
//             {
//                 return g_intern_table[hash];
//             }
//             else
//             {
//                 string interned = new string(NEW_ALLOC_CHAR, value.Length);
//                 memcpy(interned, value);
//                 g_intern_table.Add(hash, interned);
//                 return interned;
//             }
//         }
//
//         /// <summary>
//         /// 获取指定长度的缓存队列 - 从核心缓存或次级缓存中获取相应的对象池
//         /// </summary>
//         /// <param name="index">字符串长度索引</param>
//         /// <param name="outStack">输出参数，返回对应的ZString队列</param>
//         /// <remarks>
//         /// <para>
//         /// 缓存策略：
//         /// - 优先从核心缓存g_cache中查找（固定长度范围0-127）
//         /// - 如果核心缓存不足，从次级缓存g_secCache中查找
//         /// - 次级缓存不足时自动创建新的队列并缓存
//         /// </para>
//         /// <para>
//         /// 设计原理：
//         /// - 核心缓存：数组访问O(1)，适用于常见长度
//         /// - 次级缓存：字典访问O(1)，适用于长字符串
//         /// - 按需创建：避免内存浪费，提高缓存命中率
//         /// </para>
//         /// </remarks>
//         private static void GetStackInCache(int index, out Queue<XString> outStack)
//         {
//             int length = g_cache.Length;
//             if (length > index) //从核心缓存中取
//             {
//                 outStack = g_cache[index];
//             }
//             else //从次级缓存中取
//             {
//                 if (!g_secCache.TryGetValue(index, out outStack))
//                 {
//                     outStack = new Queue<XString>(INITIAL_STACK_CAPACITY);
//                     g_secCache[index] = outStack;
//                 }
//             }
//         }
//
//         //获取特定长度zstring
//         private static XString get(int length)
//         {
//             if (g_current_block == null || length <= 0)
//             {
//                 throw new InvalidOperationException("zstring 操作必须在一个zstring_block块中。");
//             }
//
//             XString result;
//             Queue<XString> stack;
//             GetStackInCache(length, out stack);
//             //从缓存中取Stack
//             if (stack.Count == 0)
//             {
//                 result = new XString(length);
//             }
//             else
//             {
//                 result = stack.Dequeue();
//             }
//
//             result._disposed = false;
//             g_current_block.Push(result); //zstring推入块所在栈
//             return result;
//         }
//
//         //value是10的次方数
//         private static int get_digit_count(long value)
//         {
//             int cnt;
//             for (cnt = 1; (value /= 10) > 0; cnt++)
//             {
//                 ;
//             }
//
//             return cnt;
//         }
//
//         //value是10的次方数
//         private static uint get_digit_count(uint value)
//         {
//             uint cnt;
//             for (cnt = 1; (value /= 10) > 0; cnt++)
//             {
//                 ;
//             }
//
//             return cnt;
//         }
//
//         //value是10的次方数
//         private static int get_digit_count(int value)
//         {
//             int cnt;
//             for (cnt = 1; (value /= 10) > 0; cnt++)
//             {
//                 ;
//             }
//
//             return cnt;
//         }
//
//         //获取char在input中start起往后的下标
//         private static int internal_index_of(string input, char value, int start)
//         {
//             return internal_index_of(input, value, start, input.Length - start);
//         }
//
//         //获取string在input中起始0的下标
//         private static int internal_index_of(string input, string value)
//         {
//             return internal_index_of(input, value, 0, input.Length);
//         }
//
//         //获取string在input中自0起始下标
//         private static int internal_index_of(string input, string value, int start)
//         {
//             return internal_index_of(input, value, start, input.Length - start);
//         }
//
//         //获取格式化字符串
//         private unsafe static XString internal_format(string input, int num_args)
//         {
//             if (input == null)
//             {
//                 throw new ArgumentNullException("value");
//             }
//
//             //新字符串长度
//             int inputLength = input.Length;
//             for (int i = -3;;)
//             {
//                 i = internal_index_of(input, '{', i + 3);
//                 if (i == -1)
//                 {
//                     break;
//                 }
//
//                 inputLength -= 3;
//                 int argIdx = input[i + 1] - '0';
//                 XString arg = g_format_args[argIdx];
//                 inputLength += arg.Length;
//             }
//
//             XString result = get(inputLength);
//             string resultValue = result._value;
//
//             int nextOutputIdx = 0;
//             int nextInputIdx = 0;
//             int braceIdx = -3;
//             for (int i = 0, j = 0, x = 0;; x++) // x < num_args
//             {
//                 braceIdx = internal_index_of(input, '{', braceIdx + 3);
//                 if (braceIdx == -1)
//                 {
//                     break;
//                 }
//
//                 nextInputIdx = braceIdx;
//                 int argIdx = input[braceIdx + 1] - '0';
//                 string arg = g_format_args[argIdx]._value;
//                 if (braceIdx == -1)
//                 {
//                     throw new InvalidOperationException("没有发现大括号{ for argument " + arg);
//                 }
//
//                 if (braceIdx + 2 >= input.Length || input[braceIdx + 2] != '}')
//                 {
//                     throw new InvalidOperationException("没有发现大括号} for argument " + arg);
//                 }
//
//                 fixed (char* ptrInput = input)
//                 {
//                     fixed (char* ptrResult = resultValue)
//                     {
//                         for (int k = 0; i < inputLength;)
//                         {
//                             if (j < braceIdx)
//                             {
//                                 ptrResult[i++] = ptrInput[j++];
//                                 ++nextOutputIdx;
//                             }
//                             else
//                             {
//                                 ptrResult[i++] = arg[k++];
//                                 ++nextOutputIdx;
//                                 if (k == arg.Length)
//                                 {
//                                     j += 3;
//                                     break;
//                                 }
//                             }
//                         }
//                     }
//                 }
//             }
//
//             nextInputIdx += 3;
//             for (int i = nextOutputIdx, j = 0; i < inputLength; i++, j++)
//             {
//                 fixed (char* ptrInput = input)
//                 {
//                     fixed (char* ptrResult = resultValue)
//                     {
//                         ptrResult[i] = ptrInput[nextInputIdx + j];
//                     }
//                 }
//             }
//
//             return result;
//         }
//
//         //获取char在字符串中start开始的下标
//         private unsafe static int internal_index_of(string input, char value, int start, int count)
//         {
//             if (start < 0 || start >= input.Length)
//                 // throw new ArgumentOutOfRangeException("start");
//             {
//                 return -1;
//             }
//
//             if (start + count > input.Length)
//             {
//                 return -1;
//             }
//             // throw new ArgumentOutOfRangeException("count=" + count + " start+count=" + start + count);
//
//             fixed (char* ptrThis = input)
//             {
//                 int end = start + count;
//                 for (int i = start; i < end; i++)
//                 {
//                     if (ptrThis[i] == value)
//                     {
//                         return i;
//                     }
//                 }
//
//                 return -1;
//             }
//         }
//
//         //获取value在input中自start起始下标
//         private unsafe static int internal_index_of(string input, string value, int start, int count)
//         {
//             int inputLength = input.Length;
//
//             if (start < 0 || start >= inputLength)
//             {
//                 throw new ArgumentOutOfRangeException("start");
//             }
//
//             if (count < 0 || start + count > inputLength)
//             {
//                 throw new ArgumentOutOfRangeException("count=" + count + " start+count=" + (start + count));
//             }
//
//             if (count == 0)
//             {
//                 return -1;
//             }
//
//             fixed (char* ptrInput = input)
//             {
//                 fixed (char* ptrValue = value)
//                 {
//                     int found = 0;
//                     int end = start + count;
//                     for (int i = start; i < end; i++)
//                     {
//                         for (int j = 0; j < value.Length && i + j < inputLength; j++)
//                         {
//                             if (ptrInput[i + j] == ptrValue[j])
//                             {
//                                 found++;
//                                 if (found == value.Length)
//                                 {
//                                     return i;
//                                 }
//
//                                 continue;
//                             }
//
//                             if (found > 0)
//                             {
//                                 break;
//                             }
//                         }
//                     }
//
//                     return -1;
//                 }
//             }
//         }
//
//         //移除string中自start起始count长度子串
//         private static unsafe XString internal_remove(string input, int start, int count)
//         {
//             if (start < 0 || start >= input.Length)
//             {
//                 throw new ArgumentOutOfRangeException("start=" + start + " Length=" + input.Length);
//             }
//
//             if (count < 0 || start + count > input.Length)
//             {
//                 throw new ArgumentOutOfRangeException("count=" + count + " start+count=" + (start + count) + " Length=" + input.Length);
//             }
//
//             if (count == 0)
//             {
//                 return input;
//             }
//
//             XString result = get(input.Length - count);
//             internal_remove(result, input, start, count);
//             return result;
//         }
//
//         //将src中自start起count长度子串复制入dst
//         private unsafe static void internal_remove(string dst, string src, int start, int count)
//         {
//             fixed (char* srcPtr = src)
//             {
//                 fixed (char* dstPtr = dst)
//                 {
//                     for (int i = 0, j = 0; i < dst.Length; i++)
//                     {
//                         if (i >= start && i < start + count) // within removal range
//                         {
//                             continue;
//                         }
//
//                         dstPtr[j++] = srcPtr[i];
//                     }
//                 }
//             }
//         }
//
//         //字符串replace，原字符串，需替换子串，替换的新子串
//         private unsafe static XString internal_replace(string value, string oldValue, string newValue)
//         {
//             // "Hello, World. There World" | World->Jon =
//             // "000000000000000000000" (len = orig - 2 * (world-jon) = orig - 4
//             // "Hello, 00000000000000"
//             // "Hello, Jon00000000000"
//             // "Hello, Jon. There 000"
//             // "Hello, Jon. There Jon"
//
//             // "Hello, World. There World" | World->Alexander =
//             // "000000000000000000000000000000000" (len = orig + 2 * (alexander-world) = orig + 8
//             // "Hello, 00000000000000000000000000"
//             // "Hello, Alexander00000000000000000"
//             // "Hello, Alexander. There 000000000"
//             // "Hello, Alexander. There Alexander"
//
//             if (oldValue == null)
//             {
//                 throw new ArgumentNullException("oldValue");
//             }
//
//             if (newValue == null)
//             {
//                 throw new ArgumentNullException("newValue");
//             }
//
//             int idx = internal_index_of(value, oldValue);
//             if (idx == -1)
//             {
//                 return value;
//             }
//
//             g_finds.Clear();
//             g_finds.Add(idx);
//
//             // 记录所有需要替换的idx点
//             while (idx + oldValue.Length < value.Length)
//             {
//                 idx = internal_index_of(value, oldValue, idx + oldValue.Length);
//                 if (idx == -1)
//                 {
//                     break;
//                 }
//
//                 g_finds.Add(idx);
//             }
//
//             // calc the right new total length
//             int newLen;
//             int dif = oldValue.Length - newValue.Length;
//             if (dif > 0)
//             {
//                 newLen = value.Length - (g_finds.Count * dif);
//             }
//             else
//             {
//                 newLen = value.Length + (g_finds.Count * -dif);
//             }
//
//             XString result = get(newLen);
//             fixed (char* ptrThis = value)
//             {
//                 fixed (char* ptrResult = result._value)
//                 {
//                     for (int i = 0, x = 0, j = 0; i < newLen;)
//                     {
//                         if (x == g_finds.Count || g_finds[x] != j)
//                         {
//                             ptrResult[i++] = ptrThis[j++];
//                         }
//                         else
//                         {
//                             for (int n = 0; n < newValue.Length; n++)
//                             {
//                                 ptrResult[i + n] = newValue[n];
//                             }
//
//                             x++;
//                             i += newValue.Length;
//                             j += oldValue.Length;
//                         }
//                     }
//                 }
//             }
//
//             return result;
//         }
//
//         //向字符串value中自start位置插入count长度的to_insertChar
//         private unsafe static XString internal_insert(string value, char toInsert, int start, int count)
//         {
//             // "HelloWorld" (to_insert=x, start=5, count=3) -> "HelloxxxWorld"
//
//             if (start < 0 || start >= value.Length)
//             {
//                 throw new ArgumentOutOfRangeException("start=" + start + " Length=" + value.Length);
//             }
//
//             if (count < 0)
//             {
//                 throw new ArgumentOutOfRangeException("count=" + count);
//             }
//
//             if (count == 0)
//             {
//                 return Get(value);
//             }
//
//             int newLen = value.Length + count;
//             XString result = get(newLen);
//             fixed (char* ptrValue = value)
//             {
//                 fixed (char* ptrResult = result._value)
//                 {
//                     for (int i = 0, j = 0; i < newLen; i++)
//                     {
//                         if (i >= start && i < start + count)
//                         {
//                             ptrResult[i] = toInsert;
//                         }
//                         else
//                         {
//                             ptrResult[i] = ptrValue[j++];
//                         }
//                     }
//                 }
//             }
//
//             return result;
//         }
//
//         //向input字符串中插入to_insert串，位置为start
//         private static unsafe XString internal_insert(string input, string toInsert, int start)
//         {
//             if (input == null)
//             {
//                 throw new ArgumentNullException("input");
//             }
//
//             if (toInsert == null)
//             {
//                 throw new ArgumentNullException("toInsert");
//             }
//
//             if (start < 0 || start >= input.Length)
//             {
//                 throw new ArgumentOutOfRangeException("start=" + start + " Length=" + input.Length);
//             }
//
//             if (toInsert.Length == 0)
//             {
//                 return Get(input);
//             }
//
//             int newLen = input.Length + toInsert.Length;
//             XString result = get(newLen);
//             internal_insert(result, input, toInsert, start);
//             return result;
//         }
//
//         //字符串拼接
//         private unsafe static XString internal_concat(string s1, string s2)
//         {
//             int totalLength = s1.Length + s2.Length;
//             XString result = get(totalLength);
//             fixed (char* ptrResult = result._value)
//             {
//                 fixed (char* ptrS1 = s1)
//                 {
//                     fixed (char* ptrS2 = s2)
//                     {
//                         memcpy(dst: ptrResult, src: ptrS1, length: s1.Length, srcOffset: 0);
//                         memcpy(dst: ptrResult, src: ptrS2, length: s2.Length, srcOffset: s1.Length);
//                     }
//                 }
//             }
//
//             return result;
//         }
//
//         //将to_insert串插入src的start位置，内容写入dst
//         private unsafe static void internal_insert(string dst, string src, string toInsert, int start)
//         {
//             fixed (char* ptrSrc = src)
//             {
//                 fixed (char* ptrDst = dst)
//                 {
//                     fixed (char* ptrToInsert = toInsert)
//                     {
//                         for (int i = 0, j = 0, k = 0; i < dst.Length; i++)
//                         {
//                             if (i >= start && i < start + toInsert.Length)
//                             {
//                                 ptrDst[i] = ptrToInsert[k++];
//                             }
//                             else
//                             {
//                                 ptrDst[i] = ptrSrc[j++];
//                             }
//                         }
//                     }
//                 }
//             }
//         }
//
//         //将长度为count的数字插入dst中，起始位置为start，dst的长度需大于start+count
//         private static unsafe void longcpy(char* dst, long value, int start, int count)
//         {
//             int end = start + count;
//             for (int i = end - 1; i >= start; i--, value /= 10)
//             {
//                 *(dst + i) = (char)(value % 10 + 48);
//             }
//         }
//
//         //将长度为count的数字插入dst中，起始位置为start，dst的长度需大于start+count
//         private static unsafe void intcpy(char* dst, int value, int start, int count)
//         {
//             int end = start + count;
//             for (int i = end - 1; i >= start; i--, value /= 10)
//             {
//                 *(dst + i) = (char)(value % 10 + 48);
//             }
//         }
//
//         private static unsafe void _memcpy4(byte* dest, byte* src, int size)
//         {
//             /*while (size >= 32) {
//                 // using long is better than int and slower than double
//                 // FIXME: enable this only on correct alignment or on platforms
//                 // that can tolerate unaligned reads/writes of doubles
//                 ((double*)dest) [0] = ((double*)src) [0];
//                 ((double*)dest) [1] = ((double*)src) [1];
//                 ((double*)dest) [2] = ((double*)src) [2];
//                 ((double*)dest) [3] = ((double*)src) [3];
//                 dest += 32;
//                 src += 32;
//                 size -= 32;
//             }*/
//             while (size >= 16)
//             {
//                 ((int*)dest)[0] = ((int*)src)[0];
//                 ((int*)dest)[1] = ((int*)src)[1];
//                 ((int*)dest)[2] = ((int*)src)[2];
//                 ((int*)dest)[3] = ((int*)src)[3];
//                 dest += 16;
//                 src += 16;
//                 size -= 16;
//             }
//
//             while (size >= 4)
//             {
//                 ((int*)dest)[0] = ((int*)src)[0];
//                 dest += 4;
//                 src += 4;
//                 size -= 4;
//             }
//
//             while (size > 0)
//             {
//                 ((byte*)dest)[0] = ((byte*)src)[0];
//                 dest += 1;
//                 src += 1;
//                 --size;
//             }
//         }
//
//         private static unsafe void _memcpy2(byte* dest, byte* src, int size)
//         {
//             while (size >= 8)
//             {
//                 ((short*)dest)[0] = ((short*)src)[0];
//                 ((short*)dest)[1] = ((short*)src)[1];
//                 ((short*)dest)[2] = ((short*)src)[2];
//                 ((short*)dest)[3] = ((short*)src)[3];
//                 dest += 8;
//                 src += 8;
//                 size -= 8;
//             }
//
//             while (size >= 2)
//             {
//                 ((short*)dest)[0] = ((short*)src)[0];
//                 dest += 2;
//                 src += 2;
//                 size -= 2;
//             }
//
//             if (size > 0)
//             {
//                 ((byte*)dest)[0] = ((byte*)src)[0];
//             }
//         }
//         //从src，0位置起始拷贝count长度字符串src到dst中
//         //private unsafe static void memcpy(char* dest, char* src, int count)
//         //{
//         //    // Same rules as for memcpy, but with the premise that 
//         //    // chars can only be aligned to even addresses if their
//         //    // enclosing types are correctly aligned
//
//         //    superMemcpy(dest, src, count);
//         //    //if ((((int)(byte*)dest | (int)(byte*)src) & 3) != 0)//转换为byte指针
//         //    //{
//         //    //    if (((int)(byte*)dest & 2) != 0 && ((int)(byte*)src & 2) != 0 && count > 0)
//         //    //    {
//         //    //        ((short*)dest)[0] = ((short*)src)[0];
//         //    //        dest++;
//         //    //        src++;
//         //    //        count--;
//         //    //    }
//         //    //    if ((((int)(byte*)dest | (int)(byte*)src) & 2) != 0)
//         //    //    {
//         //    //        _memcpy2((byte*)dest, (byte*)src, count * 2);//转换为short*指针一次两个字节拷贝
//         //    //        return;
//         //    //    }
//         //    //}
//         //    //_memcpy4((byte*)dest, (byte*)src, count * 2);//转换为int*指针一次四个字节拷贝
//         //}
//         //--------------------------------------手敲memcpy-------------------------------------//
//         private const int MCharLen = sizeof(char);
//
//         private unsafe static void Memcpy(char* dest, char* src, int count)
//         {
//             byteCopy((byte*)dest, (byte*)src, count * MCharLen);
//         }
//
//         private unsafe static void byteCopy(byte* dest, byte* src, int byteCount)
//         {
//             if (byteCount < 128)
//             {
//                 goto g64;
//             }
//             else if (byteCount < 2048)
//             {
//                 goto g1024;
//             }
//
//             while (byteCount >= 8192)
//             {
//                 ((Byte8192*)dest)[0] = ((Byte8192*)src)[0];
//                 dest += 8192;
//                 src += 8192;
//                 byteCount -= 8192;
//             }
//
//             if (byteCount >= 4096)
//             {
//                 ((Byte4096*)dest)[0] = ((Byte4096*)src)[0];
//                 dest += 4096;
//                 src += 4096;
//                 byteCount -= 4096;
//             }
//
//             if (byteCount >= 2048)
//             {
//                 ((Byte2048*)dest)[0] = ((Byte2048*)src)[0];
//                 dest += 2048;
//                 src += 2048;
//                 byteCount -= 2048;
//             }
//
//             g1024:
//             if (byteCount >= 1024)
//             {
//                 ((Byte1024*)dest)[0] = ((Byte1024*)src)[0];
//                 dest += 1024;
//                 src += 1024;
//                 byteCount -= 1024;
//             }
//
//             if (byteCount >= 512)
//             {
//                 ((Byte512*)dest)[0] = ((Byte512*)src)[0];
//                 dest += 512;
//                 src += 512;
//                 byteCount -= 512;
//             }
//
//             if (byteCount >= 256)
//             {
//                 ((Byte256*)dest)[0] = ((Byte256*)src)[0];
//                 dest += 256;
//                 src += 256;
//                 byteCount -= 256;
//             }
//
//             if (byteCount >= 128)
//             {
//                 ((Byte128*)dest)[0] = ((Byte128*)src)[0];
//                 dest += 128;
//                 src += 128;
//                 byteCount -= 128;
//             }
//
//             g64:
//             if (byteCount >= 64)
//             {
//                 ((Byte64*)dest)[0] = ((Byte64*)src)[0];
//                 dest += 64;
//                 src += 64;
//                 byteCount -= 64;
//             }
//
//             if (byteCount >= 32)
//             {
//                 ((Byte32*)dest)[0] = ((Byte32*)src)[0];
//                 dest += 32;
//                 src += 32;
//                 byteCount -= 32;
//             }
//
//             if (byteCount >= 16)
//             {
//                 ((Byte16*)dest)[0] = ((Byte16*)src)[0];
//                 dest += 16;
//                 src += 16;
//                 byteCount -= 16;
//             }
//
//             if (byteCount >= 8)
//             {
//                 ((Byte8*)dest)[0] = ((Byte8*)src)[0];
//                 dest += 8;
//                 src += 8;
//                 byteCount -= 8;
//             }
//
//             if (byteCount >= 4)
//             {
//                 ((Byte4*)dest)[0] = ((Byte4*)src)[0];
//                 dest += 4;
//                 src += 4;
//                 byteCount -= 4;
//             }
//
//             if (byteCount >= 2)
//             {
//                 ((Byte2*)dest)[0] = ((Byte2*)src)[0];
//                 dest += 2;
//                 src += 2;
//                 byteCount -= 2;
//             }
//
//             if (byteCount >= 1)
//             {
//                 ((Byte1*)dest)[0] = ((Byte1*)src)[0];
//                 dest += 1;
//                 src += 1;
//                 byteCount -= 1;
//             }
//         }
//         //-----------------------------------------------------------------------------------------//
//
//         //将字符串dst用字符src填充
//         private unsafe static void memcpy(string dst, char src)
//         {
//             fixed (char* ptrDst = dst)
//             {
//                 int len = dst.Length;
//                 for (int i = 0; i < len; i++)
//                 {
//                     ptrDst[i] = src;
//                 }
//             }
//         }
//
//         //将字符拷贝到dst指定index位置
//         private unsafe static void memcpy(string dst, char src, int index)
//         {
//             fixed (char* ptr = dst)
//             {
//                 ptr[index] = src;
//             }
//         }
//
//         //将相同长度的src内容拷入dst
//         private unsafe static void memcpy(string dst, string src)
//         {
//             if (dst.Length != src.Length)
//             {
//                 throw new InvalidOperationException("两个字符串参数长度不一致。");
//             }
//
//             fixed (char* dstPtr = dst)
//             {
//                 fixed (char* srcPtr = src)
//                 {
//                     Memcpy(dstPtr, srcPtr, dst.Length);
//                 }
//             }
//         }
//
//         //将src指定length内容拷入dst，dst下标src_offset偏移
//         private unsafe static void memcpy(char* dst, char* src, int length, int srcOffset)
//         {
//             Memcpy(dst + srcOffset, src, length);
//         }
//
//         private unsafe static void memcpy(string dst, string src, int length, int srcOffset)
//         {
//             fixed (char* ptrDst = dst)
//             {
//                 fixed (char* ptrSrc = src)
//                 {
//                     Memcpy(ptrDst + srcOffset, ptrSrc, length);
//                 }
//             }
//         }
//
//         /// <summary>
//         /// ZString作用域块类 - 管理指定作用域内所有ZString的生命周期
//         /// 实现IDisposable接口，支持using语句的自动资源管理
//         /// </summary>
//         public sealed class ZStringBlock : IDisposable
//         {
//             /// <summary>
//             /// ZString栈 - 存储此作用域内创建的所有ZString对象
//             /// 当块被释放时，栈中所有ZString都会被自动释放回对象池
//             /// </summary>
//             readonly Stack<XString> _stack;
//
//             /// <summary>
//             /// 构造函数 - 创建指定容量的ZString作用域块
//             /// </summary>
//             /// <param name="capacity">栈的初始容量，预分配空间以提高性能</param>
//             internal ZStringBlock(int capacity)
//             {
//                 _stack = new Stack<XString>(capacity);
//             }
//
//             /// <summary>
//             /// 将ZString推入作用域栈 - 由ZString内部调用
//             /// 新创建的ZString会自动注册到当前活动块中
//             /// </summary>
//             /// <param name="str">要管理的ZString对象</param>
//             internal void Push(XString str)
//             {
//                 _stack.Push(str);
//             }
//
//             /// <summary>
//             /// 开始作用域 - 初始化块并返回IDisposable引用
//             /// </summary>
//             /// <returns>返回自身作为IDisposable对象</returns>
//             /// <remarks>
//             /// 此方法主要用于调试日志记录，在DEBUG模式下输出块开始信息
//             /// </remarks>
//             internal IDisposable Begin()
//             {
// #if DBG
//                 if (Log != null)
//                     Log("Began block");
// #endif
//                 return this;
//             }
//
//             /// <summary>
//             /// 释放作用域 - 释放块内所有ZString并清理资源
//             /// 由using语句自动调用，或在Dispose()时手动调用
//             /// </summary>
//             /// <remarks>
//             /// <para>
//             /// 释放过程：
//             /// 1. 释放栈中所有ZString对象回相应的对象池
//             /// 2. 将自身推入块缓存栈以供重用
//             /// 3. 更新当前活动块引用以支持嵌套作用域
//             /// </para>
//             /// <para>
//             /// 异常安全：即使部分ZString释放失败，也会继续释放其他对象
//             /// </para>
//             /// </remarks>
//             void IDisposable.Dispose()
//             {
// #if DBG
//                 if (Log != null)
//                     Log("Disposing block");
// #endif
//                 while (_stack.Count > 0)
//                 {
//                     var str = _stack.Pop();
//                     str.dispose(); // 循环调用栈中zstring的Dispose方法
//                 }
//
//                 XString.g_blocks.Push(this); // 将自身push入缓存栈
//
//                 // 更新当前活动块引用
//                 g_open_blocks.Pop();
//                 if (g_open_blocks.Count > 0)
//                 {
//                     XString.g_current_block = g_open_blocks.Peek();
//                 }
//                 else
//                 {
//                     XString.g_current_block = null;
//                 }
//             }
//         }
//
//         // Public API
//
//         #region
//
//         /// <summary>
//         /// 日志回调委托 - 用于调试和日志记录
//         /// 可设置外部日志处理函数，在调试模式下输出ZString操作信息
//         /// </summary>
//         public static Action<string> Log = null;
//
//         /// <summary>
//         /// 浮点数精度 - 控制float转string时小数点后的位数
//         /// 默认值为3，表示保留3位小数。可在运行时调整以满足不同精度需求
//         /// </summary>
//         public static uint DecimalAccuracy = 3;
//
//         /// <summary>
//         /// 字符串长度属性 - 获取当前ZString的字符数量
//         /// </summary>
//         /// <returns>字符串中的字符数量</returns>
//         public int Length
//         {
//             get { return _value.Length; }
//         }
//
//         /// <summary>
//         /// 初始化ZString系统 - 配置对象池的各项参数
//         /// 必须在使用ZString之前调用，通常由静态构造函数自动调用
//         /// </summary>
//         /// <param name="cacheCapacity">核心缓存数组容量，定义支持的最大字符串长度</param>
//         /// <param name="stackCapacity">每个长度对应的缓存队列容量</param>
//         /// <param name="blockCapacity">块缓存栈容量，控制同时可用的zstring_block数量</param>
//         /// <param name="internCapacity">字符串驻留表初始容量</param>
//         /// <param name="openCapacity">同时打开的块数量上限，支持嵌套深度</param>
//         /// <param name="shallowCacheCapacity">浅拷贝缓存栈容量</param>
//         /// <remarks>
//         /// 初始化过程：
//         /// 1. 创建核心缓存数组和次级缓存字典
//         /// 2. 预分配指定数量的ZString对象到各缓存池
//         /// 3. 创建zstring_block对象池
//         /// 4. 初始化字符串驻留表和其他辅助数据结构
//         /// </remarks>
//         public static void Initialize(int cacheCapacity, int stackCapacity, int blockCapacity, int internCapacity, int openCapacity, int shallowCacheCapacity)
//         {
//             g_cache = new Queue<XString>[cacheCapacity];
//             g_secCache = new Dictionary<int, Queue<XString>>(cacheCapacity);
//             g_blocks = new Stack<ZStringBlock>(blockCapacity);
//             g_intern_table = new Dictionary<int, string>(internCapacity);
//             g_open_blocks = new Stack<ZStringBlock>(openCapacity);
//             g_shallowCache = new Stack<XString>(shallowCacheCapacity);
//             for (int c = 0; c < cacheCapacity; c++)
//             {
//                 var stack = new Queue<XString>(stackCapacity);
//                 for (int j = 0; j < stackCapacity; j++)
//                 {
//                     stack.Enqueue(new XString(c));
//                 }
//
//                 g_cache[c] = stack;
//             }
//
//             for (int i = 0; i < blockCapacity; i++)
//             {
//                 var block = new ZStringBlock(blockCapacity * 2);
//                 g_blocks.Push(block);
//             }
//
//             for (int i = 0; i < shallowCacheCapacity; i++)
//             {
//                 g_shallowCache.Push(new XString(null, true));
//             }
//         }
//
//         /// <summary>
//         /// 创建ZString作用域块 - 提供自动化的ZString生命周期管理
//         /// 使用using语句创建作用域，所有在此期间创建的ZString会在作用域结束时自动释放
//         /// </summary>
//         /// <returns>实现了IDisposable的块对象，用于using语句</returns>
//         /// <exception cref="InvalidOperationException">不直接抛出，但内部操作可能会抛出</exception>
//         /// <remarks>
//         /// <para>
//         /// 工作原理：
//         /// 1. 从块缓存池获取一个zstring_block，如果池为空则创建新块
//         /// 2. 将当前块设置为活动块（g_current_block）
//         /// 3. 将块推入开放块栈以支持嵌套使用
//         /// 4. 所有新创建的ZString都会自动注册到当前块中
//         /// </para>
//         /// <para>
//         /// 使用示例：
//         /// <code>
//         /// using (ZString.Block()) {
//         ///     ZString str = "Hello";
//         ///     ZString num = 123;
//         ///     ZString result = str + " " + num;
//         ///     // 所有ZString在作用域结束时自动释放
//         /// }
//         /// </code>
//         /// </para>
//         /// <para>
//         /// 重要特性：
//         /// - 支持嵌套使用，最多支持INITIAL_OPEN_CAPACITY层嵌套
//         /// - 确保异常安全，即使发生异常也能正确释放资源
//         /// - 块对象会被回收重用，提高性能
//         /// </para>
//         /// </remarks>
//         public static IDisposable Block()
//         {
//             if (g_blocks.Count == 0)
//             {
//                 g_current_block = new ZStringBlock(INITIAL_BLOCK_CAPACITY * 2);
//             }
//             else
//             {
//                 g_current_block = g_blocks.Pop();
//             }
//
//             g_open_blocks.Push(g_current_block); // 将此块压入开放栈
//             return g_current_block.Begin();
//         }
//
//         //将zstring value放入intern缓存表中以供外部使用
//         public string Intern()
//         {
//             //string interned = new string(NEW_ALLOC_CHAR, _value.Length);
//             //memcpy(interned, _value);
//             //return interned;
//             return __intern(_value);
//         }
//
//         //将string放入zstring intern缓存表中以供外部使用
//         public static string Intern(string value)
//         {
//             return __intern(value);
//         }
//
//         public static void Intern(string[] values)
//         {
//             for (int i = 0; i < values.Length; i++)
//             {
//                 __intern(values[i]);
//             }
//         }
//
//         //下标取值函数
//         public char this[int i]
//         {
//             get { return _value[i]; }
//             set { memcpy(this, value, i); }
//         }
//
//         //获取hashcode
//         public override int GetHashCode()
//         {
//             return _value.GetHashCode();
//         }
//
//         //字面值比较
//         public override bool Equals(object obj)
//         {
//             if (obj == null)
//             {
//                 return ReferenceEquals(this, null);
//             }
//
//             var gstr = obj as XString;
//             if (gstr != null)
//             {
//                 return gstr._value == this._value;
//             }
//
//             var str = obj as string;
//             if (str != null)
//             {
//                 return str == this._value;
//             }
//
//             return false;
//         }
//
//         //转化为string
//         public override string ToString()
//         {
//             return _value;
//         }
//
//         //bool->zstring转换
//         public static implicit operator XString(bool value)
//         {
//             return Get(value ? "True" : "False");
//         }
//
//         // long - >zstring转换
//         public unsafe static implicit operator XString(long value)
//         {
//             // e.g. 125
//             // first pass: count the number of digits
//             // then: get a zstring with length = num digits
//             // finally: iterate again, get the char of each digit, memcpy char to result
//             bool negative = value < 0;
//             value = Math.Abs(value);
//             int numDigits = get_digit_count(value);
//             XString result;
//             if (negative)
//             {
//                 result = get(numDigits + 1);
//                 fixed (char* ptr = result._value)
//                 {
//                     *ptr = '-';
//                     longcpy(ptr, value, 1, numDigits);
//                 }
//             }
//             else
//             {
//                 result = get(numDigits);
//                 fixed (char* ptr = result._value)
//                 {
//                     longcpy(ptr, value, 0, numDigits);
//                 }
//             }
//
//             return result;
//         }
//
//         //int->zstring转换
//         public unsafe static implicit operator XString(int value)
//         {
//             // e.g. 125
//             // first pass: count the number of digits
//             // then: get a zstring with length = num digits
//             // finally: iterate again, get the char of each digit, memcpy char to result
//             bool negative = value < 0;
//             value = Math.Abs(value);
//             int numDigits = get_digit_count(value);
//             XString result;
//             if (negative)
//             {
//                 result = get(numDigits + 1);
//                 fixed (char* ptr = result._value)
//                 {
//                     *ptr = '-';
//                     intcpy(ptr, value, 1, numDigits);
//                 }
//             }
//             else
//             {
//                 result = get(numDigits);
//                 fixed (char* ptr = result._value)
//                 {
//                     intcpy(ptr, value, 0, numDigits);
//                 }
//             }
//
//             return result;
//         }
//
//         //float->zstring转换
//         public unsafe static implicit operator XString(float value)
//         {
//             // e.g. 3.148
//             bool negative = value < 0;
//             if (negative)
//             {
//                 value = -value;
//             }
//
//             long mul = (long)Math.Pow(10, DecimalAccuracy);
//             long number = (long)(value * mul); // gets the number as a whole, e.g. 3148
//             int leftNum = (int)(number / mul); // left part of the decimal point, e.g. 3
//             int rightNum = (int)(number % mul); // right part of the decimal pnt, e.g. 148
//             int leftDigitCount = get_digit_count(leftNum); // e.g. 1
//             int rightDigitCount = get_digit_count(rightNum); // e.g. 3
//             //int total = left_digit_count + right_digit_count + 1; // +1 for '.'
//             int total = leftDigitCount + (int)DecimalAccuracy + 1; // +1 for '.'
//
//             XString result;
//             if (negative)
//             {
//                 result = get(total + 1); // +1 for '-'
//                 fixed (char* ptr = result._value)
//                 {
//                     *ptr = '-';
//                     intcpy(ptr, leftNum, 1, leftDigitCount);
//                     *(ptr + leftDigitCount + 1) = '.';
//                     int offest = (int)DecimalAccuracy - rightDigitCount;
//                     for (int i = 0; i < offest; i++)
//                     {
//                         *(ptr + leftDigitCount + i + 1) = '0';
//                     }
//
//                     intcpy(ptr, rightNum, leftDigitCount + 2 + offest, rightDigitCount);
//                 }
//             }
//             else
//             {
//                 result = get(total);
//                 fixed (char* ptr = result._value)
//                 {
//                     intcpy(ptr, leftNum, 0, leftDigitCount);
//                     *(ptr + leftDigitCount) = '.';
//                     int offest = (int)DecimalAccuracy - rightDigitCount;
//                     for (int i = 0; i < offest; i++)
//                     {
//                         *(ptr + leftDigitCount + i + 1) = '0';
//                     }
//
//                     intcpy(ptr, rightNum, leftDigitCount + 1 + offest, rightDigitCount);
//                 }
//             }
//
//             return result;
//         }
//
//         //string->zstring转换
//         public static implicit operator XString(string value)
//         {
//             //return get(value);
//             return GetShallow(value);
//         }
//
//         //string->zstring转换
//         public static XString Shallow(string value)
//         {
//             return GetShallow(value);
//         }
//
//         //zstring->string转换
//         public static implicit operator string(XString value)
//         {
//             return value._value;
//         }
//
//         //+重载
//         public static XString operator +(XString left, XString right)
//         {
//             return internal_concat(left, right);
//         }
//
//         //==重载
//         public static bool operator ==(XString left, XString right)
//         {
//             if (ReferenceEquals(left, null))
//             {
//                 return ReferenceEquals(right, null);
//             }
//
//             if (ReferenceEquals(right, null))
//             {
//                 return false;
//             }
//
//             return left._value == right._value;
//         }
//
//         //!=重载
//         public static bool operator !=(XString left, XString right)
//         {
//             return !(left._value == right._value);
//         }
//
//         //转换为大写
//         public unsafe XString ToUpper()
//         {
//             var result = get(Length);
//             fixed (char* ptrThis = this._value)
//             {
//                 fixed (char* ptrResult = result._value)
//                 {
//                     for (int i = 0; i < _value.Length; i++)
//                     {
//                         var ch = ptrThis[i];
//                         if (char.IsLower(ch))
//                         {
//                             ptrResult[i] = char.ToUpper(ch);
//                         }
//                         else
//                         {
//                             ptrResult[i] = ptrThis[i];
//                         }
//                     }
//                 }
//             }
//
//             return result;
//         }
//
//         //转换为小写
//         public unsafe XString ToLower()
//         {
//             var result = get(Length);
//             fixed (char* ptrThis = this._value)
//             {
//                 fixed (char* ptrResult = result._value)
//                 {
//                     for (int i = 0; i < _value.Length; i++)
//                     {
//                         var ch = ptrThis[i];
//                         if (char.IsUpper(ch))
//                         {
//                             ptrResult[i] = char.ToLower(ch);
//                         }
//                         else
//                         {
//                             ptrResult[i] = ptrThis[i];
//                         }
//                     }
//                 }
//             }
//
//             return result;
//         }
//
//         //移除剪切
//         public XString Remove(int start)
//         {
//             return Remove(start, Length - start);
//         }
//
//         //移除剪切
//         public XString Remove(int start, int count)
//         {
//             return internal_remove(this._value, start, count);
//         }
//
//         //插入start起count长度字符
//         public XString Insert(char value, int start, int count)
//         {
//             return internal_insert(this._value, value, start, count);
//         }
//
//         //插入start起字符串
//         public XString Insert(string value, int start)
//         {
//             return internal_insert(this._value, value, start);
//         }
//
//         //子字符替换
//         public unsafe XString Replace(char oldValue, char newValue)
//         {
//             XString result = get(Length);
//             fixed (char* ptrThis = this._value)
//             {
//                 fixed (char* ptrResult = result._value)
//                 {
//                     for (int i = 0; i < Length; i++)
//                     {
//                         ptrResult[i] = ptrThis[i] == oldValue ? newValue : ptrThis[i];
//                     }
//                 }
//             }
//
//             return result;
//         }
//
//         //子字符串替换
//         public XString Replace(string oldValue, string newValue)
//         {
//             return internal_replace(this._value, oldValue, newValue);
//         }
//
//         //剪切start位置起后续子串
//         public XString Substring(int start)
//         {
//             return Substring(start, Length - start);
//         }
//
//         //剪切start起count长度的子串
//         public unsafe XString Substring(int start, int count)
//         {
//             if (start < 0 || start >= Length)
//             {
//                 throw new ArgumentOutOfRangeException("start");
//             }
//
//             if (count > Length)
//             {
//                 throw new ArgumentOutOfRangeException("count");
//             }
//
//             XString result = get(count);
//             fixed (char* src = this._value)
//             {
//                 fixed (char* dst = result._value)
//                 {
//                     Memcpy(dst, src + start, count);
//                 }
//             }
//
//             return result;
//         }
//
//         //子串包含判断
//         public bool Contains(string value)
//         {
//             return IndexOf(value) != -1;
//         }
//
//         //字符包含判断
//         public bool Contains(char value)
//         {
//             return IndexOf(value) != -1;
//         }
//
//         //子串第一次出现位置
//         public int LastIndexOf(string value)
//         {
//             int idx = -1;
//             int lastFind = -1;
//             while (true)
//             {
//                 idx = internal_index_of(this._value, value, idx + value.Length);
//                 lastFind = idx;
//                 if (idx == -1 || idx + value.Length >= this._value.Length)
//                 {
//                     break;
//                 }
//             }
//
//             return lastFind;
//         }
//
//         //字符第一次出现位置
//         public int LastIndexOf(char value)
//         {
//             int idx = -1;
//             int lastFind = -1;
//             while (true)
//             {
//                 idx = internal_index_of(this._value, value, idx + 1);
//                 lastFind = idx;
//                 if (idx == -1 || idx + 1 >= this._value.Length)
//                 {
//                     break;
//                 }
//             }
//
//             return lastFind;
//         }
//
//         //字符第一次出现位置
//         public int IndexOf(char value)
//         {
//             return IndexOf(value, 0, Length);
//         }
//
//         //字符自start起第一次出现位置
//         public int IndexOf(char value, int start)
//         {
//             return internal_index_of(this._value, value, start);
//         }
//
//         //字符自start起count长度内，
//         public int IndexOf(char value, int start, int count)
//         {
//             return internal_index_of(this._value, value, start, count);
//         }
//
//         //子串第一次出现位置
//         public int IndexOf(string value)
//         {
//             return IndexOf(value, 0, Length);
//         }
//
//         //子串自start位置起，第一次出现位置
//         public int IndexOf(string value, int start)
//         {
//             return IndexOf(value, start, Length - start);
//         }
//
//         //子串自start位置起，count长度内第一次出现位置
//         public int IndexOf(string value, int start, int count)
//         {
//             return internal_index_of(this._value, value, start, count);
//         }
//
//         //是否以某字符串结束
//         public unsafe bool EndsWith(string postfix)
//         {
//             if (postfix == null)
//             {
//                 throw new ArgumentNullException("postfix");
//             }
//
//             if (this.Length < postfix.Length)
//             {
//                 return false;
//             }
//
//             fixed (char* ptrThis = this._value)
//             {
//                 fixed (char* ptrPostfix = postfix)
//                 {
//                     for (int i = this._value.Length - 1, j = postfix.Length - 1; j >= 0; i--, j--)
//                     {
//                         if (ptrThis[i] != ptrPostfix[j])
//                         {
//                             return false;
//                         }
//                     }
//                 }
//             }
//
//             return true;
//         }
//
//         //是否以某字符串开始
//         public unsafe bool StartsWith(string prefix)
//         {
//             if (prefix == null)
//             {
//                 throw new ArgumentNullException("prefix");
//             }
//
//             if (this.Length < prefix.Length)
//             {
//                 return false;
//             }
//
//             fixed (char* ptrThis = this._value)
//             {
//                 fixed (char* ptrPrefix = prefix)
//                 {
//                     for (int i = 0; i < prefix.Length; i++)
//                     {
//                         if (ptrThis[i] != ptrPrefix[i])
//                         {
//                             return false;
//                         }
//                     }
//                 }
//             }
//
//             return true;
//         }
//
//         /// <summary>
//         /// 获取指定长度的字符串缓存数量 - 用于调试和性能监控
//         /// </summary>
//         /// <param name="length">要查询的字符串长度</param>
//         /// <returns>指定长度的可用缓存对象数量</returns>
//         /// <remarks>
//         /// 此方法主要用于：
//         /// - 调试对象池的缓存状态
//         /// - 监控内存使用情况
//         /// - 性能分析和优化
//         /// </remarks>
//         public static int GetCacheCount(int length)
//         {
//             Queue<XString> stack;
//             GetStackInCache(length, out stack);
//             return stack.Count;
//         }
//
//         /// <summary>
//         /// 字符串拼接 - 将当前ZString与另一个ZString拼接
//         /// </summary>
//         /// <param name="value">要拼接的ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         /// <remarks>
//         /// 此方法会创建新的ZString对象，原对象保持不变。
//         /// 性能优于string + 操作符，避免了GC分配。
//         /// </remarks>
//         public XString Concat(XString value)
//         {
//             return internal_concat(this, value);
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接两个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         /// <remarks>
//         /// 提供静态方法调用方式，语义更清晰。
//         /// 内部使用 + 操作符实现，保证零GC分配。
//         /// </remarks>
//         public static XString Concat(XString s0, XString s1)
//         {
//             return s0 + s1;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接三个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         public static XString Concat(XString s0, XString s1, XString s2)
//         {
//             return s0 + s1 + s2;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接四个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <param name="s3">第四个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         public static XString Concat(XString s0, XString s1, XString s2, XString s3)
//         {
//             return s0 + s1 + s2 + s3;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接五个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <param name="s3">第四个ZString对象</param>
//         /// <param name="s4">第五个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         public static XString Concat(XString s0, XString s1, XString s2, XString s3, XString s4)
//         {
//             return s0 + s1 + s2 + s3 + s4;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接六个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <param name="s3">第四个ZString对象</param>
//         /// <param name="s4">第五个ZString对象</param>
//         /// <param name="s5">第六个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         public static XString Concat(XString s0, XString s1, XString s2, XString s3, XString s4, XString s5)
//         {
//             return s0 + s1 + s2 + s3 + s4 + s5;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接七个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <param name="s3">第四个ZString对象</param>
//         /// <param name="s4">第五个ZString对象</param>
//         /// <param name="s5">第六个ZString对象</param>
//         /// <param name="s6">第七个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         public static XString Concat(XString s0, XString s1, XString s2, XString s3, XString s4, XString s5, XString s6)
//         {
//             return s0 + s1 + s2 + s3 + s4 + s5 + s6;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接八个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <param name="s3">第四个ZString对象</param>
//         /// <param name="s4">第五个ZString对象</param>
//         /// <param name="s5">第六个ZString对象</param>
//         /// <param name="s6">第七个ZString对象</param>
//         /// <param name="s7">第八个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         public static XString Concat(XString s0, XString s1, XString s2, XString s3, XString s4, XString s5, XString s6, XString s7)
//         {
//             return s0 + s1 + s2 + s3 + s4 + s5 + s6 + s7;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接九个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <param name="s3">第四个ZString对象</param>
//         /// <param name="s4">第五个ZString对象</param>
//         /// <param name="s5">第六个ZString对象</param>
//         /// <param name="s6">第七个ZString对象</param>
//         /// <param name="s7">第八个ZString对象</param>
//         /// <param name="s8">第九个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         public static XString Concat(XString s0, XString s1, XString s2, XString s3, XString s4, XString s5, XString s6, XString s7, XString s8)
//         {
//             return s0 + s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8;
//         }
//
//         /// <summary>
//         /// 静态字符串拼接 - 拼接十个ZString对象
//         /// </summary>
//         /// <param name="s0">第一个ZString对象</param>
//         /// <param name="s1">第二个ZString对象</param>
//         /// <param name="s2">第三个ZString对象</param>
//         /// <param name="s3">第四个ZString对象</param>
//         /// <param name="s4">第五个ZString对象</param>
//         /// <param name="s5">第六个ZString对象</param>
//         /// <param name="s6">第七个ZString对象</param>
//         /// <param name="s7">第八个ZString对象</param>
//         /// <param name="s8">第九个ZString对象</param>
//         /// <param name="s9">第十个ZString对象</param>
//         /// <returns>拼接后的新ZString实例</returns>
//         /// <remarks>
//         /// 这是支持的最大参数数量的Concat方法重载。
//         /// 对于更多参数的拼接，建议使用链式调用或Format方法。
//         /// </remarks>
//         public static XString Concat(XString s0, XString s1, XString s2, XString s3, XString s4, XString s5, XString s6, XString s7, XString s8, XString s9)
//         {
//             return s0 + s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9;
//         }
//
//         /// <summary>
//         /// 静态字符串格式化 - 支持10个参数的字符串格式化
//         /// </summary>
//         /// <param name="input">格式化模板字符串，使用{0}、{1}等占位符</param>
//         /// <param name="arg0">参数0，对应{0}占位符</param>
//         /// <param name="arg1">参数1，对应{1}占位符</param>
//         /// <param name="arg2">参数2，对应{2}占位符</param>
//         /// <param name="arg3">参数3，对应{3}占位符</param>
//         /// <param name="arg4">参数4，对应{4}占位符</param>
//         /// <param name="arg5">参数5，对应{5}占位符</param>
//         /// <param name="arg6">参数6，对应{6}占位符</param>
//         /// <param name="arg7">参数7，对应{7}占位符</param>
//         /// <param name="arg8">参数8，对应{8}占位符</param>
//         /// <param name="arg9">参数9，对应{9}占位符</param>
//         /// <returns>格式化后的新ZString实例</returns>
//         /// <exception cref="ArgumentNullException">当任意参数为null时抛出</exception>
//         /// <remarks>
//         /// <para>
//         /// 格式化规则：
//         /// - 使用数字占位符{0}到{9}，必须按顺序使用
//         /// - 每个占位符必须用对应的}结束，如{0}
//         /// - 占位符不能重复或跳跃
//         /// </para>
//         /// <para>
//         /// 性能特点：
//         /// - 零GC分配，使用对象池技术
//         /// - 性能优于string.Format()
//         /// - 适用于高频格式化场景
//         /// </para>
//         /// <para>
//         /// 使用示例：
//         /// <code>
//         /// ZString result = ZString.Format("用户{0}的分数是{1}", playerName, score);
//         /// // 输出: 用户Alice的分数是100
//         /// </code>
//         /// </para>
//         /// </remarks>
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2, XString arg3, XString arg4, XString arg5, XString arg6, XString arg7, XString arg8, XString arg9)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//             if (arg3 == null)
//             {
//                 throw new ArgumentNullException("arg3");
//             }
//
//             if (arg4 == null)
//             {
//                 throw new ArgumentNullException("arg4");
//             }
//
//             if (arg5 == null)
//             {
//                 throw new ArgumentNullException("arg5");
//             }
//
//             if (arg6 == null)
//             {
//                 throw new ArgumentNullException("arg6");
//             }
//
//             if (arg7 == null)
//             {
//                 throw new ArgumentNullException("arg7");
//             }
//
//             if (arg8 == null)
//             {
//                 throw new ArgumentNullException("arg8");
//             }
//
//             if (arg9 == null)
//             {
//                 throw new ArgumentNullException("arg9");
//             }
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             g_format_args[3] = arg3;
//             g_format_args[4] = arg4;
//             g_format_args[5] = arg5;
//             g_format_args[6] = arg6;
//             g_format_args[7] = arg7;
//             g_format_args[8] = arg8;
//             g_format_args[9] = arg9;
//             return internal_format(input, 10);
//         }
//
//         /// <summary>
//         /// 静态字符串格式化 - 支持9个参数的字符串格式化
//         /// </summary>
//         /// <param name="input">格式化模板字符串，使用{0}到{8}占位符</param>
//         /// <param name="arg0">参数0，对应{0}占位符</param>
//         /// <param name="arg1">参数1，对应{1}占位符</param>
//         /// <param name="arg2">参数2，对应{2}占位符</param>
//         /// <param name="arg3">参数3，对应{3}占位符</param>
//         /// <param name="arg4">参数4，对应{4}占位符</param>
//         /// <param name="arg5">参数5，对应{5}占位符</param>
//         /// <param name="arg6">参数6，对应{6}占位符</param>
//         /// <param name="arg7">参数7，对应{7}占位符</param>
//         /// <param name="arg8">参数8，对应{8}占位符</param>
//         /// <returns>格式化后的新ZString实例</returns>
//         /// <exception cref="ArgumentNullException">当任意参数为null时抛出</exception>
//         /// <seealso cref="Format(string, XString, XString, XString, XString, XString, XString, XString, XString, XString, XString)"/>
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2, XString arg3, XString arg4, XString arg5, XString arg6, XString arg7, XString arg8)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//             if (arg3 == null)
//             {
//                 throw new ArgumentNullException("arg3");
//             }
//
//             if (arg4 == null)
//             {
//                 throw new ArgumentNullException("arg4");
//             }
//
//             if (arg5 == null)
//             {
//                 throw new ArgumentNullException("arg5");
//             }
//
//             if (arg6 == null)
//             {
//                 throw new ArgumentNullException("arg6");
//             }
//
//             if (arg7 == null)
//             {
//                 throw new ArgumentNullException("arg7");
//             }
//
//             if (arg8 == null)
//             {
//                 throw new ArgumentNullException("arg8");
//             }
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             g_format_args[3] = arg3;
//             g_format_args[4] = arg4;
//             g_format_args[5] = arg5;
//             g_format_args[6] = arg6;
//             g_format_args[7] = arg7;
//             g_format_args[8] = arg8;
//             return internal_format(input, 9);
//         }
//
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2, XString arg3, XString arg4, XString arg5, XString arg6, XString arg7)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//             if (arg3 == null)
//             {
//                 throw new ArgumentNullException("arg3");
//             }
//
//             if (arg4 == null)
//             {
//                 throw new ArgumentNullException("arg4");
//             }
//
//             if (arg5 == null)
//             {
//                 throw new ArgumentNullException("arg5");
//             }
//
//             if (arg6 == null)
//             {
//                 throw new ArgumentNullException("arg6");
//             }
//
//             if (arg7 == null)
//             {
//                 throw new ArgumentNullException("arg7");
//             }
//
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             g_format_args[3] = arg3;
//             g_format_args[4] = arg4;
//             g_format_args[5] = arg5;
//             g_format_args[6] = arg6;
//             g_format_args[7] = arg7;
//             return internal_format(input, 8);
//         }
//
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2, XString arg3, XString arg4, XString arg5, XString arg6)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//             if (arg3 == null)
//             {
//                 throw new ArgumentNullException("arg3");
//             }
//
//             if (arg4 == null)
//             {
//                 throw new ArgumentNullException("arg4");
//             }
//
//             if (arg5 == null)
//             {
//                 throw new ArgumentNullException("arg5");
//             }
//
//             if (arg6 == null)
//             {
//                 throw new ArgumentNullException("arg6");
//             }
//
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             g_format_args[3] = arg3;
//             g_format_args[4] = arg4;
//             g_format_args[5] = arg5;
//             g_format_args[6] = arg6;
//             return internal_format(input, 7);
//         }
//
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2, XString arg3, XString arg4, XString arg5)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//             if (arg3 == null)
//             {
//                 throw new ArgumentNullException("arg3");
//             }
//
//             if (arg4 == null)
//             {
//                 throw new ArgumentNullException("arg4");
//             }
//
//             if (arg5 == null)
//             {
//                 throw new ArgumentNullException("arg5");
//             }
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             g_format_args[3] = arg3;
//             g_format_args[4] = arg4;
//             g_format_args[5] = arg5;
//             return internal_format(input, 6);
//         }
//
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2, XString arg3, XString arg4)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//             if (arg3 == null)
//             {
//                 throw new ArgumentNullException("arg3");
//             }
//
//             if (arg4 == null)
//             {
//                 throw new ArgumentNullException("arg4");
//             }
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             g_format_args[3] = arg3;
//             g_format_args[4] = arg4;
//             return internal_format(input, 5);
//         }
//
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2, XString arg3)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//             if (arg3 == null)
//             {
//                 throw new ArgumentNullException("arg3");
//             }
//
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             g_format_args[3] = arg3;
//             return internal_format(input, 4);
//         }
//
//         public static XString Format(string input, XString arg0, XString arg1, XString arg2)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//             if (arg2 == null)
//             {
//                 throw new ArgumentNullException("arg2");
//             }
//
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             g_format_args[2] = arg2;
//             return internal_format(input, 3);
//         }
//
//         public static XString Format(string input, XString arg0, XString arg1)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             if (arg1 == null)
//             {
//                 throw new ArgumentNullException("arg1");
//             }
//
//
//             g_format_args[0] = arg0;
//             g_format_args[1] = arg1;
//             return internal_format(input, 2);
//         }
//
//         public static XString Format(string input, XString arg0)
//         {
//             if (arg0 == null)
//             {
//                 throw new ArgumentNullException("arg0");
//             }
//
//             g_format_args[0] = arg0;
//             return internal_format(input, 1);
//         }
//
//         /// <summary>
//         /// 浮点数转ZString - 指定精度的显式转换方法
//         /// </summary>
//         /// <param name="value">要转换的浮点数值</param>
//         /// <param name="DecimalAccuracy">小数点后的精度位数</param>
//         /// <returns>转换后的ZString实例</returns>
//         /// <remarks>
//         /// <para>
//         /// 与隐式转换的区别：
//         /// - 隐式转换使用全局静态变量DecimalAccuracy（默认3位）
//         /// - 此方法可临时指定精度，不影响全局设置
//         /// - 精度范围建议在0-6之间，避免精度损失
//         /// </para>
//         /// <para>
//         /// 使用场景：
//         /// - 需要特定精度显示（如货币显示2位小数）
//         /// - 临时精度调整而不影响系统全局设置
//         /// - 高精度数据显示需求
//         /// </para>
//         /// <para>
//         /// 使用示例：
//         /// <code>
//         /// ZString price = ZString.FloatToZstring(12.5f, 2);  // "12.50"
//         /// ZString precise = ZString.FloatToZstring(3.14159f, 4);  // "3.1416"
//         /// </code>
//         /// </para>
//         /// </remarks>
//         public static XString FloatToZstring(float value, uint DecimalAccuracy)
//         {
//             uint oldValue = XString.DecimalAccuracy;
//             XString.DecimalAccuracy = DecimalAccuracy;
//             XString target = (XString)value;
//             XString.DecimalAccuracy = oldValue;
//             return target;
//         }
//
//         /// <summary>
//         /// 检查ZString是否为null或空字符串
//         /// </summary>
//         /// <param name="str">要检查的ZString对象</param>
//         /// <returns>如果为null或长度为0则返回true，否则返回false</returns>
//         /// <remarks>
//         /// 等价于string.IsNullOrEmpty()的ZString版本。
//         /// 常用于输入验证和边界条件检查。
//         /// </remarks>
//         /// <seealso cref="string.IsNullOrEmpty"/>
//         public static bool IsNullOrEmpty(XString str)
//         {
//             return str == null || str.Length == 0;
//         }
//
//         /// <summary>
//         /// 检查ZString是否以指定字符串开头
//         /// </summary>
//         /// <param name="str">要检查的ZString对象</param>
//         /// <param name="value">前缀字符串</param>
//         /// <returns>如果以指定字符串开头则返回true，否则返回false</returns>
//         /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
//         /// <remarks>
//         /// 这是StartsWith()方法的静态版本，提供更直观的命名。
//         /// "IsPrefix"命名表示"是否为前缀"，语义更清晰。
//         /// </remarks>
//         /// <seealso cref="XString.StartsWith(string)"/>
//         public static bool IsPrefix(XString str, string value)
//         {
//             return str.StartsWith(value);
//         }
//
//         /// <summary>
//         /// 检查ZString是否以指定字符串结尾
//         /// </summary>
//         /// <param name="str">要检查的ZString对象</param>
//         /// <param name="postfix">后缀字符串</param>
//         /// <returns>如果以指定字符串结尾则返回true，否则返回false</returns>
//         /// <exception cref="ArgumentNullException">当postfix为null时抛出</exception>
//         /// <remarks>
//         /// 这是EndsWith()方法的静态版本，提供更直观的命名。
//         /// "IsPostfix"命名表示"是否为后缀"，与IsPrefix对应。
//         /// </remarks>
//         /// <seealso cref="XString.EndsWith(string)"/>
//         public static bool IsPostfix(XString str, string postfix)
//         {
//             return str.EndsWith(postfix);
//         }
//
//         #endregion
//     }
// }

