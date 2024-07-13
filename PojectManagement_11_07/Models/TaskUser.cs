using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement_11_07.Models
{
    [Table(("TaskUser"))]
    public class TaskUser
    {
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Tasks Task { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }
    }
}
