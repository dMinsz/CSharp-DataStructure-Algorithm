using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    #region Adapter Queue 구현

    /// Adapter Pattern
    // 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환

    // LinkedList(연결리스트)가 C#에서는 매우 비효율적인 작업을 하기때문에
    // 비효율적인 작업 => 삭제 시 인스턴스화 된 데이터를 GC가 처리해야하는데 이부분이 비효율적이다.
    // 사용안하는게 좋다. 차라리 Queue를 다시 만드는게 좋다.
    public class AdapterQueue_LinkedList<T>
    {
        private LinkedList<T> container;
        /// 컨테이너로 List 를 사용하지 않고 LinkeList를 이용하는이유
        /// 가장 앞에 꺼를 삭제해야할때 복사가 일어나는데 너무 비효율적이다.
        public AdapterQueue()
        {
            container = new LinkedList<T>();
        }

        //추가
        public void Enqueue(T item)
        {
            container.AddLast(item);
        }

        public T Peek() 
        {
            if (container.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return container.First<T>();
        }

        //삭제
        public T DeQueue()
        {
            T value = container.First<T>();
            container.RemoveFirst();
            return value;
        }
    }
    #endregion


    #region Queue 구현
    // 구현방식
    // 가장 앞을 가리키는 것 front, 마지막을 가리키는것 rear 를 만들어두고
    // 기존 리스트에서 가장 앞에 데이터를 추가할때, 혹은 삭제할때 복사하는 작업을 하지않고
    // front, rear 를 움직여서 데이터를 추가 , 삭제한다.
    // 즉 환형 배열 의 방식을 이용한다.

    /// 참고 사항 
    /// Tech Interveiw 에 사진을 추가하여 설명해두었으니 
    /// 확인 바람 
    // 첫 값을 비워두고 빈칸 하나를 써서 공간이 다 찼는지, 첫값인지 확인한다.
    // front - tail = 0 이면 queue가 비워진것

    public class Queue<T>
    {
        private const int DefaultCapacity = 4;
        //기본 크기설정
        private T[] array;// 실제 데이터가 저장될 공간
        private int front;// 첫 인덱스를 가리킴
        private int tail;// 마지막 인덱스를 가리킴

        public Queue()
        {
            array = new T[DefaultCapacity + 1]; 
            // 첫값은 빈칸을 가리키고있어야하기때문에 + 1
            front = 0;
            tail = 0;
        }

        public int Count
        {
            get
            {
                if (front <= tail) // front 가 tail 보다 작으면 위치상 한바퀴 안돈거
                    return tail - front;
                else // front 가 크면 전체 배열 크기에서 front값을 뺀걸 더해줘야 실제 카운트가나온다.
                    return tail + (array.Length - front);
            }
        }

        //비우기
        public void Clear()
        {
            array = new T[DefaultCapacity + 1];
            front = 0;
            tail = 0;
        }

        //값추가
        public void Enqueue(T item)
        {
            if (IsFull())
            {
                Grow(); // 배열이 꽉찼으면 늘려준다.
            }

            array[tail] = item; // 값을 tail 위치에 넣어준다.
            MoveNext(ref tail); // 다음값으로 넘겨준다.
        }

        //값 삭제
        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            T result = array[front];
            MoveNext(ref front); // 삭제후에는 이동시켜줘야한다.
            return result;
        }

        //삭제 시도
        public bool TryDequeue(out T result)
        {
            if (IsEmpty())
            {
                result = default(T);
                return false;
            }

            result = array[front];
            MoveNext(ref front); // 삭제하고 하나 이동해줘야한다.
            return true;
        }

        // 현재 값 가져오기
        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            return array[front];
        }

        //Peek 시도
        public bool TryPeek(out T result)
        {
            if (IsEmpty())
            {
                result = default(T);
                return false;
            }

            result = array[front];
            return true;
        }

        private void Grow()
        {
            int newCapacity = array.Length * 2;
            T[] newArray = new T[newCapacity + 1];
            if (!IsEmpty())
            {
                if (front < tail)
                {
                    Array.Copy(array, front, newArray, 0, tail);
                    //front 가 tail 보다 작으면
                    //그대로 카피하면된다.
                }
                else
                {
                    Array.Copy(array, front, newArray, 0, array.Length - front);
                    Array.Copy(array, 0, newArray, array.Length - front, tail);

                    //front가 tail보다 크면
                    // 0 부터 array.Length - front 즉 한바퀴돌고 추가된 부분만 카피
                    // 한바퀴 돌기 전에 부분을 카피해준다. 
                    //array.Length - front 부터 tail 까지가 그부분을 의미한다.
                }
            }

            array = newArray;
            tail = Count;
            front = 0;
        }

        //배열의 공간이 모자른지 확인
        private bool IsFull()
        {
            if (front > tail)
            {
                return front == tail + 1;
                // front 가 더 크면 한바퀴 돈거다.
                // 그상태에서는 front 와 tail 같으면 배열이 다찬것인데
                // 빈칸이 있기때문에 +1 해준것
            }
            else
            {
                return front == 0 && tail == array.Length - 1;
                //front 가 0 이면 첫값 && tail이 배열 끝에있다. => 다찬것
            }
        }

        private bool IsEmpty()
        {
            return front == tail; // front - tail = 0 이면 queue가 비워진것
        }

        private void MoveNext(ref int index)
        {
            index = (index == array.Length - 1) ? 0 : index + 1;
            // 인덱스가 마지막값을 가리키면 첫값으로(0) 으로 인덱스 변경
            // 아니면 +1 해주면된다.
        }
    }
    #endregion
}
