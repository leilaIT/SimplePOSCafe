﻿using System;
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
            int quant = 0;
            int amount = 0;
            string nameNum = "";
            string inp = "";

            quant = getFoodQuant(quant);

            foreach (food_items food in foods)
            {
                if (food.food_id() == choice)
                {
                    amount = food.price() * quant;
                    nameNum = quant + " " + food.name();
                    food_items selectedFood = new food_items(food.food_id(), nameNum, amount);
                    selected.Add(selectedFood);
                    Console.WriteLine("\nOrder Added! Press any key to resume. . .");
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
                Console.WriteLine("\nHow many would you like to order?");
                input = Console.ReadLine();
                if (int.TryParse(input, out quant))
                    break;
                else
                    Console.WriteLine("Invalid input");
            }
            return quant;
        }
        public string getFoodInput (string input)
        {
            bool cont = true;

            while(cont)
            {
                Console.WriteLine("Select the ID of your chosen order: ");
                input = Console.ReadLine().ToUpper();
                foreach (food_items food in foods)
                {
                    if (food.food_id() == input)
                    {
                        cont = false;
                        break;
                    }
                }
                if(cont)
                    Console.WriteLine("System does not contain that ID.\n");
            }
            
            return input;
        }
    }
}
