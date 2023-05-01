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
        public static int SiquentialSearch<T>(in IList<T> list, in T item) where T : IEquatable<T>
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
            int high = list.Count - 1;

            while (low <= high)//엇갈리지않을 때 까지 반복
            {
                int mid = (low + high) >> 1; // 나누기 2
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


        // <깊이 우선 탐색 (Depth-First Search)>
        // 그래프의 분기를 만났을 때 최대한 깊이 내려간 뒤,
        // 더이상 깊이 갈 곳이 없을 경우 다음 분기를 탐색 
        // 분할정복을 통해 구현(백트래킹)

        //깊이우선탐색
        public static void DFS(in bool[,] graph, int start, out bool[] visited, out int[] parents)
        {   //인접행렬 그래프 기준으로 구현
            //탐색의 결과로 갈수있는지 (visit), 어떤 경로로 갈수있는지(parents)
            visited = new bool[graph.GetLength(0)];
            parents = new int[graph.GetLength(0)];

            for (int i = 0; i < graph.GetLength(0); i++)
            { // 일단 모두 갈수없고 경로가없게 세팅
                visited[i] = false;
                parents[i] = -1;//갈수없음을 뜻함
            }

            SearchNode(graph, start, visited, parents);
        }
        private static void SearchNode(in bool[,] graph, int start, bool[] visited, int[] parents)
        {   // parents 에는 어떤 노드에의해서 탐색됬는지 저장해둔다.
            // 어떤 노드에 의해서 탐색됬는지 확인해서 하나씩 역으로 추적해보면 전체 적으로 어떻게 탐색되어있는지 확인 가능
            
            visited[start] = true; // 방문했음으로 세팅

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[start, i] &&      // 연결되어 있는 정점이며,
                    !visited[i])            // 방문한적 없는 정점
                {
                    parents[i] = start;// 어떤 정점에 의해서 탐색됬는지 저장
                    SearchNode(graph, i, visited, parents); // 다음 연결로 분할정복/백트래킹
                }
            }

        }


        // <너비 우선 탐색 (Breadth-First Search)>
        // 그래프의 분기를 만났을 때 모든 분기를 저장한 뒤,
        // 저장한 분기를 하나씩 탐색
        // 동적계획법을 통해 구현
        public static void BFS(in bool[,] graph, int start, out bool[] visited, out int[] parents)
        {
            visited = new bool[graph.GetLength(0)];
            parents = new int[graph.GetLength(0)];

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                visited[i] = false;
                parents[i] = -1;
            }//초기값 세팅

            Queue<int> bfsQueue = new Queue<int>();
            
            bfsQueue.Enqueue(start);
            while (bfsQueue.Count > 0) // Queue에 값이 없을때까지 반복
            {
                int next = bfsQueue.Dequeue(); // 이제 탐색할 곳
                visited[next] = true; // 방문했음으로 세팅

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (graph[next, i] &&       // 연결되어 있는 정점이며,
                        !visited[i])            // 방문한적 없는 정점
                    {
                        parents[i] = next;  // 어떤 정점에 의해서 탐색됬는지 저장
                        bfsQueue.Enqueue(i);//다음 탐색을위해 Queue에 저장
                    }
                }
            }


        }
    }



}
