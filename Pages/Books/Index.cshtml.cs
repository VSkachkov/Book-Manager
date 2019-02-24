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
        readonly int pageSize;
        public PaginatedList<Book> Books { get; set; }

        public IndexModel(BookContext ctx)
        {
            db = ctx;
            db.Database.EnsureCreated();
            DbInitializer.Seed(db);
            pageSize = 5;
        }
        [TempData]
        public string Result { get; set; }
        [BindProperty]
        public Book Book { get; set; }
            public async Task OnGetAsync(string sort, string filter, string search, int? pageID)
        {
            var books = db.Books.Include("Author").AsQueryable();
            Books = await PaginatedList<Book>.CreateAsync(books, pageID ?? 1, pageSize);
        }

    }
}