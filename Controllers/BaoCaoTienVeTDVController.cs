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
    public class BaoCaoTienVeTDVController : Controller
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
        //public ActionResult BaoCaoTienVeTDV(Account Acc)
        //{
        //    connectSQL();
        //    con.Open();
        //    sqlc.Connection = con;
        //    string Pname = "usp_Kcd_BaoCaoTienVe";
        //    Acc.Ma_DvCs_1 = Request.Cookies["MA_DVCS"].Value;
        //    Acc.UserName = Request.Cookies["UserName"].Value;
        //    SqlCommand com = new SqlCommand(Pname, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@_ma_dvcs", Acc.Ma_DvCs_1);
        //    com.Parameters.AddWithValue("@_Ngay_Ct1", Acc.From_date);
        //    com.Parameters.AddWithValue("@_Ngay_Ct2", Acc.To_date);
        //    com.Parameters.AddWithValue("@_user_name", Acc.Name);
        //    com.Parameters.AddWithValue("@_WEB", 1);
        //    com.Parameters.AddWithValue("@_Donvicoso", Acc.Ma_DvCs_1);
        //    SqlDataReader dr = com.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    return View(dt);
        //}
        public ActionResult BaoCaoTienVeTDV(Account Acc)
        {
            DataSet ds = new DataSet();
            connectSQL();
            Acc.Ma_DvCs_1 = Request.Cookies["MA_DVCS"].Value;
            Acc.UserName = Request.Cookies["UserName"].Value;
            //string query = "exec usp_Vth_BC_BHCNTK_CN @_ngay_Ct1 = '" + Acc.From_date + "',@_Ngay_Ct2 ='"+ Acc.To_date+"',@_Ma_Dvcs='"+ Acc.Ma_DvCs_1+"'";
            string Pname = "[usp_Kcd_BaoCaoTienVe]";

            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {
                cmd.CommandTimeout = 950;

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@_ma_dvcs", Acc.Ma_DvCs_1);
                    cmd.Parameters.AddWithValue("@_Ngay_Ct1", Acc.From_date);
                    cmd.Parameters.AddWithValue("@_Ngay_Ct2", Acc.To_date);
                    cmd.Parameters.AddWithValue("@_user_name", Acc.Name);
                    cmd.Parameters.AddWithValue("@_WEB", 1);
                    cmd.Parameters.AddWithValue("@_Donvicoso", Acc.Ma_DvCs_1);
                    sda.Fill(ds);
                }
            }


            return View(ds);

        }
        public ActionResult BCTVTDV_Fill()
        {
            return View();
        }
	}
}