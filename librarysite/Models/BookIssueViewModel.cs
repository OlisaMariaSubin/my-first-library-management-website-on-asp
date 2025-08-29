using Microsoft.AspNetCore.Mvc.Rendering;

namespace librarysite.Models
{
    public class BookIssueViewModel
    {
        public int Memberid { get; set; }
        public string? Membername { get; set; }
        public int BookId { get; set; }
        public string? Bookname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? AuthorName { get; set; }
        public string? Genre { get; set; }
        public string? Publishername { get; set; }
        public DateTime PublishDate { get; set; }
        public int ActualStock { get;set; }
        public int CurrentStock { get; set; }
        public int IssuedBooks { get; set; }
        public string? Description { get; set; }

        public required string Language { get; set; }
        public List<SelectListItem> LanguageList { get; set; }
        public int Edition { get; set; }
        public int BookCost { get; set; }
        public int Pages { get; set; }


    }
}
