using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaCRUD.Models;
using System.Diagnostics;

namespace PruebaCRUD.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibrosContext context;

        public LibrosController(LibrosContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var libros = new List<Libro>();
            try
            {
                string error = TempData["error"]?.ToString();
                ViewBag.Error = error;

                libros = context.Libros.ToList();
            }
            catch(Exception ex)
            {
                return View(libros);
            }

            return View(libros);
        }

        [HttpPost]
        public async Task<IActionResult> AddLibro(Libro libro)
        {
            try
            {
                context.Libros.Add(libro);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al agregar el libro." }));
            }

            return RedirectToAction("Index"); 
           
        }

        public async Task<IActionResult> EditarLibro(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.IdLibro == id);

            if (libro != null)
            {
                return View(libro);
            }

            return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al editar el libro." }));
        }

        [HttpPost]
        public async Task<IActionResult> EditarLibro(Libro libro)
        {
            try
            {
                context.Libros.Update(libro);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al editar el libro." }));
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EliminarLibro(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.IdLibro == id);

            if (libro != null)
            {
                try
                {
                    context.Libros.Remove(libro);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al eliminar el libro." }));
                }
            }

            return RedirectToAction("Index");
        }
    }
}
