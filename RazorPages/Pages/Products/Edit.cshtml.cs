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
using Microsoft.AspNetCore.Hosting;

namespace RazorPages.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        public string? _path;

        public EditModel(RazorPages.Data.ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _path = new string("");
        }

        [BindProperty]
        public Product Product { get; set; }

        public List<Cation> Cations { get; set; }
        public List<Anion> Anions { get; set; }
        public string? FilePath { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product =  await _context.Products
                .Include(p => p.Producer)
                .Include(p => p.Anions)
                .Include(p => p.Cations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _path = product.ImageData.ToCharArray().ToString();
            Product = product;

            FilePath = product.ImageData;
            ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Email");
            ViewData["Anions"] = new SelectList(_context.Anions, "Id", "Name");
            ViewData["Cations"] = new SelectList(_context.Cations, "Id", "Name");

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

            _context.Attach(Product).State = EntityState.Modified;

            if (HttpContext.Request.Form.Files.Any())
            {
                if (PhotoHandler.DeletePreviousPhoto(_appEnvironment, FilePath)) Product.ImageData = "";

                Product.ImageData = await PhotoHandler.GetPhoto(_appEnvironment, HttpContext.Request.Form.Files);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
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

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
