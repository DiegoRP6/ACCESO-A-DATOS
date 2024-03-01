using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC2024.Models;

namespace MVC2024.Controllers
{
    public class CarroceriaController : Controller
    {
        public Contexto Contexto { get; }
        public CarroceriaController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: CarroceriaController
        public ActionResult Index()
        {
            return View(
                //se le pasa una lista

                Contexto.CarroceriaModelo.ToList());
        }

        // GET: CarroceriaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarroceriaController/Create
        public ActionResult Create()
        {
            ViewBag.CarroceriaID = new SelectList(Contexto.CarroceriaModelo, "Id", "Nombre");
            return View();
        }

        // POST: CarroceriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarroceriaModelo carroceria)
        {

            //insertar datos obtenidos en la tabla de series
            Contexto.CarroceriaModelo.Add(carroceria);
            Contexto.Database.EnsureCreated();
            Contexto.SaveChanges();

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarroceriaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarroceriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarroceriaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarroceriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
