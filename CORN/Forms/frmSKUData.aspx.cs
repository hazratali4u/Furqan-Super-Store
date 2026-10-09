using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmSKUData : System.Web.UI.Page
{
  
    readonly SkuController _mSkuController = new SkuController();
    readonly SkuHierarchyController _mHerController = new SkuHierarchyController();
   
    readonly DataControl _dc = new DataControl();
    private DataTable _mDt, _mSkuDt, _mSkuDt2;
    private static int _mSkuId;
    

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Populate_drpSKUCategory();
            LoadUom();
            LoadData();
            
           // LoadGrid();
            Page.Form.Enctype = "multipart/form-data";
           
        }
    }

    #region Load
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        _mDt = _mHerController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskucategory, _mDt, 0, 3, true);
    }
    private void LoadUom()
    {

        DataTable dtUoMs = _mSkuController.SelectUOMs(Constants.IntNullValue, null);
        clsWebFormUtil.FillDropDownList(drpUom, dtUoMs, 0, 1, true);
    }
    private void LoadData()
    {
        _mSkuDt2 = _mSkuController.SelectSkuInfo2(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
        _mSkuDt = _mSkuController.SelectSkuInfo2(Constants.IntNullValue, Constants.IntNullValue, int.Parse(_dc.chkNull_0(ddskucategory.SelectedValue)), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
       
        Session.Add("m_SKUDt", _mSkuDt);
        Session.Add("m_SKUDt2", _mSkuDt2);
    }
   
    /// <summary>
    /// Loads Data From Session To Grid
    /// </summary>
    private void LoadGrid()
    {
        //m_SKUDt = (DataTable)Session["m_SKUDt"];

        //switch (ddSearchType.SelectedIndex)
        //{
            
        //    case 1:
        //        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    case 2:
        //        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    case 3:
        //        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    case 4:
        //        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    default:
        //        m_SKUDt.DefaultView.RowFilter = "SKU_CODE" + " like '%" + "" + "%'";
        //        break; 
        //}
        //grdSKUData.DataSource = m_SKUDt.DefaultView;   
        //grdSKUData.DataBind();
       }
    private void LoadGrid2()
    {
        if (ddskucategory.Items.Count <= 0) return;

        var dt = _mSkuController.SelectSkuInfo3(Constants.IntNullValue, ddSearchType.SelectedValue, txtSeach.Text);
        grdSKUData.DataSource = dt;
        grdSKUData.DataBind();
    }

    #endregion

    #region Grid Operations

    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
   {
       Populate_drpSKUCategory();
       ddskucategory.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[0].Text;
       _mSkuId = int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[1].Text);
       txtbarcode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;","");
       txtskuname.Text = grdSKUData.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
       txtpacksize.Text = grdSKUData.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", "");
       txtUnit.Text = grdSKUData.Rows[e.NewEditIndex].Cells[6].Text.Replace("&nbsp;", "");
       char val= Convert.ToChar(grdSKUData.Rows[e.NewEditIndex].Cells[7].Text.Trim());
       DrpSKUTaxType.SelectedValue = Convert.ToString(val);

       if (int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[8].Text) > 0)
       {
           chkUom.Checked = true;
           drpUom.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[8].Text;
       }
       else 
       {
           chkUom.Checked = false;
       }

       btnSave.Text = "Update";
   }
    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {
       try
       {
           string result = _mSkuController.UpdateSKUS(false, false, Constants.CharNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
           Constants.DecimalNullValue, Constants.DecimalNullValue, null, int.Parse(grdSKUData.Rows[e.RowIndex].Cells[1].Text), null, null, null, null, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
           LoadData();
       }
        catch(Exception ex)
       {

       }
       //LoadGrid();
   }

    #endregion

    #region Check Duplicate

    private bool CheckDublicateSku()
    {
        _mSkuDt2 = (DataTable)Session["m_SKUDt2"];

        DataRow[] foundRows = _mSkuDt2.Select("SKU_CODE ='" + txtbarcode.Text + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }
    private bool CheckDublicateSku2()
    {
        _mSkuDt2 = (DataTable)Session["m_SKUDt2"];

        var foundRows = _mSkuDt2.Select("SKU_CODE ='" + txtbarcode.Text + "' AND SKU_ID <> '" + _mSkuId + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Click Operations

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        if (txtbarcode.Text == "")
        {
            lblErrorMsg.Text = "Must enter Sku Code";
            return;
        }
        if (txtskuname.Text == "")
        {
            lblErrorMsg.Text = "Must enter Sku Name";
            return;
        }
        int Uom = 0;
        if (chkUom.Checked)
        {
            Uom =int.Parse(drpUom.SelectedValue);
        }
        switch (btnSave.Text)
        {
            case "Save":
                if (CheckDublicateSku())
                {

                    _mSkuController.InsertSKUS2(true, true, char.Parse(DrpSKUTaxType.SelectedValue), Uom, 0, int.Parse(ddskucategory.SelectedValue),0, Constants.IntNullValue, 
                        0, 0, txtUnit.Text,txtbarcode.Text.ToUpper(), txtskuname.Text, null, txtpacksize.Text, int.Parse(Session["UserId"].ToString()),
                        int.Parse(Session["CompanyId"].ToString()), "", Convert.ToString(txtUnit.Text), 0, "", "");
              
                    ClearAll();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('SKU already exist of that Barcode.');", true);
                }
                break;
            case "Update":
                if (CheckDublicateSku2())
                {
                    _mSkuController.UpdateSKUS2(true, true, char.Parse(DrpSKUTaxType.SelectedValue), Uom, 0,
                        int.Parse(ddskucategory.SelectedValue), 0, Constants.IntNullValue, 0, 0, txtUnit.Text, _mSkuId, txtbarcode.Text.ToUpper(), txtskuname.Text, null, 
                        txtpacksize.Text, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), "", Convert.ToString(txtUnit.Text), 
                        0, "", "");

               
                    ClearAll();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('SKU already exist of that Barcode and Name.');", true);
                }
                break;
        }

        LoadData();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid2();
    }

    #endregion

    #region Sel/Index Change

    protected void ddskubrand_selectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
       
        }
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        //  LoadGrid();


    }

    #endregion

    private void ClearAll()
    {
        txtpacksize.Text = "";
        txtUnit.Text = "";
        txtskuname.Text = "";
        txtbarcode.Text = "";
        btnSave.Text = "Save";
        chkUom.Checked = false;
        lblErrorMsg.Text = "";
        //  Session.Remove("m_SKUDt");
        LoadData();
        LoadGrid2();


    }   
}