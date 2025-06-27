using Microsoft.AspNetCore.Mvc;
using Interrapidisimo.Application.Interfaces;
using Interrapidisimo.Application.DTOs;

namespace Interrapidisimo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriasController : ControllerBase
{
    private readonly IMateriaService _materiaService;

    public MateriasController(
        IMateriaService materiaService)
    {
        _materiaService = materiaService;
    }

    /// <summary>
    /// Obtiene todas las materias
    /// </summary>
    /// <returns>Lista de materias</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MateriaListDto>>> GetMaterias()
    {
        var materias = await _materiaService.GetAllMateriasAsync();
        return Ok(materias);
    }

    /// <summary>
    /// Obtiene una materia por ID
    /// </summary>
    /// <param name="id">ID de la materia</param>
    /// <returns>Datos de la materia</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<MateriaDto>> GetMateria(int id)
    {
        var materia = await _materiaService.GetMateriaByIdAsync(id);
        if (materia == null)
        {
            return NotFound($"Materia con ID {id} no encontrada");
        }
        return Ok(materia);
    }

    /// <summary>
    /// Crea una nueva materia
    /// </summary>
    /// <param name="materiaCreateDto">Datos de la materia a crear</param>
    /// <returns>Materia creada</returns>
    [HttpPost]
    public async Task<ActionResult<MateriaDto>> CreateMateria([FromBody] MateriaCreateDto materiaCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var materia = await _materiaService.CreateMateriaAsync(materiaCreateDto);
            return CreatedAtAction(nameof(GetMateria), new { id = materia.Id }, materia);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Actualiza una materia existente
    /// </summary>
    /// <param name="id">ID de la materia a actualizar</param>
    /// <param name="materiaUpdateDto">Datos actualizados de la materia</param>
    /// <returns>Materia actualizada</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<MateriaDto>> UpdateMateria(int id, [FromBody] MateriaUpdateDto materiaUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var materia = await _materiaService.UpdateMateriaAsync(id, materiaUpdateDto);
            return Ok(materia);
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
    /// Elimina una materia
    /// </summary>
    /// <param name="id">ID de la materia a eliminar</param>
    /// <returns>Resultado de la eliminación</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMateria(int id)
    {
        try
        {
            var result = await _materiaService.DeleteMateriaAsync(id);
            if (!result)
            {
                return NotFound($"Materia con ID {id} no encontrada");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Obtiene materias disponibles para un estudiante
    /// </summary>
    /// <param name="estudianteId">ID del estudiante</param>
    /// <returns>Lista de materias disponibles</returns>
    [HttpGet("disponibles/{estudianteId}")]
    public async Task<ActionResult<IEnumerable<MateriasDisponiblesParaEstudianteDto>>> GetMateriasDisponibles(int estudianteId)
    {
        try
        {
            var materias = await _materiaService.GetMateriasDisponiblesParaEstudianteAsync(estudianteId);
            return Ok(materias);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Verifica si existe una materia
    /// </summary>
    /// <param name="id">ID de la materia</param>
    /// <returns>True si existe, false si no</returns>
    [HttpGet("{id}/exists")]
    public async Task<ActionResult<bool>> ExisteMateria(int id)
    {
        var existe = await _materiaService.ExisteMateriaAsync(id);
        return Ok(existe);
    }
}
