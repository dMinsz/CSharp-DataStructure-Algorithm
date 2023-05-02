# ���ͽ�Ʈ�� �˰��� (Dijkstra Algorithm) 

## �⺻ �˰���

Ư���� ��忡�� ����Ͽ� �ٸ� ���� ���� ������ �ִ� ��θ� ���Ѵ�.

�湮���� ���� ��� �߿��� �ִ� �Ÿ��� ���� ª�� ��带 ���� ��, �ش� ��带 ���� �ٸ� ���� ���� ��� ����Ѵ�.


## ���� ���

1. ��� ���� ���� ��带 �����Ѵ�.
2. '�ִ� �Ÿ� ���̺�'�� �ʱ�ȭ�Ѵ�.(���Ѵ�(�ִ밪,Ȥ���ּҰ�)�� �صд�.)
3. ���� ��ġ�� ����� ���� ��� �� �湮���� ���� ��带 �����ϰ�,
        �湮���� ���� ��� �� �Ÿ��� ���� ª�� ��带 �����Ѵ�.�� ��带 �湮 ó���Ѵ�.
4. �ش� ��带 ���� �ٸ� ���� �Ѿ�� ���� ���(����ġ) �� ����� '�ִ� �Ÿ� ���̺�'�� ������Ʈ�Ѵ�.
5. ��~���� ������ �ݺ��Ѵ�.


## �׸����� ǥ�� �غ� �˰���

|Vertex |Visit|Cost|Path
|---|---|---|---|
|����|�湮�ߴ��� üũ|���|�ش������� �湮�� ����|

������ 8���� �׷����� ��������  �⺻ ���̺� ������
![dijkstra1](./Images/DijkstraIamges/Dijkstra-Seting.png)
�̷��Եȴ�.

**Visit �� ���� ������ üũ�ߴ����� �ǹ��ϸ� F=false , T = true**

**Cost �� ����� �ǹ��ϸ� INFI �� �ִ밪(���Ѵ�)�� �ǹ��Ѵ�.**

**Path �� �ش� ������ �湮�� ������ �ǹ��Ѵ�. �ʱⰪ�� -1**

*���� ������ �⺻���� Ž���� cost �� �ٲ�� ���� ������ ������ ���̸�*

*Ư�̻����� ����� �κи� ������ ���̴�*


###  0�� �������� Ž���� �����Ѵ�. 

![dijkstra1](./Images/DijkstraIamges/dijkstra1.png)


Ž���� �����Ҷ� visit ���� true�� �ٲ��ش�.

Ž���� ���� cost �� �������ִ� ���� ���� �����ϸ�(���� 1������ Ž���Ϸ�)

Ž���Ŀ��� cost �� ���Ͽ� ������ cost�� �ٲ��ش�.

![dijkstra2](./Images/DijkstraIamges/dijkstra2.png)

![dijkstra3](./Images/DijkstraIamges/dijkstra3.png)


###  0 ������ Ž���� �����⶧���� ���� ���� ������ 1�� �������� Ž���Ѵ�.


![dijkstra4](./Images/DijkstraIamges/dijkstra4.png)


![dijkstra5](./Images/DijkstraIamges/dijkstra5.png)

![dijkstra6](./Images/DijkstraIamges/dijkstra6.png)


1�� ���� Ž���� 3�� �������� ���� ���ο� Cost �� ����⶧����
   
cost�� �ٲ��ְ� path ���� �������ش�.



###  ���� ���� ������ 2�� ��带 Ž���Ѵ�.
![dijkstra7](./Images/DijkstraIamges/dijkstra7.png)

![dijkstra8](./Images/DijkstraIamges/dijkstra8.png)

![dijkstra9](./Images/DijkstraIamges/dijkstra9.png)

6�� �������� ���� ���ο� Cost �� ����⶧����

cost�� �ٲ��ְ� path ���� �������ش�.

### 4�� ���� Ž��

![dijkstra10](./Images/DijkstraIamges/dijkstra10.png)
![dijkstra11](./Images/DijkstraIamges/dijkstra11.png)
![dijkstra12](./Images/DijkstraIamges/dijkstra12.png)
![dijkstra13](./Images/DijkstraIamges/dijkstra13.png)
![dijkstra14](./Images/DijkstraIamges/dijkstra14.png)

7�� �������� ���� ���ο� Cost �� ����⶧����

cost�� �ٲ��ְ� path ���� �������ش�.


### 6�� ���� Ž��

![dijkstra15](./Images/DijkstraIamges/dijkstra15.png)
![dijkstra16](./Images/DijkstraIamges/dijkstra16.png)
![dijkstra17](./Images/DijkstraIamges/dijkstra17.png)
![dijkstra18](./Images/DijkstraIamges/dijkstra18.png)
![dijkstra19](./Images/DijkstraIamges/dijkstra19.png)

5�� �������� ���� ���ο� Cost �� ����⶧����

cost�� �ٲ��ְ� path ���� �������ش�.


### 3�� ���� Ž��

![dijkstra20](./Images/DijkstraIamges/dijkstra20.png)



### 5�� ���� Ž��

![dijkstra21](./Images/DijkstraIamges/dijkstra21.png)
![dijkstra22](./Images/DijkstraIamges/dijkstra22.png)

### 7�� ���� Ž��
![dijkstra23](./Images/DijkstraIamges/dijkstra23.png)
![dijkstra24](./Images/DijkstraIamges/dijkstra24.png)


��� ������ Ž���ؼ� �Ϸ�Ǿ���.


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

path �� �̿��ؼ� ������ ����� �غ��� 0�� �������� �����Ͽ�

��� ������ ���� �ִܰŸ��� ���� �� �ְ� �ȴ�.

* 0->0 �� ����

* 0->1 �� 0->1

* 0->2 �� 0->2

* 0->3 �� 0->1->3

* 0->4 �� 0->4

* 0->5 �� 0->2->6->5

* 0->6 �� 0->2->6

* 0->7 �� 0->4->7

�� �ȴ�.


���� ������ �ڵ�� �ٲٸ� 
�Ʒ�ó�� �ȴ�.

```cs
    public class Dijkstra
    {
        const int INF = 99999; // max �������ϸ� + �������ϸ� �����÷ο�Ǽ� �ϴ��� ������ ū���� ����
        public static void ShortestPath(in int[,] graph, in int start, out int[] cost, out int[] parent)
        {
            int size = graph.GetLength(0);
            bool[] visited = new bool[size];//�湮 Ȯ�ο�
            cost = new int[size]; // �Ÿ�,���
            parent = new int[size];// �湮���� �����

            for (int i = 0; i < size; i++)
            {
                cost[i] = graph[start, i];// start ���� i �� ��� ��� �����ؼ� cost(���,�Ÿ�) ����
                parent[i] = graph[start, i] < INF ? start : -1; // ����Ǿ��ִ��� Ȯ��
            }

            for (int i = 0; i < size; i++)
            {
                // 1. �湮���� ���� ���� �� ���� ����� �������� Ž��
                int next = -1;
                int minCost = INF;
                for (int j = 0; j < size; j++)
                {
                    if (!visited[j] && // �湮���� ����
                        cost[j] < minCost) // ���� ���� ��������
                    {
                        next = j; // ���� ��������
                        minCost = cost[j];// ������ ������ �ٲ��ش�.
                    }
                    
                }
                if (next < 0)//next �� ���� ����� ������ �����Եȴ�.
                    break;

                // 2. ��������� �Ÿ����� ���ļ� �� ª�����ٸ� ����.
                for (int j = 0; j < size; j++)
                {
                    // cost[j] : ���������� ���� ����� �Ÿ�
                    // cost[next] : Ž������ �������� �Ÿ�
                    // graph[next, j] : Ž������ �������� �������� �Ÿ�
                    if (cost[j] > cost[next] + graph[next, j]) //next �� ���İ��³�� // ���İ��°� ��ª�����
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