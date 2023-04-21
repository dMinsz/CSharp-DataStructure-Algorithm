using System;
using System.Collections.Generic;
using System.Dynamic;
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

    #region 스택 계산기
    // 일반적으로 (a+b)*c 를
    // 컴퓨터는 후위 표기법 혹은 전위 표기법으로 바꿔서 계산을해야 연산을할 수있다.
    // 일단 후위 표기법으로 바꿔서 만드려고한다.


    /// 계산하는법
    //(a+b)*c => 후위 표기법으로는 ab+c* 로 표현되며
    // (각 토큰)각 문자하나하나를 읽으면서 계산을 처리한다.
    // 토큰이 피연산자라면 그대로 스택에 삽입하고
    // 연산자라면 스택에서 두개의 값을 뽑아서 연산하는 방식으로
    // 계산하게된다.

    //중위 연산식을 후위 연산식으로 바꿔주는 클래스
    public static class NotationChanger
    {
        //각 연산자의 우선순위를 얻어오는 함수
        public static int GetPriority(char op, bool InStack)
        {
            if (op == '+' || op == '-')
            {
                return 1;
            }
            else if (op == '*' || op == '/')
            {
                return 2;
            }
            else if (op == '(')
            {
                if (InStack) return 0;
                else return 5;
            }
            else if (op == ')')
            {
                return 4;
            }

            return 0;//error
        }
        public static string Change(string inFix) //infix 기본적인 중위 표현 
        {
            char[] op = { '(', ')', '+', '-', '*', '/' };
            Stack<char> stack = new Stack<char>();
            string postFix = ""; // 후위 연산식으로 바꾼 내용을 저장할 곳

            foreach (var token in inFix)
            {

                if (!op.Contains(token)) // token이 피연산자일시 결과값에 넣는다.
                {
                    postFix += token;
                }
                else//token 이 연산자일경우
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(token);
                    }
                    else if (token == ')')
                    {
                        while (stack.Peek() != '(')
                        {
                            postFix += stack.Pop();
                        }
                    }
                    else // ")" 연산자가 아닌 다른 연산자일 경우 
                    {
                        char poped = stack.Pop();
                        if (GetPriority(poped, true) > GetPriority(token, false))
                        {
                            //postFix += poped;

                            stack.Push((char)token);
                            stack.Push(poped);
                        }
                        else
                        {
                            stack.Push(poped);
                            stack.Push((char)token);
                        }
                    }
                }

            }

            while (stack.Count != 0)
            {
                if ('(' == stack.Peek())
                {
                    stack.Pop();
                }
                else 
                {
                    postFix += stack.Pop();
                }
            }

            return postFix;
        }
    }

    /* 후위 표현식 계산 알고리즘
     * 
     *① 표현식 계산에 사용할 스택을 준비합니다.
     *② 표현식을 왼쪽부터 오른쪽으로 읽으며 토큰 단위로 분해하고 계산을 진행합니다.
     *③ 읽고 있는 토큰이 피연산자라면, 그대로 스택에 삽입합니다.
     *④ 읽고 있는 토큰이 연산자라면, 스택에서 피연산자 2개를 빼낸 다음 계산해서 다시 스택에 삽입합니다.
     *⑤ 위 과정을 반복하면 마지막에는 스택에 하나의 노드만 남게 되는데, 그것이 최종 결과가 됩니다.
     * 
     */
    //사칙연산만 구현했다.
    public static class StackCalculator
    {
        public static int Calculate(string inFix) 
        {
            char[] op = { '(', ')', '+', '-', '*', '/' };
            Stack<string> stack = new Stack<string>();
            string postFix = NotationChanger.Change(inFix);

            foreach (char token in postFix) 
            {
                if (!op.Contains(token)) // token이 피연산자일시 결과값에 넣는다.
                {
                    stack.Push(token.ToString());
                }
                else //연산자라면
                {
                    int value2 = Convert.ToInt32(stack.Pop());
                    int value1 = Convert.ToInt32(stack.Pop());
                    //string 이기 때문에 convert 해줘야한다.
                    int result = default(int);
                    switch (token)
                    {
                        case '+':
                            result = value1 + value2;
                            break;
                        case '-':
                            result = value1 - value2;
                            break;
                        case '*':
                            result = value1 * value2;
                            break;
                        case '/':
                            result = value1 / value2;
                            break;
                        default:
                            break;
                    }
                    stack.Push(result.ToString());
                }

            }

            return Convert.ToInt32(stack.Pop());
        } 

    }


    #endregion

}
