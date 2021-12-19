using System;

namespace VISMA
{
    class Menu
    {

        public void PrintMenu()
        {
            Console.WriteLine("\n\n1. Add a new book \n" +
                              "2. Take a book from the library\n" +
                              "3. Return a book to the library\n" +
                              "4. List all available books\n" +
                              "5. Delete a book\n\n");
        }

        public void PrintFilterOptions()
        {
            Console.WriteLine("\n\n1. Filter books by author\n" +
                              "2. Filter by category\n" +
                              "3. Filter by language\n" +
                              "4. Filter by ISBN \n" +
                              "5. Filter by name\n" +
                              "6. Show taken books\n" +
                              "7. Show available books\n\n");
        }

    }
}
