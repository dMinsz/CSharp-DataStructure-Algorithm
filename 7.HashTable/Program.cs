using System.Security.Cryptography.X509Certificates;

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
        /// 해시 함수의 효율
        // 해시 함수자체가 느린경우 의미가 없다.

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

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Hash Table!");

            //해시테이블을 쓰는 C#의 Dictionary 사용해보기
            Dictionary();
        }
    }
}