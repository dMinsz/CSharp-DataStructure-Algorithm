namespace _5.Heap_PriorityQueue
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
            //C#의 우선순위큐 테스트
            PriorityQueue();

            Console.WriteLine("내가 만든 우선순위큐 테스트");
            
            DataStructure.PriorityQueue<string, int> pq = new DataStructure.PriorityQueue<string,int>();
            pq.Enqueue("Data1", 1);
            pq.Enqueue("Data2", 3);
            pq.Enqueue("Data3", 4);
            pq.Enqueue("Data4", 2);

            Console.WriteLine("우선순위큐 내용 모두 출력");

            while (pq.Count > 0)// 우선순위가 높은 순서대로 데이터 출력
            {
                Console.WriteLine("데이터:{0} 우선순위:{1}",pq.TopElement,pq.TopPriority);
                pq.Dequeue();
            }

            //응급실 시스템 테스트

            Console.WriteLine("\n**************응급실 시스템 테스트**************");
            HeapSystem.EmergencySystem emergencySystem = new HeapSystem.EmergencySystem();

            emergencySystem.AddPatient("강성민", 1);
            emergencySystem.AddPatient("성민강", 3);
            emergencySystem.AddPatient("우석", 5);
            emergencySystem.AddPatient("개발자", 2);

            while (emergencySystem.Count > 0)
            {
                emergencySystem.Healing();
            }
            Console.WriteLine("**************응급실 시스템 테스트**************\n");

            //중간값구하기 테스트
            Console.WriteLine("\n**************중간값 테스트**************");

            int[] ints = new int[10];
            

            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();
                ints[i] = random.Next(0, 10000);
            }

            HeapSystem.MedianPeeker medPeeker = new HeapSystem.MedianPeeker(ints);

            Console.WriteLine("내가찾은 중간값은 :{0}", medPeeker.Peek());
            
            Array.Sort(ints);

            for (int i = 0; i < ints.Length; i++)
            {
                Console.Write(" {0} ", ints[i]);
            }

            Console.WriteLine("중간값은 :{0}", ints[ints.Length/2]);


            // ADD 사용 해서 찾은 중간값
            Console.WriteLine("\n**************ADD 사용 중간값 테스트**************");
            HeapSystem.MedianPeeker medPeeker2 = new HeapSystem.MedianPeeker();
            int size = 10;
            int[] ints2 = new int[size];

            for (int i = 0; i < size; i++)
            {
                Random random = new Random();
                int temp = random.Next(0, 10000);
                medPeeker2.Add(temp);
                ints2[i] = temp; // 보여주기위함
            }

            Console.WriteLine("Add 사용 : 중간값은 :{0}", medPeeker2.Peek());
            
            Array.Sort(ints2);

            for (int i = 0; i < ints2.Length; i++)
            {
                Console.Write(" {0} ", ints2[i]);
            }

            Console.WriteLine("Array 사용 : 중간값은 :{0}", ints2[ints2.Length / 2]);
            Console.WriteLine("\n**************중간값 테스트**************");
        }
    }
}