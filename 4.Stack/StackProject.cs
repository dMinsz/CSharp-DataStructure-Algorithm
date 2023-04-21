using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    #region 괄호 검사기
    //각 괄호가 쌍으로 있는지 확인
    public class ParenthesisChecker
    {
        DataStructure.AdapterStack<char> stack;

        public ParenthesisChecker() 
        {
            stack = new DataStructure.AdapterStack<char>();
        }

        public bool Check(string text) 
        {
            foreach (var character in text) 
            {
                if (character == '{' || character == '[' || character == '(')
                {
                    stack.Push(character);
                }

                switch (character)
                {
                    case '}':
                        if (stack.IsEmpty())
                            return false;
                        else
                        {
                            if (stack.Peek() == '{')
                                stack.Pop();
                            else
                                return false;
                        }
                        break;
                    case ']':
                        if (stack.IsEmpty())
                            return false;
                        else
                        {
                            if (stack.Peek() == '[')
                                stack.Pop();
                            else
                                return false;
                        }
                        break;
                    case ')':
                        if (stack.IsEmpty())
                            return false;
                        else
                        {
                            if (stack.Peek() == '(')
                                stack.Pop();
                            else
                                return false;
                        }
                        break;
                    default:
                        break;
                }

            }
            return true;
        }


        //미리 지정해둔 스트링을 테스트해본다.
        public void Test() 
        {
            Console.WriteLine($"---------괄호검사기 테스트---------");
            string test1 = "{(1+1)+(1+2}"; // false 를 줘야 정답
            string test2 = "{(1+2)*(3+2)}"; // true 를 줘야 정답

            Console.WriteLine($"\"{test1}\" 의 괄호 검사 결과 {Check(test1)}");
            Console.WriteLine($"\"{test2}\" 의 괄호 검사 결과 {Check(test2)}");
            Console.WriteLine($"---------괄호검사기 테스트---------");
        }
    }
    #endregion


}
