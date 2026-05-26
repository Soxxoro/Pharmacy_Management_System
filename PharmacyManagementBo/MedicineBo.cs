using System.Collections.Generic;
using PharmacyManagementVo;
using PharmacyManagementDao;
using PharmacyException;

namespace PharmacyManagementBo
{
    public class MedicineBo
    {
        private IMedicineDao dao;

        public MedicineBo(IMedicineDao medicineDao)
        {
            dao = medicineDao;
        }

        public bool AddMedicine(MedicineVo med)
        {
            if (string.IsNullOrEmpty(med.Medicine_Name))
            {
                throw new MedicineException("Medicine Name cannot be empty.");
            }
            if (string.IsNullOrEmpty(med.Medicine_Dosage))
            {
                throw new MedicineException("Medicine Dosage cannot be empty.");
            }
            if (med.Medicine_Price < 0)
            {
                throw new MedicineException("Medicine Price cannot be negative.");
            }

            return dao.AddMedicine(med);
        }

        public MedicineVo FindById(int id)
        {
            if (id <= 0)
            {
                throw new MedicineException("Medicine ID must be greater than zero.");
            }

            return dao.FindById(id);
        }

        public List<MedicineVo> GetAllMedicines()
        {
            return dao.GetAllMedicines();
        }

        public bool UpdateMedicine(MedicineVo med)
        {
            if (med.Medicine_Id_PK <= 0)
            {
                throw new MedicineException("Invalid Medicine ID for update.");
            }
            if (string.IsNullOrEmpty(med.Medicine_Name))
            {
                throw new MedicineException("Medicine Name cannot be empty.");
            }
            if (string.IsNullOrEmpty(med.Medicine_Dosage))
            {
                throw new MedicineException("Medicine Dosage cannot be empty.");
            }
            if (med.Medicine_Price < 0)
            {
                throw new MedicineException("Medicine Price cannot be negative.");
            }

            return dao.UpdateMedicine(med);
        }
    }
}
