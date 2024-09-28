
// ----------------------------------------------------
// Title: Person.cs
// Author: Mohamed Salah
// 
// Created on: 28/9/2024
// ----------------------------------------------------


using System;
using System.Collections.Generic;

namespace Rental_movie_manager
{
    public class Person
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public Person(string name, string address, string phone, int age, string email)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Age = age;
            Email = email;
        }

        public bool UpdateInfo()
        {
            // Placeholder for updating info logic
            return true;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Email: {Email}, Phone: {Phone}");
        }
    }

    public class Employee : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public double Salary { get; set; }
        private List<Customer> customers = new List<Customer>();
        private static List<Employee> employees = new List<Employee>();


        public Employee(string name, string address, string phone, int age, string email, string username, string password, double salary)
            : base(name, address, phone, age, email)
        {
            Username = username;
            Password = password;
            Salary = salary;
        }

        public bool Login(string username, string password)
        {
            return Username == username && Password == password;
        }
        public static Employee EmployeeLogin()
        {
            Console.Write("Enter Employee Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            foreach (var employee in employees)
            {
                if (employee.Login(username, password))
                {
                    Console.WriteLine("Login successful.");
                    return employee;
                }
            }

            Console.WriteLine("Invalid username or password. Try again.");
            return null;
        }

        public static Employee RegisterEmployee()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter Salary: ");
            double salary = double.Parse(Console.ReadLine());

            Employee newEmployee = new Employee(name, address, phone, age, email, username, password, salary);
            employees.Add(newEmployee);
            return newEmployee;
        }

        public static void EmployeeActions(Employee employee, MovieManager movieManager)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Employee Actions ---");
                Console.WriteLine("1. List all movies");
                Console.WriteLine("2. Add movie");
                Console.WriteLine("3. Remove movie");
                Console.WriteLine("4. Update movie quantity");
                Console.WriteLine("5. Show movie cost");
                Console.WriteLine("6. Rent movie to customer");
                Console.WriteLine("7. Register customer");
                Console.WriteLine("8. Show customer profile");
                Console.WriteLine("9. Logout");

                Console.Write("Select an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        employee.ListAllMovies(movieManager.Movies);
                        break;
                    case 2:
                        employee.AddMovie(movieManager);
                        break;
                    case 3:
                        employee.RemoveMovie(movieManager);
                        break;
                    case 4:
                        employee.UpdateMovieQuantity(movieManager);
                        break;
                    case 5:
                        employee.ShowMovieCost(movieManager);
                        break;
                    case 6:
                        employee.RentMovie(movieManager);
                        break;
                    case 7:
                        employee.RegisterCustomer();
                        break;
                    case 8:
                        employee.ShowCustomerProfile();
                        break;
                    case 9:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    
    public void ListAllMovies(List<Movie> movies)
        {
            Movie.ListMovies(movies);
        }

        public void AddMovie(MovieManager movieManager)
        {
            Console.Write("Enter Movie Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Genre: ");
            string genre = Console.ReadLine();
            Console.Write("Enter Age Restriction: ");
            int ageRestriction = int.Parse(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Enter Cost: ");
            double cost = double.Parse(Console.ReadLine());

            movieManager.AddMovie(new Movie(name, genre, ageRestriction, quantity, cost));
            Console.WriteLine("Movie added successfully.");
        }

        public void RemoveMovie(MovieManager movieManager)
        {
            Console.Write("Enter Movie Name to Remove: ");
            string movieName = Console.ReadLine();
            if (movieManager.RemoveMovie(movieName))
            {
                Console.WriteLine($"Movie '{movieName}' removed successfully.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        public void UpdateMovieQuantity(MovieManager movieManager)
        {
            Console.Write("Enter Movie Name: ");
            string movieName = Console.ReadLine();
            Movie movie = movieManager.Movies.Find(m => m.Name == movieName);

            if (movie != null)
            {
                Console.Write("Enter New Quantity: ");
                int newQuantity = int.Parse(Console.ReadLine());
                movie.Quantity = newQuantity;
                Console.WriteLine($"Quantity updated for {movieName}.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        public void ShowMovieCost(MovieManager movieManager)
        {
            Console.Write("Enter Movie Name: ");
            string movieName = Console.ReadLine();
            Movie movie = movieManager.Movies.Find(m => m.Name == movieName);

            if (movie != null)
            {
                Console.WriteLine($"Cost of {movieName}: ${movie.Cost}");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        public void RentMovie(MovieManager movieManager)
        {
            Console.Write("Enter Movie Name to Rent: ");
            string movieName = Console.ReadLine();
            Movie movie = movieManager.Movies.Find(m => m.Name == movieName);

            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            DateTime rentalTime = DateTime.Now;
            Console.Write("Enter Due Date (format: yyyy-MM-dd): ");
            DateTime dueTime = DateTime.Parse(Console.ReadLine());

            Rental rental = new Rental(rentalTime, dueTime, movie);

            Console.Write("Enter Customer Username: ");
            string username = Console.ReadLine();
            Customer customer = customers.Find(c => c.Username == username);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            if (rental.ValidateRental(customer))
            {
                movie.Quantity--;
                customer.RentalHistory.Add(rental);

                Transaction transaction = new Transaction();
                transaction.PrintReceipt(customer, rental);
            }
        }

        public void RegisterCustomer()
        {
            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Customer newCustomer = new Customer(name, address, phone, age, email, username);
            customers.Add(newCustomer);
            Console.WriteLine("Customer registered successfully.");
        }

        public void ShowCustomerProfile()
        {
            Console.Write("Enter Customer Username: ");
            string username = Console.ReadLine();
            Customer customer = customers.Find(c => c.Username == username);

            if (customer != null)
            {
                Console.WriteLine("\n--- Customer Profile ---");
                customer.DisplayInfo();
                customer.ShowRentalHistory();
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }

    public class Customer : Person
    {
        public string Username { get; set; }
        public List<Rental> RentalHistory { get; private set; } = new List<Rental>();

        public Customer(string name, string address, string phone, int age, string email, string username)
            : base(name, address, phone, age, email)
        {
            Username = username;
        }

        public void ShowRentalHistory()
        {
            Console.WriteLine("--- Rental History ---");
            foreach (var rental in RentalHistory)
            {
                Console.WriteLine($"Rented Movie: {rental.MovieRented.Name}, Due Date: {rental.DueTime:dd-MM-y}");
            }
        }
    }
}
