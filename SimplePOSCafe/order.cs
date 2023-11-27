using System;
using System.Collections.Generic;
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
            Console.WriteLine("Payment\n" +
                              "Here are your order(s) along with the total price:");
            Console.WriteLine("—————————————————————————————————");
            foreach (food_items food in selected_foods)
            {
                Console.WriteLine($"{food.food_id()} | {food.name()} : {food.price()}");
            }
            Console.WriteLine($"\nCurrent price total: {totalPrice}");
            Console.WriteLine("—————————————————————————————————\n");

            while (true)
            {
                //ask user for payment
                Console.WriteLine("Please enter payment amount: ");
                payment = int.Parse(Console.ReadLine());
                
                //process payment
                if (payment >= totalPrice)
                {
                    change = payment - totalPrice;
                    break;
                }
                else
                    Console.WriteLine("Insufficient payment to process order");
            }
            Console.Clear();
            Console.WriteLine($"Received payment: {payment}");
            Console.WriteLine("Processing Payment");
            for(int x = 0; x < 10; x++) 
            {
                Console.Write(". ");
                Thread.Sleep(250);
            }
            Console.WriteLine($"\n\nYour change is: {change}");
            //print receipt
        }
    }
}
