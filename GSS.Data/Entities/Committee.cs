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
    
    public partial class Committee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Committee()
        {
            this.MembersToCommittees = new HashSet<MembersToCommittee>();
        }
    
        public long Id { get; set; }
        public string ComitteeName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MembersToCommittee> MembersToCommittees { get; set; }
    }
}
