﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Intel_AirlineReservationService.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IntelAirlineReservationDBEntities : DbContext
    {
        public IntelAirlineReservationDBEntities()
            : base("name=IntelAirlineReservationDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<booking> bookings { get; set; }
        public virtual DbSet<flightClassPrice> flightClassPrices { get; set; }
        public virtual DbSet<flightSchedule> flightSchedules { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<passenger> passengers { get; set; }
        public virtual DbSet<seatingArrangement> seatingArrangements { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
