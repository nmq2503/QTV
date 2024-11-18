using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using QTV.DataAccess;
using QTV.Models;

namespace QTV.Controllers;

public class StudentController
{
    public DataTable LoadComingExam()
    {
        try
        {
            // get now in D-m-Y format
            var ado = ADO.Instance;

            // 1. Query for list of MaLHP
            var list_of_lop_hoc_phan = new List<string>();
            var MaSV = UserSession.Instance.UserId;
            string lhp_query = "SELECT MaLHP FROM SV_LopHP WHERE MaSV = @MaSV";
            var MaSV_param = ado.CreateParameter("@MaSV", MaSV);
            var lhp_result = ado.ExecuteQuery(lhp_query, MaSV_param);
            foreach (DataRow row in lhp_result.Rows)
            {
                list_of_lop_hoc_phan.Add(row["MaLHP"].ToString());
            }

            // 2. Get now and tomorrow
            var now = DateTime.Now.ToString("yyyy-MM-dd");
            var a_week_later = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            // 3. Query BaiThi with JOIN
            if (list_of_lop_hoc_phan.Count == 0)
            {
                return new DataTable(); // Return empty result if no classes found
            }

            // Create base query
            string query = @"SELECT BaiThi.*, GiangVien.*, LopHP.* 
                 FROM BaiThi 
                 LEFT JOIN GiangVien ON BaiThi.GV = GiangVien.MaGV 
                 LEFT JOIN LopHP ON BaiThi.LopHP = LopHP.MaLHP
                 WHERE TGBatDau BETWEEN @now AND @a_week_later 
                 AND BaiThi.LopHP IN (" + 
                 string.Join(",", list_of_lop_hoc_phan.Select((_, i) => "@MaLHP" + i)) + ")";

            // Create parameter list
            var parameters = new List<SqlParameter>
            {
                ado.CreateParameter("@now", now),
                ado.CreateParameter("@a_week_later", a_week_later)
            };

            // Add MaLHP parameters
            for (int i = 0; i < list_of_lop_hoc_phan.Count; i++)
            {
                parameters.Add(ado.CreateParameter("@MaLHP" + i, list_of_lop_hoc_phan[i]));
            }

            // Execute query with all parameters
            var result = ado.ExecuteQuery(query, parameters.ToArray());
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Co loi: " + ex);
            return null;
        }
    }
}