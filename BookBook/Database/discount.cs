//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookBook.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class discount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public discount()
        {
            this.bill_detail = new HashSet<bill_detail>();
            this.order_detail = new HashSet<order_detail>();
        }
    
        public int id { get; set; }
        public double discount_percent { get; set; }
        public int quantity { get; set; }
        public Nullable<System.DateTime> datevalid { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public string createuser { get; set; }
        public Nullable<System.DateTime> alterdate { get; set; }
        public string alteruser { get; set; }
        public int status { get; set; }
        public string name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bill_detail> bill_detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_detail> order_detail { get; set; }
    }
}
