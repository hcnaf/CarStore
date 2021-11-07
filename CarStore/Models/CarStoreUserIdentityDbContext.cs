using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Models
{
    public class CarStoreUserIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public CarStoreUserIdentityDbContext(DbContextOptions<CarStoreUserIdentityDbContext> options) : base(options) { }
    }
}
