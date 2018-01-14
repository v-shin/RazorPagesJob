using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using AngleSharp;
using System.Threading.Tasks;

namespace RazorPagesJob.Models
{
    public static class SeedData
    {
        public static bool Loading;
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new JobContext(
                serviceProvider.GetRequiredService<DbContextOptions<JobContext>>()))
            {
                Loading = true;

                // Setup the configuration to support document loading
                var config = Configuration.Default.WithDefaultLoader();
                var address = "https://obninsk.hh.ru/search/vacancy?text=&area=301&salary=&currency_code=RUR&experience=doesNotMatter&order_by=publication_time&search_period=&items_on_page=50&no_magic=true";
                // Asynchronously get the document in a new context using the configuration
                var document = await BrowsingContext.New(config).OpenAsync(address);
                if (document == null) {
                    Loading = false;
                    return;
                }
                var cells = document.All.Where(m => m.LocalName == "a" && m.ClassName == "bloko-link HH-LinkModifier");
                if (cells == null)
                {
                    Loading = false;
                    return;
                }
                var titles = cells.Select(m => m.GetAttribute("href"));
                if (titles == null)
                {
                    Loading = false;
                    return;
                }
                foreach (var item in titles)
                {
                    Console.WriteLine(item);
                    document = await BrowsingContext.New(config).OpenAsync(item);
                    if (document == null) continue;
                    cells = document.QuerySelectorAll("p[data-qa=\"vacancy-contacts__phone\"]");
                    var hh = from x in cells select x.TextContent;
                    context.Job.Add(
                        new Job
                        {
                            Link = item,
                            Title = document.QuerySelector("h1[data-qa=\"vacancy-title\"]")?.TextContent ?? "Не указана",
                            Firma = document.QuerySelector("div[data-qa=\"vacancy-company\"]")?.TextContent ?? "Не указана",
                            Salary = document.QuerySelector("div[data-qa=\"vacancy-compensation\"]")?.TextContent ?? "Не указана",
                            Description = document.QuerySelector("div.b-vacancy-desc")?.TextContent ?? "Нет описания",
                            Type = document.QuerySelector("div.b-vacancy-employmentmode div")?.TextContent ?? "Не указан",
                            Person = document.QuerySelector("p[data-qa=\"vacancy-contacts__fio\"]")?.TextContent ?? "Не указано",
                            Phone = string.Join(" : ", hh)
                        }
                    );
                }

                context.Job.RemoveRange(context.Job);
                context.SaveChanges();
                Loading = false;
            }
        }
    }
}
