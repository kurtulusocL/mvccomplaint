using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class SocialMedia:BaseHome
    {
        [Required]
        public string Facebook { get; set; }

        [Required]
        public string Twitter { get; set; }

        [Required]
        public string Instagram { get; set; }

        [Required]
        public string LinkedIn { get; set; }
    }
}
