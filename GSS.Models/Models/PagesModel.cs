using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GSS.Models.Models
{
    public class PagesModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Title  can't be more than 40 characters and less than 5 characters")]
        [DisplayName("Title")]
        public string Tittle { get; set; }
        [DisplayName("Content")]
        [Required(ErrorMessage = "Page Cotent is required")]
        public string PageCotent { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Title  can't be more than 250 characters and less than 5 characters")]
        [DisplayName("Description")]
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public bool IsActive { get; set; }
    }
}
