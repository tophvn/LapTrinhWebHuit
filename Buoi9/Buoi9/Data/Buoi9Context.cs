using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Buoi9.Models;

namespace Buoi9.Data
{
    public class Buoi9Context : DbContext
    {
        public Buoi9Context (DbContextOptions<Buoi9Context> options)
            : base(options)
        {
        }

        public DbSet<Buoi9.Models.KhachHang> KhachHang { get; set; } = default!;
    }
}
