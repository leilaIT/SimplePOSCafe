using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimplePOSCafe
{
    internal class order
    {
        public void processPayment(List<food_items> selected_foods, int totalPrice)
        {
            int payment = 0;
            int change = 0;
            //show all orders and total price
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Payment");
            Console.ResetColor();
            Console.WriteLine("Here are your order(s) along with the total price:");
            totalPrice = displayCurrent(selected_foods);

            while (true)
            {
                //ask user for payment
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                while(true)
                {
                    Console.WriteLine("Please enter payment amount: ");
                    Console.ResetColor();
                    //payment = int.Parse(Console.ReadLine());
                    if(int.TryParse(Console.ReadLine(), out payment))
                        break;
                }

                //process payment
                if (payment >= totalPrice)
                {
                    change = payment - totalPrice;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Insufficient payment to process order");
                    Console.ResetColor();
                }
            }

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Received payment: {payment}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Processing Payment");
            for(int x = 0; x < 10; x++) 
            {
                Console.Write(". ");
                Thread.Sleep(250);
            }
            Console.WriteLine($"\n\nYour change is: Php{change}");
            Console.ResetColor();

            //print receipt
            printReceipt(selected_foods, totalPrice, change, payment);
        }
        public int displayCurrent(List<food_items> selected_foods)
        {
            int totalPrice = 0;
            Console.WriteLine("—————————————————————————————————");
            Console.WriteLine($"Your orders: {selected_foods.Count}");

            foreach (food_items food in selected_foods)
            {
                Console.WriteLine($"{food.food_id()} | {food.name()} : {food.price()}");
                totalPrice += food.price();
            }

            Console.WriteLine($"\nCurrent price total: {totalPrice}\n");
            Console.WriteLine("—————————————————————————————————");
            
            return totalPrice;
        }
        private void printReceipt(List<food_items> selected_foods, int totalPrice, int change, int payment)
        {
            DateTime dtnow = DateTime.Now;
            int numSpace = 0;
            string spaces = "";
            using (StreamWriter sw = new StreamWriter("Order Receipt.txt"))
            {
                sw.WriteLine(" ———————————————————————————————— ");
                sw.WriteLine("             RECEIPT             ");
                sw.WriteLine("            Lei's cafe           ");
                sw.WriteLine("          Sample Address         ");
                sw.WriteLine($"      {dtnow}                   ");
                sw.WriteLine(" ———————————————————————————————— ");
                sw.WriteLine();
                sw.WriteLine("ID  | x Name and Food Price");
                foreach (food_items food in selected_foods)
                {
                    numSpace = 33 - (food.food_id().Length + food.name().Length + food.price().ToString().Length + 10);
                    if(numSpace < 0) 
                        numSpace = 0;
                    spaces = new string(' ', numSpace);
                    sw.WriteLine($"{food.food_id()} | {food.name()}{spaces}: Php{food.price()}");
                    totalPrice += food.price();
                }
                sw.WriteLine(" ———————————————————————————————— ");
                sw.WriteLine($"Total:                   Php{totalPrice}");
                sw.WriteLine($"Amount Paid:             Php{payment}");
                sw.WriteLine($"Change:                  Php{change}");
                sw.WriteLine(" ———————————————————————————————— ");
                sw.WriteLine("Thank you. We hope you come again!");
                sw.WriteLine(" ———————————————————————————————— ");
            }
        }
    }
}
