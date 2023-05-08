using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogueLike
{
    public abstract class Item
    {
        public string name;
        public string description;
        public char icon = '★';
        public Position pos;
        public int weight;

        public abstract void Use();
    }
}
