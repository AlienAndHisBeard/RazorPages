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
using System.Globalization;
using Microsoft.Extensions.Options;

namespace RazorPages.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public CreateModel(RazorPages.Data.ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;

            Product = new Product();
            Anions = new List<Anion>();
            Cations = new List<Cation>();
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Email");
            ViewData["Anions"] = new SelectList(_context.Anions, "Id", "Name");
            ViewData["Cations"] = new SelectList(_context.Cations, "Id", "Name");

            Cations = await _context.Cations.ToListAsync();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public List<Cation> Cations { get; set; }
        public List<Anion> Anions { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null || Product == null)
            {
                return Page();
            }

            var cationsForm = Request.Form["Cations"];
            var anionsForm = Request.Form["Anions"];

            var phForm = Request.Form["Product.Ph"];
            if(!float.TryParse(phForm, out var phFloat)) return Page();

            var volumeForm = Request.Form["Product.Volume"];
            if(!float.TryParse(volumeForm, out var volume)) return Page();

            Product.ImageData = await PhotoHandler.GetPhoto(_appEnvironment, HttpContext.Request.Form.Files);

            double ions = 0;

            foreach (var cationId in cationsForm)
            {
                if(!int.TryParse(cationId, out int index)) continue;
                var cation = _context.Cations.Find(index);

                if (cation == null) continue;

                Product.Cations.Add(cation);
                ions += cation.Concentration;
            }

            foreach (var anionId in anionsForm)
            {
                if (!int.TryParse(anionId, out int index)) continue;
                var anion = _context.Anions.Find(index);

                if (anion == null) continue;

                Product.Anions.Add(anion);
                ions += anion.Concentration;
            }

            Product.Mineralization = ions switch
            {
                <= 0.05 => "very low mineralised ",
                <= 0.5 => "low mineralised ",
                <= 1.5 => "medium mineralised ",
                _ => "highly mineralised "
            };

            Product.Ph = phFloat;
            Product.Volume = volume;
            _context.Products.Add(Product);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
