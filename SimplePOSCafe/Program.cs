using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
        //the menu should not be hardcoded into the program but must be referenced.
        //The POS must also print out receipts for the customers.
        //Transaction history must also be kept track of. (idea: use datetime)
        
        //FLOW
        //after selecting category
        //  ask: would you like to add an order or go back to main page? [DONE]
        //  if a (chooses to order)
        //      -add chosen order info to current order list [DONE]
        //      -go back to main menu [DONE]
        //  else b(doesnt want to order from the options)
        //      -gp back to main menu [DONE]

        static List<food_items> selected_foods = new List<food_items>();
        static int totalPrice = 0;
        static void Main(string[] args)
        {
            allFood drinks = new allFood("Alldrinks.csv");
            allFood desserts = new allFood("Alldesserts.csv");
            order ordr = new order();
            string ans = "";

            //start menu
            while(true)
            {
                Console.Clear();
                ans = startMenu();

                //choose which food option
                if (ans == "A")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    drinks.displayContents();
                    while (orderStatus())
                    {
                        selected_foods = drinks.getFoods(selected_foods);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        drinks.displayContents();
                    }
                }
                else if (ans == "B")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    desserts.displayContents();
                    while (orderStatus())
                    {
                        selected_foods = desserts.getFoods(selected_foods);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        desserts.displayContents();
                    }
                }
                else if(ans == "C")
                {
                    ordr.processPayment(selected_foods, totalPrice);
                    break;
                }
                else if (ans == "D")
                {
                    Console.Clear();
                    break;
                }
            }
            Console.WriteLine("Thank you. Please come again!\nPress any key to exit. . .");
            Console.ReadKey();
        }
        static string startMenu()
        {
            string input = "";
            while(input != "A" && input != "B" && input != "C" && input != "D")
            {
                Console.Clear();
                Console.WriteLine("Welcome to something cafe\n" +
                                  "Please refer to the menu below to view our available treats\n" +
                                  "What do you want to do?\n" +
                                  "Type here: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n[A] Drinks");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[B] Desserts");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("[C] Proceed to Payment");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[D] Exit cafe\n");
                Console.ResetColor();
                displayCurrent();
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
            Console.WriteLine("Would you like to order or return to main menu?\n" +
                              "[O] - Order\n" +
                              "[R] - Return");
            if(uInput("") == "R")
                cont = false;
            return cont;
        }
        static void displayCurrent()
        {
            Console.WriteLine("—————————————————————————————————");
            Console.WriteLine($"Your orders: {selected_foods.Count}");
            
            foreach (food_items food in selected_foods)
            {
                Console.WriteLine($"{food.food_id()} | {food.name()} : {food.price()}");
                totalPrice += food.price();
            }

            Console.WriteLine($"\nCurrent price total: {totalPrice}");
        }
    }
}
