namespace _6.BinarySearchTree
{
    internal class Program
    {
        #region Intro Description
        //이전에 배운 리스트, 연결리스트 모두 탐색에 O(n) 의 시간복잡도를 갖고있고
        //또한 스택,큐,우선순위 큐는 역할때문에 쓰는것이다.
        //그렇다면 탐색이 유용한 자료구조가 뭐가있을까?


        /// 이진 탐색트리!(BinarySearchTree)
        // 이진속성과 탐색속성을 적용한 트리
        // 이진탐색을 통한 탐색영역을 절반으로 줄여가며 탐색 가능
        // 이진 : 부모노드는 최대 2개의 자식노드들을 가질 수 있음
        // 탐색 : 자신의 노드보다 작은 값들은 왼쪽, 큰 값들은 오른쪽에 위치

        /* 참고 : 트리 (Tree)
		 * 계층적인 자료를 나타내는데 자주 사용되는 자료구조
		 * 부모노드가 0개 이상의 자식노드들을 가질 수 있음
		 * 한 노드에서 출발하여 다시 자기 자신의 노드로 돌아오는 순환구조를 가질 수 없음
		 * => 순환구조를 가지면 그래프 라는 자료구조가 된다.
		 */


        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Binary Search Tree!");
        }
    }
}