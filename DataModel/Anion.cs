using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Anion : IIon
    {
        [Key]
        public int Id { get; init; }
        [Required]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [StringLength(50, ErrorMessage = "Name is too long")]
        public string? Name { get; set; }
        [Required]
        [RegularExpression(@"^.{2,}$", ErrorMessage = "Minimum 2 characters required")]
        [StringLength(7, ErrorMessage = "Symbol is too long")]
        public string? Symbol { get; set; }

        //Anions concentration e.g 1.55E+2
        [Required]
        [DisplayFormat(DataFormatString = "{0:E2}", ApplyFormatInEditMode = true)]
        public double Concentration { get; set; }

        public List<Product> Products { get; } = new();
    }
}
