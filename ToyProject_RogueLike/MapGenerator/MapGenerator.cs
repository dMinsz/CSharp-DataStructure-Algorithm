using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RogueLike;
// 벽 1
// 복도 2
// 방 3
namespace RogueLike.GMap
{
  
    public struct Line
    {
        public Position start;
        public Position end;

        private Direction direction;

        public Line(Position start, Position end, Direction dir) 
        {
            this.start = start;
            this.end = end;
            this.direction = dir;
        }
        public bool IsHorizontal() 
        {
           
            if (direction == Direction.HORIZONTAL)
                 return true;
            else
                 return false;
        }
    }

    public enum Direction { VERTICAL, HORIZONTAL } // 맵을 만들때 나누는 방식

    public class MapNode
    {
        public Position bottomLeft, topRight; // 사각형을 기준 으로 구분할것이기때문에
                                              //사각형을 만들수있는 최소단위인 가상으로 점두개를 만든다.

        public MapNode parent;
        public MapNode left, right;

        public bool isDivided = false; // 나눠지는건 한번

        public int depth;

        public Position roomBL, roomTR; // botomLeft, TopRight 이다.
                                         //현재 노드가 트리구조의 마지막 노드라면 방을 만드는 기초가 되므로
                                         // roomBL, roomTR으로 직사각형 방의 형태를 잡습니다.


        private Direction direction;


        public MapNode() { }
        public MapNode(Position botomLeft, Position topRight)
        {
            this.bottomLeft = botomLeft;
            this.topRight = topRight;
        }


        public void SetDireciton() // 방을 나누는 방식 세팅
        {
            //가로세로 를 비교하여 더 긴쪽을 나누는 방식으로 방을 나눈다.
            if (topRight.x - bottomLeft.x > topRight.y - bottomLeft.y)
                direction = Direction.VERTICAL;

            else if (topRight.x - bottomLeft.x < topRight.y - bottomLeft.y)
                direction = Direction.HORIZONTAL;

            else
            {
                Random rand = new Random();
                int temp = rand.Next(1, 3);
                if (temp == 1) direction = Direction.VERTICAL;
                else direction = Direction.HORIZONTAL;
            }
        }

        public bool GetDirection()
        {
            if (direction == Direction.VERTICAL)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 노드의 직사각형을 2개로 나누어 자식노드로 만든다.
        /// </summary>
        /// <param name="ratio"></param>
        /// <param name="minSize">직사각형이 최소로 가져야 할 길이</param>
        /// <returns></returns>
        public bool DivideNode(int ratio, int minSize)
        {
            float temp;
            Position divideLine1, divideLine2;
            if (direction == Direction.VERTICAL)
            {
                //노드를 수직으로 나눴으면
                // 왼쪽 노드가 Left , 오른쪽 노드가 Right
                temp = (topRight.x - bottomLeft.x);
                temp = temp * ratio / 100;
                int width = (int)temp;
                if (width < minSize || topRight.x - bottomLeft.x - width < minSize)
                    return false;
                divideLine1 = new Position(bottomLeft.x + width, topRight.y);
                divideLine2 = new Position(bottomLeft.x + width, bottomLeft.y);
            }
            else
            {   // 노드를 수평으로 나누었으면
                //아래의 노드를 left, 위의 노드를 right
                temp = (topRight.y - bottomLeft.y);
                temp = temp * ratio / 100;
                int height = (int)temp;
                if (height < minSize || topRight.y - bottomLeft.y - height < minSize)
                    return false;
                divideLine1 = new Position(topRight.x, bottomLeft.y + height);
                divideLine2 = new Position(bottomLeft.x, bottomLeft.y + height);
            }
            left = new MapNode(bottomLeft, divideLine1);
            right = new MapNode(divideLine2, topRight);
            left.parent = right.parent = this;
            isDivided = true;
            return true;
        }

        // 마지막 노드에 방에대한 좌표를 정하는 함수
        public void CreateRoom()
        {
            int distanceFrom = 2;//노드의 안쪽에
                                 //distanceFrom 변수의 크기만큼
                                 //방을 둘러싼 벽의 좌표를 구합니다.

            if (!isDivided)
            {
                roomBL = new Position(bottomLeft.x + distanceFrom, bottomLeft.y + distanceFrom);
                roomTR = new Position(topRight.x - distanceFrom, topRight.y - distanceFrom);
            }
        }




    }

    // 맵 자동생성
    // BST 이진탐색트리이용

    public class Generator
    {
        private List<MapNode> treeList; // 이진탐색트리
        private List<MapNode> roomList; // 마지막노드들로 만든 방 리스트

        private List<Line> lineList; // 방과 방사이를 이어주는 라인(복도) 리스트

        private Position bottomLeft, topRight; // 최초 맵의 사이즈 및 위치

        private int roomMinSize = 5; // 방을 구성하는 너비와 높이 최소값
        private int maxDepth; // 트리 의 최대 깊이
        private int minDepth = 0;

        private Datas.Tile[,] map; // 맵
                             

        public Generator() 
        {
            treeList = new List<MapNode>();
            roomList = new List<MapNode>();
            lineList = new List<Line>();

        }

        public Datas.Tile[,] GenerateMap(int width, int height , int MaxDepth)
        {
            this.bottomLeft = new Position(0, 0);
            this.topRight = new Position(width,height);

            this.maxDepth = MaxDepth;

            map = new Datas.Tile[height + 1, width + 1];
        
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    map[y, x] = Datas.Tile.NONE;

            treeList.Clear();
            roomList.Clear();
            lineList.Clear();

            MapNode root = root = new MapNode(bottomLeft, topRight);
            treeList.Add(root);
            MakeTree(ref root, minDepth);
            ToMakeRoom();
            ConnectRoom();
            ExtendLine();
            BuildWall();

            return map;
        }

        //트리생성
        //현재 짝수만 가능
        public void MakeTree(ref MapNode node, int depth)
        {
            node.depth = depth;
            if (depth >= maxDepth)
                return;
            Random rand = new Random();
            int dividedRatio = rand.Next(40, 81);
            //노드를 나눌때 자식노드가 가질수 있는 노드의 넓이를 40~81프로로 정해둠
            depth++;
            node.SetDireciton();
            if (node.DivideNode(dividedRatio, roomMinSize))
            {
                MakeTree(ref node.left, depth);
                MakeTree(ref node.right, depth);
                treeList.Add(node.left);
                treeList.Add(node.right);
            }
        }

        public void ToMakeRoom()
        { //모든 노드들을 방으로 만들어준다.
            for (int x = 0; x < treeList.Count; x++)
            {
                treeList[x].CreateRoom();

                if (!treeList[x].isDivided)
                {
                    for (int ry = treeList[x].roomBL.y; ry <= treeList[x].roomTR.y; ry++)
                    {
                        for (int rx = treeList[x].roomBL.x; rx <= treeList[x].roomTR.x; rx++)
                        {
                            if (rx == treeList[x].roomBL.x || rx == treeList[x].roomTR.x || ry == treeList[x].roomBL.y || ry == treeList[x].roomTR.y)
                                map[ry, rx] = Datas.Tile.WALL; //벽
                            else
                                map[ry, rx] = Datas.Tile.ROOM; // 방
                        }
                    }
                    roomList.Add(treeList[x]);
                }

            }
        }

        void ConnectRoom() // 방연결
        {
            // 같은 부모노드를 가지는 노드끼리 서로 연결
            for (int x = 0; x < treeList.Count; x++)
            {
                for (int y = 0; y < treeList.Count; y++)
                {
                    if (treeList[x] != treeList[y] && treeList[x].parent == treeList[y].parent)
                    {
                        if (treeList[x].parent.GetDirection())
                        {
                            int temp = (treeList[x].parent.left.topRight.y + treeList[x].parent.left.bottomLeft.y) / 2;
                            Line line = new Line(new Position(treeList[x].parent.left.topRight.x - 2, temp), new Position(treeList[y].parent.right.bottomLeft.x + 2, temp), Direction.VERTICAL);
                            lineList.Add(line);
                            MarkLineOnMap(line);
                        }
                        else
                        {
                            int temp = (treeList[x].parent.left.topRight.x + treeList[x].parent.left.bottomLeft.x) / 2;
                            Line line = new Line(new Position(temp, treeList[x].parent.left.topRight.y - 2), new Position(temp, treeList[y].parent.right.bottomLeft.y + 2), Direction.HORIZONTAL);
                            lineList.Add(line);
                            MarkLineOnMap(line);
                        }
                    }
                }
            }
        }

        void MarkLineOnMap(Line line)
        {
            if (line.start.x == line.end.x)
                for (int y = line.start.y; y <= line.end.y; y++)
                    map[y, line.start.x] = Datas.Tile.CORRIDOR;
            else
                for (int x = line.start.x; x <= line.end.x; x++)
                    map[line.start.y, x] = Datas.Tile.CORRIDOR;
        }

        // 마지막노드들 말고 다른 노드들도 모두 연결하는 함수
        void ExtendLine()
        {
            for (int x = 0; x < lineList.Count; x++)
            {
                if (lineList[x].IsHorizontal())
                {
                    while (true)
                    {
                        int lx = lineList[x].start.x;
                        int ly = lineList[x].start.y;
                        if (lx == map.GetLength(1)-1)
                            break;
                        if (lx == 0) break;
                        if (map[ly, lx - 1] == Datas.Tile.NONE || map[ly, lx - 1] == Datas.Tile.WALL)
                        {
                            if (map[ly + 1, lx] == Datas.Tile.ROOM || map[ly - 1, lx] == Datas.Tile.ROOM) // 위아래 체크해서 방일때 까지 돈다.
                                break;
                            map[ly, lx - 1] = Datas.Tile.CORRIDOR;

                            Line temp = lineList[x];
                            temp.start.x = lx - 1;
                            lineList[x] = temp;

                        }
                        else break;
                    }
                    while (true)
                    {
                        int lx = lineList[x].end.x;
                        int ly = lineList[x].end.y;
                        if (lx == map.GetLength(1)-1)
                            break;
                        
                        if (map[ly, lx + 1] == Datas.Tile.NONE || map[ly, lx + 1] == Datas.Tile.WALL)
                        {
                            if (map[ly + 1, lx] == Datas.Tile.ROOM || map[ly - 1, lx] == Datas.Tile.ROOM) // 위아래 체크해서 방일때까지 돈다.
                                break;
                            map[ly, lx + 1] = Datas.Tile.CORRIDOR;

                            Line temp = lineList[x];
                            temp.end.x = lx + 1;
                            lineList[x] = temp;
                        }
                        else break;
                    }
                }
                if (!lineList[x].IsHorizontal())
                {
                    while (true)
                    {
                        int lx = lineList[x].end.x;
                        int ly = lineList[x].end.y;
                        if (ly == map.GetLength(0)-1)
                            break;
                        if (map[ly + 1, lx] == Datas.Tile.NONE || map[ly + 1, lx] == Datas.Tile.WALL)
                        {
                            if (map[ly, lx + 1] == Datas.Tile.ROOM || map[ly, lx - 1] == Datas.Tile.ROOM) // 양옆을 체크해서 방일때까지 돈다.
                                break;
                            map[ly + 1, lx] = Datas.Tile.CORRIDOR;
                            Line temp = lineList[x];
                            temp.end.y = ly + 1;
                            lineList[x] = temp;
                        }
                        else break;
                    }
                    while (true)
                    {
                        int lx = lineList[x].start.x;
                        int ly = lineList[x].start.y;
                        if (ly == map.GetLength(0)-1)
                            break;
                        if (ly == 0) break;
                        if (map[ly - 1, lx] == Datas.Tile.NONE || map[ly - 1, lx] == Datas.Tile.WALL)
                        {
                            if ( map[ly, lx + 1] == Datas.Tile.ROOM || map[ly, lx - 1] == Datas.Tile.ROOM) // 양옆을 체크 해서 방일때까지 돈다.
                                break;
                          
                            map[ly - 1, lx] = Datas.Tile.CORRIDOR;
                            Line temp = lineList[x];
                            temp.start.y = ly - 1;
                            lineList[x] = temp;
               
                        }
                        else break;
                    }
                }
            }
        }

        // 모든 방생성을 마치면 방도 복도도 아닌곳을 모두 벽으로 만든다.
        void BuildWall()
        {
            for (int y = 0; y < topRight.y; y++)
            {
                for (int x = 0; x < topRight.x; x++)
                {
                    if (map[y,x] == Datas.Tile.NONE || map[y, x] == 0)
                    {
                        map[y, x] = Datas.Tile.WALL;
                    }
                }
            }

            for (int x = 0; x < map.GetLength(1); x++)
            {
                map[0,x] = Datas.Tile.WALL;
                map[map.GetLength(0)-1,x] = Datas.Tile.WALL;
            }

            for (int y = 0; y < map.GetLength(0); y++)
            {
                map[y, 0] = Datas.Tile.WALL;
                map[y, map.GetLength(1)-1] = Datas.Tile.WALL;
            }
        }


        public void PrintMap() 
        {
            StringBuilder sb = new StringBuilder();
            //StringBuilder sb2 = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    //sb2.Append(map[y, x]);
                    switch (map[y, x])
                    {
                        case Datas.Tile.WALL: // 벽
                            sb.Append($"{"X",2}");
                            break;
                        case Datas.Tile.CORRIDOR: // 복도
                            sb.Append($"{"",2}");
                            break;
                        case Datas.Tile.ROOM: // 방
                            sb.Append($"{"",2}");
                            break;
                        case Datas.Tile.NEXTSTAGE: // 방
                            sb.Append($"{"◎",2}");
                            break;
                    }
                }
                sb.AppendLine();
                //sb2.AppendLine();
            }
            Console.WriteLine(sb.ToString());
           // Console.WriteLine(sb2.ToString());
        }


        public Position GetRandomRoom() 
        {
           
            while (true)
            {
                Random rand = new Random();

                int y = rand.Next(0, map.GetLength(0));
                int x = rand.Next(0, map.GetLength(1));

                if (map[y, x] == Datas.Tile.ROOM)
                {
                    return new Position(x, y);
                }
                
            }
           
        }

        public void SetTile(Position pos , Datas.Tile tile)
        {
            if (map[pos.y, pos.x] == Datas.Tile.ROOM)
            {
                map[pos.y, pos.x] = tile;
            }
            
        }


    }
}
