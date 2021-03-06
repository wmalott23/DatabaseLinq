using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var userNum = _context.Users.ToList().Count;
            Console.WriteLine(userNum); 

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products.Where(p => p.Price > 150);
            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Name} {product.Price}");
            }
        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products.Where(p => p.Name.Contains("s"));
            foreach (var item in products)
            {
                Console.WriteLine(item.Name);
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            DateTime before2016 = DateTime.Parse("2016-01-01");
            var users = _context.Users.Where(u => u.RegistrationDate < before2016);
            foreach (User user in users)
            {
                Console.WriteLine($"{user.Email} {user.RegistrationDate}");
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            DateTime after2016 = DateTime.Parse("2016-01-01");
            DateTime before2018 = DateTime.Parse("2018-01-01");
            var users = _context.Users.Where(u => u.RegistrationDate > after2016 && u.RegistrationDate < before2018);
            foreach (User user in users)
            {
                Console.WriteLine($"{user.Email} {user.RegistrationDate}");
            }
        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var products = _context.ShoppingCarts.Include(p => p.Product).Include(p => p.User).Where(p => p.User.Email == "afton@gmail.com");
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Product.Name} {item.Product.Price} {item.Quantity}");
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var products = _context.ShoppingCarts.Include(p => p.Product).Include(p => p.User).Where(p => p.User.Email == "oda@gmail.com").Select(p => p.Product.Price).Sum();
            Console.WriteLine(products);
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var employees = _context.UserRoles.Include(u => u.User).Include(r => r.Role).Where(e => e.RoleId == 2).Select(u => u.UserId);
            var products = _context.ShoppingCarts.Include(u => u.User).Include(p => p.Product).Where(u => employees.Contains(u.UserId));
            foreach (var item in products)
            {
                Console.WriteLine($"{item.User.Email} {item.Product.Name} {item.Product.Price} {item.Quantity}");
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "dCC Course",
                Description = "12-week course designed to make a web developer",
                Price = 15000
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var userId = _context.Users.Where(u => u.Id == 6).Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Id == 8).Select(p => p.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Id == 8).SingleOrDefault();
            product.Price = 15001;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Please enter your email");
            var email = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            var pass = Console.ReadLine();
            bool validEmail = _context.Users.Where(u => u.Email == email).Any();
            bool validPass = _context.Users.Where(u => u.Password== pass).Any();
            if (validEmail && validPass)
            {
                Console.WriteLine("Signed In!");
            }
            else Console.WriteLine("Invalid Email or Password");
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.
            var users = _context.Users.ToList();
            decimal total = 0;
            foreach (var user in users)
            {
                var userTotal = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Id == user.Id).Select(sc => sc.Product.Price * sc.Quantity).Sum();
                Console.WriteLine($"{user.Email} Total: ${userTotal}");
                total += userTotal.Value;
            }
            Console.WriteLine($"Combined Total: ${total}");
        }


        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials
            bool signedIn = false;
            string email;
            string pass;
            do
            {
                Console.WriteLine("Please enter your email");
                email = Console.ReadLine();
                Console.WriteLine("Please enter your password");
                pass = Console.ReadLine();
                bool validEmail = _context.Users.Where(u => u.Email == email).Any();
                bool validPass = _context.Users.Where(u => u.Password == pass).Any();
                if (validEmail && validPass)
                {
                    signedIn = true;
                    Console.WriteLine("Signed In!");
                }
                else Console.WriteLine("Invalid Email or Password");
            }
            while (!signedIn);
            int userId = _context.Users.Where(u => u.Email == email).Select(u => u.Id).SingleOrDefault();
            Console.WriteLine("Please enter 1 for the products page");
            Console.WriteLine("Please enter 2 for the shopping cart page");
            int decision = Convert.ToInt32(Console.ReadLine());
            if (decision > 2 || decision < 1)
            {
                Console.WriteLine("Invalid input, please enter 1 or 2");
            }
            else
            {
                var shoppingCartIds = _context.ShoppingCarts.Include(p => p.Product).Include(p => p.User).Where(p => p.UserId == userId).Select(p => p.ProductId);
                Console.WriteLine(shoppingCartIds);
                if (decision == 1)
                {
                    var products = _context.Products.ToList();
                    var productIds = _context.Products.Select(u => u.Id).ToList();
                    foreach (var item in products)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
                    }

                    int choice;
                    do
                    {
                        Console.WriteLine("Please enter the ID of the item you would like to add to the shopping cart");
                        Console.WriteLine("Please enter 0 if you would like to return to the main menu");
                        choice = Convert.ToInt16(Console.ReadLine());
                        if (productIds.Contains(choice))
                        {
                            if (shoppingCartIds.Contains(choice))
                            {
                                var product = _context.ShoppingCarts.Include(p => p.Product).Include(u => u.User).Where(u => u.UserId == userId && u.ProductId == choice).FirstOrDefault();
                                product.Quantity++;
                                _context.ShoppingCarts.Update(product);
                                _context.SaveChanges();
                            }
                            else
                            {
                                ShoppingCart newShoppingCart = new ShoppingCart()
                                {
                                    UserId = userId,
                                    ProductId = choice,
                                    Quantity = 1
                                };
                                _context.ShoppingCarts.Add(newShoppingCart);
                                _context.SaveChanges();
                            }
                        }
                    }
                    while (!productIds.Contains(choice) && choice != 0);
                }
                else if (decision == 2)
                {
                    var shoppingCart = _context.ShoppingCarts.Include(p => p.Product).Include(u => u.User).Where(sc => sc.UserId == userId).ToList();
                    foreach (var product in shoppingCart)
                    {
                        Console.WriteLine($"{product.Product.Id} {product.Product.Name} {product.Product.Price} {product.Quantity}");
                    }
                    Console.WriteLine("Please enter the id of the product you wish to remove");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    var itemChoice = shoppingCart.Where(u => u.ProductId == choice).FirstOrDefault();
                    _context.ShoppingCarts.Remove(itemChoice);
                    _context.SaveChanges();
                }




            }
        }
    }
}
