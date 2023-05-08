using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public static class Datas
    {
        public enum Tile { NONE,WALL,CORRIDOR,ROOM,PLAYER,MONSTER,NEXTSTAGE }

        public static int maxMap = 5; // 총 맵갯수
        public static int level = 1;
        public static bool isNext = false;

        public static Tile[,] map;
        public static Player player;
        public static List<Item> inventory;
        public static List<Monster> monsters;
        public static List<Item> items;

        public static void Init()
        {
            player = new Player();
            inventory = new List<Item>();
            monsters = new List<Monster>();
            items = new List<Item>();

            inventory.Add(new Potion());

        }

        public static bool IsObjectInPos(Position pos)
        {
            return MonsterInPos(pos) == null && ItemInPos(pos) == null;
        }

        public static Monster MonsterInPos(Position pos)
        {
            foreach (Monster monster in monsters)
            {
                if (monster.pos.x == pos.x &&
                    monster.pos.y == pos.y)
                {
                    return monster;
                }
            }
            return null;
        }

        public static Item ItemInPos(Position pos)
        {
            foreach (Item item in items)
            {
                if (item.pos.x == pos.x &&
                    item.pos.y == pos.y)
                {
                    return item;
                }
            }
            return null;
        }

        public static void LoadLevel1()
        {
            

            player.pos = new Position(2, 2);

            monsters.Clear();
            items.Clear();

            Slime slime1 = new Slime();
            slime1.pos = new Position(3, 5);
            monsters.Add(slime1);

            Slime slime2 = new Slime();
            slime2.pos = new Position(7, 5);
            monsters.Add(slime2);

            Dragon dragon = new Dragon();
            dragon.pos = new Position(12, 12);
            monsters.Add(dragon);

            Item potion = new Potion();
            potion.pos = new Position(12, 1);
            items.Add(potion);
        }
    }
}