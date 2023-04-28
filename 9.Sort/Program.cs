using System.Diagnostics;

namespace Sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //선형정렬 테스트(버블정렬)
            Stopwatch stopwatch = new Stopwatch();

            List<int> ints = new List<int>(50);
            Random random = new Random();
            for (int i = 0; i < ints.Capacity; i++)
            {
                ints.Add(random.Next(100));
            }

            stopwatch.Start();
            Sorts.SortExample.BubbleSort(ints);
            stopwatch.Stop();
            foreach (int i in ints) 
            {
                Console.Write($" {i} ");
            }
            Console.WriteLine("\n버블정렬 시간:{0}",stopwatch.ElapsedTicks);

            //퀵소트 테스트
            List<int> ints2 = new List<int>(50);
            
            for (int i = 0; i < ints.Capacity; i++)
            {
                ints2.Add(random.Next(100));
            }
            
            stopwatch.Restart();
            Sorts.SortExample.QuickSort(ints2,0,ints2.Count-1);
            stopwatch.Stop();

            foreach (int i in ints2)
            {
                Console.Write($" {i} ");
            }

            Console.WriteLine("\n퀵정렬 시간 :{0}", stopwatch.ElapsedTicks);
        }
    }
}