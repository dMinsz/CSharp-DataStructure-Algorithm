using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorts
{
    
    internal class SortExample
    {
        #region 선형정렬
        ///선형정렬
        // 하나하나 전부 정렬 - 잘안쓰임
        // 1개의 요소를 재위치시키기 위해 전체를 확인하는 정렬
        // n개의 요소를 재위치시키기 위해 n개를 확인하는 정렬
        // 시간복잡도 : O(N^2)

        //left, right 스왑
        private static void Swap(IList<int> list, int left, int right)
        {
            int temp = list[left];
            list[left] = list[right];
            list[right] = temp;
        }

        // <선택정렬>
        // 데이터 중 가장 작은 값부터 하나씩 선택하여 정렬
        // 전체 데이터 중에 가장 작은값 찾아서 맨앞(n)에 놓고
        // 또 (맨앞에 놓은 거빼고 전체)에서 작은값 찾아서 (n+1)번째에 놓고
        // (n+i) 번째 부터 전체 에서 가장작은값 구해서 맨앞에 넣고 이걸 반복
        public static void SelectionSort(IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j] < list[minIndex])
                    {
                        minIndex = j;
                    }
                }
                Swap(list, i, minIndex);
            }
        }
        // <삽입정렬>
        // 데이터를 하나씩 꺼내어 정렬된 자료 중 적합한 위치에 삽입하여 정렬
        public static void InsertionSort(IList<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                int select = list[i];
                int j;
                for (j = i-1; j >=0 && select < list[j]; j--)
                {
                    list[j + 1] = list[j];
                }
                list[j + 1] = select;
            }
        }

        // <버블정렬>
        // 서로 인접한 데이터를 비교하여 정렬
        public static void BubbleSort(IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 1; j < list.Count; j++)
                {
                    if (list[j - 1] > list[j])
                        Swap(list, j - 1, j);
                }
            }
        }
        #endregion
        #region 분할정복 정렬
        /******************************************************
		 * 분할정복 정렬
		 * 
		 * 1개의 요소를 재위치시키기 위해 전체의 1/2를 확인하는 정렬
		 * n개의 요소를 재위치시키기 위해 n/2개를 확인하는 정렬
		 * 시간복잡도 : O(NlogN)
		 ******************************************************/

        //힙정렬
        //우선순위큐를(최소힙) 이용하여 넣고 빼면 자동으로 정렬이된다.
        //힙을 이용하여 우선순위가 가장 높은 요소부터 가져와 정렬

        // 참고:
        // 안정성이 떨어진다. (4 5(1) 5(2) 9 일때 같은숫자인 5 의 위치가 깨질수도있다.)

        // 이상적인 경우에 퀵정렬과 비교했을 때 똑같이 O(NlogN)이 나오긴 하나
        // 실제 시간을 측정하면 분할정복 정렬들 보다 느리다고 한다.
        // <-- 그이유는 선형적인 데이터는 CPU 의 캐시에 올라가서 
        // 반복작업하는게 빠르다!
        // 단 heap도 배열 기반이지만 데이터를 뛰엄뛰엄 탐색하기때문에
        // 일반적인 배열을 탐색할때보다 속도가 느리다.
        public static void HeapSort(IList<int> list) 
        {
            PriorityQueue<int, int> heap = new PriorityQueue<int, int>();

            for (int i = 0; i < list.Count; i++)
            {
                heap.Enqueue(list[i], list[i]);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = heap.Dequeue();
            }

        }
        //병합정렬
        // 데이터를 2분할하여 정렬 후 합병
        // 참고: 메모리 공간을 많이먹어서 메모리가 부족하다면 불리하다.
        // 안정성이 이 좋다 (4 5(1) 5(2) 9 일때 같은숫자인 5 의 위치가 안깨진다.)
        public static void MergeSort(IList<int> list, int left, int right)
        {
            if (left == right) return;

            int mid = (left + right) / 2;
            MergeSort(list, left, mid);
            MergeSort(list, mid + 1, right);
            Merge(list, left, mid, right);
        }

        public static void Merge(IList<int> list, int left, int mid, int right)
        {
            List<int> sortedList = new List<int>();
            int leftIndex = left;
            int rightIndex = mid + 1;

            // 분할 정렬된 List를 병합
            while (leftIndex <= mid && rightIndex <= right)
            {
                if (list[leftIndex] < list[rightIndex])
                    sortedList.Add(list[leftIndex++]);
                else
                    sortedList.Add(list[rightIndex++]);
            }

            if (leftIndex > mid)    // 왼쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = rightIndex; i <= right; i++)
                    sortedList.Add(list[i]);
            }
            else  // 오른쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = leftIndex; i <= mid; i++)
                    sortedList.Add(list[i]);
            }

            // 정렬된 sortedList를 list로 재복사
            for (int i = left; i <= right; i++)
            {
                list[i] = sortedList[i - left];
            }
        }

        // <퀵정렬>
        // 하나의 피벗을 기준으로 작은값과 큰값을 2분할하여 정렬
        // 참고 : 추가적인 메모리를 잡지않기 때문에 메모리적 부담은 적다.
        //       단 최악의 경우에 정렬시간이 오래걸릴수있다. -> 정렬이 거꾸로되어있을때 가 최악이다.
        //       -> 최악의 경우 버블정렬과 다를게 없어진다.
        // 안정성이 떨어진다. (4 5(1) 5(2) 9 일때 같은숫자인 5 의 위치가 깨질수도있다.)
        public static void QuickSort(IList<int> list, int start, int end)
        {
            if (start >= end) return;

            int pivotIndex = start;
            int leftIndex = pivotIndex + 1;
            int rightIndex = end;

            while (leftIndex <= rightIndex) // 엇갈릴때까지 반복
            {
                // pivot보다 큰 값을 만날때까지
                while (list[leftIndex] <= list[pivotIndex] && leftIndex < end)
                    leftIndex++;
                while (list[rightIndex] >= list[pivotIndex] && rightIndex > start)
                    rightIndex--;

                if (leftIndex < rightIndex)     // 엇갈리지 않았다면
                    Swap(list, leftIndex, rightIndex);
                else    // 엇갈렸다면
                    Swap(list, pivotIndex, rightIndex);
            }

            QuickSort(list, start, rightIndex - 1);
            QuickSort(list, rightIndex + 1, end);
        }
        #endregion


    }
}
