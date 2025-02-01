using System;
using System.Collections.Generic;

namespace proiectul_1.Models
{
    // Book.cs - Clase pentru cărți (FictionBook, NonFictionBook)
    public abstract class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public List<Loan> LoanHistory { get; set; } // Adăugăm LoanHistory pentru a urmări împrumuturile

        public Book(string title, string author, string genre, int year, string isbn)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            ISBN = isbn;
            IsAvailable = true;
            LoanHistory = new List<Loan>(); // Inițializăm LoanHistory
        }

        public abstract int GetMaxLoanPeriod();

        // Proprietate pentru câștigurile totale din împrumuturi
        public decimal TotalEarnings
        {
            get
            {
                decimal earnings = 0;
                foreach (var loan in LoanHistory)
                {
                    earnings += loan.CalculateLoanPrice();
                }
                return earnings;
            }
        }

        public override string ToString()
        {
            return $"Titlu: {Title}, Autor: {Author}, Gen: {Genre}, An: {Year}, ISBN: {ISBN}, Disponibil: {IsAvailable}";
        }
    }

    public class FictionBook : Book
    {
        public FictionBook(string title, string author, string genre, int year, string isbn)
            : base(title, author, genre, year, isbn)
        {
        }

        public override int GetMaxLoanPeriod()
        {
            return 14; // Perioadă maximă de împrumut: 14 zile
        }
    }

    public class NonFictionBook : Book
    {
        public NonFictionBook(string title, string author, string genre, int year, string isbn)
            : base(title, author, genre, year, isbn)
        {
        }

        public override int GetMaxLoanPeriod()
        {
            return 30; // Perioadă maximă de împrumut: 30 zile
        }
    }
}
