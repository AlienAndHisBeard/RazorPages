using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Sales
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Sale Sale { get; set; } = default!; 
      public List<string> SaleEntries { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.SaleEntries).ThenInclude(saleEntry => saleEntry.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)return NotFound();

            Sale = sale;
            SaleEntries = sale.SaleEntries.ConvertAll(input => input.Product.Name + ", " + input.Product.Type + ", " + input.Product.PackingType + ", " + input.Product.Volume + "L, Bought:" + input.Amount);

            return Page();
        }
    }
}
