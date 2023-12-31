using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.SaleEntries
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public IndexModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SaleEntry> SaleEntry { get;set; } = default!;
        public string CurrentProduct { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.SaleEntries != null)
            {
                SaleEntry = await _context.SaleEntries
                    .Include(s => s.Product)
                    .ToListAsync();
            }
        }
    }
}
