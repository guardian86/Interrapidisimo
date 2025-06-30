using Microsoft.AspNetCore.Mvc;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteService _estudianteService;

    public EstudiantesController(
        IEstudianteService estudianteService)
    {
        _estudianteService = estudianteService;
    }

    /// <summary>
    /// Obtiene todos los estudiantes
    /// </summary>
    /// <returns>Lista de estudiantes</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstudianteListDto>>> GetEstudiantes()
    {
        var estudiantes = await _estudianteService.GetAllAsync();
        return Ok(estudiantes);
    }

    /// <summary>
    /// Obtiene un estudiante por ID
    /// </summary>
    /// <param name="id">ID del estudiante</param>
    /// <returns>Datos del estudiante</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EstudianteDto>> GetEstudiante(int id)
    {
        var estudiante = await _estudianteService.GetByIdAsync(id);
        if (estudiante == null)
        {
            return NotFound($"Estudiante con ID {id} no encontrado");
        }
        return Ok(estudiante);
    }

    /// <summary>
    /// Crea un nuevo estudiante
    /// </summary>
    /// <param name="estudianteDto">Datos del estudiante</param>
    /// <returns>Estudiante creado</returns>
    [HttpPost]
    public async Task<ActionResult<EstudianteDto>> CreateEstudiante([FromBody] EstudianteCreateDto estudianteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var estudiante = await _estudianteService.CreateAsync(estudianteDto);
            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.Id }, estudiante);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza un estudiante existente
    /// </summary>
    /// <param name="id">ID del estudiante</param>
    /// <param name="estudianteDto">Datos actualizados</param>
    /// <returns>Estudiante actualizado</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<EstudianteDto>> UpdateEstudiante(int id, [FromBody] EstudianteUpdateDto estudianteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var estudiante = await _estudianteService.UpdateAsync(id, estudianteDto);
            return Ok(estudiante);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Elimina un estudiante
    /// </summary>
    /// <param name="id">ID del estudiante</param>
    /// <returns>Confirmación de eliminación</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEstudiante(int id)
    {
        try
        {
            await _estudianteService.DeleteAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene los compañeros de clase de un estudiante en una materia específica
    /// </summary>
    /// <param name="estudianteId">ID del estudiante</param>
    /// <param name="materiaId">ID de la materia</param>
    /// <returns>Lista de compañeros</returns>
    [HttpGet("{estudianteId}/companeros/materia/{materiaId}")]
    public async Task<ActionResult<IEnumerable<EstudianteCompaneroDto>>> GetCompaneros(int estudianteId, int materiaId)
    {
        try
        {
            var companeros = await _estudianteService.GetCompanerosAsync(estudianteId, materiaId);
            return Ok(companeros);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
