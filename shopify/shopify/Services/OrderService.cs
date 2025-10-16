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
            var lines = orders.Select(o => $"{o.OrderId},{o.CustomerId},{o.ProductId},{o.Quantity},{o.TotalPrice}").ToList();
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
                    Thread.Sleep(50);
                    Console.Write(".");
                }
            });
            t.Start();
            //t.Join();

            double total = Math.Round(product.Price * quantity, 2);
            var order = new Order
            {
                OrderId = orders.Count > 0 ? orders.Max(o => o.OrderId) + 1 : 1,
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
            return orders.Where(o => o.CustomerId == customerId).ToList();
        }
    }
}
