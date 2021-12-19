using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VISMA
{
    public class TransactionService
    {
        int MAX_BOOK_COUNT = 3;
        public TransactionService()
        {

        }

        public void TakeBook(string bookName, string author, List<Book> allBooks)
        {

            BookService bookService = new BookService();
            var allTransactions = GetAllTransactions();
            
            Book selectedBook = allBooks.FirstOrDefault(b => b.Author == author && b.Name == bookName);
            if (selectedBook == null)
            {
                Console.WriteLine($"There is no book called {bookName} by {author}.\n\n");
                return;
            }

            LibraryTransaction libraryTransaction = GetUsersInformationAndValidate(selectedBook, allTransactions);
            if (libraryTransaction == null)
                return;


            allTransactions.Add(libraryTransaction);

            bookService.DeleteBook(selectedBook.Isbn);


            string json = JsonConvert.SerializeObject(allTransactions.ToArray());
            File.WriteAllText("Transactions.json", json);

            Console.WriteLine("\n" + libraryTransaction.ToString() + "\n\n");
        }

        public void ReturnBook(string username, string bookName, string author, DateTime date)
        {
            var allTransactions = GetAllTransactions();
            var selectedBookIndexToReturn = allTransactions.FindIndex(b => string.Equals(b.Recipient, username)
                                && string.Equals(b.GetBook.Author, author) && string.Equals(b.GetBook.Name, bookName));

            if (selectedBookIndexToReturn == -1)
            {
                Console.WriteLine("Wrong book name or author name");
                return;
            }
            if(date > allTransactions[selectedBookIndexToReturn].Deadline)
            {
                Console.WriteLine("very very late... shame...");
            }

            RemoveTransaction(selectedBookIndexToReturn);

        }

        public List<LibraryTransaction> GetAllTransactions()
        {
            string json = File.ReadAllText("Transactions.json");
            var transactionList = JsonConvert.DeserializeObject<List<LibraryTransaction>>(json);
            return transactionList;
        }
        public void RemoveTransaction(int index)
        {
            BookService bookService = new BookService();
            var allTransactions = GetAllTransactions();
            var addBackToLibBook = allTransactions[index].GetBook;

            allTransactions.RemoveAt(index);
            bookService.AddBook(addBackToLibBook);


            string json = JsonConvert.SerializeObject(allTransactions.ToArray());
            File.WriteAllText("Transactions.json", json);
        }

        public LibraryTransaction GetUsersInformationAndValidate(Book selectedBook, List<LibraryTransaction> allTransactions)
        {
            Console.WriteLine("Enter your name: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter the date, from when you want to have the book (YYYY-MM-DD)");
            DateTime starting = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter the date, until you want to have the book (YYYY-MM-DD)");
            DateTime deadline = DateTime.Parse(Console.ReadLine());

            TimeSpan span = deadline - starting;
            if (span < TimeSpan.Zero)
            {
                Console.WriteLine("deadline is earlier than starting point\n");
                return null;
            }

            var days = span.TotalDays;

            if (days > 61)
            {
                Console.WriteLine("You can only book a book ( ;) ) for 2 months max.\n\n");
                return null;
            }

            if (allTransactions.Where(x => string.Equals(x.Recipient, username)).Count() > MAX_BOOK_COUNT)
            {
                Console.WriteLine($"You can only have {MAX_BOOK_COUNT} books at a time");
                return null;
            }

            return new LibraryTransaction(selectedBook, username, starting, deadline);
        }

    }
}
