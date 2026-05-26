using PharmacyManagement.Vo;

namespace PharmacyManagement.Service
{
    public interface IMedicineService
    {
        void StartMenu();
        ResponseObject AddMedicine();
        ResponseObject FindById();
        ResponseObject GetAllMedicines();
        ResponseObject UpdateMedicine();
    }
}
