using ShortestPath;

namespace _11.ShortestPath
{
    //최단거리 알고리즘
    //다익스트라 알고리즘 테스트
    internal class Program
    {
        const int INF = 99999;
        static void Main(string[] args)
        {
            int[,] graph = new int[9, 9]
             {
                {   0, INF,   1,   7, INF, INF, INF,   5, INF},
                { INF,   0, INF, INF, INF,   4, INF, INF, INF},
                { INF, INF,   0, INF, INF, INF, INF, INF, INF},
                {   5, INF, INF,   0, INF, INF, INF, INF, INF},
                { INF, INF,   9, INF,   0, INF, INF, INF,   2},
                {   1, INF, INF, INF, INF,   0, INF,   6, INF},
                { INF, INF, INF, INF, INF, INF,   0, INF, INF},
                {   1, INF, INF, INF,   4, INF, INF,   0, INF},
                { INF,   5, INF,   2, INF, INF, INF, INF,   0}
             };

            int[] cost;
            int[] parent;
            Dijkstra.ShortestPath(in graph, 0, out cost, out parent);
            Console.WriteLine("<Dijkstra>");
            PrintResult(cost, parent);

            int[,] costTable;
            int[,] parentTable;
            FloydWarshall.ShortestPath(in graph, out costTable, out parentTable);
            Console.WriteLine("<Floyd-Warshall>");
            PrintFloydWarshall(costTable, parentTable);
        }

        private static void PrintResult(int[] cost, int[] path)
        {
            Console.Write("Vertex");
            Console.Write("\t");
            Console.Write("cost");
            Console.Write("\t");
            Console.WriteLine("parent");

            for (int i = 0; i < cost.Length; i++)
            {
                Console.Write(i);
                Console.Write("\t");
                if (cost[i] >= INF)
                    Console.Write("INF");
                else
                    Console.Write("{0:D3}", cost[i]);
                Console.Write("\t");
                Console.WriteLine(path[i]);
            }
        }

        private static void PrintFloydWarshall(int[,] costTable, int[,] pathTable)
        {
            Console.WriteLine("Cost table");
            for (int y = 0; y < costTable.GetLength(0); y++)
            {
                for (int x = 0; x < costTable.GetLength(1); x++)
                {
                    if (costTable[y, x] >= INF)
                        Console.Write("INF ");
                    else
                        Console.Write("{0,3} ", costTable[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Parent table");
            for (int y = 0; y < pathTable.GetLength(0); y++)
            {
                for (int x = 0; x < pathTable.GetLength(1); x++)
                {
                    if (pathTable[y, x] < 0)
                        Console.Write("  X ");
                    else
                        Console.Write("{0,3} ", pathTable[y, x]);
                }
                Console.WriteLine();
            }
        }
    }
}
