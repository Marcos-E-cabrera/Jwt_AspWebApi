using System;
using System.Collections.Generic;

namespace PermisosMVC.Models.DB;

public partial class Rol
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<RolOperacion> RolOperacions { get; set; } = new List<RolOperacion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
