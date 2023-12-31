using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Pallets
{
    public class CreateModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public CreateModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Pallet Pallet { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Pallets == null || Pallet == null)
            {
                return Page();
            }

            if (!int.TryParse(Request.Form["Pallet.Product"], out var id)) return Page();
            Pallet.Product = _context.Products.Find(id);

            _context.Pallets.Add(Pallet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
