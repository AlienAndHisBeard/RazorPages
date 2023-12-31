using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Pallets
{
    public class EditModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public EditModel(RazorPages.Data.ApplicationDbContext context)
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
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            var pallet =  await _context.Pallets.Include(s => s.Product).FirstOrDefaultAsync(m => m.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }
            Pallet = pallet;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!int.TryParse(Request.Form["Pallet.Product"], out var id)) return Page();
            Pallet.Product = _context.Products.Find(id);

            _context.Attach(Pallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PalletExists(Pallet.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PalletExists(int id)
        {
          return (_context.Pallets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
