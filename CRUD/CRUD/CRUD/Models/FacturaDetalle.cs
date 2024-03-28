using System;
using System.Collections.Generic;

namespace CRUD.Models;

public partial class FacturaDetalle
{
    public int? FacturaId { get; set; }

    public int FacturaDetalleId { get; set; }

    public string? ProductoId { get; set; }

    public decimal? Precio { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual Producto? Producto { get; set; }
}
