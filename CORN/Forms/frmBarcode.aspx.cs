using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data;
using CORNBusinessLayer.Classes;
using CORNCommon.Classes;
using System.Web.UI.HtmlControls;


public partial class Forms_frmBarcode : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    DataTable dt = new DataTable();
    public static string CompanyName;
    SkuController cn = new SkuController();
    private void loadloginDetail()
    {
        try
        {
            UserController userControl = new UserController();
            DataTable dt = userControl.SelectSlashUser2(int.Parse(this.Session["UserId"].ToString()));
          
            CompanyName = dt.Rows[0]["COMPANY_NAME"].ToString();
        }
        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    

        if (!Page.IsPostBack)
        {
            loadloginDetail();
            lbl_companyname.Text = CompanyName;
            txt_price.Enabled=false;
            txt_code.Enabled = false;
        }
        //btn_print.Attributes.Add("onclick", "return ValidateForm();");
        btn_generate.Attributes.Add("onclick", "return ValidateForm2();");

    }    
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        string Code = "*" + txt_code.Text + "*";
        int w = Code.Length * 27;

        Bitmap oBitmap = new Bitmap(w, 100);
        Graphics oGraphics = Graphics.FromImage(oBitmap);

        Font oFont = new Font("IDAutomationHC39M", 18);
        PointF oPoint = new PointF(2f, 2f);

        SolidBrush oBrushWrite = new SolidBrush(Color.Black);
        SolidBrush oBrush = new SolidBrush(Color.White);

        oGraphics.FillRectangle(oBrush, 0, 0, w, 100);
        oGraphics.DrawString(Code, oFont, oBrushWrite, oPoint);


        lbl_pcode.Text = txt_productName.Text;
        lbl_pprice.Text =txt_price.Text;

        MemoryStream ms = new MemoryStream();
        oBitmap.Save(ms, ImageFormat.Png);

        var bytearray = ms.ToArray();
        var base64Data= Convert.ToBase64String(ms.ToArray());

        img_brcode.ImageUrl = "data:image/Jpeg;base64," + base64Data; 

        Session["base64Data"] = base64Data;

        if (Session["base64Data"] != null)
        {
            Panel1.Visible = true;
            img_brcode.Visible = true;
        }

        cn.InsertBarcode(CompanyName, Convert.ToString(lbl_pcode.Text), Convert.ToString("Rs. "+txt_price.Text), bytearray);

        SkuController sk = new SkuController();
        DataRow dr;
        dt = sk.SelectSkuBarcode();
        for (int i = 1; i < int.Parse(txt_row.Text); i++)
        {
          
           dr = dt.Rows[0];
           dt.ImportRow(dr);
        }

        CORNBusinessLayer.Reports.CrpBarcode CrpReport = new CORNBusinessLayer.Reports.CrpBarcode();
        CrpReport.SetDataSource(dt);
        CrpReport.Refresh();


        this.Session.Add("ReportType", 0);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

    }
    protected void btn_print_Click(object sender, EventArgs e)
    {
       
        if ((txt_row.Text != null) && (txt_col.Text != null))
        {
            int tblRows = Convert.ToInt32(txt_row.Text);
            int tblCols = Convert.ToInt32(txt_col.Text);
           
            Unit width = new Unit(500, UnitType.Pixel);

            Table tbl = new Table();
            tbl.ID = "tcResource"; 
            tbl.Width = width;
            tbl.Height = Unit.Pixel(1200);
            tbl.BorderStyle = BorderStyle.Double;
            tbl.BorderWidth = 2;
            tbl.CellPadding = 0;
            tbl.CellSpacing = 0;

            tbl.HorizontalAlign = HorizontalAlign.Center;
            

            for (int i = 0; i < tblRows; i++)
            {
                TableRow tr = new TableRow();
                tr.HorizontalAlign = HorizontalAlign.Center;
                for (int j = 0; j < tblCols; j++)
                {
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    Label lblpname = new Label();
                    
                    lblpname.Font.Size = new FontUnit(10);
                    Label lblprice = new Label();
                    
                    lblprice.Font.Size = new FontUnit(10);
                    Label lblCompany = new Label();

                    TableCell tc = new TableCell();
                  
                    tc.Width = Unit.Pixel(125);
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    tc.BorderStyle = BorderStyle.Double;
                    tc.BorderWidth = 2;

                    Panel dpnl1 = new Panel();
                    Panel dpnl2 = new Panel();
                    //Panel dpnl3 = new Panel();
                    Panel dpnl4 = new Panel();

                    lblCompany.Text = "need";
                   
                    //lblprice.Text = + txt_price.Text;
                   
                    lblpname.Text = txt_productName.Text + " " + " " +"Rs." + " " + txt_price.Text;
                    
                    
                    var data = Session["base64Data"];
                    img.ImageUrl = "data:image/Jpeg;base64," + data;
                    img.Height = Unit.Pixel(60);
                    img.Width = Unit.Pixel(300);
                    // Add the control to the TableCell

                    dpnl1.Controls.Add(lblCompany);
                    dpnl2.Controls.Add(lblpname);
                    //dpnl3.Controls.Add(lblprice);
                    dpnl4.Controls.Add(img);

                    tc.Controls.Add(dpnl1);
                    tc.Controls.Add(dpnl2);
                    //tc.Controls.Add(dpnl3);
                    tc.Controls.Add(dpnl4);
                    tr.Cells.Add(tc);
                }
                // Add the TableRow to the Table
                tbl.Rows.Add(tr);
                // Panel1.Controls.Add(tbl);
                Session["ctrl"] = tbl;
            }
        }
        Control ctrl = (Control)Session["ctrl"];
      //  PrintHelper.PrintWebControl(ctrl);
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        //string Script;
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ctrl is WebControl)
        {
            Unit w = new Unit(40, UnitType.Percentage);
            ((WebControl)ctrl).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        
        pg.Controls.Add(frm);
        
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        //HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

       
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
      //  LoadGrid(Convert.ToInt32(hfSKUID.Value));
    }
}