using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOSCafe
{
    internal class food_items
    {
        private List<string[]> items = new List<string[]>();
        private string[] tempArr = new string[] { };
        private string tempWord = "";
        public food_items(string fileName)
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
                    items.Add(tempArr);
                }
            }
        }
        public void displayItem()
        {
            for (int x = 0; x < items.Count; x++)
            {
                for (int y = 0; y < items[x].Length; y++)
                {
                    Console.WriteLine(items[x][y]);
                }
            }
        }
    }
}
