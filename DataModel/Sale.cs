using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [StringLength(50, ErrorMessage = "Name is too long")]
        public string? UserName { get; set; }

        public List<SaleEntry> SaleEntries { get; set; } = new();

        [Required]
        public DateTime? Date { get; set; }
    }
}
