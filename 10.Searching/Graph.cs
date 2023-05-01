using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searching
{

    /// 그래프
    // 정점의 모음과 이 정점을 잇는 간선의 모음의 결합 (정점:Vertex,간선:Edge)
    // 한 노드에서 출발하여 다시 자기 자신의 노드로 돌아오는 순환구조를 가짐 (트리와의 차이점)
    // 간선의 방향성에 따라 단방향 그래프, 양방향 그래프가 있음
    // 간선의 가중치에 따라   연결 그래프, 가중치 그래프가 있음

    // 기존의 트리는 노드로 만들었는데(Edge의 갯수가 정해져 있다.)
    // 그래프는 몇 개의 Edge가(연결이) 있는 지 알 수가 없다..
    // Edge 갯수가 언제나 늘어날수있는 노드를 만들어야한다.

    internal class AdjacencyListGraph
    {
        // <인접리스트 그래프>
        // 그래프 내의 각 정점의 인접 관계를 표현하는 리스트
        // 인접한 간선만을 리스트에 추가하여 관리
        // 장점 : 메모리 사용량이 적음
        // 단점 : 인접여부를 확인하기 위해 리스트 탐색이 필요

        List<List<int>> listGraph;
        List<List<(int, int)>> listWeightGraph;//가중치 그래프
        public void Create(int size)
        {
            listGraph = new List<List<int>>();
            listWeightGraph = new List<List<(int, int)>>();

            for (int i = 0; i < size; i++)
                listGraph.Add(new List<int>());

            //연결방법
            //listGraph[0].Add(1); // 0번 노드가 1번과 연결되어있다.
            //listGraph[1].Add(0); // 1번 노드가 0번 노드와연결
            //listGraph[1].Add(3); // 1번 노드가 3번 노드와 연결
            //listGraph[2].Add(0); // 2번 노드가 0번 노드와 연결


        }

        //인접 여부 확인
        //ver = 1 이면 그냥 그래프, 2면 가중치 그래프
        public bool IsPossibleToMove(int ver, int start, int end)
        {
            if (ver == 1)
            {
                foreach (int vertex in listGraph[start])
                {
                    if (vertex == end)
                        return true;
                }
            }
            else
            {
                foreach ((int item,int weight) in listWeightGraph[start])
                {
                    if (item == end)
                        return true;
                }
            }
            return false;
        }
    }
    internal class MatrixGraph
    {
        // <인접행렬 그래프>
        // 그래프 내의 각 정점의 인접 관계를 나타내는 행렬
        // 2차원 배열을 [출발정점, 도착정점] 으로 표현
        // 장점 : 인접여부 접근이 빠름
        // 단점 : 메모리 사용량이 많음 / Vertex 가 많은데 Edge는 적을때 쓸모없는 데이터가 많아짐

        // 양방향 연결 그래프

        bool[,] matrixGraph1 = new bool[5, 5] // 정점이 5개일때
        {
            { false, false, false, false,  true },
            { false, false,  true, false, false },
            { false,  true, false,  true, false },
            { false, false,  true, false,  true },
            {  true, false, false,  true, false },
        };
        // 특징: 대각선으로 봤을때 정확히 대칭이된다.
        // 단방향으로 하고싶으면 true false 로 바꿔주면된다.

        // 예시 - 단방향 가중치 그래프 (단절은 최대값으로 표현)
        const int INF = int.MaxValue;//임의의 수를 단절로 표현가능 // 보통 -1 이나 최댓값 최소값 등을 씀
        int[,] matrixGraph2 = new int[5, 5]
        {
            {   0, 132, INF, INF,  16 },
            {  12,   0, INF, INF, INF },
            { INF,  38,   0, INF, INF },
            { INF,  12, INF,   0,  54 },
            { INF, INF, INF, INF,   0 },
        };

        //인접 여부 확인
        //ver = 1 이면 그냥 그래프, 2면 가중치 그래프
        public bool IsPossibleToMove(int ver,int start, int end)
        {
            if (ver == 1)
            {
                if (matrixGraph1[start, end])
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else 
            {
                if (matrixGraph2[start, end] != INF)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
