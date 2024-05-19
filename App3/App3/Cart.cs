using System;
using System.Collections.Generic;
using System.Text;

namespace App3
{
    public class Cart
    {
        public static List<Good> goods = new List<Good>();
        public static bool Contains(int id)
        {
            bool contain = false;
            foreach(Good good in goods)
            {
                if(good.Id == id)
                {
                    contain = true;
                }
            }

            return contain;
        }
    }
    
}
