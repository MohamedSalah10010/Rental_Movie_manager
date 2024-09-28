
// ----------------------------------------------------
// Title: Transaction.cs
// Author: Mohamed Salah
// 
// Created on: 28/9/2024
// ----------------------------------------------------

using System;

namespace Rental_movie_manager
{
    public class Transaction
    {
        public long Id { get; private set; }
        private static long nextId = 1;

        public Transaction()
        {
            Id = nextId++;
        }

        public void PrintReceipt(Customer customer, Rental rental)
        {
            Console.WriteLine("\n--- Receipt ---");
            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine($"Rented Movie: {rental.MovieRented.Name}");
            Console.WriteLine($"Due Date: {rental.DueTime:yyyy-MM-dd}");
            Console.WriteLine($"Initial Cost: ${rental.MovieRented.Cost}");

            double fees = rental.Fees();
            Console.WriteLine($"Final Cost: ${fees}");
            Console.WriteLine("Transaction completed successfully.\n");
        }
    }

    public class Rental
    {
        public DateTime RentalTime { get; private set; }
        public DateTime DueTime { get; private set; }
        public Movie MovieRented { get; private set; }

        public Rental(DateTime rentalTime, DateTime dueTime, Movie movieRented)
        {
            RentalTime = rentalTime;
            DueTime = dueTime;
            MovieRented = movieRented;
        }

        public double Fees()
        {
            double totalFees = MovieRented.Cost;

            // Calculate the number of days rented
            TimeSpan daysRented = DueTime - RentalTime;

            // Use the Days property to get whole days rented
            int wholeDaysRented = daysRented.Days;
            Console.WriteLine($"Days Rented: {wholeDaysRented}");


            // Check if the rental period exceeds 7 days
            if (daysRented.Days > 7)
            {
                // Calculate the number of overdue days beyond the standard 7-day rental
                int extraDays = daysRented.Days - 7;
                // Increase the movie price by 5 cents for each extra day
                totalFees += extraDays * 0.05;
                Console.WriteLine($"Extra fees for {extraDays} days: ${extraDays * 0.05}");
            }
            else
            {
                Console.WriteLine("No extra fees for the rental period.");
            }

            // Check if the current date is after the due date
            if (DateTime.Now > DueTime)
            {
                // Calculate late days
                int lateDays = (DateTime.Now - DueTime).Days;
                // Charge 10 cents for each day late
                totalFees += lateDays * 0.10;
                Console.WriteLine($"Late fees for {lateDays} days: ${lateDays * 0.10}");
            }
            else
            {
                Console.WriteLine("No late fees applied.");
            }

            return totalFees; ;
        }

        public bool ValidateRental(Customer customer)
        {
            if (!MovieRented.IsAvailable())
            {
                Console.WriteLine("Movie is not available for rent.");
                return false;
            }

            if (!MovieRented.ValidateAge(customer.Age))
            {
                Console.WriteLine($"Customer is too young to rent this movie. Minimum age required is {MovieRented.AgeRestriction}.");
                return false;
            }

            Console.WriteLine($"Customer is allowed to rent the movie '{MovieRented.Name}'.");
            return true;
        }
    }
}
