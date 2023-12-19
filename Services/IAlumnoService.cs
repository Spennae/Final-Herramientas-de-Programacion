namespace FinalHerr.Services;
using FinalHerr.Models;
// IAlumnoService.cs
public interface IAlumnoService
{
    List<Alumno> ObtenerTodos();
    Alumno ObtenerPorId(int id);
    Task CrearAlumnoAsync(Alumno alumno);
    Task EditarAlumnoAsync(int id, Alumno alumno);
    Task EliminarAlumnoAsync(int id);
    List<Alumno> FiltrarAlumnos(string filtro);
    bool AlumnoExiste(int id);
}

