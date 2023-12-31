using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Anions
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Anion Anion { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Anions == null)
            {
                return NotFound();
            }

            var anion = await _context.Anions.FirstOrDefaultAsync(m => m.Id == id);
            if (anion == null)
            {
                return NotFound();
            }
            else 
            {
                Anion = anion;
            }
            return Page();
        }
    }
}
