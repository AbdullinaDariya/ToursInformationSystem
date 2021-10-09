using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    public class Touroperator

    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public List<Tour> Tours { get; set; }


        //Вычисляемое свойство
        public int Count
        {
            get
            {
                if (Tours == null)
                    return 0;
                else return Tours.Count;
            }
        }
        //Конструкторы
        public Touroperator(string name, string phone, string mail, string address, List<Tour> tours)
        {
            Name = name;
            Phone = phone;
            Mail = mail;
            Address = address;
            Tours = tours;
        }
        
        public Touroperator()
        {
            Tours = new List<Tour>();
        }

    }

}
