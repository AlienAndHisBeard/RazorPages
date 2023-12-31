using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Anions
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
            return Page();
        }

        [BindProperty]
        public Anion Anion { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Anions == null || Anion == null)
            {
                return Page();
            }

            var concentrationString = Request.Form["Anion.Concentration"];
            var parsed = double.TryParse(concentrationString, out var concentrationResult);

            if (!parsed) return Page();

            Anion.Concentration = concentrationResult;
            _context.Anions.Add(Anion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
