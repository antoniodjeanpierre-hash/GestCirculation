using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestCirculation.Models
{
    public class GestCirculationContext:IdentityDbContext<IdentityUser>
    
    {
        public GestCirculationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet <Conducteur> Conducteur { get; set; }
        public DbSet <Contravention> Contravention { get; set; }
        public DbSet <Agent> Agent { get; set; }  
    }
}