using System;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Collections.Generic;
using System.Web.Services;
using System.Drawing;

public partial class Forms_frmOrderPOS : System.Web.UI.Page
{
    #region Variables

    readonly DataControl _dc = new DataControl();
    readonly DistributorController _mDist = new DistributorController();
    readonly OrderEntryController _mOrderController = new OrderEntryController();
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();

    string _expiryDate;
    string mop;
    DataTable _purchaseSku;
    DataTable _dtFreeSku;
    DataTable _psiDt;

    static int order = 0;
    int _saleInvId;
    decimal _cashRcd;
    decimal _ttamount;
    decimal _ntamount;
    decimal _discouunt;
   // private static int _mCustomerTypeId;
    private static int _mCustomerVolClassId;
    private int mTownId;
    private static int _rowId;
    private static int _unitType;
    public int Onhold = 0;
    public long NewCustomerId;
    private System.Drawing.Printing.PrintDocument _prnDocument;
    long _saleInvoiceId=0;
    private int Credit_Note_id;
    public int PrintType;
    private readonly Font _saleFont = new Font("Arial Narrow", 8, FontStyle.Bold);
    private static string HirarchyNameQuiz;
    private static string HirarchyNameQuiz2; //AFTER COMPANY NAME ON INVOICE IP_ADDREES OF DISTRIBUTOR
    private static string Address;
    public static string CompanyPhonNmbr;
    // for Report:
    private int CurrentY;
    private int CurrentX;
    private int leftMargin;
    private int rightMargin;
    private int topMargin;
    private int bottomMargin;
    private int InvoiceWidth;
    private int InvoiceHeight;
   
    private bool ReadInvoice;

    private int XNoOfuntit;
    private int YNoOfUnit;

    // for Invoice Head:
    private string InvTitle;
    private string InvTitle1;
    private string InvSubTitle1;
    private string InvSubTitle2;
    private string InvSubTitle3;
    private string InvSubTitle4;
    private string InvSubTitle5;
    // Font and Color:------------------
    // Title Font
    private Font InvTitleFont = new Font("Arial Narrow", 14, FontStyle.Bold);
    // Title Font height
    private int InvTitleHeight;
    private int InvSubTitleHeight;
    // SubTitle Font
    private Font InvSubTitleFont = new Font("Arial Narrow", 12, FontStyle.Regular);
    private Font InvSubTitle2Font = new Font("Arial Narrow", 10, FontStyle.Regular);
   // private Font InvSubTitleFont4 = new Font("Arial Narrow", 12, FontStyle.Bold);
    // SubTitle Font height
   
    // Invoice Font
    private Font InvoiceFont = new Font("Arial Narrow", 9, FontStyle.Regular);
    private Font InvoiceFont2 = new Font("Arial Narrow", 7, FontStyle.Bold);
    private Font InvoiceFont3 = new Font("Arial Narrow", 7, FontStyle.Bold);
    private Font InvoiceFont4 = new Font("Arial Narrow", 8, FontStyle.Bold);
    // Invoice Font height
    private int InvoiceFontHeight;
    // Blue Color
    private readonly SolidBrush _blueBrush = new SolidBrush(Color.Blue);
    // Red Color
    private readonly SolidBrush _redBrush = new SolidBrush(Color.Red);
    // Black Color
    private readonly SolidBrush _blackBrush = new SolidBrush(Color.Black);

   
    #endregion
    protected void Calculate()
    {
        try
        {
            
                decimal ObjGrossSales = 0;
                decimal ObjStandardDiscount = 0;
                decimal ObjExtraDiscount = 0;
                decimal ObjTotalGST = 0;
                decimal ObjTotalSED = 0;
                decimal ObjTotalTST = 0;
                decimal ObjFreeGSTAmount = 0;
                decimal ObjFreeGrossAmt = 0;
                decimal ObjFreeTST = 0;
                decimal ProductExtDist = 0;
                decimal ProductStdDist = 0;
                decimal amount = 0;


                OrderEntryController or = new OrderEntryController();
                SKUGroupController GroupCtl = new SKUGroupController();
                DataTable dt = new DataTable();
                //  dt = GetDataTable(GrdPurchase);// (DataTable)GrdPurchase.DataSource; //(DataTable)Session["PurchaseSKU"];
                dt = (DataTable)Session["PurchaseSKU"];
                foreach (DataRow dr in dt.Rows)
                {
                    dr["STANDARD_DISCOUNT"] = 0;
                    dr["STANDARD_DISCOUNT_PER"] = 0;
                    dr["EXTRA_DISCOUNT"] = 0;
                    dr["CLAIM_EXTRA_AMOUNT"] = 0;
                    dr["CLAIM_STANDARD_DISCOUNT"] = 0;
                    dr["CLAIM_PER"] = 0;
                    dr["SED_AMOUNT"] = 0;
                    dr["GST_AMOUNT"] = 0;
                    dr["TST_AMOUNT"] = 0;
                    dr["GST_RATE"] = 0;

                    amount = Convert.ToDecimal(dr["AMOUNT"]);

                }
                PromotionCollections_Controller Arpc = (PromotionCollections_Controller)Session["Arpc"];
                CreateFreeSku();
                LoadFreeGrid();

                if (dt.Rows.Count > 0)
                {
                    if (Arpc != null)
                    {
                        ArrayList ARlistOffer = or.GetPromotionOffers(Arpc, _mCustomerVolClassId, 90, dt, false);

                        for (int i = 0; i < ARlistOffer.Count; i++)
                        {
                            numtxtTotalExtraDiscnt.ReadOnly = false;

                            PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];

                            if (pofferCol.SKU_ID > 0)
                            {
                                #region slab on SKU
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                                    {

                                        if (pofferCol.Is_And == true)
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        if (pofferCol.Free_SKU_ID > 0)
                                        {
                                            DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
                                            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                            int RowId = FreeSKUExist(_dtFreeSku, pofferCol.Free_SKU_ID);
                                            if (foundRows.Length > 0)
                                            {
                                                if (RowId < 0)
                                                {
                                                    DataRow dr1 = _dtFreeSku.NewRow();
                                                    dr1["P_SKU_ID"] = dr["SKU_ID"];
                                                    dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                    dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                    dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];

                                                    if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                    {

                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        dr1["TST_AMOUNT"] = 0;

                                                    }
                                                    else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                    }
                                                    else
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["TST_AMOUNT"] = 0;
                                                    }

                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                    _dtFreeSku.Rows.Add(dr1);
                                                }
                                                else
                                                {

                                                    DataRow dr1 = _dtFreeSku.Rows[RowId];
                                                    ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                    dr1["P_SKU_ID"] = dr["SKU_ID"];
                                                    dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                    dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                    dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                    dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;

                                                    if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                    {

                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        dr1["TST_AMOUNT"] = 0;

                                                    }
                                                    else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                    }
                                                    else
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    }
                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                }

                                            }
                                        }
                                        Session.Add("dtFreeSKU", _dtFreeSku);
                                        LoadFreeGrid();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region slab on SKU Group

                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                                    {


                                        if (pofferCol.Is_And == true)
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()) - decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                    }
                                    #region Free SKU

                                    if (pofferCol.Free_SKU_ID > 0)
                                    {
                                        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
                                        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                        int RowId = FreeSKUExist(_dtFreeSku, pofferCol.Free_SKU_ID);
                                        if (foundRows.Length > 0)
                                        {
                                            if (RowId < 0)
                                            {
                                                DataRow dr1 = _dtFreeSku.NewRow();
                                                dr1["P_SKU_ID"] = dr["SKU_ID"];
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                _dtFreeSku.Rows.Add(dr1);
                                            }
                                            else
                                            {

                                                DataRow dr1 = _dtFreeSku.Rows[RowId];
                                                ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                dr1["P_SKU_ID"] = dr["SKU_ID"];
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            }
                                            Session.Add("dtFreeSKU", _dtFreeSku);
                                            LoadFreeGrid();

                                        }
                                    }
                                    #endregion



                                }
                                #endregion
                            }

                        }
                    }
                    #region Recalculate Products

                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductStdDist = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                        if (ProductStdDist > 0)
                        {
                            dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                            // dr["CLAIM_STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_PER"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())));
                            //   dr["SED_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString())) + decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString()));
                        }
                        ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                        ObjStandardDiscount += decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        ObjExtraDiscount += decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        ObjTotalSED += decimal.Parse(_dc.chkNull_0(dr["SED_AMOUNT"].ToString()));


                        #region GST Calculation
                        decimal TempAmount = decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                        decimal TempDiscount = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        decimal TempExtraDisAmt = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(_dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                        dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) + decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(_dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        ObjTotalGST += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                        ObjTotalTST += decimal.Parse(_dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region Reversecalulate Products
                    if (amount > 0)
                    {
                        //decimal ObjGrossSales = 0;
                        decimal ObjTaxableAmount = 0;
                        decimal ObjClaimAblePer = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));

                        }
                        if (decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) > 0)
                        {
                            ProductExtDist = decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) / (ObjGrossSales - ObjTaxableAmount);
                        }

                        ObjGrossSales = 0;

                        foreach (DataRow dr in dt.Rows)
                        {

                            decimal TempAmount = 0;
                            decimal TempDiscount = 0;
                            decimal TempExtraDisAmt = 0;

                            // dr["EXTRA_DISCOUNT"] = ProductExtDist * decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                            //dr["STANDARD_DISCOUNT"] = ProductStdDist * decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));

                            // dr["SED_AMOUNT"] = ObjClaimAblePer * decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));

                            #region GST Calculation

                            TempAmount = decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                            //  TempDiscount = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                            //  TempExtraDisAmt = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));

                            //  dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(_dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                            dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt);

                            ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                            //  ObjTotalGST += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                            // ObjTotalTST += decimal.Parse(_dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                            // ObjTotalSED += decimal.Parse(_dc.chkNull_0(dr["SED_AMOUNT"].ToString()));

                            //    ObjStandardDiscount += decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                            //    ObjExtraDiscount += decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                            #endregion
                        }
                    }
                    #endregion
                }
                #region Set Total Values
                // ManualDiscount =decimal.Parse( _dc.chkNull_0(txtManualDiscount.Text));
                txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                if (ObjGrossSales != 0)
                {
                    numtxtTotalExtraDiscnt.Text = Convert.ToString(Math.Round(ObjStandardDiscount, 2));
                    numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST + ObjFreeGSTAmount, 2));
                    numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjGrossSales - ObjStandardDiscount + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST, 2));
                    txtBalance.Text = (Math.Round(Convert.ToDecimal(_dc.chkNull_0(numTxtTotlAmnt.Text)) - (Convert.ToDecimal(_dc.chkNull_0(txtCashReceived.Text))), 2)).ToString();
                }
                else
                {

                    numtxtTotalExtraDiscnt.Text = "0.00";
                    //  ObjStandardDiscount = 0.00;
                    numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST + ObjFreeGSTAmount, 2));
                    numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjGrossSales - Convert.ToDecimal(numtxtTotalExtraDiscnt.Text) + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST, 2));
                    txtBalance.Text = (Math.Round(Convert.ToDecimal(_dc.chkNull_0(numTxtTotlAmnt.Text)) - (Convert.ToDecimal(_dc.chkNull_0(txtCashReceived.Text))), 2)).ToString();

                }

                #endregion

                btnSaveOrder.Enabled = true;
                if (RblPayMode.SelectedIndex != 2)
                {

                    txtCashReceived.ReadOnly = false;

                }
                else
                {
                    txtCashReceived.ReadOnly = true;

                }
            
        }

        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
       
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
        try
        {            
            LoadProduct();
            txtQuantity.Text = "1";
            txtstartDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            txtEndDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            lblCurrentWorkingDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            txtskuCode.Attributes.Add("onChange", "SearchProduct();");
            if (Session["TogelBtnId"] != null)
            {
                if (Session["TogelBtnId"].ToString() == "2")
                {
                    btnToggleMode.Value = "REFUND MODE";
                    btnToggleMode.Attributes.Add("class", "BtnModereturn");
                }
                else
                {
                    btnToggleMode.Value = "SALE MODE";
                    btnToggleMode.Attributes.Add("class","BtnModesale");

                }
                hfToggleMode.Value = btnToggleMode.Value;
                Session.Remove("TogelBtnId");
            }
           
            
            LoadloginDetail();

           // ((LinkButton)Master.FindControl("btnEditinvoice")).Enabled = true;
            ((LinkButton)Master.FindControl("btnHold")).Enabled = true;
            ((LinkButton)Master.FindControl("btnUnhold")).Enabled = true;
            ((LinkButton)Master.FindControl("btnNewCustomer")).Enabled = true;
            ((LinkButton)Master.FindControl("btnpricelookup")).Enabled = true;
            ((Panel)Master.FindControl("ponholdpanel")).Enabled = true;


            
            _unitType = 0;
 
            if (Request.QueryString["skuid"] != null)
            {
                var skuid = Request.QueryString["skuid"];
                LoadGird();

                DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];

                if (Session["DiscountAmount"] != null)
                {
                    numtxtTotalExtraDiscnt.Text = Session["DiscountAmount"].ToString();
                }
                if (Session["AuthorizedBy"] != null)
                {
                    txtAuthorisedBy.Text = Session["AuthorizedBy"].ToString();
                }
                if (Session["CashRecives"] != null)
                {
                    txtCashReceived.Text = Session["CashRecives"].ToString();
                }
                if (Session["BalanceAmount"] != null)
                {
                    txtBalance.Text = Session["BalanceAmount"].ToString();
                }
                //For Payment Mode
                if (Session["paymode"] != null)
                {
                    var sesionPaymode = (Session["paymode"].ToString());

                    switch (sesionPaymode)
                    {
                        case "214":
                            RblPayMode.SelectedValue = "214";
                            break;
                        case "215":
                            RblPayMode.SelectedValue = "215";
                            break;
                        default:
                            RblPayMode.SelectedValue = "217";
                            break;
                    }
                    Session.Remove("paymode");
                }
              
                if (skuid != "-1")
                {
                    DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + skuid + "'");
                    txtskuCode.Text = foundRows[0]["SKU_CODE"].ToString();
                    txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    txtUnitRate.Text = foundRows[0]["TRADE_PRICE"].ToString();
                    if (Session["quantity"] != null)
                    {
                        var sesionQuantity = (Session["quantity"].ToString());

                        txtQuantity.Text = sesionQuantity == "1" ? "1" : sesionQuantity;
                        Session.Remove("quantity");
                    }


                    btnSave_Click(sender, e);
                    var scriptManager = ScriptManager.GetCurrent(Page);
                    if (scriptManager != null) scriptManager.SetFocus(txtskuCode);
                }
                //   calculate();
            }
            else
            {
                CreatTable();
            }
            LoadPromotion();
            LoadCustomerData();
            GetLicense();

        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    #region DataTable Creation

    private void CreatTable()
    {
        _purchaseSku = new DataTable();
        _purchaseSku.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        _purchaseSku.Columns.Add("DistributorId", typeof(int));
        _purchaseSku.Columns.Add("SALE_ORDER_ID", typeof(int));
        _purchaseSku.Columns.Add("SKU_ID", typeof(int));
        _purchaseSku.Columns.Add("SKU_Code", typeof(string));
        _purchaseSku.Columns.Add("SKU_Name", typeof(string));
        _purchaseSku.Columns.Add("BATCH_NO", typeof(string));
        _purchaseSku.Columns.Add("UNIT_PRICE", typeof(decimal));
        _purchaseSku.Columns.Add("QUANTITY_UNIT", typeof(decimal));
        _purchaseSku.Columns.Add("AMOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("STANDARD_DISCOUNT_PER", typeof(decimal));
        _purchaseSku.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("RETAIL_AMOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("GST_RATE", typeof(decimal));
        _purchaseSku.Columns.Add("GST_AMOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("TST_AMOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("CLAIM_EXTRA_AMOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("CLAIM_STANDARD_DISCOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("CLAIM_PER", typeof(decimal));
        _purchaseSku.Columns.Add("SED_AMOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("NET_AMOUNT", typeof(decimal));
        _purchaseSku.Columns.Add("QUANTITY_CTN", typeof(decimal));
        _purchaseSku.Columns.Add("PACKSIZE", typeof(string));
        _purchaseSku.Columns.Add("IS_DELETED", typeof(bool));
        _purchaseSku.Columns.Add("SkuImage", typeof(string));
        _purchaseSku.Columns.Add("COLOR", typeof(string));
        _purchaseSku.Columns.Add("TRADE_PRICE", typeof(string));
        _purchaseSku.Columns.Add("AMOUNT_TEMP", typeof(int));
        _purchaseSku.Columns.Add("CHECK_DELETE", typeof(int));
        _purchaseSku.Columns.Add("ORDER", typeof(int));
      
        Session.Add("PurchaseSKU", _purchaseSku);
    }
    private void CreateFreeSku()
    {
        _dtFreeSku = new DataTable();
        _dtFreeSku.Columns.Add("SKU_ID", typeof(int));
        _dtFreeSku.Columns.Add("SKU_Code", typeof(string));
        _dtFreeSku.Columns.Add("SKU_Name", typeof(string));
        _dtFreeSku.Columns.Add("UNIT_PRICE", typeof(decimal));
        _dtFreeSku.Columns.Add("Quantity", typeof(decimal));
        _dtFreeSku.Columns.Add("AMOUNT", typeof(decimal));
        _dtFreeSku.Columns.Add("GST_RATE", typeof(decimal));
        _dtFreeSku.Columns.Add("GST_AMOUNT", typeof(decimal));
        _dtFreeSku.Columns.Add("TST_AMOUNT", typeof(decimal));
        _dtFreeSku.Columns.Add("PROMOTION_ID", typeof(int));
        _dtFreeSku.Columns.Add("BASKET_ID", typeof(int));
        _dtFreeSku.Columns.Add("BASKET_DETAIL_ID", typeof(int));
        _dtFreeSku.Columns.Add("PROMOTION_OFFER_ID", typeof(int));

        _dtFreeSku.Columns.Add("CHECK_DELETE", typeof(int));
        _dtFreeSku.Columns.Add("AMOUNT_TEMP", typeof(int));
        _dtFreeSku.Columns.Add("P_SKU_ID", typeof(int));
        Session.Add("dtFreeSKU", _dtFreeSku);
    }

    #endregion
      
    private bool CheckDublicateSku()
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = _purchaseSku.Select("SKU_CODE  = '" + txtskuCode.Text + "'");
        
            if (foundRows.Length == 0)
            {
                return true;
            }
            return false;
           
    }
        
    private void EnableDisableController(bool cValue)
    {
        try
        {
            if (cValue != true) return;
            if (txtGrossAmount.Text == "") return;
            if (decimal.Parse(txtGrossAmount.Text) <= 0) return;
            drpCustomer.Enabled = true;
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    private void DisableControls()
    {
       
        txtCashReceived.Enabled = false;
        numTxtTotalGST.Enabled = false;
        txtGrossAmount.Enabled = false;
        numTxtTotlAmnt.Enabled = false;
        RblPayMode.Enabled = false;
        txtskuCode.Enabled = false;
        btnCancel.Enabled = false;
        btnCalculate.Enabled = false;
        btnSaveOrder.Enabled = false;
        btnRePrint.Enabled = false;
       
        PnlPrintInvoice.Visible = true;
        btn_exitprint.Visible = true;

        //((LinkButton)Master.FindControl("btnEditinvoice")).Enabled = false;
        ((LinkButton)Master.FindControl("btnHold")).Enabled = false;
        ((LinkButton)Master.FindControl("btnUnhold")).Enabled = false;
        ((LinkButton)Master.FindControl("btnpricelookup")).Enabled = false;

    }
 
    private void EnableControls()
    {
        if (Session["CustName"] != null)
        {
            if (Session["CustName"].ToString() != "")
            {
               // txtOutletName.Text = Session["CustName"].ToString();
               // txtOutletCode.Text = Session["CustCode"].ToString();
            }
        }
        btn_exitprint.Visible = false;
        PnlPrintInvoice.Visible = false;

        txtQuantity.Text = "1";
        btnCancel.Enabled = true;
        btnCalculate.Enabled = true;
        btnSaveOrder.Enabled = true;
        btnRePrint.Enabled = true;
       
        txtCashReceived.Enabled = true;
        numTxtTotalGST.Enabled = true;
        txtGrossAmount.Enabled = true;
        numTxtTotlAmnt.Enabled = true;
        RblPayMode.Enabled = true;
        txtskuCode.Enabled = true;
        //((LinkButton)Master.FindControl("btnEditinvoice")).Enabled = true;
        ((LinkButton)Master.FindControl("btnHold")).Enabled = true;
        ((LinkButton)Master.FindControl("btnUnhold")).Enabled = true;
        ((LinkButton)Master.FindControl("btnpricelookup")).Enabled = true;

    }
   
    #region IndexChange

    protected void ddonHoldList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (long.Parse(ddonHoldList.SelectedValue) != Constants.LongNullValue)
            {
                DataTable dt = (DataTable)Session["dtOrder"];
                DataRow[] foundRows = dt.Select("SALE_ORDER_ID  = '" + ddonHoldList.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    Session.Add("SALE_ORDER_ID", ddonHoldList.SelectedValue);
                    if (foundRows[0]["MANUAL_ORDER_ID"].ToString() == "1")
                    {
                        btnToggleMode.Value = "SALE MODE";
                        btnToggleMode.Attributes.Add("class", "BtnModesale");
                        //sale mode
                    }
                    else
                    {
                        btnToggleMode.Value = "REFUND MODE";
                        btnToggleMode.Attributes.Add("class", "BtnModereturn");
                        //refund mode
                    }
                    hfToggleMode.Value = btnToggleMode.Value;
                    txtGrossAmount.Text = (Math.Round(decimal.Parse(foundRows[0]["TOTAL_AMOUNT"].ToString()), 2)).ToString();
                    decimal extraDiscount = decimal.Parse(foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString());
                    numtxtTotalExtraDiscnt.Text = (Math.Round(extraDiscount, 2)).ToString();
                    numTxtTotalGST.Text = (Math.Round(decimal.Parse(foundRows[0]["GST_AMOUNT"].ToString()), 2)).ToString();
                    numTxtTotlAmnt.Text = (Math.Round(decimal.Parse(foundRows[0]["TOTAL_NET_AMOUNT"].ToString()), 2)).ToString();
                    
                    numtxtTotalExtraDiscnt.Text = (Math.Round(decimal.Parse(foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString()), 2)).ToString();
                    Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
                    drpCustomer.SelectedValue=foundRows[0]["CUSTOMER_ID"].ToString();
                    ExistenOrderDetail(long.Parse(ddonHoldList.SelectedValue));
                  
                    EnableDisableController(false);
                    btnSaveOrder.Enabled = true;
                    ClearAll();
                    
                    RblPayMode.SelectedValue = foundRows[0]["ORDER_TYPE_ID"].ToString();
                   
                    ddonHoldList.Visible = false;
                    lblUnhold.Visible = false;
                }
            }
            else
            {
                CreateFreeSku();
                CreatTable();
                LoadGird();
                LoadFreeGrid();
                ClearMasterAll();
                
                ddonHoldList.Visible = false;

            }
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    protected void ddEditInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddEditInvoice.Items.Count > 0)
            {
                ClearMasterAll();
                ClearAll();
                DataTable dt = (DataTable)Session["dtDoc"];
                DataRow[] foundRows = dt.Select("DocID  = '" + ddEditInvoice.SelectedValue + "'");
                if (foundRows.Length <= 0) return;
                LoadCustomerData();
               // txtOutletCode.Text = dt.Rows[0]["CUSTOMER_CODE"].ToString();
               // txtOutletName.Text = dt.Rows[0]["CUSTOMER_NAME"].ToString();
                RblPayMode.SelectedValue = dt.Rows[0]["PayMode"].ToString();

                LoadDocumentDetail(Convert.ToInt64(ddEditInvoice.SelectedValue), 0);
                btnSaveOrder.ToolTip = "Update";
            }

        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
  
    protected void RblPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RblPayMode.SelectedIndex == 2)
        {
            txtCashReceived.Text = "";
            txtBalance.Text = "";
            txtCashReceived.ReadOnly = true;
        }
        else
        {
            txtCashReceived.ReadOnly = false;

            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.SetFocus(txtskuCode);
        }
        if (hfToggleMode.Value == "REFUND MODE")
        {
            btnToggleMode.Value = "REFUND MODE";
            btnToggleMode.Attributes.Add("class", "BtnModereturn");
        }
        else
        {
            btnToggleMode.Value = "SALE MODE";
            btnToggleMode.Attributes.Add("class", "BtnModesale");
        }
      
    }
    #endregion

    #region Click OPerations

   
    //protected void btnToggleMode_Click(object sender, EventArgs e)
    //{
    //    if (btnToggleMode.Text == "SALE MODE")
    //    {
    //        btnToggleMode.Text = "REFUND MODE";
    //        btnToggleMode.CssClass = "BtnModereturn";
    //        txtQuantity.Text = "-1";
           
    //    }
    //    else
    //    {
    //        btnToggleMode.Text = "SALE MODE";
    //        btnToggleMode.CssClass = "BtnModesale";
    //        txtQuantity.Text = "1";
            
    //    }
    //    txtskuCode.Focus();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataControl _dc = new DataControl();

            decimal Quantity = decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
            
                btnToggleMode.Disabled = true;
            
                DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];

                DataRow[] foundRows = Dtsku_Price.Select("SKU_CODE  = '" + txtskuCode.Text + "'");

                if (foundRows.Length > 0)
                {
                    _purchaseSku = (DataTable)Session["PurchaseSKU"];

                    decimal mTradePrice = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));

                    if (btnSave.ToolTip == "Add Sku")
                    {

                        if (hfToggleMode.Value == "SALE MODE")//code for order invoice without negative entry
                        {
                            if (Quantity > 0)
                            {
                                if (CheckDublicateSku())
                                {
                                    DataRow dr = _purchaseSku.NewRow();
                                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];


                                    dr["ORDER"] = order + 1;
                                    order = order + 1;

                                   
                                        dr["QUANTITY_UNIT"] = Quantity;

                                  
                                    dr["UNIT_PRICE"] = mTradePrice.ToString();

                                   
                                            dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                                            dr["TST_AMOUNT"] = 0;
                                            dr["GST_RATE"] = 0;

                                     



                                    dr["GST_AMOUNT"] = 0;
                                    dr["NET_AMOUNT"] = 0;
                                    _purchaseSku.Rows.Add(dr);
                                }
                                else
                                {
                                    _purchaseSku = (DataTable)Session["PurchaseSKU"];
                                    DataRow[] foundRowsexist = _purchaseSku.Select("SKU_CODE  = '" + txtskuCode.Text + "'");//QUANTITY_UNIT

                                    
                                    if (foundRowsexist.Length > 0)
                                    {
                                        foundRowsexist[0]["QUANTITY_UNIT"] = decimal.Parse(foundRowsexist[0]["QUANTITY_UNIT"].ToString()) + Quantity;
                                        foundRowsexist[0]["AMOUNT"] = decimal.Parse(foundRowsexist[0]["AMOUNT"].ToString()) + (mTradePrice * Quantity);
                                    }
                                    

                                }
                            }
                        }
                        else //Refund Mode
                        {
                            _purchaseSku = (DataTable)Session["PurchaseSKU"];
                              DataRow[] foundRowsexist = _purchaseSku.Select("SKU_CODE  = '" + txtskuCode.Text + "'");

                              if (foundRowsexist.Length == 0)
                              {
                                  DataRow dr = _purchaseSku.NewRow();
                                  dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                                  dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                  dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                  dr["ORDER"] = order + 1;
                                  order = order + 1;


                                
                                      dr["QUANTITY_UNIT"] = Quantity;

                                 
                                  dr["UNIT_PRICE"] = mTradePrice.ToString();

                                 
                                          dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                                          dr["TST_AMOUNT"] = 0;
                                          dr["GST_RATE"] = 0;

                                  dr["GST_AMOUNT"] = 0;
                                  dr["NET_AMOUNT"] = 0;
                                  _purchaseSku.Rows.Add(dr);
                              }
                              else
                              {
                                  //if -1 then update existing else 
                                  if (Quantity < 0)
                                  {
                                      foundRowsexist[0]["QUANTITY_UNIT"] = decimal.Parse(foundRowsexist[0]["QUANTITY_UNIT"].ToString()) + Quantity;
                                      foundRowsexist[0]["AMOUNT"] = decimal.Parse(foundRowsexist[0]["AMOUNT"].ToString()) + (mTradePrice * Quantity);
                                  }
                              }  
                        }
                    }


                    Session.Add("PurchaseSKU", _purchaseSku);
                    EnableDisableController(false);
                    LoadGird();
                    ClearAll12();
                    
                    btnSaveOrder.Enabled = true;


                    
                    Calculate();
                  //  numTxtTotlAmnt.Attributes.Add("readonly", "readonly");
                    

                    if (hfToggleMode.Value == "REFUND MODE")
                    {
                        btnToggleMode.Value = "REFUND MODE";
                        btnToggleMode.Attributes.Add("class", "BtnModereturn");
                    }
                    else
                    {
                        btnToggleMode.Value = "SALE MODE";
                        btnToggleMode.Attributes.Add("class", "BtnModesale");
                    }
                    var current = ScriptManager.GetCurrent(Page);
                    if (current != null) current.SetFocus(txtskuCode);
                }
        }
        catch (Exception ej)
        {
            string t = ej.Message.ToString();
        }
       
    }

    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        try
        {
                string manualId = hfToggleMode.Value != "SALE MODE" ? "1" : "2";

                _purchaseSku = (DataTable)Session["PurchaseSKU"];
                if (_purchaseSku.Rows.Count > 0)
                {
                    _dtFreeSku = (DataTable)Session["dtFreeSKU"];

                    decimal totalAmount = decimal.Parse(_dc.chkNull_0(txtGrossAmount.Text));
                    decimal discount = decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text));
                    decimal gst = decimal.Parse(_dc.chkNull_0(numTxtTotalGST.Text));

                    decimal netAmount = 0;
                    if (totalAmount < 0)
                    {
                        netAmount = (totalAmount + discount) - gst;

                    }
                    else
                    {
                        netAmount = (totalAmount - discount) + gst;

                    }

                    //if (btnSaveOrder.ToolTip == "Save")
                   // {
                        long orderId= 0;

                        if (ddonHoldList.SelectedValue != "")
                        {
                            orderId = long.Parse(ddonHoldList.SelectedValue);
                        }
                        _saleInvoiceId = _mOrderController.Add_Invoice2(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), manualId, mTownId, 0, int.Parse(Session["PRINCIPAL_ID"].ToString()), long.Parse(drpCustomer.SelectedValue), 0, 0, 0, orderId,
                        decimal.Parse(_dc.chkNull_0(txtGrossAmount.Text)), decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(_dc.chkNull_0(txtCashReceived.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotalGST.Text)), netAmount, 0, int.Parse(RblPayMode.SelectedValue.ToString()),
                        _purchaseSku, _dtFreeSku, int.Parse(Session["UserId"].ToString()), decimal.Parse(_dc.chkNull_0(txtCashReceived.Text)), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 0, 0, _dc.chkNull_0(txtAuthorisedBy.Text));
                       
                        if (_saleInvoiceId == -2 || _saleInvoiceId == -1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insertion Failed!!!.');", true);
                        }
                        else
                        {
                            if (chkPrint.Checked)
                            {
                                if (System.Configuration.ConfigurationManager.AppSettings["IsPrint"] == "1")
                                {

                                    if (txt_chkprint.Text != "")
                                    {
                                        int i = int.Parse(txt_chkprint.Text);

                                        if (i > 0)
                                        {
                                            for (int g = 0; g < i; g++)
                                            {
                                                PrintReport(1);
                                            }

                                        }
                                    }
                                }
                            }
                            ClearMasterAll();
                            if (orderId > 0) {
                                LoadOnHoldInvoicNumber();
                            }
                            btnToggleMode.Disabled = false;

                            txtskuCode.Focus();
                        }
                  //  }
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No product added');", true);
                }
           
        }
        catch (Exception eee)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Insertion Failed!!!.');", true);
        }
        
    }
    protected void btnVoid_Click(object sender, EventArgs e)//cancel button click 
    {
        
        if (Session["SALE_ORDER_ID"] != null)
        {
            long OrderID = Convert.ToInt64(Session["SALE_ORDER_ID"]);

            _mOrderController.Update_Order(OrderID);
            LoadOnHoldInvoicNumber();
        }


        ClearAll();
        ClearMasterAll();
        btnToggleMode.Disabled = false;

        txtskuCode.Focus();
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        //Session.RemoveAll();
        Session.Remove("PurchaseSKU");
        Session.Remove("dtFreeSKU");
        Session.Remove("psi_dt");
        Response.Redirect("Home.aspx?");
    }

    //private void Credit_Note_Click(object sender, EventArgs e)
    //{

    //    if (FindCustomer())
    //    {
    //        Credit_Note_id = _mOrderController.Add_CreditNote(decimal.Parse(_dc.chkNull_0(numTxtTotlAmnt.Text)), Int32.Parse(Session["CUSTOMER_ID"].ToString()), 0, 2);
    //        if (System.Configuration.ConfigurationManager.AppSettings["IsPrint"] == "1")
    //        {
    //            PrintReport(2);
    //        }
    //        ClearAll();
    //        ClearMasterAll();
    //        btnToggleMode.Text = "SALE MODE";
    //        btnToggleMode.CssClass = "BtnModesale";
    //        btnToggleMode.Enabled = true;
    //    }
    //}

    protected void btnViewSalesReport_Click(object sender, EventArgs e)
    {
        int type = 1;
        if (chkMalee.Checked)
        {
            type = 2;
            
        }
        showReport(type);
    }

   

    protected void btn_exitprint_Click(object sender, EventArgs e)
    {

        EnableControls();
        Session.Remove("psi_dt");
        GrdPrintInvoice.DataSource = null;
        GrdPrintInvoice.DataBind();

    }
    //For Print INvoices again
    protected void btnRePrint_Click(object sender, EventArgs e)
    {

        DisableControls();

        LoadGridInvoices();


    }

    //protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        const string url = "winCustomerNew.aspx?ID=1&cat=test";
    //        const string script = "window.open('" + url + "','')";
    //        if (!ClientScript.IsClientScriptBlockRegistered("NewWindow"))
    //        {
    //            ClientScript.RegisterClientScriptBlock(GetType(), "NewWindow", script, true);
    //        }
    //    }
    //    catch (Exception eee)
    //    {
    //        eee.Message.ToString();
    //    }
    //}

    #endregion

    #region Grid OPerations

    //protected void GrdPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    e.Row.VerticalAlign = VerticalAlign.Middle;
    //    switch (e.Row.Cells[9].Text)
    //    {
    //        case "1":
    //            e.Row.BackColor = Color.LightGray;
    //            break;
    //        default:
    //            e.Row.BackColor = Color.White;
    //            break;
    //    }
    //}

    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    //protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    _rowId = e.NewEditIndex;
    //    txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
    //    txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
       
    //    txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
    //    txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[7].Text;
        
    //    txtskuCode.Enabled = false;
    //    btnSave.ToolTip = "Update SKU";

    //}

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];

        DataView view = _purchaseSku.DefaultView;
        view.Sort = "ORDER DESC";
        _purchaseSku = view.ToTable();

        _dtFreeSku = (DataTable)Session["dtFreeSKU"];
        var skuId = GrdPurchase.Rows[e.RowIndex].Cells[0].Text;

        if (_dtFreeSku != null)
        {
            foreach (DataRow dr in _dtFreeSku.Rows)
            {
                var chk = dr["P_SKU_ID"].ToString(); //free SKU_ID

                if (skuId != chk) continue;
                if ((dr["AMOUNT"].ToString() == "0"))
                {
                    dr["AMOUNT"] = dr["AMOUNT_TEMP"];
                    dr["CHECK_DELETE"] = "0";
                }
                else
                {
                    dr["AMOUNT_TEMP"] = dr["AMOUNT"];
                    dr["AMOUNT"] = "0";
                    dr["CHECK_DELETE"] = "1";
                }
            }

            Session.Add("dtFreeSKU", _dtFreeSku);

        }

        _purchaseSku.Rows.RemoveAt(e.RowIndex);

        Session.Add("PurchaseSKU", _purchaseSku);

        LoadGird();
        LoadFreeGrid();

        btnCalculate.Enabled = true;
        Calculate();

        if (_purchaseSku.Rows.Count <= 0)
        {
            btnToggleMode.Disabled = false;
            txtCashReceived.Text = "";
            txtBalance.Text = "0.00";
        }
    }

    /// <summary>
    /// Get An invoice Id From print invoice Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrintInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _saleInvId = Convert.ToInt32(GrdPrintInvoice.Rows[e.RowIndex].Cells[0].Text);
        RptCustomerController rcc = new RptCustomerController();

        var psiDt = rcc.SelectCustomerInvoiceDetail(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["PRINCIPAL_ID"].ToString()), 1, _saleInvId, DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtstartDate.Text));

        Session.Add("psi_dt", psiDt);

        if (_saleInvId == -2 && _saleInvId == -1) return;
        if (System.Configuration.ConfigurationManager.AppSettings["IsPrint"] == "1")
        {
            PrintReport(1);
        }
    }

    protected void GrdPrintInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.BackColor = e.Row.Cells[7].Text == "Sales Refund" ? Color.Lavender : Color.White;
    }

    #endregion

    #region Load Functions
    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadCustomerData()
    {
        drpCustomer.Items.Clear();
        CustomerDataController mController = new CustomerDataController();//int.Parse(Session["DistributorId"].ToString()),int.Parse(Session["AreaId"].ToString()), int.Parse(Session["PrincipalId"].ToString())
        var dtCustomer = mController.SelectPrincipalCustomer(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);

        clsWebFormUtil.FillDropDownList(drpCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL", true);
        if (Session["CustomerID"].ToString() == null)
        {
            DataRow[] foundRows = dtCustomer.Select("CUSTOMER_ID  = '" + drpCustomer.SelectedValue + "'");
            if (foundRows.Length > 0)
            {
                Session.Add("CustomerID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
                _mCustomerVolClassId = int.Parse(foundRows[0]["VOLUME_CLASS_ID"].ToString());
                drpCustomer.SelectedValue = Session["CustomerID"].ToString();
            }

        }
        else
        {
            drpCustomer.SelectedValue = Session["CustomerID"].ToString();
        }
        if (Session["dtCustomer"].ToString() == null)
        {
            Session.Add("dtCustomer", dtCustomer);
        }
    }
    private void LoadPromotion()
    {
        if (int.Parse(Session["DISTRIBUTOR_ID"].ToString()) > 0)
        {
            SKUPriceDetailController pController = new SKUPriceDetailController();

            DataTable dt = pController.SelectDataPrice(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 5, Constants.DateNullValue);

            if ((int.Parse(dt.Rows[0]["Is_ManualDiscount"].ToString()) > 0))
            {
                OrderEntryController orderController = new OrderEntryController();
                PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Convert.ToDateTime(Session["CurrentWorkDate"]));
                
                numtxtTotalExtraDiscnt.ReadOnly = true;
                Session.Add("Arpc", Arpc);
            }
            else
            {

                numtxtTotalExtraDiscnt.ReadOnly = false;

            }
        }
    }

    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadGird()
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        DataView view = _purchaseSku.DefaultView;
        view.Sort = "ORDER DESC";
        _purchaseSku = view.ToTable();

         GrdPurchase.DataSource = _purchaseSku;
        GrdPurchase.DataBind();
    }


    /// <summary>
    /// Loads Free SKUS To SKU Grid
    /// </summary>
    private void LoadFreeGrid()
    {
        _dtFreeSku = (DataTable)Session["dtFreeSKU"];
        GrdFreeSKU.DataSource = _dtFreeSku;
        GrdFreeSKU.DataBind();
    }

    //Re-Print Grid
    private void LoadGridInvoices()
    {

        RptCustomerController rcc = new RptCustomerController();
        DataTable pi_dt = rcc.SelectCustomerInvoicePrint(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["PRINCIPAL_ID"].ToString()), 0, DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtstartDate.Text));
        GrdPrintInvoice.DataSource = pi_dt;
        GrdPrintInvoice.DataBind();

    }

    /// <summary>
    /// Verifies Customer Code
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
 

    private void LoadDocumentDetail(long docId, int typeId)
    {
        try
        {
            
            _purchaseSku = _mOrderController.GetDocumentDetail(docId, typeId);
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    private void LoadloginDetail()
    {
        try
        {
            UserController userControl = new UserController();
            var dt = userControl.SelectSlashUser2(int.Parse(Session["UserId"].ToString()));

            lbllogintimedate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");//DateTime.Now.ToString("MM/dd/yyyy");
            lbluserlogin.Text = dt.Rows[0]["USER_NAME"].ToString();
            lblLoacation.Text = dt.Rows[0]["DISTRIBUTOR_NAME"].ToString();
            CompanyPhonNmbr = dt.Rows[0]["CONTACT_NUMBER"].ToString(); //Location Contact Number
            HirarchyNameQuiz2 = dt.Rows[0]["LC2"].ToString();
            Address = dt.Rows[0]["LocationAddress"].ToString();

            Session.Add("DISTRIBUTOR_ID", dt.Rows[0]["DISTRIBUTOR_ID"].ToString());

            var dt2 = userControl.SelectUserPrincipal(int.Parse(Session["UserId"].ToString()));
            HirarchyNameQuiz = dt.Rows[0]["COMPANY_NAME"].ToString();

            Session.Add("PRINCIPAL_ID", dt2.Rows[0]["PRINCIPAL_ID"].ToString());

                     
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    /// <summary>
    /// Loads Batch Nos
    /// </summary>
    private void LoadProduct()
    {
        try
        {            
            var dtProduct = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 2, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            hfProduct.Value = GetJson(dtProduct);
            Session.Add("Dtsku_Price", dtProduct);
            aceSku.ContextKey = Session["DISTRIBUTOR_ID"].ToString();
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    public void LoadOnHoldInvoicNumber()
    {
        
        var dtOrder = _mOrderController.SelectPendingOrder(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), 0, 0, 0, 0, Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
        ((TextBox)(Master.FindControl("lblonHold"))).Text = Convert.ToString(dtOrder.Rows.Count);

        Response.Redirect("frmOrderPOS.aspx");
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

    /// <summary>
    /// Checks Free SKU in Free SKU Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private int FreeSKUExist(DataTable dt, int skuId)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (int.Parse(dt.Rows[i]["SKU_ID"].ToString()) == skuId)
            {
                return i;
            }

        }
        return -1;
    }

    /// <summary>
    /// Loads Order And Free SKUS Grids
    /// </summary>
    /// <param name="orderId">Order</param>
    private void ExistenOrderDetail(long orderId)
    {
        try
        {
            
            _purchaseSku = _mOrderController.SelectOrderDetail(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), orderId);
            _dtFreeSku = _mOrderController.SelectOrderPromotion(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), orderId);
            Session.Add("PurchaseSKU", _purchaseSku);
            Session.Add("dtFreeSKU", _dtFreeSku);
            LoadGird();
            LoadFreeGrid();
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    DataTable GetDataTable(GridView dtg)
    {
        DataTable dt = new DataTable();
        try
        {


            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }

                //int chk_del = Convert.ToInt32(row.Cells[9].Text);

                //if (chk_del == 0)
                //{
                dt.Rows.Add(dr);
                // }
            }
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
        return dt;
    }

    public void GetLicense()
    {
        var dtLicenseData = _mDist.GetLicenseData();
        if (dtLicenseData.Rows.Count <= 0) return;
        if (Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"]) >= 80)
        {
            lbl_licencesmsg.Visible = true;
            int days = Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"]);
            int edays = 90 - (days);
            _expiryDate = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).AddDays(edays).ToString("dd-MMM-yyyy");
            lbl_licencesmsg.ForeColor = Color.Blue;
            lbl_licencesmsg.Text = "Software Expiry Date:  " + _expiryDate + "  (Plz Contact to Administrator).";
        }
    }

    #endregion

    #region Clear

   
    private void ClearAll12()
    {
        try
        {
            txtskuCode.Text = "";
            txtskuName.Text = "";
            txtUnitRate.Text = "";
            txtQuantity.Text = hfToggleMode.Value != "SALE MODE" ? "-1" : "1";
            btnSave.ToolTip = "Add Sku";
            btnSaveOrder.Enabled = true;
            txtskuCode.Enabled = true;
            txtskuCode.Focus();

        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }

    }

    private void ClearAll()
    {
        try
        {
            txtskuCode.Text = "";
            txtskuName.Text = "";
            txtUnitRate.Text = "";
            txtQuantity.Text = hfToggleMode.Value != "SALE MODE" ? "-1" : "1";
            txtCashReceived.Text = "";
            txtAuthorisedBy.Text = "";
            txtBalance.Text = "";

            btnSave.ToolTip = "Add Sku";
            btnSaveOrder.Enabled = true;
            txtskuCode.Enabled = true;
            txtskuCode.Focus();

        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }

    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterAll()
    {
        try
        {
            EnableDisableController(true);
            Session.Remove("PurchaseSKU");
            Session.Remove("dtFreeSKU");
            Session.Remove("psi_dt");
            Session.Remove("SALE_ORDER_ID");
            Session.Remove("CashRecives");
            Session.Remove("CustomerID");
            Session.Remove("dtCustomer");

            btnSaveOrder.Enabled = true;
            CreatTable();
            CreateFreeSku();

            LoadGird();
            LoadFreeGrid();
            txtGrossAmount.Text = "";
            numtxtTotalExtraDiscnt.Text = "";
            numTxtTotalGST.Text = "";
            numTxtTotlAmnt.Text = "";
            txtCashReceived.Text = "";
            txtAuthorisedBy.Text = "";
            txtBalance.Text = "";

           
            _rowId = 0;

            _mCustomerVolClassId = 0;

           
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    #endregion

    #region PrintINvoice

    private void PrintReport(int type)
    {
        try
        {
            PrintType = type;
            _prnDocument = new System.Drawing.Printing.PrintDocument();
            _prnDocument.PrinterSettings.PrinterName = System.Configuration.ConfigurationManager.AppSettings["PrinterName"];
            // The Event of 'PrintPage'
            _prnDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prnDocument_PrintPage);
            _prnDocument.Print();
        }
        catch (Exception e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + e.Message + "');", true);
        }
    }
    
    private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        leftMargin = (int)e.MarginBounds.Left;//100
        rightMargin = (int)e.MarginBounds.Right;//215
        topMargin = (int)e.MarginBounds.Top;//100
        topMargin = 1;
        bottomMargin = (int)e.MarginBounds.Bottom;//1069
        InvoiceWidth = (int)e.MarginBounds.Width;//115
        InvoiceHeight = (int)e.MarginBounds.Height;//969

        if (PrintType != 4)
        {
            SetInvoiceHead(e.Graphics); // Draw Invoice Head
            SetInvoiceData(e.Graphics, e); // Draw Invoice Data
        }
        else if (PrintType == 4)
        {
            SetSaleReportHead(e.Graphics);
        }

        ReadInvoice = true;
    }

    private void SetInvoiceHead(Graphics g)
    {
        ReadInvoiceHead();

        if (Session["psi_dt"] != null)
        {
            _psiDt = (DataTable)Session["psi_dt"];

            if (_psiDt.Rows.Count <= 0) return;
            foreach (DataRow dr in _psiDt.Rows)
            {
                CurrentY = topMargin;
                CurrentX = leftMargin;
                const int imageHeight = 0;
                InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
                InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g));

                // Get Titles Length:
                int lenInvTitle = (int)g.MeasureString(InvTitle, InvTitleFont).Width;
                int lenInvTitle1 = (int)g.MeasureString(InvTitle1, InvSubTitleFont).Width;
                int lenInvSubTitle1 = (int)g.MeasureString(InvSubTitle1, InvSubTitleFont).Width;
                int lenInvSubTitle2 = (int)g.MeasureString(InvSubTitle2, InvSubTitle2Font).Width;
                int lenInvSubTitle5 = (int)g.MeasureString(InvSubTitle2, InvSubTitle2Font).Width;
                int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvSubTitleFont).Width;
                int lenInvSubTitle4 = (int)g.MeasureString(InvSubTitle4, InvSubTitleFont).Width;
                
                // Set Titles Left:
                int xInvTitle = CurrentX + (InvoiceWidth - lenInvTitle) / 2;
                int xInvTitle1 = CurrentX + (InvoiceWidth - lenInvTitle1) / 2;
                int xInvSubTitle1 = CurrentX + (InvoiceWidth - lenInvSubTitle1) / 2;
                int xInvSubTitle2 = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2;
                int xInvSubTitle5 = CurrentX + (InvoiceWidth - lenInvSubTitle5) / 2;
                int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;
                int xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2;

                // Draw Invoice Head:
                if (InvTitle != "")
                {
                    CurrentY = CurrentY + imageHeight;
                    g.DrawString(InvTitle, InvTitleFont, _blueBrush, xInvTitle, CurrentY);
                }
                if (InvTitle1 != "")
                {
                    CurrentY = CurrentY + InvTitleHeight;
                    g.DrawString(InvTitle1, InvSubTitleFont, _blueBrush, xInvTitle1, CurrentY);
                }
                if (InvSubTitle1 != "")
                {
                    CurrentY = CurrentY + InvTitleHeight;
                    g.DrawString(InvSubTitle1, InvSubTitleFont, _blueBrush, xInvSubTitle1, CurrentY);
                }
                if (InvSubTitle2 != "")
                {
                    CurrentY = CurrentY + InvSubTitleHeight;
                    g.DrawString(InvSubTitle2, InvSubTitle2Font, _blueBrush, xInvSubTitle2, CurrentY);
                }
                if (InvSubTitle5 != "")
                {
                    CurrentY = CurrentY + InvSubTitleHeight;
                    g.DrawString(InvSubTitle5, InvSubTitle2Font, _blueBrush, xInvSubTitle5, CurrentY);
                }
                long idToPrint = 0;
                var invoiceCreditHeading = "";

                var invType = dr["InvoiceType"].ToString();

                if (invType == "SaleInvoice")
                {
                    idToPrint = _saleInvId;
                    InvSubTitle3 = "Sale Invoice";//sale mode
                    invoiceCreditHeading = "INV #  ";
                }
                else
                {
                    idToPrint = _saleInvId;
                    InvSubTitle3 = "Refund Invoice";
                    invoiceCreditHeading = "INV #  ";
                }

                if (InvSubTitle3 != "")
                {
                    CurrentY = CurrentY + InvSubTitleHeight + 10;
                    g.DrawString(InvSubTitle3, InvSubTitleFont, _blueBrush, 10, CurrentY);
                }
                //For Payment Mode
                InvSubTitle4 = "MOP: ";

                var MOP = dr["MOP"].ToString();
                if (MOP == "Cash")
                {
                    g.DrawString(InvSubTitle4 + MOP, InvSubTitleFont, _blueBrush, 160, CurrentY);
                }
                else if (MOP == "Credit")
                {
                    g.DrawString(InvSubTitle4 + MOP, InvSubTitleFont, _blueBrush, 160, CurrentY);
                }
                else
                {
                    g.DrawString(InvSubTitle4 + MOP, InvSubTitleFont, _blueBrush, 160, CurrentY);
                }
                // Draw line:
                CurrentY = CurrentY + InvSubTitleHeight + 8;
                g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);

                CurrentY = CurrentY + 10;
                //   g.DrawString("DATE:" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), InvoiceFont, BlueBrush, 10, CurrentY);
                g.DrawString("DATE: " + Convert.ToDateTime(Session["Currentworkdate"]).ToString("dd-MMM-yyyy")+' '+ DateTime.Now.ToString("hh:mm:ss"), InvoiceFont, _blueBrush, 10, CurrentY);

                string units = dr["UNITS"].ToString();
                g.DrawString("NO OF UNITS: ", InvoiceFont, _blueBrush, 160, CurrentY);

                XNoOfuntit = (int)g.MeasureString("NO OF UNITS: ", InvoiceFont).Width + 160;
                YNoOfUnit = CurrentY;
                CurrentY = CurrentY + 15;
                g.DrawString(invoiceCreditHeading + idToPrint, InvoiceFont, _blueBrush, 10, CurrentY);
                g.DrawString("CASHIER: " + lbluserlogin.Text, InvoiceFont, _blueBrush, 160, CurrentY);

                CurrentY = CurrentY + 15;
                g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);//
                CurrentY = CurrentY + 10;

                string customerName = dr["CUSTOMER"].ToString();

                g.DrawString("CUSTOMER: " + customerName, InvoiceFont, _blueBrush, 10, CurrentY);
                CurrentY = CurrentY + 15;
                g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);

                break;
            }
        }
        else
        {
            CurrentY = topMargin;
            CurrentX = leftMargin;
            const int imageHeight = 0;
            InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
            InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g));

            // Get Titles Length:
            int lenInvTitle = (int)g.MeasureString(InvTitle, InvTitleFont).Width;
            int lenInvTitle1 = (int)g.MeasureString(InvTitle1, InvSubTitleFont).Width;
            int lenInvSubTitle1 = (int)g.MeasureString(InvSubTitle1, InvSubTitleFont).Width;
            int lenInvSubTitle2 = (int)g.MeasureString(InvSubTitle2, InvSubTitle2Font).Width;
            int lenInvSubTitle5 = (int)g.MeasureString(InvSubTitle2, InvSubTitle2Font).Width;
            int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvSubTitleFont).Width;
            int lenInvSubTitle4 = (int)g.MeasureString(InvSubTitle4, InvSubTitleFont).Width;
            // Set Titles Left:
            int xInvTitle = CurrentX + (InvoiceWidth - lenInvTitle) / 2;
            int xInvTitle1 = CurrentX + (InvoiceWidth - lenInvTitle1) / 2;
            int xInvSubTitle1 = CurrentX + (InvoiceWidth - lenInvSubTitle1) / 2;
            int xInvSubTitle2 = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2;
            int xInvSubTitle5 = CurrentX + (InvoiceWidth - lenInvSubTitle5) / 2;
            int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;
            int xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2;

            // Draw Invoice Head:
            if (InvTitle != "")
            {
                CurrentY = CurrentY + imageHeight;
                g.DrawString(InvTitle, InvTitleFont, _blueBrush, xInvTitle, CurrentY);
            }
            if (InvTitle1 != "")
            {
                CurrentY = CurrentY + InvTitleHeight;
                g.DrawString(InvTitle1, InvSubTitleFont, _blueBrush, xInvTitle1, CurrentY);
            }
            if (InvSubTitle1 != "")
            {
                CurrentY = CurrentY + InvTitleHeight;
                g.DrawString(InvSubTitle1, InvSubTitleFont, _blueBrush, xInvSubTitle1, CurrentY);
            }
            if (InvSubTitle2 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle2, InvSubTitle2Font, _blueBrush, xInvSubTitle2, CurrentY);
            }
            if (InvSubTitle5 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle5, InvSubTitle2Font, _blueBrush, xInvSubTitle5, CurrentY);
            }
            long idToPrint = 0;
            string invoiceCreditHeading = "";
            if (hfToggleMode.Value == "SALE MODE")
            {
                idToPrint = _saleInvoiceId;
                InvSubTitle3 = "Sale Invoice";//slae mode
                invoiceCreditHeading = "INV #  ";

            }
            //else if (printType == 2)
            //{
            //    IdToPrint = long.Parse(Credit_Note_id.ToString()); //credit note mode
            //    InvSubTitle3 = "Credit Note ";
            //    InvoiceCreditHeading = "CREDIT NOTE # :";
            //}
            else
            {
                //refund mode
                idToPrint = _saleInvoiceId;
                InvSubTitle3 = "Refund Invoice";
                invoiceCreditHeading = "INV #  ";
            }
            if (InvSubTitle3 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight + 10;
                g.DrawString(InvSubTitle3, InvSubTitleFont, _blueBrush, 10, CurrentY);
            }
            //For Payment Mode
            InvSubTitle4 = "MOP: ";

            g.DrawString(InvSubTitle4 + RblPayMode.SelectedItem.Text, InvSubTitleFont, _blueBrush, 160, CurrentY);

            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight + 8;
            g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);

            CurrentY = CurrentY + 10;
            g.DrawString("DATE: " + Convert.ToDateTime(Session["Currentworkdate"]).ToString("dd-MMM-yyyy") + ' ' + DateTime.Now.ToString("hh:mm:ss"), InvoiceFont, _blueBrush, 10, CurrentY);
            g.DrawString("NO OF UNITS: ", InvoiceFont, _blueBrush, 160, CurrentY);
            XNoOfuntit = (int)g.MeasureString("NO OF UNITS: ", InvoiceFont).Width + 160;
            YNoOfUnit = CurrentY;
            CurrentY = CurrentY + 15;
            g.DrawString(invoiceCreditHeading + idToPrint, InvoiceFont, _blueBrush, 10, CurrentY);
            g.DrawString("CASHIER: " + lbluserlogin.Text, InvoiceFont, _blueBrush, 160, CurrentY);

            CurrentY = CurrentY + 15;
            g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);//
            CurrentY = CurrentY + 10;
            string customerName = "WALK IN CUSTOMER";
            //if (cbselectCustomer.Checked == true)
            //{
            customerName = drpCustomer.SelectedItem.Text;
            //}
            g.DrawString("CUSTOMER: " + customerName, InvoiceFont, _blueBrush, 10, CurrentY);
            CurrentY = CurrentY + 15;
            g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);
        }
    }

    private void SetSaleReportHead(Graphics g)
    {
        var dt = _mOrderController.SelectSaleReport(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtEndDate.Text));
        decimal totalGrossSale = 0;
        decimal totalDiscount = 0;
        decimal totalGst = 0;
        decimal totalNetAMount = 0;
        decimal totalCredit = 0;
        totalGrossSale = Math.Round(decimal.Parse(dt.Rows[0]["TOTAL_AMOUNT"].ToString()), 2);
        totalDiscount = Math.Round(decimal.Parse(dt.Rows[0]["DISCOUNT"].ToString()), 2);
        totalGst = Math.Round(decimal.Parse(dt.Rows[0]["GST_AMOUNT"].ToString()), 2);
        totalNetAMount = Math.Round(decimal.Parse(dt.Rows[0]["NET_TOTAL"].ToString()), 2);
        totalCredit = Math.Round(decimal.Parse(dt.Rows[0]["CREDIT_AMOUNT"].ToString()), 2);
        ReadInvoiceHead();

        CurrentY = topMargin;
        CurrentX = leftMargin;
        const int imageHeight = 0;
        InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
        InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g));

        // Get Titles Length:
        int lenInvTitle = (int)g.MeasureString(InvTitle, InvTitleFont).Width;
        int lenInvTitle1 = (int)g.MeasureString(InvTitle1, InvSubTitleFont).Width;
        int lenInvSubTitle1 = (int)g.MeasureString(InvSubTitle1, InvSubTitleFont).Width;
        int lenInvSubTitle2 = (int)g.MeasureString(InvSubTitle2, InvSubTitleFont).Width;
        int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvSubTitleFont).Width;
        // Set Titles Left:
        int xInvTitle = CurrentX + (InvoiceWidth - lenInvTitle) / 2;
        int xInvTitle1 = CurrentX + (InvoiceWidth - lenInvTitle1) / 2;
        int xInvSubTitle1 = CurrentX + (InvoiceWidth - lenInvSubTitle1) / 2;
        int xInvSubTitle2 = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2;
        int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;

        // Draw Invoice Head:
        if (InvTitle != "")
        {
            CurrentY = CurrentY + imageHeight;
            g.DrawString(InvTitle, InvTitleFont, _blueBrush, xInvTitle, CurrentY);
        }
        if (InvTitle1 != "")
        {
            CurrentY = CurrentY + imageHeight;
            g.DrawString(InvTitle1, InvSubTitleFont, _blueBrush, xInvTitle1, CurrentY);
        }
        if (InvSubTitle1 != "")
        {
            CurrentY = CurrentY + InvTitleHeight;
            g.DrawString(InvSubTitle1, InvSubTitleFont, _blueBrush, xInvSubTitle1, CurrentY);
        }
        if (InvSubTitle2 != "")
        {
            CurrentY = CurrentY + InvSubTitleHeight;
            g.DrawString(InvSubTitle2, InvSubTitleFont, _blueBrush, xInvSubTitle2, CurrentY);
        }
        InvSubTitle3 = "SALES REPORT";//
        CurrentY = CurrentY + InvSubTitleHeight + 10;
        g.DrawString(InvSubTitle3, InvSubTitleFont, _blueBrush, 100, CurrentY);

        CurrentY = CurrentY + InvSubTitleHeight + 15;
        g.DrawString("From  :" + txtstartDate.Text + "   To  :" + txtEndDate.Text, InvSubTitleFont, _blueBrush, 10, CurrentY);

        // Draw line:

        // totalGrossSale=totalGrossSale-totalDiscount-totalGST;
        CurrentY = CurrentY + InvSubTitleHeight + 8;
        g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);

        CurrentY = CurrentY + 20;
        g.DrawString("GROSS SALE  : ", _saleFont, _blueBrush, 30, CurrentY);
        g.DrawString(totalGrossSale.ToString(), _saleFont, _blueBrush, 200, CurrentY);
        CurrentY = CurrentY + 30;

        g.DrawString("DISCOUNT  : ", _saleFont, _blueBrush, 30, CurrentY);
        g.DrawString(totalDiscount.ToString(), _saleFont, _blueBrush, 200, CurrentY);

        CurrentY = CurrentY + 30;
        g.DrawString("GST  : ", _saleFont, _blueBrush, 30, CurrentY);
        g.DrawString(totalGst.ToString(), _saleFont, _blueBrush, 200, CurrentY);

        CurrentY = CurrentY + 30;
        g.DrawString("CREDIT  : ", _saleFont, _blueBrush, 30, CurrentY);
        g.DrawString(totalCredit.ToString(), _saleFont, _blueBrush, 200, CurrentY);

        CurrentY = CurrentY + 30;
        g.DrawString("NET SALE  : ", _saleFont, _blueBrush, 30, CurrentY);
        g.DrawString(totalNetAMount.ToString(), _saleFont, _blueBrush, 200, CurrentY);

        CurrentY = CurrentY + 30;
        g.DrawString("CASH RECIVED  : ", _saleFont, _blueBrush, 30, CurrentY);
        g.DrawString(totalNetAMount.ToString(), _saleFont, _blueBrush, 200, CurrentY);
        CurrentY = CurrentY + 30;

        g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);
        CurrentY = CurrentY + 15;
        // g.DrawString("User  :" + _dc.chkNull_0(ddsalesForce.SelectedItem.ToString()), SaleFont, BlueBrush, 10, CurrentY);
        g.DrawString("Date : " + DateTime.Now.ToString("dd-MMM-yyyy"), _saleFont, _blueBrush, 200, CurrentY);
        CurrentY = CurrentY + 40;
        g.DrawString("POWERD BY:  www.fastservices.pk.", InvoiceFont3, _blackBrush, 10, CurrentY);
    }

    private void ReadInvoiceHead()
    {
        //Titles and Image of invoice:
        InvTitle = HirarchyNameQuiz;
        InvTitle1 = HirarchyNameQuiz2;
        InvSubTitle1 = lblLoacation.Text;
        InvSubTitle2 = Address; 
        InvSubTitle5 = "PH # " + CompanyPhonNmbr; //lblphone.Text; 
        // InvSubTitle3 = lblphone.Text;
        //InvImage = Application.StartupPath + @"\Images\" + "InvPic.jpg";
    }

    private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
    {// Set Invoice Table:
        string fieldValue = "";
        decimal fieldValue2 = 0;
        int currentRecord = 0;
        int RecordsPerPage = 20; // twenty items in a page
        decimal Amount = 0;
        bool StopReading = false;

        // Set Table Head:
        const int xProductId = 10; //leftMargin;
      //  CurrentY = CurrentY + 20;//InvoiceFontHeight;
      //  g.DrawLine(new Pen(Brushes.Black), 10, CurrentY, 300, CurrentY);
        CurrentY = CurrentY + 10;//InvoiceFontHeight;
        g.DrawString("ITEM NAME", InvoiceFont, _blueBrush, xProductId, CurrentY);

        int xProductName = xProductId + (int)g.MeasureString("ITEM", InvoiceFont).Width + 100;
        g.DrawString("QTY", InvoiceFont, _blueBrush, xProductName, CurrentY);


        int xUnitPrice = xProductName + (int)g.MeasureString("QTY", InvoiceFont).Width + 10;
        g.DrawString("PRICE", InvoiceFont, _blueBrush, xUnitPrice, CurrentY);

        int xQuantity = xUnitPrice + (int)g.MeasureString("PRICE", InvoiceFont).Width + 10;
        g.DrawString("TOTAL", InvoiceFont, _blueBrush, xQuantity, CurrentY);



        // Set Invoice Table:
        CurrentY = CurrentY + 20;//InvoiceFontHeight + 8;
        g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);
        CurrentY = CurrentY + 10;//InvoiceFontHeight + 8;
        //while (CurrentRecord < RecordsPerPage)
        decimal numberOfUnit = 0;

        if (Session["psi_dt"] != null)
        {
            _psiDt = (DataTable)Session["psi_dt"];

            if (_psiDt.Rows.Count > 0)
            {
                foreach (DataRow dr in _psiDt.Rows)
                {
                    decimal chkdiscount = Convert.ToDecimal(dr["EXTRA_DISCOUNT_AMOUNT"]);
                    //if (chkdiscount < 0)
                    //{
                    //    fieldValue = dr["BAR_CODE"] + " " + "    UPSELL";
                    //    g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);
                    //}
                    //else
                    //{
                    //    fieldValue = dr["BAR_CODE"].ToString();
                    //    g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);
                    //}
                   
                    //For Refund Check
                    decimal qty = 0;
                    qty = Convert.ToDecimal(dr["QUANTITY_UNIT"]);

                   
                    if (qty > 0)
                    {
                        fieldValue = dr["SKU_NAME"].ToString().ToUpper();// name
                        if (fieldValue.Length > 25)
                            fieldValue = fieldValue.Remove(25, fieldValue.Length - 25);
                        g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);
                    }
                    else
                    {
                        fieldValue = dr["SKU_NAME"] + "_" + "(Rfnd)";// name
                        if (fieldValue.Length > 25)
                            fieldValue = fieldValue.Remove(25, fieldValue.Length - 25);
                        g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);
                    }
                    fieldValue = String.Format("{0:0}", dr["QUANTITY_UNIT"]);  //quantity
                    numberOfUnit += Convert.ToDecimal(dr["QUANTITY_UNIT"].ToString());
                    g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductName + 4, CurrentY);

                    decimal untPrice = Convert.ToDecimal(dr["UNIT_PRICE"]);

                   // FieldValue2 = Math.Ceiling(Convert.ToDecimal((unt_price)));//Price
                    fieldValue2 = Convert.ToDecimal(untPrice);//Price
                    
                    fieldValue = String.Format("{0:0.00}",fieldValue2);
                    g.DrawString(fieldValue, InvoiceFont4, _blackBrush, xUnitPrice, CurrentY);

                    untPrice = Convert.ToDecimal(dr["AMOUNT"]);

                   // FieldValue2 = Math.Ceiling(Convert.ToDecimal((unt_price)));//Total
                    fieldValue2 = Convert.ToDecimal(untPrice);//Total
                    
                    fieldValue = String.Format("{0:0.00}",fieldValue2);
                    g.DrawString(fieldValue, InvoiceFont4, _blackBrush, xQuantity, CurrentY);

                    CurrentY = CurrentY + 15;
                   // g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);
                   // CurrentY = CurrentY + 10;
                    currentRecord++;
                    if (chkdiscount >= 0)
                    {
                        _cashRcd = Convert.ToDecimal(dr["CASH_RECIEVED"]);//CAsh Rec
                        _discouunt = Convert.ToDecimal(dr["EXTRA_DISCOUNT_AMOUNT"]);//Discount Amount
                        _ttamount = Convert.ToDecimal(dr["TOTAL_AMOUNT"]);//Sub-total
                        _ntamount = Convert.ToDecimal(dr["TOTAL_NET_AMOUNT"]);//Net total
                    }
                }

            }
            if (currentRecord > 0)
            {
                if (numberOfUnit < 0)
                {
                    numberOfUnit = numberOfUnit * (-1);
                }
             
                g.DrawString(String.Format("{0:0}",numberOfUnit), InvoiceFont, _blackBrush, XNoOfuntit, YNoOfUnit);
                SetPrintInvoiceTotal(g);
                g.Dispose();
            }
        }
        else
        {
            foreach (GridViewRow dr in GrdPurchase.Rows)
            {
                decimal qty =Convert.ToDecimal(dr.Cells[5].Text);
               // qty = Convert.ToDecimal(qty);
                if (qty > 0)
                {
                    fieldValue = dr.Cells[2].Text.ToUpper();// name
                    if (fieldValue.Length > 25)
                        fieldValue = fieldValue.Remove(25, fieldValue.Length - 25);
                    g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);

                }
                else
                {
                    fieldValue = dr.Cells[2].Text.ToUpper()+"_"+"Rfnd";// name
                    if (fieldValue.Length > 25)
                        fieldValue = fieldValue.Remove(25, fieldValue.Length - 25);
                    g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);
                }
                fieldValue = String.Format("{0:0}", dr.Cells[5].Text);  //quantity

                g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductName + 4, CurrentY);
                numberOfUnit += Convert.ToDecimal(dr.Cells[5].Text);

              //  FieldValue2 = Math.Ceiling(Convert.ToDecimal(_dc.chkNull_0(dr.Cells[6].Text)));//price
                fieldValue2 = Convert.ToDecimal(_dc.chkNull_0(dr.Cells[6].Text));//price
                
                fieldValue = String.Format("{0:0.00}",fieldValue2);
                g.DrawString(fieldValue, InvoiceFont4, _blackBrush, xUnitPrice, CurrentY);

               // FieldValue2 = Math.Ceiling(Convert.ToDecimal(_dc.chkNull_0(dr.Cells[7].Text)));//total
                fieldValue2 = Convert.ToDecimal(_dc.chkNull_0(dr.Cells[7].Text));//total

                fieldValue = String.Format("{0:0.00}", fieldValue2);
                g.DrawString(fieldValue, InvoiceFont4, _blackBrush, xQuantity, CurrentY);

                CurrentY = CurrentY + 15;
             //   g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);
             //   CurrentY = CurrentY + 10;
                currentRecord++;
                // }
            }
            if (GrdFreeSKU.Rows.Count > 0)
            {
                foreach (GridViewRow dr2 in GrdFreeSKU.Rows)
                {
                    int i = 0;
                    i = Convert.ToInt32(dr2.Cells[9].Text);
                    if (i == 0)
                    {
                        fieldValue = dr2.Cells[1].Text + " " + "(UPSll)";//  bar code
                        g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);

                        fieldValue = dr2.Cells[3].Text;  //quantity

                        g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductName + 4, CurrentY);

                        numberOfUnit += Convert.ToInt32(dr2.Cells[3].Text);
                        CurrentY = CurrentY + 12;
                        fieldValue = dr2.Cells[2].Text;// name
                        if (fieldValue.Length > 30)
                            fieldValue = fieldValue.Remove(30, fieldValue.Length - 30);
                        g.DrawString(fieldValue, InvoiceFont2, _blackBrush, xProductId, CurrentY);

                        CurrentY = CurrentY + 15;
                     //   g.DrawLine(new Pen(Brushes.Black, 1), 10, CurrentY, 300, CurrentY);
                     //   CurrentY = CurrentY + 10;
                        currentRecord++;
                    }

                }
            }
            if (currentRecord > 0)
            {
                if (numberOfUnit < 0)
                {
                    numberOfUnit = numberOfUnit * (-1);
                }
                g.DrawString(numberOfUnit.ToString(), InvoiceFont, _blackBrush, XNoOfuntit, YNoOfUnit);
                SetInvoiceTotal(g);
                g.Dispose();
            }
        }
    }

    private void SetInvoiceTotal(Graphics g)
    {
        
        string invoiceValue = "";
        decimal invoiceValue2 = 0;
        decimal netAmount = 0;
        decimal totalAmount = decimal.Parse(_dc.chkNull_0(txtGrossAmount.Text));
        decimal discount = decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text));
        decimal gst = decimal.Parse(_dc.chkNull_0(numTxtTotalGST.Text));
        decimal balance=0;

        if (totalAmount > 0)
        {
            netAmount = (totalAmount - discount) + gst;
        }
        else
        {
            netAmount = (totalAmount + discount) - gst;
        }
        decimal resAmount = decimal.Parse(_dc.chkNull_0(txtCashReceived.Text));

        if (netAmount < 0)
        {
            balance = netAmount + resAmount;
        }
        else
        {
            balance = netAmount - resAmount;
        }

        g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);
        CurrentY = CurrentY + 5;
        g.DrawString("GROSS AMOUNT:", InvoiceFont, _redBrush, 50, CurrentY);
        invoiceValue2 = Math.Ceiling(Convert.ToDecimal(_dc.chkNull_0(txtGrossAmount.Text)));
        invoiceValue = String.Format("{0:0.00}", invoiceValue2);
        g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
        CurrentY = CurrentY + 15;//InvoiceFontHeight;

        if (discount != 0)
        {
            CurrentY = CurrentY + 5;//InvoiceFontHeight;
            g.DrawString("DISCOUNT:", InvoiceFont, _redBrush, 50, CurrentY);
            invoiceValue2 = Math.Ceiling(Convert.ToDecimal(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)));
            invoiceValue = String.Format("{0:0.00}", invoiceValue2);

            g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
            CurrentY = CurrentY + 15;//InvoiceFontHeight;
        }

        CurrentY = CurrentY + 5;//InvoiceFontHeight;
        g.DrawString("AMOUNT-DUE:", InvoiceFont, _redBrush, 50, CurrentY);
        invoiceValue2 = Math.Ceiling(Convert.ToDecimal(_dc.chkNull_0(netAmount.ToString())));
        invoiceValue = String.Format("{0:0.00}", invoiceValue2);
        //int xInvoiceValue = xRightEdg - (int)g.MeasureString(InvoiceValue, InvoiceFont).Width;
        g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
        CurrentY = CurrentY + 15;//InvoiceFontHeight;

        if (PrintType != 2)
        {
            CurrentY = CurrentY + 5;//InvoiceFontHeight;
            g.DrawString("CASH-PAID-IN:", InvoiceFont, _redBrush, 50, CurrentY);
            invoiceValue2 = Math.Ceiling(Convert.ToDecimal(_dc.chkNull_0(txtCashReceived.Text)));
            invoiceValue = String.Format("{0:0.00}", invoiceValue2);
            //int xInvoiceValue = xRightEdg - (int)g.MeasureString(InvoiceValue, InvoiceFont).Width;
            g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
            CurrentY = CurrentY + 15;//InvoiceFontHeight;
            if (RblPayMode.SelectedValue != "217")
            {
                CurrentY = CurrentY + 5;//InvoiceFontHeight;
                g.DrawString("CUSTOMER BALANCE:", InvoiceFont, _redBrush, 50, CurrentY);
                invoiceValue2 = Math.Ceiling(Convert.ToDecimal(_dc.chkNull_0(balance.ToString())));
                invoiceValue = String.Format("{0:0.00}", invoiceValue2);
                g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);

                CurrentY = CurrentY + 15;//InvoiceFontHeight;
            }
        }
        CurrentY = CurrentY + 20;//InvoiceFontHeight;
        //nOTES
        g.DrawString("OPEN 7 DAYS A WEEK (8:00 AM - 12:00 PM)", InvoiceFont2, _blackBrush, 10, CurrentY);
        CurrentY = CurrentY + 15;
        g.DrawString("FRIDAY BREAK (1:30 PM - 2:30 PM)", InvoiceFont2, _blackBrush, 10, CurrentY);
        CurrentY = CurrentY + 15;
        g.DrawString("REFUND/EXCHANGE WITH BILL IN 7 DAYS.", InvoiceFont2, _blackBrush, 10, CurrentY);
        //

        CurrentY = CurrentY + 25;//InvoiceFontHeight;

        g.DrawString("THANKS FOR YOUR VISIT.", InvoiceFont, _blackBrush, 80, CurrentY);
        CurrentY = CurrentY + 20;//InvoiceFontHeight;

        g.DrawString("POWERD BY:  www.fastservices.pk.", InvoiceFont3, _blackBrush, 10, CurrentY);
    }

    //For RePrint Invoice
    private void SetPrintInvoiceTotal(Graphics g)
    {
        g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);
        CurrentY = CurrentY + 5;
       
        g.DrawString("GROSS AMOUNT:", InvoiceFont, _redBrush, 50, CurrentY);
        string invoiceValue = String.Format("{0:0.00}", _ttamount);
        g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
        CurrentY = CurrentY + 20;//InvoiceFontHeight;
        //g.DrawLine(new Pen(Brushes.Black, 2), 10, CurrentY, 300, CurrentY);

        if (_discouunt != 0)
        {
            CurrentY = CurrentY + 5;//InvoiceFontHeight;
            g.DrawString("DISCOUNT:", InvoiceFont, _redBrush, 50, CurrentY);
            invoiceValue = String.Format("{0:0.00}", _discouunt);
            g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
            CurrentY = CurrentY + 20;//InvoiceFontHeight;
        }
        CurrentY = CurrentY + 5;//InvoiceFontHeight;
        g.DrawString("AMOUNT-DUE:", InvoiceFont, _redBrush, 50, CurrentY);

        invoiceValue = String.Format("{0:0.00}", _ntamount);

        g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
        CurrentY = CurrentY + 20;//InvoiceFontHeight;
        if (PrintType != 2)
        {
            CurrentY = CurrentY + 5;//InvoiceFontHeight;
            g.DrawString("CASH-PAID-IN:", InvoiceFont, _redBrush, 50, CurrentY);
            invoiceValue = String.Format("{0:0.00}", _cashRcd);
            g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
            CurrentY = CurrentY + 20;//InvoiceFontHeight;
            if (mop != "Cash & Credit Card")
            {
                CurrentY = CurrentY + 5;//InvoiceFontHeight;
                g.DrawString("CUSTOMER BALANCE:", InvoiceFont, _redBrush, 50, CurrentY);
                if (_ntamount > 0)
                {
                    invoiceValue = String.Format("{0:0.00}", _ntamount - _cashRcd);
                }

                else
                {
                    invoiceValue = String.Format("{0:0.00}", _ntamount + _cashRcd);
                }

                g.DrawString(invoiceValue, InvoiceFont, _blackBrush, 200, CurrentY);
                CurrentY = CurrentY + 20;//InvoiceFontHeight;
            }
        }
        CurrentY = CurrentY + 20;//InvoiceFontHeight;
        //nOTES
        g.DrawString("OPEN 7 DAYS A WEEK (8:00 AM - 12:00 PM)", InvoiceFont2, _blackBrush, 10, CurrentY);
        CurrentY = CurrentY + 15;
        g.DrawString("FRIDAY BREAK (1:30 PM - 2:30 PM)", InvoiceFont2, _blackBrush, 10, CurrentY);
        CurrentY = CurrentY + 15;
        g.DrawString("REFUND/EXCHANGE WITH BILL IN 7 DAYS.", InvoiceFont2, _blackBrush, 10, CurrentY);
        //
        CurrentY = CurrentY + 25;
        g.DrawString("THANKS FOR YOUR VISIT.", InvoiceFont, _blackBrush, 80, CurrentY);
        CurrentY = CurrentY + 20;//InvoiceFontHeight;

        g.DrawString("POWERD BY:  www.fastservices.pk.", InvoiceFont3, _blackBrush, 10, CurrentY);

        Session.Remove("psi_dt");
    }

    #endregion
    
    private void showReport(int type)
    {
        try
        {

            RptSaleController rptSaleCtl = new RptSaleController();



            DataSet ds = null;
            if (type == 1)
            {
                CORNBusinessLayer.Reports.crpSalesReportPos crpReport = new CORNBusinessLayer.Reports.crpSalesReportPos();
                ds = rptSaleCtl.SelectSaleReport(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtEndDate.Text + " 23:59:59"), type);

                crpReport.SetDataSource(ds);
                crpReport.Refresh();



                crpReport.SetParameterValue("COMPANY_NAME", HirarchyNameQuiz);
                crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtstartDate.Text));
                crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                crpReport.SetParameterValue("USER_NAME", lbluserlogin.Text);
                crpReport.SetParameterValue("LOCATION", lblLoacation.Text);
                crpReport.SetParameterValue("PHONE_NUMBER", CompanyPhonNmbr);

                Session.Add("CrpReport", crpReport);
                Session.Add("ReportType", 0);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else // incase of malee
            {
                type = 2;
                CORNBusinessLayer.Reports.crpSalesReportPos2 crpReport2 = new CORNBusinessLayer.Reports.crpSalesReportPos2();
                ds = rptSaleCtl.SelectSaleReport(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtEndDate.Text + " 23:59:59"), type);

                crpReport2.SetDataSource(ds);
                crpReport2.Refresh();



                crpReport2.SetParameterValue("COMPANY_NAME", HirarchyNameQuiz);
                crpReport2.SetParameterValue("FROM_DATE", DateTime.Parse(txtstartDate.Text));
                crpReport2.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
                crpReport2.SetParameterValue("USER_NAME", lbluserlogin.Text);
                crpReport2.SetParameterValue("LOCATION", lblLoacation.Text);
                crpReport2.SetParameterValue("PHONE_NUMBER", CompanyPhonNmbr);

                Session.Add("CrpReport", crpReport2);
                Session.Add("ReportType", 0);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void showReport2()
    {
        try
        {

            RptSaleController rptSaleCtl = new RptSaleController();
            CORNBusinessLayer.Reports.crpSalesReportPos crpReport = new CORNBusinessLayer.Reports.crpSalesReportPos();

            DataSet ds = null;
            int type = 1;
            if (chkMalee.Checked)
            {
                type = 2;
                CORNBusinessLayer.Reports.crpSalesReportPos2 crpReport2 = new CORNBusinessLayer.Reports.crpSalesReportPos2();
                crpReport2.SetDataSource(ds);
                crpReport2.Refresh();
            }
            ds = rptSaleCtl.SelectSaleReport(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), int.Parse(Session["UserId"].ToString()), DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtEndDate.Text + " 23:59:59"), type);

            crpReport.SetDataSource(ds);
            crpReport.Refresh();



            crpReport.SetParameterValue("COMPANY_NAME", HirarchyNameQuiz);
            crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtstartDate.Text));
            crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            crpReport.SetParameterValue("USER_NAME", lbluserlogin.Text);
            crpReport.SetParameterValue("LOCATION", lblLoacation.Text);
            crpReport.SetParameterValue("PHONE_NUMBER", CompanyPhonNmbr);

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}