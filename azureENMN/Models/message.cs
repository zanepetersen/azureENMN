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
    
    public partial class message
    {
        public int MessageID { get; set; }
        public int MessageThreadID { get; set; }
        public System.DateTime DateTime { get; set; }
        public int OrderNo { get; set; }
        public string Text { get; set; }
        public int SenderID { get; set; }
    
        public virtual person person { get; set; }
        public virtual messagethread messagethread { get; set; }
    }
}
