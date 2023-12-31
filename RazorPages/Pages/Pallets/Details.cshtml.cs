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
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Pallet Pallet { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pallets == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets.Include(s => s.Product).FirstOrDefaultAsync(m => m.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }
            else 
            {
                Pallet = pallet;
            }
            return Page();
        }
    }
}
