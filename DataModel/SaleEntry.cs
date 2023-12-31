using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class SaleEntry
    {
        [Key]
        public int Id { get; set; }

        public uint Amount { get; set; }
        public Product? Product { get; set; }
    }
}
