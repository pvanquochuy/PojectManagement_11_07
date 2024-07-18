using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement_11_07.Models
{
    [Table(("Users"))]
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; } // Consider using a secure hashing mechanism for password storage

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress] // Add EmailAddress attribute for better validation
        public string Email { get; set; }

        [StringLength(255)] 
        public string ImageUrl { get; set; }

        public int RoleId { get; set; } // Assuming a separate Role table exists

        [ForeignKey("RoleId")]
        public Roles Role { get; set; }

        public ICollection<ProjectUser> ProjectUsers { get; set; }

        public ICollection<TaskUser> TaskUsers { get; set; }
    }
}
