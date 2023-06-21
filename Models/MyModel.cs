using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Models
{

    public class MyModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class Product
    {
        public string Name { get; set; }       
    }

    public class Event
    {
        public string Name { get; set; }
    }

    public class Card
    {
        public string Name { get; set; }
    }

}
