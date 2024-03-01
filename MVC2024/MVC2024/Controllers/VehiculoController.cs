using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
            // Incluye los datos de la Serie y la Marca relacionadas con cada VehiculoModelo
            var vehiculos = contexto.VehiculoModelo
                                   .Include(x => x.Serie)
                                   .Include(x => x.Serie.Marca)
                                   .Include(x => x.Carroceria)
                                   .ToList();

            // Carga los datos de los extras de vehículos para cada VehiculoModelo
            var vehiculosConExtras = contexto.VehiculoModelo
                                            .Include(v => v.VehiculoExtras)
                                                .ThenInclude(ve => ve.Extra)
                                            .ToList();

            return View(vehiculos);
        }

        public ActionResult Listado2()
        {   //esto es para llamar a una vista creada en SQL Management
            var lista = contexto.VistaTotal.FromSql($"SELECT dbo.Marcas.nomMarca, dbo.SerieModelo.nomSerie, dbo.VehiculoModelo.Matricula, dbo.VehiculoModelo.Color\r\nFROM   dbo.Marcas INNER JOIN\r\n             dbo.SerieModelo ON dbo.Marcas.ID = dbo.SerieModelo.MarcaID INNER JOIN\r\n             dbo.VehiculoModelo ON dbo.SerieModelo.ID = dbo.VehiculoModelo.SerieID");
            /*hacemos una consulta a la base de datos para obtener los datos de los vehiculos de la vista creada en SQL Management
			return View(Contexto.vistaTotal.ToList());*/
            return View(lista);
        }

        public ActionResult ListWithProcedureAndParameter(string color = "%")
        {

            var elColor = new SqlParameter("@ColorSel", color);

            //este viewbag es para mostrar el color del vehiculo en el formulario
            /* Esta es la forma de hacerlo de Agustin
			 * ViewBag.color = new SelectList(Contexto.Vehiculo.Select(x => x.Color).Distinct(), "Color", "Color");*/
            ViewBag.color = new SelectList(contexto.VehiculoModelo.Select(x => x.Color).Distinct().ToList());

            //el parametro es el color del vehiculo, el % es para que muestre todos los colores
            return View(contexto.VistaTotal.FromSql($"EXECUTE getVehiculosPorColor {elColor}"));
        }        //-------------------------------------------------------------

        public ActionResult ListWithProcedure()
        {
            //esto es para llamar a un procedimiento almacenado
            return View(contexto.VistaTotal.FromSql($"EXECUTE getseriesVehiculos"));
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
            ViewBag.VehiculoExtra = new MultiSelectList(contexto.Extras, "Id", "NomExtra");

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

            foreach (int xtraId in vehiculo.ExtrasSeleccionados)
            {
                var obj = new VehiculoExtraModelo()
                {
                    ExtraId = xtraId,
                    VehiculoId = vehiculo.Id
                };
                contexto.VehiculoExtraModelos.Add(obj);
            }
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
            VehiculoModelo vehiculo = contexto.VehiculoModelo.Find(id);


            vehiculo.ExtrasSeleccionados = contexto.VehiculoExtraModelos.Where(x => x.VehiculoId == id).Select(x => x.ExtraId).ToList();
            ViewBag.VehiculoExtra = new MultiSelectList(contexto.Extras, "Id", "NomExtra");


            return View(vehiculo);
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

            var ExtrasActuales = contexto.VehiculoExtraModelos.Where(x => x.VehiculoId == id);

            // Limpiamos los extras seleccionados existentes del vehículo
            foreach (var extraAEliminar in ExtrasActuales)
            {
                contexto.VehiculoExtraModelos.Remove(extraAEliminar);
            }

            // Agregamos los nuevos extras seleccionados al vehículo
            foreach (var extraId in cocheModificado.ExtrasSeleccionados)
            {
                cocheAnterior.VehiculoExtras.Add(new VehiculoExtraModelo { ExtraId = extraId });
            }

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
