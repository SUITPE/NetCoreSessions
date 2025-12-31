// Controllers/PersonasController.cs
using CorpManager.Api.DTOs;
using CorpManager_Completo.Models;
using CorpManager_Completo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CorpManager.Api.Controllers;

/// <summary>
/// Controlador de API REST para gestionar personas (Sesión 15).
/// Demuestra los métodos HTTP: GET, POST, PUT, DELETE.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonasController : ControllerBase
{
    private readonly PersonaService _servicio;

    public PersonasController(PersonaService servicio)
    {
        _servicio = servicio;
    }

    /// <summary>
    /// GET api/personas - Obtiene todas las personas.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<PersonaDto>> GetAll()
    {
        var personas = _servicio.ObtenerTodos();
        var dtos = personas.Select(kvp => ConvertirADto(kvp.Key, kvp.Value)).ToList();
        return Ok(dtos);
    }

    /// <summary>
    /// GET api/personas/{id} - Obtiene una persona por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<PersonaDto> GetById(int id)
    {
        var personas = _servicio.ObtenerTodos();
        if (!personas.ContainsKey(id))
            return NotFound(new { mensaje = $"Persona con ID {id} no encontrada" });

        return Ok(ConvertirADto(id, personas[id]));
    }

    /// <summary>
    /// POST api/personas/empleado - Crea un nuevo empleado.
    /// </summary>
    [HttpPost("empleado")]
    public ActionResult<EmpleadoDto> CrearEmpleado([FromBody] CrearEmpleadoDto dto)
    {
        try
        {
            var empleado = _servicio.CrearEmpleado(dto.Nombre, dto.Cargo, dto.Edad, dto.SalarioMensual);
            int id = _servicio.Agregar(empleado);

            var resultado = new EmpleadoDto
            {
                Id = id,
                Nombre = dto.Nombre,
                Cargo = dto.Cargo,
                Edad = dto.Edad,
                SalarioMensual = dto.SalarioMensual,
                Tipo = "Empleado"
            };

            return CreatedAtAction(nameof(GetById), new { id }, resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// POST api/personas/gerente - Crea un nuevo gerente.
    /// </summary>
    [HttpPost("gerente")]
    public ActionResult<GerenteDto> CrearGerente([FromBody] CrearGerenteDto dto)
    {
        try
        {
            var gerente = _servicio.CrearGerente(dto.Nombre, dto.Departamento, dto.Edad, dto.SalarioMensual, dto.BonoAnual);
            int id = _servicio.Agregar(gerente);

            var resultado = new GerenteDto
            {
                Id = id,
                Nombre = dto.Nombre,
                Departamento = dto.Departamento,
                Edad = dto.Edad,
                SalarioMensual = dto.SalarioMensual,
                BonoAnual = dto.BonoAnual,
                Tipo = "Gerente"
            };

            return CreatedAtAction(nameof(GetById), new { id }, resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// DELETE api/personas/{id} - Elimina una persona.
    /// Nota: Requiere implementar método Eliminar en PersonaService.
    /// </summary>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var personas = _servicio.ObtenerTodos();
        if (!personas.ContainsKey(id))
            return NotFound(new { mensaje = $"Persona con ID {id} no encontrada" });

        // Por simplicidad, indicamos que no está implementado aún
        return Ok(new { mensaje = $"Funcionalidad de eliminación pendiente de implementar" });
    }

    /// <summary>
    /// GET api/personas/estadisticas - Obtiene estadísticas generales.
    /// </summary>
    [HttpGet("estadisticas")]
    public ActionResult GetEstadisticas()
    {
        var (total, promSalario, promEdad, masaSalarial) = _servicio.CalcularEstadisticas();

        return Ok(new
        {
            totalPersonas = total,
            salarioPromedio = promSalario,
            edadPromedio = promEdad,
            masaSalarial = masaSalarial
        });
    }

    /// <summary>
    /// Convierte un modelo de dominio a DTO.
    /// </summary>
    private static PersonaDto ConvertirADto(int id, Persona persona)
    {
        if (persona is Gerente gerente)
        {
            return new GerenteDto
            {
                Id = id,
                Nombre = gerente.Nombre,
                Edad = gerente.Edad,
                Tipo = "Gerente",
                Departamento = gerente.Departamento,
                SalarioMensual = gerente.SalarioMensual,
                BonoAnual = gerente.BonoAnual
            };
        }

        if (persona is Empleado empleado)
        {
            return new EmpleadoDto
            {
                Id = id,
                Nombre = empleado.Nombre,
                Edad = empleado.Edad,
                Tipo = "Empleado",
                Cargo = empleado.Cargo,
                SalarioMensual = empleado.SalarioMensual
            };
        }

        return new PersonaDto
        {
            Id = id,
            Nombre = persona.Nombre,
            Edad = persona.Edad,
            Tipo = persona.ObtenerRol()
        };
    }
}
