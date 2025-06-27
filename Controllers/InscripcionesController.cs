using Microsoft.AspNetCore.Mvc;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InscripcionesController : ControllerBase
{
    private readonly IInscripcionService _inscripcionService;

    public InscripcionesController(
        IInscripcionService inscripcionService)
    {
        _inscripcionService = inscripcionService;
    }

    /// <summary>
    /// Inscribe un estudiante en una materia con un profesor específico
    /// </summary>
    /// <param name="inscripcionDto">Datos de la inscripción</param>
    /// <returns>Resultado de la inscripción</returns>
    [HttpPost]
    public async Task<ActionResult<InscripcionResponseDto>> InscribirEstudiante([FromBody] InscripcionCreateDto inscripcionDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var resultado = await _inscripcionService.InscribirEstudianteAsync(inscripcionDto);
            return CreatedAtAction(nameof(GetMateriasDelEstudiante), 
                new { estudianteId = inscripcionDto.EstudianteId }, resultado);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    /// <summary>
    /// Desinscribe un estudiante de una materia con un profesor específico
    /// </summary>
    /// <param name="estudianteId">ID del estudiante</param>
    /// <param name="materiaId">ID de la materia</param>
    /// <param name="profesorId">ID del profesor</param>
    /// <returns>Resultado de la desinscripción</returns>
    [HttpDelete("{estudianteId}/{materiaId}/{profesorId}")]
    public async Task<ActionResult> DesinscribirEstudiante(int estudianteId, int materiaId, int profesorId)
    {
        try
        {
            var resultado = await _inscripcionService.DesinscribirEstudianteAsync(estudianteId, materiaId, profesorId);
            if (!resultado)
            {
                return NotFound("Inscripción no encontrada");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene todas las materias en las que está inscrito un estudiante
    /// </summary>
    /// <param name="estudianteId">ID del estudiante</param>
    /// <returns>Lista de materias inscritas</returns>
    [HttpGet("estudiante/{estudianteId}")]
    public async Task<ActionResult<IEnumerable<MateriaInscritaDto>>> GetMateriasDelEstudiante(int estudianteId)
    {
        try
        {
            var materias = await _inscripcionService.GetMateriasDelEstudianteAsync(estudianteId);
            return Ok(materias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    /// <summary>
    /// Valida si una inscripción es posible
    /// </summary>
    /// <param name="estudianteId">ID del estudiante</param>
    /// <param name="materiaId">ID de la materia</param>
    /// <param name="profesorId">ID del profesor</param>
    /// <returns>True si la inscripción es válida, false si no</returns>
    [HttpGet("validar/{estudianteId}/{materiaId}/{profesorId}")]
    public async Task<ActionResult<bool>> ValidarInscripcion(int estudianteId, int materiaId, int profesorId)
    {
        try
        {
            var esValida = await _inscripcionService.ValidarInscripcionAsync(estudianteId, materiaId, profesorId);
            return Ok(esValida);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene un resumen de inscripciones por estudiante
    /// </summary>
    /// <param name="estudianteId">ID del estudiante</param>
    /// <returns>Resumen de inscripciones</returns>
    [HttpGet("resumen/{estudianteId}")]
    public async Task<ActionResult> GetResumenInscripciones(int estudianteId)
    {
        try
        {
            var materias = await _inscripcionService.GetMateriasDelEstudianteAsync(estudianteId);
            var resumen = new
            {
                EstudianteId = estudianteId,
                TotalMaterias = materias.Count(),
                TotalCreditos = materias.Sum(m => m.Creditos),
                Materias = materias
            };
            return Ok(resumen);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}
