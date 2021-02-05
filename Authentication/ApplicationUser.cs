using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using SoTelAPI.Models;

namespace SoTelAPI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Post> Posts { get; set; }

        public string FullName { get; set; }
        public string Tel { get; set; }
        public string Room { get; set; }
    }
}
