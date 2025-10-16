using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopify.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"Customer ID: {Id}, Name: {Name}, Phone: {Phone}";
        }
    }
}
