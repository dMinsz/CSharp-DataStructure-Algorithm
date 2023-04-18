using System;
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

namespace DataStructure
{
    internal class List<T>
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

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();

                return items[index];
            }
            set
            {
                if (index < 0 || index >= size)
                    throw new IndexOutOfRangeException();

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
            {
                Grow();
            }

        }

        public void Clear()
        {
            items = new T[DefaultCapacity];
            size = 0;
        }

        public T? Find(Predicate<T> match)
        {
            if (match == null) throw new ArgumentNullException();

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }

            return default(T);
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

            int endIndex = startIndex + count;
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

            T result = default(T);

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

            int result = -1;

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

            int result = -1;

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

            int result = -1;

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
        }

        private void Grow()
        {
            int newCapacity = items.Length * 2;
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
        public DataStructure.List<T> FindAll(Predicate<T> match) 
        {
            if (match == null) throw new ArgumentNullException();

            DataStructure.List<T> result = new List<T>();

            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    result.Add(items[i]);
            }

            if (result.Count > 0)
            {
                return result;
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
                return new T[0];
            }

            T[] result = new T[size];

            for (int i = 0; i < size + 1; i++)
            {
                result[i] = items[i];
            }

            return result;
        }
    }
}
