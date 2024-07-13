using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement_11_07.Models
{
    [Table(("Projects"))]
    public class Projects
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }

        [StringLength(255)]
        public string ProjectDescription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        public ICollection<Tasks> Tasks { get; set; }

        public ICollection<ProjectUser> ProjectUsers { get; set; }

       
    }
}
