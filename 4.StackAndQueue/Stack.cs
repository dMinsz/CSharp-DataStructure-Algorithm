using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    #region adapter 패턴으로 Stack 구현

    // Adapter Pattern
    // 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환

    public class AdapterStack<T>
    {
        private List<T> container;

        public AdapterStack()
        {
            container = new List<T>(); // 어댑터 패턴을 사용하기위해 초기화해준다.
        }

        public void Push(T item)
        {
            container.Add(item); // push 를 List 의 add 로 구현
        }

        public T Pop()
        {
            T value = container.Last<T>(); // 마지막 값을 빼야되기때문에 저장해둠
            container.RemoveAt(container.Count - 1);// 마지막값을 삭제
            return value; // 저장해둔 마지막값 리턴
        }

        public T Peek()
        {
            if (container.Count == 0) // 아무런 값이 없을때
                throw new InvalidOperationException();
            
            return container[container.Count - 1]; // 현재 마지막 위치가 count에 들어있기때문에 이렇게 리턴
        }
        
        //현재 스택이 다차있는지 확인
        public bool IsFull()
        {
            return container.Count == container.Capacity;
        }

        public bool IsEmpty()
        {
            return container.Count == 0;
        }

    }

    #endregion

    #region Basic Stack 구현
    public class Stack<T>
    {
        private const int DefaultCapacity = 4;
        //기본크기 설정 

        private T[] array; // 실제 데이터저장할곳
        private int topIndex;// 현재 위치

        public Stack()
        {
            array = new T[DefaultCapacity];
            topIndex = -1; // 처음엔 빈곳을 가리킨다.
        }

        public int Count { get { return topIndex + 1; } } //처음에 빈곳을 가리키기 때문에 +1 해준다.

        //첫 초기화 값으로 바꾼다.
        public void Clear()
        {
            array = new T[DefaultCapacity];
            topIndex = -1;
        }

        //현재 위치의 값가져오기
        public T Peek()
        {
            if (IsEmpty()) // 비웠으면 throw
                throw new InvalidOperationException();

            return array[topIndex]; // 스택의 현재위치는 마지막위치와 동의어다
        }

        //Peek 시도 실패시 false 와 기본값 을 준다.
        public bool TryPeek(out T result)
        {
            if (IsEmpty())
            {
                result = default(T);
                return false;
            }
            else
            {
                result = array[topIndex];
                return true;
            }
        }

        // 현재 값 가져오기
        public T Pop()
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            return array[topIndex--];
            //굳이 데이터는 삭제할필요는 없고 topIndex를 이동하여
            //안사용하면 된다.
        }

        //Pop 시도
        public bool TryPop(out T result)
        {
            if (IsEmpty())
            {
                result = default(T);
                return false;
            }
            else
            {
                result = array[topIndex--];
                return true;
            }
        }

        //데이터 추가
        public void Push(T item)
        {
            if (IsFull())
            {
                Grow(); // 배열의 크기가 모자를때 늘려준다.
            }
            array[++topIndex] = item;
        }

        //배열의 크기 늘려주는 함수
        private void Grow()
        {
            int newCapacity = array.Length * 2;
            T[] newArray = new T[newCapacity];
            Array.Copy(array, 0, newArray, 0, Count);
            array = newArray;
        }

        //비워져 있는지 확인
        private bool IsEmpty()
        {
            return Count == 0;
        }

        //현재 스택이 다차있는지 확인
        private bool IsFull()
        {
            return Count == array.Length;
        }
    }
    #endregion
}
