using System;
using System.Collections.Generic;

namespace Postcard
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Wollen Sie eine Postkarte erstellen und versenden? (j/n)");

            if (Console.ReadKey(true).Key != ConsoleKey.J)
            {
                Console.WriteLine("Ok, dann hau ab!!11");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Ihr Name:");
            string actUser = Console.ReadLine();

            List<Postcard> userPostcards = CreatePostcards(actUser);

            Console.WriteLine("Wollen Sie die Karten versenden? (j/n)");
            if (Console.ReadKey(true).Key == ConsoleKey.J)
            {
                foreach (var card in userPostcards)
                {
                    card.Send();
                }
            }

            Console.WriteLine("\nAlle Karten wurde versendet.\n" +
                                "Besten Dank und beehren Sie uns bald wieder!");
            Console.ReadKey();
        }


        public static List<Postcard> CreatePostcards(string actUser)
        {
            var tempList = new List<Postcard>();
            do
            {
                Postcard newCard = new Postcard(actUser, 0);

                tempList.Add(newCard);

                Console.WriteLine("Wollen Sie eine weitere Postkarte erstellen? (j/n)");

            } while (Console.ReadKey(true).Key == ConsoleKey.J);

            return tempList;
        }
    }







    public class Postcard
    {
        private const int POSTAGE = 80;
        private readonly string firstname;
        private readonly string postcardText;
        private int paidPostage;

        public Postcard(string firstname, int paidPostage)
        {
            this.firstname = firstname;
            this.paidPostage = paidPostage;
            postcardText = $"The weather is fkn great dudes!! Your King {this.firstname}";
            AskUserToPay();
        }


        private int AddPostage(int cent)
        {
            paidPostage += cent;

            return paidPostage;
        }

        public void Send()
        {
            AskUserToPay();

            Console.WriteLine($"\nKarte gesendet:\t\t" +
                $"Porto bezahlt: {paidPostage}\t" +
                $"Kartentext: {postcardText}\t");
        }


        private void AskUserToPay()
        {
            while(paidPostage < POSTAGE)
            {
                bool result;
                int paidAmount;

                do
                {
                    string notFirstTime = paidPostage != 0 ? "weitere " : string.Empty;

                    Console.WriteLine($"Zahlen Sie {notFirstTime}{POSTAGE - paidPostage} Cents für das Porto ein:");

                    result = int.TryParse(Console.ReadLine(), out paidAmount);
                    if (!result)
                    {
                        Console.WriteLine("Geben Sie eine ZAHL ein!");
                    }
                } while (!result);

                AddPostage(paidAmount);
            };
        }
    }
}
