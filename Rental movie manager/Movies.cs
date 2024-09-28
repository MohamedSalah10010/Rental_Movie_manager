
// ----------------------------------------------------
// Title: Movies.cs
// Author: Mohamed Salah
// 
// Created on: 28/9/2024
// ----------------------------------------------------


using System;
using System.Collections.Generic;

namespace Rental_movie_manager
{
    public class Movie
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public int AgeRestriction { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }

        public Movie(string name, string genre, int ageRestriction, int quantity, double cost)
        {
            Name = name;
            Genre = genre;
            AgeRestriction = ageRestriction;
            Quantity = quantity;
            Cost = cost;
        }

        public static void ListMovies(List<Movie> movies)
        {
            Console.WriteLine("--- Movies Available ---");
            foreach (var movie in movies)
            {
                Console.WriteLine($"Name: {movie.Name}, Genre: {movie.Genre}, Age Restriction: {movie.AgeRestriction}, Quantity: {movie.Quantity}, Cost: ${movie.Cost}");
            }
        }

        public bool IsAvailable()
        {
            return Quantity > 0;
        }

        public bool ValidateAge(int customerAge)
        {
            return customerAge >= AgeRestriction;
        }
    }

    public class MovieManager
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

        public bool RemoveMovie(string movieName)
        {
            Movie movie = Movies.Find(m => m.Name == movieName);
            if (movie != null)
            {
                Movies.Remove(movie);
                return true;
            }
            return false;
        }
    }
}
