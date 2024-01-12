using DiegoRodriguez.Modelos;
using DiegoRodriguez.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiegoRodriguez.Pages.Provincias
{
    public class IndexModel : PageModel
    {
		private readonly IProvinciaRepositorio ProvinciaRepositorio;
		public IEnumerable<Provincia> Provincias;

		[BindProperty(SupportsGet = true)]
		public Comunidad elementoABuscar { get; set; }

		public IndexModel(IProvinciaRepositorio ProvinciaRepositorio)
		{
			this.ProvinciaRepositorio = ProvinciaRepositorio;
		}

		public void OnGet()
        {
			Provincias = ProvinciaRepositorio.BuscarProvinciasPorComunidad(elementoABuscar);
        }
    }
}
