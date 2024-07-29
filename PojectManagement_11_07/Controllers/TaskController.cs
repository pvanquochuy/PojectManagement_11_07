using Microsoft.AspNetCore.Mvc;
using ProjectManagement_11_07.Models.ViewModel;
using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Repository;

namespace ProjectManagement_11_07.Controllers
{
    public class TaskController : Controller
    {

        private readonly AdminRepository _adminRepository;
        private readonly TaskRepository _taskRepository;

        public TaskController(AdminRepository adminRepository, TaskRepository taskRepository)
        {
            _adminRepository = adminRepository;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Task(int projectId)
        {
            var project = await _adminRepository.GetProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var tasks = await _taskRepository.GetTaskByIdProject(projectId);

            var taskUsers = new Dictionary<int, List<Users>>();
            foreach (var task in tasks)
            {
                var users = await _taskRepository.GetUsersByTaskId(task.TaskId);
                taskUsers[task.TaskId] = users;
            }


            var model = new TaskViewModel
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Tasks = tasks,
                TaskUsers = taskUsers,


                ProcessingTasks = tasks.Where(t => t.StatusTask == "Đang thực hiện").ToList(),
                CompletedTasks = tasks.Where(t => t.StatusTask == "Hoàn thành").ToList()
            };

            return View("~/Views/Admin/Task.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            var task = await _taskRepository.GetTaskByTaskId(request.TaskId);
            if (task == null)
            {
                return NotFound(new { success = false, message = "Task not found" });
            }

            task.StatusTask = request.NewStatus;
            await _taskRepository.UpdateTask(task);

            return Ok(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> AddTask(int projectId)
        {
            //var users = await _taskRepository.GetUsersByProjectId(projectId);
            var viewModel = new AddTaskViewModel
            {
                ProjectId = projectId,
                Users = await _taskRepository.GetUsersByProjectId(projectId)
            };

            return View("/Views/Admin/AddTask.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Users = await _taskRepository.GetUsersByProjectId(viewModel.ProjectId);
                var result = await _taskRepository.AddTask(viewModel);

                if (result)
                {
                    return RedirectToAction("Task", new { projectId = viewModel.ProjectId });
                }

            }
            // Nếu có lỗi, hoặc model không hợp lệ, trả về view với dữ liệu hiện tại
            viewModel.Users = await _taskRepository.GetUsersByProjectId(viewModel.ProjectId);
            return View("/Views/Admin/AddTask.cshtml", viewModel);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTask(id);
            {
                return Json(new { success = true, message = "Xóa nhiệm vụ thành công." });
            }
            return Json(new { success = false, message = "Xóa nhiệm vụ thất bại." });
        }

        [HttpGet]
        public async Task<IActionResult> SearchTaskByName(string searchString)
        {
            var task = await _taskRepository.SearchTaskByName(searchString);
            return View("Task", task);
        }

        [HttpGet]
        public async Task<IActionResult> EditTask(int id)
        {
            var task = await _taskRepository.GetTaskByTaskId(id);
            if (task == null)
            {
                return NotFound();
            }
            var viewModel = new AddTaskViewModel
            {
                TaskId = id,
                Users = await _taskRepository.GetUsersByProjectId(task.ProjectId)
            };


            var model = new AddTaskViewModel
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                ProjectId = task.ProjectId,
                EndDate = task.EndDate,
                StatusTask = task.StatusTask,
                Users = viewModel.Users,

            };

            return View("/Views/Admin/AddTask.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(AddTaskViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _taskRepository.EditTask(model);
                if (result)
                {
                    return RedirectToAction("Home", "Admin");
                }
            }

            return View("~/Views/Admin/Task.cshtml", model);

        }

    }
}

