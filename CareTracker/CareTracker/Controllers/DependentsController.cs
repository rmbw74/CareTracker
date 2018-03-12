using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareTracker.Data;
using Microsoft.AspNetCore.Identity;
using CareTracker.Models;

namespace CareTracker.Controllers
{
    public class DependentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DependentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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


        // GET: Dependents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dependent.ToListAsync());
        }

        // GET: Dependents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent
                .SingleOrDefaultAsync(m => m.DependentId == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // GET: Dependents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dependents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DependentId,FirstName,LastName,Birthday,SocialSecurityNumber,DependentNotes")] Dependent dependent)
        {

            //gets the current user
            ApplicationUser user = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                _context.Add(dependent);
                await _context.SaveChangesAsync();
                

                //create entry on DependentUser Table
                var newDependentUser = new DependentUser
                {
                    User = user,
                    DependentId = dependent.DependentId
                };

                _context.Add(newDependentUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");


            }
            return View(dependent);
        }

        // GET: Dependents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent.SingleOrDefaultAsync(m => m.DependentId == id);
            if (dependent == null)
            {
                return NotFound();
            }
            return View(dependent);
        }

        // POST: Dependents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DependentId,FirstName,LastName,Birthday,SocialSecurityNumber,DependentNotes")] Dependent dependent)
        {
            if (id != dependent.DependentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependentExists(dependent.DependentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dependent);
        }

        // GET: Dependents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent
                .SingleOrDefaultAsync(m => m.DependentId == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // POST: Dependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependent = await _context.Dependent.SingleOrDefaultAsync(m => m.DependentId == id);
            _context.Dependent.Remove(dependent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependentExists(int id)
        {
            return _context.Dependent.Any(e => e.DependentId == id);
        }
    }
}
