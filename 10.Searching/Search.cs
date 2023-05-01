using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searching
{
    internal class Search
    {
        //순차탐색 모든원소를 모두 확인
        public static int SiquentialSearch<T>(in IList<T> list,in T item) where T: IEquatable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (item.Equals(list[i]))
                { 
                    return i;
                }
            }
            return -1;
        }

        //모든 원소가 정렬되어있을때에는 이진탐색을 할수있게된다. (반씩 탐색)
        //list 가 정렬되어 있다고 가정, item 가 같은 값을 가지고있는 인덱스값을 준다.
        public static int BinarySearch<T>(in IList<T> list, in T item) where T : IComparable
        {
            int low = 0;
            int high = list.Count-1;

            while(low <= high)//엇갈리지않을 때 까지 반복
            {
                int mid = (low+high) >> 1; // 나누기 2
                int compare = item.CompareTo(list[mid]);
                   
                if (compare < 0)
                    low = mid + 1;
                else if (compare > 0)
                    high = mid - 1;
                else
                    return mid;
            }
            return -1;//찾는 값이 없다.
        }
    }
}
