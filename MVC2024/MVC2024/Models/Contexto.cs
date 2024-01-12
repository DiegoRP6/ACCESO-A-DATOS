using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC2024.Models;

namespace MVC2024.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) :base(options)
        {
            
        }

        public DbSet<MarcaModelo> Marcas { get; set; }

        public DbSet<MVC2024.Models.SerieModelo>? SerieModelo { get; set; }

        public DbSet<MVC2024.Models.VehiculoModelo>? VehiculoModelo { get; set; }
    }
}
