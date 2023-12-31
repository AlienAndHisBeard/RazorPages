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
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public SaleEntry SaleEntry { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SaleEntries == null)
            {
                return NotFound();
            }

            var saleentry = await _context.SaleEntries.Include(s => s.Product).FirstOrDefaultAsync(m => m.Id == id);
            if (saleentry == null)
            {
                return NotFound();
            }
            else 
            {
                SaleEntry = saleentry;
            }
            return Page();
        }
    }
}
