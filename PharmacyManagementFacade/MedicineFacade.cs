using System.Collections.Generic;
using PharmacyManagementVo;
using PharmacyManagement.Bo;

namespace PharmacyManagement.Facade
{
    public class MedicineFacade : IMedicineFacade
    {
        private MedicineBo bo;

        public MedicineFacade(MedicineBo medicineBo)
        {
            bo = medicineBo;
        }

        public bool AddMedicine(MedicineVo med)
        {
            return bo.AddMedicine(med);
        }

        public MedicineVo FindById(int id)
        {
            return bo.FindById(id);
        }

        public List<MedicineVo> GetAllMedicines()
        {
            return bo.GetAllMedicines();
        }

        public bool UpdateMedicine(MedicineVo med)
        {
            return bo.UpdateMedicine(med);
        }
    }
}
