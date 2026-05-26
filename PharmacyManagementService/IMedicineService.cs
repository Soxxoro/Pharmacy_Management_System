using PharmacyManagementVo;

namespace PharmacyManagementService
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
