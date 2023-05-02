# 다익스트라 알고리즘 (Dijkstra Algorithm) 

## 기본 알고리즘

특정한 노드에서 출발하여 다른 노드로 가는 각각의 최단 경로를 구한다.

방문하지 않은 노드 중에서 최단 거리가 가장 짧은 노드를 선택 후, 해당 노드를 거쳐 다른 노드로 가는 비용 계산한다.


## 구현 방법

1. 출발 노드와 도착 노드를 설정한다.
2. '최단 거리 테이블'을 초기화한다.(무한대(최대값,혹은최소값)로 해둔다.)
3. 현재 위치한 노드의 인접 노드 중 방문하지 않은 노드를 구별하고,
        방문하지 않은 노드 중 거리가 가장 짧은 노드를 선택한다.그 노드를 방문 처리한다.
4. 해당 노드를 거쳐 다른 노드로 넘어가는 간선 비용(가중치) 을 계산해 '최단 거리 테이블'을 업데이트한다.
5. ③~④의 과정을 반복한다.


## 그림으로 표현 해본 알고리즘

|Vertex |Visit|Cost|Path
|---|---|---|---|
|정점|방문했는지 체크|비용|해당정점을 방문한 정점|

정점이 8개인 그래프를 기준으로  기본 테이블 세팅은
![dijkstra1](./Images/DijkstraIamges/Dijkstra-Seting.png)
이렇게된다.

**Visit 은 현재 정점을 체크했는지를 의미하며 F=false , T = true**

**Cost 는 비용을 의미하며 INFI 는 최대값(무한대)을 의미한다.**

**Path 는 해당 정점을 방문한 정점을 의미한다. 초기값은 -1**

*이후 설명에는 기본적인 탐색후 cost 가 바뀌는 것은 설명을 생략할 것이며*

*특이사항이 생기는 부분만 설명할 것이다*


###  0번 정점부터 탐색을 시작한다. 

![dijkstra1](./Images/DijkstraIamges/dijkstra1.png)


탐색을 시작할때 visit 값을 true로 바꿔준다.

탐색은 낮은 cost 를 가지고있는 정점 부터 시작하며(현재 1번노드로 탐색완료)

탐색후에는 cost 를 비교하여 더작은 cost로 바꿔준다.

![dijkstra2](./Images/DijkstraIamges/dijkstra2.png)

![dijkstra3](./Images/DijkstraIamges/dijkstra3.png)


###  0 정점의 탐색이 끝났기때문에 다음 낮은 정점인 1을 기준으로 탐색한다.


![dijkstra4](./Images/DijkstraIamges/dijkstra4.png)


![dijkstra5](./Images/DijkstraIamges/dijkstra5.png)

![dijkstra6](./Images/DijkstraIamges/dijkstra6.png)


1번 정점 탐색시 3번 정점으로 가는 새로운 Cost 가 생겼기때문에
   
cost를 바꿔주고 path 값도 갱신해준다.



###  이제 다음 정점인 2번 노드를 탐색한다.
![dijkstra7](./Images/DijkstraIamges/dijkstra7.png)

![dijkstra8](./Images/DijkstraIamges/dijkstra8.png)

![dijkstra9](./Images/DijkstraIamges/dijkstra9.png)

6번 정점으로 가는 새로운 Cost 가 생겼기때문에

cost를 바꿔주고 path 값도 갱신해준다.

### 4번 정점 탐색

![dijkstra10](./Images/DijkstraIamges/dijkstra10.png)
![dijkstra11](./Images/DijkstraIamges/dijkstra11.png)
![dijkstra12](./Images/DijkstraIamges/dijkstra12.png)
![dijkstra13](./Images/DijkstraIamges/dijkstra13.png)
![dijkstra14](./Images/DijkstraIamges/dijkstra14.png)

7번 정점으로 가는 새로운 Cost 가 생겼기때문에

cost를 바꿔주고 path 값도 갱신해준다.


### 6번 정점 탐색

![dijkstra15](./Images/DijkstraIamges/dijkstra15.png)
![dijkstra16](./Images/DijkstraIamges/dijkstra16.png)
![dijkstra17](./Images/DijkstraIamges/dijkstra17.png)
![dijkstra18](./Images/DijkstraIamges/dijkstra18.png)
![dijkstra19](./Images/DijkstraIamges/dijkstra19.png)

5번 정점으로 가는 새로운 Cost 가 생겼기때문에

cost를 바꿔주고 path 값도 갱신해준다.


### 3번 정점 탐색

![dijkstra20](./Images/DijkstraIamges/dijkstra20.png)



### 5번 정점 탐색

![dijkstra21](./Images/DijkstraIamges/dijkstra21.png)
![dijkstra22](./Images/DijkstraIamges/dijkstra22.png)

### 7번 정점 탐색
![dijkstra23](./Images/DijkstraIamges/dijkstra23.png)
![dijkstra24](./Images/DijkstraIamges/dijkstra24.png)


모든 정점을 탐색해서 완료되었다.


|Vertex |Visit|Cost|Path
|---|---|---|---|
|0|T|0|-1|
|1|T|4|0|
|2|T|5|0|
|3|T|9|1|
|4|T|7|0|
|5|T|13|6|
|6|T|7|2|
|7|T|5|4|

path 를 이용해서 역으로 계산을 해보면 0번 정점부터 시작하여

모든 정점에 대한 최단거리를 구할 수 있게 된다.

* 0->0 은 없음

* 0->1 은 0->1

* 0->2 는 0->2

* 0->3 은 0->1->3

* 0->4 는 0->4

* 0->5 는 0->2->6->5

* 0->6 은 0->2->6

* 0->7 은 0->4->7

이 된다.


위의 내용을 코드로 바꾸면 
아래처럼 된다.

```cs
    public class Dijkstra
    {
        const int INF = 99999; // max 값으로하면 + 연산을하면 오버플로우되서 일단은 임의의 큰수로 설정
        public static void ShortestPath(in int[,] graph, in int start, out int[] cost, out int[] parent)
        {
            int size = graph.GetLength(0);
            bool[] visited = new bool[size];//방문 확인용
            cost = new int[size]; // 거리,비용
            parent = new int[size];// 방문정점 저장용

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
```