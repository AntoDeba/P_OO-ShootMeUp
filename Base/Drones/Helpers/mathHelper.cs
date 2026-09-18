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
    }
}
