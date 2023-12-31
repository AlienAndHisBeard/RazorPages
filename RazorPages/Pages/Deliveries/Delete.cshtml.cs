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
    public class DeleteModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Delivery Delivery { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Deliveries == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.FirstOrDefaultAsync(m => m.Id == id);

            if (delivery == null)
            {
                return NotFound();
            }
            else 
            {
                Delivery = delivery;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Deliveries == null)
            {
                return NotFound();
            }
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery != null)
            {
                Delivery = delivery;
                _context.Deliveries.Remove(Delivery);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
