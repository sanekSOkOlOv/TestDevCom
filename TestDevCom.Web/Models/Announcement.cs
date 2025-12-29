using System.ComponentModel.DataAnnotations;

namespace TestDevCom.Web.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string SubCategory { get; set; } = null!;
    }
}
