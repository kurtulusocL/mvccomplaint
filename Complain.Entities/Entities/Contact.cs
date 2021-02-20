using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class Contact:BaseHome
    {
        [Required]
        [MinLength(8, ErrorMessage = "En az 8 karakterlik bir isim giriniz.")]
        public string NameSurname { get; set; }

        [Required]
        [Display(Name = "Mail Adresiniz"), EmailAddress(ErrorMessage = "Geçerli Bir Mail Adresi Giriniz")]
        public string MailAddress { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "En az 20 karakterlik bir isim konu başlığı yazınız.")]
        public string Subject { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "En az 50 karakterlik bir mesaj bırakınız."), MaxLength(250, ErrorMessage = "En fazla 250 karakterlik bir mesaj bırakabilirsiniz.")]
        public string Text { get; set; }
    }
}
