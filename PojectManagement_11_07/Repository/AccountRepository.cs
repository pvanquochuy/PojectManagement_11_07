using Microsoft.EntityFrameworkCore;
using ProjectManagement_11_07.Data;
using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;
using ProjectManagement_11_07.Repository.IRepository;


namespace ProjectManagement_11_07.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      

        public async Task<bool> RegisterAccount(RegisterViewModel registerViewModel)
        {

            string UserName = registerViewModel.Username.Trim();
            string Password = registerViewModel.Password.Trim();
            string FullName = registerViewModel.FullName.Trim();
            string Email = registerViewModel.Email.Trim();

            var existingUser = await GetUserByUsername(UserName);
            if (existingUser != null)
            {
                return false;
            }
            Users users = new Users
            {
                Username = UserName,
                Password = Password,
                FullName = FullName,
                Email = Email,
                RoleId = 2 // 1: manager, 2: employee

            };
            await AddUserAsync(users);
            return true;
        }


        public async Task<Users> GetUserByUsername(string Username)
        {
            return await _dbContext.Users
            .SingleOrDefaultAsync(u => u.Username == Username);
        }

        public async Task AddUserAsync(Users user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Users> loginAccount(LoginViewModel loginViewModel)
        {
            string username = loginViewModel.Username.Trim();
            string password = loginViewModel.Password.Trim();

            var user  = await _dbContext.Users
        .SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

            return user;
        }
    }
}
