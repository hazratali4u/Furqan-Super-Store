<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmSaleForce.aspx.cs" Inherits="Forms_frmSaleForce" Title="CORN :: Employee Information" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm()
		{
			var str;
			str  = document.getElementById('<%=txtUserName.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter User Name');
				return false;
			}
			str  = document.getElementById('<%=txtNICNo.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter NIC No');
				return false;
			}
			str  = document.getElementById('<%=txtLoginId.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Login Id');
				return false;
			}
			str  = document.getElementById('<%=txtpassword.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Password');
				return false;
			}
			str  = document.getElementById('<%=txtMobileNo.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Mobile No');
				return false;
			}
			str  = document.getElementById('<%=txtAddress2.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Address');
				return false;
			}
			return true;		
		}
    </script>
    <div id="right_data">
    <div>
        <table width="100%">
            <tr>
                <td style="width: 100px">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<TABLE><TBODY><TR><TD style="WIDTH: 143px" align=left></TD><TD style="WIDTH: 175px">
<asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></TD><TD style="WIDTH: 1px"></TD><TD align=left></TD><TD></TD></TR><TR><TD style="WIDTH: 143px; HEIGHT: 15px" align=left>
<strong><asp:Label id="lbldesignationID" runat="server" Width="94px" Text="Base Location" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 15px"><asp:DropDownList id="ddDistributorId" runat="server" Width="205px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged"></asp:DropDownList></TD><TD style="WIDTH: 1px; HEIGHT: 15px">&nbsp; &nbsp; </TD><TD style="HEIGHT: 15px" align=left>
<strong><asp:Label id="Label1" runat="server" Width="96px" Text="Name" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 15px"><asp:TextBox id="txtUserName" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 143px; HEIGHT: 12px" align=left>
<strong><asp:Label id="Label4" runat="server" Width="79px" Text="Designation" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 12px"><asp:DropDownList id="ddDesignation" runat="server" Width="205px" CssClass="DropList"></asp:DropDownList></TD><TD style="WIDTH: 1px; HEIGHT: 12px"></TD><TD style="HEIGHT: 12px" align=left>
<strong><asp:Label id="Label5" runat="server" Width="79px" Text="N.I.C No" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 12px"><asp:TextBox id="txtNICNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox></TD></TR><TR><TD style="WIDTH: 143px; HEIGHT: 22px" align=left>
<strong><asp:Label id="lblMobileNo" runat="server" Width="96px" Text="Mobile No:" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 22px"><asp:TextBox id="txtMobileNo" runat="server" Width="198px" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 1px; HEIGHT: 22px"></TD><TD style="HEIGHT: 22px" align=left>
<strong><asp:Label id="lblPhNo" runat="server" Width="90px" Text="Phone No:" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 22px"><asp:TextBox id="txtPhoneNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox></TD></TR><TR><TD style="WIDTH: 143px" vAlign=top align=left>
<strong><asp:Label id="lblEmail" runat="server" Width="87px" Text="Email Address" CssClass="lblbox"></asp:Label></strong></TD><TD colSpan=4><asp:TextBox id="txtEmail" runat="server" Width="514px" CssClass="txtBox "></asp:TextBox></TD></TR><TR><TD style="WIDTH: 143px" vAlign=top align=left>
<strong><asp:Label id="lblAddress2" runat="server" Width="130px" Text="Permanent Address " CssClass="lblbox"></asp:Label></strong></TD><TD colSpan=4><asp:TextBox id="txtAddress2" runat="server" Width="514px" CssClass="txtBox "></asp:TextBox></TD></TR><TR><TD style="WIDTH: 143px" vAlign=top align=left>
<strong><asp:Label id="lblAddress1" runat="server" Width="110px" Text="Present Address " CssClass="lblbox"></asp:Label></strong></TD><TD colSpan=4><asp:TextBox id="txtAddress1" runat="server" Width="514px" CssClass="txtBox "></asp:TextBox></TD></TR><TR><TD style="WIDTH: 143px" vAlign=top align=left>
<strong><asp:Label id="Label3" runat="server" Visible="False" Width="82px" Text="Login Id" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px"><asp:TextBox id="txtLoginId" runat="server" Visible="False" Width="200px" CssClass="txtBox "></asp:TextBox></TD><TD style="WIDTH: 1px"></TD><TD vAlign=top align=left>
<strong><asp:Label id="Label2" runat="server" Visible="False" Width="69px" Text="Password" CssClass="lblbox"></asp:Label></strong></TD><TD><asp:TextBox id="txtpassword" runat="server" Visible="False" Width="200px" CssClass="txtBox "></asp:TextBox></TD></TR><TR><TD style="WIDTH: 143px; HEIGHT: 21px" align=left></TD><TD style="WIDTH: 175px; HEIGHT: 21px">
<asp:Button id="btnSave" onclick="btnSave_Click" runat="server" Width="82px" Font-Size="8pt" Text="Save" ValidationGroup="vg" CssClass="Button" /> 
<asp:Button id="btnCancel" runat="server" Width="73px" Font-Size="8pt" Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" /> </TD><TD style="WIDTH: 1px; HEIGHT: 21px"></TD><TD style="HEIGHT: 21px"><asp:CheckBox id="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"></asp:CheckBox></TD><TD style="HEIGHT: 21px"></TD></TR></TBODY></TABLE>
</contenttemplate>
                    </asp:UpdatePanel></td>
            </tr>
        </table>
        
           </div>
    <div>
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
                <asp:Panel ID="Panel2" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
<asp:GridView id="Grid_users" runat="server" Width="99%" ForeColor="SteelBlue" CssClass="gridRow2" BorderColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" OnRowEditing="Grid_users_RowEditing" PageSize="15">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>

<Columns>
<asp:BoundField DataField="USER_ID" HeaderText="User Id">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="USER_CODE" HeaderText="Code">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="USER_NAME" HeaderText="Name">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NIC_NO" HeaderText="NIC No">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PHONE" HeaderText="Phone No">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="MOBILE" HeaderText="Mobile">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="EMAIL" HeaderText="Email">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ADDRESS1" HeaderText="Present Address">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ADDRESS2" HeaderText="Permanent Address">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SLASH_DESC" HeaderText="Designation">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="USER_TYPE_ID" HeaderText="USERTYPE_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="COMPANY_ID" HeaderText="COMPANY_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:CommandField>
</Columns>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead"></HeaderStyle>
</asp:GridView> 
                </asp:Panel>
</contenttemplate>
        </asp:UpdatePanel>
        
    </div>
    </div>   
</asp:Content>
