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
    
    public partial class textfield
    {
        public textfield()
        {
            this.textfieldresponses = new HashSet<textfieldresponse>();
        }
    
        public int TextFieldID { get; set; }
        public int TextBlastID { get; set; }
    
        public virtual textblast textblast { get; set; }
        public virtual ICollection<textfieldresponse> textfieldresponses { get; set; }
    }
}
