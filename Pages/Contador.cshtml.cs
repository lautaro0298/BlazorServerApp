using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BlazorServer.Pages
{
    public class Contador : PageModel
    {
        private readonly ILogger<Contador> _logger;

        public Contador(ILogger<Contador> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}