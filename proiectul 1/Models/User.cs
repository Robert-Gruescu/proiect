using System;
using System.Collections.Generic;

namespace proiectul_1.Models
{
    // File: User.cs
    public abstract class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        protected User(string name, string username, string password)
        {
            Name = name;
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"Nume: {Name}, Username: {Username}";
        }
    }

    public class Administrator : User
    {
        public Administrator(string name, string username, string password)
            : base(name, username, password)
        {
        }

        public void AddBook(Library library, Book book)
        {
            library.AddBook(book);
        }

        public void RemoveBook(Library library, string isbn)
        {
            library.RemoveBook(isbn);
        }

        public void RemoveClient(Library library, string username)
        {
            var client = library.Users.Find(u => u.Username == username && u is Client);
            if (client != null)
            {
                library.Users.Remove(client);
                Console.WriteLine($"Clientul '{client.Name}' a fost sters.");
            }
            else
            {
                Console.WriteLine("Clientul nu a fost gasit.");
            }
        }

        public void ClearClientDebts(Library library, string username)
        {
            var client = library.Users.Find(u => u.Username == username && u is Client) as Client;
            if (client != null)
            {
                client.Debts = 0;
                Console.WriteLine($"Datoriile clientului '{client.Name}' au fost sterse.");
            }
            else
            {
                Console.WriteLine("Clientul nu a fost gasit.");
            }
        }

        public void ViewLibraryLoanHistory(Library library)
        {
            Console.WriteLine(" Istoric Imprumuturi ");
            foreach (var book in library.Books)
            {
                if (book.LoanHistory.Count > 0)
                {
                    Console.WriteLine($"Cartea: {book.Title}");
                    foreach (var loan in book.LoanHistory)
                    {
                        Console.WriteLine(loan);
                    }
                }
            }
        }

        public void ViewClientLoanHistory(Library library, string username)
        {
            var client = library.Users.Find(u => u.Username == username && u is Client) as Client;
            if (client != null)
            {
                Console.WriteLine($" Istoric Imprumuturi pentru {client.Name} ");
                foreach (var loan in client.LoanHistory)
                {
                    Console.WriteLine(loan);
                }
            }
            else
            {
                Console.WriteLine("Clientul nu a fost gasit.");
            }
        }

        public void ViewTotalEarnings(Library library)
        {
            decimal totalEarnings = 0;
            foreach (var book in library.Books)
            {
                totalEarnings += book.TotalEarnings;
            }
            Console.WriteLine($"Castiguri totale: {totalEarnings} RON");
        }

        public void ViewEarningsForPeriod(Library library, DateTime startDate, DateTime endDate)
        {
            decimal periodEarnings = 0;
            foreach (var book in library.Books)
            {
                foreach (var loan in book.LoanHistory)
                {
                    if (loan.StartDate >= startDate && loan.EndDate <= endDate)
                    {
                        periodEarnings += loan.Price;
                    }
                }
            }
            Console.WriteLine($"Castiguri pentru perioada {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd}: {periodEarnings} RON");
        }
    }

    public class Client : User
    {
        public string CNP { get; set; }
        public List<Loan> LoanHistory { get; set; }
        public decimal Debts { get; set; }

        public Client(string name, string username, string password)
            : base(name, username, password)
        {
            LoanHistory = new List<Loan>();
            Debts = 0;
        }

        public void ViewAvailableBooks(Library library)
        {
            Console.WriteLine(" Carti Disponibile ");
            foreach (var book in library.Books)
            {
                if (book.IsAvailable)
                {
                    Console.WriteLine(book);
                }
            }
        }

        public void BorrowBook(Library library, string isbn)
        {
            var book = library.Books.Find(b => b.ISBN == isbn);
            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false;
                var loan = new Loan(this, book);
                LoanHistory.Add(loan);
                book.LoanHistory.Add(loan);
                Console.WriteLine($"Cartea '{book.Title}' a fost imprumutata.");
            }
            else
            {
                Console.WriteLine("Cartea nu este disponibila sau nu exista.");
            }
        }

        public void ReturnBook(Library library, string isbn)
        {
            var book = library.Books.Find(b => b.ISBN == isbn);
            if (book != null && !book.IsAvailable)
            {
                book.IsAvailable = true;
                Console.WriteLine($"Cartea '{book.Title}' a fost returnata.");
            }
            else
            {
                Console.WriteLine("Cartea nu este valida pentru returnare.");
            }
        }

        public void ChangeName(string newName)
        {
            Name = newName;
            Console.WriteLine("Numele a fost actualizat cu succes.");
        }

        internal void ReturnBook(Library library, Book book)
        {
            throw new NotImplementedException();
        }
    }
}