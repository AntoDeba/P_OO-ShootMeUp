using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp
{
    public class Obstacle
    {
        private int _x;
        private int _y;
        public const int OBSTACLE_WIDTH = 46;
        public const int OBSTACLE_HEIGHT = 120;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public Obstacle(int x,int y)
        {
            this._x = x;
            this._y = y;
        }

        public void Update(int interval)
        {

        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.obstacle, _x - (OBSTACLE_WIDTH / 2), _y - (OBSTACLE_HEIGHT / 2), OBSTACLE_WIDTH, OBSTACLE_HEIGHT);
        }
    }
}
