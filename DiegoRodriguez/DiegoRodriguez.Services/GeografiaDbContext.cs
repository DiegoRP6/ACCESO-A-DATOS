using DiegoRodriguez.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DiegoRodriguez.Services
{
	public class GeografiaDbContext : DbContext
	{

		public GeografiaDbContext(DbContextOptions<GeografiaDbContext> options) : base(options)
		{
		}

		public DbSet<Provincia> Provincias { get; set; }

	}
}