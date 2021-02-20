using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Entities.AdminModel
{
    public class ChangePassword
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ila 40 karakter aralığında olmalıdır.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor, kontrol edin.")]
        public string ConfirmNewPassword { get; set; }
    }
}
