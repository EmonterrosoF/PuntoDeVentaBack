namespace PuntoDeVenta.services.Autenticacion;

using PuntoDeVenta.Models;

public interface IAutenticacionServicio
{
    UsuarioAutenticado? Login(string correo, string password);
}