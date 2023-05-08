using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueLike
{
    public abstract class Monster
    {
        public string name;
        public char icon = '▼';
        public Position pos;

        public string image;
        public int curHp;
        public int maxHp;
        public int ap;
        public int dp;

        public abstract void MoveAction();

        protected void TryMove(Direction dir)
        {
            Position prevPos = pos;
            // 이동
            switch (dir)
            {
                case Direction.Up:
                    pos.y--;
                    break;
                case Direction.Down:
                    pos.y++;
                    break;
                case Direction.Left:
                    pos.x--;
                    break;
                case Direction.Right:
                    pos.x++;
                    break;
            }

            // 이동한 자리가 벽일 경우
            if (Datas.map[pos.y, pos.x] == Datas.Tile.WALL)
            {
                // 원위치 시키기
                pos = prevPos;
            }
            else if (Datas.IsObjectInPos(pos))
            {
                pos = prevPos;
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage > dp)
            {
                Console.WriteLine($"{name}(은/는) {damage - dp} 데미지를 받았다.");
                curHp -= damage - dp;
            }
            else
                Console.WriteLine($"공격은 {name}에게 먹히지 않았다.");

            Thread.Sleep(1000);

            if (curHp <= 0)
            {
                Console.WriteLine($"{name}(은/는) 쓰려졌다!");
                Thread.Sleep(1000);
                Console.WriteLine("몬스터는 포션을 떨어뜨렸다!");
                //Data.player.GetItem(new Potion());
                Thread.Sleep(1000);
            }
        }

        public void Attack(Player player)
        {
            Console.WriteLine($"{name}(이/가) 플레이어를 공격합니다.");
            Thread.Sleep(1000);
            player.TakeDamage(ap);
        }
    }
}
