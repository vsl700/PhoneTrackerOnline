using PhoneTracker.Models;
using PhoneTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneTrackerOnline.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PhoneTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(_db.CallerUsers.Where(user => user.Username == model.Username).Count() > 0)
                {
                    ModelState.AddModelError("", "Such username already exists!");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Name = model.Username
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    
                    User userModel = new User() { Username = user.UserName };
                    _db.CallerUsers.Add(userModel);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Track");
                }
                ModelState.AddModelError("", "Invalid login attempt!");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        /*public IActionResult Register()
        {
            return View();
        }*/
    }
}
