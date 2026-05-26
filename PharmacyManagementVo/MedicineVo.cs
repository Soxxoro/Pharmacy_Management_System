namespace PharmacyManagementVo
{
    public class MedicineVo
    {
        public int Medicine_Id_PK { get; set; }
        public string Medicine_Name { get; set; } = "";
        public string Medicine_Dosage { get; set; } = "";
        public decimal Medicine_Price { get; set; }
    }
}
