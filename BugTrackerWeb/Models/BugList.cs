using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerWeb.Models
{
    public class BugList
    {
        //ID
        [Key]
        public int Id { get; set; }

        //Title
        [Required]
        public string? Name { get; set; }

        //Description
        public string? Description { get; set; }

        //Status
        public bool Status { get; set; } = true;

        //Time
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        //User
        public string? User { get; set; }
    }
}
