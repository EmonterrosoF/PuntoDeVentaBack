using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PuntoDeVenta.Models;
using PuntoDeVenta.Contratos;
using PuntoDeVenta.Contratos.Usuario;
using PuntoDeVenta.services.Autenticacion;

[ApiController]
[Route("api")]
public class LoginControlador : ControllerBase
{

    private readonly PuntoDeVentaContext _puntoDeVentaContext;
    private readonly IAutenticacionServicio _autenticIAutenticacionServicio;

    public LoginControlador(IAutenticacionServicio autenticIAutenticacionServicio, PuntoDeVentaContext puntoDeVentaContext)
    {
        _autenticIAutenticacionServicio = autenticIAutenticacionServicio;
        _puntoDeVentaContext = puntoDeVentaContext;
    }

    [HttpPost("login")]
    public IActionResult IniciarSesion(UsuarioRespuesta request)
    {

        var usuario = _autenticIAutenticacionServicio.Login(request.correo, request.password);

        if (usuario is null) return NotFound(new Respuesta(System.Net.HttpStatusCode.NotFound, null, Status.ERROR, mensaje: "contraseña o correo incorrectos"));


        return Ok(new Respuesta(System.Net.HttpStatusCode.OK, usuario, Status.EXITO, "exito"));

    }

    [HttpGet("home")]
    [Authorize]
    public IActionResult home()
    {

        return Ok(new Respuesta(System.Net.HttpStatusCode.OK, null, Status.EXITO, ""));
    }

    [HttpGet("prueba")]
    public IActionResult prueba()
    {
        return Ok("prueba");
    }

}