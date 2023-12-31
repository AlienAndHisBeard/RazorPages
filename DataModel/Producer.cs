using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Producer
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(?<!\w)(\(?(\+|00)?48\)?)?[ -]?\d{3}[ -]?\d{3}[ -]?\d{3}(?!\w)", ErrorMessage = "You mast provide proper phone number")]
        public string? PhoneNumber { get; set; }
    }
}
