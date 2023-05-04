using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Abstract;
using static System.Formats.Asn1.AsnWriter;

namespace TextRPG
{
    public static class Global
    {
        public static Game game = new Game();
    }
    public class Game
    {
        private bool running = true;

        public Scene currentScene; // 현재 게임 씬
        //private Dictionary<string,Scene> Scenes; // 여러 다른 씬 저장용

        private TextRPG.Scenes.MainMenu mainMenu;
        private TextRPG.Scenes.Map mapScene;
        private TextRPG.Scenes.Inventory inventoryScene;
        private TextRPG.Scenes.Battle battleScene;


        public void Run()
        {
            Init();

            while (running)
            {
                Render();
                Update(); // Text RPG 의 특성상 갱신에서 입력을 확인하는게 편하다.
            }

            Release();
        }

        private void Init()
        {
            Console.SetWindowSize(Console.WindowWidth,Console.LargestWindowHeight);
            Console.CursorVisible = false;

            Data.Init();
            mainMenu = new TextRPG.Scenes.MainMenu(this);
            mapScene = new TextRPG.Scenes.Map(this);
            inventoryScene = new TextRPG.Scenes.Inventory(this);
            battleScene = new TextRPG.Scenes.Battle(this);

            currentScene = mainMenu;
        }

        private void Render()
        {
            Console.Clear();
            currentScene.Render();
        }

        private void Update()
        {
            currentScene.Update();
        }

        private void Release()
        {

        }

        public void MainMenu()
        {
        }

        public void Map()
        {
            currentScene = mapScene;
        }

        public void Battle(Monster monster)
        {
            currentScene = battleScene;
            battleScene.StartBattle(monster);
        }

        public void Inventory()
        {
            currentScene = inventoryScene;
        }

        public void GameStart()
        {
            currentScene = mapScene;
            mapScene.GenerateMap();
        }

        public void GameOver(string text = "")
        {
            Console.Clear();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("  ***    *   *   * *****       ***  *   * ***** ****  ");
            sb.AppendLine(" *      * *  ** ** *          *   * *   * *     *   * ");
            sb.AppendLine(" * *** ***** * * * *****      *   * *   * ***** ****  ");
            sb.AppendLine(" *   * *   * *   * *          *   *  * *  *     *  *  ");
            sb.AppendLine("  ***  *   * *   * *****       ***    *   ***** *   * ");
            sb.AppendLine();

            sb.AppendLine();
            sb.Append(text);

            Console.WriteLine(sb.ToString());

            running = false;

        }
    }
}
