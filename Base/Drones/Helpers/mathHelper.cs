using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMeUp.Helpers
{
    public static class mathHelper
    {
        public static double distance(double X1, double Y1, double X2, double Y2)
        {
            double distance;
            double deltaX = X2 - X1;
            double deltaY = Y2 - Y1;

            distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY); //Théorême de pythagore

            return distance;
        }
        public static bool areTouching(float x1, float x2, float y1, float y2, int width1, int width2, int height1, int height2)
        {
            if (Math.Abs(x1 - x2) < ((width1/2) + (width2/2)) && Math.Abs(y1 - y2) < ((height1 / 2) + (height2 / 2)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
