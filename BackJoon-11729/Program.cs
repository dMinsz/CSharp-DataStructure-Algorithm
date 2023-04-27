using System.Text;

namespace BackJoonTest_11729
{   //문제 : https://www.acmicpc.net/problem/11729
    internal class Program
    {
        public static StringBuilder sbResult = new StringBuilder();
        static void Main(string[] args)
        {
            int diskCount = int.Parse(Console.ReadLine());
            int minMove = (int)Math.Pow(2, diskCount) - 1;

            Console.WriteLine(minMove);
            hanoi(diskCount, 1, 2, 3);
            Console.WriteLine(sbResult);
        }
        
        public static void hanoi(int disk, int from , int by, int to) 
        {
            if (disk == 1)
            {
                sbResult.Append(from);
                sbResult.Append(' ');
                sbResult.Append(to);
                sbResult.AppendLine();
            }
            else 
            {
                hanoi(disk-1, from, to, by);
                sbResult.Append(from);
                sbResult.Append(' ');
                sbResult.Append(to);
                sbResult.AppendLine();
                hanoi(disk - 1, by, from, to);
            }
        }
    }
}