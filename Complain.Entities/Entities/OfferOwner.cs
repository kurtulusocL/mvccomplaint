using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class OfferOwner:BaseHome
    {       
        public string NameSurname { get; set; }
        public string NickName { get; set; }
        public int GoTime { get; set; }
        public string MailAddress { get; set; }

        public ICollection<OfferCompany> OfferCompanies { get; set; }
    }
}
