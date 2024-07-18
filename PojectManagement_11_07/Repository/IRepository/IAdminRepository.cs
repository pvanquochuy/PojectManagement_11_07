using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;

namespace ProjectManagement_11_07.Repository.IRepository
{
    public interface IAdminRepository
    {
        Task<List<Projects>> GetAllProjects();
        Task<List<Users>> GetAllUsers();
        Task<bool> AddProjectWithUsers(AddProjectViewModel model);
        Task DeleteProject(int id);

        Task<bool> EditProject(AddProjectViewModel model);

        Task<Projects> GetProjectById(int projectId);

    }
}
