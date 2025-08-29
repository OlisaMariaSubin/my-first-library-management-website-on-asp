namespace librarysite.Models
{
    public class IssuedBooks
    {
        public int BookId { get; set; }
        public required string Bookname { get; set; }
        public DateTime DueDate { get; set; }

    }
}
