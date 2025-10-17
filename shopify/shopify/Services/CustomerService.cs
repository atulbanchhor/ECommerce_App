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
            List<string> lines = new List<string>();

            foreach (var c in customers)
            {
                string l = c.Id + "," + c.Name + "," + c.Phone; 
                lines.Add(l);  
            }
            
            FileService.SaveToFile(filePath, lines);
        }

        
        public Customer GetOrCreateByPhone(string name, string phone)    // If phone exists, return existing customer, otherwise create new one
        {
            Customer available  = null;  

            foreach (var c in customers)
            {
                if (c.Phone == phone)   
                {
                    available  = c;       // to us customer ko save kar lo
                    break;              // mil gaya to loop se nikal jao
                }
            }
            
            if (available != null) return available;

            int newId;

            if (customers.Count > 0)
            {
                int maxId = 0;

                foreach (var c in customers)
                {
                    if (c.Id > maxId)
                    {
                        maxId = c.Id;   // sabse badi ID find karo usko ek + kar 
                    }
                }

                newId = maxId + 1;     
            }
            else
            {
                newId = 1;             // agar list empty hai, pehla ID 1
            }

           
            Customer customer = new Customer();
            customer.Id = newId;
            customer.Name = name;
            customer.Phone = phone;
            customers.Add(customer);
            SaveCustomers();
            return customer;
        }

        public Customer GetById(int id)
        {
            Customer cust = null;  // pehle null se start kar diya

            foreach (var c in customers)
            {
                if (c.Id == id)
                {
                    cust = c;   // agar match mil gaya to us customer ko save kara
                    break;      // mil gaya to loop se bahar aagaya
                }
            }
            if (cust == null) throw new CustomerNotFoundException($"Customer with ID {id} not found.");
            return cust;
        }

        public List<Customer> GetAll() => customers;
    }
}
