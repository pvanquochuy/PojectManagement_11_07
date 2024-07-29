using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;

namespace ProjectManagement_11_07.Repository.IRepository
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetTaskByIdProject(int projectId);
        Task<Tasks> GetTaskByTaskId(int taskId);
        Task UpdateTask(Tasks task);
        Task<List<Users>> GetUsersByProjectId(int projectId);
        Task<bool> AddTask(AddTaskViewModel task);

        Task<List<Users>> GetUsersByTaskId(int taskId);
        Task DeleteTask(int id);
        Task<bool> EditTask(AddTaskViewModel model);
    }
}
