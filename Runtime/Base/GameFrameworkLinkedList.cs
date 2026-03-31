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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架链表类。
    /// </summary>
    /// <remarks>
    /// Game framework linked list class.
    /// </remarks>
    /// <typeparam name="T">指定链表的元素类型 / Specifies the element type of the linked list</typeparam>
    [UnityEngine.Scripting.Preserve]
    public sealed class GameFrameworkLinkedList<T> : ICollection<T>, IEnumerable<T>, ICollection, IEnumerable
    {
        private readonly LinkedList<T> m_LinkedList;
        private readonly Queue<LinkedListNode<T>> m_CachedNodes;

        /// <summary>
        /// 初始化游戏框架链表类的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the game framework linked list class.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public GameFrameworkLinkedList()
        {
            m_LinkedList = new LinkedList<T>();
            m_CachedNodes = new Queue<LinkedListNode<T>>();
        }


        /// <summary>
        /// 获取链表中实际包含的结点数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of nodes actually contained in the linked list.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int Count
        {
            get { return m_LinkedList.Count; }
        }


        /// <summary>
        /// 获取链表结点缓存数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of cached linked list nodes.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int CachedNodeCount
        {
            get { return m_CachedNodes.Count; }
        }


        /// <summary>
        /// 获取链表的第一个结点。
        /// </summary>
        /// <remarks>
        /// Gets the first node of the linked list.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> First
        {
            get { return m_LinkedList.First; }
        }

        /// <summary>
        /// 获取链表的最后一个结点。
        /// </summary>
        /// <remarks>
        /// Gets the last node of the linked list.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> Last
        {
            get { return m_LinkedList.Last; }
        }

        /// <summary>
        /// 获取一个值，该值指示 ICollection`1 是否为只读。
        /// </summary>
        /// <remarks>
        /// Gets a value indicating whether the ICollection`1 is read-only.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsReadOnly
        {
            get { return ((ICollection<T>)m_LinkedList).IsReadOnly; }
        }

        /// <summary>
        /// 获取可用于同步对 ICollection 的访问的对象。
        /// </summary>
        /// <remarks>
        /// Gets an object that can be used to synchronize access to the ICollection.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public object SyncRoot
        {
            get { return ((ICollection)m_LinkedList).SyncRoot; }
        }


        /// <summary>
        /// 获取一个值，该值指示是否同步对 ICollection 的访问（线程安全）。
        /// </summary>
        /// <remarks>
        /// Gets a value indicating whether access to the ICollection is synchronized (thread-safe).
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsSynchronized
        {
            get { return ((ICollection)m_LinkedList).IsSynchronized; }
        }

        /// <summary>
        /// 在链表中指定的现有结点后添加包含指定值的新结点。
        /// </summary>
        /// <remarks>
        /// Adds a new node containing the specified value after the specified existing node in the linked list.
        /// </remarks>
        /// <param name="node">指定的现有结点 / The specified existing node</param>
        /// <param name="value">指定值 / The specified value</param>
        /// <returns>包含指定值的新结点 / The new node containing the specified value</returns>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            m_LinkedList.AddAfter(node, newNode);
            return newNode;
        }

        /// <summary>
        /// 在链表中指定的现有结点后添加指定的新结点。
        /// </summary>
        /// <remarks>
        /// Adds the specified new node after the specified existing node in the linked list.
        /// </remarks>
        /// <param name="node">指定的现有结点 / The specified existing node</param>
        /// <param name="newNode">指定的新结点 / The specified new node</param>
        [UnityEngine.Scripting.Preserve]
        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            m_LinkedList.AddAfter(node, newNode);
        }

        /// <summary>
        /// 在链表中指定的现有结点前添加包含指定值的新结点。
        /// </summary>
        /// <remarks>
        /// Adds a new node containing the specified value before the specified existing node in the linked list.
        /// </remarks>
        /// <param name="node">指定的现有结点 / The specified existing node</param>
        /// <param name="value">指定值 / The specified value</param>
        /// <returns>包含指定值的新结点 / The new node containing the specified value</returns>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            m_LinkedList.AddBefore(node, newNode);
            return newNode;
        }

        /// <summary>
        /// 在链表中指定的现有结点前添加指定的新结点。
        /// </summary>
        /// <remarks>
        /// Adds the specified new node before the specified existing node in the linked list.
        /// </remarks>
        /// <param name="node">指定的现有结点 / The specified existing node</param>
        /// <param name="newNode">指定的新结点 / The specified new node</param>
        [UnityEngine.Scripting.Preserve]
        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            m_LinkedList.AddBefore(node, newNode);
        }

        /// <summary>
        /// 在链表的开头处添加包含指定值的新结点。
        /// </summary>
        /// <remarks>
        /// Adds a new node containing the specified value at the start of the linked list.
        /// </remarks>
        /// <param name="value">指定值 / The specified value</param>
        /// <returns>包含指定值的新结点 / The new node containing the specified value</returns>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            m_LinkedList.AddFirst(node);
            return node;
        }

        /// <summary>
        /// 在链表的开头处添加指定的新结点。
        /// </summary>
        /// <remarks>
        /// Adds the specified new node at the start of the linked list.
        /// </remarks>
        /// <param name="node">指定的新结点 / The specified new node</param>
        [UnityEngine.Scripting.Preserve]
        public void AddFirst(LinkedListNode<T> node)
        {
            m_LinkedList.AddFirst(node);
        }

        /// <summary>
        /// 在链表的结尾处添加包含指定值的新结点。
        /// </summary>
        /// <remarks>
        /// Adds a new node containing the specified value at the end of the linked list.
        /// </remarks>
        /// <param name="value">指定值 / The specified value</param>
        /// <returns>包含指定值的新结点 / The new node containing the specified value</returns>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            m_LinkedList.AddLast(node);
            return node;
        }

        /// <summary>
        /// 在链表的结尾处添加指定的新结点。
        /// </summary>
        /// <remarks>
        /// Adds the specified new node at the end of the linked list.
        /// </remarks>
        /// <param name="node">指定的新结点 / The specified new node</param>
        [UnityEngine.Scripting.Preserve]
        public void AddLast(LinkedListNode<T> node)
        {
            m_LinkedList.AddLast(node);
        }

        /// <summary>
        /// 从链表中移除所有结点。
        /// </summary>
        /// <remarks>
        /// Removes all nodes from the linked list.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void Clear()
        {
            LinkedListNode<T> current = m_LinkedList.First;
            while (current != null)
            {
                ReleaseNode(current);
                current = current.Next;
            }

            m_LinkedList.Clear();
        }

        /// <summary>
        /// 清除链表结点缓存。
        /// </summary>
        /// <remarks>
        /// Clears the linked list node cache.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void ClearCachedNodes()
        {
            m_CachedNodes.Clear();
        }

        /// <summary>
        /// 确定某值是否在链表中。
        /// </summary>
        /// <remarks>
        /// Determines whether a value is in the linked list.
        /// </remarks>
        /// <param name="value">指定值 / The specified value</param>
        /// <returns>某值是否在链表中 / Whether the value is in the linked list</returns>
        [UnityEngine.Scripting.Preserve]
        public bool Contains(T value)
        {
            return m_LinkedList.Contains(value);
        }

        /// <summary>
        /// 从目标数组的指定索引处开始将整个链表复制到兼容的一维数组。
        /// </summary>
        /// <remarks>
        /// Copies the entire linked list to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </remarks>
        /// <param name="array">一维数组，它是从链表复制的元素的目标。数组必须具有从零开始的索引。 / The one-dimensional array that is the destination of the elements copied from the linked list. The array must have zero-based indexing</param>
        /// <param name="index">array 中从零开始的索引，从此处开始复制。 / The zero-based index in array at which copying begins</param>
        [UnityEngine.Scripting.Preserve]
        public void CopyTo(T[] array, int index)
        {
            m_LinkedList.CopyTo(array, index);
        }

        /// <summary>
        /// 从特定的 ICollection 索引开始，将数组的元素复制到一个数组中。
        /// </summary>
        /// <remarks>
        /// Copies the elements of the ICollection to an array, starting at a particular ICollection index.
        /// </remarks>
        /// <param name="array">一维数组，它是从 ICollection 复制的元素的目标。数组必须具有从零开始的索引。 / The one-dimensional array that is the destination of the elements copied from the ICollection. The array must have zero-based indexing</param>
        /// <param name="index">array 中从零开始的索引，从此处开始复制。 / The zero-based index in array at which copying begins</param>
        [UnityEngine.Scripting.Preserve]
        public void CopyTo(Array array, int index)
        {
            ((ICollection)m_LinkedList).CopyTo(array, index);
        }

        /// <summary>
        /// 查找包含指定值的第一个结点。
        /// </summary>
        /// <remarks>
        /// Finds the first node that contains the specified value.
        /// </remarks>
        /// <param name="value">要查找的指定值 / The specified value to find</param>
        /// <returns>包含指定值的第一个结点 / The first node containing the specified value</returns>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> Find(T value)
        {
            return m_LinkedList.Find(value);
        }

        /// <summary>
        /// 查找包含指定值的最后一个结点。
        /// </summary>
        /// <remarks>
        /// Finds the last node that contains the specified value.
        /// </remarks>
        /// <param name="value">要查找的指定值 / The specified value to find</param>
        /// <returns>包含指定值的最后一个结点 / The last node containing the specified value</returns>
        [UnityEngine.Scripting.Preserve]
        public LinkedListNode<T> FindLast(T value)
        {
            return m_LinkedList.FindLast(value);
        }

        /// <summary>
        /// 从链表中移除指定值的第一个匹配项。
        /// </summary>
        /// <remarks>
        /// Removes the first occurrence of the specified value from the linked list.
        /// </remarks>
        /// <param name="value">指定值 / The specified value</param>
        /// <returns>是否移除成功 / Whether the removal was successful</returns>
        [UnityEngine.Scripting.Preserve]
        public bool Remove(T value)
        {
            LinkedListNode<T> node = m_LinkedList.Find(value);
            if (node != null)
            {
                m_LinkedList.Remove(node);
                ReleaseNode(node);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 从链表中移除指定的结点。
        /// </summary>
        /// <remarks>
        /// Removes the specified node from the linked list.
        /// </remarks>
        /// <param name="node">指定的结点 / The specified node</param>
        [UnityEngine.Scripting.Preserve]
        public void Remove(LinkedListNode<T> node)
        {
            m_LinkedList.Remove(node);
            ReleaseNode(node);
        }

        /// <summary>
        /// 移除位于链表开头处的结点。
        /// </summary>
        /// <remarks>
        /// Removes the node at the start of the linked list.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void RemoveFirst()
        {
            LinkedListNode<T> first = m_LinkedList.First;
            if (first == null)
            {
                throw new GameFrameworkException("First is invalid.");
            }

            m_LinkedList.RemoveFirst();
            ReleaseNode(first);
        }

        /// <summary>
        /// 移除位于链表结尾处的结点。
        /// </summary>
        /// <remarks>
        /// Removes the node at the end of the linked list.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void RemoveLast()
        {
            LinkedListNode<T> last = m_LinkedList.Last;
            if (last == null)
            {
                throw new GameFrameworkException("Last is invalid.");
            }

            m_LinkedList.RemoveLast();
            ReleaseNode(last);
        }

        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <remarks>
        /// Returns an enumerator that iterates through the collection.
        /// </remarks>
        /// <returns>循环访问集合的枚举数 / An enumerator that iterates through the collection</returns>
        [UnityEngine.Scripting.Preserve]
        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_LinkedList);
        }

        private LinkedListNode<T> AcquireNode(T value)
        {
            LinkedListNode<T> node = null;
            if (m_CachedNodes.Count > 0)
            {
                node = m_CachedNodes.Dequeue();
                node.Value = value;
            }
            else
            {
                node = new LinkedListNode<T>(value);
            }

            return node;
        }

        private void ReleaseNode(LinkedListNode<T> node)
        {
            node.Value = default(T);
            m_CachedNodes.Enqueue(node);
        }

        /// <summary>
        /// 将值添加到 ICollection`1 的结尾处。
        /// </summary>
        /// <remarks>
        /// Adds a value to the end of the ICollection`1.
        /// </remarks>
        /// <param name="value">要添加的值 / The value to add</param>
        void ICollection<T>.Add(T value)
        {
            AddLast(value);
        }

        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <remarks>
        /// Returns an enumerator that iterates through the collection.
        /// </remarks>
        /// <returns>循环访问集合的枚举数 / An enumerator that iterates through the collection</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <remarks>
        /// Returns an enumerator that iterates through the collection.
        /// </remarks>
        /// <returns>循环访问集合的枚举数 / An enumerator that iterates through the collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 循环访问集合的枚举数。
        /// </summary>
        /// <remarks>
        /// An enumerator that iterates through the collection.
        /// </remarks>
        [StructLayout(LayoutKind.Auto)]
        [UnityEngine.Scripting.Preserve]
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private LinkedList<T>.Enumerator m_Enumerator;

            internal Enumerator(LinkedList<T> linkedList)
            {
                if (linkedList == null)
                {
                    throw new GameFrameworkException("Linked list is invalid.");
                }

                m_Enumerator = linkedList.GetEnumerator();
            }

            /// <summary>
            /// 获取当前结点。
            /// </summary>
            /// <remarks>
            /// Gets the current node.
            /// </remarks>
            public T Current
            {
                get { return m_Enumerator.Current; }
            }

            /// <summary>
            /// 获取当前的枚举数。
            /// </summary>
            /// <remarks>
            /// Gets the current enumerator.
            /// </remarks>
            object IEnumerator.Current
            {
                get { return m_Enumerator.Current; }
            }

            /// <summary>
            /// 清理枚举数。
            /// </summary>
            /// <remarks>
            /// Disposes the enumerator.
            /// </remarks>
            public void Dispose()
            {
                m_Enumerator.Dispose();
            }

            /// <summary>
            /// 获取下一个结点。
            /// </summary>
            /// <remarks>
            /// Gets the next node.
            /// </remarks>
            /// <returns>返回下一个结点 / Returns the next node</returns>
            public bool MoveNext()
            {
                return m_Enumerator.MoveNext();
            }

            /// <summary>
            /// 重置枚举数。
            /// </summary>
            /// <remarks>
            /// Resets the enumerator.
            /// </remarks>
            void IEnumerator.Reset()
            {
                ((IEnumerator<T>)m_Enumerator).Reset();
            }
        }
    }
}