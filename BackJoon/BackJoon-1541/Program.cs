namespace BackJoon_1541
{
    //문제 https://www.acmicpc.net/problem/1541
    internal class Program
    {
        //-가 있을 시 -이후의 숫자를 더한 값들을 모두 더해서
        //-값을 크게 만들어주는 편이 최솟값이 나올 것이다.
        static void Main(string[] args)
        {
            string Question = Console.ReadLine();
            string[] plusInput = Question.Split('-'); // - 를 만나면 split된다.


            int result = 0;
            bool flag = false;
            foreach (string strNum in plusInput[0].Split("+")) // 처음 -를 만나기전에 + 된거 다 더해준다.
            {
                result += int.Parse(strNum);
            }

            if (plusInput.Length == 1) //-를 한번도 안만난것
            {
                Console.WriteLine(result);
                return;
            }
            else // -가 하나라도 있는경우
            {
                for (int i = 1; i < plusInput.Length; i++)
                {
                    string[] plusSplitInput = plusInput[i].Split('+');
                    foreach (string strNum in plusSplitInput)
                        result -= int.Parse(strNum); // 다빼준다.
                }
            }

            Console.WriteLine(result);
        }

    }
}