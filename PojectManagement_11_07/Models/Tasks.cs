using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement_11_07.Models
{
    [Table(("Tasks"))]
    public class Tasks
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }

        [StringLength(255)]
        public string TaskDescription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public int ProjectId { get; set; } // Assuming a separate Project table exists

        [ForeignKey("ProjectId")]
        public Projects Project { get; set; }

        public ICollection<TaskUser> TaskUsers { get; set; }
    }
}
