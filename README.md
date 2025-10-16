-Commerce Console Application (Using File Handling)

This is a small console-based E-commerce system with two types of users: Admin and Customer.

Features:

Admin:

Can add, update, and delete products.

Can view the list of products.

Product data is stored in files (like products.txt) so that data is not lost when the program is closed.

Customer:

Can view the list of products.

Can place orders by selecting products and quantities.

Can view their order history.

Customer and order data are also saved in text files.

Use of File Handling:

All data (Products, Customers, Orders) is read from and written to text files.

After every operation, data is persisted so it remains even if the program is restarted.

Flow:

User logs in (as Admin or Customer).

Menu is displayed according to the role.

User selects an operation → program reads/writes data from/to files.

All changes are saved safely using file handling.

✅ Summary:

This is a simple console-based E-commerce application that uses file handling to store and manage products, customers, and orders. It’s beginner-friendly because it does not use databases or complex libraries—everything is handled using plain text files.
