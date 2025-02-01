// File: Program.cs
using proiectul_1.Models;
using System;
using proiectul_1.Handlers.LibraryManagementSystem;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = Library.LoadFromFile("library.json") ?? new Library("Biblioteca Centrala", "Strada Exemplu, Nr. 1");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistem de Gestionare a Bibliotecii ");
                Console.WriteLine("1. Autentificare / Inregistrare");
                Console.WriteLine("0. Iesire");
                Console.Write("Alegeti o optiune: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AuthenticationHandler.HandleAuthentication(library);
                        break;
                    case "0":
                        library.SaveToFile("library.json");
                        return;
                    default:
                        Console.WriteLine("Optiune invalida. Apasati Enter pentru a continua...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}