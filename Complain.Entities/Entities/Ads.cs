﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class Ads:BaseHome
    {
        [Required]
        public string CompanyName { get; set; }
        public string Title { get; set; }

        [Required]
        public string WebSite { get; set; }
        public string Photo { get; set; }
    }
}
