<%@ Page Title="CORN :: Price LookUp" Language="C#" MasterPageFile="~/Forms/masterPOS.master"
    AutoEventWireup="true" CodeFile="frmProductSearch.aspx.cs" Inherits="Forms_frmProductSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../AjaxLibrary/jquery-1.7.1.min.js" type="text/javascript"></script>
       <script type="text/javascript" language="javascript">
    
        $(document).ready(function () {
            search();
        });
        function search(e) {

            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');

            $('#<%=txtSearch.ClientID%>').keyup(function (e) {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=GrdPurchase.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=GrdPurchase.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=GrdPurchase.ClientID%> tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
                if (iCounter == 0) {
                    $('#<%=lblNoRecords.ClientID%>').css('display', '');
                }
                e.preventDefault();
            });

        }

        function SelectRow(row) {
            var url = "frmOrderPOS.aspx?skuid=" + row + "";
            document.location.href = url;

        }
    </script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-left: 160px">
        <asp:ScriptManager runat="server" ID="smPOs">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatpanel12" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="left" width="200px">
                            <asp:Button ID="btnrefresh" runat="server" Text="Refresh" CssClass="ButtonPOS" OnClick="btnrefresh_Click"
                                UseSubmitBehavior="false" Width="90px" Height="30px" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" UseSubmitBehavior="false"
                                CssClass="ButtonPOS" Width="90px" Height="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td width="210px">
                            <strong>
                                <asp:Label ID="Label21" runat="server" Width="210px" Text="Enter SKU code Or Name To Search:"></asp:Label></strong>
                        </td>
                        <td width="50%" align="left">
                            <asp:TextBox ID="txtSearch" runat="server" BorderColor="Black" 
                                Width="260px" Height="20px" onkeyup="search()"></asp:TextBox>
                                 <asp:Button ID="btnSearch" Width="70px" Text="Search" runat="server" 
                                onclick="btnSearch_Click" CssClass="Button"/>
                        </td>
                        <td>
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="menu2" style="width: 79%">
                                <div class="main">
                                    <ul>
                                        <li class="sku-c2">SKU Code</li>
                                        <li class="sperator"></li>
                                        <li class="sku-name2">SKU Name</li>
                                        <li class="sperator"></li>
                                        <li class="u-prize2">Unit Price</li>
                                        <li class="sperator"></li>
                                        <li class="u-prize">Closing Stock</li>
                                        <li class="sperator"></li>
                                    </ul>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div>
                                <asp:Panel ID="Panel2" runat="server" Height="100%" ScrollBars="Vertical" Width="78.7%"
                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="3px">
                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" SelectedRowStyle-BackColor="DarkBlue"
                                        OnRowDataBound="selectValuandGOtoPos" AutoGenerateColumns="false" BackColor="White"
                                        HorizontalAlign="Center" BorderColor="White" RowStyle-Height="25px" ShowHeader="false"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                    Width="84px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                    Width="250px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TRADE_PRICE" HeaderText="Unit PRICE" DataFormatString="{0:F2}">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                    Width="80px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CLOSING_STOCK" HeaderText="Closing Stock" DataFormatString="{0:F2}">
                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                    Width="80px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblNoRecords" Text="No records to display"  runat="server" ForeColor="red"></asp:Label>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
