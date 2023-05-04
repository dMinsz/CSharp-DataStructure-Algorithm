using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Abstract;

namespace TextRPG.Items
{
    public class Potion : Item
    {
        private int point = 30;

        public Potion()
        {
            name = "포션";
            description = $"플레이어의 체력을 {point} 회복시킵니다.";
            weight = 1;
        }

        public override void Use()
        {
            Console.WriteLine($"포션을 사용하여 플레이어의 체력을 {point} 회복시킵니다.");
            Thread.Sleep(1000);
            Data.player.Heal(point);
            Console.WriteLine($"플레이어의 체력이 {Data.player.CurHp}이 되었습니다.");
            Thread.Sleep(1000);
        }
    }
}
