using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public static class MyFunc<T> 
    {
        //배열과 List 의 버블정렬 함수
        public static void Sort(IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < (list.Count - 1) - i; j++)
                {
                    if (Comparer<T>.Default.Compare(list[j], list[j + 1]) > 0)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp; 
                    }
                }
            }

        }

        //int 형 자료구조의 평균값을 구하는 함수
        public static int Average(ICollection<int> ints)
        {
            int sum = 0;

            foreach (var i in ints) 
            {
                sum += i;
            }

            return sum / ints.Count;
        }


    }
}
