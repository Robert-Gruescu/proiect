namespace proiectul_1.Models
{
    // Loan.cs - Gestionarea imprumuturilor
    using System;

    public class Loan
    {
        public Client Borrower { get; set; }
        public Book BorrowedBook { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LoanDuration => (EndDate - StartDate).Days;

        // Proprietatea Price pentru pretul imprumutului
        public decimal Price => CalculateLoanPrice();

        // Constructor complet
        public Loan(Client borrower, Book borrowedBook, DateTime startDate, DateTime endDate)
        {
            Borrower = borrower;
            BorrowedBook = borrowedBook;
            StartDate = startDate;
            EndDate = endDate;
        }

        // Constructor cu calcul automat al EndDate
        public Loan(Client borrower, Book borrowedBook)
        {
            Borrower = borrower;
            BorrowedBook = borrowedBook;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(borrowedBook.GetMaxLoanPeriod());
        }

        // Calcularea taxelor de intarziere
        public decimal CalculateLateFees()
        {
            if (DateTime.Now <= EndDate)
                return 0;

            int overdueDays = (DateTime.Now - EndDate).Days;
            decimal dailyFee = BorrowedBook is FictionBook ? 1m : 2m;

            return overdueDays * dailyFee;
        }

        // Calcularea pretului total al imprumutului
        public decimal CalculateLoanPrice()
        {
            return 60m * LoanDuration;
        }

        // Reprezentare detaliata sub forma de text
        public override string ToString()
        {
            return $"Imprumut: {BorrowedBook.Title} de la {StartDate:yyyy-MM-dd} pana la {EndDate:yyyy-MM-dd} \n" +
                   $"Client: {Borrower.Name}, Durata: {LoanDuration} zile, Pret: {Price} RON, Taxa intarziere: {CalculateLateFees()} RON";
        }
    }
}