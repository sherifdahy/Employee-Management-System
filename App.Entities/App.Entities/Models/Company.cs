﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TaxRegistrationNumber { get; set; }
        public string? TaxFileNumber { get; set; }
        public string? EntityType { get; set; }
        public string? TaxOfficeName { get; set; }
        public virtual ICollection<Owner> Owners { get; set; } = new HashSet<Owner>();
        public string? Address { get; set; }
        public virtual ICollection<Email> Emails { get; set; } = new HashSet<Email>();
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();

    }
}
