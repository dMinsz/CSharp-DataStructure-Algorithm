using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class Battle : Scene
    {
        private Monster monster;
        public Battle(GameManager game):base(game) { }
        public override void Render()
        {
            Console.WriteLine();
            Console.WriteLine($"{monster.name}    {monster.curHp,3}/{monster.maxHp,3}");
            Console.WriteLine();
            Console.WriteLine(monster.image);
            Console.WriteLine();
            Console.WriteLine(Datas.player.image);
            Console.WriteLine();
            Console.WriteLine($"플레이어    {Datas.player.CurHp,3}/{Datas.player.MaxHp}");
            Console.WriteLine();
        }

        public override void Update()
        {
            for (int i = 0; i < Datas.player.skills.Count; i++)
            {
                Console.Write($"{i + 1,2}. {Datas.player.skills[i].name} ");
            }
            Console.WriteLine();
            Console.Write("명령을 입력하세요 : ");

            string input = Console.ReadLine();

            int index;
            if (!int.TryParse(input, out index))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                return;
            }
            if (index < 1 || index > Datas.player.skills.Count)
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                return;
            }

            Datas.player.skills[index - 1].action(monster);

            // 턴 결과
            if (monster.curHp <= 0)
            {
                game.Map();
                return;
            }

            // 몬스터 턴
            monster.Attack(Datas.player);

            // 턴 결과
            if (Datas.player.CurHp <= 0)
            {
                game.GameOver("몬스터에게 패배했습니다.");
                return;
            }
        }

        public void StartBattle(Monster monster)
        {
            this.monster = monster;
            Datas.monsters.Remove(monster);

            Console.Clear();
            Console.WriteLine($"{monster.name}(와/과) 전투 시작!");
            Thread.Sleep(1000);
        }

        public void EndBattle()
        {
            Console.Clear();
            Console.WriteLine("전투에서 승리했다!");

            Thread.Sleep(2000);
            game.Map();
        }
    }
}
