using System;
using System.Collections.Generic;

namespace VISMA
{
    class Program
    {
        static void Main(string[] args)
        {
            DoUsersScenario();

        }
        static void DoUsersScenario()
        {
            Menu menu = new Menu();
            BookService service = new BookService();
            TransactionService transactionService = new TransactionService();
            int option = -1;
            string name = string.Empty;
            string author = string.Empty;
            string username = string.Empty;
            while (option != 0)
            {
                menu.PrintMenu();
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        var newBook = service.CollectNewBookInfo();
                        service.AddBook(newBook);
                        break;
                    case 2:
                        Console.WriteLine("Enter book name you want to take: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter book author: ");
                        author = Console.ReadLine();
                        transactionService.TakeBook(name, author, service.GetAllBooks());

                        break;
                    case 3:
                        Console.WriteLine("Enter your name: ");
                        username = Console.ReadLine();

                        Console.WriteLine("Enter name of book to return: ");
                        name = Console.ReadLine();

                        Console.WriteLine("Enter author of book:");
                        author = Console.ReadLine();

                        Console.WriteLine("Enter date of return: ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        transactionService.ReturnBook(username, name, author, date);
                        break;
                    case 4:
                        var allBooks = service.GetAllBooks();
                        var allTransactions = transactionService.GetAllTransactions();

                        menu.PrintFilterOptions();

                        var books = FilterBooks(service, allBooks, allTransactions);

                        foreach (var book in books)
                        {
                            Console.WriteLine(book.ToString());
                        }
                        break;
                    case 5:
                        Console.WriteLine("Enter book isbn to delete it");
                        string isbn = Console.ReadLine();
                        service.DeleteBook(isbn);
                        break;
                    default:
                        Console.WriteLine("Dont know about that one chief");
                        break;
                }

            }
        }

        private static List<Book> FilterBooks(BookService service, List<Book> allBooks, List<LibraryTransaction> allTransactions)
        {

            int choice = int.Parse(Console.ReadLine());
            List<Book> filteredBooks;
            var filterOption = string.Empty;
            if (choice < 6)
            {
                filterOption = Console.ReadLine();
            }
            
            filteredBooks = service.FilterBy(choice, filterOption, allBooks, allTransactions);
            return filteredBooks;
        }
    }
}
