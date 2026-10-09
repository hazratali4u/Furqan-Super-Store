<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmOrderEntryStep1.aspx.cs" Inherits="Forms_frmOrderEntryStep1" Title="CORN :: Order/Invoice Step 1" %>
 <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %> 
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
     <div id="right_data">
     <div>
     <span class="heading">Order/Invoice Step 1</span>
     </div>
     <div >
        <table width="100%">
            <tr>
                <td align="left">
                    <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 309px;
                        height: 100px">
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="27px" />
                                    Wait Update.......
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </td>
                <td align="left">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<TABLE><TBODY><TR><TD align=left>
<strong> <asp:Label id="Label2" runat="server" Width="104px" Text="Transaction Type" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left><asp:DropDownList id="DrpDocumentType" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged"><asp:ListItem Value="0">Order Entry</asp:ListItem>
<asp:ListItem Value="1">Spot Sale</asp:ListItem>
    <asp:ListItem Value="2">Sale Return</asp:ListItem>
</asp:DropDownList>&nbsp;</TD></TR><TR><TD align=left>
<strong> <asp:Label id="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left><asp:DropDownList id="drpDistributor" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"></asp:DropDownList></TD></TR><TR><TD align=left>
<strong><asp:Label id="Label1" runat="server" Width="94px" Text="Principal" CssClass="lblbox" __designer:wfdid="w1"></asp:Label></strong></TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left><asp:DropDownList id="DrpPrincipal" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" __designer:wfdid="w2"></asp:DropDownList></TD></TR><TR><TD align=left>
<strong><asp:Label id="lblfromLocation" runat="server" Width="94px" Text="Customer Route" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left><asp:DropDownList id="DrpRoute" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged"></asp:DropDownList></TD></TR><TR><TD align=left>
<strong><asp:Label id="Label6" runat="server" Width="94px" Text="Order Booker" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left><asp:DropDownList id="DrpOrderBooker" runat="server" Width="200px" CssClass="DropList"></asp:DropDownList></TD></TR><TR><TD align=left>
<strong><asp:Label id="Label5" runat="server" Width="94px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left><asp:DropDownList id="DrpDeliveryMan" runat="server" Width="200px" CssClass="DropList"></asp:DropDownList></TD></TR>

<tr>
        <td align="left">
            <strong><asp:Label ID="Label7" runat="server" Text="Working Date" Width="94px"></asp:Label></strong></td>
        <td align="left" style="width: 238px; height: 25px">
            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtBox" MaxLength="10" Width="150px"></asp:TextBox><asp:ImageButton
                ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                Format="dd-MMM-yyyy" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate">
            </cc1:CalendarExtender>
           </td>
    </tr>


<TR><TD align=left>
<strong><asp:Label id="Label4" runat="server" Width="94px" Text="Quantity Type" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left><asp:RadioButtonList id="RbUnitType" runat="server" Width="225px" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Quantity In Unit</asp:ListItem>
                <asp:ListItem>Quantity In Ctns</asp:ListItem>
            </asp:RadioButtonList></TD></TR><TR><TD align=right>
            <asp:Button id="btnGetOrder" onclick="btnGetOrder_Click" runat="server" Width="100px" Font-Size="8pt" Text="Get Order" CssClass="Button" /> </TD><TD style="WIDTH: 238px; HEIGHT: 25px" align=left>
            <asp:Button id="btnNext" onclick="btnNext_Click" runat="server" Width="100px" Font-Size="8pt" Text="Next" CssClass="Button" /> </TD></TR><TR><TD align=right colSpan=2>
            <asp:GridView id="GrdFreeSKU" runat="server" Visible="False" Width="100%" Height="57px" ForeColor="Red" CssClass="gridRow2" AutoGenerateColumns="False" BackColor="White" BorderColor="White" HorizontalAlign="Center">
                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                    PreviousPageText="Previous" />
                <RowStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" />
                <Columns>
                    <asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Order Id">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                            Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Order Qty">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BALANCE" HeaderText="Stock">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                            Width="50px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" />
                <PagerStyle BackColor="Transparent" />
                <HeaderStyle BackColor="Red" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="Black" />
            </asp:GridView>
             <asp:GridView id="gvRateDifference" runat="server" Visible="False" Width="100%" Height="57px" ForeColor="Red" CssClass="gridRow2" AutoGenerateColumns="False" BackColor="White" BorderColor="White" HorizontalAlign="Center">
<PagerSettings PreviousPageText="Previous" Mode="NextPrevious" LastPageText="" FirstPageText="" NextPageText="Next"></PagerSettings>

<FooterStyle BackColor="White"></FooterStyle>
<Columns>
<asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Order Id">
<ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
<ItemStyle BorderStyle="Solid" Width="80px" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Order_Rate" HeaderText="Order Rate">
<ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SSR_Rate" HeaderText="SSR Rate">
<ItemStyle BorderStyle="Solid" Width="50px" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
</Columns>

<RowStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" BorderColor="Silver"></RowStyle>

<PagerStyle BackColor="Transparent"></PagerStyle>

<HeaderStyle BackColor="Red" BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
</asp:GridView>

 </TD></TR></TBODY></TABLE>
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
     <div >
    <table width="100%">
        <tr>
            <td style="width: 100px; height: 173px;" align="left" valign="top">
                <asp:UpdatePanel id="UpdatePanel2" runat="server">
                    <ContentTemplate>
<table style="width : 700px;">
<tbody>
<tr>
<td >
<strong><asp:CheckBox id="cbAll" runat="server" Width="103px" Text="Select All" AutoPostBack="True" OnCheckedChanged="cbAll_CheckedChanged"></asp:CheckBox> </strong></td>
<td>
<strong><asp:Label id="Label3" runat="server" Width="100px" Text="Payment Mode"></asp:Label></strong> </td>
<td>
<asp:DropDownList id="ddSearchType" runat="server" Width="176px" CssClass="DropList">
                                            <asp:ListItem Value="214">Cash</asp:ListItem>
                                            <asp:ListItem Value="215">Credit</asp:ListItem>
                                            <asp:ListItem Value="216">Advance</asp:ListItem>
                                        </asp:DropDownList> </td>
                                        <td>
                                        <asp:Button id="btnPost" onclick="btnPost_Click" runat="server" Width="128px" Font-Size="8pt" Text="Convert to Invoice" CssClass="Button" /> </td>
                                        <td ><asp:Button id="btnPrint" runat="server" Width="104px" Font-Size="8pt" Text="Print Invoice" CssClass="Button" /> </td>
                                        </tr><tr><td style="WIDTH: 500px; HEIGHT: 21px" colspan="5">
                                        <asp:Panel id="Panel1" runat="server" Width="710px" Height="180px" ScrollBars="Vertical">
<asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center" Width="700px">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
PreviousPageText="Previous" />
<Columns>
<asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
<HeaderStyle CssClass="HidePanel" />
<ItemStyle CssClass="HidePanel" />
</asp:BoundField>
<asp:TemplateField HeaderText="Select">
<ItemTemplate>
<asp:CheckBox ID="ChbInvoice" runat="server" />
</ItemTemplate>
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
</asp:TemplateField>
<asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Sale Order Id">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
</asp:BoundField>
<asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Order Date">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
</asp:BoundField>
<asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gross Amount">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
</asp:BoundField>
<asp:BoundField DataField="DISCOUNT_AMOUNT" DataFormatString="{0:F2}" HeaderText="Discount">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
</asp:BoundField>
<asp:BoundField DataField="SCHEME_AMOUNT" DataFormatString="{0:F2}" HeaderText="Scheme">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
</asp:BoundField>
<asp:BoundField DataField="GST_AMOUNT" DataFormatString="{0:F2}" HeaderText="GST Amount">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
</asp:BoundField>
<asp:BoundField DataField="TOTAL_NET_AMOUNT" DataFormatString="{0:F2}" HeaderText="Net Amount">
<ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
</asp:BoundField>
<asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID" >
<HeaderStyle CssClass="HidePanel" />
<ItemStyle CssClass="HidePanel" />
</asp:BoundField>
 <asp:BoundField DataField="MANUAL_ORDER_ID" HeaderText="Bill Book No">
 <HeaderStyle CssClass="HidePanel" />
 <ItemStyle CssClass="HidePanel" />
 </asp:BoundField>
 </Columns>
 <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
 </asp:GridView>
 </asp:Panel> </TD></TR></TBODY>
 </table>
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>   
   </div>
</asp:Content>


