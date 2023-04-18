namespace DataStructure.List
{
    internal class Program
    {
        /// 리스트를 다루기전 배열을 먼저 다시 알아보자. 

        /*
         * 배열 (Array)
         * 연속적인 메모리상에 동일한 타입의 요소를 일렬로 저장하는 자료구조
         * 초기화때 정한 크기가 소멸까지 유지됨
         * 배열의 요소는 인덱스를 사용하여 직접적으로 엑세스 가능
         */

        public void Array() 
        {
            int[] arr = new int[100];// int 형 100개의 자료구조 만듬

            //인덱스를 통한 접근
            arr[0] = 1;
            int value = arr[0];
        }

        /*
         * 배열의 시간복잡도
         * 접근       탐색
         * O(1)      O(n)
         */

        /*
         * 배열이 만들어진 과정
         * 
         * new 를 통해서 int형 10개짜리 배열을 만든다하면
         * int 는 4byte 이니 4byte * 10 을해서 총 40byte 자리를 잡고
         * 각 4byte 기준으로 10개를 연속적으로 인접하게 만들어준다.
         * 
         * 즉 첫 주소값을 기준으로 다음 요소는 (첫주소값+4byte) 에 있는 것
         * 
         * 역으로 사용가능한대
         * 예를 들어 8번째 자리는 (첫주소값 + (4byte*7)) 를 해주면 된다.
         * 
         * 즉 이러한 접근을 간단하게 만들어준게 인덱스이다.
         */

        //참고 사항
        //c 스타일로 주소값으로 인덱스의 값을 찾아내는 함수
        public static int? ArrayIndex(ref int[] arr, int index)
        {
            int? result;
            if (arr?.Length > 0)
            {
                unsafe
                {
                    fixed (int* arrAddress = &arr[0])
                    {
                        result = *(arrAddress + index);
                        //index 가 4 byte 정수라서 4바이트씩 이동한다.
                    }
                }
            }
            else
            {
                result = null;
            }

            return result;
        }


        /*
		 * 동적배열 (Dynamic Array)
		 * 
		 * 런타임 중 크기를 확장할 수 있는 배열기반의 자료구조
		 * 배열요소의 갯수를 특정할 수 없는 경우 사용
		 */

        // <List의 사용>
        void List()
        {
            List<string> list = new List<string>();

            // 배열 요소 삽입
            list.Add("0번 데이터");
            list.Add("1번 데이터");
            list.Add("2번 데이터");

            // 배열 요소 삭제
            list.Remove("1번 데이터");

            // 배열 요소 접근
            list[0] = "데이터0";
            string value = list[0];

            // 배열 요소 탐색
            string? findValue = list.Find(x => x.Contains('2'));
            int findIndex = list.FindIndex(x => x.Contains('0'));
        }

        // <List의 시간복잡도>
        // 접근		탐색		삽입		삭제
        // O(1)		O(n)	O(n)	O(n)


        static void Main(string[] args)
        {

            //unsafe 모드로해야함
            Console.WriteLine("배열 주소로 접근 테스트");

            int[] arr = { 1, 2, 3 };
            Console.WriteLine("배열{0}번째의 값 : {1}",1, ArrayIndex(ref arr,1));

            //따로만든 리스트 사용
            DataStructure.List<string> list = new DataStructure.List<string>();

            list.Add("1번 데이터");
            list.Add("2번 데이터");
            list.Add("3번 데이터");
            list.Add("4번 데이터");
            list.Add("5번 데이터");

            string value;
            value = list[0];
            value = list[1];
            value = list[2];
            value = list[3];
            value = list[4];

            list[0] = "5번 데이터";
            list[1] = "4번 데이터";
            list[2] = "3번 데이터";
            list[3] = "2번 데이터";
            list[4] = "1번 데이터";

            list.Remove("3번 데이터");
            list.Remove("2번 데이터");

            string? findValue = list.Find(x => x.Contains('4'));
            int findIndex = list.FindIndex(x => x.Contains('1'));

        }
    }
}