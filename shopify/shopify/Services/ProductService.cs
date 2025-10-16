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
            var lines = products.Select(p => $"{p.Id},{p.Name},{p.Price}").ToList();
            FileService.SaveToFile(filePath, lines);
        }

        public void Add(Product product)
        {
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            SaveProducts();
        }

        public void Update(Product product)
        {
            var existing = products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null)
                throw new CustomException($"Product with ID {product.Id} not found.");

            existing.Name = product.Name;
            existing.Price = product.Price;
            SaveProducts();
        }

        public void Delete(int id)
        {
            var prod = products.FirstOrDefault(p => p.Id == id);
            if (prod == null)
                throw new CustomException($"Product with ID {id} not found.");

            products.Remove(prod);
            SaveProducts();
        }

        public List<Product> GetAll() => products;
    }
}
