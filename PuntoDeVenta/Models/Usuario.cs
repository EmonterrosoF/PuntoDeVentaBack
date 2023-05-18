using System;
using System.Collections.Generic;

namespace PuntoDeVenta.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Password { get; set; } = null!;
}
