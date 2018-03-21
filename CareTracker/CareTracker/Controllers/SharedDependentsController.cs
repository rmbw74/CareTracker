using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareTracker.Data;
using CareTracker.Models;
using CareTracker.Models.SharedDependentViewModels;
using Microsoft.AspNetCore.Identity;

namespace CareTracker.Controllers
{
    public class SharedDependentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SharedDependentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: SharedDependents
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var model = new SharedDependentViewModel(user, _context);

            return View(model);
        }

        // GET: SharedDependents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedDependent = await _context.SharedDependent
                .Include(s => s.DependentUser)
                .SingleOrDefaultAsync(m => m.SharedDependentId == id);
            if (sharedDependent == null)
            {
                return NotFound();
            }

            return View(sharedDependent);
        }

        // GET: SharedDependents/Create
        public IActionResult Create()
        {
            ViewData["DependentUserId"] = new SelectList(_context.DependentUser, "DependentUserId", "UserId");
            return View();
        }

        // POST: SharedDependents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (string ToUserEmail, int DependentId)
        {
            //string normalized = ToUserEmail.ToUpper();
            ApplicationUser toUser = await _context.Users.Where(u => u.UserName == ToUserEmail).FirstOrDefaultAsync();

            var SharedDependent = new SharedDependent
            {
                ToUser = toUser,
                DependentUserId = DependentId
            };
            if (ModelState.IsValid)
            {
                _context.Add(SharedDependent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: SharedDependents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedDependent = await _context.SharedDependent.SingleOrDefaultAsync(m => m.SharedDependentId == id);
            if (sharedDependent == null)
            {
                return NotFound();
            }
            ViewData["DependentUserId"] = new SelectList(_context.DependentUser, "DependentUserId", "UserId", sharedDependent.DependentUserId);
            return View(sharedDependent);
        }

        // POST: SharedDependents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SharedDependentId,DependentUserId")] SharedDependent sharedDependent)
        {
            if (id != sharedDependent.SharedDependentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharedDependent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharedDependentExists(sharedDependent.SharedDependentId))
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
            ViewData["DependentUserId"] = new SelectList(_context.DependentUser, "DependentUserId", "UserId", sharedDependent.DependentUserId);
            return View(sharedDependent);
        }

        // GET: SharedDependents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedDependent = await _context.SharedDependent
                .Include(s => s.DependentUser)
                .SingleOrDefaultAsync(m => m.SharedDependentId == id);
            if (sharedDependent == null)
            {
                return NotFound();
            }

            return View(sharedDependent);
        }

        // POST: SharedDependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sharedDependent = await _context.SharedDependent.SingleOrDefaultAsync(m => m.SharedDependentId == id);
            _context.SharedDependent.Remove(sharedDependent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharedDependentExists(int id)
        {
            return _context.SharedDependent.Any(e => e.SharedDependentId == id);
        }

        public async Task<IActionResult> AcceptShare(int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            SharedDependent remove = await _context.SharedDependent
                                                    .Where(sd => sd.DependentUserId == id && sd.ToUser == user)
                                                    .SingleOrDefaultAsync();
            DependentUser DependentUser = new DependentUser
            {
                DependentId = id,
                User = user,

            };
            _context.Add(DependentUser);
            await _context.SaveChangesAsync();

            _context.Remove(remove);
            await _context.SaveChangesAsync();



            // SharedDependent Remove =  RemoveFromShareTable(id);
            //return RedirectToAction("RemoveShared", new { id = id });
            return RedirectToAction(nameof(Index));

        }
        
        public async Task<IActionResult> RejectShare(int id)
        {
            SharedDependent DeleteShare = await _context.SharedDependent
                                                        .Where(d => d.SharedDependentId == id)
                                                        .SingleOrDefaultAsync();
            _context.Remove(DeleteShare);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
 
        
        
    }
}
