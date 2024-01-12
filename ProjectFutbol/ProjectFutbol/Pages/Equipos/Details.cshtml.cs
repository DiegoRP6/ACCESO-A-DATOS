using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modelos;
using Services;

namespace ProjectFutbol.Pages.Equipos
{
    public class DetailsModel : PageModel
    {
        public Equipo equipo { get; set; }
        private readonly IEquiposRepositorio equiposRepositorio;
        public DetailsModel(IEquiposRepositorio equiposRepositorio)
        {
            this.equiposRepositorio = equiposRepositorio;
        }
        public void OnGet(int id)
        {
            equipo = equiposRepositorio.GetEquipo(id);
        }
    }
}
