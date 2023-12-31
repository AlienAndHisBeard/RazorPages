using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using RazorPages.Data;

namespace RazorPages.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPages.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public DetailsModel(RazorPages.Data.ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

      public Product Product { get; set; }
      public List<string> Anions { get; set; }
      public List<string> Cations { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Producer)
                .Include(p => p.Anions)
                .Include(p => p.Cations)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            Product = product;
            Anions = product.Anions.ConvertAll(input => input.Name + ", " + input.Symbol + ", " + input.Concentration);
            Cations = product.Cations.ConvertAll(input => input.Name + ", " + input.Symbol + ", " + input.Concentration);

            return Page();
        }
    }
}
