using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class Category : BaseHome
    {
        [Required]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        public string Photo { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<OfferCompany> OfferCompanies { get; set; }
    }
}
