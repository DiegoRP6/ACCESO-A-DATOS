using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC2024.Models;
using static MVC2024.Controllers.VehiculoController;

namespace MVC2024.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) :base(options)
        {
            
        }

        //onModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiculoTotal>(
                eb =>
                {
                    eb.HasNoKey();
                });
        }
        public DbSet<VehiculoTotal> VistaTotal { get; set; }

        public DbSet<MarcaModelo> Marcas { get; set; }

        public DbSet<MVC2024.Models.SerieModelo>? SerieModelo { get; set; }

        public DbSet<MVC2024.Models.VehiculoModelo>? VehiculoModelo { get; set; }

        public DbSet<ExtraModelo> Extras { get; set; }
        public DbSet<VehiculoExtraModelo> VehiculoExtraModelos { get; set; }


    }
}
