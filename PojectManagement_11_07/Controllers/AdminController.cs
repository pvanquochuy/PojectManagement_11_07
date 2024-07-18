using Microsoft.AspNetCore.Mvc;
using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;
using ProjectManagement_11_07.Repository;
using System.Globalization;

namespace ProjectManagement_11_07.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminRepository _adminRepository;
        public AdminController(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IActionResult> Home()


        {
            var projects = await _adminRepository.GetAllProjects();

            var users = await _adminRepository.GetAllUsers();

            var viewModel = new ProjectUserViewModel
            {
                Projects = projects,
                Users = users
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddProject()
        {
            var users = await _adminRepository.GetAllUsers();

            var viewModel = new AddProjectViewModel
            {
                Users = users
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(AddProjectViewModel model)
        {
            AddProjectViewModel test = model;

            if (ModelState.IsValid)
            {
                var result = await _adminRepository.AddProjectWithUsers(model);
                if (result)
                {
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            
            return View(model);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _adminRepository.DeleteProject(id);
            {
                return Json(new { success = true, message = "Xóa dự án thành công." });
            }
            return Json(new { success = false, message = "Xóa dự án thất bại." });
        }

        [HttpGet]
        public async Task<IActionResult> EditProject(int id)
        {
            var project = await _adminRepository.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            var users = await _adminRepository.GetAllUsers();

            var model = new AddProjectViewModel
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                StartDate = project.StartDate,
                StatusProject = project.StatusProject,
                SelectedUserIds = project.ProjectUsers.Select(pu => pu.UserId).ToList(),
                Users = users
            };

            return View("AddProject", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(AddProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminRepository.EditProject(model);
                if (result)
                {
                    return RedirectToAction("Home", "Admin");
                }
            }

            return View("AddProject", model);

        }

    }
}
