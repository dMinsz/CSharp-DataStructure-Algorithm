using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructure
{
    /// 엄밀히 말하면 이중연결리스트이다.
    // 일반적인 연결리스트는 다음 노드 의 참조만 가지고있다.

    public class LinkedListNode<T>
    {
        internal LinkedList<T>? list;
        internal LinkedListNode<T>? prev;
        internal LinkedListNode<T>? next;
        private T item;


        public LinkedList<T>? List { get { return list; } }
        public LinkedListNode<T>? Prev { get { return prev; } }
        public LinkedListNode<T>? Next { get { return next; } }
        public T Item { get { return item; } set { item = value; } }

        public LinkedListNode(T value)
        {//생성자, 기본 세팅
            this.list = null;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> list, T value)
        { // 매개변수에 링크드리스트를 추가 했을때
            this.list = list;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T value)
        { // 매개변수로 모든 변수를 세팅할때
            this.list = list;
            this.prev = prev;
            this.next = next;
            this.item = value;
        }
    }

    //디버그에 보여주려고 만듬
    [DebuggerTypeProxy(typeof(LinkedList<>.DebugView))]
    public class LinkedList<T>
    {

        private LinkedListNode<T>? head; // 가장앞에있는 노드
        private LinkedListNode<T>? tail; // 끝에 있는 노드
        private int count; // 총 노드 수

        // 기본 변수에대한 get 함수들
        public LinkedListNode<T>? First { get { return this?.head; } }
        public LinkedListNode<T>? Last { get { return this?.tail; } }
        public int Count { get { return count; } }

        public bool IsReadOnly => throw new NotImplementedException();

        public LinkedList()
        {//기본 생성자
            this.head = null;
            this.tail = null;
            count = 0;
        }

        //node 예외 체크
        private void ValidateNode(LinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException();
            if (node.list != this)
                throw new InvalidOperationException();
        }

        //node 기준으로 이전 위치에 새로운 노드 추가하는 함수
        private void InsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            //노드연결구조바꾸기
            newNode.next = node;
            newNode.prev = node.prev;
            if (newNode.prev != null)
                newNode.prev.next = newNode;

            node.prev = newNode;

            count++;
        }

        //node 기준으로 이후에 새로운 노드를 추가하는 함수
        private void InsertNodeAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            newNode.prev = node;
            newNode.next = node.next;
            if (newNode.next != null)
                newNode.next.prev = newNode;

            node.next = newNode;

            count++;
        }


        //노드가 하나도없는 리스트일대 사용하는 노드 추가함수
        private void InsertNodeToEmptyList(LinkedListNode<T> newNode)
        {
            if (count != 0)
                throw new InvalidOperationException();

            head = newNode;
            tail = newNode;
            count++;
        }

        //처음 위치에 노드 추가
        public LinkedListNode<T> AddFirst(T value)
        {
            //새로운 노드생성
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            if (head != null)
            {
                InsertNodeBefore(head, newNode);
                head = newNode;
            }
            else
            {
                InsertNodeToEmptyList(newNode);
            }
            return newNode;
        }

        //마지막 위치에 노드 추가
        public LinkedListNode<T> AddLast(T value)
        {
            //새로운 노드생성
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            if (tail != null)
            {
                InsertNodeAfter(tail, newNode);
                tail = newNode;
            }
            else
            {
                InsertNodeToEmptyList(newNode);
            }
            return newNode;
        }

        // node 기준으로 "이전에" value값을 데이터를 추가
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            InsertNodeBefore(node, newNode);

            if (node == head)
            {
                head = newNode;
            }

            return newNode;
        }

        //node 기준으로 "이후에/다음에" vlaue 값을 데이터로 추가
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            InsertNodeAfter(node, newNode);

            if (node == tail)
            {
                tail = newNode;
            }

            return newNode;
        }

        //연결리스트 비우기
        public void Clear()
        {

            //리스트에 하나라도 노드가있을시
            LinkedListNode<T>? current = head;
            while (current != null)
            {
                LinkedListNode<T> temp = current;
                current = current.Next;
                temp.list = null;
                temp.next = null;
                temp.prev = null; // 다음 노드에 연결을 모두 끊어버린다.
            }

            this.head = null;
            this.tail = null; // 이미 다음노드의 연결을 모두 끊어서 해줄 필요는 없다.
            count = 0;
        }


        //해당하는 value 가 있는지 확인하는 함수
        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        //해당하는 value 가 있는지 찾아서 node로 반환해주는 함수
        public LinkedListNode<T>? Find(T value)
        {
            LinkedListNode<T>? node = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default; // null 체크를 위해 만듬

            if (value != null)
            {
                while (node != null)
                {
                    if (comparer.Equals(node.Item, value))
                    {
                        return node;
                    }
                    else
                    {
                        node = node.next;
                    }
                }
            }
            else // value 가 null일때
            {
                while (node != null)
                {
                    if (node.Item == null)
                    {
                        return node;
                    }
                    else
                    {
                        node = node.next;
                    }
                }
            }

            return null;
        }

        //노드 기준 노드 삭제 
        public void Remove(LinkedListNode<T> node)
        {
            ValidateNode(node);//node 예외처리 확인

            //node 가 첫노드 혹은 마지막노드일때
            if (head == node)
                head = node.next;
            if (tail == node)
                tail = node.prev;

            //이전 이후 노드 연결 바꿈.
            if (node.next != null)
                node.next.prev = node.prev;
            if (node.prev != null)
                node.prev.next = node.next;

            //굳이 안해도 알아서 GC 가 관리해줌
            //node.next = null;
            //node.prev = null;

            count--;
        }

        //value 값을 찾아서 첫번째 찾아진 노드 삭제
        public void Remove(T value)
        {
            if (value == null)
                throw new ArgumentNullException();


            LinkedListNode<T>? node = Find(value);
            if (node != null)
            {
                //이전 이후 노드 연결 바꿈.
                if (node.next != null)
                    node.next.prev = node.prev;
                if (node.prev != null)
                    node.prev.next = node.next;
            }

            count--;
        }

        // 첫번째 노드 삭제
        public void RemoveFirst()
        {
            if (head == null)
                throw new InvalidOperationException(); // head가 null이면 실행불가.
            LinkedListNode<T> headNode = head;
            Remove(headNode);
        }

        //마지막 노드 삭제
        public void RemoveLast()
        {
            if (tail == null)
                throw new InvalidOperationException(); // head가 null이면 실행불가.
            LinkedListNode<T> tailNode = tail;
            Remove(tailNode);
        }

        //testCode
        public void Print()
        {
            LinkedListNode<T>? node = head;

            if (node == null)
            {
                throw new InvalidOperationException(); // head가 null이면 실행불가.
            }

            //요소가 하나일때
            if (node == tail)
            {
                Console.WriteLine("{0}", node.Item);
                return;
            }

            //요소가 두개이상일때
            while (node != tail)
            {
                Console.Write("{0},", node.Item);
                node = node.Next;
            }

            Console.WriteLine("{0}", node.Item);

        }
        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (array.Length - index < Count)
            {
                throw new ArgumentException();
            }

            LinkedListNode<T>? node = head;
            if (node != null)
            {
                do
                {
                    array[index++] = node!.Item;
                    node = node.next;
                } while (node != head);
            }
        }

        public T[] ToArray() 
        {
            LinkedListNode<T> newNode = head;
            T[] result = new T[this.count];

            if (count <= 0)
            {
                return result; // 카운트가없으면 그냥리턴
            }


            if (newNode == null)
            {
                return result; // head가 null이면 그냥리턴
            }

            for (int i = 0; i < count; i++)
            {
                result[i] = newNode.Item;
                newNode = newNode.next;
            }
           
            return result;
        }

        internal sealed unsafe class DebugView
        {
            private readonly LinkedList<T> _linkedList;

            public DebugView(LinkedList<T> linkedList)
            {
                _linkedList = linkedList;
            }

            public int Count => _linkedList.Count;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public T[] Items
            {
                get
                {
                    ref readonly var linkedList = ref _linkedList;

                    //var count = linkedList.count;
                    //var items = GC.AllocateUninitializedArray<T>(linkedList.count);

                    //linkedList.CopyTo(linkedList, count);

                    var items = linkedList.ToArray();
                    return items;
                }
            }
        }


    }
 



}
