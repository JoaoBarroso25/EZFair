using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EZFair.Class;

namespace EZFair.Data
{
    public class EZFairContext : DbContext
    {
        public EZFairContext (DbContextOptions<EZFairContext> options)
            : base(options)
        {
        }

        public DbSet<EZFair.Class.Cliente> Cliente { get; set; } = default!;
    }
}
