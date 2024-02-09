using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC2024.Models;

namespace MVC2024.Controllers
{
    public class VehiculoController : Controller
    { 

        public class VehiculoTotal
        {
            public string nomMarca { get; set; }
            public string nomSerie { get; set; }
            public string Matricula { get; set; }
            public string Color { get; set; }


        }


        public Contexto contexto { get; }

        public VehiculoController(Contexto contexto)
        {
            this.contexto = contexto;
        }
        // GET: VehiculoController
        public ActionResult Index()
        {

            return View(contexto.VehiculoModelo.Include(x => x.Serie.Marca).ToList());
        }

        public ActionResult Listado2()
        {
            List<VehiculoTotal> lista = contexto.VistaTotal.ToList();
            return View(lista); //devuelve la vista
        }

        public ActionResult Seleccion(int marcaId = 1, int serieId = 0)
        {
            ViewBag.lasMarcas = new SelectList(contexto.Marcas, "ID", "nomMarca", marcaId);
            ViewBag.lasSeries = new SelectList(contexto.SerieModelo.Where(x => x.MarcaID == marcaId), "ID", "nomSerie", serieId);

            //devuelve los vehiculos que contienen la serie seleccionada
            return View(contexto.VehiculoModelo.Include(x => x.Serie.Marca).Where(x => x.SerieID == serieId).ToList());
        }


        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {

            var vehiculoModelo = contexto.VehiculoModelo
                                           .Include("Serie.Marca") // Reemplaza "PropiedadRelacionada" con el nombre real de la propiedad de navegación
                                           .FirstOrDefault(v => v.Id == id);
            return View(vehiculoModelo);
        }

        // GET: VehiculoController/Create
        public ActionResult Create()
        {
            ViewBag.serieID = new SelectList(contexto.SerieModelo, "ID", "nomSerie");
            return View();
        }

        // POST: VehiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehiculoModelo vehiculo)
        {
            contexto.VehiculoModelo.Add(vehiculo);
            contexto.Database.EnsureCreated();
            contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehiculoController/Edit/5
		public ActionResult Edit(int id)
		{
			ViewBag.SerieId = new SelectList(contexto.SerieModelo, "ID", "nomSerie");
			return View(contexto.VehiculoModelo.Find(id));
		}

        // POST: VehiculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehiculoModelo cocheModificado)
        {
            VehiculoModelo cocheAnterior = contexto.VehiculoModelo.Find(id);
            cocheAnterior.Matricula = cocheModificado.Matricula;
            cocheAnterior.SerieID = cocheModificado.SerieID;
            cocheAnterior.Color = cocheModificado.Color;
            contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehiculoController/Delete/5
        public ActionResult Delete(int id)
        {

            var vehiculoModelo = contexto.VehiculoModelo
                               .Include("Serie.Marca") // Reemplaza "PropiedadRelacionada" con el nombre real de la propiedad de navegación
                               .FirstOrDefault(v => v.Id == id);
            return View(vehiculoModelo);
        }

        // POST: VehiculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {

            contexto.VehiculoModelo.Remove(contexto.VehiculoModelo.Find(id));
            contexto.SaveChanges();
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Busqueda(String elementoABuscar = "")
        {
         //así Agustín
        ViewBag.buscar = elementoABuscar;
        var lista = from v in contexto.VehiculoModelo where (v.Matricula.Contains(elementoABuscar)) select v;
        return View(lista.Include(x => x.Serie).ToList());

        // asi copilot    return View(contexto.VehiculoModelo.Include(x => x.Serie.Marca).Where(x => x.Matricula.Contains(elementoABuscar)).ToList());
        }

        public ActionResult Busqueda2(String elementoABuscar = "")
        {
            //viewBag que cargue las matriculas del atributo de vehiculo
            ViewBag.listaMatriculas = new SelectList(contexto.VehiculoModelo, "Matricula", "Matricula", elementoABuscar);  //El cuarto parámetro es para que se quede ese seleccionado
            //return view con equals
            return View(contexto.VehiculoModelo.Include(x => x.Serie.Marca).Where(x => x.Matricula.Equals(elementoABuscar)));
        }
    }
}
