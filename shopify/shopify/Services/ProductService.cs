using shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopify.Services
{
    public class ProductService : IRepository<Product>      
    {
        private string filePath = @"C:\Users\ibz\Desktop\Tasks\shopify\shopify\Data\products.txt";

        private List<Product> products = new List<Product>();

        public ProductService()
        {
            LoadProducts();         //jub bhi product service class ka object create hoga, to ye LoadProducts() automaticaly call ho jayega 
        }

        private void LoadProducts()
        {
            var lines = FileService.ReadFromFile(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int id) &&
                    double.TryParse(parts[2], out double price))
                {
                    products.Add(new Product(id, parts[1], price));
                }
            }
        }

        private void SaveProducts()
        {
            List<string> lines = new List<string>();

            foreach (var p in products)
            {
                string line = p.Id + "," + p.Name + "," + p.Price;  
                lines.Add(line);                                     
            }
            
            FileService.SaveToFile(filePath, lines);
        }

        public void Add(Product product)
        {
            int newId;

            if (products.Count > 0)
            {
                int maxId = 0;

                foreach (var p in products)
                {
                    if (p.Id > maxId)
                    {
                        maxId = p.Id;   
                    }
                }

                newId = maxId + 1;    
            }
            else
            {
                newId = 1;            // agar list empty hai, pehla ID 1
            }

            product.Id = newId;          
            products.Add(product);
            SaveProducts();
        }

        public void Update(Product product)
        {
            Product existing = null;  

            foreach (var p in products)
            {
                if (p.Id == product.Id)   // agar product ID match ho gaya
                {
                    existing = p;         // us product ko variable me store karo
                    break;                // mil gaya to loop se bahar aa jao
                }
            }        

            if (existing == null)
                throw new CustomException($"Product with ID {product.Id} not found.");

            existing.Name = product.Name;
            existing.Price = product.Price;
            SaveProducts();
        }

        public void Delete(int id)
        {
            Product prod = null;  

            foreach (var p in products)
            {
                if (p.Id == id)   
                {
                    prod = p;     
                    break;        
                }
            }

          
            if (prod == null)
                throw new CustomException($"Product with ID {id} not found.");

            products.Remove(prod);
            SaveProducts();
        }

        public List<Product> GetAll()
        {
            return products;  
        }     
    }
}
