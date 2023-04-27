using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greedy
{
     ///탐욕 알고리즘(Greedy Algorithm)
     // 문제를 해결하는데 사용되는 근시안(짧은시야)적 방법
     // 문제를 직면한 당시에 당장 최적인 답을 선택하는 과정을 반복
     // 단, 반드시 최적의 해를 구해준다는 보장이 없음

    internal static class GreedyTest
    {
        // 예시 - 자판기 거스름돈
        public static void CoinMachine(int money)
        {
            int[] coinType = { 500, 100, 50, 10, 5, 1 };

            foreach (int coin in coinType)
            {
                while (money >= coin)
                {
                    Console.WriteLine("{0} 코인 배출", coin);
                    money -= coin;
                }
            }
        }
        public static void NotGreedyCoinMachine(int money)
        {
            while (money >= 9) // 9이하의 숫자는 거슬러 줄수 없기때문에 9이상의 값에서만 반복을 돌린다.
            {
                if (money >= 500) // 앞선 방식과 같이 큰범위부터 if 를 사용하여 500의 갯수부터 결정지어준다.
                {
                    Console.Write("500원 {0}개", money / 500); // 500원의 갯수를 money 에 나누기 계산하여 세어준다.
                    money %= 500; // 500원의 갯수를 모두 세었으면 나머지연산자를 통해 나머지 값을 다시 money에 저장해준다.
                                  // 다시 반복하여 더 낮은 동전의 갯수를 세기 위함이기도 하다.
                }
                else if (money >= 100) // 100원의 갯수를 결정짓기위한 if
                {
                    Console.Write(", 100원 {0}개", money / 100); // // 100원의 갯수를 money 에 나누기 계산하여 세어준다.
                    money %= 100; // 100원의 갯수를 모두 세었으면 나머지연산자를 통해 나머지 값을 다시 money에 저장해준다. 
                }
                else if (money >= 50) // 위의 설명과 같다
                {
                    Console.Write(", 50원 {0}개 ", money / 50);
                    money %= 50;
                }
                else if (money >= 10)// 위의 설명과 같다
                {
                    Console.Write(", 10원 {0}개", money / 10);
                    money %= 10;
                }
                else //여기에 들어올일이 계산이 제대로되면
                     //없겠지만 혹시나 문제가 생길시 들어오는 곳이다.
                {
                    Console.WriteLine("무언가 잘못되었다..!");
                }
            }
            Console.WriteLine("를 거슬러드렸습니다.");
        }
    }
}
