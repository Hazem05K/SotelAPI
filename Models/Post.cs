using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SoTelAPI.Authentication;

namespace SoTelAPI.Models
{
    public class Post
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created_at { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    
    }
}
