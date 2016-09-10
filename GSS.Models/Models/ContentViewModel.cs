using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSS.Models.Models;


namespace GSS.Models.Models
{
    public class ContentViewModel
    {
        public PagesModel page { get; set; }
        public List<NewsEventsModel> newsevents { get; set; }
        public List<GalleryModel> gallery { get; set; }
       
    }
}
