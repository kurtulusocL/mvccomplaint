using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class OfferCompany:BaseHome
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Description { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Photo { get; set; }
        public bool IsConfirm { get; set; }

        public string UserId { get; set; }
        public int OfferOwnerId { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }

        public virtual OfferOwner OfferOwner { get; set; }
        public virtual Category Category { get; set; }
        public virtual Country Country { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public OfferCompany()
        {
            IsConfirm = false;
        }
    }
}
