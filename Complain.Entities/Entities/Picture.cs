using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.Entities
{
    public class Picture:BaseHome
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Photo { get; set; }
        public bool IsConfirm { get; set; }

        public int? ProductId { get; set; }
        public string UserId { get; set; }       

        public virtual Product Product { get; set; }

        public Picture()
        {
            IsConfirm = false;
        }
    }
}
