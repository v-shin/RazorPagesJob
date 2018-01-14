﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesJob.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Контактная информация.";
        }
    }
}
