namespace PuntoDeVenta.Models;

public class UsuarioAutenticado
{
    public UsuarioAutenticado(long id, string nombre, string correo, string token)
    {
        this.id = id;
        this.nombre = nombre;
        this.correo = correo;
        this.token = token;
    }

    public long id { get; set; }
    public string nombre { get; set; }

    public string correo { get; set; }

    public string token { get; set; }
}
