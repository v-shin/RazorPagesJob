using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesJob.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesJob.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        //private readonly RazorPagesJob.Models.JobContext _context;
        private readonly JobContext _context;

        public IndexModel(RazorPagesJob.Models.JobContext context)
        {
            _context = context;
        }

        public IList<Job> Job { get;set; }
        public SelectList WTypes;
        public string JobWType { get; set; }

        public async Task OnGetAsync(string jobWType, string searchString)
        {
            // Use LINQ to get list of types.
            IQueryable<string> typeQuery = from m in _context.Job
                                            orderby m.Type
                                            select m.Type;

            var jobs = from m in _context.Job
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(jobWType))
            {
                jobs = jobs.Where(x => x.Type == jobWType);
            }
            WTypes = new SelectList(await typeQuery.Distinct().ToListAsync());
            Job = await jobs.ToListAsync();
        }

    }
}
