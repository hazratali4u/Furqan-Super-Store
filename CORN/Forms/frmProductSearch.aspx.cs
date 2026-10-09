using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

public partial class Forms_frmProductSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
                
                txtSearch.Focus();
            
            
        }
    }
   
    private void loadSkuDetail()
     {
         int t = int.Parse(this.Session["DISTRIBUTOR_ID"].ToString());
         SKUPriceDetailController PController = new SKUPriceDetailController();
         DataTable Dtsku_Price = PController.SelectDataPrice(Convert.ToString(txtSearch.Text),Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
         if (Dtsku_Price.Rows.Count > 0)
         {
             GrdPurchase.DataSource = Dtsku_Price;
             GrdPurchase.DataBind();

             lblNoRecords.Visible = false;
         }
         else
         {
             txtSearch.Focus();
             lblNoRecords.Visible = true;
         }
        // Session.Add("skuDetail",Dtsku_Price);
         
     }
    protected  void selectValuandGOtoPos(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             // javascript function to call on row-click event
             string st = e.Row.Cells[0].Text;
             
             e.Row.Attributes.Add("onClick", "javascript:void SelectRow("+ st +");");
         }

         //if (e.Row.RowType == DataControlRowType.DataRow)
         //{

         //    string queryString = string.Empty;



         //    for (int x = 0; x < GrdPurchase.Columns.Count; x++)
         //    {

         //        string separator = (x == 0 ? "?" : "&");

         //        queryString += string.Format("{0}{1}={2}", separator, GrdPurchase.Columns[x].HeaderText, e.Row.Cells[x].Text);

         //    }



         //    e.Row.Attributes["ondblclick"] = string.Format("popItUp({0})", queryString);

         //}

     }
    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmOrderPOS.aspx?skuid=-1");
    }
    
    //[WebMethod]
    //public static string GetCustomers(string searchTerm, int pageIndex)
    //{
    //    string query = "[GetProducts]";
    //    SqlCommand cmd = new SqlCommand(query);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
    //    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
    //    cmd.Parameters.AddWithValue("@PageSize", 30);
    //    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
    //    return GetData(cmd, pageIndex).GetXml();
    //}

    //private static DataSet GetData(SqlCommand cmd, int pageIndex)
    //{
    //    string strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(strConnString))
    //    {
    //        using (SqlDataAdapter sda = new SqlDataAdapter())
    //        {
    //            cmd.Connection = con;
    //            sda.SelectCommand = cmd;
    //            using (DataSet ds = new DataSet())
    //            {
    //                sda.Fill(ds, "SKUS");
    //                DataTable dt = new DataTable("Pager");
    //                dt.Columns.Add("PageIndex");
    //                dt.Columns.Add("PageSize");
    //                dt.Columns.Add("RecordCount");
    //                dt.Rows.Add();
    //                dt.Rows[0]["PageIndex"] = pageIndex;
    //                dt.Rows[0]["PageSize"] = 30;
    //                dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
    //                ds.Tables.Add(dt);
    //                return ds;
    //            }
    //        }
    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadSkuDetail();
    }
}