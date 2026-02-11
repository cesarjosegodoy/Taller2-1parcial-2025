using MiWebAPI.TvProgram;
using MiWebAPI.AcDatosTV;
using Microsoft.AspNetCore.Mvc;

namespace MiWebAPI.Controllers;


///<summary> Controlador para gestionar Programas </summary>
[ApiController]
[Route("api/{controller}")]

public class ProgramasController : ControllerBase
{

    private AccesoADatosCanalTV ADProgramas;


    ///<summary> Constructor Clase Programas </summary>
    public ProgramasController()
    {
        ADProgramas = new AccesoADatosCanalTV();

        //Esto es el Constructor de la Clase

    }

    /// <summary>Lista todos los Programas</summary>
    [HttpGet]
    public IActionResult GetProgramas()
    {
        var Pedidos = ADProgramas.GetAll();
        return Ok(Pedidos);

    }

    ///<summary>Genera un Programa nuevo</summary>
    ///<returns>200 OK - Listado completo de Programas </returns>
    [HttpPost]
    public IActionResult CrearPrograma([FromBody] Programa programa)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var programaCreado = ADProgramas.AddPrograma(programa);

        return CreatedAtAction(nameof(GetProgramas), new { id = programaCreado.Id }, programaCreado);
    }

    /// <summary>Lista un Programa por su Id.</summary>
    [HttpGet("{id}")]
    public IActionResult GetPedido(int id)
    {
        var programa = ADProgramas.GetById(id);
        if (programa is null) return NotFound();

        return Ok(programa);
    }

    /// <summary>Modifica un programa del listado.</summary>
    [HttpPut("{id}")]
    public IActionResult ModificarPrograma(int id, [FromBody] Programa programa)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var programaModificado = ADProgramas.UpdatePrograma(id, programa);
        if (programaModificado is false) return NotFound();

        return Ok(new { mensaje = "Programa modificado con éxito" });
    }


   /// <summary>Elimina un programa.</summary>
    [HttpDelete("{id}")]
    public IActionResult EliminarCadete(int id)
    {
        var cadeteEliminado = ADProgramas.Delete(id);
        if (cadeteEliminado is false) return NotFound();

        return Ok(new { mensaje = "Programa eliminado con éxito" });
    }


   /// <summary>Lista un Programa por su Id.</summary>
    [HttpGet("by-day/{day}")]
    public IActionResult GetProgramaXdia(Estado day)
    {
        var programa = ADProgramas.GetByDay(day);
        if (programa is null) return NotFound();

        return Ok(programa);
    }


    
}