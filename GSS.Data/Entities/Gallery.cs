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
    
    public partial class Gallery
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ImagePath { get; set; }
        public bool IsActivated { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool> IsBannerImage { get; set; }
        public Nullable<long> EventId { get; set; }
    
        public virtual NewsEvent NewsEvent { get; set; }
    }
}