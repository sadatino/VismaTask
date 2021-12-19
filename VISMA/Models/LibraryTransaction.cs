using Newtonsoft.Json;
using System;

namespace VISMA
{
    public class LibraryTransaction
    {
        private Book _book;
        private string _recipient;
        private DateTime _taken, _deadline;

        public LibraryTransaction()
        {

        }
        public LibraryTransaction(Book book, string recipient, DateTime taken, DateTime deadline)
        {
            this._book = book;
            this._recipient = recipient;
            this._taken = taken;
            this._deadline = deadline;

            GetBook = _book;
            Recipient = _recipient;
            Taken = _taken;
            Deadline = _deadline;
        }

        [JsonProperty("book")]
        public Book GetBook { get; private set; }

        [JsonProperty("recipient")]
        public string Recipient { get; private set; }

        [JsonProperty("taken")]
        public DateTime Taken { get; private set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; private set; }

        public override string ToString()
        {
            return $"Book {GetBook.Name} by {GetBook.Author} is taken by {Recipient} from {Taken.ToString()} until {Deadline.ToString()}";
        }

    }
}
