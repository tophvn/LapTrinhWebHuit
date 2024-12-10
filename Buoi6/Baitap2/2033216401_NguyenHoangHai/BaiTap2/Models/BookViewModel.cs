namespace BaiTap2.Models
{
    public class BookViewModel
    {
        public List<BookAdmin> Books { get; set; }
        public List<Category> Categories { get; set; }
        public List<Publisher> Publishers { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }
    }
}
