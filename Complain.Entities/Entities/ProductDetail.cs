using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class ProductDetail:BaseHome
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        public int UsedTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
