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
            container = new List<T>();
        }

        public void Push(T item)
        {
            container.Add(item);
        }

        public T Pop()
        {
            T value = container.Last<T>();
            container.RemoveAt(container.Count);
            return value;
        }

        public T Peek()
        {
            if (container.Count == 0)
                throw new InvalidOperationException();

            return container[container.Count];
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
            topIndex = -1; // 처음엔 빈곳을 가르킨다.
        }

        public int Count { get { return topIndex + 1; } }

        public void Clear()
        {
            array = new T[DefaultCapacity];
            topIndex = -1;
        }

        //현재 위치의 값가져오기
        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            return array[topIndex];
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
