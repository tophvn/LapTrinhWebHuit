using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Buoi4.Models;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;

namespace Buoi4.Controllers
{
    public class UserController1 : Controller
    {
        public IActionResult Index()
        {
            List<User1> danhsach = new List<User1>();
            string connStr = "workstation id=hoanghaisql.mssql.somee.com;packet size=4096;user id=hoanghai07077_SQLLogin_1;pwd=1osi174ffz;data source=hoanghaisql.mssql.somee.com;persist security info=False;initial catalog=hoanghaisql;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM USERS", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    danhsach.Add(new User1
                    {
                        Id = int.Parse(dr.GetValue(0).ToString()),
                        Name = dr.GetValue(1).ToString(),
                        Address = dr.GetValue(2).ToString(),
                        Avatar = dr.GetValue(4).ToString()
                    });
                }
            }

            return View(danhsach);
        }
    }
}