using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class ComplainOwner:BaseHome
    {
        [Required]
        [MinLength(8, ErrorMessage = "En az 8 karakterlik bir isim giriniz.")]
        public string NameSurname { get; set; }

        [Required]
        [Display(Name = "Mail Adresiniz"), EmailAddress(ErrorMessage = "Geçerli Bir Mail Adresi Giriniz")]
        public string MailAddress { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "En az 8 karakterlik bir bölge adı giriniz.")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Telefon Numaranız"), Phone(ErrorMessage = "Geçerli Bir Telefon Numarası Giriniz")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }       

        public ICollection<Product> Products { get; set; }
    }
}
