<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUHierarchyData.aspx.cs" Inherits="frmSKUHierarchyData" Title="CORN :: SKU Hierarchy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <cc1:TabContainer ID="TabContainer1" runat="server" Height="400px" Width="650px"
                        ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabPanel1" runat="server">
                            <HeaderTemplate>
                                Principal
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 120px">
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 120px; height: 29px">
                                                                    <strong>
                                                                        <asp:Label ID="Label2" runat="server" Width="120px" Text="Principal Name"></asp:Label></strong>
                                                                </td>
                                                                <td colspan="3" style="width: 500px; height: 29px">
                                                                    <asp:TextBox ID="txtPrincipalName" runat="server" Width="465px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 120px; height: 29px">
                                                                    <strong>
                                                                        <asp:Label ID="Label11" runat="server" Width="120px" Text="Address"></asp:Label></strong>
                                                                </td>
                                                                <td colspan="3" style="width: 500px; height: 29px">
                                                                    <asp:TextBox ID="txtAddress" runat="server" Width="465px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 120px; height: 29px">
                                                                    <strong>
                                                                        <asp:Label ID="Label13" runat="server" Width="120px" Text="Contact Person"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 49px; height: 29px">
                                                                    <asp:TextBox ID="txtNTN" runat="server" Width="150px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td align="left" style="width: 100px; height: 29px;">
                                                                    <strong>
                                                                        <asp:Label ID="Label14" runat="server" Width="85px" Text="Email Address"></asp:Label></strong>
                                                                </td>
                                                                <td align="left" style="width: 70px; height: 29px;">
                                                                    <asp:TextBox ID="txtSTRN" runat="server" Width="150px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 120px; height: 29px">
                                                                    <strong>
                                                                        <asp:Label ID="Label1" runat="server" Width="120px" Text="Phone #"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 49px; height: 29px">
                                                                    <asp:TextBox ID="txtPhoneNumber" runat="server" Width="150px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td align="left" style="width: 100px; height: 29px;">
                                                                    <strong>
                                                                        <asp:Label ID="Label3" runat="server" Width="85px" Text="Fax Number"></asp:Label></strong>
                                                                </td>
                                                                <td align="left" style="width: 70px; height: 29px;">
                                                                    <asp:TextBox ID="txtFaxNumber" runat="server" Width="150px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="center" style="width: 100px; height: 37px">
                                                                    <asp:CheckBox ID="ChIsMunalDiscount" runat="server" Width="190px" Visible="false" Text="Is Manual Discount" Checked="true"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px">
                                                                </td>
                                                                <td style="width: 49px" align="right">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 100px">
                                                                    <asp:Button ID="btnSavePrincipal" OnClick="btnSavePrincipal_Click" runat="server"
                                                                        Width="85px" Font-Size="8pt" Text="New" CssClass="Button" />
                                                                </td>
                                                                <td style="width: 100px">
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div style="z-index: 101; left: 80px; width: 100px; position: absolute; top: 250px;
                                                height: 90px">
                                                &nbsp;<asp:Panel ID="Panel21" runat="server">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
                                                        <ProgressTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                Width="31px" />
                                                            Wait Update
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Height="160px" ScrollBars="Vertical" BorderColor="Silver"
                                                Width="100%" BorderWidth="1px">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdPrincipal" runat="server" Width="100%" ForeColor="SteelBlue"
                                                            Height="150px" Font-Size="9pt" CssClass="gridRow2" BorderColor="White" BackColor="White"
                                                            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GrdPrincipal_RowEditing"
                                                            OnRowDeleting="GrdPrincipal_RowDeleting" OnPageIndexChanging="GrdPrincipal_PageIndexChanging">
                                                            <PagerSettings PreviousPageText="Previous" Mode="NextPrevious" LastPageText="" FirstPageText=""
                                                                NextPageText="Next"></PagerSettings>
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" Visible="False" HeaderText="ID">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code" Visible="false">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </ItemStyle>
                                                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IS_MANUALDISCOUNT" HeaderText="Manual Discount" Visible="false">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ADDRESS" Visible="False" HeaderText="ADDRESS"></asp:BoundField>
                                                                <asp:BoundField DataField="NTN" HeaderText="Contact Person">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="STRN" Visible="false" HeaderText="Contact Person"></asp:BoundField>
                                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                    <ItemStyle BorderColor="Silver" Width="50px" BorderWidth="1px" BorderStyle="Solid">
                                                                    </ItemStyle>
                                                                </asp:CommandField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemStyle BorderColor="Silver" Width="50px" BorderWidth="1px" BorderStyle="Solid">
                                                                    </ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            Text="Delete"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                            <AlternatingRowStyle CssClass="GridAlternateRowStyle"></AlternatingRowStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel3" runat="server">
                            <HeaderTemplate>
                                Category
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100px">
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 100px; height: 16px">
                                                                </td>
                                                                <td style="height: 16px" align="left" colspan="2">
                                                                    <br />
                                                                    <asp:Label ID="lblErrorMsgCategory" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; height: 16px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 8px">
                                                                </td>
                                                                <td style="width: 69px; height: 8px">
                                                                </td>
                                                                <td style="width: 100px; height: 8px">
                                                                </td>
                                                                <td style="width: 100px; height: 8px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 28px">
                                                                </td>
                                                                <td style="width: 69px; height: 28px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label121" runat="server" Width="90px" Text="Category Code"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 100px; height: 28px">
                                                                    <asp:TextBox ID="txtCategoryCode" runat="server" Width="194px" Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td style="width: 100px; height: 28px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 29px">
                                                                </td>
                                                                <td style="width: 69px; height: 29px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label211" runat="server" Width="90px" Text="Category Name" __designer:wfdid="w121"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 100px; height: 29px">
                                                                    <asp:TextBox ID="txtCategoryName" runat="server" Width="194px" __designer:wfdid="w122"
                                                                        Enabled="False" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td style="width: 100px; height: 29px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 37px">
                                                                </td>
                                                                <td style="width: 69px; height: 37px" align="right">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 100px; height: 37px" align="left">
                                                                    <asp:Button ID="btnSaveCategory" OnClick="btnSaveCategory_Click" runat="server" Width="85px"
                                                                        Font-Size="8pt" Text="New" CssClass="Button" />
                                                                </td>
                                                                <td style="width: 100px; height: 37px">
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div style="z-index: 101; left: 80px; width: 100px; position: absolute; top: 150px;
                                                height: 90px">
                                                &nbsp;<asp:Panel ID="Panel2" runat="server">
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel21">
                                                        <ProgressTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                Width="31px" />
                                                            Wait Update
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" Height="245px" ScrollBars="Vertical" BorderColor="Silver"
                                                Width="100%" BorderWidth="1px">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GrdCategory" runat="server" Width="100%" ForeColor="SteelBlue"
                                                            Font-Size="9pt" CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                            AutoGenerateColumns="False" OnRowEditing="GrdCategory_RowEditing" OnRowDeleting="GrdCategory_RowDeleting"
                                                            OnPageIndexChanging="GrdCategory_PageIndexChanging">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_HIE_ID" HeaderText="Id" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_CODE" HeaderText="Code">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                    </ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Name">
                                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                        Width="400px"></ItemStyle>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </ItemStyle>
                                                                </asp:CommandField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver" HorizontalAlign="Left">
                                                                    </ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            Text="Delete"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
