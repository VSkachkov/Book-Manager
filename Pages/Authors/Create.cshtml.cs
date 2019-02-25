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
    public class CreateModel : PageModel
    {
        readonly BookContext db;

        public CreateModel(BookContext ctx)
        {
            db = ctx;
            db.Database.EnsureCreated();
        }
        public void OnGet()
        {

        }

        [BindProperty]
        public Author Author { get; set; }

        public async Task<IActionResult> OnPostAsync(Author author)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(author.LastName))
            {
                try { 
                db.Authors.Add(author);
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