using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public static class Global
    {
        public static GameManager gameManager = new GameManager();
    }

    public class GameManager
    {
        private bool running = true;

        public Scene currentScene; // 현재 게임 씬

        private MainMenu mainMenu;
        private Map mapScene;
        private Inventory inventoryScene;
        private Battle battleScene;


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
            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight);
            Console.CursorVisible = false;

            Datas.Init();
            mainMenu = new MainMenu(this);
            mapScene = new Map(this);
            inventoryScene = new Inventory(this);
            battleScene = new Battle(this);

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
