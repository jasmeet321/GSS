using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.Models.Models
{
    public class EventImageModel
    {
        public long Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public long EventGalleryId { get; set; }
    
    }
}
