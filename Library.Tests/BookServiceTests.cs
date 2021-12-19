using System;
using Xunit;
using VISMA;
using System.Collections.Generic;

namespace Library.Tests
{
    public class BookServiceTests
    {

        public static readonly object[][] CorrectData =
        {
            new object[] { "testName", "testAuthor", "testCategory", "testLanguage", "testIsbn",  new DateTime(2021,11,27)},
           new object[] { "t", "test", "Cat", "Lang", "testIsbn",  new DateTime(2021,05,14)},
           new object[] { "testName", "testAuthor", "testCategory", "testLanguage", "testIsbn",  new DateTime(2021,08,17)}
        };

        List<Book> books = new List<Book>
        {
            new Book("testName", "testAuthor1", "testCategory", "testLanguage25", new DateTime(2021,08,17) ,"testIsbn"),
            new Book("testName123", "testAuthor", "testCategory", "testLanguage", new DateTime(2021,08,17) ,"testIsbn"),
            new Book("testName34", "testAuthor", "testCategory123", "testLanguage", new DateTime(2021,08,17) ,"testIsbnx1234")
        };

        List<LibraryTransaction> transactions = new List<LibraryTransaction>
        {
            new LibraryTransaction(new Book("testName111111", "testAuthor111111", "testCategory11111", "testLanguage1111", new DateTime(2020,08,17) ,"testIsbn"), "tester1", new DateTime(2021,06,17), new DateTime(2021,08,15)),
            new LibraryTransaction(new Book("testName222222", "testAuthor222222", "testCategory22222", "testLanguage2222", new DateTime(2020,06,17) ,"testIsbn"), "tester2", new DateTime(2021,08,17), new DateTime(2021,09,17))
        };


        [Theory]
        [MemberData(nameof(CorrectData))]
        public void AddNewBook_ShouldAddNewBookToJson_IfCorrect_ReturnsTrue(string name, string author, string category, string language, string isbn, DateTime publDate) 
        {
            BookService bookService = new BookService();
            Book book = new Book(name, author, category, language, publDate, isbn);


            bookService.AddBook(book);
            var allBooks = bookService.GetAllBooks();

            var bookInList = allBooks.Find(r => r.Name == book.Name && r.Author == book.Author
                    && r.Category == book.Category && r.Language == book.Language
                            && r.Isbn == book.Isbn && r.PublicationDate == book.PublicationDate);


            Assert.NotNull(bookInList);

            bookService.DeleteBook(book.Isbn);
        }


        [Theory]
        [InlineData(1, 2, "testAuthor")]
        [InlineData(2, 2, "testCategory")]
        [InlineData(3, 2, "testLanguage")]
        [InlineData(4, 2, "testIsbn")]
        [InlineData(5, 1, "testName")]
        [InlineData(6, 2, "")]
        [InlineData(7, 3, "")]
        public void FilterBy_ShouldShowFilteredBooks_IfCorrect_ReturnsTrue(int choice, int count, string filterOption)
        {
            BookService bookService = new BookService();

            var filtered = bookService.FilterBy(choice, filterOption, books, transactions);

            Assert.Equal(count, filtered.Count);

        }

    }
}
