
using System.Net;
using System.Text.Json.Serialization;

namespace PuntoDeVenta.Contratos;

public class Respuesta
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Estado { get; set; }
    public HttpStatusCode EstadoCodigo { get; set; }
    public Object Datos { get; set; }

    public string Mensaje { get; set; }

    public Respuesta(HttpStatusCode estadoCodigo, object datos, Status estado, string mensaje)
    {
        this.EstadoCodigo = estadoCodigo;
        this.Datos = datos;
        this.Estado = estado;
        Mensaje = mensaje;
    }
}
public enum Status
{
    EXITO,
    ERROR
}
