namespace ProjectManagement_11_07.Models.ViewModel
{
    public class UpdateStatusRequest
    {
        public int TaskId { get; set; }
        public string NewStatus { get; set; }
    }
}
