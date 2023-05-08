using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueLike
{
    public class Inventory : Scene
    {
        public Inventory(GameManager game) : base(game) { }
        public override void Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<인벤토리>");
            sb.AppendLine($"\t이름\t무게\t설명");
            for (int i = 0; i < Datas.inventory.Count; i++)
            {
                sb.Append($"{i,3}");
                sb.Append($"\t{Datas.inventory[i].name}");
                sb.Append($"\t{Datas.inventory[i].weight}");
                sb.Append($"\t{Datas.inventory[i].description}");
                sb.AppendLine();
            }
            sb.AppendLine();

            Console.Write(sb.ToString());
        }

        public override void Update()
        {
            Console.WriteLine("0 : 뒤로   1 : 아이템사용   2 : 아이템정렬");
            Console.Write("선택 : ");
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
                case 0:
                    game.Map();
                    break;
                case 1:
                    UseItem();
                    break;
                case 2:
                    SortItem();
                    break;
                default:
                    Console.WriteLine("잘못 입력 하셨습니다.");
                    Thread.Sleep(1000);
                    break;
            }
        }

        private void UseItem()
        {
            Console.WriteLine();
            Console.Write("사용할 아이템의 번호입력 : ");

            string input;
            input = Console.ReadLine();

            int index;
            if (!int.TryParse(input, out index))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                return;
            }

            if (index >= Datas.inventory.Count)
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                return;
            }

            Datas.inventory[index].Use();
        }

        private void SortItem()
        {
            Console.WriteLine();
            Console.WriteLine("1. 이름   2. 무게");
            Console.Write("정렬할 기준 : ");

            string input;
            input = Console.ReadLine();

            int index;
            if (!int.TryParse(input, out index))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                return;
            }

            switch (index)
            {
                case 1:
                    Datas.inventory.Sort(Comparer<Item>.Create((a, b) => a.name.CompareTo(b.name)));
                    break;
                case 2:
                    Datas.inventory.Sort(Comparer<Item>.Create((a, b) => a.weight - b.weight));
                    break;
                default:
                    Console.WriteLine("잘못 입력 하셨습니다.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
