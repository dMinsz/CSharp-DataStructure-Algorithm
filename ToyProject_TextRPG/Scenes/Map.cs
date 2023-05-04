using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Abstract;

namespace TextRPG.Scenes
{
    public class Map : Scene
    {
        int sizeX = 1;// 윈도우 버전마다 간격이달라서 사용한다.

        public Map(Game game) : base(game) { }
        public override void Render()
        {
            PrintMap();
            PrintMenu();
            PrintInfo();

            Console.SetCursorPosition(0, Data.map.GetLength(0) + 1);
        }

        public override void Update()
        {
            ConsoleKeyInfo input;

            while (true)
            {
                input = Console.ReadKey();

                if (input.Key == ConsoleKey.Q ||
                    input.Key == ConsoleKey.I ||
                    input.Key == ConsoleKey.UpArrow ||
                    input.Key == ConsoleKey.DownArrow ||
                    input.Key == ConsoleKey.LeftArrow ||
                    input.Key == ConsoleKey.RightArrow)
                {
                    break;
                }
            }

            // 시스템 키 입력시 씬 전환
            if (input.Key == ConsoleKey.Q)
            {
                game.MainMenu();
                return;
            }
            else if (input.Key == ConsoleKey.I)
            {
                game.Inventory();
                return;
            }

            // 플레이어 이동
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    Data.player.TryMove(Type.Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    Data.player.TryMove(Type.Direction.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    Data.player.TryMove(Type.Direction.Left);
                    break;
                case ConsoleKey.RightArrow:
                    Data.player.TryMove(Type.Direction.Right);
                    break;
            }

            //monster 이동
            foreach (Monster m in Data.monsters)
            {
                m.MoveAction();
                if (m.pos.x == Data.player.pos.x &&
                    m.pos.y == Data.player.pos.y)
                {
                    game.Battle(m);
                    return;
                }
            }
        }

        public void GenerateMap()
        {
            Data.LoadLevel1();

            //window version check
            OperatingSystem os = Environment.OSVersion;
            var windowVersion = os.Version.Major;

            if (windowVersion == 10)
            {
                sizeX = 2;
            }
        }

        private void PrintMap()
        {
            StringBuilder sb = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0; y < Data.map.GetLength(0); y++)
            {
                for (int x = 0; x < Data.map.GetLength(1); x++)
                {
                    if (Data.map[y, x])
                        sb.Append($"{" ",2}");
                    else
                        sb.Append($"{"X",2}");
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());

            foreach (TextRPG.Abstract.Monster monster in Data.monsters)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(monster.pos.x * sizeX, monster.pos.y);
                Console.Write(monster.icon);
            }

            //foreach (Item item in Data.items)
            //{
            //    Console.ForegroundColor = ConsoleColor.Yellow;
            //    Console.SetCursorPosition(item.pos.x* sizeX, item.pos.y);
            //    Console.Write(item.icon);
            //}

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Data.player.pos.x * sizeX, Data.player.pos.y);
            Console.Write(Data.player.icon);
        }

        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            (int left, int top) pos = Console.GetCursorPosition();
            Console.SetCursorPosition(Data.map.GetLength(1)*sizeX + 3, 1);
            Console.Write("메뉴 : Q");
            Console.SetCursorPosition(Data.map.GetLength(1) * sizeX + 3, 3);
            Console.Write("이동 : 방향키");
            Console.SetCursorPosition(Data.map.GetLength(1) * sizeX + 3, 4);
            Console.Write("인벤토리 : I");
        }

        private void PrintInfo()
        {
            Console.SetCursorPosition(0, Data.map.GetLength(0) + 1);
            Console.Write($"HP : {Data.player.CurHp,3}/{Data.player.MaxHp,3}\t");
            Console.Write($"EXP : {Data.player.CurExp,3}/{Data.player.MaxExp,3}");
        }
    }
}
