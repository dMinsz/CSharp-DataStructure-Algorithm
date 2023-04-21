# Queue ���� ��� �� ����

## ����

[1. �⺻ ����](#queue-��-�⺻-����)

[2. ���� ���](#����-���)

[3. �⺻ ������ ����](#�⺻-������-����)

[4. Enqueue ����](#enqueue-����)

[5. IsFull �Լ� ���� 1](#�迭��-������-��-á����-Ȯ���ϴ�-���)

[6. Dequeue ����](#dequeue-����)

[7. IsFull �Լ� ���� 2](#�迭��-������-��-á����-Ȯ���ϴ�-���)

## Queue �� �⺻ ����

![Queue01](./Images/Queue-01.PNG)

**FIFO**(First in First OUT) ���Լ��� LILO(��������) ����� �ڷᱸ��

���� ���� �Էµ� �����Ͱ� (������� �� ������) ó���Ǿ���ϴ� ��Ȳ�� ������ �ڷᱸ��
�������� �Ͽ� ���ؼ� ����Ѵ�.

����: ��⿭, ������ ���(��⿭�� ����), �ʺ�켱Ž��(BFS) �˰���

## ���� ���

�������� ���� ������ְ����� C#�� ����� ������ �̿��Ͽ� LinekdList�� ����°� 
���� ���� ���� ����̴�.
[����� �������α��� �� ������ ���⼭ ����ȴ�.](https://github.com/dMinsz/CSharp-DataStructure-Algorithm/blob/master/4.StackAndQueue/Stack.cs)


�׷��� ���� �۾��� �ν��Ͻ�ȭ �� �����͸� GC�� ó���ϰԵǾ� �̺κ��� �ſ� ��ȿ�����̴�.

���� �迭�� �̿��Ͽ� �����Ϸ����Ѵ�.

�迭�� �̿��Ͽ� �����Ϸ��� ���� ������ �迭�� ����ؾ��Ѵ�.

![Queue02](./Images/Queue-02.PNG)

������ �׸��� �ڵ�� �������ڸ� �Ʒ��� ����.
## �⺻ ������ ����
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

## Enqueue ����
### (�� �߰� �Լ�)

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

![Queue03](./Images/Queue-03.PNG)

## �迭�� ������ �� á���� Ȯ���ϴ� ���
### (IsFull �Լ� ���� 1)

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

## Dequeue ����
#### (������ �Լ�)

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


## �迭�� ������ �� á���� Ȯ���ϴ� ��� 2
#### (IsFull �Լ� ���� 2)

![Queue04](./Images/Queue-04.PNG)

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