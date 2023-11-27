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
        //SCENARIO
        //Using csv files as a pseudo database create a simple POS for a café. [DONE]
        //You may use more than one csv file for this project. [DONE]
        //The POS you will be making for the café must use a menu, [DONE]
        //the menu should not be hardcoded into the program but must be referenced. [DONE]
        //The POS must also print out receipts for the customers. [DONE]
        //Transaction history must also be kept track of. (idea: use datetime) [DONE]

        static List<food_items> selected_foods = new List<food_items>();
        static int totalPrice = 0;
        static order ordr = new order();

        static void Main(string[] args)
        {
            allFood drinks = new allFood("Alldrinks.csv");
            allFood desserts = new allFood("Alldesserts.csv");
            string ans = "";

            //start menu
            while(true)
            {
                Console.Clear();
                ans = startMenu();

                //choose which food option
                if (ans == "A")
                    foodOption(drinks, ConsoleColor.DarkYellow);
                else if (ans == "B")
                    foodOption(desserts, ConsoleColor.DarkGreen);
                else if(ans == "C")
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
            }
            Console.WriteLine("Thank you. We hope you come again!\nPress any key to exit. . .");
            ordr.transactionHistory(readTransactFile(), selected_foods, totalPrice);
            Console.ReadKey();
        }
        static string readTransactFile()
        {
            List<string> transactLines = new List<string>();
            string transactID = "";
            try
            {
                string line = "";
                using (StreamReader sr = new StreamReader("Transaction History.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                        transactLines.Add(line);
                    transactID = "TR" + (transactLines.Count + 1);
                }
            }
            catch (Exception e)
            {
                transactID = "TR1";
                Console.WriteLine();
            }
            return transactID;
        }
        static void foodOption(allFood fOption, ConsoleColor fColor)
        {
            Console.ForegroundColor = fColor;
            Console.Clear();
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
                Console.WriteLine("Please refer to the menu below to view our available treats\n" +
                                  "What do you want to do?\n" +
                                  "Type here: ");
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
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            
            while(ans != "O" && ans != "R")
            {
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
