using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_11_07.Models.ViewModel
{
    public class AddTaskViewModel
    {
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string TaskDescription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public String StatusTask { get; set; }

        [Required]
        [Display(Name = "Project Id")]
        public int ProjectId { get; set; }

        public List<Users> Users { get; set; } = new List<Users>();

        public List<int> SelectedUserIds { get; set; } = new List<int>();

    }
}
