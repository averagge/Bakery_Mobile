using System;
using System.Collections.Generic;
using System.Text;

namespace App3
{
    public class UserOrder
    {
        public int Id;
        public int Total;
        public UserOrder(int Id, int Total) 
        {
            this.Id = Id;
            this.Total = Total;
        } 
    }
}
