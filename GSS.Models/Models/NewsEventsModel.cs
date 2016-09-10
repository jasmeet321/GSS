using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GSS.Models.Models
{

    [MetadataType(typeof(NewsEventsModel))]
    public class NewsEventsModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Tittle is required")]
        [DisplayName("Tittle")]
        public string HeadLine { get; set; }
       
        public string Description { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Event Date is required")]
        [DisplayName("Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Place of event is required")]
        [DisplayName("Event Place")]
        public string PlaceofEvent { get; set; }

        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<GalleryModel> Images { get; set; }

        public GalleryModel DefaultImage { get; set; }

        public HttpPostedFileBase[] ImagesBases { get; set; }

    }
}
