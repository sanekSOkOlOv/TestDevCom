using TestDevCom.Enums;

namespace TestDevCom.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
