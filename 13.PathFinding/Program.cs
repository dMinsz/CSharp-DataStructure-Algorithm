using PathFinding;

namespace _13.PathFinding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool[,] tileMap = new bool[11, 11]
            {
                { false, false, false, false, false, false, false, false, false, false, false },
                { false, true, true, true, true, true, true, true, true,true, false },
                { false, true, true, true, true, false,  false,  true, true,true, false },
                { false, true, true, true, true, false,  true,  true, true ,true, false},
                 { false, true, true, true, true, false,  true,  true, true ,true, false},
                 { false, true, true, true, true, false,  true,  true, true ,true, false},
                 { false, true, true, true, true, false,  true,  true, true ,true, false},
                 { false, true, true, true, true, false,  true,  true, true ,true, false},
                 { false, true, true, true, true, false,  true,  true, true ,true, false},
                 { false, true, true, true, true, false,  true,  true, true ,true, false},
                 { false, false, false, false, false, false, false, false, false, false, false },
            }; // true면 갈수있는 곳 false 면 못가는곳
            List<Point> path;

            //1,1 에서 1,7 까지 가는 경로탐색
            //AStar.PathFinding(tileMap, new Point(3, 3), new Point(7, 8), out path);
            //PrintResult(tileMap, path);

            Qube[,] visualQubes;
            AStarVisualizer.PathFinding(tileMap, new Point(3, 3), new Point(7, 8), out path, out visualQubes);
            AStarVisualizer.VisaulizePrint(visualQubes,11,11);
        }
        //test code
        static void PrintResult(in bool[,] tileMap, in List<Point> path)
        {
            char[,] pathMap = new char[tileMap.GetLength(0), tileMap.GetLength(1)];
            for (int y = 0; y < pathMap.GetLength(0); y++)
            {
                for (int x = 0; x < pathMap.GetLength(1); x++)
                {
                    if (tileMap[y, x]) // true면
                        pathMap[y, x] = ' ';
                    else // 갈수있는 곳이 아니면 //false 면
                        pathMap[y, x] = 'X';
                }
            }

            foreach (Point point in path)
            {
                pathMap[point.y, point.x] = '*'; // 이동경로를 * 로 표시
            }

            Point start = path.First();
            Point end = path.Last();
            pathMap[start.y, start.x] = 'S'; // 시작지점을 S 로 지정
            pathMap[end.y, end.x] = 'E'; // 끝나는지점을 E 로 지정

            for (int i = 0; i < pathMap.GetLength(0); i++) // pathMap 모두 순회하면서 출력
            {
                for (int j = 0; j < pathMap.GetLength(1); j++)
                {
                    Console.Write(pathMap[i, j]);
                }
                Console.WriteLine();
            }
        }

    }
}