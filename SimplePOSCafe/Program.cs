using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOSCafe
{
    internal class Program
    {
        //*create a constructor for each food category
        //with parameter string fileName (this is the file to be read)
        //in constructor method, put all the code in "reading CSV file"

        static void Main(string[] args)
        {
            Dictionary<string, List<string[]>> menu = new Dictionary<string, List<string[]>>();
            List<string[]> drinks = new List<string[]>();
            string[] tempArr = new string[] { };
            string tempWord = "";

            //store food items from CSV to program
            List<food_items> foods = new List<food_items>();
            foods.Add(new food_items("Alldrinks.csv"));
            foods.Add(new food_items("Alldesserts.csv"));

            //display all food items
            foreach (food_items item in foods)
                item.displayItem();

            Console.ReadKey();
            //reading csv file
            using (StreamReader sr = new StreamReader("Alldrinks.csv"))
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
                    drinks.Add(tempArr);
                }
            }

        }
    }
}
