using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructure
{
    //트리구조는 노드기반구조여서 C#에서 지양되는 구조이지만
    //다른 형태로 만들수없기때문에 노드구조로 만든다.

    //중복을 허용하지 않는 이진탐색트리
    internal class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node? root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        //값추가 // 중복시 False 반환하고 데이터를 붙히지않는다.
        public bool Add(T item)
        {
            Node newNode = new Node(item, null, null, null);

            if (root == null)//root 가 null이면 즉 첫번째 추가이면
            {
                root = newNode;
                return true;
            }

            Node current = root;

            while (current != null) // 모든 노드를 순회한다.
            {
                if (item.CompareTo(current.Item) < 0) // item이 current item 보다 작을때
                {
                    if (current.Left != null) // 왼쪽노드가 비워져있지않을경우
                    {
                        current = current.Left; // current를 바꿔주어서 깊이를 내려간다.
                    }
                    else
                    { // 왼쪽노드가 비워져있을경우 newNode 를 왼쪽자식으로 붙혀준다.
                        current.Left = newNode;
                        current.Parent = current;
                        break;
                    }
                }
                else if (item.CompareTo(current.Item) > 0)//item이 current item 보다 클때
                {
                    if (current.Right != null)
                    {//오른쪽노드의 값이 있을경우
                        current = current.Right; // current를 바꿔주어 깊이를 내려간다.
                    }
                    else
                    {// 오른쪽노드가 비워져있을경우 newNode 를 오른쪽자식으로 붙혀준다.
                        current.Right = newNode;
                        current.Parent = current;
                        break;
                    }
                }
                else //중복된 값이면 false 를 준다.
                {
                    return false;
                }

            }
            return true;
        }

        //값을 찾아서 노드 반환
        private Node? FindNode(T item)
        {
            if (root == null)
            {
                return null;
            }

            Node? current = root;

            while (current != null)//노드 순회
            {
                if (item.CompareTo(current.Item) < 0) //item이 current item 보다 작을때
                {
                    current = current.Left;//왼쪽으로 깊이를 내려간다.
                }
                else if (item.CompareTo(current.Item) > 0)//item이 current item 보다 클때
                {
                      current = current.Right;//오른쪽으로 깊이를 내려간다.
                }
                else //똑같은경우
                {
                    return current;
                }
            }

            return null;
        }

        //노드 삭제
        private void EraseNode(Node node)
        {
            if (node.HasNoChild) // 노드가 자식이없을때
            {
                if (node.IsLeftChild) // 만약 노드가 왼쪽자식일때
                    if(node.Parent != null) /// 굳이안해도되지만 워링이떠서 체크해줬다.
                        node.Parent.Left = null; // 스스로를 연결을 끊는다
                else if (node.IsRightChild)// 만약 노드가 오른쪽자식일때
                    if (node.Parent != null)/// 굳이안해도되지만 워링이떠서 체크해줬다.
                            node.Parent.Right = null;// 스스로를 연결을 끊는다
                else
                    root = null; // 왼쪽 자식도아니고 오른쪽자식도아니면 root가된다.
            }
            else if (node.HasLeftChild || node.HasRightChild) //자식이 하나 있을 때
            {
                if (node.Parent != null) /// 굳이안해도되지만 워링이떠서 체크해줬다.
                {                           ///이미 else if 위에 조건에서 체킹이된다.
                    Node parent = node.Parent;
                    Node? child = node.HasLeftChild ? node.Left : node.Right;

                    if (child != null)
                    {// error check
                        if (node.IsLeftChild)
                        { // node의 자식이 왼쪽노드일때 왼쪽노드 를 node의 부모와 연결해준다.
                            parent.Left = child;
                            child.Parent = parent;
                        }
                        else if (node.IsRightChild)
                        {// node의 자식이 오른쪽노드일때 오른쪽노드 를 node의 부모와 연결해준다.
                            parent.Right = child;
                            child.Parent = parent;
                        }
                        else
                        { // 왼쪽자식도 아니고 오른쪽 자식도아니면 본인이 루트이다.
                            root = child;
                            child.Parent = null;
                        }
                    }
                }
               
            }
            else //자식이 두개일때
            {   // 자식노드중 더 큰 값인 오른쪽 노드 기준으로 (오른쪽 자식 노드중에 가장 작은값구해서 교체)
                // 오른쪽 노드의 왼쪽 자식 노드에 깊이를 내려가면서 가장 아래에있는 노드를 
                // 현재 노드와 교체하고 가장 아래있는 노드는 삭제하면된다.
                //참고: 자식노드중 더 작은 값인 왼쪽 노드 기준으로 가장 큰 값을 구해서 교체하는 것도가능하다.
                if (node.Right != null)
                {//warning check
                    Node nextNode = node.Right;
                    while (nextNode.Left != null)
                    { //오른쪽자식의 다음 자식의 왼쪽 으로 깊이탐색
                        nextNode = nextNode.Left;
                    }
                    //왼쪽 자식이 없을때 까지 깊이를 내려간상황
                    node.Item = nextNode.Item; 
                    // (node 의 자식 노드중) 오른쪽 노드를 기준으로 가장 아래있는 왼쪽 자식 값은
                    // (node 의 왼쪽 자식노드) 보다 크기 때문에 위치를 교체해주고 삭제하면된다.
                    EraseNode(nextNode); // 자식노드가 없는 상태가 될것이다.
                }
            }
            
        }

        //값을 찾아서 해당하는 노드 삭제
        public bool Remove(T item)
        {
            if (root == null)
            {
                return false;
            }

            Node? findNode = FindNode(item); //찾아본다.

            if (findNode == null) //찾은 값이 없을때
            {
                return false;
            }
            else//찾은값이 있을때
            {
                EraseNode(findNode);//삭제
                return true;
            }

        }

        //값찾기
        public bool TryGetValue(T item, out T? outValue)
        {
            if (root == null) // 노드가 구성이안되어있을때
            {
                outValue = default(T);
                return false;
            }

            Node? findNode = FindNode(item);
            if (findNode == null) // 찾을 값의 노드가 없을때
            {
                outValue = default(T);
                return false;
            }
            else
            {//해당하는 노드를 찾았을때
                outValue = findNode.Item;
                return true;
            }
        }

        //트리 비우기
        public void Clear()
        {
            root = null;
        }
        #region Node 클래스
        private class Node
        {
            private T item;
            private Node? parent; // 부모값
            private Node? left; // 왼쪽 자식
            private Node? right; // 오른쪽 자식

            public Node(T item, Node? parent, Node? left, Node? right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }

            public T Item { get { return item; } set { item = value; } }
            public Node? Parent { get { return parent; } set { parent = value; } }
            public Node? Left { get { return left; } set { left = value; } }
            public Node? Right { get { return right; } set { right = value; } }

            public bool IsRootNode { get { return parent == null; } }
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }

            public bool HasNoChild { get { return left == null && right == null; } }
            public bool HasLeftChild { get { return left != null && right == null; } }
            public bool HasRightChild { get { return left == null && right != null; } }
            public bool HasBothChild { get { return left != null && right != null; } }
        }
        #endregion
    }
}
