using System.ComponentModel.DataAnnotations;

namespace Interrapidisimo.Application.DTOs
{
    public class MateriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public bool IsActive { get; set; }
        public List<ProfesorListDto> ProfesoresDisponibles { get; set; } = new();
    }

    public class MateriaCreateDto
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(10, ErrorMessage = "El código no puede exceder 10 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La {0} no puede exceder 500 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(1, 6, ErrorMessage = "Los {0} deben estar entre 1 y 6")]
        public int Creditos { get; set; } = 3;
    }

    public class MateriaUpdateDto
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(10, ErrorMessage = "El código no puede exceder 10 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La {0} no puede exceder 500 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(1, 6, ErrorMessage = "Los {0} deben estar entre 1 y 6")]
        public int Creditos { get; set; }
    }

    public class MateriaListDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public bool IsActive { get; set; }
    }

    public class MateriaInscritaDto
    {
        public int MateriaId { get; set; }
        public string NombreMateria { get; set; } = string.Empty;
        public string CodigoMateria { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int ProfesorId { get; set; }
        public string NombreProfesor { get; set; } = string.Empty;
        public DateTime FechaInscripcion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<EstudianteCompaneroDto> CompanerosDeClase { get; set; } = new();
    }

    public class EstudianteCompaneroDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
