using System.Collections.Generic;
using PharmacyManagementVo;

namespace PharmacyManagementDao
{
    public interface IMedicineDao
    {
        bool AddMedicine(MedicineVo med);
        MedicineVo FindById(int id);
        List<MedicineVo> GetAllMedicines();
        bool UpdateMedicine(MedicineVo med);
    }
}
