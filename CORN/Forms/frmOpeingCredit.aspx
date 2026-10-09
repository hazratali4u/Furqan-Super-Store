<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmOpeingCredit.aspx.cs" Inherits="Forms_frmOpeingCredit" Title="CORN :: Opening Credit" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">
    
    function ValidateForm()
    {
        var str;
        str  =  document.getElementById("<%= txtOutletCode.ClientID %>").value;
        if(str == null || str.length == 0)
		{
			alert('Must enter Outlet Code');
			return false;
		}
		str  =  document.getElementById("<%= txtOutletName.ClientID %>").value;
        if(str == null || str.length == 0)
		{
			alert('Must enter Outlet Code');
			return false;
		}
		str  =  document.getElementById("<%= txtAmount.ClientID %>").value;
		if(str == null || str.length == 0)
		{
			alert('Must Enter Amount');
			return false;
		}
		str  =  document.getElementById("<%= txtFromdate.ClientID %>").value;
		if(str == null || str.length <= 1)
		{
			alert('Must Enter Invoice Date');
			return false;
		}
		str  =  document.getElementById("<%= txtChequeNo.ClientID %>").value;
		if(str == null || str.length <= 1)
		{
			alert('Must Enter Invoice No');
			return false;
		}
    }
 function SearchList()
 {
     var l =  document.getElementById('<%= ListCustomer.ClientID %>');
     var tb = document.getElementById('<%= txtOutletCode.ClientID %>');
        
            if(tb.value == "")
            {
                ClearSelection(l);
            }
            else
            {
                for (var i=0; i < l.options.length; i++)
                {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase()))
                    {
                        l.options[i].selected = true;
                        return false;
                    }
                    else
                    {
                        ClearSelection(l);
                    }
                }
           }
 }
 function SearchedCode()
 {
    var str  = document.getElementById("<%= ListCustomer.ClientID %>").value;
    var stroption  = document.getElementById("<%= txtOutletCode.ClientID %>").value;
     if(str.length > 0)
     {      
            document.getElementById("<%= txtOutletCode.ClientID %>").value =  "OT" + str.substring(str.indexOf('|') + 2);
            document.getElementById("<%= txtOutletName.ClientID %>").value =  str.substring(0,str.indexOf('|'));
                
     }
     ClearSelection(document.getElementById('<%= ListCustomer.ClientID %>'));
    
 }
 function SelectCode(e)
 {
      if(e.keyCode == 13)
      {
        var str  = document.getElementById("<%= ListCustomer.ClientID %>").value;
        document.getElementById("<%= txtOutletCode.ClientID %>").value = "OT" + str.substring(str.indexOf('|') + 2);
        document.getElementById("<%= txtOutletName.ClientID %>").value =  str.substring(0,str.indexOf('|'));
        document.getElementById("<%= txtChequeNo.ClientID %>").focus();
      }
 }
 function ClearSelection(lb)
 {
    lb.selectedIndex = -1;
 }
 
  </script>
  <div id="right_data">
    <div >
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel id="UpdatePanel2" runat="server" RenderMode="Inline">
                        <contenttemplate>
<TABLE><TBODY><TR><TD align=left colSpan=2><asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label> </TD>
    <TD style="WIDTH: 50px" align=left colSpan=1></TD><TD align=left colSpan=1></TD></TR><TR><TD align=left>
    <strong> <asp:Label id="Label8" runat="server" Width="89px" Text="Principal" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="DrpPrincipal" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
            </asp:DropDownList></TD><TD style="WIDTH: 50px" align=left></TD><TD align=left><asp:DropDownList id="DrpRoute" runat="server" Width="250px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged"></asp:DropDownList></TD></TR><TR><TD align=left>
            <strong><asp:Label id="Label7" runat="server" Width="94px" Text="Credit Type" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="DrpCreditType" runat="server" Width="200px" CssClass="DropList"><asp:ListItem Value="24">New Credit</asp:ListItem>
<asp:ListItem Value="25">Opening Credit</asp:ListItem>
<asp:ListItem Value="26">Credit Transfer In</asp:ListItem>
</asp:DropDownList></TD><TD style="WIDTH: 50px" align=left></TD><TD align=left rowSpan=11><asp:ListBox id="ListCustomer" onkeyup="SelectCode(event)" runat="server" Width="250px" Height="210px" CssClass="DropList"></asp:ListBox></TD></TR><TR><TD align=left>
<strong><asp:Label id="lblfromLocation" runat="server" Width="94px" Text="Distributor" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="drpDistributor" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"></asp:DropDownList></TD><TD style="WIDTH: 201px" align=left>
<strong><asp:Label id="lblDocumentNo" runat="server" Width="32px" Text="            " CssClass="lblbox"></asp:Label></strong></TD></TR><TR><TD style="HEIGHT: 21px" align=left>
<strong><asp:Label id="Label2" runat="server" Width="89px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 21px"><asp:DropDownList id="DrpDeliveryMan" runat="server" Width="200px" CssClass="DropList">
    </asp:DropDownList></TD><TD style="HEIGHT: 21px" align=left></TD></TR><TR><TD align=left>
    <strong><asp:Label id="Label1" runat="server" Width="89px" Text="Bill Date" CssClass="lblbox"></asp:Label></strong></TD><TD><asp:TextBox style="TEXT-ALIGN: justify" id="txtFromdate" onkeyup="BlockFromDateKeyPress()" runat="server" Width="192px" CssClass="txtBox" MaxLength="1" ReadOnly="True"></asp:TextBox></TD><TD align=left><asp:ImageButton id="ImgBntFromCalc" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Granite/Images/date.gif" Enabled="False"></asp:ImageButton></TD></TR><TR><TD vAlign=bottom align=left>
    <strong><asp:Label id="Label4" runat="server" Width="94px" Text="Customer Code" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=bottom align=left>
    <strong><asp:Label id="Label3" runat="server" Width="94px" Text="Customer Name" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=bottom align=left></TD></TR><TR><TD vAlign=top align=left><asp:TextBox id="txtOutletCode" onkeyup="SearchList()" runat="server" Width="112px" CssClass="txtBox "></asp:TextBox></TD><TD vAlign=top><asp:TextBox id="txtOutletName" runat="server" Width="193px" ForeColor="Black" Font-Bold="True" CssClass="txtBox " Enabled="False"></asp:TextBox></TD><TD vAlign=top><asp:HiddenField id="hfPrincipalID" runat="server" __designer:wfdid="w1" Value="-1"></asp:HiddenField> <asp:HiddenField id="hfLegendID" runat="server" Visible="False" __designer:wfdid="w2" Value="-1"></asp:HiddenField> <asp:HiddenField id="hfCustomerID" runat="server" __designer:wfdid="w3" Value="-1"></asp:HiddenField> <asp:HiddenField id="hfSaleInvoiceID" runat="server" __designer:wfdid="w4" Value="-1"></asp:HiddenField></TD></TR><TR><TD align=left>
    <strong><asp:Label id="Label5" runat="server" Width="94px" Text="Invoice No" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 201px" vAlign=top align=left>
    <strong><asp:Label id="Label6" runat="server" Width="74px" Text="Amount" CssClass="lblbox"></asp:Label></strong> </TD><TD vAlign=top align=left></TD></TR><TR><TD vAlign=top align=left><asp:TextBox id="txtChequeNo" onfocus="SearchedCode()" runat="server" Width="112px" CssClass="uppercase" MaxLength="10"></asp:TextBox></TD><TD vAlign=top align=left><asp:TextBox id="txtAmount" runat="server" Width="118px" CssClass="txtBox "></asp:TextBox></TD><TD vAlign=top align=left>&nbsp;</TD></TR><TR><TD vAlign=top align=left>
    <strong><asp:Label id="Label9" runat="server" Width="73px" Text="Remarks" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=top align=left></TD><TD vAlign=top align=left></TD></TR><TR><TD style="HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtRemarks" runat="server" Width="239px" CssClass="txtBox "></asp:TextBox></TD><TD style="HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD align=left colSpan=2></TD><TD align=left colSpan=1></TD></TR><TR><TD style="HEIGHT: 36px" align=left>
    <asp:Button accessKey="S" id="btnSave" onclick="btnSave_Click" runat="server" Width="119px" Font-Size="8pt" Text="Save" CssClass="Button" /> <DIV style="Z-INDEX: 101; LEFT: 503px; WIDTH: 100px; POSITION: absolute; TOP: 251px; HEIGHT: 100px"><asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                        Width="22px" />
                    Record Update
                </ProgressTemplate>
            </asp:UpdateProgress> </DIV></TD><TD style="WIDTH: 201px; HEIGHT: 36px" vAlign=middle align=left>
            <asp:Button accessKey="C" id="btnCancel" onclick="btnCancel_Click" runat="server" Width="120px" Font-Size="8pt" Text="Cancel" CssClass="Button" /> </TD><TD style="WIDTH: 201px; HEIGHT: 36px" vAlign=middle align=left></TD><TD style="WIDTH: 201px; HEIGHT: 36px" vAlign=middle align=left></TD></TR></TBODY></TABLE><ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount"></ajaxToolkit:FilteredTextBoxExtender> <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Enabled="False" TargetControlID="txtFromdate" Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc">
                            </ajaxToolkit:CalendarExtender> &nbsp; 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="DrpRoute" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        
           </div>
    <div >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
<TABLE style="BORDER-RIGHT: silver thin inset; BORDER-TOP: silver thin inset; BORDER-LEFT: silver thin inset; WIDTH: 650px; BORDER-BOTTOM: silver thin inset"><TBODY><TR><TD style="HEIGHT: 20px" align=left colSpan=5><asp:Panel id="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Vertical"><asp:GridView id="GrdOrder" runat="server" Width="728px" ForeColor="SteelBlue" CssClass="gridRow2" AutoGenerateColumns="False" BackColor="White" BorderColor="White" HorizontalAlign="Center" OnRowEditing="GrdOrder_RowEditing" OnRowDeleting="GrdOrder_RowDeleting">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>

<asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Principal">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Invoice Date"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("DOCUMENT_DATE") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:Label runat="server" Text='<%# Bind("DOCUMENT_DATE", "{0:dd-MM-yyyy}") %>' id="Label1"></asp:Label>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="Invoice No">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="LEGEND_ID" HeaderText="LEGEND_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>
<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>
<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>

<asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DOCUMENT_DATE">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>
<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>

<asp:BoundField DataField="SALE_INVOICE_MASTER_ID" HeaderText="SALE_INVOICE_MASTER_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>
<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>

<asp:CommandField ShowEditButton="True" ShowHeader="True" HeaderText="Edit"></asp:CommandField>

<asp:TemplateField HeaderText="Delete">
<ItemTemplate>
<asp:LinkButton id="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" CommandName="Delete"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>

</Columns>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead"></HeaderStyle>
</asp:GridView> </asp:Panel> </TD></TR></TBODY></TABLE>
</ContentTemplate>
        </asp:UpdatePanel>
    </div>   
    </div>
</asp:Content>





