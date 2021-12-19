using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VISMA
{
    public class BookService
    {

        public BookService()
        {

        }

        public void AddBook(Book book)
        {
            var allBooks = GetAllBooks();
            
            if(book != null)
            {
                allBooks.Add(book);
            }

            string json = JsonConvert.SerializeObject(allBooks.ToArray());
            File.WriteAllText("Books.json", json);
        }

        public Book CollectNewBookInfo()
        {
            bool occupiedIsbn = true;
            var allBooks = GetAllBooks();

            Console.WriteLine("Enter book name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter book author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Enter book category: ");
            string category = Console.ReadLine();

            Console.WriteLine("Enter book language: ");
            string language = Console.ReadLine();

            Console.WriteLine("Enter book publication date: (YYYY-MM-DD) ");
            string date = Console.ReadLine();
            DateTime publicationDate = DateTime.Parse(date);


            string isbn = string.Empty;
            while (occupiedIsbn)
            {
                Console.WriteLine("Enter book isbn: ");
                isbn = Console.ReadLine();
                occupiedIsbn = allBooks.Any(b => string.Equals(b.Isbn, isbn));
                if (occupiedIsbn)
                {
                    Console.WriteLine("Isbn taken. Please try again.");
                }
            }

            if(name != string.Empty && author != string.Empty && category != string.Empty && language != string.Empty && isbn != string.Empty)
            {
                return new Book(name, author, category, language, publicationDate, isbn);
            }
            return null;
        }

        public List<Book> GetAllBooks()
        {
            string json = File.ReadAllText("Books.json");
            var bookList = JsonConvert.DeserializeObject<List<Book>>(json);
            return bookList;
        }

        public List<Book> FilterBy(int choice, string filterOption, List<Book> allBooks, List<LibraryTransaction> allTransactions)
        {
            List<Book> filteredBooks = null;
            switch (choice)
            {
                case 1:
                    filteredBooks = allBooks.FindAll(b => b.Author == filterOption);
                    break;
                case 2:
                    filteredBooks = allBooks.FindAll(b => b.Category == filterOption);
                    break;
                case 3:
                    filteredBooks = allBooks.FindAll(b => b.Language == filterOption);
                    break;
                case 4:
                    filteredBooks = allBooks.FindAll(b => b.Isbn == filterOption);
                    break;
                case 5:
                    filteredBooks = allBooks.FindAll(b => b.Name == filterOption);
                    break;
                case 6:     //taken
                    filteredBooks = new List<Book>();
                    foreach (var transact in allTransactions)
                    {
                        filteredBooks.Add(transact.GetBook);
                    }
                    if (!filteredBooks.Any())
                    {
                        Console.WriteLine("There are no books in the library currently...");
                    }
                    break;
                case 7:     //available
                    filteredBooks = new List<Book>();
                    foreach (var book in allBooks)
                    {
                        filteredBooks.Add(book);
                    }

                    if (!filteredBooks.Any())
                    {
                        Console.WriteLine("There are no taken books..");
                    }

                    break;
                default:
                    break;

            }
            return filteredBooks;
        }

        public void DeleteBook(string isbn)
        {
            var allBooks = GetAllBooks();
            var bookToDelete = allBooks.FirstOrDefault(b => string.Equals(b.Isbn, isbn));

            if(bookToDelete != null)
                allBooks.Remove(bookToDelete);

            string json = JsonConvert.SerializeObject(allBooks.ToArray());
            File.WriteAllText("Books.json", json);
        }

    }
}
