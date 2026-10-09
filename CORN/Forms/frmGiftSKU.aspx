<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmGiftSKU.aspx.cs" Inherits="Forms_frmGiftSKU" Title="CORN :: Gift SKU Entry" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">
 
  function ValidateForm()
 {
		var str;
    
		str  = document.getElementById('<%=txtskuName.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Select SKU Name');
			return false;
		}
		str  = document.getElementById('<%=txtQuantity.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Enter Quantity');
			return false;
		}
		
        return true;
 }
 function SearcSKUList()
 {
     var l =  document.getElementById('<%= lstCode.ClientID %>');
     var tb = document.getElementById('<%= txtskuCode.ClientID %>');
        
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
 
 
 function SearchSKUCode()
 {
    var str  = document.getElementById("<%= lstCode.ClientID %>").value;
    var stroption  = document.getElementById("<%= txtskuCode.ClientID %>").value; 
    
    if(str.length > 0)
     {      
            document.getElementById("<%= txtskuCode.ClientID %>").value =  str.substring(0,str.indexOf('-'));
            document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1,str.indexOf(':'));
            document.getElementById("<%= txtUnitRate.ClientID %>").value =  str.substring(str.indexOf(':') + 1);
                
     }
     ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));
    
 }
 function SelectSkuCode(e)
 {
      if(e.keyCode == 13)
      {
        var str  = document.getElementById("<%= lstCode.ClientID %>").value;
        document.getElementById("<%= txtskuCode.ClientID %>").value =  str.substring(0,str.indexOf('-'));
        document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1,str.indexOf(':'));
        document.getElementById("<%= txtUnitRate.ClientID %>").value =  str.substring(str.indexOf(':') + 1);
        document.getElementById("<%= txtQuantity.ClientID %>").focus();
      }
 }

 function ClearSelection(lb)
 {
    lb.selectedIndex = -1;
 }
 
  </script>
     <div id="right_data">
         <table width="100%">
             <tr>
                 <td >
                     <div style="z-index: 101; left: 477px; width: 100px; position: absolute; top: 229px;
                         height: 100px">
                         &nbsp;<asp:Panel ID="Panel21" runat="server">
                             <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                 <ProgressTemplate>
                                     <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                         Width="23px" />
                                     Wait Update
                                 </ProgressTemplate>
                             </asp:UpdateProgress>
                         </asp:Panel>
                     </div>
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                         <ContentTemplate>
<TABLE><TBODY><TR><TD vAlign=top align=left>
<strong> <asp:Label id="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=top align=left colSpan=2><asp:DropDownList id="drpDistributor" runat="server" Width="280px" CssClass="DropList" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                         </asp:DropDownList></TD></TR><TR><TD style="HEIGHT: 17px" vAlign=top align=left>
                                         <strong><asp:Label id="Label5" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 17px" vAlign=top align=left colSpan=2><asp:DropDownList id="DrpPrincipal" runat="server" Width="280px" CssClass="DropList" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                         </asp:DropDownList></TD></TR><TR><TD style="HEIGHT: 17px" vAlign=top align=left>Sale Force</TD><TD style="HEIGHT: 17px" vAlign=top align=left colSpan=2><asp:DropDownList id="DrpSaleForce" runat="server" Width="280px" CssClass="DropList" OnSelectedIndexChanged="DrpSaleForce_SelectedIndexChanged" AutoPostBack="True" __designer:wfdid="w1"></asp:DropDownList></TD></TR><TR><TD style="HEIGHT: 17px" vAlign=top align=left>
                                         <strong><asp:Label id="lblDocumentNo" runat="server" Width="79px" Text="Invoice No" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 17px" vAlign=top align=left colSpan=2><asp:DropDownList id="drpDocumentNo" runat="server" Width="280px" CssClass="DropList" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></TD></TR><TR><TD vAlign=top align=left>
                                         <strong><asp:Label id="Label1" runat="server" Width="94px" Text="Customer Code" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=top align=left><asp:TextBox id="txtOutletCode" onkeyup="SearchList()" runat="server" Width="76px" CssClass="txtBox " ReadOnly="True"></asp:TextBox></TD><TD align=left><asp:TextBox id="txtOutletName" onfocus="SearchedCode()" runat="server" Width="188px" CssClass="txtBox " ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="HEIGHT: 34px" vAlign=top align=left colSpan=3><asp:ListBox id="lstCode" onkeyup="SelectSkuCode(event)" runat="server" Width="374px" Height="288%" SelectionMode="Multiple"></asp:ListBox></TD></TR></TBODY></TABLE>&nbsp;&nbsp; 
</ContentTemplate>
                     </asp:UpdatePanel>
                     <strong><asp:CheckBox ID="ChbAllTax" runat="server" Checked="True" Text="Is Apply All Tax" /></strong>
                     
                 </td>
             </tr>
             <tr>
                 <td >
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
<TABLE><TBODY><TR><TD style="HEIGHT: 16px"><asp:Label id="lblskuCode" runat="server" Width="74px" Height="16px" ForeColor="White" Font-Bold="True" Text="  SKU Code" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD style="HEIGHT: 16px"><asp:Label id="lblskuname" runat="server" Width="187px" Height="16px" ForeColor="White" Font-Bold="True" Text="   SKU Name" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD style="HEIGHT: 16px"><asp:Label id="Label3" runat="server" Width="61px" Height="16px" ForeColor="White" Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD style="HEIGHT: 16px"><asp:Label id="lblquantity" runat="server" Width="62px" Height="16px" ForeColor="White" Font-Bold="True" Text="Unit Price" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD style="HEIGHT: 16px" align=center><asp:Label id="Label7" runat="server" Width="100%" Height="16px" ForeColor="White" Font-Bold="True" Text="GST Rate" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD style="HEIGHT: 16px"><asp:Label id="Label2" runat="server" Width="100%" Height="16px" ForeColor="White" Font-Bold="True" Text="Amount" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD style="HEIGHT: 16px"><asp:Label id="Label4" runat="server" Width="100%" Height="16px" ForeColor="White" Font-Bold="True" Text="Add SKU" CssClass="lblbox" BackColor="#006699"></asp:Label></TD></TR><TR><TD style="HEIGHT: 21px"><asp:TextBox id="txtskuCode" onkeyup="SearcSKUList()" runat="server" Width="68px" CssClass="txtBox"></asp:TextBox></TD><TD style="HEIGHT: 21px"><asp:TextBox id="txtskuName" runat="server" Width="181px" Font-Bold="True" CssClass="txtBox" Enabled="False"></asp:TextBox></TD><TD style="HEIGHT: 21px"><asp:TextBox id="txtQuantity" onfocus="SearchSKUCode()" runat="server" Width="55px" CssClass="txtBox "></asp:TextBox></TD><TD style="HEIGHT: 21px"><asp:TextBox id="txtUnitRate" runat="server" Width="56px" CssClass="txtBox" Enabled="False"></asp:TextBox></TD><TD style="HEIGHT: 21px"><asp:TextBox id="txtBatchNo" onfocus="SearchSKUCode()" runat="server" Width="62px" CssClass="txtBoxnNum"></asp:TextBox></TD><TD style="HEIGHT: 21px"><asp:TextBox id="TextBox1" onfocus="SearchSKUCode()" runat="server" Width="72px" CssClass="txtBoxnNum" Enabled="False"></asp:TextBox></TD><TD style="HEIGHT: 21px">
<asp:Button accessKey="A" id="btnSave" onclick="btnSave_Click" runat="server" Width="100px" Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" /> </TD></TR><TR><TD align=left colSpan=7><asp:Panel id="Panel2" runat="server" Width="640px" Height="130px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver"><asp:GridView id="GrdPurchase" runat="server" ForeColor="SteelBlue" CssClass="gridRow2" BackColor="White" BorderColor="White" ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" HorizontalAlign="Center" AutoGenerateColumns="False">
                                                 <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                     PreviousPageText="Previous" />
                                                 <RowStyle ForeColor="Black" />
                                                 <Columns>
                                                     <asp:BoundField DataField="SALE_INVOICE_PROMOTION_ID" HeaderText="SALE_INVOICE_PROMOTION_ID">
                                                         <HeaderStyle CssClass="HidePanel" />
                                                         <ItemStyle CssClass="HidePanel" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                         <HeaderStyle CssClass="HidePanel" />
                                                         <ItemStyle CssClass="HidePanel" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                             Width="75px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                             Width="200px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                             Width="65px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="UNIT_PRICE" DataFormatString="{0:F2}" HeaderText="PRICE">
                                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                             Width="65px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="GST_RATE" HeaderText="GST Rate">
                                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Amount" DataFormatString="{0:F2}" HeaderText="Amount">
                                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                             Width="80px" />
                                                     </asp:BoundField>
                                                     <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                         <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" />
                                                     </asp:CommandField>
                                                     <asp:TemplateField HeaderText="Delete">
                                                         <ItemTemplate>
                                                             <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                 Text="Delete"></asp:LinkButton>
                                                         </ItemTemplate>
                                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                     </asp:TemplateField>
                                                 </Columns>
                                                 <FooterStyle BackColor="White" />
                                                 <PagerStyle BackColor="Transparent" />
                                                 <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                     VerticalAlign="Middle" />
                                                 <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                             </asp:GridView> </asp:Panel> </TD></TR></TBODY></TABLE>
</ContentTemplate>
                     </asp:UpdatePanel>
                 </td>
             </tr>
         </table>
        
           </div>   
</asp:Content>
