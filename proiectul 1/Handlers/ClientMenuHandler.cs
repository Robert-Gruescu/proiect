using System;
using proiectul_1.Models;

namespace proiectul_1.Handlers
{
    // File: ClientMenuHandler.cs
    public static class ClientMenuHandler
    {
        public static void ClientMenu(Library library, Client client)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Meniu Client ===");
                Console.WriteLine("1. Vizualizare carti disponibile pentru inchiriat");
                Console.WriteLine("2. Inchiriere carte selectata");
                Console.WriteLine("3. Restituire carte");
                Console.WriteLine("4. Schimbare nume");
                Console.WriteLine("0. Iesire");
                Console.Write("Alegeti o optiune: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewAvailableBooks(library);
                        break;
                    case "2":
                        BorrowBook(library, client); // Modificat din RentBook in BorrowBook
                        break;
                    case "3":
                        ReturnBook(library, client);
                        break;
                    case "4":
                        ChangeName(client);
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

        private static void ViewAvailableBooks(Library library)
        {
            Console.WriteLine("=== Carti Disponibile ===");
            foreach (var book in library.Books)
            {
                if (book.IsAvailable)
                {
                    Console.WriteLine(book);
                }
            }
            Console.WriteLine("Apasati Enter pentru a continua...");
            Console.ReadLine();
        }

        private static void BorrowBook(Library library, Client client) // Modificat numele metodei
        {
            Console.Write("Introduceti ISBN-ul cartii dorite: ");
            string isbn = Console.ReadLine();
            var book = library.Books.Find(b => b.ISBN == isbn);

            if (book != null && book.IsAvailable)
            {
                client.BorrowBook(library, isbn); // Foloseste metoda BorrowBook din clasa Client
                Console.WriteLine("Cartea a fost inchiriata cu succes. Apasati Enter pentru a continua...");
            }
            else
            {
                Console.WriteLine("Cartea nu este disponibila sau nu exista. Apasati Enter pentru a continua...");
            }
            Console.ReadLine();
        }

        private static void ReturnBook(Library library, Client client)
        {
            Console.Write("Introduceti ISBN-ul cartii de returnat: ");
            string isbn = Console.ReadLine();
            var book = library.Books.Find(b => b.ISBN == isbn);

            if (book != null && !book.IsAvailable)
            {
                client.ReturnBook(library, isbn);
                Console.WriteLine("Cartea a fost returnata cu succes. Apasati Enter pentru a continua...");
            }
            else
            {
                Console.WriteLine("Cartea nu se afla in posesia dumneavoastra. Apasati Enter pentru a continua...");
            }
            Console.ReadLine();
        }

        private static void ChangeName(Client client)
        {
            Console.Write("Introduceti noul nume: ");
            string newName = Console.ReadLine();
            client.Name = newName;
            Console.WriteLine("Numele a fost actualizat cu succes. Apasati Enter pentru a continua...");
            Console.ReadLine();
        }
    }
}