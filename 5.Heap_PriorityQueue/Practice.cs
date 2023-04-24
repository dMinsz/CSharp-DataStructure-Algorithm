using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSystem
{
    #region 응급실 우선순위 치료시스템
    //응급실 우선순위 치료 시스템
    //골든타임이 짧은 순서대로 치료
    public class EmergencySystem
    {
        private DataStructure.PriorityQueue<string, int> patientList = new DataStructure.PriorityQueue<string, int>();

        public int Count { get { return patientList.Count; } }

        public void AddPatient(string name, int goldenTime)
        {
            patientList.Enqueue(name, goldenTime);
        }

        public void Healing()
        {
            if (patientList.Count < 0)
            {
                Console.WriteLine("환자가 없습니다.");
                return;
            }

            Console.WriteLine("{0} 환자 치료 했습니다 (우선순위: {1})", patientList.TopElement, patientList.TopPriority);
            patientList.Dequeue();
        }
    }
    #endregion

    #region 많은 데이터 중 중간 값 구하기

    /*  
    중간 값 구하기 알고리즘
    1. 최대 힙의 크기는 최소 힙의 크기와 같거나, 하나 더 크다.
    2. 최대 힙의 최대 원소는 최소 합의 최소 원소보다 작거나 같다.
    3. 이때 알고리즘에 맞지 않다면 최대 힙, 최소 힙의 가장 위의 값을 swap해준다.
    */

    /*
     1. 최대 힙과 최소 힙을 준비한다.
     2. 첫 번째 수를 중앙값으로 한다.
     3. 두 번째부터 입력받은 수를 중앙값과 비교해서 작으면 최대 힙, 크면 최소 힙에 push한다.
     4. 홀수 번째라면 최대 힙과 최소 힙 중에 사이즈가 작은 힙에 중앙값을 push 하고 그리고 힙 중에 사이즈가 큰 힙의 top을 중앙값으로 한다.
     */

    public class MedianPeeker
    {
        //1. 최대 힙과 최소 힙을 준비한다.
        private PriorityQueue<int, int > minHeap
              = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => a - b));

        private PriorityQueue<int, int> maxHeap
              = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));

        private int median;
        private int count;
        public MedianPeeker()
        {
        }
        public MedianPeeker(IList<int> datas)
        {
            if (datas.Count < 0)
            {
                throw new InvalidOperationException();
            }

            IEnumerator<int> enumerator = datas.GetEnumerator();
            enumerator.MoveNext();

            median = enumerator.Current; //2.첫 번째 수를 중앙값으로 한다.
            count++;

            for (int i = 1; i < datas.Count; i++)
            { //  3. 두 번째부터 입력받은 수를 중앙값과 비교해서 작으면 최대 힙, 크면 최소 힙에 push한다.
                if (datas[i] < median)
                {
                    maxHeap.Enqueue(datas[i], datas[i]);
                }
                else
                {
                    minHeap.Enqueue(datas[i], datas[i]);
                }


                if (i % 2 == 1)
                { //4.1홀수 번째 수를 읽을 때 마다

                    if (maxHeap.Count < minHeap.Count)
                    {//4.2최대 힙과 최소 힙 중에 사이즈가 작은 힙에 중앙값을 push 하고
                        maxHeap.Enqueue(median, median);
                    }
                    else 
                    {
                        minHeap.Enqueue(median, median);
                    }

                    //4.3힙 중에 사이즈가 큰 힙의 top을 중앙값으로 한다.
                    if (maxHeap.Count > minHeap.Count)
                    {
                        median = maxHeap.Dequeue();
                    }
                    else 
                    {
                        median = minHeap.Dequeue();
                    }

                }
                count++;
            }
        }

        public void Add(int item)
        {
            if (maxHeap.Count < 0)
            {
                median = item;//첫 번째 수를 중앙값으로 한다.
                count++;

                return;
            }
            else
            {
                //두 번째부터 입력받은 수를 중앙값과 비교해서 작으면 최대 힙, 크면 최소 힙에 push한다.
                if (item < median)
                {
                    maxHeap.Enqueue(item, item);
                }
                else
                {
                    minHeap.Enqueue(item, item);
                }

                if (count % 2 == 1)
                { //4.1 홀수 번째 수를 읽을 때 마다

                    if (maxHeap.Count < minHeap.Count)
                    {//4.2최대 힙과 최소 힙 중에 사이즈가 작은 힙에 중앙값을 push 하고
                        maxHeap.Enqueue(median, median);
                    }
                    else
                    {
                        minHeap.Enqueue(median, median);
                    }

                    //4.3힙 중에 사이즈가 큰 힙의 top을 중앙값으로 한다.
                    if (maxHeap.Count > minHeap.Count)
                    {
                        median = maxHeap.Dequeue();
                    }
                    else
                    {
                        median = minHeap.Dequeue();
                    }

                    count++;
                }

            }
        }

        public int Peek() 
        {
            return median;
        }


    }


    #endregion
}
