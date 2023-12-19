namespace FinalHerr.Models;

public class Profesor
{
    public int ProfesorId { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    
    public virtual ICollection<Clase>? Clases { get; set; }

    


}
