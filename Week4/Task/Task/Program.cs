using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Ship> list = new List<Ship>();
            int choice = 0;

            while (choice != 5)
            {
                choice = Menu();
                if (choice == 1)
                {
                    Console.Clear();
                    list.Add(addShip());
                }
                else if (choice == 2)
                {
                    if (list != null)
                    {
                        Console.Clear() ;
                        getShipPosition(list);
                    }
                    else
                    {
                        Console.WriteLine("No ship found. Please add a ship first.");
                    }
                }
                else if (choice == 3)
                {
                    if (list != null)
                    {
                        Console.Clear() ;
                        getShipNumber(list);
                    }
                    else
                    {
                        Console.WriteLine("No ship found. Please add a ship first.");
                    }
                }
                else if (choice == 4)
                {
                    if (list != null)
                    {
                        Console.Clear() ;
                        changeDirection(list);
                    }
                    else
                    {
                        Console.WriteLine("No ship found. Please add a ship first.");
                    }
                }
                Console.Clear();
            }
        }
        static int Menu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Ship");
            Console.WriteLine("2. View Ship Position");
            Console.WriteLine("3. View Ship Serial Number");
            Console.WriteLine("4. Change Ship Position");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }
        static Ship addShip()
        {
            Angle angle1 = new Angle();
            Angle angle2 = new Angle();
            Console.Write("Enter ship's serial number: ");
            string Number = Console.ReadLine();
            Console.Write("Enter ship's latitude in degrees: ");
            angle1.Degrees = int.Parse(Console.ReadLine());
            Console.Write("Enter ship's latitude in minutes: ");
            angle1.Minutes = float.Parse(Console.ReadLine());
            Console.Write("Enter ship's latitude direction (N or S): ");
            angle1.Direction = char.Parse(Console.ReadLine().ToUpper());
            Console.Write("Enter ship's longitude in degrees: ");
            angle2.Degrees = int.Parse(Console.ReadLine());
            Console.Write("Enter ship's longitude in minutes: ");
            angle2.Minutes = float.Parse(Console.ReadLine());
            Console.Write("Enter ship's longitude direction (E or W): ");
            angle2.Direction = char.Parse(Console.ReadLine().ToUpper());
            Ship ship = new Ship(Number, angle1, angle2);
            Console.WriteLine("Ship added successfully.");
            return ship;
        }
        static void getShipPosition(List<Ship> shipList)
        {
            Console.WriteLine("Enter ship number: ");
            string serial = Console.ReadLine();
            foreach (Ship ship in shipList)
            {
                if(serial==ship.Number)
                {
                    ship.printPosition();
                }
            }
        }
        static void getShipNumber(List<Ship> shipList)
        {
            Angle latitude = new Angle();
            Angle longitude = new Angle();

            Console.WriteLine("Enter ship latitude in degrees: ");
            latitude.Degrees= int.Parse(Console.ReadLine());
            Console.WriteLine("Enter ship latitude in Minutes: ");
            latitude.Minutes = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter ship latitude Direction: ");
            latitude.Direction = char.Parse(Console.ReadLine().ToUpper());
            Console.WriteLine("Enter ship longitude in degrees: ");
            longitude.Degrees = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter ship longitude in Minutes: ");
            longitude.Minutes = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter ship longitude Direction: ");
            longitude.Direction = char.Parse(Console.ReadLine().ToUpper());

            foreach (Ship ship in shipList)
            {
                if(longitude.Direction == ship.Longitude.Direction && longitude.Degrees == ship.Longitude.Degrees && longitude.Minutes == ship.Longitude.Minutes && latitude.Degrees == ship.Latitude.Degrees && latitude.Minutes == ship.Latitude.Minutes && latitude.Direction == ship.Latitude.Direction)
                {
                    ship.displaySerial();
                }
            }
        }
        static void changeDirection(List<Ship> shipList)
        {
            Console.Write("Enter ship serial number: ");
            string serial = Console.ReadLine();
            foreach(Ship ship in shipList)
            {
                if(serial == ship.Number)
                {
                    Console.Write("Enter new latitude in degrees: ");
                    ship.Latitude.Degrees = int.Parse(Console.ReadLine());
                    Console.Write("Enter new latitude in minutes: ");
                    ship.Latitude.Minutes = float.Parse(Console.ReadLine());
                    Console.Write("Enter new latitude direction (N or S): ");
                    ship.Latitude.Direction = char.Parse(Console.ReadLine().ToUpper());
                    Console.Write("Enter new longitude in degrees: ");
                    ship.Longitude.Degrees = int.Parse(Console.ReadLine());
                    Console.Write("Enter new longitude in minutes: ");
                    ship.Longitude.Minutes = float.Parse(Console.ReadLine());
                    Console.Write("Enter new longitude direction (E or W): ");
                    ship.Longitude.Direction = char.Parse(Console.ReadLine().ToUpper());
                }    
            }
            Console.WriteLine("Ship position changed successfully.");
        }
    }
}
