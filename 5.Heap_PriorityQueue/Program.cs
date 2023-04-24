﻿namespace _5.Heap_PriorityQueue
{
    /// 데이터 영역의 힙과 전혀무관한 자료구조이다. 

    /// 힙 (Heap)
    //부모 노드가 항상 자식노드보다 우선순위가 높은 속성을 만족하는 트리기반의 자료구조 
    //많은 자료 중 우선순위가 가장 높은 요소를 빠르게 가져오기 위해 사용

    internal class Program
    {
        static void PriorityQueue()
        {// Heap 을 쓰는 우선순위 큐로 사용해본다.

            //<실제데이터 ,우선순위를 판단할 데이터>
            PriorityQueue<string, int> pq1 = new PriorityQueue<string, int>();

            pq1.Enqueue("Data1", 1);     // 우선순위와 데이터를 추가
            pq1.Enqueue("Data2", 3);
            pq1.Enqueue("Data3", 5);
            pq1.Enqueue("Data4", 2);
            pq1.Enqueue("Data5", 4);


            // 우선순위가 높은 순서대로 데이터 출력
            while (pq1.Count > 0) Console.WriteLine(pq1.Dequeue());


            // 두번째 매개변수는 비교가능한 자료면 상관없다.
            // Comparer를 이용하면
            // 이렇게 내림차순으로 만들수있다.

            //AI 가 이동할시 점수제로 높은게 먼저행동해야할때 사용가능
            PriorityQueue<string, int> pq2
                = new PriorityQueue<string, int>(Comparer<int>.Create((a, b) => b - a));

            pq2.Enqueue("Data1", 1);     // 우선순위와 데이터를 추가
            pq2.Enqueue("Data2", 3);
            pq2.Enqueue("Data3", 5);
            pq2.Enqueue("Data4", 2);
            pq2.Enqueue("Data5", 4);

            while (pq2.Count > 0) Console.WriteLine(pq2.Dequeue()); // 우선순위가 높은 순서대로 데이터 출력


        }

        //트리 기반 자료구조 조건
        // 1. 부모 + 여러 자식을 갖을수있다.(없으면 트리중에 leaf 구조)
        // 2. 자식노드가 부모노드를 갖을수없다.(순환구조가 아니여야한다.)
        // (순환구조까지 갖을수있으면 그래프 구조라한다.)

        // 참고:
        // 트리 종류 => 자식의 최대 갯수가 정해져있으면 , <이진,헥사,옥타> 트리 라고한다.
        // 트리는 선형자료가아니라 비선형 자료구조이다.

        //힙은 2개의 자식을 가질수있는 2진 트리 자료구조이다.

        //Heap 의 특징
        /// <Heap의 시간복잡도>
        // 최우선순위 탐색 ,우선순위 가 가장 높은 노드 삽입, 가장 우선순위가 낮은 노드 삭제
        // 접근		탐색		삽입		    삭제
        // O(n)		O(1)   O(logn)	   O(logn)


        static void Main(string[] args)
        {
            //우선순위큐 테스트
            PriorityQueue();


        }
    }
}