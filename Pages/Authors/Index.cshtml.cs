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
        public Author Author { get; set; }
        public IList<Author> Authors { get; private set; }

        public async Task OnGetAsync()
        {
            Authors = await db.Authors.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            if (!string.IsNullOrWhiteSpace(Author.LastName))
            {
                db.Authors.Add(Author);
                await db.SaveChangesAsync();
                Result = $"Message id = {Author.Id} added to Db. (handler: OnPostAddAsync)";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var m = await db.Authors.FindAsync(id);
            db.Authors.Remove(m);
            await db.SaveChangesAsync();
            Result = $"Message with Id = {m.Id} was deleted. (handler: OnPostDeleteAsync)";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAll()
        {
            foreach (Author m in db.Authors)
                db.Authors.Remove(m);
            await db.SaveChangesAsync();
            Result = "All messages were deleted. (handler: OnPostDeleteAll)";
            return RedirectToPage();
        }


    }
}