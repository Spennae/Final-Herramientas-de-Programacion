// InscripcionController.cs
using FinalHerr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "SysAdmin,Administracion")]
public class InscripcionController : Controller
{
    private readonly IClaseService _claseService;
    private readonly IAlumnoService _alumnoService;

    public InscripcionController(IClaseService claseService, IAlumnoService alumnoService)
    {
        _claseService = claseService;
        _alumnoService = alumnoService;
    }

    public IActionResult Index()
    {
        var viewModel = new AsignarAlumnosViewModel
        {
            Clases = _claseService.ObtenerTodos(),
            Alumnos = _alumnoService.ObtenerTodos()
        };
    
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult InscribirAlumnos(AsignarAlumnosViewModel viewModel)
    {
        if (viewModel.SelectedClaseId.HasValue)
        {
            var clase = _claseService.ObtenerPorId(viewModel.SelectedClaseId.Value);

            if (clase != null && viewModel.SelectedAlumnoIds.Any())
            {
                foreach (var alumnoId in viewModel.SelectedAlumnoIds)
                {
                    var alumno = _alumnoService.ObtenerPorId(alumnoId);

                    if (alumno != null)
                    {
                        _claseService.InscribirAlumno(clase, alumno);
                    }
                }
            }
        }

        return RedirectToAction("Index");
    }
}
