//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace azureENMN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class group
    {
        public group()
        {
            this.groupmembers = new HashSet<groupmember>();
            this.textblasts = new HashSet<textblast>();
        }
    
        public int GroupID { get; set; }
        public string Name { get; set; }
        public bool isDeleted { get; set; }
    
        public virtual ICollection<groupmember> groupmembers { get; set; }
        public virtual ICollection<textblast> textblasts { get; set; }
    }
}