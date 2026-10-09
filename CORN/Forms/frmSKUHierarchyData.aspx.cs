using System;
using System.Data;
using System.Web.UI.WebControls;
using CORNCommon.Classes;
using CORNBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUHierarchyData : System.Web.UI.Page
{
    readonly SkuHierarchyController _mController = new SkuHierarchyController();
    static int _principalId;
    static int _categoryId;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadPrincipal();
            this.LoadCategroy();
            TabContainer1.ActiveTabIndex = 0;
        }
    }

    #region Principal Tab

    /// <summary>
    /// Loads Principals To Principal Grid On Principal Tab And To All Principal Combos On Form
    /// </summary>
    private void LoadPrincipal()
    {
        
        DataTable dt = _mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()));
        GrdPrincipal.DataSource = dt;
        GrdPrincipal.Columns[0].Visible = true;
        GrdPrincipal.Columns[4].Visible = true;
        GrdPrincipal.Columns[5].Visible = true;
        GrdPrincipal.Columns[6].Visible = true;
        GrdPrincipal.DataBind();
        GrdPrincipal.Columns[0].Visible = false;
        GrdPrincipal.Columns[4].Visible = false;
        
        GrdPrincipal.Columns[6].Visible = false;
    }
    
    /// <summary>
    /// Sets Principal Data For Edit. This Function Runs When An Existing Principal Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        _principalId = int.Parse(GrdPrincipal.Rows[e.NewEditIndex].Cells[0].Text);
        txtPrincipalName.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[2].Text;
        if (GrdPrincipal.Rows[e.NewEditIndex].Cells[3].Text == "True")
        {
            ChIsMunalDiscount.Checked = true;
        }
        else
        {
            ChIsMunalDiscount.Checked = false;
        }
        txtAddress.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtNTN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;", "");
        txtSTRN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[6].Text.Replace("&nbsp;", "");
        txtPrincipalName.Enabled = true;
        txtAddress.Enabled = true;
        txtNTN.Enabled = true;
        txtSTRN.Enabled = true;
        txtPhoneNumber.Enabled = true;
        txtFaxNumber.Enabled = true;
        btnSavePrincipal.Text = "Update";
    }

    /// <summary>
    /// Deletes A Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _principalId = int.Parse(GrdPrincipal.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUDivision, _principalId);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsg.Text = "Wrong Command: first delete associated division";
        }
        else
        {
            _mController.UpdateHierarchy(Constants.SKUPrincipal, _principalId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadPrincipal();
            lblErrorMsg.Text = "";
        }
    }

    /// <summary>
    /// Sets PageIndex Of Principal Grid On Principal Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdPrincipal.PageIndex = e.NewPageIndex;
        this.LoadPrincipal();

    }

    /// <summary>
    /// Save Or Updates a Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSavePrincipal_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        switch (btnSavePrincipal.Text)
        {
            case "New":
                txtPrincipalName.Enabled = true;
                txtAddress.Enabled = true;
                txtNTN.Enabled = true;
                txtSTRN.Enabled = true;
                txtPhoneNumber.Enabled = true;
                txtFaxNumber.Enabled = true;
                btnSavePrincipal.Text = "Save";
                break;
            case "Save":
                if (txtPrincipalName.Text.Length == 0)
                {
                    lblErrorMsg.Text = "Must Enter Principal Name";
                    return;
                }
                _mController.InsertPrincipal(Constants.SKUPrincipal, Constants.IntNullValue, "", txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), ChIsMunalDiscount.Checked, txtAddress.Text, txtPhoneNumber.Text, txtSTRN.Text,txtNTN.Text);
                ClearAll();
                LoadPrincipal();
                break;
            case "Update":
                if (txtPrincipalName.Text.Length == 0)
                {
                    lblErrorMsg.Text = "Must Enter Principal Name";
                    return;
                }
                _mController.UpdatePrincipal(Constants.SKUPrincipal, _principalId, Constants.IntNullValue, "", txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), ChIsMunalDiscount.Checked, txtAddress.Text, txtNTN.Text, txtSTRN.Text);
                ClearAll();
                LoadPrincipal();
                break;
        }
    }

    private void ClearAll()
    {
        btnSavePrincipal.Text = "New";
        txtPrincipalName.Text = "";
        txtAddress.Text = "";
        txtNTN.Text = "";
        txtSTRN.Text = "";
        txtPhoneNumber.Text = "";
        txtFaxNumber.Text = "";
        txtPrincipalName.Enabled = false;
        txtAddress.Enabled = false;
        txtNTN.Enabled = false;
        txtSTRN.Enabled = false;
        txtFaxNumber.Enabled = false;
        txtPhoneNumber.Enabled = false;
    }

    #endregion
    
    #region Category Tab
    
    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    private void LoadCategroy()
    {
        DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        GrdCategory.DataSource = dt;
        GrdCategory.Columns[0].Visible = true;
        GrdCategory.DataBind();
        GrdCategory.Columns[0].Visible = false;
    }

    /// <summary>
    /// Sets Category Data For Edit. This Function Runs When An Existing Category Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ddCategoryDivision.SelectedValue = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        _categoryId = int.Parse(GrdCategory.Rows[e.NewEditIndex].Cells[0].Text);
        txtCategoryCode.Text = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        txtCategoryName.Text = GrdCategory.Rows[e.NewEditIndex].Cells[2].Text;
        txtCategoryName.Enabled = true;
        txtCategoryCode.Enabled = true;
        btnSaveCategory.Text = "Update";
    }

    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _categoryId = int.Parse(GrdCategory.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = _mController.SelectSkuHierarchy(Constants.SKUBrand, _categoryId);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsgCategory.Text = "Wrong Command: first delete associated Brand";
        }
        else
        {
            _mController.UpdateHierarchy(Constants.SKUCategory, _categoryId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadCategroy();
        }
    }

    /// <summary>
    /// Sets PageIndex Of Category Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdCategory.PageIndex = e.NewPageIndex;
        this.LoadCategroy();
    }

    /// <summary>
    /// Save Or Updates a Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        lblErrorMsgCategory.Text = "";
        if (btnSaveCategory.Text == "New")
        {
          //  txtCategoryCode.Text = this.GetAutoCode("CA", 0);
            txtCategoryName.Enabled = true;
            txtCategoryCode.Enabled = true;
            txtCategoryCode.Focus();
            btnSaveCategory.Text = "Save";
        }
        else if (btnSaveCategory.Text == "Save")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                lblErrorMsgCategory.Text = "Must Enter Category";
                return;
            }
            _mController.InsertHierarchy(Constants.SKUCategory, 0, txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
           // this.SetAutoCode("CA", long.Parse(txtCategoryCode.Text.Substring(2)));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            txtCategoryCode.Enabled = false;
            this.LoadCategroy();
            lblErrorMsgCategory.Text = "";

        }
        else if (btnSaveCategory.Text == "Update")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                lblErrorMsgCategory.Text = "Must Enter Category";
                return;
            }
            _mController.UpdateHierarchy(Constants.SKUCategory, _categoryId, 0, txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryCode.Enabled = false;
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
        }
    }

    #endregion
    
    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="preeFix">Prefix</param>
    /// <param name="codeType">Type</param>
    /// <returns>Code As String</returns>
    private string GetAutoCode(string preeFix,int codeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(preeFix, codeType, Constants.LongNullValue);
    }
    
    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="preeFix">Prefix</param>
    /// <param name="cValue">Value</param>
    private void SetAutoCode(string preeFix, long cValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(preeFix, 1, cValue);
    }
}