using Microsoft.EntityFrameworkCore;
using ProjectManagement_11_07.Data;
using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;
using ProjectManagement_11_07.Repository.IRepository;

namespace ProjectManagement_11_07.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Projects>> GetAllProjects()
        {
            var projects = await _dbContext.Projects
                                   .Include(p => p.ProjectUsers)
                                   .ThenInclude(pu => pu.User)
                                   .ToListAsync();
            return projects;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<bool> AddProjectWithUsers(AddProjectViewModel model)
        {
            var newProject = new Projects
            {
                ProjectName = model.ProjectName.Trim(),
                ProjectDescription = model.ProjectDescription.Trim(),
                StartDate = model.StartDate,
                StatusProject = model.StatusProject.Trim(),
                Tasks = new List<Tasks>(), // Initialize an empty list of tasks if needed
                ProjectUsers = new List<ProjectUser>() // Initialize an empty list of project users if needed
            };

            _dbContext.Projects.Add(newProject);
            await _dbContext.SaveChangesAsync();

            foreach (var userId in model.SelectedUserIds)
            {
                var projectUser = new ProjectUser
                {
                    ProjectId = newProject.ProjectId,
                    UserId = userId,
                    RoleId = 2 // Set role ID as needed
                };

                _dbContext.ProjectUser.Add(projectUser);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task DeleteProject(int id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project != null)
            {
                _dbContext.Projects.Remove(project);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<Projects> GetProjectById(int projectId)
        {
            return await _dbContext.Projects
                .Include(p => p.ProjectUsers)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }


        public async Task<bool> EditProject(AddProjectViewModel model)
        {
            // Load the existing project including its associated ProjectUsers
            var project = await _dbContext.Projects
                .Include(p => p.ProjectUsers)
                .FirstOrDefaultAsync(p => p.ProjectId == model.ProjectId);

            if (project == null) return false;

            // Update project details from the ViewModel
            project.ProjectName = model.ProjectName.Trim();
            project.ProjectDescription = model.ProjectDescription.Trim();
            project.StartDate = model.StartDate;
            project.StatusProject = model.StatusProject.Trim();

            // Clear existing ProjectUsers
            project.ProjectUsers.Clear();

            // Add selected users to the project
            foreach (var userId in model.SelectedUserIds)
            {
                var projectUser = new ProjectUser
                {
                    ProjectId = project.ProjectId,
                    UserId = userId,
                    RoleId = 2 // Set role ID as needed
                };

                project.ProjectUsers.Add(projectUser);
            }

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
            return true;
        }



        public async Task<List<Projects>> SearchProjects(string searchString, string statusProject, int? minMemberCount, int? maxMemberCount, DateTime? startDate)
        {
            var query = _dbContext.Projects
                .Include(p => p.ProjectUsers)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(p => p.ProjectName.Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(statusProject))
            {
                query = query.Where(p => p.StatusProject == statusProject);
            }

            if (minMemberCount.HasValue)
            {
                query = query.Where(p => p.ProjectUsers.Count >= minMemberCount.Value);
            }

            if (maxMemberCount.HasValue)
            {
                query = query.Where(p => p.ProjectUsers.Count <= maxMemberCount.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.StartDate >= startDate.Value);
            }

            return await query.ToListAsync();
        }

    }
}
