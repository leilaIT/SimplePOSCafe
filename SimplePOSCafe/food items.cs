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
        private string foodName = "";
        private int foodPrice = 0;
        private string foodid = "";
        public food_items(string fId, string fName, int fPricw)
        {
           foodid = fId;
           foodName = fName;
           foodPrice = fPricw;
        }
        public string food_id()
        {
            return foodid;
        }
        public string name()
        { 
            return foodName; 
        }
        public int price()
        {
            return foodPrice;
        }
    }
}
