<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmBarcode.aspx.cs" Inherits="Forms_frmBarcode" Title="CORN :: SKU Barcode" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <%--  <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
    <script language="javascript" type="text/javascript">
        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txt_row.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Insert no of Rows..');
                return false;
            }
            str = document.getElementById('<%=txt_col.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Insert no of colmns..');
                return false;
            }
            var str;

            str = document.getElementById('<%=txt_productName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Select Product');
                return false;
            }
            str = document.getElementById('<%=txt_code.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Insert Code');
                return false;
            }
            str = document.getElementById('<%=txt_price.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Insert Code');
                return false;
            }
            return true;
        }

        function ValidateForm2() {
            var str;

            str = document.getElementById('<%=txt_productName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Select Product');
                return false;
            }
            str = document.getElementById('<%=txt_code.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Insert Code');
                return false;
            }
            str = document.getElementById('<%=txt_price.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Insert Price');
                return false;
            }
            str = document.getElementById('<%=txt_row.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Plz Insert no of Rows..');
                return false;
            }
            else if (str.length > 10) {
                alert('Range is not valid..');
            }

            return true;
        }

        function ProductSelected(source, eventArgs) {
            var SKUDetail = eventArgs.get_text();
            var num = eventArgs.get_value();
            num = Math.round(num);

            document.getElementById("<%= txt_price.ClientID %>").value = num;

            document.getElementById("<%=txt_code.ClientID %>").value = SKUDetail.substring(0, SKUDetail.indexOf('-'));
            document.getElementById("<%=txt_productName.ClientID %>").value = SKUDetail.substring(SKUDetail.indexOf('-') + 1);
            document.getElementById("<%= btnDummy.ClientID %>").click();
        }

        function SearchProduct() {
            var obj = jQuery.parseJSON($("#<%=hfSKU.ClientID %>").val());
            for (var i = 0; i < obj.length; i++) {
                var item = obj[i];
                if (item.SKU_CODE == document.getElementById('<%=txt_code.ClientID%>').value) {
                    document.getElementById("<%= txt_productName.ClientID %>").value = item.SKU_NAME;

                    document.getElementById("<%= btnDummy.ClientID %>").click();
                    break;
                }
            }
        }

    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="up_pnl1" runat="server">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
                                <asp:HiddenField ID="hfSKU" runat="server" />
                                <asp:HiddenField ID="hfSKUID" runat="server" />
                                <asp:Button ID="btnDummy" runat="server" CssClass="HidePanel" OnClick="btnLoadGrid_Click" />
                            </td>
                            <td>
                                <asp:TextBox ID="txt_productName" runat="server" Width="200px"></asp:TextBox>
                                <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                <cc1:AutoCompleteExtender ID="aceProduct" runat="server" TargetControlID="txt_productName"
                                    ServicePath="wsProductList.asmx" MinimumPrefixLength="1" CompletionInterval="500"
                                    CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" EnableCaching="true" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="ProductSelected" FirstRowSelected="true" ServiceMethod="GetProducts2">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td style="width:100px;"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_code" runat="server" Text="Product Code"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_code" runat="server" Width="200px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Price"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_price" runat="server"  Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                              <asp:Label ID="lbl_rc" Text="No Of Row's" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_row" runat="server" Width="60" MaxLength="2" Text="10" Visible="true"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbe_txtrow" runat="server" ValidChars="0123456789"
                                    TargetControlID="txt_row">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RangeValidator ID="RangeValidator1" runat="server"  ControlToValidate="txt_row"
                                MaximumValue="10" MinimumValue="1" SetFocusOnError="true" ValidationGroup="vg"></asp:RangeValidator>
                                <asp:TextBox ID="txt_col" runat="server" Width="60" MaxLength="1" Text="3"  ReadOnly="true" Visible="false"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbe_txtcol" runat="server" ValidChars="012345"
                                    TargetControlID="txt_col">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                    <hr />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_companyname" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_pcode" runat="server"></asp:Label>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_pprice" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="img_brcode" runat="server" />
                                                           
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>     
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
         <div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_print" runat="server" Text="Print" OnClick="btn_print_Click"
                                                Width="80" CssClass="Button" Visible="false" />
                                            <asp:Button ID="btn_generate" runat="server" Text="Generate" OnClick="btn_generate_Click"
                                                Width="80" CssClass="Button" CausesValidation="true" ValidationGroup="vg"/>
                                        </td>
                                    </tr>
                                </table>
                                </div>
    </div>
</asp:Content>
