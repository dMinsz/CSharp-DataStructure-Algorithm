﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RogueLike
{
    public class Map : Scene
    {
        int sizeX = 1;// 윈도우 버전마다 간격이달라서 사용한다.
        
        GMap.Generator mapGenerator;

       
        public Map(GameManager game) : base(game) { }
        public override void Render()
        {
            PrintMap();
            //mapGenerator.PrintMap();

            PrintMenu();
            PrintInfo();

            Console.SetCursorPosition(0, Datas.map.GetLength(0) + 1);
        }

        public override void Update()
        {

            if (Datas.level == Datas.maxMap)
            {
                Datas.level = 0;
                Datas.isNext = false;

                game.GameOver("모든 맵을 클리어했습니다.");
            }


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
                    Datas.player.TryMove(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    Datas.player.TryMove(Direction.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    Datas.player.TryMove(Direction.Left);
                    break;
                case ConsoleKey.RightArrow:
                    Datas.player.TryMove(Direction.Right);
                    break;
            }

            //monster 이동
            foreach (Monster m in Datas.monsters)
            {
                m.MoveAction();
                if (m.pos.x == Datas.player.pos.x &&
                    m.pos.y == Datas.player.pos.y)
                {
                    game.Battle(m);
                    return;
                }
            }


            if (Datas.isNext)
            {
                Datas.isNext = false;
                GenerateMap();
            }



        }

        public void GenerateMap()
        {

            //기본으로 가지고잇는 맵으로사용
            //Datas.LoadLevel1();

            //자동으로 맵만들기
            mapGenerator = new GMap.Generator();

            Datas.map = mapGenerator.GenerateMap(15+(Datas.level*5), 15+((Datas.level * 5)),1+ Datas.level);

            Datas.player.pos = mapGenerator.GetRandomRoom();
            Datas.map[Datas.player.pos.y, Datas.player.pos.x] = Datas.Tile.PLAYER;

            Position end = mapGenerator.GetRandomRoom();

            Datas.map[end.y, end.x] = Datas.Tile.NEXTSTAGE;


            for (int i = 0; i < Datas.level * 2; i++) // 레벨에 * 2 로나온다.
            {
                Slime slime = new Slime();
                slime.pos = mapGenerator.GetRandomRoom();
                Datas.monsters.Add(slime);
            }

            for (int i = 0; Datas.level > 2 && i < Datas.level - 1 ; i++) //3~5레벨 까지 드래곤이 등장한다. 각각 2,3,4 마리 등장
            {
                Dragon dragon = new Dragon();
                dragon.pos = new Position(12, 12);
                Datas.monsters.Add(dragon);
            }




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


            //자동생성 테스트
            mapGenerator.PrintMap();

            foreach (Monster monster in Datas.monsters)
            {
                if (monster.name == "드래곤")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
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
            Console.SetCursorPosition(Datas.player.pos.x * sizeX, Datas.player.pos.y);
            Console.Write(Datas.player.icon);
        }

        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            (int left, int top) pos = Console.GetCursorPosition();


            Console.SetCursorPosition(Datas.map.GetLength(1) * sizeX + 3, 0);
            Console.Write($"현재 스테이지 레벨 : {Datas.level}");
            Console.SetCursorPosition(Datas.map.GetLength(1) * sizeX + 3, 2);
            Console.Write("메뉴 : Q");
            Console.SetCursorPosition(Datas.map.GetLength(1) * sizeX + 3, 4);
            Console.Write("이동 : 방향키");
            Console.SetCursorPosition(Datas.map.GetLength(1) * sizeX + 3, 5);
            Console.Write("인벤토리 : I");

            Console.SetCursorPosition(Datas.map.GetLength(1) * sizeX + 3, 7);
            Console.Write("P:캐릭터");
            Console.SetCursorPosition(Datas.map.GetLength(1) * sizeX + 3, 8);
            Console.Write("◈:다음 스테이지로 이동");

        }

        private void PrintInfo()
        {
            Console.SetCursorPosition(0, Datas.map.GetLength(0) + 1);
            Console.Write($"HP : {Datas.player.CurHp,3}/{Datas.player.MaxHp,3}\t");
            Console.Write($"EXP : {Datas.player.CurExp,3}/{Datas.player.MaxExp,3}");

            Console.SetCursorPosition(0,Datas.map.GetLength(0) + 3);
            Console.Write("현재 콘솔 문제때문에 정확한 위치가 아닐 수 도있습니다.");

            Console.SetCursorPosition(0, Datas.map.GetLength(0) + 4);
            Console.Write("잘 비벼서 지나가세요");
        }
    }
}
