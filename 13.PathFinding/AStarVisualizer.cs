using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PathFinding
{
    public class ASNode // A*의 정점을 의미한다.
    {
        public Point point;     // 현재 정점
        public Point? parent;   // 이 정점을 탐색한 정점

        public int g;           // 현재까지의 값, 즉 지금까지 경로 가중치
        public int h;           // 앞으로 예상되는 값, 목표까지 추정 경로 가중치
        public int f;           // f(x) = g(x) + h(x);

        public ASNode(Point point, Point? parent, int g, int h)
        {
            this.point = point;
            this.parent = parent;
            this.g = g;
            this.h = h;
            this.f = g + h;
        }
    }

    public enum Type { None, Visit, Select,Start,End , Block , Pass}
    //A* 비주얼라이징
    public class Qube
    {
        public string[] info = new string[5]; // top,f,g,h,bottom 가지고있음

        public Type type;
        public Qube()
        {
            info[0] = "-------";
            info[1] = $"|F={" ",3}|";
            info[2] = $"|G={" ",3}|";
            info[3] = $"|H={" ",3}|";
            info[4] = "-------";
            type = Type.None;
        }
        public Qube(ASNode? node)
        {
            info[0] = "-------";
            info[1] = $"|F={node.f,3}|";
            info[2] = $"|G={node.g,3}|";
            info[3] = $"|H={node.h,3}|";
            info[4] = "-------";
        }

        public void Set(ASNode? node)
        {
            if (node == null)
            {
                info[0] = "-------";
                info[1] = $"|F={" ",3}|";
                info[2] = $"|G={" ",3}|";
                info[3] = $"|H={" ",3}|";
                info[4] = "-------";
            }
            else
            {
                info[0] = "-------";
                info[1] = $"|F={node.f,3}|";
                info[2] = $"|G={node.g,3}|";
                info[3] = $"|H={node.h,3}|";
                info[4] = "-------";
            }
        }

        public void SetPass() { type = Type.Pass; }
        public void SetBlock() { type = Type.Block; }
        public void SetStart() { type = Type.Start; }
        public void SetEnd() { type = Type.End; }
        public void Visit() { type = Type.Visit; }
        public void Select() { type = Type.Select; }
    }
    internal class AStarVisualizer
    {
        static Point[] Direction =
        {
            new Point(  0, +1 ),			// 상
			new Point(  0, -1 ),			// 하
			new Point( -1,  0 ),			// 좌
			new Point( +1,  0 ),			// 우
			//new Point( -1, +1 ),		    // 좌상
			//new Point( -1, -1 ),		    // 좌하
			//new Point( +1, +1 ),		    // 우상
			//new Point( +1, -1 )		    // 우하
		}; // 방향을 위해 사용


        const int CostStraight = 10; // 상하좌우 를 10으로 가중치를 가정
        const int CostDiagonal = 14; // 대각선 // 루트 2 의 대략적인 값 의 가중치이다 // 피타고라스 법칙, 유클리드 거리 
                                     //이러한 가중치를 사용하면 맨해탄 거리 계산의 의해서 많은 수의 Path를 볼 필요가 없어진다.

        PriorityQueue<ASNode, int> nextPointPQ;
        ASNode[,] nodes;
        bool[,] visited;
        bool[,] tileMap;
        Qube[,] visualQubes;
        List<Point> path;
        Point start;
        Point end;
        //탐색 성공시 true , 실패시 false
        public static bool PathFinding(in bool[,] tileMap, in Point start, in Point end, out List<Point> path, out Qube[,] visualQubes)
        {
            int ySize = tileMap.GetLength(0); // 첫 차원을 y값으로 사용
            int xSize = tileMap.GetLength(1);

            ASNode[,] nodes = new ASNode[ySize, xSize]; // 각정점들을 의미하며 모든 정점을 담을수있게 생성
            bool[,] visited = new bool[ySize, xSize]; // 방문확인용

            //visualizing 을위해 Qube 값 세팅
            visualQubes = new Qube[ySize, xSize];

            for (int y = 0; y < nodes.GetLength(0); y++)
            {
                for (int x = 0; x < nodes.GetLength(1); x++)
                {
                    visualQubes[y, x] = new Qube();
                }
            }

            // F값이 제일 낮은거부터 사용하기위해 우선순위 큐 사용
            // 그래서 ASNode 값과 (int)F값 으로 우선순위 큐를 만들어준다.
            PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();

            // 0. 시작 정점을 생성하여 추가
            ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
            nodes[startNode.point.y, startNode.point.x] = startNode;

            visualQubes[startNode.point.y, startNode.point.x].Set(startNode);
            visualQubes[startNode.point.y, startNode.point.x].SetStart();
            visualQubes[end.y, end.x].SetEnd();

            nextPointPQ.Enqueue(startNode, startNode.f);

            while (nextPointPQ.Count > 0) //휴리스틱 값이 있을때 // 다음 경로로 이동가능할때
            {
                // 1. 다음으로 탐색할 정점 꺼내기
                ASNode nextNode = nextPointPQ.Dequeue();

                // 2. 방문한 정점은 방문표시
                visited[nextNode.point.y, nextNode.point.x] = true;
                visualQubes[nextNode.point.y, nextNode.point.x].Visit(); //방문으로 바꿈

                // 3. 다음으로 탐색할 정점이 도착지인 경우 // 도착했다고 판단해서 경로 반환
                if (nextNode.point.x == end.x && nextNode.point.y == end.y)
                {
                    Point? pathPoint = end; // 이동중인 포인트(정점)
                    path = new List<Point>();

                    while (pathPoint != null) // PathPoint 가 null이면 정점에 방문자가 더이상없는것
                    {
                        Point point = pathPoint.GetValueOrDefault();
                        //GetValueOrDefault 은 값이 있으면 값을 가져오고 null이면 디폴트값으로 가져온다.
                        path.Add(point); // path 에 값을 넣어줘서 이동하는 모든 정점을 순서대로 구할수있다.
                        pathPoint = nodes[point.y, point.x].parent;
                    }

                    path.Reverse(); // 목적지부터 계산했기때문에 거꾸로해줘야한다.


                    for (int i = 0; i < path.Count; i++)
                    {
                        visualQubes[path[i].y, path[i].x].Select();
                    }


                    return true;
                }

                // 4. AStar 탐색을 진행 // 방향 탐색
                for (int i = 0; i < Direction.Length; i++)//상하 좌우로 체크
                {
                    int x = nextNode.point.x + Direction[i].x; // 탐색해야할 좌표
                    int y = nextNode.point.y + Direction[i].y; // 상하좌우로 이동했을때 좌표 x,y

                    // 4-1. 탐색하면 안되는 경우
                    // 맵을 벗어났을 경우
                    if (x < 0 || x >= xSize || y < 0 || y >= ySize)
                        continue;
                    // 탐색할 수 없는 정점일 경우
                    else if (tileMap[y, x] == false)
                        continue;
                    // 이미 방문한 정점일 경우
                    else if (visited[y, x])
                        continue;

                    // 4-2. 탐색한 정점 만들기

                    //지금까지의 가중치 g
                    int g = nextNode.g + ((nextNode.point.x == x || nextNode.point.y == y) ? CostStraight : CostDiagonal);
                    int h = Heuristic(new Point(x, y), end); // 목표 지점까지의 가중치 추산
                    ASNode newNode = new ASNode(new Point(x, y), nextNode.point, g, h); // 이동했을때 이동한곳에 node를 만들어준다. 


                    // 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
                    if (nodes[y, x] == null ||      // 탐색하지 않은 정점이거나
                        nodes[y, x].f > newNode.f)  // 가중치가 높은 정점인 경우
                    {
                        nodes[y, x] = newNode;
                        nextPointPQ.Enqueue(newNode, newNode.f);

                        //visualize
                        visualQubes[y, x].Set(newNode);
                        visualQubes[y, x].Visit();
                    }
                }
            }
            //경로를 못찾을 경우
            path = null;
            return false;
        }

        public void Init(in bool[,] tileMap, in Point start, in Point end) 
        {
            this.tileMap = tileMap;
            int ySize = this.tileMap.GetLength(0); // 첫 차원을 y값으로 사용
            int xSize = this.tileMap.GetLength(1);

            this.start = start;
            this.end = end;

            nodes = new ASNode[ySize, xSize]; // 각정점들을 의미하며 모든 정점을 담을수있게 생성
            visited = new bool[ySize, xSize]; // 방문확인용

            //visualizing 을위해 Qube 값 세팅
            visualQubes = new Qube[ySize, xSize];

            for (int y = 0; y < nodes.GetLength(0); y++)
            {
                for (int x = 0; x < nodes.GetLength(1); x++)
                {
                    if (tileMap[y, x] == false) // 블록체크
                    {
                        visualQubes[y, x] = new Qube();
                        visualQubes[y, x].type = Type.Block;
                    }
                    else 
                    {
                        visualQubes[y, x] = new Qube();
                    }
                }
            }

            // F값이 제일 낮은거부터 사용하기위해 우선순위 큐 사용
            // 그래서 ASNode 값과 (int)F값 으로 우선순위 큐를 만들어준다.
            nextPointPQ = new PriorityQueue<ASNode, int>();

            // 0. 시작 정점을 생성하여 추가
            ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
            nodes[startNode.point.y, startNode.point.x] = startNode;

            visualQubes[startNode.point.y, startNode.point.x].Set(startNode);
            visualQubes[startNode.point.y, startNode.point.x].SetStart();
            visualQubes[end.y, end.x].SetEnd();

            nextPointPQ.Enqueue(startNode, startNode.f);
            VisaulizePrint(this.visualQubes);
        }

        public bool Run()
        {
            int ySize = tileMap.GetLength(0); // 첫 차원을 y값으로 사용
            int xSize = tileMap.GetLength(1);

            if (nextPointPQ.Count > 0) //휴리스틱 값이 있을때 // 다음 경로로 이동가능할때
            {
                // 1. 다음으로 탐색할 정점 꺼내기
                ASNode nextNode = nextPointPQ.Dequeue();

                // 2. 방문한 정점은 방문표시
                visited[nextNode.point.y, nextNode.point.x] = true;
                if (visualQubes[nextNode.point.y, nextNode.point.x].type != Type.Start)
                    visualQubes[nextNode.point.y, nextNode.point.x].SetPass(); //방문으로 바꿈

                // 3. 다음으로 탐색할 정점이 도착지인 경우 // 도착했다고 판단해서 경로 반환
                if (nextNode.point.x == end.x && nextNode.point.y == end.y)
                {
                    Point? pathPoint = end; // 이동중인 포인트(정점)
                    path = new List<Point>();

                    while (pathPoint != null) // PathPoint 가 null이면 정점에 방문자가 더이상없는것
                    {
                        Point point = pathPoint.GetValueOrDefault();
                        //GetValueOrDefault 은 값이 있으면 값을 가져오고 null이면 디폴트값으로 가져온다.
                        path.Add(point); // path 에 값을 넣어줘서 이동하는 모든 정점을 순서대로 구할수있다.
                        pathPoint = nodes[point.y, point.x].parent;
                    }

                    path.Reverse(); // 목적지부터 계산했기때문에 거꾸로해줘야한다.


                    for (int i = 0; i < path.Count; i++)
                    {
                        visualQubes[path[i].y, path[i].x].Select();
                    }
                    VisaulizePrint(this.visualQubes);
                    return false;
                }

                // 4. AStar 탐색을 진행 // 방향 탐색
                for (int i = 0; i < Direction.Length; i++)//상하 좌우로 체크
                {
                    int x = nextNode.point.x + Direction[i].x; // 탐색해야할 좌표
                    int y = nextNode.point.y + Direction[i].y; // 상하좌우로 이동했을때 좌표 x,y

                    // 4-1. 탐색하면 안되는 경우
                    // 맵을 벗어났을 경우
                    if (x < 0 || x >= xSize || y < 0 || y >= ySize)
                        continue;
                    // 탐색할 수 없는 정점일 경우
                    else if (tileMap[y, x] == false)
                        continue;
                    // 이미 방문한 정점일 경우
                    else if (visited[y, x])
                        continue;

                    // 4-2. 탐색한 정점 만들기

                    //지금까지의 가중치 g
                    int g = nextNode.g + ((nextNode.point.x == x || nextNode.point.y == y) ? CostStraight : CostDiagonal);
                    int h = Heuristic(new Point(x, y), end); // 목표 지점까지의 가중치 추산
                    ASNode newNode = new ASNode(new Point(x, y), nextNode.point, g, h); // 이동했을때 이동한곳에 node를 만들어준다. 


                    // 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
                    if (nodes[y, x] == null ||      // 탐색하지 않은 정점이거나
                        nodes[y, x].f > newNode.f)  // 가중치가 높은 정점인 경우
                    {
                        nodes[y, x] = newNode;
                        nextPointPQ.Enqueue(newNode, newNode.f);
                        //visualize
                        visualQubes[y, x].Set(newNode);
                        visualQubes[y, x].Visit();
                    }

                }
                VisaulizePrint(this.visualQubes);
                return true;
            }
            else
            { return false; }
        }


        // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
        private static int Heuristic(Point start, Point end)
        {
            int xSize = Math.Abs(start.x - end.x);  // 가로로 가야하는 횟수
            int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수

            // 맨해튼 거리 : 가로 세로를 통해 이동하는 거리
            // return CostStraight * (xSize + ySize);

            // 유클리드 거리 : 대각선을 통해 이동하는 거리
            return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);
        }

        public static void VisaulizePrint(in Qube[,] visualQubes)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J"); // 콘솔비우기

            Console.WriteLine("아무키나 누르면 계속 진행");
            int LineMax = 5;

            for (int y = 0; y < visualQubes.GetLength(0) * LineMax; y++)
            {
           
                for (int x = 0; x < visualQubes.GetLength(1); x++)
                {
                    
                    int index = y % LineMax;
                    switch (visualQubes[y / LineMax, x].type)
                    {
                        case Type.Block:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write( visualQubes[y / LineMax, x].info[index]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case Type.Select:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(visualQubes[y / LineMax, x].info[index]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case Type.Start:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(visualQubes[y / LineMax, x].info[index] );
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case Type.End:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write( visualQubes[y / LineMax, x].info[index]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case Type.Pass:
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(visualQubes[y / LineMax, x].info[index]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case Type.None:
                            Console.Write(visualQubes[y / LineMax, x].info[index]);
                            break;
                        case Type.Visit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(visualQubes[y / LineMax, x].info[index]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        default:
                            break;
                    }
                   
                }
            
                Console.WriteLine();

            }
        }
    }
}

