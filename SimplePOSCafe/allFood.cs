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
        public List<food_items> selected_foods = new List<food_items>();

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
            Console.Clear();
            foreach(food_items food in foods)
            {
                Console.WriteLine($"{food.food_id()} | {food.name()}: Php{food.price()}");
            }
            Console.ResetColor();
        }
        public List<food_items> getFoods(List<food_items> selected)
        {
            string choice = "";
            choice = getFoodInput("");

            foreach (food_items food in foods)
            {
                if (food.food_id() == choice)
                {
                    selected.Add(food);
                    Console.WriteLine("Order Added!\n Press any key to resume. . .");
                    Console.ReadKey();
                    break;
                }
            }
            return selected;
        }
        public string getFoodInput (string input)
        {
            Console.WriteLine("Select the ID of your chosen order: ");
            input = Console.ReadLine().ToUpper();
            return input;
        }
    }
}
