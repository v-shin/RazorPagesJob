using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesJob.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Краткое описание приложения.";
        }
    }
}
