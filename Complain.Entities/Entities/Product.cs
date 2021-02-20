using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{    
    public class Product : BaseHome
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string ProductName { get; set; }
        public string Subject { get; set; }

        [Required]
        public string Detail { get; set; }
        public bool IsConfirm { get; set; }

        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int ProductDetailId { get; set; }
        public int ComplainOwnerId { get; set; }
        public int CountryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
        public virtual ComplainOwner ComplainOwner { get; set; }
        public virtual Country Country { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Picture> Pictures { get; set; }

        public Product()
        {
            IsConfirm = false;
        }
    }
}

