using shopify.Enum;
using shopify.Models;
using shopify.Services;

namespace shopify
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ProductService productService = new ProductService();
            CustomerService customerService = new CustomerService();
            OrderService orderService = new OrderService();

            bool choicea = false;
            bool valid = false;

            while (!valid)
            {

                Console.WriteLine("______ Welcome to shopify ______");
                Console.WriteLine("Login as:");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Customer");
                Console.Write("Enter choice (1 or 2): ");
                string roleInput = Console.ReadLine();

                if (roleInput == "1")
                {
                    choicea = true;
                    valid = true;
                }
                else if (roleInput == "2")
                {
                    choicea = false;
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Please entre correct input 1 for admin and 2 for customer \n");
                }
            }
                      

           
            //----------------------------------------------------------------------------------------
            
            while (true)
            {
                try
                {
                    if (choicea)
                    {
                        // admin ---------------------------------------------------

                        Console.WriteLine("\n______ ADMIN MENU ______");
                        Console.WriteLine("1. Add Product");
                        Console.WriteLine("2. List Products");
                        Console.WriteLine("3. Update Product");
                        Console.WriteLine("4. Delete Product");
                        Console.WriteLine("5. Exit");
                        Console.Write("Enter choice: ");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                Console.Write("Product Name: ");
                                string pname = Console.ReadLine();

                                Console.Write("Product Price: ");
                                double pprice= Convert.ToDouble(Console.ReadLine());

                                productService.Add(new Product { Name = pname, Price = pprice });
                                Console.WriteLine(ConstValues.SUCCESSFULLY_ADDED);
                                break;

                            case "2":
                                Console.WriteLine("Products:");
                                var all = productService.GetAll();
                                if (all.Count == 0)
                                {
                                    Console.WriteLine("No products available.");
                                }

                                foreach (var p in all)
                                {
                                    Console.WriteLine(p);
                                }
                                break;

                            case "3":
                                Console.Write("Enter Product ID to update: ");
                                string inputId = Console.ReadLine();
                                int upId;

                                try
                                {
                                    upId = Convert.ToInt32(inputId); 
                                }
                                catch
                                {
                                    Console.WriteLine(" Invalid ID! Please enter a number.");
                                    break;                                                           
                                }

                                
                                var allProducts = productService.GetAll();
                                Product prod = null;        // assuming here that product not found

                                
                                foreach (var p in allProducts)
                                {
                                    if (p.Id == upId)
                                    {
                                        prod = p; // product found
                                        break;    // exit loop
                                    }
                                }
                                if (prod == null)
                                {
                                    Console.WriteLine(ConstValues.NOT_FOUND); 
                                    break; 
                                }


                                Console.Write("New Name: ");
                                prod.Name = Console.ReadLine();
                                Console.Write("New Price: ");
                                string inputPrice = Console.ReadLine();
                                double newP;

                                try
                                {
                                    newP = Convert.ToDouble(inputPrice); 
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid price! Please enter a valid number.");
                                    break; 
                                }
                                prod.Price = newP;
                                productService.Update(prod);
                                break;

                            case "4":
                                Console.Write("Enter Product ID to delete: ");
                                if (!int.TryParse(Console.ReadLine(), out int delId))
                                {
                                    Console.WriteLine(ConstValues.INVALID);
                                    break;
                                }
                                productService.Delete(delId);
                                break;

                            case "5":
                                Console.WriteLine("logged out.");
                                return;

                            default:
                                Console.WriteLine(ConstValues.INVALID + "choice (1-5).");
                                break;
                        }
                    }
                    else
                    {
                        //  customer ---------------------------------------------------

                        Console.WriteLine("\n______ CUSTOMER MENU ______");
                        Console.WriteLine("1. List Products");
                        Console.WriteLine("2. Place Order");
                        Console.WriteLine("3. View Order History");
                        Console.WriteLine("4. Exit");
                        Console.Write("Enter choice: ");
                        string cchoice = Console.ReadLine();

                        switch (cchoice)
                        {
                            case "1":
                                Console.WriteLine("\nProducts:");
                                var list = productService.GetAll();
                                if (list.Count == 0)
                                {
                                    Console.WriteLine("No products available.");
                                }
                                foreach (var p in list)
                                {
                                    Console.WriteLine(p);
                                }
                                break;

                            case "2":
                                Console.Write("Enter your name: ");
                                string cname = Console.ReadLine();
                                Console.Write("Enter your phone number: ");
                                string cphone = Console.ReadLine();

                                var customer = customerService.GetOrCreateByPhone(cname, cphone);
                                Console.WriteLine($" Hello {customer.Name}, Your Customer ID: {customer.Id}");

                                Console.WriteLine("\nAvailable Products are:");
                                var products = productService.GetAll();
                                if (products.Count == 0)
                                {
                                    Console.WriteLine("No products to order.");
                                    break; 
                                }

                                foreach (var p in products) Console.WriteLine(p);

                                Console.Write("Enter Product ID to order: ");
                                string inputId = Console.ReadLine();
                                int pid;                                            

                                try
                                {
                                    pid = Convert.ToInt32(inputId); 
                                }
                                catch
                                {
                                    Console.WriteLine(" Invalid Product ID! Please enter a number.");
                                    break; 
                                }

                                Product selected = null;
                                foreach(var p in products)
                                {
                                    if(p.Id == pid)         // here i am checking the product id is matches
                                    {
                                        selected = p;       // product found here
                                        break;
                                    }
                                }
                                if (selected == null)
                                {
                                    Console.WriteLine(" Product not found.");
                                    break;
                                }

                                Console.Write("Enter Quantity: ");
                                string inputQty = Console.ReadLine();
                                int qty;

                                try
                                {
                                    qty = Convert.ToInt32(inputQty); 
                                    if (qty <= 0)
                                    {
                                        Console.WriteLine("Quantity must be greater than 0.");
                                        break; 
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine(" Invalid quantity! Please enter a valid number.");
                                    break; 
                                }
                                orderService.PlaceOrder(customer, selected, qty);
                                break;

                            case "3":
                                Console.Write("Enter your Customer ID: ");              // ----------complex
                                string inputCid = Console.ReadLine();
                                int cid;

                                try
                                {
                                    cid = Convert.ToInt32(inputCid); 
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid Customer ID! Please enter a valid number.");
                                    break; 
                                }
                                try
                                {
                                    var cust = customerService.GetById(cid);
                                    var custOrders = orderService.GetOrdersByCustomer(cid);
                                    Console.WriteLine($"\nOrder History for {cust.Name}:");
                                    if (custOrders.Count == 0)
                                    {
                                        Console.WriteLine(" No orders found.");
                                    }

                                    foreach (var o in custOrders)
                                    {
                                        Product prod = null;
                                        var allProducts = productService.GetAll(); // saare products uth k yaha aaye aur phir check 

                                        foreach (var p in allProducts)
                                        {
                                            if (p.Id == o.ProductId)            //  order ka product ID match 
                                            {
                                                prod = p;
                                                break; // product mil gaya, loop se nikal jao
                                            }
                                        }
                                        if (prod != null)
                                        {
                                            Console.WriteLine($"{o} | ProductName: {prod.Name}");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{o} | ProductName: Unknown");
                                        }
                                    }
                                }
                                catch (CustomerNotFoundException cnf)
                                {
                                    Console.WriteLine($"{cnf.Message}");
                                }
                                break;

                            case "4":
                                Console.WriteLine("Thank you for visiting shopify!");
                                return;

                            default:
                                Console.WriteLine(" Invalid choice (1-4).");
                                break;
                        }
                    }
                }
                catch (CustomException pnfe)
                {
                    Console.WriteLine($" Product Error: {pnfe.Message}");
                }
                catch (CustomerNotFoundException cnfe)
                {
                    Console.WriteLine($"Customer Error: {cnfe.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }
            }
        }
    }
}
