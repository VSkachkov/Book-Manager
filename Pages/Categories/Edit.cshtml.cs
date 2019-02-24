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
    public class EditModel : PageModel
    {
        readonly BookContext db;

        public EditModel(BookContext ctx) { db = ctx; }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await db.Categories.FindAsync(id);
            if (Category == null)
                return RedirectToPage("./Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(Category.Name))
            {
                try
                {
                    Category.Id = id;
                    db.Attach(Category).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ViewData["Error"] = $"UpdateConcurrencyException: {ex.Message}";
                    return RedirectToPage("./Index");
                }
            }
            return RedirectToPage("./Index");
        }
    }
}