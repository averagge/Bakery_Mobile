using System;
using System.Collections.Generic;
using System.Text;

namespace App3
{
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public int Price {  get; set; }
        public int Weight { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
