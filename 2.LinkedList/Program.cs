namespace _2.LinkedList
{
    internal class Program
    {
        #region Intro Description
        //배열 형식의 데이터 구조는 접근에는 용이하나 삽입&삭제시에는 효율이 떨어진다.
        ///  그래서 연결리스트에 대해 알아보자.

        /*
         * 연결 리스트(Linked List)
         * 종류: 단방향 연결리스트, 원형 연결리스트, 이중(양방향) 연결리스트
         * 데이터를 포함하는 노드들을 연결식으로 만든 자료구조
         * 노드는 데이터와 이전/다음 노드 객체를 참조하고 있음 
         * 노드가 메모리에 연속적으로 배치되지 않고 이전/다음노드의 위치를 확인
         */

        /// null <- [0] <-> [1] <-> [2] <-> [3] .. 이런식으로 참조로 서로를 확인한다.
        /// 주의: 실제 메모리에는 다 따로있다. (위의 형식은 이중연결리스트)
        /// null -> [0] -> [1] -> [2] -> [3] .. <== 단방향 연결리스트

        // 삽입,삭제 작업시 배열 자료구조와 다르게 당겨주는 작업(복사) 을 하지않아도된다.
        // 단, 참조하는 연결 구조를 바꿔줘야함
        #endregion

        //C#에서 구현되있는 LinkedList 사용법
        public static void LinkedList()
        {
            //정의
            LinkedList<string> linkedList = new LinkedList<string>();

            // 링크드리스트 요소 삽입
            linkedList.AddFirst("0번 앞데이터");
            linkedList.AddFirst("1번 앞데이터");
            linkedList.AddLast("0번 뒤데이터");
            linkedList.AddLast("1번 뒤데이터");

            // 링크드리스트 요소 삭제
            linkedList.Remove("1번 앞데이터");

            // 링크드리스트 요소 탐색
            LinkedListNode<string> findNode = linkedList.Find("0번 뒤데이터");

            ///노드란?(Node)
            // Node class => [prev Ref][value][next Ref]
            // 노드 => 이전 노드 의 참조, 데이터 , 다음 노드의 참조
            // 이전,다음 노드가 없을 수 있기때문에 nullable 하다.


            // 링크드리스트 노드를 통한 노드 참조
            LinkedListNode<string> prevNode = findNode.Previous;
            LinkedListNode<string> nextNode = findNode.Next;

            // 링크드리스트 노드를 통한 노드 삽입
            linkedList.AddBefore(findNode, "찾은노드 앞데이터");
            linkedList.AddAfter(findNode, "찾은노드 뒤데이터");

            // 링크드리스트 노드를 통한 삭제
            linkedList.Remove(findNode);
        }

        /// <summary>
        /// LinkedList 는 노드 기반의 자료구조이다.
        /// (Array와 List 는 선형자료구조)

        /// <LinkedList의 시간복잡도>
        // 접근		탐색		삽입		삭제
        // O(n)		O(n)	O(1)	O(1)
        
        //참고: 삭제가 O(1) 인이유는 탐색을 안하고 바로 해당노드를 삭제한다는 가정하에 O(1) 이다.

        /// 참고 : List 의 시간복잡도
        // 접근		탐색		삽입		삭제
        // O(1)		O(n)	O(n)	O(n)

        //연결리스트는 가비지컬렉터(GC)에 많은 부담을준다.
        //(힙영역에 데이터를 마구 뿌리고 정리는 안한다. 그걸 정리하는건 GC가해주니까..)
        //C++에서는 힙영역관리도 본인이 하기때문에 쓸만 하다.
        //(C/C++ 에서는 참조하는 데이터가 null 상태가 되면 Delete 해주면된다.)
        //그래서, C#에서는 사용을 지양하는 성향이 있다... / 다른 노드 기반의 자료구조도 마찬가지이다..


        static void Main(string[] args)
        {
            /// Defalt Linked List Test Code
            //Console.WriteLine("Defalt Linked List");
            //LinkedList();


            ///만든 연결리스트 테스트
            
            DataStructure.LinkedList<string> myList = new DataStructure.LinkedList<string>();

            //요소 뒤에 추가
            myList.AddLast("a");
            myList.AddLast("b");
            myList.AddLast("c");
            myList.AddLast("d");
            myList.AddLast("e");
            
            //첫노드에추가
            myList.AddFirst("Second");
            myList.AddFirst("First");
            //"b" 삭제
            myList.Remove("b");

            // 리스트 요소 탐색
            DataStructure.LinkedListNode<string>? findNode = myList.Find("Second");

            if (findNode != null)
            {
                //찾은 노드 앞뒤에 추가
                myList.AddBefore(findNode, "찾은노드 앞데이터");
                myList.AddAfter(findNode, "찾은노드 뒤데이터");

                //삭제
                myList.Remove(findNode);

                //test code
                myList.Print();
            }


        }
    }
}