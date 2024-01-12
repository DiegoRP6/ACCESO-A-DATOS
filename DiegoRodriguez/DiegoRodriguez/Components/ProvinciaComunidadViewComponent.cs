using DiegoRodriguez.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiegoRodriguez.Components
{
    public class ProvinciaComunidadViewComponent : ViewComponent
    {
        public IProvinciaRepositorio ProvinciaRepositorio { get; set; }
        public ProvinciaComunidadViewComponent(IProvinciaRepositorio ProvinciaRepositorio)
        {
            this.ProvinciaRepositorio = ProvinciaRepositorio;
        }
        public IViewComponentResult Invoke()
        {
            return View(ProvinciaRepositorio.provinciasPorComunidad());
        }

    }
}
