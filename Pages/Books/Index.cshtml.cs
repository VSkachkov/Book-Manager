using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Pages.Books
{
    public class IndexModel : PageModel
    {
        readonly BookContext db;

        public IndexModel(BookContext ctx) {
            db = ctx;
            db.Database.EnsureCreated();
            DbInitializer.Seed(db);
        }
        [TempData]
        public string Result { get; set; }
        [BindProperty]
        public Book Book { get; set; }
        public IList<Book> Books { get; private set; }

        public async Task OnGetAsync()
        {
            Books = await db.Books.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            if (!string.IsNullOrWhiteSpace(Book.Title))
            {
                db.Books.Add(Book);
                await db.SaveChangesAsync();
                Result = $"Message id = {Book.Id} added to Db. (handler: OnPostAddAsync)";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var m = await db.Books.FindAsync(id);
            db.Books.Remove(m);
            await db.SaveChangesAsync();
            Result = $"Message with Id = {m.Id} was deleted. (handler: OnPostDeleteAsync)";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAll()
        {
            foreach (Book m in db.Books)
                db.Books.Remove(m);
            await db.SaveChangesAsync();
            Result = "All messages were deleted. (handler: OnPostDeleteAll)";
            return RedirectToPage();
        }

    }
}