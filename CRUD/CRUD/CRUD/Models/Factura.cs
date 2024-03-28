using System;
using System.Collections.Generic;

namespace CRUD.Models;

public partial class Factura
{
    public int FacturaId { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Total { get; set; }

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();
}
