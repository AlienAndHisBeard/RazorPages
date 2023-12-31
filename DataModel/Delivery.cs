using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [StringLength(50, ErrorMessage = "Name is too long")]
        public string? UserName { get; set; }

        public Producer? Supplier { get; set; }

        public List<Pallet> Pallets { get; set; } = new();
    }
}
