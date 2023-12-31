using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Pallets
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public IndexModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pallet> Pallet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Pallets != null)
            {
                Pallet = await _context.Pallets.Include(p => p.Product).ToListAsync();
            }
        }
    }
}
