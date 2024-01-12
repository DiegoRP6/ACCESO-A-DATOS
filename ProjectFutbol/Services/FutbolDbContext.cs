﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modelos;

namespace Services
{
    public class FutbolDbContext : DbContext
    {
        public FutbolDbContext(DbContextOptions<FutbolDbContext> options) : base(options)
        {
            
        }
        public DbSet<Equipo> Equipos { get; set; }
    }
}
