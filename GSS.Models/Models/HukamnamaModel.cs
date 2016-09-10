using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.Models.Models
{
    public class HukamnamaModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Hukamnama Image is required")]
        public string HukamnamImage { get; set; }
        [Required(ErrorMessage = "Tittle is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Tittle can't be more than 20 characters ")]
        public string Tittle { get; set; }
        public DateTime CratedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
