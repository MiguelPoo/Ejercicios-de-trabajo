using System;
using System.Collections.Generic;

namespace CRUD.Models;

public partial class Producto
{
    public string ProductoId { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();
}
