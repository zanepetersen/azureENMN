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
    
    public partial class forumthread
    {
        public forumthread()
        {
            this.forumposts = new HashSet<forumpost>();
        }
    
        public int ForumThreadID { get; set; }
        public int Creator { get; set; }
        public string Title { get; set; }
    
        public virtual ICollection<forumpost> forumposts { get; set; }
        public virtual person person { get; set; }
    }
}