using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class MainMenu : Scene
    {
        public MainMenu(GameManager game) : base(game) { }

        public void Title(StringBuilder sb) 
        {
            sb.AppendLine("______                             _      _____  _   __ _____ ");
            sb.AppendLine("| ___ \\                           | |    |_   _|| | / /|  ___|");
            sb.AppendLine("| |_/ /  ___    __ _  _   _   ___ | |      | |  | |/ / | |__  ");
            sb.AppendLine("|    /  / _ \\  / _` || | | | / _ \\| |      | |  |    \\ |  __| ");
            sb.AppendLine("| |\\ \\ | (_) || (_| || |_| ||  __/| |____ _| |_ | |\\  \\| |___ ");
            sb.AppendLine("\\_| \\_| \\___/  \\__, | \\__,_| \\___|\\_____/ \\___/ \\_| \\_/\\____/ ");
            sb.AppendLine("                __/ |                                         ");
            sb.AppendLine("               |___/                                          ");
        }
        public override void Render()
        {
            StringBuilder sb = new StringBuilder();

            Title(sb);


            sb.AppendLine("1. 게임시작");
            sb.AppendLine("2. 게임종료");
            sb.Append("메뉴를 선택하세요 : ");

            Console.Write(sb.ToString());
        }

        public override void Update()
        {
            //input 작업도 같이..
            string input = Console.ReadLine();

            int index;
            if (!int.TryParse(input, out index))
            {
                Console.WriteLine("잘못 입력 하셨습니다.");
                Thread.Sleep(1000);
                return;
            }

            switch (index)
            {
                case 1:
                    game.GameStart();
                    break;
                case 2:
                    game.GameOver("게임을 종료했습니다.");
                    break;
                default:
                    Console.WriteLine("잘못 입력 하셨습니다.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
