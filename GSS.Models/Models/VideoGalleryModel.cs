using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.Models.Models
{
    public class VideoGalleryModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Tittle is required")]
        [DisplayName("Tittle")]
        public string Tittle { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please provide video url")]
        [DisplayName("Video URL")]
        public string VideoURL { get; set; }
    }
}
