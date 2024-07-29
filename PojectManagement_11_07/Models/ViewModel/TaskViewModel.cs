namespace ProjectManagement_11_07.Models.ViewModel
{
    public class TaskViewModel
    {
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public string ProjectName { get; set; }
        public List<Tasks> Tasks { get; set; } = new List<Tasks>();
        public List<Users> Users { get; set; } = new List<Users>();
        public Dictionary<int, List<Users>> TaskUsers { get; set; } = new Dictionary<int, List<Users>>();


        public List<Tasks> ProcessingTasks { get; set; } = new List<Tasks>();
        public List<Tasks> CompletedTasks { get; set; } = new List<Tasks>();

    }
}
