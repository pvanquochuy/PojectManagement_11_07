using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement_11_07.Models;
using ProjectManagement_11_07.Models.ViewModel;
using ProjectManagement_11_07.Repository;
using ProjectManagement_11_07.Repository.IRepository;

namespace ProjectManagement_11_07.Controllers
{


    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository;
        public AccountController( AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.loginAccount(loginViewModel);
                if (result != null)
                {
                    var userJson = JsonConvert.SerializeObject(result, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        });
                    HttpContext.Session.SetString("Users", userJson);
                    HttpContext.Session.SetInt32("RoleId", result.RoleId);
                    if (result.RoleId == 1)
                    {
                        TempData["SuccessMessage"] = "Đăng nhập thành công";
                        return RedirectToAction("Home", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng";

                }
            }
            
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.RegisterAccount(registerViewModel);
                if (result)
                {
                    TempData["SuccessMessage"] = "Đăng kí thành công!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại.";

                }
            }
            return View("Login");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
