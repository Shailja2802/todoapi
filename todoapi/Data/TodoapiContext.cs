using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoapi;

namespace todoapi.Data
{
    public class TodoapiContext : DbContext
    {
        public TodoapiContext (DbContextOptions<TodoapiContext> options)
            : base(options)
        {
        }

        public DbSet<todoapi.Todo> Todo { get; set; }
    }
}
