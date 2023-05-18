using PuntoDeVenta.Common;
using PuntoDeVenta.Models;

namespace PuntoDeVenta.services.Autenticacion;

public class AutenticacionServicio : IAutenticacionServicio
{

    private readonly PuntoDeVentaContext _puntoDeVentaContext;


    private readonly IJwtGeneratorToken _jwtGeneraIJwtGeneratorToken;

    public AutenticacionServicio(IJwtGeneratorToken jwtGeneraIJwtGeneratorToken, PuntoDeVentaContext puntoDeVentaContext)
    {
        _jwtGeneraIJwtGeneratorToken = jwtGeneraIJwtGeneratorToken;
        _puntoDeVentaContext = puntoDeVentaContext;

    }

    public UsuarioAutenticado? Login(string correo, string password)
    {

        // if (usuario.correo != correo || usuario.password != password) return null;
        var usuario = _puntoDeVentaContext.Usuarios.Where(u => u.Correo == correo && u.Password == password).FirstOrDefault();

        if (usuario is null) return null;

        var token = _jwtGeneraIJwtGeneratorToken.GenerarToken(usuario.Nombre, usuario.Correo);

        UsuarioAutenticado usuarioAutenticado = new UsuarioAutenticado(usuario.Id, usuario.Nombre, usuario.Correo, token);

        return usuarioAutenticado;

    }
}