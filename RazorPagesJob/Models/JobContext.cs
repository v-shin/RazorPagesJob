using Microsoft.EntityFrameworkCore;

namespace RazorPagesJob.Models
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options)
               : base(options)
        {
        }

        public DbSet<Job> Job { get; set; }
    }
}
