using BestBuyBestPractices;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace BestBuyBestPractices
{
    class Program
    {


        static IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        static string connString = config.GetConnectionString("DefaultConnection");

        static IDbConnection conn = new MySqlConnection(connString);

        static void Main(string[] args)
        {
            {
                ListProducts();

                DeleteProduct();

                UpdateProductName();

               
                
            }

            
            static void DeleteProduct()
            {
                
                var prodRepo = new DapperProductRepository(conn);

                
                Console.WriteLine($"Please enter the productID of the product you would like to delete:");
                var productID = Convert.ToInt32(Console.ReadLine());

                
                prodRepo.DeleteProduct(productID);
            }

            static void UpdateProductName()
            {
                var prodRepo = new DapperProductRepository(conn);

                Console.WriteLine($"What is the productID of the product you would like to update?");
                var productID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"What is the new name you would like for the product with an id of {productID}?");
                var updatedName = Console.ReadLine();

                prodRepo.UpdateProductName(productID, updatedName);
            }

            static void CreateAndListProducts()
            {
                
                var prodRepo = new DapperProductRepository(conn);

                Console.WriteLine($"What is the new product name?");
                var prodName = Console.ReadLine();

                Console.WriteLine($"What is the new product's price?");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"What is the new product's category id?");
                var categoryID = Convert.ToInt32(Console.ReadLine());

                prodRepo.CreateProduct(prodName, price, categoryID);


                
                var products = prodRepo.GetAllProducts();

                
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductID} {product.Name}");
                }
            }

            static void ListProducts()
            {
                var prodRepo = new DapperProductRepository(conn);
                var products = prodRepo.GetAllProducts();

                
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductID} {product.Name}");
                }
            }





            var departmentRepo = new DapperDepartmentRepository(conn);

            departmentRepo.InsertDepartment("The New Stuff");

            var departments = departmentRepo.GetAllDepartments();

            foreach (var department in departments)
            {
                Console.WriteLine(department.DepartmentID);
                Console.WriteLine(department.Name);
                Console.WriteLine();
                Console.WriteLine();
            }


            
            
        }
    }
} 

