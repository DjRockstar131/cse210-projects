using System;
using System.Collections.Generic;

namespace OnlineOrdering
{
    class Program
    {
        static void Main(string[] args)
        {
            // ----- Create Addresses -----
            Address usaAddress = new Address(
                "123 Elm Street",
                "Boise",
                "ID",
                "USA");

            Address internationalAddress = new Address(
                "456 Maple Road",
                "Toronto",
                "ON",
                "Canada");

            // ----- Create Customers -----
            Customer customer1 = new Customer("Alice Johnson", usaAddress);
            Customer customer2 = new Customer("Liam O’Connor", internationalAddress);

            // ----- Create Orders -----
            Order order1 = new Order(customer1);
            Order order2 = new Order(customer2);

            // Add 2–3 products to each order
            order1.AddProduct(new Product("Gaming Mouse", "GM101", 29.99, 2));
            order1.AddProduct(new Product("Mechanical Keyboard", "MK202", 79.99, 1));
            order1.AddProduct(new Product("Mouse Pad", "MP303", 9.99, 3));

            order2.AddProduct(new Product("Noise-Cancelling Headphones", "NH404", 149.99, 1));
            order2.AddProduct(new Product("Webcam 1080p", "WC505", 59.99, 2));

            // Put orders in a list to iterate
            List<Order> orders = new List<Order> { order1, order2 };

            // ----- Display Info for Each Order -----
            int orderNumber = 1;
            foreach (Order order in orders)
            {
                Console.WriteLine("========================================");
                Console.WriteLine($"Order #{orderNumber}");
                Console.WriteLine();

                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Price: {order.GetTotalPrice():C}"); // Formats as currency
                Console.WriteLine();

                orderNumber++;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
