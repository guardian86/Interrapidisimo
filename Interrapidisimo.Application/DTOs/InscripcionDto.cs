using System.ComponentModel.DataAnnotations;

namespace Interrapidisimo.Application.DTOs
{
    public class InscripcionDto
    {
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public int ProfesorId { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Estado { get; set; } = "Activo";
    }

    public class InscripcionCreateDto
    {
        [Required(ErrorMessage = "El ID del estudiante es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del estudiante debe ser mayor a 0")]
        public int EstudianteId { get; set; }

        [Required(ErrorMessage = "El ID de la materia es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la materia debe ser mayor a 0")]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "El ID del profesor es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del profesor debe ser mayor a 0")]
        public int ProfesorId { get; set; }
    }

    public class InscripcionResponseDto
    {
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public int ProfesorId { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }

    public class MateriasDisponiblesParaEstudianteDto
    {
        public int MateriaId { get; set; }
        public string NombreMateria { get; set; } = string.Empty;
        public string CodigoMateria { get; set; } = string.Empty;
        public int CreditosMateria { get; set; }
        public List<ProfesorDisponibleDto> ProfesoresDisponibles { get; set; } = new();
    }

    public class ProfesorDisponibleDto
    {
        public int ProfesorId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
    }
}
