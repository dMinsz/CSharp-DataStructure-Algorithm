using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//미구현
namespace DataStructure
{
    //자가 균형 기능을 가진 Red Black Tree 구현
    internal class RedBlackTree<T> where T : IComparable<T>
    {
        #region Node
        //트리가 가질 노드 세팅
        public enum NodeType { Red, Black, None }
        public class Node
        {
            private T item;
            private NodeType type;
            private Node? parent; // 부모값
            private Node? left; // 왼쪽 자식
            private Node? right; // 오른쪽 자식

            public Node() 
            {
                this.item = default(T);
                this.parent = null;
                this.left = null;
                this.right = null;
                type = NodeType.Red;
            }

            public Node(T item, Node? parent, Node? left, Node? right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
                type = NodeType.Red;
            }

            public T Item { get { return item; } set { item = value; } }
            public Node? Parent { get { return parent; } set { parent = value; } }
            public Node? Left { get { return left; } set { left = value; } }
            public Node? Right { get { return right; } set { right = value; } }
            public bool HasLeftChild { get { return left != null; } }
            public bool HasRightChild { get { return right != null; } }
            public bool IsRootNode { get { return parent == null; } }
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }

            public NodeType GetType()
            {
                return type;
            }
            public void ChangeType()
            {
                if (this.type == NodeType.Red)
                {
                    this.type = NodeType.Black;
                }
                else
                {
                    this.type = NodeType.Red;
                }
            }

            public void ChangeType(NodeType type) { this.type = type; }
        }
        #endregion

        private Node? root;
        private Node? leafNode;
        public int size { get { return size; } set { } }

        public RedBlackTree()
        {
            leafNode = new Node();
            leafNode.ChangeType(NodeType.Black);
            root = leafNode; 
            size = 0; 
        }

        public bool IsEmpty() { return size == 0; }

        //왼쪽으로 회전
        public void RotateLeft(Node node) 
        {
            Node? rightChild = node.Right;

            if (rightChild == null)
            {
                throw new InvalidOperationException();
            }

            node.Right = rightChild.Left;

            if (rightChild.Left == null)
            {
               rightChild.Left.Parent = node;
            }
            rightChild.Parent = node.Parent;

            if (node.IsRootNode)
            {
                this.root = rightChild;
            }
            else if (node.IsLeftChild)
            {
                node.Parent.Left = rightChild;
            }
            else 
            {
                node.Parent.Right = rightChild;
            }

            node.Parent = rightChild;
            rightChild.Left = node;
        }

        //오른쪽으로 회전
        public void RotateRight(Node node)
        {
            Node? leftChild = node.Left;

            if (leftChild == null)
            {
                throw new InvalidOperationException();
            }

            node.Left = leftChild.Right;

            if (leftChild.Right == null)
            {
                leftChild.Right.Parent = node;
            }
            leftChild.Parent = node.Parent;

            if (node.IsRootNode)
            {
                this.root = leftChild;
            }
            else if (node.IsLeftChild)
            {
                node.Parent.Left = leftChild;
            }
            else
            {
                node.Parent.Right = leftChild;
            }

            node.Parent = leftChild;
            leftChild.Left = node;
        }

        public void AddFixUp(Node node) 
        {
            while (node != root && node.Parent.GetType() != NodeType.Red)
            {
                var parent = node.Parent;
                var grandParent = node.Parent.Parent;

                //부모가 왼쪽 자식일경우
                if (parent.IsLeftChild)
                {
                    Node? uncle = grandParent.Right;
                    // 삼촌이 빨간색일 경우
                    // 조부모를 빨간색으로 삼촌을 검은색으로 변경 후, 조부모를 기준으로 반복
                    if (null != uncle && uncle.GetType() == NodeType.Red)
                    {
                        grandParent.ChangeType(NodeType.Red);
                        parent.ChangeType(NodeType.Black);
                        uncle.ChangeType(NodeType.Black);

                        node = grandParent;
                    }
                    else
                    {// 삼촌이 검은색이며 현재 노드가 오른쪽 자식인 경우
                     // 부모를 기준으로 왼쪽회전 후, 새로 삽입한 노드를 기준으로 진행
                        if (node.IsRightChild)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }
                        // 삼촌이 검은색이며 현재 노드가 왼쪽 자식일 때
                        // 부모를 검은색으로 조부모를 빨간색으로 변경 후, 조부모를 기준으로 오른쪽으로 회전 후 반복
                        parent.ChangeType(NodeType.Black);
                        grandParent.ChangeType(NodeType.Red);
                        RotateRight(node);
                    }
                }
                else//부모가 오른쪽 자식인 경우
                {
                    // 삼촌이 검은색이며 현재 노드가 왼쪽 자식일 때
                    // 부모를 기준으로 오른쪽회전 후, 새로 삽입한 노드를 기준으로 반복
                    if (node.IsLeftChild)
                    {
                        node = node.Parent;
                        RotateRight(node);
                    }

                    // 삼촌이 검은색이며 현재 노드가 오른쪽 자식일 때
                    // 부모를 검은색으로 조부모를 빨간색으로 변경 후, 조부모를 기준으로 왼쪽으로 회전 후 반복
                    parent.ChangeType(NodeType.Black);
                    grandParent.ChangeType(NodeType.Red);
                    RotateLeft(grandParent);
                }
            }
            root.ChangeType(NodeType.Black);
        }

        public bool Add(T item) 
        {
            Node newNode = new Node(item,null,null,null);

            //1. 데이터가 비워져있을때
            if (this.IsEmpty()) 
            {
                this.root = newNode;
                size++;
                return true;
            }

            Node Current = this.root;

            while (true)
            {
                // 2-1. 추가 하는 데이터가 더 큰 경우
                if (Comparer<T>.Default.Compare(item, Current.Item) > 0)
                {
                    if (Current.HasRightChild)
                    {// 3-1. 오른쪽에 노드가 있는 경우, 오른쪽으로 가기.
                        Current = Current.Right;
                    }
                    else // 3-2. 오른쪽에 노드가 없는 경우, 그 위치에 추가
                    {
                        Current.Right = newNode;
                        newNode.Parent = Current;
                        break;
                    }

                }
                else if (Comparer<T>.Default.Compare(item, Current.Item) < 0)
                {// 2-2. 추가 하는 데이터가 더 작은 경우
                    if (Current.HasLeftChild)
                    {// 3-1. 왼쪽에 노드가 있는 경우, 왼쪽으로 가기.
                        Current = Current.Left;
                    }
                    else // 3-2. 왼쪽에 노드가 없는 경우, 그 위치에 추가.
                    {
                        Current.Left = newNode;
                        newNode.Parent = Current;
                        break;
                    }
                }
                else // 2-3. 추가 하는 데이터가 같은 경우, 무시
                {
                    return false;
                }

            }

            AddFixUp(newNode);
            size++;
            return true;

        }

        public Node Find(T item)
        {
            return new Node(item, null, null, null);
        }
        public void Erase(Node node)
        {
            if (this.IsEmpty())
            {
               throw new InvalidOperationException();
            }

            Node current = node;
            Node nextNode = leafNode;
            Node removeNode = leafNode;
            Node doubleBlackNode = leafNode;


            if (current.Left == leafNode || current.Right == leafNode)
            {
                removeNode = current;
                //nextNode = 
            }

        }

        public void Next(Node node) 
        {
            //예외처리, 하나도 없었을 때
            if (this.leafNode == this.root || null == this.root)
            {
                throw new InvalidOperationException();
            }

            Node current = this.root;
            Node leafNode = this.leafNode;

            if (leafNode != current.Right)
            {//오른쪽 자식이 있는 경우, 오른쪽 자식으로 가서 왼쪽 자식이 없을 때까지 내려감
             // 오른쪽 자식의 가장 작은 값이 당연히 다음 노드이다.


            }
        }
        public bool Remove(T item)
        {
            if (this.IsEmpty())
            {
                return false;
            }

            Node findNode = Find(item);

            if (false) // 노드가 끝에있으면
            {
                Erase(findNode);
            }
            return true;
        }

    }
}
