using shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopify.Services
{
    // Order service: place order, list by customer
    public class OrderService
    {
        private readonly string filePath = @"C:\Users\ibz\Desktop\Tasks\shopify\shopify\Data\orders.txt";
        List<Order> orders = new List<Order>();

        public OrderService()
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            var lines = FileService.ReadFromFile(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 5 &&
                    int.TryParse(parts[0], out int oid) &&          // tryparse return boolean value 
                    int.TryParse(parts[1], out int cid) &&
                    int.TryParse(parts[2], out int pid) &&
                    int.TryParse(parts[3], out int qty) &&
                    double.TryParse(parts[4], out double total))
                {
                    orders.Add(new Order
                    {
                        OrderId = oid,
                        CustomerId = cid,
                        ProductId = pid,
                        Quantity = qty,
                        TotalPrice = total
                    });
                }
            }
        }

        private void SaveOrders()
        {
            List<string> lines = new List<string>();

            foreach (var o in orders)
            {
                string line = o.OrderId + "," + o.CustomerId + "," + o.ProductId + "," + o.Quantity + "," + o.TotalPrice;
                lines.Add(line);
            }       
            FileService.SaveToFile(filePath, lines);
        }

        
        public void PlaceOrder(Customer customer, Product product, int quantity)
        {
            if (product == null)
                throw new CustomException("Selected product does not exist.");

            
            Thread t = new Thread(() =>
            {
                Console.Write("Processing order");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(5000);
                    Console.Write(".");
                }
            });
            t.Start();
            



            double total = Math.Round(product.Price * quantity, 2);

            
            int newOrderId;

            if (orders.Count > 0)
            {
                int maxOrderId = 0;

                foreach (var o in orders)
                {
                    if (o.OrderId > maxOrderId)
                    {
                        maxOrderId = o.OrderId;   
                    }
                }

                newOrderId = maxOrderId + 1;      
            }
            else
            {
                newOrderId = 1;  // agar koi order nahi hai to start from 1
            }

           
            var order = new Order
            {
                OrderId = newOrderId,
                CustomerId = customer.Id,
                ProductId = product.Id,
                Quantity = quantity,
                TotalPrice = total
            };

            orders.Add(order);
            SaveOrders();
            Console.WriteLine($"\n Order placed successfully! Total: ₹{total}");
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            List<Order> customerOrders = new List<Order>();

            foreach (var o in orders)
            {
                if (o.CustomerId == customerId)  
                {
                    customerOrders.Add(o);       
                }
            }

            return customerOrders;
        }
    }
}
