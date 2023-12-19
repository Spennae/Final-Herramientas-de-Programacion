
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalHerr.Models;
using FinalHerr.Services;
using Microsoft.AspNetCore.Authorization;

namespace FinalHerr.Controllers
{
    [Authorize(Roles = "SysAdmin,Secretaria")]
    public class ProfesoresController : Controller
    {
        private readonly IProfesorService _profesorService;

        public ProfesoresController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        public IActionResult Index(string Filtro)
        {
            var profesores = _profesorService.ObtenerTodos();

            if (!string.IsNullOrEmpty(Filtro))
            {
                profesores = profesores
                    .Where(p => p.Nombre.ToLower().Contains(Filtro.ToLower()))
                    .ToList();
            }

            return View(profesores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_profesorService.ProfesorExiste(id.Value))
            {
                return NotFound();
            }

            var profesor = _profesorService.ObtenerPorId(id.Value);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfesorId,Nombre,Correo")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                await _profesorService.CrearProfesorAsync(profesor);
                return RedirectToAction(nameof(Index));
            }
            return View(profesor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_profesorService.ProfesorExiste(id.Value))
            {
                return NotFound();
            }

            var profesor = _profesorService.ObtenerPorId(id.Value);
            if (profesor == null)
            {
                return NotFound();
            }
            return View(profesor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfesorId,Nombre,Correo")] Profesor profesor)
        {
            if (id != profesor.ProfesorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _profesorService.EditarProfesorAsync(id, profesor);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(profesor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_profesorService.ProfesorExiste(id.Value))
            {
                return NotFound();
            }

            var profesor = _profesorService.ObtenerPorId(id.Value);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _profesorService.EliminarProfesorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
