using System;
using System.Collections.Generic;

namespace PermisosMVC.Models.DB;

public partial class RolOperacion
{
    public int Id { get; set; }

    public int? IdRol { get; set; }

    public int? IdOperacion { get; set; }

    public virtual Operacione? IdOperacionNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
