using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopify.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString()
        {
            return $"OrderID: {OrderId}, CustomerID: {CustomerId}, ProductID: {ProductId}, Qty: {Quantity}, Total: ₹{TotalPrice}";
        }
    }
}
