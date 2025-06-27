using Microsoft.AspNetCore.Mvc;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _profesorService;

    public ProfesoresController(
        IProfesorService profesorService)
    {
        _profesorService = profesorService;
    }

    /// <summary>
    /// Obtiene todos los profesores
    /// </summary>
    /// <returns>Lista de profesores</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfesorListDto>>> GetProfesores()
    {
        var profesores = await _profesorService.GetAllProfesoresAsync();
        return Ok(profesores);
    }

    /// <summary>
    /// Obtiene un profesor por ID
    /// </summary>
    /// <param name="id">ID del profesor</param>
    /// <returns>Datos del profesor</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfesorDto>> GetProfesor(int id)
    {
        var profesor = await _profesorService.GetProfesorByIdAsync(id);
        if (profesor == null)
        {
            return NotFound($"Profesor con ID {id} no encontrado");
        }
        return Ok(profesor);
    }

    /// <summary>
    /// Crea un nuevo profesor
    /// </summary>
    /// <param name="profesorCreateDto">Datos del profesor a crear</param>
    /// <returns>Profesor creado</returns>
    [HttpPost]
    public async Task<ActionResult<ProfesorDto>> CreateProfesor([FromBody] ProfesorCreateDto profesorCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var profesor = await _profesorService.CreateProfesorAsync(profesorCreateDto);
            return CreatedAtAction(nameof(GetProfesor), new { id = profesor.Id }, profesor);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza un profesor existente
    /// </summary>
    /// <param name="id">ID del profesor a actualizar</param>
    /// <param name="profesorUpdateDto">Datos actualizados del profesor</param>
    /// <returns>Profesor actualizado</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProfesorDto>> UpdateProfesor(int id, [FromBody] ProfesorUpdateDto profesorUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var profesor = await _profesorService.UpdateProfesorAsync(id, profesorUpdateDto);
            return Ok(profesor);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Elimina un profesor
    /// </summary>
    /// <param name="id">ID del profesor a eliminar</param>
    /// <returns>Resultado de la eliminación</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProfesor(int id)
    {
        try
        {
            var result = await _profesorService.DeleteProfesorAsync(id);
            if (!result)
            {
                return NotFound($"Profesor con ID {id} no encontrado");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Verifica si existe un profesor
    /// </summary>
    /// <param name="id">ID del profesor</param>
    /// <returns>True si existe, false si no</returns>
    [HttpGet("{id}/exists")]
    public async Task<ActionResult<bool>> ExisteProfesor(int id)
    {
        var existe = await _profesorService.ExisteProfesorAsync(id);
        return Ok(existe);
    }
}
