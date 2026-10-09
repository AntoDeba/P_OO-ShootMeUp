using ShootMeUp.Helpers;
using ShootMeUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp.Model
{
    public class Obstacle
    {
        private int _x;
        private int _y;
        private const int OBSTACLE_WIDTH = 46;
        private const int OBSTACLE_HEIGHT = 120;

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
