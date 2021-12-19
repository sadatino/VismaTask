using Newtonsoft.Json;
using System;

namespace VISMA
{
    public class Book
    {
        private string _name, _author, _category, _language, _isbn;
        private DateTime _publicationDate;

        public Book()
        {

        }
        public Book(string name, string author, string category, string language, DateTime publicationDate, string isbn)
        {
            if (name == string.Empty || author == string.Empty || category == string.Empty || language == string.Empty || isbn == string.Empty)
                throw new Exception();

            this._name = name;
            this._author = author;
            this._category = category;
            this._language = language;
            this._publicationDate = publicationDate;
            this._isbn = isbn;

            Name = _name;
            Author = _author;
            Category = _category;
            Language = _language;
            PublicationDate = _publicationDate;
            Isbn = _isbn;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }

        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        public override string ToString()
        {
            return Name + "\t\t" + Author + "\t\t" + Category + "\t\t" + Language + "\t\t" + PublicationDate.ToString("yyyy-MM-dd") + "\t\t" + Isbn;
        }
    }
}
