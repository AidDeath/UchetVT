using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows;
using UchetVT;

namespace UchetVT
{ 
    public class UchetVTContext : DbContext
    {
        public UchetVTContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<BookBoard> Board { get; set; }
        public DbSet<BookCPU> CPU { get; set; }
        public DbSet<BookHDD> HDD { get; set; }
        public DbSet<BookLicense> License { get; set; }
        public DbSet<BookNetworkDevice> NetworkDevice { get; set; }
        public DbSet<BookOS> OS { get; set; }
        public DbSet<BookPrinter> Printer { get; set; }
        public DbSet<BookRegion> Region { get; set; }
        public DbSet<BookUPS> UPS { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
