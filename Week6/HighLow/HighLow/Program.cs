using HighLow.BL;
using HighLow.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HighLow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            while(choice != 2)
            {
                choice = Menu();
                if (choice == 1)
                {
                    string returnCard;
                    string userGuess;
                    string highOrLowString;
                    int totalScoreAfterGameEnds = 0;
                    int totalScore = 0;
                    bool gameRunning;
                    Deck deck = new Deck();

                    for (int x = 2; x >= 0; x--)
                    {
                        Console.Clear();
                        gameRunning = true;
                        deck.shuffle();

                        while (gameRunning)
                        {
                            Card card = deck.dealCard();
                            returnCard = card.toString();
                            Console.WriteLine(returnCard);
                            DriverUI.cardsLeft(deck.cardsLeft());
                            userGuess = DriverUI.Guess();
                            highOrLowString = deck.check();
                            if (highOrLowString == userGuess)
                            {
                                totalScore = totalScore + card.getValue();
                                deck.popOffCard();
                            }
                            else
                            {
                                gameRunning = false;
                            }
                        }
                        DriverUI.Score(totalScore);
                        totalScoreAfterGameEnds = totalScoreAfterGameEnds + totalScore;
                        DriverUI.roundsLeft(x);
                    }
                    DriverUI.finalScore(totalScoreAfterGameEnds);
                }
            }
        }

        public static int Menu()
        {
            Console.WriteLine("Enter 1 to play game");
            Console.WriteLine("Enter 2 to exit");
            return int.Parse(Console.ReadLine());
        }
    }
}