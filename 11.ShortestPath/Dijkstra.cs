using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    #region Theory
    /// 다익스트라 알고리즘 (Dijkstra Algorithm) 
    // 특정한 노드에서 출발하여 다른 노드로 가는 각각의 최단 경로를 구함
    // 방문하지 않은 노드 중에서 최단 거리가 가장 짧은 노드를 선택 후,
    // 해당 노드를 거쳐 다른 노드로 가는 비용 계산

    // 직접적으로 연결된 엣지가 없고(혹은 최단경로가아닐때) 거쳐가는 엣지가 있을때
    // 거쳐가는 엣지 중 가장 최단거리인걸 직접적으로 연결된 것의 거리로 본다.

    //알고리즘
    //① 출발 노드와 도착 노드를 설정한다.
    //② '최단 거리 테이블'을 초기화한다.(무한대(최대값,혹은최소값)로 해둔다.)
    //③ 현재 위치한 노드의 인접 노드 중 방문하지 않은 노드를 구별하고,
    //     방문하지 않은 노드 중 거리가 가장 짧은 노드를 선택한다.그 노드를 방문 처리한다.
    //④ 해당 노드를 거쳐 다른 노드로 넘어가는 간선 비용(가중치) 을 계산해 '최단 거리 테이블'을 업데이트한다.
    //⑤ ③~④의 과정을 반복한다.
    #endregion
    internal class Dijkstra
    {
        const int INF = 99999; // max 값으로하면 + 연산을하면 오버플로우되서 일단은 임의의 큰수로 설정
        public static void ShortestPath(in int[,] graph, in int start, out int[] cost, out int[] parent)
        {
            int size = graph.GetLength(0);
            bool[] visited = new bool[size];//방문 확인용
            cost = new int[size]; // 거리,비용
            parent = new int[size];// 방문노드 저장용

            for (int i = 0; i < size; i++)
            {
                cost[i] = graph[start, i];// start 에서 i 즉 모든 노드 에대해서 cost(비용,거리) 저장
                parent[i] = graph[start, i] < INF ? start : -1; // 연결되어있는지 확인
            }

            for (int i = 0; i < size; i++)
            {
                // 1. 방문하지 않은 정점 중 가장 가까운 정점부터 탐색
                int next = -1;
                int minCost = INF;
                for (int j = 0; j < size; j++)
                {
                    if (!visited[j] && // 방문하지 않은
                        cost[j] < minCost) // 가장 작은 정점부터
                    {
                        next = j; // 다음 정점으로
                        minCost = cost[j];// 더작은 값으로 바꿔준다.
                    }
                    
                }
                if (next < 0)//next 에 가장 가까운 정점이 나오게된다.
                    break;

                // 2. 직접연결된 거리보다 거쳐서 더 짧아진다면 갱신.
                for (int j = 0; j < size; j++)
                {
                    // cost[j] : 목적지까지 직접 연결된 거리
                    // cost[next] : 탐색중인 정점까지 거리
                    // graph[next, j] : 탐색중인 정점부터 목적지의 거리
                    if (cost[j] > cost[next] + graph[next, j]) //next 가 거쳐가는노드 // 거쳐가는게 더짧을경우
                    {
                        cost[j] = cost[next] + graph[next, j];
                        parent[j] = next;
                    }
                    visited[next] = true;
                }

            }
        }
    }
}