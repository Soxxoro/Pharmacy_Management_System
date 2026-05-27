using System;
using System.Collections.Generic;

namespace EntityFrameworkDao.Models;

public partial class Medicine
{
    public int MedicineIdPk { get; set; }

    public string MedicineName { get; set; }

    public string MedicineDosage { get; set; }

    public decimal? MedicinePrice { get; set; }

    public int? UnitIdFk { get; set; }

    public virtual Unit UnitIdFkNavigation { get; set; }
}
