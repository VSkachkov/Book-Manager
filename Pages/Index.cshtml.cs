using BookManager.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookManager.Pages
{
    public class IndexModel : PageModel
    {
        BookContext db;
        public bool Hidden { get; set; }
        public string Result { get; set; }

        public IndexModel(BookContext ctx)
        {
            db = ctx;
            Hidden = true;
            Result = "";
        }

        public void OnGetTryDropDbAsync()
        {
            Result = "Confirm deleting the whole database. (handler: OnGetTryDropDbAsync)";
            Hidden = false;
        }
        public void OnGetCancel() { Hidden = true; }

        public async Task OnGetDropDbAsync()
        {
            await db.Database.EnsureDeletedAsync();
            Hidden = true;
            Result = @"Database is destroyed. Check up in C:\Users\YourName\";
        }
    }

}
