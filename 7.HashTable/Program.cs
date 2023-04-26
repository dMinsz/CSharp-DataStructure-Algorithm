using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace _7.HashTable
{
    internal class Program
    {
        #region Hash Table Introduction
        /// 해시 테이블 (HashTable)
        // 키 값을 해시함수로 해싱하여 해시테이블의 특정 위치로 직접 엑세스하도록 만든 방식
        // 해시 : 임의의 길이를 가진 데이터를 고정된 길이를 가진 데이터로 매핑

        // 키값을 특정 규칙에 따라 특정 인덱스로 치환해주는걸 해싱이라한다.
        // 배열의 크기를 크게 잡아두기때문에 공간을 포기하고 속도를 택하는 방식이라고도 할 수 있다.

        /// 해시함수의 조건
        // 입력해대한 결과가 항상동일한 결과여야한다.(해시함수의 조건은 1:1 매칭)

        /// <해시테이블 주의점 - 충돌>
        // 해시함수가 서로 다른 입력 값에 대해 동일한 해시테이블 주소를 반환하는 것
        // 모든 입력 값에 대해 고유한 해시 값을 만드는 것은 불가능하며 충돌은 피할 수 없음
        // 대표적인 충돌 해결방안으로 체이닝과 개방주소법이 있음

        // 예시 : 나눗셈법 (공간크기로 키값을 나눠준다.)
        // 2 222 -> 222 , 1 222 ->222 가된다. 즉 충돌이 생김

        /// <충돌해결방안 - 체이닝>
        // 해시 충돌이 발생하면 연결리스트로 데이터들을 연결하는 방식
        // 장점 : 해시테이블에 자료가 많아지더라도 성능저하가 적음
        // 단점 : 해시테이블 외 추가적인 저장공간이 필요
        // -> 탐색법 키값을 먼저 찾은후 연결리스트에 Find 사용하면된다.

        /// <충돌해결방안 - 개방주소법>
        // 해시 충돌이 발생하면 다른 빈 공간에 데이터를 삽입하는 방식
        // 해시 충돌시 선형탐색, 제곱탐색, 이중해시 등을 통해 다른 빈 공간을 선정
        // 장점 : 추가적인 저장공간이 필요하지 않음, 삽입삭제시 오버헤드가 적음
        // 단점 : 해시테이블에 자료가 많아질수록 성능저하가 많음
        //
        /// 해시테이블의 공간 사용률이 높을 경우 성능저하가 발생하므로 재해싱 과정을 진행함
        // 재해싱 : 해시테이블의 크기를 늘리고 테이블 내의 모든 데이터를 다시 해싱 

        /// 참고: C# 에서는 노드기반인 체이닝을 사용을 지양한다.
        //      보통 C#에서는 개방주소법을 쓴다

        /// 해시 함수의 효율
        // ****해시 함수자체가 느린경우 의미가 없다.
        // ****해시 함수의 결과가 밀집도가 낮아야한다! - 최대한 충돌을 피한다.
        // ****해시 테이블의 공간이 클수록 효율이 높다 - 역시나 충돌확률이 낮아진다.

        // <해시테이블의 시간복잡도>
        // 접근			탐색			삽입			삭제
        // X			O(1)		O(1)		O(1)

        //참고: Dynamic hashTable은 70~80 프로 공간이 찼으면 공간을 늘리고 재해싱을 통해 재배치 해준다.
        #endregion

        #region C# Dictionary Test Code
        //해시테이블을 쓰는 C#의 Dictionary 사용해보기
        public static void Dictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("txt", "텍스트 파일");
            dictionary.Add("bmp", "이미지 파일");
            dictionary.Add("mp3", "오디오 파일");

            Console.WriteLine(dictionary["txt"]);       // 키값은 인덱서를 통해 접근

            if (dictionary.ContainsKey("mp3"))
                Console.WriteLine("mp3 키 값의 데이터가 있음");
            else
                Console.WriteLine("mp3 키 값의 데이터가 없음");

            if (dictionary.Remove("mp3"))
                Console.WriteLine("mp3 키 값에 해당하는 데이터를 지움");
            else
                Console.WriteLine("mp3 키 값에 해당하는 데이터를 못지움");

            if (dictionary.ContainsKey("mp3"))
                Console.WriteLine("mp3 키 값의 데이터가 있음");
            else
                Console.WriteLine("mp3 키 값의 데이터가 없음");

            string output;
            if (dictionary.TryGetValue("bmp", out output))
                Console.WriteLine(output);
            else
                Console.WriteLine("bmp 키 값의 데이터가 없음");


            //이렇게 함수를 전달하는방법도있다.
            Dictionary<string, Action> cheat = new Dictionary<string, Action>();

            cheat.Add("Show Me The Money", ShowMoney); // 이런식으로 사용가능

        }

        public static void ShowMoney()
        {
            Console.WriteLine("CHEAT: Show me the money");
        }
        #endregion

        #region My HashTable Test Code
        static void TestHashTable() 
        {
            DataStructure.HashTable<string, string> myTable = new DataStructure.HashTable<string, string>();

            myTable.Add("txt", "텍스트 파일");
            myTable.Add("bmp", "이미지 파일");
            myTable.Add("mp3", "오디오 파일");

            Console.WriteLine(myTable["txt"]); // 키값은 인덱서를 통해 접근 가능

            //키가있는지 확인
            if (myTable.ContainsKey("mp3"))
                Console.WriteLine("mp3 키 값의 데이터가 있음");
            else
                Console.WriteLine("mp3 키 값의 데이터가 없음");

            //삭제
            Console.WriteLine("\n mp3 삭제 후 재 확인");
            myTable.Remove("mp3");
            if (myTable.ContainsKey("mp3"))
                Console.WriteLine("mp3 키 값의 데이터가 있음");
            else
                Console.WriteLine("mp3 키 값의 데이터가 없음");
        }
        #endregion
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Hash Table!");

            //해시테이블을 쓰는 C#의 Dictionary 사용해보기
            //Dictionary();

            //내가만든 해시테이블 사용 Test
            TestHashTable();
        }
    }
}