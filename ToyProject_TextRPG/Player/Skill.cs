using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Skill
    {
        public string name;
        public Action<TextRPG.Abstract.Monster> action;

        public Skill(string name, Action<TextRPG.Abstract.Monster> action)
        {
            this.name = name;
            this.action = action;
        }
    }
}
