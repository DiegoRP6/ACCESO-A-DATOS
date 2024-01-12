using Microsoft.AspNetCore.Mvc;
using Services;

namespace ProjectFutbol.Components
{
	public class EquipoCategoriaViewComponent : ViewComponent
	{
        public IEquiposRepositorio EquipoRepositorio { get; set; }  
        public EquipoCategoriaViewComponent(IEquiposRepositorio equipoRepositorio)
        {
            EquipoRepositorio = equipoRepositorio;
        }

        public IViewComponentResult Invoke()
        {
            var resultado = EquipoRepositorio.equiposPorCategoria();
			return View(resultado);
		}
    }
}
