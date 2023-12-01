using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOSCafe
{
    internal class Program
    {
        static List<food_items> selected_foods = new List<food_items>();
        static double totalPrice = 0;
        static order ordr = new order();

        static void Main(string[] args)
        {
            POS();

            Console.WriteLine("Thank you. We hope you come again!\nPress any key to exit. . .");
            if(selected_foods.Count > 0)
                ordr.transactionHistory(readTransactFile(), selected_foods, totalPrice);
            Console.ReadKey();
        }
        static void POS()
        {
            allFood drinks = new allFood("Alldrinks.csv");
            allFood desserts = new allFood("Alldesserts.csv");
            string ans = "";

            while (true)
            {
                Console.Clear();
                ans = startMenu();

                if (ans == "A")
                    foodOption("DRINKS", drinks, ConsoleColor.DarkYellow, ConsoleColor.Yellow);
                else if (ans == "B")
                    foodOption("DESSERTS", desserts, ConsoleColor.DarkGreen, ConsoleColor.Green);
                
                else if (ans == "C")
                {
                    if (selected_foods.Count > 0)
                    {
                        ordr.processPayment(selected_foods, totalPrice);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You must order to proceed to payment.");
                        Console.ReadKey();
                    }
                }
                else if (ans == "D")
                {
                    Console.Clear();
                    break;
                }
                else if (ans == "E")
                {
                    
                }
            }
        }
        static string readTransactFile()
        {
            List<string> transactLines = new List<string>();
            string transactID = "";
            try
            {
                string line = "";
                using (StreamReader sr = new StreamReader("Transaction History.csv"))
                {
                    while ((line = sr.ReadLine()) != null)
                        transactLines.Add(line);
                    transactID = "TR" + (transactLines.Count);
                }
            }
            catch (Exception)
            {
                transactID = "TR1";
            }
            return transactID;
        }
        static void foodOption(string category, allFood fOption, ConsoleColor fColor, ConsoleColor bColor)
        {
            Console.Clear();
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(category);
            Console.ResetColor();
            Console.ForegroundColor = fColor;
            fOption.displayContents();
            Console.ResetColor();

            while (orderStatus())
            {
                selected_foods = fOption.getFoods(selected_foods);
                Console.Clear();
                Console.ForegroundColor = fColor;
                Console.Clear();
                fOption.displayContents();
                Console.ResetColor();
            }
        }
        static string startMenu()
        {
            string input = "";
            while(input != "A" && input != "B" && input != "C" && input != "D")
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Welcome to Lei's Cafe");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Please refer to the menu below to view our available treats\n" +
                                  "What do you want to do?");
                Console.ResetColor();
                Console.WriteLine("Type here: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n[A] View Drinks");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[B] View Desserts");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("[C] Proceed to Payment");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[D] Exit cafe\n");
                Console.ResetColor();
                totalPrice = ordr.displayCurrent(selected_foods);
                Console.SetCursorPosition(11, 3);
                input = uInput("");
                if (input != "A" && input != "B" && input != "C" && input != "D")
                {
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                }
            }
            return input;
        }
        static string uInput (string input)
        {
            input = Console.ReadLine().ToUpper();
            return input;
        }
        static bool orderStatus()
        {
            bool cont = true;
            string ans = "";
            
            while(ans != "O" && ans != "R")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nWould you like to order or return to main menu?");
                Console.ResetColor();
                Console.WriteLine("[O] - Order\n" +
                                  "[R] - Return");
                ans = uInput("");
                if (ans == "O")
                    break;
                else if(ans == "R") 
                    cont = false;
            }
            return cont;
        }
    }
}
