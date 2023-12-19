namespace FinalHerr.Services;
using FinalHerr.Models;
public interface IProfesorService
{
    List<Profesor> ObtenerTodos();
    Profesor ObtenerPorId(int id);
    Task CrearProfesorAsync(Profesor profesor);
    Task EditarProfesorAsync(int id, Profesor profesor);
    Task EliminarProfesorAsync(int id);
    List<Profesor> FiltrarProfesores(string filtro);
    bool ProfesorExiste(int id);
    public List<Profesor> ObtenerProfesoresSinClase();
}
