
// ----------------------------------------------------
// Title: program.cs
// Author: Mohamed Salah
// 
// Created on: 28/9/2024
// ----------------------------------------------------

using System;
using System.Collections.Generic;

namespace Rental_movie_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize movie manager and employee list
            MovieManager movieManager = new MovieManager();
         

            // Sample movies
            movieManager.AddMovie(new Movie("Inception", "Sci-Fi", 13, 5, 10.99));
            movieManager.AddMovie(new Movie("The Matrix", "Action", 16, 3, 8.99));
            movieManager.AddMovie(new Movie("Finding Nemo", "Animation", 0, 10, 5.99));

            Console.WriteLine("Welcome to the Rental Movie Manager");
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Employee Login");
                Console.WriteLine("2. Employee Registration");
                Console.WriteLine("3. Exit");

                Console.Write("Select an option: ");
                int mainChoice = int.Parse(Console.ReadLine());

                switch (mainChoice)
                {
                    case 1:
                        // Employee Login
                        Employee loggedInEmployee = Employee.EmployeeLogin();
                        if (loggedInEmployee != null)
                        {
                            Employee.EmployeeActions(loggedInEmployee, movieManager);
                        }
                        break;
                    case 2:
                        // Employee Registration
                        Employee newEmployee = Employee.RegisterEmployee();

                        if (newEmployee != null)
                        {
                            Console.WriteLine("Employee registered successfully.");
                        }
                        break;
                    case 3:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }

            Console.WriteLine("Thank you for using the Rental Movie Manager!");
        }
    }
}
