namespace _4.StackAndQueue
{
    internal class Program
    {
        // Stack, Queue 자료구조는 사용하는 활용도(역활)때문에 사용.
        // LinkedList, List 는 자료구조의 형태 자체가 중요

        /// Stack 
        // FILO(First in Last out) 선입후출, LIFO(Last in First out) 후입선출 방식의 자료구조
        // 가장 먼저 입력된 데이터 "순서대로" 처리해야하는 상황에 유용
        //
        // 예시: 뒤로가기 (Ctrl+z) , 무르기 기능, 먼저 스킬을 찍어야하는 스킬트리

        ///Queue
        // FIFO(First in First OUT) 선입선출, LILO(후입후출) 방식의 자료구조
        // 가장 먼저 입력된 데이터가 가장먼저 온 데이터가 먼저 처리되어야하는 상황에 유용
        // 순차적인 일에 대해서 사용
        //
        // 예시: 대기열

        /// C#에서 제공하는 스택사용해보기
        public static void TestStack()
        {
            Console.WriteLine("C# Stack 사용해보기 (0~9 까지 순서대로 저장중)");

            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            while (stack.Count > 0)
            {
                Console.Write("{0} ", stack.Pop());
            }
            Console.WriteLine();
        }

        /// C#에서 제공하는 큐사용해보기
        public static void TestQueue()
        {
            Console.WriteLine("C# Queue 사용해보기 (0~9 까지 순서대로 저장중)");

            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }

            while (queue.Count > 0)
            {
                Console.Write("{0} ",queue.Dequeue());
            }
            Console.WriteLine();

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Stack And Queue!");

            //C# stack test code
            TestStack();
            TestQueue();
        }
    }
}