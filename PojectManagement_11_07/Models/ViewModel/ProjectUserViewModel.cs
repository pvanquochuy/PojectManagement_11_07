using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_11_07.Models.ViewModel
{
    public class ProjectUserViewModel
    {

        public List<Projects> Projects { get; set; } = new List<Projects>();
        public List<Users> Users { get; set; } = new List<Users>();
    }
}
