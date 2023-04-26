using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    #region 참고설명
    //GetHashCode() 함수설명
    // (SHA 방식으로) 해시 코드를 주는 함수 // 최대한 겹치지않는 일정한 길이의 수 를 준다.
    // 원래는 키값을 자릿수접기,나눗셈법, 또는 여러 규칙으로 해싱값을 만들어야하는데
    // C# 에서는 해당함수를 사용하는걸 권장한다.

    // 공간을 늘리는 hashTable은 70~80 프로 공간이 찼으면 공간을 늘리고 재해싱을 통해 재배치 해준다.
    #endregion

    //IDictionary<TKey, TValue> 인터페이스가 있지만 사용하지않고 구현
    //이중해싱을 이용할것이며 공간을 늘리진 않을것이다.
    internal class HashTable<TKey, TValue> where TKey : IEquatable<TKey>
    {
        //크기늘리는건 안만들거라 일단은 const 키워드 사용
        private const int DefaultCapacity = 1000; // 기본 공간크기 를 크게 잡아둔다.

        private struct Entry
        { // 키와 밸류값을 같이 갖고있는게 유용하다.
            public enum State { None, Using, Deleted }//현재 공간을 사용하지않음,사용중,지웠음
                                                      //Deleted 는 삭제시 빈공간으로 인식하지않게 하기위해 만듬
            public int hashCode;
            public State state;
            public TKey key;
            public TValue value;
        }
        private enum InsertionBehavior { None, OverrideExist, ThrowOnExisting }
        // Data 삽입시 행동상태 정의
        // 행동없음, 덮어쓰기, 공간이있는곳으로 넘기기

        private Func<TKey, int> hashFunc; // 함수를 전달받을 때 사용한다.
        private Entry[] table; // 해시테이블

        public HashTable()
        {//기본생성자
            table = new Entry[DefaultCapacity];
            hashFunc = HashFunc;
        }

        public HashTable(Func<TKey, int> hashFunc)
        {//함수를 전달 받을때
            this.table = new Entry[DefaultCapacity];
            this.hashFunc = hashFunc;
        }

        //Indexer
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (TryGetValue(key, out value))
                    return value;
                else // 테이블에서 해당하는 키를 못찾아올시
                    throw new KeyNotFoundException();
            }
            set
            {
                TryInsert(key, value, InsertionBehavior.OverrideExist);
                //값을 바꾸는것이라서 OverrideExist 를 사용한다.
            }
        }

        //데이터 추가
        public void Add(TKey key, TValue value)
        {
            TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
        }

        //데이터 추가 시도
        public bool TryAdd(TKey key, TValue value)
        {
            return TryInsert(key, value, InsertionBehavior.None);
        }

        //데이터 삭제
        public bool Remove(TKey key)
        {
            int index = FindIndex(key);

            if (index < 0)
            {
                return false;
            }
            else
            {
                table[index].state = Entry.State.Deleted;
                return true;
            }
        }

        //데이터 비우기
        public void Clear()
        {
            table = new Entry[DefaultCapacity];
        }
        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = FindIndex(key);

            if (index < 0) // 찾는 키가 없으면
            {
                value = default(TValue);
                return false;
            }
            else // 찾는 키가 있으면
            {
                value = table[index].value;
                return true;
            }
        }
        private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
        {

            int hashCode = hashFunc(key); // 해시코드를 얻는다.
            int index = Math.Abs(hashCode) % table.Length;//나눗셈으로 해싱된 인덱스를만든다.

            while (table[index].state == Entry.State.Using //충돌시 반복 해서 충돌 회피
                || table[index].state == Entry.State.Deleted) // 삭제된 상태일때도 값을 추가해줘야한다.
            {
                if (key.Equals(table[index].key)) //key 값이 해싱된 곳의 키값과 같다면
                {
                    switch (behavior)
                    {
                        case InsertionBehavior.OverrideExist: // 덮어쓰기
                            table[index].hashCode = hashCode;
                            table[index].key = key;
                            table[index].value = value;

                            if (table[index].state == Entry.State.Deleted)
                            {   //삭제된 곳에 값을 추가하면 상태를 변경해준다.
                                table[index].state = Entry.State.Using;
                            }
                            return true;
                        case InsertionBehavior.ThrowOnExisting: // 동일한 키값을 허용하지않는다.
                            throw new ArgumentException();
                        case InsertionBehavior.None:
                        default:
                            return false;
                    }
                }
                index = DoubleHash(index); // 재해싱
            }

            table[index].hashCode = hashCode;
            table[index].state = Entry.State.Using;
            table[index].key = key;
            table[index].value = value;
            return true;
        }

        private int FindIndex(TKey key)
        {
            int hashCode = hashFunc(key);
            int index = Math.Abs(hashCode) % table.Length;//해싱
            while (table[index].state == Entry.State.Using// 충돌상태거나 삭제된 상태일때
                    ||table[index].state == Entry.State.Deleted) // 삭제된 상태일때 는 재해싱만 해주면된다.
            {
                if (key.Equals(table[index].key))
                {                // 주어진 key가 테이블내에 있으면
                    return index;//찾은 인덱스 리턴
                }
                index = DoubleHash(index); //재해싱
            }

            return -1; // error
        }

        //이중 해싱(index를)
        private int DoubleHash(int index)
        {
            return ++index % table.Length;
            //선형 탐사 방식으로 한다.
            //제곱 탐사는 index*2 해주면된다.
        }

        // SHA 방식으로 키값을 해싱
        private int HashFunc(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return key.GetHashCode(); // 모든타입에대해 해시코드로 변환해준다.
        }
    }

   
}
