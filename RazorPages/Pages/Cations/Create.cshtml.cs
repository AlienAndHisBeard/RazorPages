using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Cations
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
        public Cation Cation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Cations == null || Cation == null)
            {
                return Page();
            }

            var concentrationString = Request.Form["Cation.Concentration"];
            var parsed = double.TryParse(concentrationString, out var concentrationResult);

            if (!parsed) return Page();

            Cation.Concentration = concentrationResult;

            _context.Cations.Add(Cation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
