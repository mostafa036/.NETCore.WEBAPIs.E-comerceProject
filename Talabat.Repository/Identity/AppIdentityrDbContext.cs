﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityrDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityrDbContext(DbContextOptions<AppIdentityrDbContext> options):base(options) 
        {
            
        }
    }
}
