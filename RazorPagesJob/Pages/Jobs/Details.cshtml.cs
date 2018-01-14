using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesJob.Models;

namespace RazorPagesJob.Pages.Jobs
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesJob.Models.JobContext _context;

        public DetailsModel(RazorPagesJob.Models.JobContext context)
        {
            _context = context;
        }

        public Job Job { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Job = await _context.Job.SingleOrDefaultAsync(m => m.Link == id);

            if (Job == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
