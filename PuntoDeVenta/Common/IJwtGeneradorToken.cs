namespace PuntoDeVenta.Common;
public interface IJwtGeneratorToken
{
    string GenerarToken(string username, string email);
}