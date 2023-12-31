using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Cations
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Cation Cation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cations == null)
            {
                return NotFound();
            }

            var cation = await _context.Cations.FirstOrDefaultAsync(m => m.Id == id);

            if (cation == null)
            {
                return NotFound();
            }
            else 
            {
                Cation = cation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cations == null)
            {
                return NotFound();
            }
            var cation = await _context.Cations.FindAsync(id);

            if (cation != null)
            {
                Cation = cation;
                _context.Cations.Remove(Cation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
