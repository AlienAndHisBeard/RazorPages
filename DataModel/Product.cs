using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public enum DrinkTypes
    {
        [Description("still")]
        still,
        [Description("carbonated")]
        carbonated
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [StringLength(50, ErrorMessage = "Name is too long")]
        public string? Name { get; set; }

        [Required] private DrinkTypes _type;
        [Required]
        public DrinkTypes Type
        { 
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(DrinkTypes), value))
                    throw new ArgumentException("Not in possible values");
                _type = value;
            }
        }

        [Required]
        public int ProducerId { get; set; }
        public Producer? Producer { get; set; }

        [Range(5, 9, ErrorMessage = "Must be between 5 and 9")]
        public float Ph { get; set; }

        [Required]
        public List<Cation> Cations { get; set; } = new();

        [Required]
        public List<Anion> Anions { get; set; } = new();

        public string? Mineralization { get; set; }

        [Required]
        public string? PackingType { get; set; }

        [Required]
        public float Volume { get; set; }

        public string? ImageData { get; set; }

        public uint AvailableAmount { get; set; }

        public static implicit operator Product(List<Product> v)
        {
            throw new NotImplementedException();
        }
    }
}
