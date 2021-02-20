using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class About:BaseHome
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }
        public string Subdetail { get; set; }

        [Required]
        public string Detail { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
    }
}
