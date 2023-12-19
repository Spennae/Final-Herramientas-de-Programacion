using System.ComponentModel.DataAnnotations;
using FinalHerr.Models;

public class AsignarAlumnosViewModel
{
    public List<Clase> Clases { get; set; }
    public List<Alumno> Alumnos { get; set; }

    [Display(Name = "Seleccionar Clase")]
    public int? SelectedClaseId { get; set; }

    [Display(Name = "Seleccionar Alumnos")]
    public List<int> SelectedAlumnoIds { get; set; } = new List<int>();
}
