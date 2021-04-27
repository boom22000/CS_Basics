using Microsoft.EntityFrameworkCore;
using RealTimeEconomy.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeEconomy.Data
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        { 

        }
        public DbSet<Currency> Currency { get; set; }
    }
}
