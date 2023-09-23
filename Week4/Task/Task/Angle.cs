using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class Angle
    {
        public int Degrees;
        public float Minutes;
        public char Direction;
        public Angle()
        { }
        public Angle(int d, float m, char dir)
        {
            Degrees = d;
            Minutes = m;
            Direction = dir;
        }
        public void setAngle(int d, float m, char dir)
        {
            Degrees = d;
            Minutes = m;
            Direction = dir;
        }
        public string displayInString()
        {
            return Degrees.ToString() + "\u00b0" + Minutes.ToString("0.0") + "' " + Direction.ToString();
        }
    }
}
