using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaCRUD.Models;
using System.Data;
using System.Diagnostics;

namespace PruebaCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibrosContext context;

        public HomeController(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            string error = TempData["error"]?.ToString();
            ViewBag.Error = error;

            var reservas = context.Reservaciones.Include(x => x.Libro).ToList();
            ViewBag.Libros = context.Libros.Where(x => x.CantidadDisponible > 0).ToList();
            return View(reservas);
        }

        [HttpPost]
        public async Task<IActionResult> AddReserva(Reservacion reserva)
        {
            var libros = await context.Libros.FirstOrDefaultAsync(x => x.IdLibro == reserva.IdLibro);
            if (libros != null && libros.CantidadDisponible > 0)
            {
                try
                {
                    var nombrePersonaParam = new SqlParameter("@NombrePersona", reserva.NombrePersona);
                    var fechaReservaParam = new SqlParameter("@FechaReserva", reserva.FechaReserva);
                    var idLibroParam = new SqlParameter("@IdLibro", reserva.IdLibro);
                    var diasAReservarParams = new SqlParameter("@DiasAReservar", reserva.DiasAReservar);

                    context.Database.ExecuteSqlRaw("exec reservar @NombrePersona, @FechaReserva, @IdLibro, @DiasAReservar",
                        nombrePersonaParam, fechaReservaParam, idLibroParam, diasAReservarParams);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al agregar la reserva." }));
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> EditarReservacion(int id)
        {
            var reserva = context.Reservaciones.FirstOrDefault(x => x.IdReservacion == id);

            ViewBag.Libros = context.Libros.Where(x => x.CantidadDisponible > 0).ToList();

            if (reserva != null)
            {
                return View(reserva);
            }

            return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al editar la reserva." }));
        }

        [HttpPost]
        public async Task<IActionResult> EditarReservacion(Reservacion reservacion)
        {
            try
            {
                context.Reservaciones.Update(reservacion);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al editar la reserva." }));
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EliminarReservacion(int id)
        {
            var reserva = context.Reservaciones.FirstOrDefault(x => x.IdReservacion == id);

            if (reserva != null)
            {
                try
                {
                    context.Reservaciones.Remove(reserva);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new RouteValueDictionary(new { error = "Sucedio un problema al editar la reserva." }));
                }
            }

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}