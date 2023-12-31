using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;

namespace RazorPages.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public CreateModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;

            SaleEntries = new List<SaleEntry>();
        }

        public IActionResult OnGet()
        {
            ViewData["SaleEntries"] = new SelectList(_context.SaleEntries.Include(p => p.Product), "Id", "Product.Name");

            return Page();
        }

        [BindProperty]
        public Sale Sale { get; set; } = default!;

        public List<SaleEntry> SaleEntries { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Sales == null || Sale == null) return Page();

            var saleEntries = Request.Form["SaleEntries"];

            foreach (var saleEntryId in saleEntries)
            {
                if(!int.TryParse(saleEntryId, out int index)) continue;
                var entry = _context.SaleEntries.Include(p => p.Product).FirstOrDefault(p => p.Id == index);

                if (entry == null) continue;

                if (entry.Amount > entry.Product.AvailableAmount) continue;

                entry.Product.AvailableAmount -= entry.Amount;

                _context.Attach(entry.Product).State = EntityState.Modified;

                Sale.SaleEntries.Add(entry);
            }
            if (Sale.SaleEntries.Count == 0) return Page();

            _context.Sales.Add(Sale);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
