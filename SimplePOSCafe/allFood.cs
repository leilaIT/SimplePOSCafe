using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOSCafe
{
    internal class allFood
    {
        private string[] tempArr = new string[] { };
        private string tempWord = "";
        public List<food_items> foods = new List<food_items>();
        public allFood(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    tempArr = line.Split(',');
                    //remove space from word
                    for (int x = 0; x < tempArr.Length; x++)
                    {
                        if (tempArr[x][0] == ' ') //if first letter is space
                        {
                            tempWord = tempArr[x];
                            tempArr[x] = "";
                            for (int i = 1; i < tempWord.Length; i++)
                                tempArr[x] += tempWord[i];
                        }
                    }
                    foods.Add(new food_items(tempArr[0], tempArr[1], int.Parse(tempArr[2])));
                }
            }
        }
        public void displayContents()
        {
            foreach(food_items food in foods)
                Console.WriteLine($"{food.food_id()} | {food.name()}: Php{food.price()}");
        }
        public List<food_items> getFoods(List<food_items> selected)
        {
            string choice = "";
            choice = getFoodInput("");
            int quant = 0;
            int amount = 0;
            string nameNum = "";

            quant = getFoodQuant(quant);

            foreach (food_items food in foods)
            {
                if (food.food_id() == choice)
                {
                    amount = food.price() * quant;
                    nameNum = quant + " " + food.name();
                    food_items selectedFood = new food_items(food.food_id(), nameNum, amount);
                    selected.Add(selectedFood);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nOrder Added! Press any key to resume. . .");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
                }
            }
            return selected;
        }
        public int getFoodQuant(int quant)
        {
            string input = "";

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nHow many would you like to order?");
                Console.ResetColor();
                input = Console.ReadLine();
                if (int.TryParse(input, out quant))
                    break;
                else
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Invalid input");
                    Console.ResetColor();
                }
            }
            return quant;
        }
        public string getFoodInput (string input)
        {
            bool cont = true;

            while (cont)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nSelect the ID of your chosen order: ");
                Console.ResetColor();
                input = Console.ReadLine().ToUpper();
                foreach (food_items food in foods)
                {
                    if (food.food_id() == input)
                    {
                        cont = false;
                        break;
                    }
                }
                if (cont)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("System does not contain that ID.");
                    Console.ResetColor();
                }
            }
            
            return input;
        }
    }
}
