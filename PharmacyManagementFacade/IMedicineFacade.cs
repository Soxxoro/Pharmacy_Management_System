using System.Collections.Generic;
using PharmacyManagementVo;

namespace PharmacyManagement.Facade
{
    public interface IMedicineFacade
    {
        bool AddMedicine(MedicineVo med);
        MedicineVo FindById(int id);
        List<MedicineVo> GetAllMedicines();
        bool UpdateMedicine(MedicineVo med);
    }
}
