using System;
using System.Collections.Generic;

namespace EntityFrameworkDao.Models;

public partial class Unit
{
    public int UnitIdPk { get; set; }

    public string UnitName { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
