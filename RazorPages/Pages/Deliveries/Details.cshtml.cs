using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Deliveries
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Delivery Delivery { get; set; } = default!;
      public List<string> Pallets { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Deliveries == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.Include(s => s.Pallets).ThenInclude(saleEntry => saleEntry.Product).FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null) return NotFound();


            Delivery = delivery;
            Pallets = delivery.Pallets.ConvertAll(input => input.Product.Name + ", " + input.Product.Type + ", " + input.Product.PackingType + ", " + input.Product.Volume + "L, Bought:" + input.Amount);

            return Page();
        }
    }
}
