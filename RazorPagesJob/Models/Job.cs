using System.ComponentModel.DataAnnotations;

namespace RazorPagesJob.Models
{
    public class Job
    {
        public int ID { get; set; }
        [Required]
        public string Link { get; set; }
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Оклад")]
        public string Salary { get; set; }
        [Display(Name = "Организация")]
        public string Firma { get; set; }
        [Display(Name = "Контактное лицо")]
        public string Person { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Тип занятости")]
        public string Type { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
