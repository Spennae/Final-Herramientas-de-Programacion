namespace FinalHerr.Models;

public class Alumno
{
    public int AlumnoId { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Carrera { get; set; }
    
    public virtual ICollection<Clase>? Clases { get; set; } = new List<Clase>();
}
