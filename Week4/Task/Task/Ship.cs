using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class Ship
    {
        public string Number;
        public Angle Latitude;
        public Angle Longitude;

        public Ship() { }

        public Ship(string num, Angle lat, Angle lon)
        {
            Number = num;
            Latitude = lat;
            Longitude = lon;
        }
        public void printPosition()
        {
            Console.WriteLine("Latitude: " + Latitude.displayInString() + ", Longitude: " + Longitude.displayInString());
        }
        public void displaySerial()
        {
            Console.WriteLine("Ship number: " + Number);
        }
    }


}
