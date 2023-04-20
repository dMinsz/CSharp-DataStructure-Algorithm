using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

//IEnumerabale 인터페이스 가 추가된 버전 개발
namespace DataStructure.Iter
{
    internal class List<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 10;

        private T[] items;
        private int size;

        public List()
        {
            items = new T[DefaultCapacity];
            size = 0;
        }

        public int Capacity { get { return items.Length; } }
        public int Count { get { return size; } }

        //Indexing
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                    throw new ArgumentOutOfRangeException();

                return items[index];
            }
            set
            {
                if (index < 0 || index >= size)
                    throw new ArgumentOutOfRangeException();

                items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (size < items.Length)
            {
                items[size++] = item;
            }
            else
            {   // 크기가 더 작으면
                Grow(); // 크기를 늘려주고
                items[size++] = item;//값을 넣어준다.
            }

        }

        //값을 비워주는 함수
        public void Clear()
        {
            items = new T[DefaultCapacity];//기본값으로 다시 설정
            size = 0;
        }

        //match 에 맞는 값을 찾아주는 함수
        public T? Find(Predicate<T> match)
        {
            if (match == null) throw new ArgumentNullException();

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }

            return default(T); // 만약 찾지못했다면 기본값으로 준다.
        }

        public int FindIndex(Predicate<T> match)
        {
            return FindIndex(0, size, match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            if (startIndex > size)
                throw new ArgumentOutOfRangeException();
            if (count < 0 || startIndex > size - count)
                throw new ArgumentOutOfRangeException();
            if (match == null)
                throw new ArgumentNullException();

            int endIndex = startIndex + count;//현재 가지고있는 요소까지만 반복하기위함
            for (int i = startIndex; i < endIndex; i++)
            {
                if (match(items[i])) return i;
            }
            return -1;
        }

        //Searches for an element that matches the conditions defined by the specified predicate,
        //and returns the last occurrence within the entire List<T>.
        public T? FindLast(Predicate<T> match) 
        {
            if (match == null)
                throw new ArgumentNullException();

            T result = default(T); // 만약 찾지 못하면 기본값을 리턴

            for (int i = 0; i < size; i++)
            {
                if (match(items[i])) 
                    result = items[i];
            }

            return result;
        }

        //Searches for an element that matches the conditions defined
        //by the specified predicate, and returns the zero-based index
        //of the last occurrence within the entire List<T>.
        public int FindLastIndex(Predicate<T> match) 
        {
            if (match == null)
                throw new ArgumentNullException();

            int result = -1; // 만약 찾지 못하면 -1을 리턴

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    result = i;
            }

            return result;
        }

        //Search from the first element to the specified index.
        public int FindLastIndex(Int32 lastIndex,Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException();
            if (lastIndex < 0 || lastIndex > size)
                throw new ArgumentOutOfRangeException();

            int result = -1;// 만약 찾지 못하면 -1을 리턴

            for (int i = 0; i <= lastIndex; i++)
            {
                if (match(items[i]))
                    result = i;
            }

            return result;
        }

        // Search for element taht matches the conditions from startIndex ~ lastIndex Ranged index
        public int FindLastIndex(Int32 startIndex, Int32 lastIndex, Predicate<T> match)
        {
            if (startIndex > size)
                throw new ArgumentOutOfRangeException();
            if (this.Count < 0 || startIndex > size - this.Count)
                throw new ArgumentOutOfRangeException();
            if (match == null)
                throw new ArgumentNullException();

            int result = -1;// 만약 찾지 못하면 -1을 리턴

            for (int i = startIndex; i <= lastIndex; i++)
            {
                if (match(items[i]))
                    result = i;
            }

            return result;
        }


        public int IndexOf(T item)
        {
            return Array.IndexOf(items, item, 0, size);
        }

        //요소 삭제함수
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException();

            size--;
            Array.Copy(items, index + 1, items, index, size - index);
            //배열의 삭제는 복사를 통해서 이루어진다.
            //삭제 할 위치 다음 위치 부터 끝까지 한칸씩 앞으로 이동된것과 같은 효과이다.
        }

        // capacity 가 모자를 때 배열의 크기를 늘려주는 함수
        private void Grow()
        {
            int newCapacity = items.Length * 2; // 임의로 2배로 늘려준다.
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size);
            items = newItems;
        }

        //Copies the entire List<T> to a compatible one-dimensional array
        public void CopyTo(T[] arr) 
        {
            for (int i = 0; i < size; i++)
            {
                arr[i] = items[i];
            }
        }

        //Copies the entire List<T> to a compatible one-dimensional array,
        //starting at the specified index of the target array
        public void CopyTo(T[] arr, Int32 startTarget) 
        {
            //예외처리
            if (startTarget < 0 || startTarget >= size)
                throw new IndexOutOfRangeException();
            for (int i = startTarget; i < size; i++)
            {
                arr[i] = items[i];
            }
        }

        //Copies a range of elements from the List<T> to a compatible one-dimensional array,
        //starting at the specified index of the target array.
        public void CopyTo(T[] arr, Int32 startTarget, Int32 endTarget)
        {
            //예외처리
            if (endTarget < 0 || endTarget >= size)
                throw new IndexOutOfRangeException();
            if (startTarget < 0 || startTarget >= size)
                throw new IndexOutOfRangeException();

            for (int i = startTarget; i <= endTarget; i++)
            { // endTarget 까지 반복해준다.
                arr[i] = items[i];
            }
        }

        //Determines whether the List<T> contains elements
        //that match the conditions defined by the specified predicate.
        public bool Exists(Predicate<T> match) 
        {
            if (match == null) throw new ArgumentNullException();

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return true;
            }

            return false;
        }

        //A List<T> containing all the elements that match the conditions defined
        //by the specified predicate, if found; otherwise, an empty List<T>.
        public DataStructure.Iter.List<T> FindAll(Predicate<T> match) 
        {
            if (match == null) throw new ArgumentNullException();

            DataStructure.Iter.List<T> result = new List<T>();

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    result.Add(items[i]);
            }

            return result;
        }


        //Performs the specified action on each element of the List<T>.
        public void ForEach(Action<T> action)
        {
            if (action == null) throw new ArgumentNullException();

            for (int i = 0; i < size; i++)
                action(items[i]);
        }

        //Copies the elements of the List<T> to a new array.
        public T[] ToArray() 
        {

            if (size <= 0)
            {
                return new T[0]; // 요소가없으면 빈 T[] 를 반환
            }

            T[] result = new T[size];

            for (int i = 0; i < size + 1; i++)
            {
                result[i] = items[i];
            }

            return result;
        }

        /// 반복자 기능 추가
        /*
         *   while (iter.MoveNext()) // 반복자의 다음이없으면 false 를 리턴한다.
         *   {
         *       Console.WriteLine(iter.Current);
         *   }
         *   iter.Dispose();
         *   
         * 반복자는 이런식으로 foreach 를 돌리는데
         * 처음 먼저 MoveNext를 해준다.
         * 즉 -1 부터 시작해줘야한다.
         * 주의: C#의 반복기는 prev 반복자가 기본이다.
         * 유효하지않은 하나의 위치를 지정해줘야하는데 그걸 앞에걸로 지정하는걸 prev 반복자라한다.
         *  // c에서는 prev 반복자가 아니라 맨뒤에값을 유효하지않은 위치로 지정한 반복자(end iterator)이다.
         */
        public struct Enumerator : IEnumerator<T>
        {
            private List<T> list;
            private int index;
            private T current;
            public T Current { get { return current; } }

            internal Enumerator(List<T> list)
            {
                this.list = list;
                this.index = -1;
                this.current = default(T); 
                // 첫값이 -1 즉 없는값이니 기본값으로 만들어둔다.
            }
            object IEnumerator.Current
            {
                get
                {
                    if (index < 0 || index >= list.Count)
                        throw new InvalidOperationException();//범위를 벗어났을때
                    return Current;
                }
            }
            //Disposable 관련이라 일단 패스
            //Dispose 는 foreach 반복이 끝났을때 불리는 함수
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                //GC에 정리하라고 알려줌.
            }

            public bool MoveNext()
            {
                if (index < list.Count - 1)
                {
                    current = list[++index];
                    return true;
                }
                else //인덱스가 범위를 벗어나면
                {
                    current = default(T);
                    index = list.Count;
                    return false;
                }
            }

            public void Reset()
            {
                index = -1;
                current = default(T);
            }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }
    }
}
