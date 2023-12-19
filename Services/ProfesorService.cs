namespace FinalHerr.Services;

using FinalHerr.Data;
using FinalHerr.Models;
using Microsoft.EntityFrameworkCore;

public class ProfesorService : IProfesorService
{
    private readonly ApplicationDbContext _context;

    public ProfesorService(ApplicationDbContext context)
    {
        _context = context;
    }


    public List<Profesor> ObtenerTodos()
    {
        return _context.Profesor.Include(p => p.Clases).ToList();
    }
    public List<Profesor> ObtenerProfesoresSinClase()
    {
        return _context.Profesor.ToList();
    }
    public Profesor ObtenerPorId(int id)
    {
        return _context.Profesor.Include(p => p.Clases).FirstOrDefault(p => p.ProfesorId == id);
    }

    public async Task CrearProfesorAsync(Profesor profesor)
    {
        _context.Add(profesor);
        await _context.SaveChangesAsync();
    }

    public async Task EditarProfesorAsync(int id, Profesor profesor)
    {
        if (id != profesor.ProfesorId)
            throw new ArgumentException("ID no coincide con el ID del profesor");

        _context.Update(profesor);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarProfesorAsync(int id)
    {
        var profesor = await _context.Profesor.FindAsync(id);
        if (profesor != null)
        {
            _context.Profesor.Remove(profesor);
            await _context.SaveChangesAsync();
        }
    }
    public List<Profesor> FiltrarProfesores(string filtro)
    {
        return _context.Profesor.Include(p => p.Clases)
            .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
            .ToList();
    }
    public bool ProfesorExiste(int id)
    {
        return _context.Profesor.Any(e => e.ProfesorId == id);
    }
}
