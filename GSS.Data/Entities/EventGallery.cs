//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventGallery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventGallery()
        {
            this.EventImages = new HashSet<EventImage>();
        }
    
        public long Id { get; set; }
        public string EventName { get; set; }
        public string ThumbnailImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventImage> EventImages { get; set; }
    }
}
