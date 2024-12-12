using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Finale_Crud.Models;

namespace Finale_Crud.Data
{
    public class Finale_CrudContext : DbContext
    {
        public Finale_CrudContext (DbContextOptions<Finale_CrudContext> options)
            : base(options)
        {
        }

        public DbSet<Finale_Crud.Models.Student> Student { get; set; } = default!;
    }
}
