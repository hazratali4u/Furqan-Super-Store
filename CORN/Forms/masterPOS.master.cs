using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Data;

public partial class Forms_masterPOS : System.Web.UI.MasterPage
{
    DataTable PurchaseSKU=new DataTable();
    DataTable dtFreeSKU=new DataTable();
    OrderEntryController or = new OrderEntryController();
    private int onhold=0;

    protected void Page_Load(object sender, EventArgs e)
    {
       
            loadOnHoldInvoicNumber();
    
    }
    
    public void loadOnHoldInvoicNumber()
    {
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 0, 0, 0, 0, Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
        lblonHold.Text = Convert.ToString(dtOrder.Rows.Count);
    }
    
    private void LoadPendingOrder()
    {
        ((DropDownList)ContentPlaceHolder1.FindControl("ddonHoldList")).Items.Clear();
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 0, 0, 0, 0, Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
        ((DropDownList)ContentPlaceHolder1.FindControl("ddonHoldList")).Items.Add(new clsListItems("Select To Unhold", Constants.LongNullValue.ToString()));
        lblonHold.Text = Convert.ToString( dtOrder.Rows.Count);
        clsWebFormUtil.FillDropDownList((DropDownList)ContentPlaceHolder1.FindControl("ddonHoldList"), dtOrder, 0, 1);
        Session.Add("dtOrder", dtOrder);
    }

   // private bool FindCustomer()
   // {
    //    if (((CheckBox)(ContentPlaceHolder1.FindControl("cbselectCustomer"))).Checked == true)
    //    {
    //        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
    //        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + ((TextBox)ContentPlaceHolder1.FindControl("txtOutletCode")).Text.Trim() + "'");
    //        if (foundRows.Length > 0)
    //        {
    //            Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
    //            return true;
    //        }
    //        return false;
    //    }
    //    return true;
   // }

    private void ClearMasterALL()
    {
        EnableDisableController(true);
        Session.Remove("PurchaseSKU");
        Session.Remove("dtFreeSKU");
      
        Session.Remove("CustomerID");
       

        CreatTable();
        CreateFreeSKU();
      
        LoadGird();
        LoadFreeGrid();

         ((TextBox)ContentPlaceHolder1.FindControl("txtGrossAmount")).Text = "";
         ((TextBox)ContentPlaceHolder1.FindControl("numtxtTotalExtraDiscnt")).Text = "";
         ((TextBox)ContentPlaceHolder1.FindControl("numTxtTotalGST")).Text = "";
         ((TextBox)ContentPlaceHolder1.FindControl("numTxtTotlAmnt")).Text = "";
         ((TextBox)ContentPlaceHolder1.FindControl("txtBalance")).Text = "";
         ((TextBox)ContentPlaceHolder1.FindControl("txtCashReceived")).Text = "";
         ((LinkButton)(ContentPlaceHolder1.FindControl("btnSaveOrder"))).Enabled = false;
       //  ((Button)(ContentPlaceHolder1.FindControl("btnToggleMode"))).Enabled = false;
         

    }
    
    private void EnableDisableController(bool CValue)
    {

        if (CValue == true)
        {
            if (((TextBox)ContentPlaceHolder1.FindControl("txtGrossAmount")).Text != "")
            {
                if (decimal.Parse(((TextBox)ContentPlaceHolder1.FindControl("txtGrossAmount")).Text) > 0)
                {
                    ((DropDownList)ContentPlaceHolder1.FindControl("drpCustomer")).Enabled = true;
                  
                }
            }
            else
            {
                ((DropDownList)ContentPlaceHolder1.FindControl("drpCustomer")).Enabled = false;
               
            }
        }
    }
    
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("DistributorId", typeof(int));
        PurchaseSKU.Columns.Add("SALE_ORDER_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("RETAIL_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_EXTRA_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("SED_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("NET_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(decimal));
        PurchaseSKU.Columns.Add("PACKSIZE", typeof(string));
        PurchaseSKU.Columns.Add("IS_DELETED", typeof(bool));
        PurchaseSKU.Columns.Add("SkuImage", typeof(string));
        PurchaseSKU.Columns.Add("COLOR", typeof(string));
        PurchaseSKU.Columns.Add("TRADE_PRICE", typeof(string));
        PurchaseSKU.Columns.Add("AMOUNT_TEMP", typeof(int));
        PurchaseSKU.Columns.Add("CHECK_DELETE", typeof(int));
        PurchaseSKU.Columns.Add("ORDER", typeof(int));
        Session.Add("PurchaseSKU", PurchaseSKU);
    }

    private void CreateFreeSKU()
    {
        dtFreeSKU = new DataTable();
        dtFreeSKU.Columns.Add("SKU_ID", typeof(int));
        dtFreeSKU.Columns.Add("SKU_Code", typeof(string));
        dtFreeSKU.Columns.Add("SKU_Name", typeof(string));
        dtFreeSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        dtFreeSKU.Columns.Add("Quantity", typeof(int));
        dtFreeSKU.Columns.Add("AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_RATE", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("PROMOTION_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_DETAIL_ID", typeof(int));
        dtFreeSKU.Columns.Add("PROMOTION_OFFER_ID", typeof(int));
        Session.Add("dtFreeSKU", dtFreeSKU);
    }

    private void LoadGird()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        ((GridView)ContentPlaceHolder1.FindControl("GrdPurchase")).DataSource = PurchaseSKU;
        ((GridView)ContentPlaceHolder1.FindControl("GrdPurchase")).DataBind();
    }

    private void LoadFreeGrid()
    {
        dtFreeSKU = (DataTable)Session["dtFreeSKU"];
         ((GridView)ContentPlaceHolder1.FindControl("GrdFreeSKU")).DataSource=dtFreeSKU;
         ((GridView)ContentPlaceHolder1.FindControl("GrdFreeSKU")).DataBind();
    }

    protected void btnNewCustomer_Click(object sender, EventArgs e)
    {
        btnEditinvoice.Visible = false;
        btnHold.Visible = false;
        btnUnhold.Visible = false;
        btnNewCustomer.Visible = false;
        btnpricelookup.Visible = false;

        Session.Add("CustomerID", ((DropDownList)(ContentPlaceHolder1.FindControl("drpCustomer"))).SelectedValue);

        if (((HiddenField)(ContentPlaceHolder1.FindControl("hfToggleMode"))).Value == "SALE MODE")
        {
            Session.Add("TogelBtnId", 1);//sale mode
        }
        else
        {
            Session.Add("TogelBtnId", 2);//refund mode
 
        }

        Server.Transfer("frmNewCustomerPOS.aspx");
    }
    
    protected void btnHold_Click(object sender, EventArgs e)
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        
        if (PurchaseSKU.Rows.Count > 0)
        {
            dtFreeSKU = (DataTable)Session["dtFreeSKU"];
       
           // if (FindCustomer())
          //  {
                bool isValid;
                
                string ManualID = ((HiddenField)(ContentPlaceHolder1.FindControl("hfToggleMode"))).Value == "SALE MODE" ? "1" : "2";
                string CustomerID =((DropDownList)(ContentPlaceHolder1.FindControl("drpCustomer"))).SelectedValue;
               
              //used 1 for sale mode
               
                DataControl _dc = new DataControl();
                OrderEntryController _mOrderController = new OrderEntryController();

                if (Session["SALE_ORDER_ID"] == null)
                {
                    isValid = _mOrderController.Add_Order(int.Parse(_dc.chkNull_0(Session["DISTRIBUTOR_ID"].ToString())), ManualID, 0, 0, 0, long.Parse(CustomerID), 0, 0, 0, int.Parse(_dc.chkNull_0(((RadioButtonList)(ContentPlaceHolder1.FindControl("RblPayMode"))).SelectedValue.ToString())),
                                decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("txtGrossAmount")).Text)), decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("numtxtTotalExtraDiscnt")).Text)),
                                0, decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("numTxtTotalGST")).Text)), decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("numTxtTotlAmnt")).Text)),
                                0, Constants.Order_Pending_Id, PurchaseSKU, dtFreeSKU, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 0, 0);
                    if (isValid)
                    {
                        onhold++;

                        ClearMasterALL();
                        
                    }
                }
                else
                {
                    isValid = _mOrderController.Update_Order(long.Parse(Session["SALE_ORDER_ID"].ToString()), int.Parse(Session["DISTRIBUTOR_ID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), long.Parse(CustomerID), int.Parse(_dc.chkNull_0(((RadioButtonList)(ContentPlaceHolder1.FindControl("RblPayMode"))).SelectedValue.ToString())),
                                                 decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("txtGrossAmount")).Text)), decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("numtxtTotalExtraDiscnt")).Text)), 0, decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("numTxtTotalGST")).Text)), decimal.Parse(_dc.chkNull_0(((TextBox)ContentPlaceHolder1.FindControl("numTxtTotlAmnt")).Text)), 0, PurchaseSKU, dtFreeSKU, int.Parse(Session["UserId"].ToString()));
                    if (!isValid) return;
                }

                if (isValid)
                {
                    ((DropDownList)ContentPlaceHolder1.FindControl("ddonHoldList")).Visible = false;
                    
                    ClearMasterALL();

                    Session.Remove("SALE_ORDER_ID");
                }
           // }
        }
        else 
        {
         ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Nothing To Hold');", true);

        }
        loadOnHoldInvoicNumber();
        LoadPendingOrder();
        ((TextBox)ContentPlaceHolder1.FindControl("txtskuCode")).Focus();
    }
    
    protected void btnUnhold_Click(object sender, EventArgs e)
    {
       if (lblonHold.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Nothing To Unhold');", true);

        }
        else
        {
            ContentPlaceHolder1.FindControl("ddonHoldList").Visible = true;
            ((Label)(ContentPlaceHolder1.FindControl("lblUnhold"))).Visible = true;
          //  ((DropDownList)(ContentPlaceHolder1.FindControl("ddEditInvoice"))).Visible = false;
            LoadPendingOrder();
        }
    }
   
  
    protected void btnpricelookup_Click(object sender, EventArgs e)
    {
        if (((TextBox)(ContentPlaceHolder1.FindControl("numtxtTotalExtraDiscnt"))).Text != "")
        {
            Session.Add("DiscountAmount", ((TextBox)(ContentPlaceHolder1.FindControl("numtxtTotalExtraDiscnt"))).Text);
        }
        if (((TextBox)(ContentPlaceHolder1.FindControl("txtAuthorisedBy"))).Text != "")
        {
            Session.Add("AuthorizedBy", ((TextBox)(ContentPlaceHolder1.FindControl("txtAuthorisedBy"))).Text);
        }
        if (((TextBox)(ContentPlaceHolder1.FindControl("txtCashReceived"))).Text != "")
        {
            Session.Add("CashRecives", ((TextBox)(ContentPlaceHolder1.FindControl("txtCashReceived"))).Text);
        }
        if (((TextBox)(ContentPlaceHolder1.FindControl("txtBalance"))).Text != "")
        {
            Session.Add("BalanceAmount", ((TextBox)(ContentPlaceHolder1.FindControl("txtBalance"))).Text);
        }
        if (((HiddenField)(ContentPlaceHolder1.FindControl("hfToggleMode"))).Value == "SALE MODE")
        {
            Session.Add("TogelBtnId", 1);//sale mode
        }
        else
        {
            Session.Add("TogelBtnId", 2);//refund mode

        }
        string quntity = "-1";
        if (((TextBox)(ContentPlaceHolder1.FindControl("txtQuantity"))).Text == "1")
        {
            quntity = "1";
            Session.Add("quantity", quntity);
        }
        else
        {
            quntity = ((TextBox)(ContentPlaceHolder1.FindControl("txtQuantity"))).Text;
            Session.Add("quantity", quntity);
        }




        if (((RadioButtonList)(ContentPlaceHolder1.FindControl("RblPayMode"))).SelectedValue == "214")
        {
            Session.Add("paymode", 214);
        }
        else if (((RadioButtonList)(ContentPlaceHolder1.FindControl("RblPayMode"))).SelectedValue == "215")
        {
            Session.Add("paymode", 215);
        }
        else
        {
            Session.Add("paymode", 217);
        }


        Session.Add("CustomerID", ((DropDownList)(ContentPlaceHolder1.FindControl("drpCustomer"))).SelectedValue);
      

        Server.Transfer("frmProductSearch.aspx");
    }

}
