using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Nohai.Models;

namespace Proiect_Nohai.Data
{
    [Authorize]
    public class DrugsController : Controller
    {
        private readonly PharmacyContext _context;

        public DrugsController(PharmacyContext context)
        {
            _context = context;
        }

        // GET: Drugs
        public async Task<IActionResult> Index(
            string sortOrder, 
            string currentFilter, 
            string searchString, 
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var drugs = from b in _context.Drugs select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                drugs = drugs.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    drugs = drugs.OrderByDescending(b => b.Name);
                    break;
                case "Price":
                    drugs = drugs.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    drugs = drugs.OrderByDescending(b => b.Price);
                    break;
                default:
                    drugs = drugs.OrderBy(b => b.Name);
                    break;
            }
            int pageSize = 4;
            return View(await PaginatedList<Drug>.CreateAsync(drugs.AsNoTracking(), pageNumber ??1, pageSize));
        }

        // GET: Drugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drugs
                .Include(s => s.Orders)
                .ThenInclude(e => e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        // GET: Drugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Manufacturer,AgeLimit,TypeOfDrug,Price")] Drug drug)
        {
            try
            {

            
            if (ModelState.IsValid)
            {
                _context.Add(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            } catch(DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }

            return View(drug);
        }

        // GET: Drugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null)
            {
                return NotFound();
            }
            return View(drug);
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id != null)
            {
                return NotFound();
            }

            var studentToUpdate = await _context.Drugs.FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Drug>(
                studentToUpdate,
                "", 
                s => s.Name, s => s.Manufacturer, s => s.AgeLimit, s => s.TypeOfDrug, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Drugs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drugs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (drug == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Please try again!";
            }

            return View(drug);
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Drugs.Remove(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }

        private bool DrugExists(int id)
        {
            return _context.Drugs.Any(e => e.ID == id);
        }
    }
}
