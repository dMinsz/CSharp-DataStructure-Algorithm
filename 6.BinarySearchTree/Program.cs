using System.Collections.Generic;
using System.Diagnostics;

namespace _6.BinarySearchTree
{
    internal class Program
    {
        #region Intro Description
        //이전에 배운 리스트, 연결리스트 모두 탐색에 O(n) 의 시간복잡도를 갖고있고
        //또한 스택,큐,우선순위 큐는 역할때문에 쓰는것이다.
        //그렇다면 탐색이 유용한 자료구조가 뭐가있을까?

        /* 참고 : 트리 (Tree)
		 * 계층적인 자료를 나타내는데 자주 사용되는 자료구조
		 * 부모노드가 0개 이상의 자식노드들을 가질 수 있음
		 * 한 노드에서 출발하여 다시 자기 자신의 노드로 돌아오는 순환구조를 가질 수 없음
		 * => 순환구조를 가지면 그래프 라는 자료구조가 된다.
		 */

        /// 이진 탐색트리!(BinarySearchTree)
        // 이진속성과 탐색속성을 적용한 트리
        // 이진탐색을 통한 탐색영역을 절반으로 줄여가며 탐색 가능
        // 이진 : 부모노드는 최대 2개의 자식노드들을 가질 수 있음
        // 탐색 : 자신의 노드보다 작은 값들은 왼쪽, 큰 값들은 오른쪽에 위치

        // 주요 알고리즘!
        // 왼쪽 자식 노드에는 부모보다 작은 값, 오른쪽 자식 노드에는 부모 보다 큰값을 넣는다.

        // <이진탐색트리의 시간복잡도> (최선일때)
        // 접근			탐색			삽입			삭제
        // O(log n)		O(log n)	O(log n)	O(log n)

        /// <이진탐색트리의 주의점>
        // 이진탐색트리의 노드들이 한쪽 자식으로만 추가되는 불균형 발생 가능
        // 이 경우 탐색영역이 절반으로 줄여지지 않기 때문에 시간복잡도 증가
        // 이러한 현상을 막기 위해 자가균형기능을 추가한 트리의 사용이 일반적
        // 대표적인 방식으로 Red-Black Tree, AVL Tree 등이 있음

        #endregion

        //이진탐색트리 가 SortedSet으로 구현되어 있다 사용해보자.
        static void BinarySearchTree()
        {
            // value 이진탐색트리
            SortedSet<int> sortedSet = new SortedSet<int>();

            sortedSet.Add(1);
            sortedSet.Add(2);
            sortedSet.Add(3);
            sortedSet.Add(4);
            sortedSet.Add(5);

            int searchValue1;
            sortedSet.TryGetValue(3, out searchValue1);             // 탐색 시도

            // key, value 이진탐색트리
            // key => 정렬의 조건 이된다.
            // value => 실제 데이터
            SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>();

            sortedDictionary.Add(1, "a");
            sortedDictionary.Add(2, "b");
            sortedDictionary.Add(3, "c");
            sortedDictionary.Add(4, "d");
            sortedDictionary.Add(5, "e");

            string? searchValue2;
            sortedDictionary.TryGetValue(3, out searchValue2);      // 탐색 시도
            string indexerValue2 = sortedDictionary[3];             // 인덱서를 통한 탐색


            // 이진탐색 검색효율
            int[] array = new int[10000000];
            SortedSet<int> set = new SortedSet<int>();

            Random random = new Random();
            int rand;

            //랜덤값으로 값을 배열과 SortedSet에 넣어준다.
            for (int i = 0; i < 1000000; i++)
            {
                rand = random.Next();
                array[i] = rand;
                set.Add(rand);
            }
            //마지막값을 -1로 넣는다.
            array[9999999] = -1;
            set.Add(-1);

            //시간 계산을 위한 클래스 선언
            Stopwatch stopwatch = new Stopwatch();

            
            stopwatch.Start();//시간재기
            Array.Find(array, (x) => x == -1); // 마지막에 넣은 값 구하기
            stopwatch.Stop();//시간재기 끝
            Console.WriteLine("배열 time : {0}", stopwatch.ElapsedTicks);//시간 잰거 출력

            stopwatch.Restart();//시간 다시재기
            int value;
            set.TryGetValue(-1, out value); // 마지막에 넣은 값 구하기
            stopwatch.Stop();//시간 재기 끝
            Console.WriteLine("트리 time : {0}", stopwatch.ElapsedTicks);//시간 잰거 출력
        }

        //내가만든 이진탐색 트리 테스트 코드
        static void TestMyBST() 
        {
            DataStructure.BinarySearchTree<int> bst = new DataStructure.BinarySearchTree<int>();

            // 이진탐색 검색 효율 테스트
            int[] array = new int[10000000];
            Random random = new Random();
            int rand;

            //랜덤값으로 값을 배열과  DataStructure.BinarySearchTree에 넣어준다.
            for (int i = 0; i < 1000000; i++)
            {
                rand = random.Next();
                array[i] = rand;
                bst.Add(rand);
            }

            array[9999999] = -1;
            bst.Add(-1); // 마지막값으로 -1 넣어준다.

            //시간 계산을 위한 클래스 선언
            Stopwatch stopwatch = new Stopwatch();


            stopwatch.Start();//시간재기
            Array.Find(array, (x) => x == -1); // 마지막에 넣은 값 구하기
            stopwatch.Stop();//시간재기 끝
            Console.WriteLine("배열 time : {0}", stopwatch.ElapsedTicks);//시간 잰거 출력

            stopwatch.Restart();//시간 다시재기
            int value;
            bst.TryGetValue(-1, out value); // 마지막에 넣은 값 구하기
            stopwatch.Stop();//시간 재기 끝
            Console.WriteLine("내가만든 트리 time : {0}", stopwatch.ElapsedTicks);//시간 잰거 출력

            //중위순회 테스트
            Console.WriteLine("중위순회로 트리 정렬해보기!");
            DataStructure.BinarySearchTree<int> bst2 = new DataStructure.BinarySearchTree<int>();

            bst2.Add(3);
            bst2.Add(1);
            bst2.Add(10);
            bst2.Add(7);
            bst2.Add(8);
            bst2.Add(5);

            bst2.Print();

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Binary Search Tree!\n");

            //C# 자료구조사용
            //BinarySearchTree();

            //내가만든 자료구조 사용 테스트
            //TestMyBST();


            //
            DataStructure.RedBlackTree<int> rdt = new DataStructure.RedBlackTree<int>();

            rdt.Add(3);
            rdt.Add(1);
            rdt.Add(10);
            rdt.Add(7);
            rdt.Add(8);
            rdt.Add(5);
            rdt.Add(-1);

            Console.WriteLine(rdt.PrintTree());
            Console.WriteLine("rdt 찾은거 {0}",rdt.Find(-1).value);
        }

        #region 트리구조의 순회설명
        /*
         * 트리 구조의 순회 순서
         * 
         * 전위순회 => 노드 왼쪽 오른쪽
         * 중위순회 => 왼쪽 노드 오른쪽  <- 이진탐색트리의 순회 / 결과는 오름차순정렬
         * 후위순회 => 왼쪽 오른쪽 노드
         * 
         * 
         * 이진탐색트리는 중위순회를 할때 정확히 오름차순으로 되어있다.
         * 
         * 
         */


        #endregion
    }
}