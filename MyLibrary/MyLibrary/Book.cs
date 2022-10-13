using SQLite;

namespace MyLibrary
{
    [Table("books")]
    public class Book
    {
        [PrimaryKey]
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsRead { get; set; }
        public int Score { get; set; }

    }
}