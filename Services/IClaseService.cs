namespace FinalHerr.Services;
using FinalHerr.Models;
public interface IClaseService
{
    List<Clase> ObtenerTodos();
    Clase ObtenerPorId(int id);
    Task CrearClaseAsync(Clase clase);
    Task EditarClaseAsync(int id, Clase clase);
    Task EliminarClaseAsync(int id);
    List<Clase> FiltrarClases(string filtro);
    bool ClaseExiste(int id);
    List<Profesor> ObtenerProfesores();
    void InscribirAlumno(Clase clase, Alumno alumno);
    void DesinscribirAlumno(Clase clase, Alumno alumno);
    List<Alumno> ObtenerAlumnosInscritos(int claseId);
}
