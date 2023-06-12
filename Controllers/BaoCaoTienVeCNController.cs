using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web4.Models;

namespace web4.Controllers
{
    public class BaoCaoTienVeCNController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand sqlc = new SqlCommand();
        SqlDataReader dt;
        //
        // GET: /test/
        public ActionResult Index()
        {
            return View();
        }
        public void connectSQL()
        {
            con.ConnectionString = "Data source= " + "118.69.109.109" + ";database=" + "B7_OPC" + ";uid=sa;password=Hai@thong";
        }
        public ActionResult BaoCaoTienVeCN(Account Acc)
        {
            DataSet ds = new DataSet();
            connectSQL();
            Acc.Ma_DvCs_1 = Request.Cookies["MA_DVCS"].Value;
            Acc.UserName = Request.Cookies["UserName"].Value;
            //string query = "exec usp_Vth_BC_BHCNTK_CN @_ngay_Ct1 = '" + Acc.From_date + "',@_Ngay_Ct2 ='"+ Acc.To_date+"',@_Ma_Dvcs='"+ Acc.Ma_DvCs_1+"'";
            string Pname = "[usp_Vth_BC_BHCNTK_CN]";

            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@_ma_dvcs", Acc.Ma_DvCs_1);
                    cmd.Parameters.AddWithValue("@_Ngay_Ct1", Acc.From_date);
                    cmd.Parameters.AddWithValue("@_Ngay_Ct2", Acc.To_date);
                    cmd.Parameters.AddWithValue("@_User_Name", Acc.Name);
                    sda.Fill(ds);
                }

            }

            return View(ds);
        }
        public ActionResult BCTVCN_Fill()
        {
            return View();
        }

       
	}
}    