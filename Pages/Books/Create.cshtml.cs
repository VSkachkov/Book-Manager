using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Pages.Books
{
    public class CreateModel : PageModel
    {
        readonly BookContext db;

        public CreateModel(BookContext ctx)
        {
            db = ctx;
            db.Database.EnsureCreated();
            CategoriesForFront = new List<SelectListItem>{};
            AuthorsForFront = new List<SelectListItem> { };
            foreach (Category category in db.Categories)
            {
                CategoriesForFront.Add(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
            };
            foreach (Author author in db.Authors)
            {
                AuthorsForFront.Add(new SelectListItem { Value = author.Id.ToString(), Text = author.ForeName + " " + author.LastName });
            }
        }
        public void OnGet()
        {

        }

        [BindProperty]
        public Book Book { get; set; }
        public List<SelectListItem> CategoriesForFront { get; set; }
        public int selectedCategoryId { get; set; }
        public int selectedAuthorId { get; set; }
        public List<SelectListItem> AuthorsForFront { get; set; }

        public async Task<IActionResult> OnPostAsync(Book book)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(book.Title))
            {
                try
                {
                    //book.AuthorId = selectedAuthorId;
                    //book.CategoryId = selectedCategoryId;
                    db.Books.Add(book);
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