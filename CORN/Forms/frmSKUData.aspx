<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUData.aspx.cs" Inherits="Forms_frmSKUData" Title="CORN :: SKU Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" language="javascript">

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtbarcode.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter SKU Barcode');
                return false;
            }
            str = document.getElementById('<%=txtskuname.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter SKU Name');
                return false;
            }
            return true;
        }
          
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="pnlHeader" runat="server">
            <ContentTemplate>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="100px" Text="SKU Category"></asp:Label></strong>
                                            </td>
                                            <td align="left" width="300px">
                                                <asp:DropDownList ID="ddskucategory" runat="server" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddskucategory_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 100px">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Text="GST On"></asp:Label></strong>
                                            </td>
                                            <td align="left" width="300px">
                                                <asp:DropDownList ID="DrpSKUTaxType" runat="server" Width="200px" CssClass="DropList">
                                                    <asp:ListItem Value="T" Text="Trade Price"></asp:ListItem>
                                                    <asp:ListItem Value="R" Text="Retail Price"></asp:ListItem>
                                                    <asp:ListItem Value="E" Text="Exempted"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Text="Bar Code"></asp:Label></strong>
                                            </td>
                                            <td align="left" width="300px">
                                                <asp:TextBox ID="txtbarcode" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Text="SKU Name"></asp:Label></strong>
                                            </td>
                                            <td align="left" width="300px">
                                                <asp:TextBox ID="txtskuname" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Text="Size"></asp:Label></strong>
                                            </td>
                                            <td width="300px">
                                                <asp:TextBox ID="txtpacksize" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label12" runat="server" Text="Unit in Case"></asp:Label></strong>
                                            </td>
                                            <td width="300px">
                                                <asp:TextBox ID="txtUnit" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Text="Unit In Measure"></asp:Label></strong>
                                            </td>
                                            <td align="left" width="280px">
                                                <asp:CheckBox ID="chkUom" runat="server" />
                                                <asp:DropDownList ID="drpUom" runat="server" Width="120px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 12px">
                                            </td>
                                            <td style="width: 37px; height: 12px">
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtUnit"
                                                    ValidChars=".0123456789" FilterType="Custom">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 37px; height: 12px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 12px">
                                            </td>
                                            <td style="height: 12px" align="left" colspan="2">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="86px" Font-Size="8pt"
                                                    Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 37px; height: 12px" align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="230px" ScrollBars="Vertical"
                        DefaultButton="btnFilter">
                        <table class="tblhead">
                            <tbody>
                                <tr>
                                    <td style="color: White; font-weight: bold;">
                                        <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                    </td>
                                    <td style="width: 170px; height: 22px" align="left">
                                        <asp:DropDownList ID="ddSearchType" runat="server" Width="200px">
                                            <asp:ListItem Value="SKU_HIE_NAME">SKU Category</asp:ListItem>
                                            <asp:ListItem Value="SKU_code">SKU Code</asp:ListItem>
                                            <asp:ListItem Value="SKU_NAME">SKU Name</asp:ListItem>
                                            <asp:ListItem Value="PACKSIZE">SKU Size</asp:ListItem>
                                            <asp:ListItem Value="COLOR">Unit in Case</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 224px; height: 22px" align="left">
                                        <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                    </td>
                                    <td style="width: 380px; height: 22px" align="left">
                                        <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                            OnClick="btnFilter_Click"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <asp:GridView ID="grdSKUData" runat="server" Width="99.9%" ForeColor="SteelBlue"
                            CssClass="gridRow2" HorizontalAlign="Center" BorderColor="White" BackColor="White"
                            AutoGenerateColumns="False" OnRowEditing="grdSKUData_RowEditing" OnRowDeleting="grdSKUData_RowDeleting"
                            EmptyDataText="<div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'>No Record for display.</div>">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous"></PagerSettings>
                            <Columns>
                                <asp:BoundField DataField="Category_Id" HeaderText="Category_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Category">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_Code" HeaderText="Bar Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PACKSIZE" HeaderText="Size">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="COLOR" HeaderText="Unit in Case">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_ON" HeaderText="GST">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Principal_Id" HeaderText="Principal_Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <%--  <asp:BoundField DataField="DISTRIBUTOR_PRICE" HeaderText="Pur.Price" DataFormatString="{0:f2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="TRADE_PRICE" HeaderText="Sell.Price" DataFormatString="{0:f2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="DATE_EFFECTED" HeaderText="GST" DataFormatString="{0:dd-MMM-yyyy}">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                  <asp:BoundField DataField="TAX_PRICE" HeaderText="GST%" DataFormatString="{0:f2}">
                                     <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>--%>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                            CommandName="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div style="z-index: 101; left: 40%; top: 150px; position: absolute; height: 100px;
                    width: 100px">
                    &nbsp;<asp:Panel ID="Panel21" runat="server">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="33px" Height="31px" ImageUrl="~/App_Themes/Granite/Images/image003.gif" />
                                Wait Loading....
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
