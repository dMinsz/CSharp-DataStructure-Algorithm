# 기술 면접 대비 정보 정리

### 배열과 선형 리스트

#### Array, List, LinkedList 차이는?


Array 는 선언할때 **크기** 와 **데이터 타입**을 지정해야한다.
List,LinkedList 는 데이터 타입만 지정해주면된다.

``` cs
//Array
int[] intArr = new int[10];
string[] stringArr = new string[5];

//List
List<int> intList = new List<int>();
List<string> stringList = new List<string>();

//LinkedList
LinkedList<string> linkedlist = new LinkedList<string>();

```

이와 같이

**Array** 는 메모리 공간에 할당할 사이즈를 미리 정해두고 사용하는 자료구조이다.
따라서 데이터 크기를 알 수 없거나, 사이즈가 변동 될 만한 상황에서는 사용하기에 부적합하다.

또한 중간에 데이터 삽입 및 삭제 할 때도 매우 비효율 적이다.

단 장점은 indexing 이 되어있어 값에 접근에 있어 편리한 장점이있다.

**List** 는 Array 와 달리 크기를 지정하지 않아도 된다는 장점이 있다.
즉 크기가 정해져 있지 않기 때문에 데이터의 크기 를 알 수 없거나 사이즈가 변동 되는 상황에
사용하기 에 적합하다.

단, Array 와 마찬가지로 데이터의 삽입 및 삭제 시에 새로운 배열을 만들고
각 요소들을 복사하여 저장하는 작업이 일어나기때문에 삽입,삭제를 자주 하는 작업에는
비효율적이다.

**LinkedList** 는 List 와 같은 사이즈를 미리 정해두지 않고 사용하는 자료구조이며,
레퍼런스 형으로 각 노드들이 연결되어 있어서 데이터의 삽입,삭제 시 Array, List 보다
더 효율적이다.

단, 데이터 검색 및 접근시 모든 노드들을 거쳐가면서 데이터를 탐색하기 때문에 Array, List
보다 더 비효율 적인 단점이있다.