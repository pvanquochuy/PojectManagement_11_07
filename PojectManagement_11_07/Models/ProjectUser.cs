using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement_11_07.Models
{
    [Table(("ProjectUser"))]
    public class ProjectUser
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Projects Project { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }

        public int RoleId { get; set; } // Assuming a separate Role table exists

        [ForeignKey("RoleId")]
        public Roles Role { get; set; }
    }
}
