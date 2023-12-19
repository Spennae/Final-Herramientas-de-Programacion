// // AlumnosController.cs
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
    public class AlumnosController : Controller
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnosController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        public IActionResult Index(string Filtro)
        {
            var alumnos = _alumnoService.ObtenerTodos();

            if (!string.IsNullOrEmpty(Filtro))
            {
                alumnos = _alumnoService.FiltrarAlumnos(Filtro);
            }
            
            return View(alumnos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_alumnoService.AlumnoExiste(id.Value))
            {
                return NotFound();
            }

            var alumno = _alumnoService.ObtenerPorId(id.Value);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlumnoId,Nombre,Correo,Carrera")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                await _alumnoService.CrearAlumnoAsync(alumno);
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !_alumnoService.AlumnoExiste(id.Value))
            {
                return NotFound();
            }

            var alumno = _alumnoService.ObtenerPorId(id.Value);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlumnoId,Nombre,Correo,Carrera")] Alumno alumno)
        {
            if (id != alumno.AlumnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _alumnoService.EditarAlumnoAsync(id, alumno);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_alumnoService.AlumnoExiste(id.Value))
            {
                return NotFound();
            }

            var alumno = _alumnoService.ObtenerPorId(id.Value);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _alumnoService.EliminarAlumnoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
