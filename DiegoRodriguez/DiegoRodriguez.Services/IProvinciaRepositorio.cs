using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiegoRodriguez.Modelos;

namespace DiegoRodriguez.Services
{
	public interface IProvinciaRepositorio
	{
		//Metodo que obtenga todas las provincias

	IEnumerable<Provincia> getProvincias();

		//Metodo busqueda de provincias por comunidad
		IEnumerable<Provincia> BuscarProvinciasPorComunidad(Comunidad elementoABuscar);

		//Metodo para el component
        IEnumerable<ComunidadCuantos> provinciasPorComunidad();



    }
}
