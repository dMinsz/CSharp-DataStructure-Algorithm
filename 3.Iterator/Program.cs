namespace _3.Iterator
{
    internal class Program
    {
        /*
         * 반복기/반복자 (Enumerator(Iterator))
		 * 
		 * 자료구조에 저장되어 있는 요소들을 순회하는 인터페이스
         */

        static void Main(string[] args)
        {
            /// 대부분의 자료구조가 반복자를 지원함
            // 반복자를 이용한 기능을 구현할 경우, 그 기능은 대부분의 자료구조를 호환할 수 있음
            List<int> list = new List<int>();
            LinkedList<int> linkedList = new LinkedList<int>();
            
            Stack<int> stack = new Stack<int>();
            Queue<int> queue = new Queue<int>();
            SortedList<int, int> sList = new SortedList<int, int>();
            SortedSet<int> set = new SortedSet<int>();
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();
            Dictionary<int, int> dic = new Dictionary<int, int>();

            /// iterator를 이용한 순회
            // foreach 반복문은 데이터집합의 반복기를 통해서 단계별로 반복
            // 즉, iterator가 있다면 foreach 반복문으로 순회 가능
            //
            // 원래는 자료구조의 구조를 다 정확히 알아야 각 요소를 순회시킬 수 있을텐대
            // 반복자를 이용해서 간단하게 순회시킬 수 있다.
            foreach (int i in list) { }
            foreach (int i in linkedList) { }
            
            foreach (int i in stack) { }
            foreach (int i in queue) { }
            foreach (int i in set) { }
            foreach (KeyValuePair<int, int> i in sList) { }
            foreach (KeyValuePair<int, int> i in map) { }
            foreach (KeyValuePair<int, int> i in dic) { }

            IEnumerable<int> IterFunc()
            {
                yield return 1;
                yield return 2;
                yield return 3;
            }

            foreach (int i in IterFunc()) { }


            ///반복자를 만들어두면
            // 처음부터 끝까지 순회를 할수있고
            // 순회가 가능하다면 정렬,검색,삭제,카피 등을 만들어낼수가있을것이다.


            // 반복자 직접조작
            List<string> strings = new List<string>();

            //반복하면서 데이터 넣기
            for (int i = 0; i < 5; i++) 
                strings.Add(string.Format("{0}데이터", i));

            //반복자의 시작,끝의 값은 null 이다.
            IEnumerator<string> iter = strings.GetEnumerator();
            iter.MoveNext();
            Console.WriteLine(iter.Current);    // output : 0데이터
            iter.MoveNext();
            Console.WriteLine(iter.Current);    // output : 1데이터

            iter.Reset();


            //이게 foreach 하는거랑 똑같은 방식이다.
            while (iter.MoveNext()) // 반복자의 다음이없으면 false 를 리턴한다.
            {
                Console.WriteLine(iter.Current);
            }
            iter.Dispose();

            //내가만든 List 에 Enumerator 를 사용해본다.
            DataStructure.Iter.List<int> myList = new DataStructure.Iter.List<int>();

            myList.Add(1);
            myList.Add(3);
            myList.Add(2);
       
            IEnumerator<int> myListIter = myList.GetEnumerator();

            Console.WriteLine("리스트에 반복자 사용");
            
            while (myListIter.MoveNext()) // 반복자의 다음이없으면 false 를 리턴한다.
            {
                Console.WriteLine(myListIter.Current);
            }
            myListIter.Dispose();

            //foreach 사용해본다
            Console.WriteLine("리스트에 foreach 사용");
            foreach (var ele in myList)
            {
                Console.WriteLine(ele);
            }


            //내가만든 LinkedList 에 Enumerator 를 사용해본다.
            DataStructure.Iter.LinkedList<string> myLinkedList= new DataStructure.Iter.LinkedList<string>();
            myLinkedList.AddLast("First");
            myLinkedList.AddLast("Second");
            myLinkedList.AddLast("Third");
            myLinkedList.AddLast("Fourth");


            IEnumerator<string> myLinkedListIter = myLinkedList.GetEnumerator();

            Console.WriteLine("연결리스트에 반복자 사용");
            while (myLinkedListIter.MoveNext()) // 반복자의 다음이없으면 false 를 리턴한다.
            {
                Console.WriteLine(myLinkedListIter.Current);
            }
            myLinkedListIter.Dispose();

            Console.WriteLine("연결리스트에 foreach 사용");

            //foreach 사용해본다
            foreach (var ele in myLinkedList) 
            {
                Console.WriteLine(ele);
            }

            //정렬 테스트코드
            Console.WriteLine("정렬 해보기");
            List<int> sorttest = new List<int>();
            sorttest.Add(10);
            sorttest.Add(3);
            sorttest.Add(2);
            sorttest.Add(6);
            sorttest.Add(1);
            DataStructure.MySort<int>.Sort(sorttest);

            foreach (var item in sorttest)
            {
                Console.WriteLine(item);
            }


        }
    }
}