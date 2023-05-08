using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public abstract class Scene
    {
        protected GameManager game;

        public Scene(GameManager game)
        {
            this.game = game;
        }

        public abstract void Render();
        public abstract void Update();
    }
}
