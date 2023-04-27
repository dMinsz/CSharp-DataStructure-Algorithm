using System.Diagnostics;

namespace _8.DesignTechniques
{
    internal class Program
    {
        // 이전에 올린 여러 자료구조도 탐색,접근,삽입,삭제 의 효율이 다르다. 
        // 이러한 선택에도 전략이 필요하다. (정답이없다.)
        ///* 알고리즘 설계기법 *//
        // 어떤 문제를 해결하는 과정에서 해당 문제의 답을 효과적으로 찾아가기 위한 전략과 접근 방식
        // 문제를 풀 때 어떤 알고리즘 설계 기법을 쓰는지에 따라 효율성이 막대하게 차이
        ///* 문제의 성질과 조건에 따라 알맞은 알고리즘 설계기법을 선택하여 사용해야 한다. *//

        static void TestRecursion() 
        {
            Console.WriteLine("\n재귀함수 테스트");
            int fac = 1000;
            //시간 계산을 위한 클래스 선언
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();//재귀함수 사용 팩토리얼 시간재기
            Console.WriteLine($"{fac} 의 팩토리얼은 : {Recursion.RecursionTest.Factorial(fac)}");
            stopwatch.Stop();
            Console.WriteLine("재귀함수 사용시 걸린시간 : {0}", stopwatch.ElapsedTicks);
            
            stopwatch.Restart();//재귀함수가 아닌 팩토리얼 시간재기
            Console.WriteLine($"{fac} 의 팩토리얼은 : {Recursion.RecursionTest.NotRecursionFactorial(fac)}");
            stopwatch.Stop();
            Console.WriteLine("재귀함수 미사용시 걸린시간 : {0}", stopwatch.ElapsedTicks);
        }

        static void TestGreedy() 
        {
            Console.WriteLine("\nGreedy 코인 배출기 테스트");
            int money = 5540;
            Greedy.GreedyTest.CoinMachine(money);
            //Greedy.GreedyTest.NotGreedyCoinMachine(money);
        }

        //static void TestHanoi() 
        //{
        //    DivideAndConquer.Hanoi hanoi = new DivideAndConquer.Hanoi(7);

        //    hanoi.Run();

        //}
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Design Technicques");

            //재귀함수 테스트
            TestRecursion();

            //그리디 테스트
            TestGreedy();

            //하노이미구현
            //TestHanoi();
        }
    }
}