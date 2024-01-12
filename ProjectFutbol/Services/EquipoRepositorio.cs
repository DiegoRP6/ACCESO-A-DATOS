using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Modelos;

namespace Services
{
	public class EquipoRepositorio : IEquiposRepositorio
    {
        private readonly FutbolDbContext context;
        public EquipoRepositorio(FutbolDbContext context) {
            this.context = context;
        }

		public IEnumerable<Equipo> BuscarEquipos(string elementoABuscar)
		{
			if(string.IsNullOrEmpty(elementoABuscar))
			{
				return context.Equipos;
			}
			return context.Equipos.Where(e => e.nomEquipo.Contains(elementoABuscar) || e.Ciudad.Contains(elementoABuscar));
		}

		public void CambiarFoto(IFormFile photo, int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CategoriaCuantos> equiposPorCategoria()
		{
			return context.Equipos.GroupBy(e => e.categoria).Select(g => new CategoriaCuantos() { categoria = g.Key, cuantos = g.Count() }).ToList();
		}

		public Equipo GetEquipo(int id)
		{
			return context.Equipos.Find(id);
		}

		public IEnumerable<Equipo> GetEquipos()
        {
            return context.Equipos;
        }

		public Equipo InsertarEquipo(Equipo equipoNuevo)
		{
			context.Equipos.Add(equipoNuevo);
			context.SaveChanges();
			return equipoNuevo;
		}

		public void InsertarFoto(IFormFile photo)
		{
		}

		public Equipo Update(Equipo equipoActualizado)
		{
			var equipo = context.Equipos.Attach(equipoActualizado);
			equipo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			context.SaveChanges();
			return equipoActualizado;
		}

		
	}

}
