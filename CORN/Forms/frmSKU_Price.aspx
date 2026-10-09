<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKU_Price.aspx.cs" Inherits="SKU_Price" Title="CORN :: SKU Price" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
   
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtSKUName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select SKU Name');
                return false;
            }
            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select Price Efftive Date');
                return false;
            }
            str = document.getElementById('<%=txtTaxPrices.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Tax Price');
                return false;
            }
            str = document.getElementById('<%=txtTradePrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Trade Price');
                return false;
            }
            str = document.getElementById('<%=txtRetailPrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Retail Price');
                return false;
            }
            str = document.getElementById('<%=txtDistributorPrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Purchase Price');
                return false;
            }

            return true;
        }
          
          function CheckBoxListSelect() {
             
            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
          
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else
             {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {

                    chkBoxCount[i].checked = false;
                 
                }
            }
          
        }

        function ProductSelected(source, eventArgs) {
            var SKUDetail = eventArgs.get_text();
            document.getElementById("<%= hfSKUID.ClientID %>").value = eventArgs.get_value();
            document.getElementById("<%=txtCode.ClientID %>").value = SKUDetail.substring(0, SKUDetail.indexOf('-'));
            document.getElementById("<%=txtSKUName.ClientID %>").value = SKUDetail.substring(SKUDetail.indexOf('-') + 1);
            document.getElementById("<%= btnDummy.ClientID %>").click();
        }

        function SearchProduct() {
            var obj = jQuery.parseJSON($("#<%=hfSKU.ClientID %>").val());
            for (var i = 0; i < obj.length; i++) {
                var item = obj[i];
               
                if (item.SKU_CODE == document.getElementById('<%=txtCode.ClientID%>').value) {
                   
                    document.getElementById("<%= txtSKUName.ClientID %>").value = item.SKU_NAME;
                    document.getElementById("<%= hfSKUID.ClientID %>").value = item.SKU_ID;
                    document.getElementById("<%= btnDummy.ClientID %>").click();
                    break;
                }
            }
        }
    </script>
    <div id="right_data">
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan="1">
                                    <asp:HiddenField ID="hfSKU" runat="server" />
                                    <asp:HiddenField ID="hfSKUID" runat="server" />
                                </td>
                                <td colspan="1">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px; " align="left">
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Width="103px" Text="Bar Code"></asp:Label></strong>
                                </td>
                                <td style="width: 298px; " align="left">
                                    <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
                                    <asp:Button ID="btnDummy" runat="server" CssClass="HidePanel"  OnClick="btnLoadGrid_Click" />
                                </td>
                                <td width="300px"  align="left"
                                    rowspan="10">
                                    <table width="100%" style=" border-right: darkgray 1px ridge; border-top: darkgray 1px ridge;
                                    border-left: darkgray 1px ridge; border-bottom: darkgray 1px ridge" >
                                        <tbody>
                                            <tr>
                                                <td class="tblhead" width="300px">
                                                    <strong>
                                                        <asp:Label ID="Label11" runat="server" Width="300px" ForeColor="White" Font-Bold="True"
                                                            Text="Location Name"></asp:Label></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="SelectAll"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="320px">
                                                    <asp:Panel ID="Panel1" runat="server" Width="320px" Height="220px" ScrollBars="Vertical"
                                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                                        <asp:CheckBoxList ID="ChbDistributorList" runat="server" Width="300px" Font-Size="8pt">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td >
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px;" align="left">
                                    <strong>
                                        <asp:Label ID="Label6" runat="server" Width="100px" Text="SKU Name"></asp:Label></strong>
                                </td>
                                <td  align="left">
                                    <asp:TextBox ID="txtSKUName" runat="server" Width="200px"></asp:TextBox>
                                
                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                    <cc1:AutoCompleteExtender ID="aceProduct" runat="server" TargetControlID="txtSKUName"
                                        ServicePath="wsProductList.asmx" MinimumPrefixLength="1" CompletionInterval="500"
                                        CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" EnableCaching="true" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        OnClientItemSelected="ProductSelected" FirstRowSelected="true" ServiceMethod="GetProducts2">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td style="width: 201px; ">
                                </td>
                            </tr>
                           
                                    <strong>
                                        <asp:Label ID="Label8" runat="server" Width="100px" Text="Date Effected" Visible="false"></asp:Label></strong>
                               
                                    <asp:TextBox Style="text-align: justify" ID="txtFromdate" TabIndex="2" runat="server"
                                        Width="180px" ValidationGroup="MKE"  Enabled="true"
                                        MaxLength="1" Visible="false"></asp:TextBox>
                                    <asp:ImageButton ID="ImgBntFromCalc" runat="server" Enabled="true" Visible="false" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px"
                                        CausesValidation="False"></asp:ImageButton><cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server"  Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc"
                                            TargetControlID="txtFromdate">
                                        </cc1:CalendarExtender>
                                
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label10" runat="server" Width="90px" Text="Purchase Price" 
                                            CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox ID="txtDistributorPrice" runat="server" Width="193px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" Width="101px" Text="Selling Price" 
                                            CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:TextBox ID="txtTradePrice" runat="server" Width="192px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px" align="left">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Width="103px" Text="Retail Price" 
                                            CssClass="lblbox" Visible="false"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRetailPrice" runat="server" Width="192px" 
                                        CssClass="txtBox " MaxLength="8" Visible="false" Text="0"></asp:TextBox>
                                </td>
                                <td style="width: 201px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px;" align="left">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Width="102px" Text="G.S.T (%)" 
                                            CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTaxPrices" runat="server" Width="192px" 
                                        CssClass="txtBox " MaxLength="8"></asp:TextBox>
                                </td>
                                <td style="width: 201px; " align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px; height: 12px" align="left">
                                    <strong>
                                        <asp:Label ID="Label14" runat="server" Width="102px" Text="S.E.D (%)" 
                                            CssClass="lblbox" Visible="False"></asp:Label></strong>
                                </td>
                                <td style="width: 298px; height: 12px" align="left">
                                    <asp:TextBox ID="txtSADTax" runat="server" Width="192px"
                                        CssClass="txtBox " MaxLength="8" Visible="False"></asp:TextBox>
                                </td>
                                <td style="width: 201px; height: 12px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                </td>
                                <td style="width: 298px" align="left">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px" Font-Size="8pt"
                                        Text="Save" ValidationGroup="vg" CssClass="Button" />
                                    &nbsp;
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" __designer:wfdid="w392"
                                        TargetControlID="txtTaxPrices" ValidChars="0123456789." FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" __designer:wfdid="w393"
                                        TargetControlID="txtTradePrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" __designer:wfdid="w394"
                                        TargetControlID="txtRetailPrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" __designer:wfdid="w395"
                                        TargetControlID="txtDistributorPrice" ValidChars=".0123456789" FilterType="Custom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="z-index: 101; left: 530px; width: 100px; position: absolute; top: 440px;
                        height: 100px">
                        &nbsp;
                        <asp:Panel ID="Panel21" runat="server" >
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server"  AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="23px" Height="26px" 
                                        ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_pricedetails" runat="server" Width="99.9%" ForeColor="SteelBlue"
                             CssClass="gridRow2" BorderColor="White" BackColor="White"
                            AutoGenerateColumns="False" HorizontalAlign="Center">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous"></PagerSettings>
                            <Columns>
                                <asp:BoundField DataField="distributor_name" HeaderText="Location" >
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="170px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="170px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_ON" HeaderText="GST">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_PRICE" HeaderText="Purchase Price" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TRADE_PRICE" HeaderText="Selling Price" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RETAIL_PRICE" HeaderText="Retail Price" Visible="false" DataFormatString="{0:F2}">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TAX_PRICE" HeaderText="GST (%)" DataFormatString="{0:F2}">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle BorderColor="DarkGray" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="DATE_EFFECTED" HeaderText="Date Effected">
                                    <ItemStyle BackColor="Transparent" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="105px">
                                    </ItemStyle>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
