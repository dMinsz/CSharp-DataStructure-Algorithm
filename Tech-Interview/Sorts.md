# ����

## ����

>[1. ���� ����](#����-����)
>>[1.1. ���� ����](#selection-sort)
>><br>[1.2. ���� ����](#insertion-sort)
>><br>[1.3. ���� ����](#bubble-sort)

>[2. �������� ����](#��������-����)
>>[2.1. �� ����](#heap-sort)
>><br>[2.2. �պ� ����](#merge-sort)
>><br>[2.3. �� ����](#quick-sort)

### �����̶�?

�������� ������ ��� ������ ��Ұ��踦 ���� ������ ������ ������ ����� ���̴�.

## ���� ����

�������� �񱳿� ���ؼ��� �����ϴ� �˰���

�ð����⵵ : O(N^2)

### Selection Sort
*(��������)*

���� ��� : ������ �� ���� ���� ������ �ϳ��� �����Ͽ� ����

���� ��� : ��ü �������� ���� ���� ���� �����Ͽ� ���� �տ� �δ� ���� �ݺ��Ͽ� ����		   

> 1. ��ü �迭�� �����ϸ鼭 ���� ���������ϱ�
> 2. ���� ���� ���� ���� �տ� ����
> 3. ���� �տ� �������� �װ��� ������ ��ü �迭�� �� Ž�� �׸��� 1.2. �ݺ�

<center><img src="./Images/SortsImages/selectionSort.gif"/></center>

```cs
    public static void SelectionSort(IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i; // ���� ���� ���� �ε���
                for (int j = i + 1; j < list.Count; j++)
                {  // j = i+1 �� ������ �̹� 0��° �ε��� ���� �������������� �����صξ
                    if (list[j] < list[minIndex]) // ���� ���� ���� ������ ���� ���� ��ȸ�ϸ鼭 ã�´�.
                    {
                        minIndex = j; // ���� ���� �������� �ٲ��ش�.
                    }
                }
                Swap(list, i, minIndex); // ��ü �迭�� ��ȸ�ؼ� ���� ���� ���� ã������ �Ǿտ� �־��ش�.
            }
        }
```


### Insertion Sort
*(���� ����)*

���� ��� : �����͸� �ϳ��� ������ ���ĵ� �ڷ� �� ������ ��ġ�� �����Ͽ� �����Ѵ�.

���� ��� :  

>1. �� ��° �ڷ���� �����Ͽ� 
>2. �� ��(����)�� �ڷ��� ���Ͽ� ������ ��ġ�� ����
>3. ������ ��ġ�� �ڷḦ �ڷ� �ű�� ������ �ڸ��� �ڷḦ �����Ͽ� ����

��, �� ��° �ڷ�� ù ��° �ڷ�, �� ��° �ڷ�� �� ��°�� ù ��° �ڷ�, �� ��° �ڷ�� �� ��°, �� ��°, ù ��° �ڷ�� ���� �� �ڷᰡ ���Ե� ��ġ�� ã�´�.

�ڷᰡ ���Ե� ��ġ�� ã�Ҵٸ� �� ��ġ�� �ڷḦ �����ϱ� ���� �ڷḦ �� ĭ�� �ڷ� �̵���Ų��.

 
<center><img src="./Images/SortsImages/insertionSort.gif"/></center>


 ```cs
        public static void InsertionSort(IList<int> list)
        {
            for (int i = 1; i < list.Count; i++) // �迭�� �ι�° �ڸ����� ����
            {
                int select = list[i]; // ���� ���õ� ������
                int j; 
                for (j = i-1; j >=0 && select < list[j]; j--) // ���õ� �������� ���� �ڷ�� ��� ���Ͽ�
                {                                           // ���õ� �������� ��ġ�� ã�Ƴ���.
                    list[j + 1] = list[j];// ��ġ�� ã������ �������� ��ĭ �ڷ� �̷� ��ġ�� �̵�
                }
                list[j + 1] = select; // ���õ� ��ġ�� ������ ����
            }
        }
 ```

 ### Bubble Sort
*(��ǰ ����)*

���� ��� : ���� ������ �����͸� ���Ͽ� ����

���� ��� :  

>1. ù �����͸� �����Ѵ�
>2. ������ �������� �� ���� ������ �� ���� �ʿ�� ������ �ٲ��ش�.
>3. ��� �迭�� ��ҿ� 2.���� �ݺ��Ѵ�.
>4. ��� �迭�� ��ҿ� �񱳰� �������� �� ���� �����͸� �����ϰ� 2.3.�� �迭ũ�⸸ŭ �ݺ����ش�.


<center><img src="./Images/SortsImages/bubbleSort.gif"/></center>


���� ������ ù ��° �ڷ�� �� ��° �ڷḦ, �� ��° �ڷ�� �� ��° �ڷḦ, 

�� ��°�� �� ��°��, �� �̷� ������ (������-1)��° �ڷ�� ������ �ڷḦ 

���Ͽ� ��ȯ�ϸ鼭 �ڷḦ �����Ѵ�.


```cs
 public static void BubbleSort(IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 1; j < list.Count; j++)
                {
                    if (list[j - 1] > list[j]) // ���� ������ ������ ��
                        Swap(list, j - 1, j); // �ʿ�� ������ ����
                }
            }
        }
```


## �������� ����

1���� ��Ҹ� ����ġ��Ű�� ���� ��ü�� **1/2**�� Ȯ���ϴ� ����

n���� ��Ҹ� ����ġ��Ű�� ���� **n/2**���� Ȯ���ϴ� ����

:fire: �ð����⵵ : O(NlogN)


### Heap Sort
*(�� ����)*
���� ��� : ���� �̿��Ͽ� �켱������ ���� ���� ��Һ��� ������ ����

���� ��� : �켱����ť�� �̿��Ͽ� �ְ� ���� �ڵ����� �����̵ȴ�.


<center><img src="./Images/SortsImages/heapSort.gif"/></center>



```cs
        public static void HeapSort(IList<int> list) 
        {
            PriorityQueue<int, int> heap = new PriorityQueue<int, int>();

            for (int i = 0; i < list.Count; i++)
            {
                heap.Enqueue(list[i], list[i]);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = heap.Dequeue();
            }

        }
```

**������**

�̻����� ��쿡 �ð����⵵�� O(NlogN)�� ������ ������

:warning: ���� �ð��� �����ϸ� **�ٸ� �������� ���ĵ� ���� ������**

�� ������ �������� �����ʹ� CPU �� ĳ�ÿ� �����Ͱ� �ö󰡼� 
�ݺ��۾��ϴ°� ��������

heap�� �� ������ �����͸� �پ��پ� Ž���ϱ⶧���� �Ϲ����� �迭�� Ž���Ҷ����� �ӵ��� ������.

:fire: **�������� ��������.** (4 5(1) 5(2) 9 �϶� ���������� 5 �� ��ġ�� ���������ִ�.)


### Merge Sort
*(�պ� ����)*

���� ��� : �����͸� 2�����Ͽ� ���� �� �պ��� �ݺ�

���� ��� : 

>1. ����Ʈ�� ���̰� 0 �Ǵ� 1�̸� �̹� ���ĵ� ������ ����. 
>2. �׷��� ���� ��쿡�� ���ĵ��� ���� ����Ʈ�� �������� �߶� ����� ũ���� �� �κ� ����Ʈ�� ������.
>3. �� �κ� ����Ʈ�� ��������� �պ� ������ �̿��� �����Ѵ�.
>4. �� �κ� ����Ʈ�� �ٽ� �ϳ��� ���ĵ� ����Ʈ�� �պ��Ѵ�.


<center><img src="./Images/SortsImages/mergeSort.gif"/></center>




```cs
        public static void MergeSort(IList<int> list, int left, int right)
        {
            if (left == right) return;

            int mid = (left + right) / 2;   // ������ ������ ��ġ ���
            MergeSort(list, left, mid);     // ���ҵ� ���� �κ� ����
            MergeSort(list, mid + 1, right);// ���ҵ� ������ �κ� ����
            Merge(list, left, mid, right);  // ���ĵ� 2���� �κ� �迭 ����
        }

        public static void Merge(IList<int> list, int left, int mid, int right)
        {
            List<int> sortedList = new List<int>();
            int leftIndex = left;
            int rightIndex = mid + 1;

            // ���� ���ĵ� List�� ����
            while (leftIndex <= mid && rightIndex <= right)
            {
                if (list[leftIndex] < list[rightIndex])
                    sortedList.Add(list[leftIndex++]);
                else
                    sortedList.Add(list[rightIndex++]);
            }

            if (leftIndex > mid)    // ���� List�� ���� ���� ���� ���
            {
                for (int i = rightIndex; i <= right; i++)
                    sortedList.Add(list[i]);
            }
            else  // ������ List�� ���� ���� ���� ���
            {
                for (int i = leftIndex; i <= mid; i++)
                    sortedList.Add(list[i]);
            }

            // ���ĵ� sortedList�� list�� �纹��
            for (int i = left; i <= right; i++)
            {
                list[i] = sortedList[i - left];
            }
        }
```

**������**

:warning: ���� �ϸ鼭 �޸� ������ �߰��� ����ϱ� ������ �޸𸮰� �����ϴٸ� �Ҹ��ϴ�.
 
:warning: �������� ���� (4 5(1) 5(2) 9 �϶� ���������� 5 �� ��ġ�� �ȱ�����.)


### Quick Sort
*(�� ����)*

���� ��� : �ϳ��� �ǹ��� �����Ͽ� �ǹ��� �������� 2���� �Ͽ� ����

���� ��� : 

> 1. �ϳ��� �ǹ��� ���Ѵ� (���� �迭�� �Ǿ�, �ǵ� ��ҷ� ����)
> 2. �ǹ��� �Ǿտ� ��ҷ� ���ߴٸ� �״��� ����� �ι�° ���(leftIndex)�� ���������(RightIndex)�� �����θ�����ش�.
> 3. leftIndex �� rightIndex�� ������ �����͸� ���Ͽ� �����͸� ��ȯ�Ѵ�.
> 4. ������ ��ȯ, Ȥ�� ��ȯ�����ʾҴ� �� Ȯ���� leftIndex �� ��ĭ ���������� rightIndex �� ��ĭ �������� �̵��Ѵ�. 
> 5. ���� leftIndex,rightIndex�� ������ �ȴٸ� �ǹ��� ���ĵ� ��ġ�� ���ԵȰ��̶� �Ǵ��ϰ� �ǹ��� ���� ��ġ�� ��ҷ� �ٲ��ش�.
> 6. ���� ������ �ݺ��ϸ� ��� �迭�� ��Ұ� �ѹ��� �� �ǹ��� �Ǿ����� ����


<center><img src="./Images/SortsImages/quickSort.gif"/></center>




```cs
        public static void QuickSort(IList<int> list, int start, int end)
        {
            if (start >= end) return;

            int pivotIndex = start;
            int leftIndex = pivotIndex + 1;
            int rightIndex = end;

            while (leftIndex <= rightIndex) // ������������ �ݺ�
            {
                // pivot���� ū ���� ����������
                while (list[leftIndex] <= list[pivotIndex] && leftIndex < end)
                    leftIndex++;
                while (list[rightIndex] >= list[pivotIndex] && rightIndex > start)
                    rightIndex--;

                if (leftIndex < rightIndex)     // �������� �ʾҴٸ�
                    Swap(list, leftIndex, rightIndex);
                else    // �����ȴٸ�
                    Swap(list, pivotIndex, rightIndex);
            }

            QuickSort(list, start, rightIndex - 1);
            QuickSort(list, rightIndex + 1, end);
        }
```

**������**

�߰����� �޸𸮸� �����ʱ� ������ �޸��� �δ��� ����.

:warning: �� �־��� ��쿡 ���Ľð��� �����ɸ����ִ�. (������ �ŲٷεǾ��������� �־��̴�.)

�־��� ��� �ð����⵵�� O(N^2)�� �Ǿ� ���� ���İ� �ٸ��� ��������.

