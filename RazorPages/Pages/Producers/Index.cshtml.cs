using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Producers
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public IndexModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Producer> Producer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Producers != null)
            {
                Producer = await _context.Producers.ToListAsync();
            }
        }
    }
}
