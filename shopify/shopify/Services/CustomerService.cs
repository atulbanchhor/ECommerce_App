using shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopify.Services
{
    
    public class CustomerService
    {
        private string filePath = @"C:\Users\ibz\Desktop\Tasks\shopify\shopify\Data\customers.txt";
        List<Customer> customers = new List<Customer>();

        public CustomerService()
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var lines = FileService.ReadFromFile(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3 && int.TryParse(parts[0], out int id))
                {
                    customers.Add(new Customer { Id = id, Name = parts[1], Phone = parts[2] });
                }
            }
        }

        private void SaveCustomers()
        {
            var lines = customers.Select(c => $"{c.Id},{c.Name},{c.Phone}").ToList();
            FileService.SaveToFile(filePath, lines);
        }

        // If phone exists, return existing customer, otherwise create new one
        public Customer GetOrCreateByPhone(string name, string phone)
        {
            var existing = customers.FirstOrDefault(c => c.Phone == phone);
            if (existing != null) return existing;

            var customer = new Customer
            {
                Id = customers.Count > 0 ? customers.Max(c => c.Id) + 1 : 1,
                Name = name,
                Phone = phone
            };
            customers.Add(customer);
            SaveCustomers();
            return customer;
        }

        public Customer GetById(int id)
        {
            var cust = customers.FirstOrDefault(c => c.Id == id);
            if (cust == null) throw new CustomerNotFoundException($"Customer with ID {id} not found.");
            return cust;
        }

        public List<Customer> GetAll() => customers;
    }
}
