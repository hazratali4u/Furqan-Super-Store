using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;


public partial class Forms_frmSaleForce_Cash : System.Web.UI.Page
{
    private static int pSaleShortId;
    private DataTable _mSkuDt;
    private void LoadUsers()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpUser.Items.Clear();
            SaleForceController mDController = new SaleForceController();
             DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()),Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
             clsWebFormUtil.FillDropDownList(DrpUser, m_dt, 0, 3);
        }
        else
        {
            DrpUser.Items.Clear();
        }
    }
   
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }
    private void LoadGrid()
    {
       
        SaleForceController mSaleForce = new SaleForceController();

        DataTable dt = mSaleForce.GetSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        GrdOrder.DataSource = dt;
        GrdOrder.DataBind();

        Session.Add("_mSkuDt", dt);
    }
    private bool checkDuplicate()
    {
        DataTable _mSkuDt = (DataTable)Session["_mSkuDt"];
        if (btnAddNew.Text == "Save")
        {
            var foundRows = _mSkuDt.Select("SALE_FORCE_ID ='" + DrpUser.SelectedValue + "' AND DISTRIBUTOR_ID = '" + drpDistributor.SelectedValue + "'");
            if (foundRows.Length == 0)
            {
                return true;
            }
        }
        else
        {
            var foundRows = _mSkuDt.Select("SALE_FORCE_ID <>'" + DrpUser.SelectedValue + "' AND DISTRIBUTOR_ID = '" + drpDistributor.SelectedValue + "'");
            if (foundRows.Length == 0)
            {
                return true;
            }
        }
        return false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();
            LoadUsers();
            LoadGrid();
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        SaleForceController mController = new SaleForceController();
        bool flag=false;
        if (btnAddNew.Text == "Save")
        {
            if (checkDuplicate())
            {
                flag = mController.InsertSaleForceCash(int.Parse(DrpUser.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), decimal.Parse(txtAmount.Text), int.Parse(Session["UserId"].ToString()));
            }
        }
        else
        {
            if (checkDuplicate())
            {
                flag = mController.UpdateSaleForceCash(pSaleShortId, int.Parse(DrpUser.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), decimal.Parse(txtAmount.Text), int.Parse(Session["UserId"].ToString()));
            }
        }
        if (flag)
        {
            txtAmount.Text = "";
            btnAddNew.Text = "Save";
            LoadGrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured');", true);
        }
    }

    
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pSaleShortId =int.Parse(GrdOrder.Rows[e.NewEditIndex].Cells[0].Text);
        DrpUser.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;
 
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;

        btnAddNew.Text = "Update";
    }
}