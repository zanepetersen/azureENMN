﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class enmndbEntities : DbContext
    {
        public enmndbEntities()
            : base("name=enmndbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<forumpost> forumposts { get; set; }
        public virtual DbSet<forumthread> forumthreads { get; set; }
        public virtual DbSet<group> groups { get; set; }
        public virtual DbSet<groupmember> groupmembers { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<messagethread> messagethreads { get; set; }
        public virtual DbSet<mother> mothers { get; set; }
        public virtual DbSet<person> people { get; set; }
        public virtual DbSet<picture> pictures { get; set; }
        public virtual DbSet<session> sessions { get; set; }
        public virtual DbSet<textblast> textblasts { get; set; }
        public virtual DbSet<textfield> textfields { get; set; }
        public virtual DbSet<textfieldresponse> textfieldresponses { get; set; }
        public virtual DbSet<textview> textviews { get; set; }
    }
}