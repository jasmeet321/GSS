using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using GSS.Models;

namespace GSS.Models.Models
{
    public  class EventGalleryModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Image Name can't be more than 50 characters ")]
        [DisplayName("Event Name")]
        public string EventName { get; set; }

        
        [DisplayName("Thumbnail Name")]
        public string ThumbnailImage { get; set; }

        public List<EventImageModel> Images { get; set; }

        public HttpPostedFileBase[] ImagesBases { get; set; }
    }
}
