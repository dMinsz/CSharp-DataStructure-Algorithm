using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DivideAndConquer
{
    //비주얼라이징 중... 현재 에러있음
    //분할 정복으로 하노이탑 문제 풀기
    public class Hanoi
    {
        public enum Place { Left,Middle,Right }

        private List<Stack<int>> stick;
      
        int diskCount;
        public Hanoi(int DiskCount) 
        {
            this.diskCount = DiskCount;
            stick = new List<Stack<int>>(3);
            stick.Add(new Stack<int>());
            stick.Add(new Stack<int>());
            stick.Add(new Stack<int>());

            for (int i = DiskCount; i > 0+1; i--)
            {
                stick[0].Push(i);
            }
        }
        public void Run() 
        {
            Console.WriteLine($"{diskCount} 높이의 하노이탑 연산시작~\n");
            //Print();
            Move(diskCount, Place.Left, Place.Middle ,Place.Right);
        }

        public void Move(int count, Place from,Place by, Place to) 
        {
            int disk;
            if (count == 1)
            {
                disk = stick[(int)from].Pop();
                stick[(int)to].Push(disk);

                Print();
                return;
            }

            Move(count - 1, from, to, by);

            disk = stick[(int)from].Pop();
            stick[(int)to].Push(disk);


            Move(count - 1, by, from,to);
        }

        public string DrawDisk(int size) 
        {
            string result = "";
            result += "[";
            for (int i = 0; i < size; i++)
            {
                result += "=";
            }
            result += "]";
            return result;
        }

        public void Print() 
        {
            var leftdisplay = stick[0].ToArray();
            var rightdisplay = stick[1].ToArray();
            var middledisplay = stick[2].ToArray();

            for (int i = 0; i <= diskCount; i++)
            {
                if (i > leftdisplay.Length-1 || leftdisplay.Length-1 < 0)
                {
                    Console.Write(" ");
                }
                else 
                {
                    Console.Write($"{DrawDisk(leftdisplay[i])}");
                }

                if (i > middledisplay.Length - 1 || middledisplay.Length-1 < 0)
                {
                    Console.Write("\t\t\t\t\t\t ");
                }
                else
                {
                    Console.Write($"\t\t\t\t\t\t{DrawDisk(middledisplay[i])}");
                }

                if (i > rightdisplay.Length - 1 || rightdisplay.Length-1 < 0)
                {
                    Console.Write("\t\t\t\t\t\t ");
                }
                else
                {
                    Console.Write($"\t\t\t\t\t\t{DrawDisk(rightdisplay[i])}");
                }

                Console.WriteLine();
            }
        }

    }
}
