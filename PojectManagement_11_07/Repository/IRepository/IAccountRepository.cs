using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;

namespace ProjectManagement_11_07.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task AddUserAsync(Users user);
        Task<Users> GetUserByUsername(string Username);
        Task<bool> RegisterAccount(RegisterViewModel registerViewModel);
        Task<Users> loginAccount(LoginViewModel loginViewModel);
    }
}
