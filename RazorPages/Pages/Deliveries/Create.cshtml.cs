using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataModel;
using RazorPages.Data;
using Microsoft.EntityFrameworkCore;

namespace RazorPages.Pages.Deliveries
{
    public class CreateModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;

        public CreateModel(RazorPages.Data.ApplicationDbContext context)
        {
            _context = context;

            Pallets = new List<Pallet>();
        }

        public IActionResult OnGet()
        {
            ViewData["Pallets"] = new SelectList(_context.Pallets, "Id", "Id");
            ViewData["Suppliers"] = new SelectList(_context.Producers, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Delivery Delivery { get; set; } = default!;
        public List<Pallet> Pallets { get; set; }
        public int SupplierId { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Deliveries == null || Delivery == null)
            {
                return Page();
            }

            var pallets = Request.Form["Pallets"];

            foreach (var saleEntryId in pallets)
            {
                if (!int.TryParse(saleEntryId, out int index)) continue;
                var entry = _context.Pallets.Include(p => p.Product).FirstOrDefault(p => p.Id == index);

                if (entry == null) continue;

                entry.Product.AvailableAmount += entry.Amount;

                _context.Attach(entry.Product).State = EntityState.Modified;

                Delivery.Pallets.Add(entry);
            }

            Delivery.Supplier = _context.Producers.Find(SupplierId);

            _context.Deliveries.Add(Delivery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
