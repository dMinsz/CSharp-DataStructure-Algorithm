using System.Text;

namespace BackJoon_15651
{
    //문제 : https://www.acmicpc.net/problem/15651
    internal class Program
    {
        public static int N, M;
        public static StringBuilder sb = new StringBuilder();
        public static int[] arr;
        static void Main(string[] args)
        {
            string[] Question = Console.ReadLine().Split();

            N = int.Parse(Question[0]); // 1~N 까지 조합한다.
            M = int.Parse(Question[1]); // 길이가 M개 인

            arr = new int[M]; // 조합된 수열 담을 배열

            dfs(0); // 무조건 0 ~ M 까지 탐색해야한다.
            Console.WriteLine(sb);

        }

        public static void dfs(int depth) 
        {
            // 재귀 깊이가 M과 같아지면 탐색과정에서 담았던 배열을 출력
            if (depth == M)
            {
                for (int i = 0; i < M; i++)
                {
                    sb.Append(arr[i] + " ");
                }
                sb.AppendLine();
                return;
            }

            for (int i = 1; i <= N; i++)
            {
                arr[depth] = i; // 본인 깊이에 구해진 수 를 넣어둔다.
                dfs(depth + 1); // 더 깊이 들어간다. // 즉 숫자를 하나씩 올리는중
            }
        }
    }
}