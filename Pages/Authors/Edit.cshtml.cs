using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Pages.Authors
{
    public class EditModel : PageModel
    {
        readonly BookContext db;

        public EditModel(BookContext ctx) { db = ctx; }

        [BindProperty]
        public Author Author { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Author = await db.Authors.FindAsync(id);
            if (Author == null)
                return RedirectToPage("./Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(Author.LastName))
            {
                try
                {
                    Author.Id = id;
                    db.Attach(Author).State = EntityState.Modified;
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