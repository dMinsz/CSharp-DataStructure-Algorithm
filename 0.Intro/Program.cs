﻿namespace Intro
{
    internal class Program
    {
        /*
         * 알고리즘 (Algorithm)
         * 
         * 문제 해결을 위해 정해진 절차나 방법
         * 
         * <알고리즘 조건>
         * 1. 입출력: 정해진 입력과 출력이 존재
         * 2. 명확성: 각 단계마다 모호성이 존재하면 안된다.
         * 3. 유한성: 특정 수의 작업 이후에 정지해야 한다.
         * 4. 효과성: 모든 과정은 수행 가능해야 한다.
         * 
         * <알고리즘의 성능>
         * - 알고리즘은 성능에 민감 -
         * 1. 정확성: 정확하게 동작하냐
         * 2. 단순성: 얼마나 단순한가         // 좀더 단순한게 좋은 것
         * 3. 최적성: 더이상 개선할 여지가 없을 만큼 최적화 되있나
         *****
         * 4. 작업량: 얼마나 적은 연산 을 하나
         * 5. 메모리 사용량: 얼마나 적은 메모리를 사용하는가?
         *****
         * 1,2,3번 은 당연하지만 4,5 번은 매우 중요하게 보는 지표 이다.
         */

        /*
         * 자료구조 (Data Structure)
         * 
         * 프로그래밍에서 데이터를 효율적인 접근 및 수정을 가능케 하는 
         * 자료의 조직, 관리, 저장을 의미
         * - 데이터 값들을 잘 보관하고 효율적으로 접근 할 수 있고, 수정 할 수 있게
         *   할 수 있느냐 가 중요 
         * 
         * <자료구조의 형태> : 예시
         * 
         * 선형자료:   자료 간 관계가 1 대 1 인 구조
         *            배열, 연결 리스트, 스택, 큐, 덱
         * 비선형 자료: 자료 간 관계가 (1대 다) 혹은 (다 대 다) 인 구조
         *             트리, 그래프
         */

        /*
         * <알고리즘, 자료구조 평가>
         * 
         * 컴퓨터에서 알고리즘과 자료구조의 평가는 시간과 공간
         * 두 자원을 얼마나 소모하는지가 효율성의 중점
         * 
         * 평균적인 상황에서와 최악의 상황에서 자원 소모량이 기준이 됨
         * *일반적으로 시간을 위해 공간이 희생되는 경우가 많음
         * 시간복잡도 : 알고리즘의 시간적 자원 소모량
         * 공간복잡도 : 알고리즘의 공간적 자원 소모량
         * 
         * 대부분 시간복잡도, 공간복잡도가 평가 대상이다.
         * 
         * <Big-O 표기법>
         * 알고리즘의 복잡도를 나타내는 점근표기법
         * 가장 높은 차수의 계수와 나머지 모든 항을 제거하고 표기
         * 알고리즘의 대략적인 효율을 파악할 수 있는 수단
         */


        // 예시 : 양의 정수 n을 n번 더하는 알고리즘
        int Case1(int n)
        {
            int sum = 0;
            sum = n * n;
            return sum;
        }
        int Case2(int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
                sum += n;
            return sum;
        }
        int Case3(int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    sum++;
            return sum;
        }
        // 입력값		Case1	    Case2	    Case3
        // n = 1		    1	        1	        1
        // n = 10		    1	       10	      100
        // n = 100		    1	      100	   10,000
        // n = 1000		    1	     1000	1,000,000
        // Big-O		 O(1)	     O(n)	   O(n^2)

        // <Good> O(1) > O(logn) > O(n) > O(nlogn) > O(n^2) > O(n^3) > O(2^n) <Bad>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Algorithm and Data Structure");
        }
    }
}