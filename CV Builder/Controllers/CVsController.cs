using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CV_Builder.Data;
using CV_Builder.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CV_Builder.Controllers
{
    [Authorize]
    public class CVsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public CVsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CVs
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(await _context.CV
                .Where(i => i.UserId == userId)
                .ToListAsync()); 
        }

        // GET: CVs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // GET: CVs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] CV cV)
        {
            
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                cV.UserId = userId;
                _context.Add(cV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cV);
        }

        // GET: CVs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV.FindAsync(id);
            if (cV == null)
            {
                return NotFound();
            }
            return View(cV);
        }

        // POST: CVs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] CV cV)
        {
            if (id != cV.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CVExists(cV.Id))
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
            return View(cV);
        }

        // GET: CVs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // POST: CVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cV = await _context.CV.FindAsync(id);
            if (cV != null)
            {
                _context.CV.Remove(cV);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CVExists(int id)
        {
            return _context.CV.Any(e => e.Id == id);
        }
    }
}
