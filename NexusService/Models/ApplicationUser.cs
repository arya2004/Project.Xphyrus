﻿using Azure;
using Microsoft.AspNetCore.Identity;

namespace Xphyrus.NexusService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? WebsiteUrl { get; set; }
        public List<Nexus>? Nexus { get; }
      
    }
}
