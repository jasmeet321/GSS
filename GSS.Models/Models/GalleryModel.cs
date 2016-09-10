using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.Models.Models
{
     

    public class GalleryModel
    {
         public int Id { get; set; }

         [Required(ErrorMessage = "Image Name is required")]
         [StringLength(20, MinimumLength = 1, ErrorMessage = "Image Name can't be more than 20 characters ")]
         [DisplayName("Image Name")]
         public string ImageName { get; set; }
         
         public string ImagePath { get; set; }

         [Required(ErrorMessage = "Tittle is required")]
         [StringLength(20, MinimumLength = 1, ErrorMessage = "Tittle can't be more than 20 characters ")]
         [DisplayName("Tittle")]
         public string Title { get; set; }

         [Required(ErrorMessage = "Description is required")]
         [StringLength(250, MinimumLength = 10, ErrorMessage = "Description can't be more than 250 characters and less than 10 characters")]
         [DisplayName("Description")]
         public string Description { get; set; }

         public DateTime CreateDate { get; set; }

         public DateTime ModifiedDate { get; set; }

         public bool IsActivated { get; set; }

         public bool IsBannerImage { get; set; }

         public bool IsDefault { get; set; }

         public long EventId { get; set; }

         public string EventName { get; set; }
    }


    public class LiveStream
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Live Stream Link is required")]

        [DisplayName("Live Stream Link")]
        public string LiveStreamLink { get; set; }
    }
   
}
