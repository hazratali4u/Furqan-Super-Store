using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit SKU Price
/// </summary>
public partial class SKU_Price : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadSKUS();
            LoadDistributor();
            txtFromdate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            txtCode.Attributes.Add("onChange", "SearchProduct();");
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
           
        }
    }
        
    /// <summary>
    /// Loads SKUS To SKU Combo
    /// </summary>
    private void LoadSKUS()
    {
        DataTable dtSKU;
        SKUPriceDetailController PController = new SKUPriceDetailController();
         dtSKU = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 4, DateTime.Parse(Session["CurrentWorkDate"].ToString()));        
         hfSKU.Value = GetJson(dtSKU);
    }

    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    private void LoadGrid(int SKUID)
    {
        SKUPriceDetailController mSKUController = new SKUPriceDetailController();
        DataTable dtsku = mSKUController.SelectSKuCurrentPrice(Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue,Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,SKUID);
        Grid_pricedetails.DataSource = dtsku.DefaultView;
        Grid_pricedetails.DataBind();
    }
    
    /// <summary>
    /// Loads Locations To Location ListBox
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);
    }

    /// <summary>
    /// Save Or Updates an SKU Price
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();
            int count = 0;
            int SKUID = Convert.ToInt32(dc.chkNull_0(hfSKUID.Value));
            string result = null;
                for (int i = 0; i < ChbDistributorList.Items.Count; i++)
                {
                    if (ChbDistributorList.Items[i].Selected)
                    {
                       count++;
                       result = mPriceController.InsertSKU_PRICES(int.Parse(ChbDistributorList.Items[i].Value), SKUID, 1, decimal.Parse(dc.chkNull_0(txtDistributorPrice.Text)),decimal.Parse(dc.chkNull_0(txtTaxPrices.Text)), decimal.Parse(dc.chkNull_0(txtDistributorPrice.Text)), decimal.Parse(dc.chkNull_0(txtTradePrice.Text)), decimal.Parse(dc.chkNull_0(txtRetailPrice.Text)), DateTime.Parse(Session["CurrentworkDate"].ToString()), decimal.Parse(dc.chkNull_0(txtSADTax.Text)));
                       LoadGrid(SKUID);
                    }                  
                }

                if (count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz Select any Location');", true);
                }
                if (result != null)
                {
                    ClearAll();
                }
        }
    }
    private void ClearAll() 
    {
        txtCode.Text = "";
        txtSKUName.Text = "";
        txtRetailPrice.Text = "";
        txtTradePrice.Text = "";
        txtDistributorPrice.Text = "";
        txtTaxPrices.Text = "";
    }
    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        serializer.MaxJsonLength = int.MaxValue;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    protected void btnLoadGrid_Click(object sender, EventArgs e)
    {

        if (hfSKUID.Value != "")
        {
            LoadGrid(Convert.ToInt32(hfSKUID.Value));
        }
        else
        {
            hfSKUID.Value = "";
            Grid_pricedetails.DataSource = null;
            Grid_pricedetails.DataBind();
        }
    }       
}
