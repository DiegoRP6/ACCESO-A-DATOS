using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC2024.Models;

namespace MVC2024.Controllers
{
	public class SerieController : Controller
	{
        public Contexto Contexto { get; }
        public SerieController(Contexto contexto)
        {
            Contexto = contexto;
        }
        // GET: SerieController
        public ActionResult Index()
		{
			//devuelve la vista de la tabla de series con la marca tambiien
			return View(Contexto.SerieModelo.Include(x => x.Marca).ToList());
		}


		public ActionResult Listado(int id)
        {
            var seriesDeMarca = Contexto.SerieModelo
										.Include(x => x.Marca)
                                        .Where(s => s.MarcaID == id)
                                        .ToList();

            return View(seriesDeMarca);
        }

        // GET: SerieController/Details/5
        public ActionResult Details(int id)
		{
			return View();
		}

		// GET: SerieController/Create
		public ActionResult Create()
		{
			ViewBag.MarcaID = new SelectList(Contexto.Marcas, "ID", "nomMarca");
			return View();
		}

		// POST: SerieController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(SerieModelo serie)
		{
            //insertar datos obtenidos en la tabla de series
			Contexto.SerieModelo.Add(serie);
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

		// GET: SerieController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: SerieController/Edit/5
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

		// GET: SerieController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: SerieController/Delete/5
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
