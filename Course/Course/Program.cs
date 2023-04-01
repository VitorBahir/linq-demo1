using Course.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Course
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T item in collection)
            {
                Console.WriteLine(item);
            }
        }

        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>() {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            var r1 = products.Where(x => x.Price < 900.0 && x.Category.Tier == 1);
            Print("Tier 1 AND PRICE < 900.0:", r1);
            Console.WriteLine();

            var r2 = products.Where(x => x.Category.Name == "Tools").Select(x => x.Name);
            Print("NAMES OF PRODUCTS FROM TOOLS:", r2);
            Console.WriteLine();

            var r3 = products.Where(x => x.Name[0] == 'C').Select(x => new { x.Name, x.Price, CategoryName = x.Category.Name });
            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT:", r3);
            Console.WriteLine();

            var r4 = products.Where(x => x.Category.Tier == 1).OrderBy(x => x.Price).ThenBy(x => x.Name);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME", r4);
            Console.WriteLine();

            var r5 = r4.Skip(2).Take(4);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4", r5);
            Console.WriteLine();

            var r6 = products.First();
            Console.WriteLine("First test1: " + r6);
            var r7 = products.Where(x => x.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("First or default test2: " + r7);
            Console.WriteLine();

            var r8 = products.Where(x => x.Id == 3).SingleOrDefault();
            Console.WriteLine("Single or default test1: " + r8);
            var r9 = products.Where(x => x.Id == 30).SingleOrDefault();
            Console.WriteLine("Single or default test2: " + r9);
            Console.WriteLine();

            var r10 = products.Max(x => x.Price);
            Console.WriteLine("Max price: " + r10);
            var r11 = products.Min(x => x.Price);
            Console.WriteLine("Min price: " + r11);
            Console.WriteLine();

            var r12 = products.Where(x => x.Category.Id == 1).Sum(x => x.Price);
            Console.WriteLine("Category 1 Sum prices: " + r12);
            var r13 = products.Where(x => x.Category.Id == 1).Average(x => x.Price);
            Console.WriteLine("Category 1 Average prices: " + r13);
            var r14 = products.Where(x => x.Category.Id == 5).Select(x => x.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Category 5 Average prices: " + r14);
            Console.WriteLine();

            var r15 = products.Where( x => x.Category.Id == 1).Select(x => x.Price).Aggregate((z, y) => z+y);
            Console.WriteLine("Category 1 aggregate sum: " + r15);
            var r16 = products.Where(x => x.Category.Id == 5).Select(x => x.Price).Aggregate(0.0, (z, y) => z + y);
            Console.WriteLine("Category 5 aggregate sum: " + r16);
            Console.WriteLine();

            var r17 = products.GroupBy(x => x.Category);
            foreach(IGrouping<Category, Product> group in r17)
            {
                Console.WriteLine("Category: " + group.Key.Name);
                foreach(Product p in group)
                {
                    Console.WriteLine( p );
                }
                Console.WriteLine();
            }
        }
    }
}