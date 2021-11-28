using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinkedList
{
    public class _LinkedList<T>
    {
        internal LinkedListNode<T> head;
        internal int count;
        internal int version;

        public _LinkedList()
        {
        }

        public _LinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                foreach (var item in collection)
                {
                    AddLast(item);
                }
            }
        }

        public int Count { get { return count; } }
        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get { return head == null ? null : head.prev; } }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> result = new LinkedListNode<T>(node.list, value);
            InternalInsertNodeBefore(node.next, result);
            return result;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> result = new LinkedListNode<T>(node.list, value);
            InternalInsertNodeBefore(node, result);
            if (node == head)
            {
                head = result;
            }
            return result;
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> result = new LinkedListNode<T>(this, value);
            if (head == null)
            {
                InternalInsertNodeToEmptyList(result);
            }
            else
            {
                InternalInsertNodeBefore(head, result);
                head = result;
            }
            return result;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            ValidateNewNode(node);

            if (head == null)
            {
                InternalInsertNodeToEmptyList(node);
            }
            else
            {
                InternalInsertNodeBefore(head, node);
                head = node;
            }
            node.list = this;
        }

        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> result = new LinkedListNode<T>(this, value);
            if (head == null)
            {
                InternalInsertNodeToEmptyList(result);
            }
            else
            {
                InternalInsertNodeBefore(head, result);
            }
            return result;
        }
        public void Clear()
        {
            LinkedListNode<T> current = head;
            while (current != null)
            {
                LinkedListNode<T> temp = current;
                current = current.Next;
                temp.Invalidate();
            }

            head = null;
            count = 0;
            version++;
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> node = head;
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            if (node != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (c.Equals(node.item, value))
                        {
                            return node;
                        }
                        node = node.next;
                    } while (node != head);
                }
                else
                {
                    do
                    {
                        if (node.item == null)
                        {
                            return node;
                        }
                        node = node.next;
                    } while (node != head);
                }
            }
            return null;
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        internal void ValidateNode(LinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.list != this)
            {
                throw new InvalidOperationException();
            }
        }



        public void AddLast(LinkedListNode<T> node)
        {
            ValidateNewNode(node);

            if (head == null)
            {
                InternalInsertNodeToEmptyList(node);
            }
            else
            {
                InternalInsertNodeBefore(head, node);
            }
            node.list = this;
        }

        private void InternalInsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev.next = newNode;
            node.prev = newNode;
            version++;
            count++;
        }


        private void InternalInsertNodeToEmptyList(LinkedListNode<T> newNode)
        {
            newNode.next = newNode;
            newNode.prev = newNode;
            head = newNode;
            version++;
            count++;
        }

        internal void InternalRemoveNode(LinkedListNode<T> node)
        {
            if (node.next == node)
            {
                head = null;
            }
            else
            {
                node.next.prev = node.prev;
                node.prev.next = node.next;
                if (head == node)
                {
                    head = node.next;
                }
            }
            node.Invalidate();
            count--;
            version++;
        }

        internal void ValidateNewNode(LinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (node.list != null)
            {
                throw new InvalidOperationException();
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }


        public struct Enumerator
        {

            private _LinkedList<T> list;
            private LinkedListNode<T> node;
            private int version;
            private T current;
            private int index;

            const string LinkedListName = "LinkedList";
            const string CurrentValueName = "Current";
            const string VersionName = "Version";
            const string IndexName = "Index";

            internal Enumerator(_LinkedList<T> list)
            {
                this.list = list;
                version = list.version;
                node = list.head;
                current = default(T);
                index = 0;
            }

            public T Current
            {
                get { return current; }
            }
            public bool MoveNext()
            {
                if (version != list.version)
                {
                    throw new InvalidOperationException();
                }

                if (node == null)
                {
                    index = list.Count + 1;
                    return false;
                }

                ++index;
                current = node.item;
                node = node.next;
                if (node == list.head)
                {
                    node = null;
                }
                return true;
            }
            void Reset()
            {
                if (version != list.version)
                {
                    throw new InvalidOperationException();
                }

                current = default(T);
                node = list.head;
                index = 0;
            }
        }

        public class LinkedListNode<T>
        {
            internal _LinkedList<T> list;
            internal LinkedListNode<T> next;
            internal LinkedListNode<T> prev;
            internal T item;

            public LinkedListNode(T value)
            {
                item = value;
            }

            internal LinkedListNode(_LinkedList<T> list, T value)
            {
                this.list = list;
                item = value;
            }

            public _LinkedList<T> List { get { return list; } }
            public LinkedListNode<T> Next { get { return next == null || next.Equals(list.head) ? null : next; } }
            public LinkedListNode<T> Previous { get { return prev == null || this.Equals(list.head) ? null : prev; } }
            public T Value
            {
                get { return item; }
                set { item = value; }
            }

            internal void Invalidate()
            {
                list = null;
                next = null;
                prev = null;
            }
        }
    }
}
