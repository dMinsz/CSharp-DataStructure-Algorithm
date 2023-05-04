using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TextRPG
{
    internal class Game
    {
        private bool running = true;

        public void Run()
        {
            Init();

            while (running)
            {
                Render();
                Update();
            }

            Release();
        }

        private void Init()
        {
            Console.CursorVisible = false;
        }

        private void Render()
        {
            Console.Clear();
        }

        private void Update()
        {
        }

        private void Release()
        {

        }

        public void MainMenu()
        {
        }

        public void Map()
        {
        }

        public void Battle()
        {
           
        }

        public void Inventory()
        {
        }

        public void GameStart()
        {
        }

        public void GameOver(string text = "")
        {
            Console.Clear();
            running = false;

        }
    }
}
