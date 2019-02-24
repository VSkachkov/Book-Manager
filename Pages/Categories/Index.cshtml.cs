using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Pages.Categories
{
    public class IndexModel : PageModel
    {
        readonly BookContext db;

        public IndexModel(BookContext ctx)
        {
            db = ctx;
            db.Database.EnsureCreated();
        }

        [TempData]
        public string Result { get; set; }
        [BindProperty]
        public Category Category { get; set; }
        public IList<Category> Categories { get; private set; }

        public async Task OnGetAsync()
        {
            Categories = await db.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            if (!string.IsNullOrWhiteSpace(Category.Name))
            {
                db.Categories.Add(Category);
                await db.SaveChangesAsync();
                Result = $"Message id = {Category.Id} added to Db. (handler: OnPostAddAsync)";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var m = await db.Categories.FindAsync(id);
            db.Categories.Remove(m);
            await db.SaveChangesAsync();
            Result = $"Message with Id = {m.Id} was deleted. (handler: OnPostDeleteAsync)";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAll()
        {
            foreach (Category m in db.Categories)
                db.Categories.Remove(m);
            await db.SaveChangesAsync();
            Result = "All messages were deleted. (handler: OnPostDeleteAll)";
            return RedirectToPage();
        }


    }
}