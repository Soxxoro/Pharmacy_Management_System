using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PharmacyManagementVo;
using Serilog;

namespace PharmacyManagementDao
{
    public class MedicineDao : IMedicineDao
    {
        private string connString;

        public MedicineDao(string connectionString)
        {
            connString = connectionString;
        }

        private MedicineVo MapMedicine(SqlDataReader rdr)
        {
            MedicineVo med = new MedicineVo();
            med.Medicine_Id_PK = Convert.ToInt32(rdr["Medicine_Id_PK"]);
            med.Medicine_Name = Convert.ToString(rdr["Medicine_Name"]);
            med.Medicine_Dosage = Convert.ToString(rdr["Medicine_Dosage"]);
            med.Medicine_Price = Convert.ToDecimal(rdr["Medicine_Price"]);
            return med;
        }

        public bool AddMedicine(MedicineVo med)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                string sql = "INSERT INTO Medicine (Medicine_Name, Medicine_Dosage, Medicine_Price) VALUES (@Name, @Dosage, @Price)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@Name", med.Medicine_Name);
                cmd.Parameters.AddWithValue("@Dosage", med.Medicine_Dosage);
                cmd.Parameters.AddWithValue("@Price", med.Medicine_Price);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                return rows > 0;
            }
            catch (Exception ex)
            {
                Log.Error("AddMedicine failed: " + ex.Message);
                throw;
            }
        }

        public MedicineVo FindById(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                string sql = "SELECT Medicine_Id_PK, Medicine_Name, Medicine_Dosage, Medicine_Price FROM Medicine WHERE Medicine_Id_PK = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                MedicineVo med = null;
                if (rdr.Read())
                {
                    med = MapMedicine(rdr);
                }
                conn.Close();
                return med;
            }
            catch (Exception ex)
            {
                Log.Error("FindById failed: " + ex.Message);
                throw;
            }
        }

        public List<MedicineVo> GetAllMedicines()
        {
            try
            {
                List<MedicineVo> list = new List<MedicineVo>();
                SqlConnection conn = new SqlConnection(connString);
                string sql = "SELECT Medicine_Id_PK, Medicine_Name, Medicine_Dosage, Medicine_Price FROM Medicine";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MedicineVo med = MapMedicine(rdr);
                    list.Add(med);
                }
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                Log.Error("GetAllMedicines failed: " + ex.Message);
                throw;
            }
        }

        public bool UpdateMedicine(MedicineVo med)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                string sql = "UPDATE Medicine SET Medicine_Name = @Name, Medicine_Dosage = @Dosage, Medicine_Price = @Price WHERE Medicine_Id_PK = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@Id", med.Medicine_Id_PK);
                cmd.Parameters.AddWithValue("@Name", med.Medicine_Name);
                cmd.Parameters.AddWithValue("@Dosage", med.Medicine_Dosage);
                cmd.Parameters.AddWithValue("@Price", med.Medicine_Price);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                return rows > 0;
            }
            catch (Exception ex)
            {
                Log.Error("UpdateMedicine failed: " + ex.Message);
                throw;
            }
        }
    }
}
