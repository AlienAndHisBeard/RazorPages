using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public DeleteModel(RazorPages.Data.ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null) return RedirectToPage("./Index");

            Product = product;
            PhotoHandler.DeletePreviousPhoto(_appEnvironment, Product.ImageData);
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
