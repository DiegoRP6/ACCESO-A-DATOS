using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Modelos;
using Services;

namespace ProjectFutbol.Pages.Equipos
{
    public class EditModel : PageModel
    {
        private readonly IEquiposRepositorio equipoRepositorio;
        public Equipo equipo { get; set; }

        public IFormFile Photo { get; set; }

        public EditModel(IEquiposRepositorio equipoRepositorio)
        {
            this.equipoRepositorio = equipoRepositorio;
        }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
				equipo = equipoRepositorio.GetEquipo(id.Value);
			}
			else
            {
				equipo = new Equipo();
			}
            return Page();
        }

        //Metodo onpost que guarde los cambios en el objeto equipo
        public IActionResult OnPost(Equipo equipo)
        {
            if (equipo.Id > 0)
            {
                equipoRepositorio.Update(equipo);
            }
			else
            {
				equipoRepositorio.InsertarEquipo(equipo);
			}
            return RedirectToPage("./Index");
		}

    }
}
