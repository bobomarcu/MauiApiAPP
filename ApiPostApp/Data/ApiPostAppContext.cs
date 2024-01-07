using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApplication.Models;

namespace ApiPostApp.Data
{
    public class ApiPostAppContext : DbContext
    {
        public ApiPostAppContext (DbContextOptions<ApiPostAppContext> options)
            : base(options)
        {
        }

        public DbSet<PostApplication.Models.Package> Package { get; set; } = default!;
    }
}
