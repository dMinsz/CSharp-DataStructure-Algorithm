# Queue ���� ��� �� ����

### **Queue �� �⺻ ����**

![RemoveList](./Images/Queue-01.png)

**FIFO**(First in First OUT) ���Լ��� LILO(��������) ����� �ڷᱸ��

���� ���� �Էµ� �����Ͱ� ������� �� �����Ͱ� ���� ó���Ǿ���ϴ� ��Ȳ�� ������ �ڷᱸ��
�������� �Ͽ� ���ؼ� ���

����: ��⿭, ������ ���(��⿭�� ����), �ʺ�켱Ž��(BFS) �˰�����


### ���� ���

�������� ���� ������ְ����� C#�� ����� ������ �̿��Ͽ� LinekdList�� ����°� 
���� ���� ���������, ���� �۾��� �ν��Ͻ�ȭ �� �����͸� GC�� ó���ϰԵǾ� �̺κ��� �ſ� ��ȿ�����̴�.

���� �迭�� �̿��Ͽ� �����Ϸ����Ѵ�.

�迭�� �̿��Ͽ� �����Ϸ��� ���� ������ �迭�� ����ؾ��Ѵ�.

![RemoveList](./Images/Queue-02.png)

������ �׸��� �ڵ�� �������ڸ� �Ʒ��� ����.
#### �⺻ ������ ����
``` cs
//..�߷�
private const int DefaultCapacity = 4; //�⺻ ũ�⼳��
private T[] array;// ���� �����Ͱ� ����� ����
private int front;// ù �ε����� ����Ŵ
private int tail;// ������ �ε����� ����Ŵ

//..�߷�

 public Queue()
{
    array = new T[DefaultCapacity + 1]; 
    // ù���� ��ĭ�� ����Ű���־���ϱ⶧���� + 1
    front = 0;
    tail = 0;
}
```

�߿������� ù ��° ���� ��ĭ���� �ξ� Front�� rear�� ���Ͽ�
��ü �迭�� �� á���� Ȯ�� �� �Ҽ� �ִ�.

#### Enqueue ���� / �� �߰� �Լ�

���� �׸��� ���� ���� �߰������� ���ε� �̸� �ڵ�� ������

``` cs
 public void Enqueue(T item)
        {
            if (IsFull())
            {
                Grow(); // �迭�� ��á���� �÷��ش�.
            }

            array[tail] = item; // ���� tail ��ġ�� �־��ش�.
            MoveNext(ref tail); // ���������� �Ѱ��ش�.
        }
//.. �߷�
  private bool IsFull()
        {
            if (front > tail)
            {
                return front == tail + 1;
                // front �� �� ũ�� �ѹ��� ���Ŵ�.
                // �׻��¿����� front �� tail ������ �迭�� �������ε�
                // ��ĭ�� �ֱ⶧���� +1 ���ذ�
            }
            else
            {
                return front == 0 && tail == array.Length - 1;
                //front �� 0 �̸� ù�� && tail�� �迭 �����ִ�. => ������
            }
        }
//.. �߷�
private void MoveNext(ref int index)
        {
            index = (index == array.Length - 1) ? 0 : index + 1;
            // �ε����� ���������� ����Ű�� ù������(0) ���� �ε��� ����
            // �ƴϸ� +1 ���ָ�ȴ�.
        }
```

�� �ȴ�. Grow �Լ��� �������������� ������ ũ�⿡ ���� �迭�̵Ǵ� ���̰�
���� �ϸ� ũ�Ⱑ ���ڸ��� �ڵ����� ũ�⸦ �÷��ִ� ���� �迭�� �ȴ�.

``` cs
//Grow �Լ� ����
private void Grow()
        {
            int newCapacity = array.Length * 2;
            T[] newArray = new T[newCapacity + 1];
            if (!IsEmpty())
            {
                if (front < tail)
                {
                    Array.Copy(array, front, newArray, 0, tail);
                    //front �� tail ���� ������
                    //�״�� ī���ϸ�ȴ�.
                }
                else
                {
                    Array.Copy(array, front, newArray, 0, array.Length - front);
                    Array.Copy(array, 0, newArray, array.Length - front, tail);

                    //front�� tail���� ũ��
                    // 0 ���� array.Length - front �� �ѹ������� �߰��� �κи� ī��
                    // �ѹ��� ���� ���� �κ��� ī�����ش�. 
                    //array.Length - front ���� tail ������ �׺κ��� �ǹ��Ѵ�.
                }
            }

            array = newArray;
            tail = Count;
            front = 0;
        }
```

![RemoveList](./Images/Queue-03.png)

#### �迭�� ������ �� á���� Ȯ���ϴ� ��� / IsFull �Լ� ���� 1
���� �׸��� ���� �迭�� ���� ��� ������ ���� �׸��� ��Ÿ�����̴�.
�� front < tail �϶� front �� 0�̰� tail �� array�� ũ�� -1 �� ���ٸ�
�迭�� ������ �� �� ���̴�.

���� ������ ���� front�� �ѹ����� �� ���� �� �޶����� �� �Ʒ����� �� �������ؼ�
IsFull �Լ��� �ϼ��غ��ڴ�.

```cs
  private bool IsFull()
        {
            //�������ִ� �ڵ��̴�.
            //�Ʒ����� ����� �� ��������.
            if(front < tail)
            {
                return front == 0 && tail == array.Length - 1;
                //front �� 0 �̸� ù�� && tail�� �迭 �����ִ�. => ������
            }
        }
```

#### Dequeue ���� / ������ 

�����ʱ׸��� ���� ���� �۾� Dequeue �ÿ� �׸��� �����ְ��ִµ�
���� ������ Front ���� �ϳ� �̵���Ű�� �ű⿡ �ִ� ���� ���� ���ָ� �ȴ�.

```cs
public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            T result = array[front];
            MoveNext(ref front); // �������Ŀ��� �̵���������Ѵ�.
            return result;
        }

//�迭�� ������� Ȯ���ϴ� �Լ�
  private bool IsEmpty()
        {
            return front == tail; // queue�� �������
        }
```


#### �迭�� ������ �� á���� Ȯ���ϴ� ��� / IsFull �Լ� ���� 2

![RemoveList](./Images/Queue-04.png)

������ �׸��� �ռ� �� ������ ��� á���� �� �ι�° ��Ȳ�̴�.
�̰� �������� �ڵ带 �ϼ��غ��ڸ�

```cs
  private bool IsFull()
        {
            if (front > tail)
            {
                return front == tail + 1;
                // front �� �� ũ�� �ѹ��� ���Ŵ�.
                // �׻��¿����� front �� tail ������ �迭�� �������ε�
                // ��ĭ�� �ֱ⶧���� +1 ���ذ�
            }
            else
            {
                return front == 0 && tail == array.Length - 1;
                //front �� 0 �̸� ù�� && tail�� �迭 �����ִ�. => ������
            }
        }

```