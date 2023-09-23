using HighLow.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighLow.UI
{
    internal class DriverUI
    {
        public static string Guess()
        {
            Console.Write("Enter 1 for high and 0 for low: ");
            return Console.ReadLine();
        }
        public static void Score(int score) {
            Console.WriteLine("Score: " + score);
        }
        public static void finalScore(int finalScore)
        {
            Console.WriteLine("Your final Score: " + finalScore);
            Console.ReadKey();
        }
        public static void cardsLeft(int cardsLeft)
        {
            Console.WriteLine("Remaining cards: " + cardsLeft);
        }
        public static void roundsLeft(int x)
        {
            Console.WriteLine("Rounds left: " + x);
            Console.ReadKey();
        }
    }
}
