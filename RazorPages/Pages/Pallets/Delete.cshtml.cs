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
    public class DeleteModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Pallet Pallet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pallets == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets.FirstOrDefaultAsync(m => m.Id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pallets == null)
            {
                return NotFound();
            }
            var pallet = await _context.Pallets.FindAsync(id);

            if (pallet != null)
            {
                Pallet = pallet;
                _context.Pallets.Remove(Pallet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
