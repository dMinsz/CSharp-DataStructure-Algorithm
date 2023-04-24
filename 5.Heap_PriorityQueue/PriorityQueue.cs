using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    #region Heap,Binary tree 설명

    //heap
    //=> 보무노드가 무조건 자식노드보다 우선순위가 높은상태를 힙상태라한다.
    // 힙 자료구조는 2개의 자식을 가질 수 있는 완전 이진 트리 자료구조로 만들 수 있다.
    // heap을이용하여 우선순위큐 만들어보자.
    // (2진 트리는 최소힙, 최대힙이있지만 최소힙으로 만들 예정)

    /*
     * 2진트리를 배열로 만들때 가지는 특성
     *              [0]
     *          [1]     [2]
     *      [3]   [4]  [5]  [6]
     * 각 숫자는 인덱스 이다.
     * 
     * 각 부모의 자식의
     * 왼쪽자식노드 => 부모인덱스 *2 + 1 
     * 오른쪽자식노드 => 부모인덱스 * 2 + 1
     * 부모노드의 인덱스는 => (자식인덱스-1)/ 2
     * 
     * 주의: 비워진 자리가있을 수 있다. // 완전 이진 트리를 이용할것이기때문에 공간은 차지하고있다.
     * 
     * 트리를 노드기반으로 노드들이 자식노드를 가리켜도되지만
     * C# 은 노드기반 자료구조는 GC에 부담을 주기때문에 배열기반의 자료구조로 구현하는게 좋다.
     */

    #endregion
    public class PriorityQueue<TElement, TPriority>
    {
        private struct Node // 각 요소를 노드로 만든다.
        {
            public TElement Element;
            public TPriority Priority;
        }

        private List<Node> nodes; // 리스트의 어댑터 패턴으로 사용하자.
        private IComparer<TPriority> comparer; // comparer 를 이용하면 더 다양한 자료구조를 받을수있다.
                                               // 단! 비교가능(IComparable) 일때


        #region For TestCode
        //testCode 를 위해 만듬
        public TElement TopElement
            {
            get { return this.nodes[0].Element; }
            }

        public TPriority TopPriority
        {
            get { return this.nodes[0].Priority; }
        }
        #endregion

        //기본생성자
        public PriorityQueue()
        {
            this.nodes = new List<Node>();
            this.comparer = Comparer<TPriority>.Default;
        }

        //comparer 를 지정했을 때 생성자
        public PriorityQueue(IComparer<TPriority> comparer)
        {
            this.nodes = new List<Node>();
            this.comparer = comparer;
        }

        public int Count { get { return nodes.Count; } }
        public IComparer<TPriority> Comparer { get { return comparer; } }

        #region Heap 구조 사용에 필요한 메서드 구현

        // 부모노드의 인덱스 가져오기
        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        // 왼쪽 자식의 인덱스 값가져오기
        private int GetLeftChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 1;
        }

        // 오른쪽 자식의 인덱스 값가져오기
        private int GetRightChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 2;
        }

        // Heap 에 데이터 넣기 알고리즘
        // 일단 데이터를 List 에 마지막 인덱스위치에 넣고
        // 우선순위를 확인하여 트리 의 데이터를 바꿔준다.
        // 트리를 반절씩만 탐색하기때문에 속도우위가 있다. (O(Logn))

        //Heap 에 데이터 넣기
        private void PushHeap(Node newNode)
        {
            nodes.Add(newNode);
            int newNodeIndex = nodes.Count - 1; // List에 마지막값으로 일단넣는다.

            //새로운 노드가 힙 상태가되도록 승격 작업을 반복 한다.
            while (newNodeIndex > 0)// 0이면 최상위 노드가 되기때문에 승격작업을 할수가 없다.
            {   // 새로운 인덱스가 첫노드의 인덱스가 (될수있다면)될때까지 돌린다.

                //부모노드 확인
                int parentIndex = GetParentIndex(newNodeIndex);
                Node parentNode = nodes[parentIndex];

                if (comparer.Compare(newNode.Priority, parentNode.Priority) < 0)
                { // 새로운 노드가 부모의 노드 보다 우선순위가 높을때
                    nodes[newNodeIndex] = parentNode;
                    newNodeIndex = parentIndex;
                }
                else
                { // 새로운 노드가 부모의 노드 보다 우선순위가 낮다는건 
                  // 해당 위치가 새로운노드의 위치인게 맞다는것 (우선순위에 맞게 정리됬다는 뜻)
                  // 그래서 반복을 끝내준다.
                    break;
                }
            }
            nodes[newNodeIndex] = newNode;
        }

        ///Heap 에서 삭제 알고리즘
        // 가장 낮은 우선순위 즉 마지막 노드를
        // 첫 노드로 바꾸고 우선순위에 맞춰서 다시 트리를 구성한다.

        //Heap에서 데이터 빼기 // 우선순위가 가장낮은 노드를 삭제 // 마지막 노드를 삭제하는것과 같다.
        private void PopHeap()
        {
            Node lastNode = nodes[nodes.Count - 1]; // 마지막노드
            nodes.RemoveAt(nodes.Count - 1);//마지막 노드를 List에서 삭제

            int index = 0;
            while (index < nodes.Count)
            {// List 의 모든 요소를 체크해서 노드우선순위를 맞춰준다. // 힙상태로 만들어준다.

                int leftChildIndex = GetLeftChildIndex(index);
                int rightChildIndex = GetRightChildIndex(index);

                if (rightChildIndex < nodes.Count) //자식이 둘다있는경우
                {
                    // 완전 2진트리에서 왼쪽 자식이 먼저생기기때문에 오른쪽 자식이있으면 자식이 둘다있는 경우이다.

                    int compareIndex //좌측,우측 자식노드중에 우선순위가 높은걸 찾는다.
                        = comparer.Compare(nodes[leftChildIndex].Priority, nodes[rightChildIndex].Priority) 
                        < 0 ? leftChildIndex : rightChildIndex;

                    if (comparer.Compare(nodes[compareIndex].Priority, lastNode.Priority) < 0)
                    {//선택한 자식노드 와 현재 lastNode 를 우선순위를 비교하여 교체
                        nodes[index] = nodes[compareIndex];
                        index = compareIndex;
                    }
                    else
                    {//선택한 자식노드와 현재 lastNode 의 우선순위를 체크했더니 
                     //현재 부모노드인 lastnode가 우선순위가 높으면 힙상태를 만족하니 반복을 끝낸다.
                        nodes[index] = lastNode;
                        break;
                    }
                }
                else if (leftChildIndex < nodes.Count)//자식이 하나만있는경우 == 왼쪽자식만있는경우
                {
                    if (comparer.Compare(nodes[leftChildIndex].Priority, lastNode.Priority) < 0)
                    {//왼쪽노드의 자식노드 와 현재 lastNode 를 우선순위를 비교하여 교체
                        nodes[index] = nodes[leftChildIndex];
                        index = leftChildIndex;
                    }
                    else
                    {//교체가 필요없을때 반복끝
                        nodes[index] = lastNode;
                        break;
                    }
                }
                else // 자식이 없는 경우
                {
                    //마지막노드에 넣고 반복 끝
                    nodes[index] = lastNode;
                    break;
                }
            }
        }
        #endregion

        #region Heap 을 사용한 Priority Queue 메서드 구현
        //Data 삽입
        public void Enqueue(TElement element, TPriority priority)
        {
            Node newNode = new Node() { Element = element, Priority = priority };

            PushHeap(newNode);
        }

        //우선순위가 가장 높은 노드를 준다. //현재 첫노드를 준다.
        public TElement Peek()
        {
            if (nodes.Count == 0)
                throw new InvalidOperationException();

            return nodes[0].Element;
        }

        //우선순위가 가장 높은 노드를 준다.//첫노드가 있는지 확인하고 있으면 첫노드준다.
        public bool TryPeek(out TElement element, out TPriority priority)
        {
            if (nodes.Count == 0)
            {
                element = default(TElement);
                priority = default(TPriority);
                return false;
            }

            element = nodes[0].Element;
            priority = nodes[0].Priority;
            return true;
        }

        // 데이터 삭제 // 우선순위가 가장 마지막인 데이터를 삭제
        public TElement Dequeue()
        {
            if (nodes.Count == 0) // 리스트에 아무것도없으면 예외처리
                throw new InvalidOperationException();

            Node rootNode = nodes[0];
            PopHeap();
            return rootNode.Element;
        }

        //데이터 삭제 시도
        public bool TryDequeue(out TElement element, out TPriority priority)
        {
            if (nodes.Count == 0)
            {
                element = default(TElement);
                priority = default(TPriority);
                return false;
            }

            Node rootNode = nodes[0]; // dequeue와 같다.
            element = rootNode.Element;
            priority = rootNode.Priority;
            PopHeap();
            return true;
        }

        #endregion

    }
}
