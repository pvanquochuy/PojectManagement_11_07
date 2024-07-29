using Microsoft.EntityFrameworkCore;
using ProjectManagement_11_07.Data;
using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;
using ProjectManagement_11_07.Repository.IRepository;

namespace ProjectManagement_11_07.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tasks>> GetTaskByIdProject(int projectId)
        {
            return await _dbContext.Tasks
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }
        public async Task<Tasks> GetTaskByTaskId(int taskId)
        {
            return await _dbContext.Tasks
                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }
        public async Task UpdateTask(Tasks task)
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Users>> GetUsersByProjectId(int projectId)
        {
            return await _dbContext.ProjectUser
                .Where(pu => pu.ProjectId == projectId)
                .Select(pu => pu.User)
                .ToListAsync();
        }

        public async Task<bool> AddTask(AddTaskViewModel model)
        {
            var task = new Tasks
            {
                TaskName = model.TaskName,
                TaskDescription = model.TaskDescription,
                EndDate = model.EndDate,
                StatusTask = model.StatusTask,
                ProjectId = model.ProjectId,
               
            };

            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<Users>> GetUsersByTaskId(int taskId)
        {
            return await _dbContext.TaskUser
                .Where(tu => tu.TaskId == taskId)
                .Select(tu => tu.User)
                .ToListAsync();
        }
        public async Task<Users> GetUserById(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task DeleteTask(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }

        }


        public async Task<List<Tasks>> SearchTaskByName(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await _dbContext.Tasks
                    .Include(p => p.TaskUsers)
                    .ToListAsync();
            }

            return await _dbContext.Tasks
                .Include(p => p.TaskUsers)
                .Where(p => p.TaskName.Contains(searchString))
                .ToListAsync();
        }

        public async Task<bool> EditTask(AddTaskViewModel model)
        {
            // Load the existing project including its associated ProjectUsers
            var task = await _dbContext.Tasks
                .Include(t => t.TaskUsers)
                .FirstOrDefaultAsync(t => t.TaskId == model.TaskId);

            if (task == null) return false;

            // Update project details from the ViewModel
            task.TaskName = model.TaskName.Trim();
            task.TaskDescription = model.TaskDescription.Trim();
            task.EndDate = model.EndDate;
            task.StatusTask = model.StatusTask.Trim();

            // Clear existing ProjectUsers
            task.TaskUsers.Clear();

            // Add selected users to the project
            foreach (var userId in model.SelectedUserIds)
            {
                var taskUser = new TaskUser
                {
                    TaskId = task.TaskId,
                    UserId = userId,
                    
                };

                task.TaskUsers.Add(taskUser);
            }

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}

