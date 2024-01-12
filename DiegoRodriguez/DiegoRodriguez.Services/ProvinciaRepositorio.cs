using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiegoRodriguez.Modelos;

namespace DiegoRodriguez.Services
{
	public class ProvinciaRepositorio : IProvinciaRepositorio
	{
		private readonly GeografiaDbContext context;
		public ProvinciaRepositorio(GeografiaDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<Provincia> BuscarProvinciasPorComunidad(Comunidad elementoABuscar)
		{
			IEnumerable<Provincia> provincias = context.Provincias;
				provincias = provincias.Where(p => p.codComunidad == elementoABuscar+1);
			return provincias;
		}

		public IEnumerable<Provincia> getProvincias()
		{
			return context.Provincias.ToList();
		}

        public IEnumerable<ComunidadCuantos> provinciasPorComunidad()
        {
			var provinciasPorComunidad = context.Provincias.GroupBy(p => p.codComunidad)
                .Select(g => new ComunidadCuantos
				{
                    Comunidad = g.First().codComunidad -1,
                    superficie = g.Sum(p => p.superficie),
                    numHabitantes = g.Sum(p => p.numHabitantes),
                    numProvincias = g.Count()
                });
            return provinciasPorComunidad;
        }
    }
}
