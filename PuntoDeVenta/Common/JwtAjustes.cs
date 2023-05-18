namespace PuntoDeVenta.Common;

public class JwtAjustes
{
    public string Secreto { get; init; } = "";
    public string Issuer { get; init; } = "";
    public int ExpiracionEnMinutos { get; init; }
}