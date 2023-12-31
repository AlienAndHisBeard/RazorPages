using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.SaleEntries
{
    public class CreateModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public CreateModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
            SaleEntry = new SaleEntry();

        }

        public IActionResult OnGet()
        {
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public SaleEntry SaleEntry { get; set; }
        public string ProductId { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.SaleEntries == null || SaleEntry == null) return Page();

            
            if (!int.TryParse(Request.Form["SaleEntry.Product"], out var id)) return Page();
            SaleEntry.Product = _context.Products.Find(id);

            _context.SaleEntries.Add(SaleEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
