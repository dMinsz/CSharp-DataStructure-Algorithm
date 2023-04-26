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
        public enum Color
        {
            Red,
            Black
        }

        public class Node
        {
            public Node()
            {
                value = default(T);
                count = 0;
            }
            public Node(T val)
            {
                value = val;
                count = 0;
            }

            public Node(T val, Node parent)
            {
                this.parent = parent;
                value = val;
            }


            public T value;// 값
            public Color color = Color.Red; // 기본값은 빨강
            public Node parent; // 부모노드
            public Node left; // 왼쪽 자식노드
            public Node right; // 오른쪽 자식노드
            public int count;//이 값을 가진 요소의 수
        }


        private int iterations;//트리의 순회, 탐색을 위해 사용
        public Node root;
        public RedBlackTree()
        {
            root = null;
        }
        public RedBlackTree(T value)
        {
            root = new Node(value);
            root.color = Color.Black; // 루트는 검정
        }

        public int GetIerations()
        {
            var res = iterations;
            iterations = 0;
            return res;
        }

        public Node Find(T value)
        {
            var current = root;
            while (current != null && !EqualityComparer<T>.Default.Equals(current.value,value))
            {
                iterations++;
                if (Comparer<T>.Default.Compare(current.value, value) < 0)
                    current = current.right;
                else
                    current = current.left;
            }
            return current;
        }

        public List<Node> ToList()
        {
            var result = new List<Node>();
            AddNode(root);

            void AddNode(Node n)
            {
                if (n.left != null)
                    AddNode(n.left);

                result.Add(n);

                if (n.right != null)
                    AddNode(n.right);
            }
            return result;
        }

        public string PrintTree()
        {
            var resultString = "";
            PrintNode(root);

            void PrintNode(Node n)
            {
                if (n.left != null)
                    PrintNode(n.left);

                resultString += " "+ n.value;

                if (n.right != null)
                    PrintNode(n.right);
            }
            return resultString;
        }

        protected Node AddNode(Node node, T data)
        {
            iterations++;
            if (EqualityComparer<T>.Default.Equals(data,node.value))
            {
                node.count++;
                return node;
            }

            if (Comparer<T>.Default.Compare(data, node.value) < 0) 
            {
                if (node.left != null)
                    return AddNode(node.left, data);
                return node.left = new Node(data, node);
            }
            else 
            {
                if (node.right != null)
                    return AddNode(node.right, data);
                return node.right = new Node(data, node);
            }
        }

        public Node Add(T data)
        {

            if (root == null)
            {
                root = new Node(data);
                root.color = Color.Black;
                return root;
            }
            var n = AddNode(root, data);

            Insert(n);
            root.color = Color.Black;
            return n;
        }

        private void Insert(Node InputNode)
        {
            Node newNode = new Node();
            InputNode.color = Color.Red;
            while (InputNode != root && InputNode.parent.color == Color.Red)
            {
                if (InputNode.parent == InputNode.parent.parent.left)
                {
                    newNode = InputNode.parent.parent.right == null 
                        ? new Node(default(T), InputNode.parent.parent.right) { color = Color.Black } 
                        : InputNode.parent.parent.right;

                    if (newNode.color == Color.Red)
                    {
                        InputNode.parent.color = Color.Black;
                        newNode.color = Color.Black;
                        InputNode.parent.parent.color = Color.Red;
                        InputNode = InputNode.parent.parent;
                    }
                    else
                    {
                        if (InputNode == InputNode.parent.right)
                        {
                            InputNode = InputNode.parent;
                            rotate_left(InputNode);
                        }

                        InputNode.parent.color = Color.Black;
                        InputNode.parent.parent.color = Color.Red;
                        rotate_right(InputNode.parent.parent);
                    }

                }
                else
                {
                    newNode = InputNode.parent.parent.left == null 
                        ? new Node(default(T), InputNode.parent.parent.left) { color = Color.Black } 
                        : InputNode.parent.parent.left;
                    if (newNode.color == Color.Red)
                    {
                        InputNode.parent.color = Color.Black;
                        newNode.color = Color.Black;
                        InputNode.parent.parent.color = Color.Red;
                        InputNode = InputNode.parent.parent;
                    }
                    else
                    {
                        if (InputNode == InputNode.parent.left)
                        {
                            InputNode = InputNode.parent;
                            rotate_right(InputNode);
                        }

                        InputNode.parent.color = Color.Black;
                        InputNode.parent.parent.color = Color.Red;
                        rotate_left(InputNode.parent.parent);
                    }

                }
            }
        }

        //오른쪽 회전
        protected void rotate_right(Node n)
        {

            var newNode = new Node();
            if (n.left != null)
            {
                newNode = n.left;
                n.left = newNode.right;



                if (newNode.right != null)
                    newNode.right.parent = n;
                newNode.parent = n.parent;
                if (n.parent == null)
                    root = newNode;
                else if (n == n.parent.right)
                    n.parent.right = newNode;
                else
                    n.parent.left = newNode;
                newNode.right = n;
                n.parent = newNode;
            }
            else
            {
                newNode = n.parent;
                newNode.parent.right = n;
                n.parent = n;
                n.parent.right = newNode;
                n.parent = newNode.parent;
                newNode.parent = n;
                newNode.left = null;
            }

        }

        protected void rotate_left(Node n)
        {
            var newNode = new Node();
            if (n.right != null)
            {
                newNode = n.right;
                n.right = newNode.left;

                if (newNode.left != null)
                    newNode.left.parent = n;
                newNode.parent = n.parent;
                if (n.parent == null)
                    root = newNode;
                else if (n == n.parent.left)
                    n.parent.left = newNode;
                else
                    n.parent.right = newNode;
                newNode.left = n;
                n.parent = newNode;
            }
            else
            {
                newNode = n.parent;

                newNode.parent.left = n;
                n.parent = n;
                n.parent.left = newNode;
                n.parent = newNode.parent;
                newNode.parent = n;
                newNode.right = null;
            }
        }


        protected Node grandparent(Node n)
        {
            if ((n != null) && (n.parent != null))
                return n.parent.parent;
            else
                return null;
        }

        //найти дядю

        protected Node uncle(Node n)
        {
            Node g = grandparent(n);
            if (g == null)
                return null;
            if (n.parent == g.left)
                return g.right;
            else
                return g.left;

        }

    }
}
