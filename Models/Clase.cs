namespace FinalHerr.Models;

public class Clase
{
    public int ClaseId { get; set; }
    public string Nombre { get; set; }
    public string Codigo { get; set; }
    public string Aula { get; set; }
    
    
    public virtual ICollection<Alumno>? Alumnos { get; set; } = new List<Alumno>();
    
    public int ProfesorId { get; set; }
    public Profesor? Profesor { get; set; }    
}
