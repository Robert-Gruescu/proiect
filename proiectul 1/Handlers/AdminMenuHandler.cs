using proiectul_1.Models;
// File: AdminMenuHandler.cs
using System;

namespace proiectul_1.Handlers
{
    public static class AdminMenuHandler
    {
        public static void AdminMenu(Library library, Administrator admin)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Meniu Administrator ===");
                Console.WriteLine("1. Adaugare carte");
                Console.WriteLine("2. Stergere carte");
                Console.WriteLine("3. Stergere client");
                Console.WriteLine("4. Scutire client de datorii");
                Console.WriteLine("5. Vizualizare istoric inchirieri ale bibliotecii");
                Console.WriteLine("6. Vizualizare istoric inchirieri ale unui client");
                Console.WriteLine("7. Vizualizare castiguri totale");
                Console.WriteLine("8. Vizualizare castiguri pentru o perioada");
                Console.WriteLine("0. Iesire");
                Console.Write("Alegeti o optiune: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddBook(library, admin);
                        break;
                    case "2":
                        RemoveBook(library, admin);
                        break;
                    case "3":
                        RemoveClient(library, admin);
                        break;
                    case "4":
                        ClearClientDebts(library, admin);
                        break;
                    case "5":
                        admin.ViewLibraryLoanHistory(library);
                        break;
                    case "6":
                        ViewClientLoanHistory(library, admin);
                        break;
                    case "7":
                        admin.ViewTotalEarnings(library);
                        break;
                    case "8":
                        ViewEarningsForPeriod(library, admin);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Optiune invalida. Apasati Enter pentru a continua...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void AddBook(Library library, Administrator admin)
        {
            Console.Write("Introduceti titlul cartii: ");
            string title = Console.ReadLine();
            Console.Write("Introduceti autorul cartii: ");
            string author = Console.ReadLine();
            Console.Write("Introduceti genul cartii: ");
            string genre = Console.ReadLine();
            Console.Write("Introduceti anul publicarii: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Introduceti ISBN-ul: ");
            string isbn = Console.ReadLine();
            Console.Write("Este cartea fictiune? (y/n): ");
            bool isFiction = Console.ReadLine().ToLower() == "y";

            Book book = isFiction ? new FictionBook(title, author, genre, year, isbn) : new NonFictionBook(title, author, genre, year, isbn);
            admin.AddBook(library, book);
        }

        private static void RemoveBook(Library library, Administrator admin)
        {
            Console.Write("Introduceti ISBN-ul cartii de sters: ");
            string isbnToRemove = Console.ReadLine();
            admin.RemoveBook(library, isbnToRemove);
        }

        private static void RemoveClient(Library library, Administrator admin)
        {
            Console.Write("Introduceti username-ul clientului de sters: ");
            string usernameToRemove = Console.ReadLine();
            admin.RemoveClient(library, usernameToRemove);
        }

        private static void ClearClientDebts(Library library, Administrator admin)
        {
            Console.Write("Introduceti username-ul clientului pentru a-l scuti de datorii: ");
            string usernameToClear = Console.ReadLine();
            admin.ClearClientDebts(library, usernameToClear);
        }

        private static void ViewClientLoanHistory(Library library, Administrator admin)
        {
            Console.Write("Introduceti username-ul clientului pentru a vedea istoricul: ");
            string usernameToView = Console.ReadLine();
            admin.ViewClientLoanHistory(library, usernameToView);
        }

        private static void ViewEarningsForPeriod(Library library, Administrator admin)
        {
            Console.Write("Introduceti data de inceput (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Introduceti data de sfarsit (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            admin.ViewEarningsForPeriod(library, startDate, endDate);
        }
    }
}