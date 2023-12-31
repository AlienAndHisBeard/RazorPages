using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Producers
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Producer Producer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Producers == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers.FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }
            else 
            {
                Producer = producer;
            }
            return Page();
        }
    }
}
