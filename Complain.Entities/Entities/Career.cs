using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class Career:BaseHome
    {
        public string IdentityNo { get; set; }

        public string PassportNo { get; set; }

        [Required]
        public string NameSurname { get; set; }

        [Required]
        public string ShortIntroduction { get; set; }

        public DateTime Birthdate { get; set; }

        [Required]
        public string Nationality { get; set; }

        public string Gender { get; set; }

        [Required]
        public string UniversityName { get; set; }

        [Required]
        public string DepartmanName { get; set; }

        [Required]
        public string KnowingLenguage { get; set; }

        [Required]
        public string MailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string LinkedIn { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Folder { get; set; }
    }
}
