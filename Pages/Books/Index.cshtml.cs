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
        public string Sort { get; set; }
        public string IdSort { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string Filter { get; set; }


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
            if (search != null)
                pageID = 1;
            else
                search = filter;
            Filter = search;


            Sort = sort;
            IdSort = string.IsNullOrEmpty(sort) ? "idDesc" : "";
            TitleSort = string.IsNullOrEmpty(sort) ? "titleDesc" : "";
            AuthorSort = string.IsNullOrEmpty(sort) ? "authorDesc" : "";

            var books = db.Books.Include("Author").AsQueryable();
            //Books = await PaginatedList<Book>.CreateAsync(books, pageID ?? 1, pageSize);
            switch (sort)
            {
                case "idDesc": books = books.OrderByDescending(x => x.Id); break;
                case "titleDesc": books = books.OrderByDescending(x => x.Title); break;
                case "authorDesc": books = books.OrderByDescending(x => x.Author.FullName()); break;
                default: books = books.OrderBy(x => x.Id); break;
            }
			if (!string.IsNullOrEmpty(search))
                books = books.Where(x => x.Title.Contains(search));

            Books = await PaginatedList<Book>.CreateAsync(books, pageID ?? 1, pageSize);
        }

    }
}