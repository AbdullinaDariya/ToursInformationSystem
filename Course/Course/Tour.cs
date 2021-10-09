using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    public class Tour

    {
        public string Country { get; set; }
        public string Hotel { get; set; }
        public int Price { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }


        //Конструкторы
        public Tour(string country, string hotel, int price, string date1, string date2)
        {
            Country = country;
            Hotel = hotel;
            Price = price;
            Date1 = date1;
            Date2 = date2;
        }

        public Tour()
        { }
    }
}
