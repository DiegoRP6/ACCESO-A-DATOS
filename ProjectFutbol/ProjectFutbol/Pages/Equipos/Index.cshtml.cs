using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modelos;
using Services;

namespace ProjectFutbol.Pages.Equipos
{
    public class IndexModel : PageModel
    {
        private readonly IEquiposRepositorio equipoRepositorio;
        public IEnumerable<Equipo> Equipos;
        public Equipo equipo;
        [BindProperty(SupportsGet = true)]
        public string elementoABuscar { get; set; }
        public IndexModel(IEquiposRepositorio equipoRepositorio)
        {
            this.equipoRepositorio = equipoRepositorio;
        }
        public void OnGet()
        {
            Equipos = equipoRepositorio.BuscarEquipos(elementoABuscar);

		}
    }
}
