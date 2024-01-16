﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC2024.Models;

namespace MVC2024.Controllers
{
    public class VehiculoController : Controller
    { 
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

        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            return View();
        }

        // POST: VehiculoController/Edit/5
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

        // GET: VehiculoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehiculoController/Delete/5
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
            ViewBag.listaMatriculas = new SelectList(contexto.VehiculoModelo, "Matricula", "Matricula");
            //return view con equals
            return View(contexto.VehiculoModelo.Include(x => x.Serie.Marca).Where(x => x.Matricula.Equals(elementoABuscar)));
        }
    }
}
