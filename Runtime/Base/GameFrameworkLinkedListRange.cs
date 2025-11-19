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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架链表范围。
    /// </summary>
    /// <typeparam name="T">指定链表范围的元素类型。</typeparam>
    [UnityEngine.Scripting.Preserve]
    [StructLayout(LayoutKind.Auto)]
    public readonly struct GameFrameworkLinkedListRange<T> : IEnumerable<T>, IEnumerable
    {
        private readonly LinkedListNode<T> m_First;
        private readonly LinkedListNode<T> m_Terminal;

        /// <summary>
        /// 初始化游戏框架链表范围的新实例。
        /// </summary>
        /// <param name="first">链表范围的开始结点。</param>
        /// <param name="terminal">链表范围的终结标记结点。</param>
        [UnityEngine.Scripting.Preserve]
        public GameFrameworkLinkedListRange(LinkedListNode<T> first, LinkedListNode<T> terminal)
        {
            if (first == null || terminal == null || first == terminal)
            {
                throw new GameFrameworkException("Range is invalid.");
            }

            m_First = first;
            m_Terminal = terminal;
        }

        /// <summary>
        /// 获取链表范围是否有效。
        /// </summary>
        public bool IsValid
        {
            get { return m_First != null && m_Terminal != null && m_First != m_Terminal; }
        }

        /// <summary>
        /// 获取链表范围的开始结点。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> First
        {
            get { return m_First; }
        }

        /// <summary>
        /// 获取链表范围的终结标记结点。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> Terminal
        {
            get { return m_Terminal; }
        }

        /// <summary>
        /// 获取链表范围的结点数量。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int Count
        {
            get
            {
                if (!IsValid)
                {
                    return 0;
                }

                int count = 0;
                for (LinkedListNode<T> current = m_First; current != null && current != m_Terminal; current = current.Next)
                {
                    count++;
                }

                return count;
            }
        }

        /// <summary>
        /// 检查是否包含指定值。
        /// </summary>
        /// <param name="value">要检查的值。</param>
        /// <returns>是否包含指定值。</returns>
        [UnityEngine.Scripting.Preserve]
        public bool Contains(T value)
        {
            for (LinkedListNode<T> current = m_First; current != null && current != m_Terminal; current = current.Next)
            {
                if (current.Value.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <returns>循环访问集合的枚举数。</returns>
        [UnityEngine.Scripting.Preserve]
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <returns>循环访问集合的枚举数。</returns>
        [UnityEngine.Scripting.Preserve]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <returns>循环访问集合的枚举数。</returns>
        [UnityEngine.Scripting.Preserve]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 循环访问集合的枚举数。
        /// </summary>
        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly GameFrameworkLinkedListRange<T> m_GameFrameworkLinkedListRange;
            private LinkedListNode<T> m_Current;
            private T m_CurrentValue;

            internal Enumerator(GameFrameworkLinkedListRange<T> range)
            {
                if (!range.IsValid)
                {
                    throw new GameFrameworkException("Range is invalid.");
                }

                m_GameFrameworkLinkedListRange = range;
                m_Current = m_GameFrameworkLinkedListRange.m_First;
                m_CurrentValue = default(T);
            }

            /// <summary>
            /// 获取当前结点。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public T Current
            {
                get { return m_CurrentValue; }
            }

            /// <summary>
            /// 获取当前的枚举数。
            /// </summary>
            object IEnumerator.Current
            {
                get { return m_CurrentValue; }
            }

            /// <summary>
            /// 清理枚举数。
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// 获取下一个结点。
            /// </summary>
            /// <returns>返回下一个结点。</returns>
            public bool MoveNext()
            {
                if (m_Current == null || m_Current == m_GameFrameworkLinkedListRange.m_Terminal)
                {
                    return false;
                }

                m_CurrentValue = m_Current.Value;
                m_Current = m_Current.Next;
                return true;
            }

            /// <summary>
            /// 重置枚举数。
            /// </summary>
            void IEnumerator.Reset()
            {
                m_Current = m_GameFrameworkLinkedListRange.m_First;
                m_CurrentValue = default(T);
            }
        }
    }
}