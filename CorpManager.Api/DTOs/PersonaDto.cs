// DTOs/PersonaDto.cs
namespace CorpManager.Api.DTOs;

/// <summary>
/// DTO base para transferencia de datos de personas (Sesión 15).
/// Los DTOs permiten controlar qué datos se exponen en la API.
/// </summary>
public class PersonaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Tipo { get; set; } = "Empleado";
}

/// <summary>
/// DTO para empleados con información adicional.
/// </summary>
public class EmpleadoDto : PersonaDto
{
    public string Cargo { get; set; } = string.Empty;
    public decimal SalarioMensual { get; set; }
}

/// <summary>
/// DTO para gerentes con información de departamento y bono.
/// </summary>
public class GerenteDto : PersonaDto
{
    public string Departamento { get; set; } = string.Empty;
    public decimal SalarioMensual { get; set; }
    public decimal BonoAnual { get; set; }
}

/// <summary>
/// DTO para crear un nuevo empleado (sin Id).
/// </summary>
public class CrearEmpleadoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public int Edad { get; set; }
    public decimal SalarioMensual { get; set; }
}

/// <summary>
/// DTO para crear un nuevo gerente (sin Id).
/// </summary>
public class CrearGerenteDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public int Edad { get; set; }
    public decimal SalarioMensual { get; set; }
    public decimal BonoAnual { get; set; }
}
