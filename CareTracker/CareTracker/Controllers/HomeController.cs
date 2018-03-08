using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CareTracker.Models;
using CareTracker.Data;
using Microsoft.AspNetCore.Identity;
using CareTracker.Models.DependentViewModels;

namespace CareTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

         public ICollection<UserDependent> GetUserDependents (ApplicationUser User)
        {
            return (from d in _context.Dependent
                    join du in _context.DependentUser
                      on d.DependentId equals du.DependentId
                    where du.User == User
                    select new UserDependent
                    {
                        DependentUserId = du.DependentUserId,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Birthday = d.Birthday
                    }).ToList();
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var model = new ViewDependentsHomePageViewModel();

            model.UserDependents = GetUserDependents(user);
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
