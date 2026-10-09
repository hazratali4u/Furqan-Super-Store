<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOrderPOS2.aspx.cs" Inherits="Forms_frmOrderPOS2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../AjaxLibrary/jquery-1.7.1.min.js"></script>
    <link rel="shortcut icon" href="../images/favicon.png" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/POSstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type=text], textarea
        {
            border: 1px solid #ccc;
        }
        input[type=text]:focus, textarea:focus
        {
            background-color: #CEC8EF;
            border: 1px solid #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            search();
        });
        function CalculateNetAndBalance() {

            var gross = 0; var disc = 0; var tax = 0; var totalamount = 0; var paydamount = 0; var balance = 0;

            gross = document.getElementById("<%= txtGrossAmount.ClientID %>").value;
            disc = document.getElementById("<%= numtxtTotalExtraDiscnt.ClientID %>").value;
            tax = document.getElementById("<%= numTxtTotalGST.ClientID %>").value;
            totalamount = document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value;
            paydamount = document.getElementById("<%= txtCashReceived.ClientID %>").value;
            balance = document.getElementById("<%= txtBalance.ClientID%>").value;



            if (document.getElementById("<%= btnToggleMode.ClientID %>").value == 'SALE MODE') {

                if ((disc < 0) || (disc == '')) {
                    disc = 0;
                }
                setTimeout(function () {
                    if (gross > 0) {
                        document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) - parseFloat(disc) + parseFloat(tax)).toFixed(2);
                    }
                }, 10);
                setTimeout(function () {
                    if (gross > 0) {
                        document.getElementById("<%= txtBalance.ClientID%>").value = ((parseFloat(gross) - parseFloat(disc) + parseFloat(tax)).toFixed(2));
                    }
                    if (paydamount > 0) {
                        document.getElementById("<%= txtBalance.ClientID%>").value = ((parseFloat(gross) - parseFloat(disc) + parseFloat(tax)) - parseFloat(paydamount)).toFixed(2);
                    }
                }, 10);
            }
            else {

                if (disc > 0) {
                }
                else if (disc < 0) {
                }
                else {
                    disc = 0;
                }


                if (totalamount < 0) {

                    setTimeout(function () {

                        document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) - parseFloat(disc) + parseFloat(tax)).toFixed(2);

                    }, 10);

                }
                else if (totalamount > 0) {

                    setTimeout(function () {
                        if (disc < 0) {
                            document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) + parseFloat(disc) - parseFloat(tax)).toFixed(2);
                        }
                    }, 10);

                }
                else if (totalamount < 0) {

                    setTimeout(function () {

                        document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) + parseFloat(disc) - parseFloat(tax)).toFixed(2);

                    }, 10);

                }
                else if (totalamount > 0) {
                    setTimeout(function () {

                        document.getElementById("<%= numTxtTotlAmnt.ClientID %>").value = (parseFloat(gross) - parseFloat(disc) + parseFloat(tax)).toFixed(2);

                    }, 10);
                }

                if (paydamount > 0) {

                    setTimeout(function () {

                        document.getElementById("<%= txtBalance.ClientID%>").value = ((parseFloat(gross) + parseFloat(disc) - parseFloat(tax)) + parseFloat(paydamount)).toFixed(2);
                    }, 10);
                }
                else if (paydamount > 0) {
                    if (totalamount > 0) {
                        setTimeout(function () {
                            document.getElementById("<%= txtBalance.ClientID%>").value = ((parseFloat(gross) - parseFloat(disc) + parseFloat(tax)) - parseFloat(paydamount)).toFixed(2);
                        }, 10);
                    }
                }
            }
        }

        function SearchProduct() {


            if (document.getElementById('<%=txtskuCode.ClientID%>').value == '') {

                document.getElementById('<%=numtxtTotalExtraDiscnt.ClientID%>').focus();
            }

            else {
                var chk = 0;
                var obj = jQuery.parseJSON($("#<%=hfProduct.ClientID %>").val());
                for (var i = 0; i < obj.length; i++) {
                    var item = obj[i];
                    if (parseInt(item.UNIT_WEIGHT) == 0) {
                        if (item.SKU_CODE == document.getElementById('<%=txtskuCode.ClientID%>').value) {

                            document.getElementById("<%= txtskuName.ClientID %>").value = item.SKU_NAME;
                            document.getElementById("<%= txtUnitRate.ClientID %>").value = item.TRADE_PRICE;

                            document.getElementById("<%= btnSave.ClientID %>").click();
                            document.getElementById("<%= txtskuCode.ClientID %>").focus();
                            chk = 1;
                        }
                    }
                }
                if (chk == 0) {
                    for (var j = 0; j < obj.length; j++) {
                        var item2 = obj[j];
                        if (parseInt(item2.UNIT_WEIGHT) >= 1) {//if item.UNIT_WEIGHT = "1"

                            var skuCode2 = document.getElementById('<%=txtskuCode.ClientID%>').value;

                            var skuQty = document.getElementById('<%=txtskuCode.ClientID%>').value;

                            skuCode2 = skuCode2.substring(2, 7);

                            skuQty = skuQty.substring(7, 12);

                            skuQty = skuQty.replace(/^0+/, '');

                            if (item2.SKU_CODE == skuCode2) {

                                if (item2.UNIT_WEIGHT == "1") {
                                    document.getElementById("<%= txtQuantity.ClientID %>").value = skuQty / 1000;

                                }
                                else {
                                    document.getElementById("<%= txtQuantity.ClientID %>").value = skuQty;

                                }

                                document.getElementById('<%=txtskuCode.ClientID%>').value = skuCode2;
                                document.getElementById("<%= txtskuName.ClientID %>").value = item2.SKU_NAME;
                                document.getElementById("<%= txtUnitRate.ClientID %>").value = item2.TRADE_PRICE;

                                document.getElementById("<%= btnSave.ClientID %>").click();
                                document.getElementById("<%= txtskuCode.ClientID %>").focus();
                                break;
                            }
                        }
                    }
                }
            }
        }


        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtskuName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Select SKU Name');
                return false;

            }
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            return true;
        }

       

        function SetFocusTocashRecived(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                CalculateNetAndBalance();
                setTimeout(function () { document.getElementById("<%= numtxtTotalExtraDiscnt.ClientID %>").focus(); }, 10);

            }
        }

        function SetFocusTocode(e) {

            if (e.keyCode == 13) {
                document.getElementById("<%= txtskuCode.ClientID %>").focus();
            }
            if (document.getElementById("<%= btnToggleMode.ClientID %>").value == 'SALE MODE') {
                if (document.getElementById("<%= txtQuantity.ClientID %>").value == '-') {

                    alert('- is not allowed in sale mode!');
                    document.getElementById("<%= txtQuantity.ClientID %>").value = '1';
                }

            }


        }
        function CalculateDiscount(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                CalculateNetAndBalance();
                setTimeout(function () { document.getElementById("<%= txtAuthorisedBy.ClientID %>").focus(); }, 10);
            }
        }

        function FocusToCash(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                CalculateNetAndBalance();
                setTimeout(function () { document.getElementById("<%= txtCashReceived.ClientID %>").focus(); }, 10);

            }
        }

        function CalculateBalance(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                CalculateNetAndBalance();
                setTimeout(function () { document.getElementById("<%= btnSaveOrder.ClientID %>").focus(); }, 10);
            }
        }


       
        function search(e) {

            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');

            $('#<%=txtSearch.ClientID%>').keyup(function (e) {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=grdPriceLookUp.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=grdPriceLookUp.ClientID%> tr:has(td)").show();
                    return false;
                }
                //Iterate through all the td.
                $("#<%=grdPriceLookUp.ClientID%> tr:has(td)").children().each(function () {
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

      
    </script>
    <asp:ScriptManager runat="server" ID="smPOs">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <div class="header">
            <div class="main">
                <div class="header-t">
                    <div class="logo">
                    </div>
                    <div class="menu-top">
                        <ul>
                            <li>
                                <asp:LinkButton runat="server" ID="btnNewCustomer" OnClick="btnNewCustomer_Click"> <image src="../images/icon-user.png" /><span>Customers<br /><b></b></span></asp:LinkButton>
                            </li>
                            <%--<li>
                                <asp:LinkButton runat="server" ID="btnHold" OnClick="btnHold_Click" Enabled="false" AccessKey="h" ToolTip="Alt + h"><image src="../images/icon-hold.png" /><span>Hold<br /><b></b></span></asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="btnUnhold" OnClick="btnUnhold_Click" Enabled="false" AccessKey="u" ToolTip="Alt + u"><image src="../images/icon-unhold.png" /><span>Unhold<br /><b></b></span></asp:LinkButton>
                            </li>--%>
                            <li>
                                <asp:LinkButton runat="server" ID="btnpricelookup" OnClick="btnpricelookup_Click" AccessKey="p" ToolTip="Alt + p"><image src="../images/icon-search.png" /><span>Price Lookup<b></b></span></asp:LinkButton>
                            </li>
                           
                          <%--  <li>
                                <asp:Panel runat="server" ID="ponholdpanel" Enabled="false">
                                    <a href="#"><span style="margin-top: 14px;">Receipts on hold</span><asp:TextBox ID="lblonHold"
                                        runat="server" Text="0" ReadOnly="true"></asp:TextBox></a>
                                </asp:Panel>
                            </li>--%>
                        </ul>
                        <div class="shadow-t-menu">
                        </div>
                    </div>
                </div>
            </div>
            <div id="dvPriceLookUp" runat="server" style="display:none;">
            
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
                        <td width="100%" align="left">
                            <asp:TextBox ID="txtSearch" runat="server" BorderColor="Black" onkeypress="search(event);"
                                Width="260px" Height="20px"></asp:TextBox>
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
                                <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Vertical" Width="78.7%"
                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="3px">
                                    <asp:GridView ID="grdPriceLookUp" runat="server" ForeColor="SteelBlue" SelectedRowStyle-BackColor="DarkBlue"
                                        OnRowCommand="grdPriceLookUp_RowCommand" AutoGenerateColumns="false" BackColor="White"
                                        HorizontalAlign="Center" BorderColor="White" RowStyle-Height="25px" ShowHeader="false"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="SKU Code">
                                                        <ItemStyle HorizontalAlign="Left" Width="130px"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkskuCode" runat="server" Font-Underline="false" CommandName="Add"
                                                                Text='<%#Eval("SKU_CODE") %>' CommandArgument='<%# Container.DataItem%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                           
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
                                  <%--  <asp:Repeater ID="grdPriceLookUp" runat="server">
                                 <ItemTemplate>
                                <table style="background-color: #ddd" class="table table-striped table-bordered table-hover table-condensed">
                                    <tr>
                                        <td style="display: none;">
                                            <label><%# Eval("SKU_ID")%></label>
                                        </td>
                                        <td style="width:10%">
                                            <label><%# Eval("SKU_CODE")%></label>
                                        </td>
                                         <td style="width:10%">
                                            <label><%# Eval("SKU_NAME")%></label>
                                        </td>
                                        
                                         <td style="width:10%">
                                            <label><%# Eval("TRADE_PRICE")%></label>
                                        </td>
                                         <td style="width:10%">
                                            <label><%# Eval("CLOSING_STOCK")%></label>
                                        </td>
                                       
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>--%>
                                    <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="red"></asp:Label>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            
            </div>
           </div>
            <div id="dvMain" runat="server" style="margin-top: 0; float: left;">
                <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                    height: 100px">
                    &nbsp;<asp:Panel ID="Panel21" runat="server">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                    Width="31px" />
                                Wait Update
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                </div>
                <div class="header-form">
                    <span style="width: 370px;"><strong>
                        <asp:Label runat="server" ID="l2" Font-Bold="true" Font-Size="15px">Customer</asp:Label>
                    </strong>
                        
                        <asp:DropDownList ID="drpCustomer" runat="server" TabIndex="2" Width="220px"></asp:DropDownList>
                    </span><span style="width: 485px"><strong>
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Font-Size="15px">Payment Type</asp:Label>
                    </strong>
                        <asp:RadioButtonList ID="RblPayMode" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="RblPayMode_SelectedIndexChanged" TabIndex="3">
                            <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                            <asp:ListItem Value="217">Cash & Credit Card</asp:ListItem>
                            <asp:ListItem Value="215">Credit Card</asp:ListItem>
                        </asp:RadioButtonList>
                    </span><span style="width: 450px;"><strong>
                        <asp:Label runat="server" ID="lblUnhold" Text="UnHold" Width="60px" Font-Bold="true"
                            Visible="false" Font-Size="15px"></asp:Label></strong>
                        <asp:DropDownList ID="ddonHoldList" runat="server" Width="150px" AutoPostBack="True"
                            Visible="false" CssClass="DropList" Height="30px" OnSelectedIndexChanged="ddonHoldList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddEditInvoice" runat="server" Width="150px" AutoPostBack="True"
                            Visible="false" CssClass="DropList" Height="30px" OnSelectedIndexChanged="ddEditInvoice_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="btnToggleMode" runat="server" Text="SALE MODE" CssClass="BtnModesale"
                            OnClick="btnToggleMode_Click" TabIndex="4" />
                    </span>
                </div>
                <div class="menu2">
                    <div class="main">
                        <ul>
                            <li class="sku-c">SKU Code</li>
                            <li class="sperator"></li>
                            <li class="sku-name">SKU Name</li>
                            <li class="sperator"></li>
                            <li class="qty">Quantity</li>
                            <li class="sperator"></li>
                            <li class="u-prize">Unit Price</li>
                            <li class="sperator"></li>
                            <li class="u-prize">Amount</li>
                            <li class="sperator"></li>
                            <li class=""><span style="font-size: medium; color: white; margin-left: 30px;">
                                <asp:Label Width="200px" Height="20px" runat="server" ID="lblLoacation"></asp:Label></span></li>
                        </ul>
                    </div>
                </div>
                <div class="main">
                    <div class="r-pannel">
                        <span>
                            <asp:TextBox ID="txtskuCode" runat="server" TabIndex="5" CssClass="sku-c-input" onkeypress="SetFocusTocashRecived(event)"></asp:TextBox>
                            <asp:HiddenField ID="hfProduct" runat="server" Value="0" />
                        </span><span>
                            <asp:TextBox ID="txtskuName" runat="server" CssClass="sku-name-input" Enabled="False"
                                Font-Bold="True"></asp:TextBox>
                        </span><span>
                            <asp:TextBox ID="txtQuantity" runat="server" TabIndex="6" onkeypress="SetFocusTocode(event)"
                                class="qty-input"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQuantity"
                                ValidChars=".0123456789-" FilterType="Custom">
                            </cc1:FilteredTextBoxExtender>
                        </span><span>
                            <asp:TextBox ID="txtUnitRate" runat="server" CssClass="u-prize-input" Enabled="False"></asp:TextBox>
                        </span><span>
                            <asp:TextBox ID="txtamount" runat="server" CssClass="u-prize-input" Enabled="False"></asp:TextBox>
                        </span><span class="HidePanel">
                            <asp:LinkButton runat="server" ID="btnSave" ToolTip="Add Sku" OnClick="btnSave_Click"></asp:LinkButton>
                        </span>
                        <div class="clr">
                        </div>
                        <div class="grid">
                            <asp:Panel ID="Panel2" runat="server" Height="333px" ScrollBars="Vertical" Width="99.3%"
                                BorderColor="Silver" BorderStyle="Groove" BorderWidth="3px">
                                <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" RowStyle-Height="30px"
                                    BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White"
                                    ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" Width="100%" EnableModelValidation="True">
                                    <Columns>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <FooterStyle VerticalAlign="Middle" />
                                            <HeaderStyle CssClass="HidePanel" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                            <FooterStyle VerticalAlign="Middle" />
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="Silver" BorderWidth="2px"
                                                BorderStyle="Solid" Font-Bold="true" Font-Size="16px" Width="110px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                            <FooterStyle VerticalAlign="Middle" />
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="308px"></ItemStyle>
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="QUANTITY_UNIT">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="110px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" Width="70px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NET_AMOUNT" HeaderText="NET_AMOUNT">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" BorderColor="Red" Text="X" 
                                                    CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle BorderColor="Silver" HorizontalAlign="Center" BorderWidth="2px" BorderStyle="Solid"
                                                Width="25px" Font-Bold="true" ForeColor="Red" Font-Overline="true" Font-Size="14px">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="30px" />
                                </asp:GridView>
                                <asp:UpdatePanel ID="up_prntinvoice" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="PnlPrintInvoice" runat="server" Height="325px" ScrollBars="Vertical"
                                            Width="99.2%" BorderColor="Silver" BorderStyle="Groove" BorderWidth="3px" Visible="false">
                                            <asp:GridView ID="GrdPrintInvoice" runat="server" ForeColor="SteelBlue" RowStyle-Height="30px"
                                                BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White"
                                                Width="100%" EnableModelValidation="True" OnRowDeleting="GrdPrintInvoice_RowDeleting"
                                                OnRowDataBound="GrdPrintInvoice_RowDataBound">
                                                <HeaderStyle HorizontalAlign="Left" BorderStyle="Solid" BackColor="Beige" Height="30px"
                                                    VerticalAlign="Middle" Font-Bold="true" Font-Size="14px" />
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous"></PagerSettings>
                                                <Columns>
                                                    <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="Invoice Id">
                                                        <FooterStyle VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="Silver" BorderWidth="2px"
                                                            BorderStyle="Solid" Font-Bold="true" Font-Size="12px" Width="60px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CUSTOMER" HeaderText="Customer">
                                                        <FooterStyle VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="Silver" BorderWidth="2px"
                                                            BorderStyle="Solid" Font-Bold="true" Font-Size="12px" Width="100px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Gross Sale" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" Font-Bold="true" Font-Size="12px" Width="100px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EXTRA_DISCOUNT_AMOUNT" HeaderText="Discount" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" Font-Bold="true" Font-Size="12px" Width="65px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GST_AMOUNT" HeaderText="Sales Tax" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" Font-Bold="true" Font-Size="12px" Width="65px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STANDARD_DISCOUNT_AMOUNT" HeaderText="Cash Received" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" Font-Bold="true" Font-Size="12px" Width="100px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TOTAL_NET_AMOUNT" HeaderText="Net Amount" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" Font-Bold="true" Font-Size="12px" Width="100px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TYPE" HeaderText="Type" DataFormatString="{0:F2}">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" Font-Bold="true" ForeColor="brown" Font-Size="12px" Width="80px">
                                                        </ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" Font-Size="12px" Width="20px"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn_print" runat="server" Text="Print" ForeColor="Blue" CommandName="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle Height="25px" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="l-pannel">
                        <ul>
                            <li><span></span>
                                <asp:Image runat="server" ID="imgSKU" ImageUrl="~/images/Store.jpg" AlternateText="No Image Found" />
                                <span></span></li>
                            <li>
                                <label>
                                    Gross Sale</label>
                                <asp:TextBox ID="txtGrossAmount" runat="server" ReadOnly="True"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label>
                                    Discount &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp Authorised By</label>
                                <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="110" onkeypress="CalculateDiscount(event)"
                                    onblur="CalculateNetAndBalance();" TabIndex="7" onkeyup="CalculateNetAndBalance();"
                                    ValidationGroup="NumbersOnly"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom"
                                    ValidChars="-0123456789." TargetControlID="numtxtTotalExtraDiscnt" />
                                <asp:TextBox ID="txtAuthorisedBy" TabIndex="8" runat="server" Width="110" onkeypress="FocusToCash(event)"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label>
                                    Sales Tax</label>
                                <asp:TextBox ID="numTxtTotalGST" runat="server" ReadOnly="True"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label>
                                    Net Amount</label>
                                <asp:TextBox ID="numTxtTotlAmnt" runat="server" ReadOnly="True"></asp:TextBox>
                            </li>
                            <li><span></span>
                                <label>
                                    Cash Received</label>
                                <asp:TextBox ID="txtCashReceived" runat="server" TabIndex="9" onkeypress="CalculateBalance(event)"
                                    onblur="CalculateNetAndBalance();" onkeyup="CalculateNetAndBalance();"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom"
                                    ValidChars="0123456789." TargetControlID="txtCashReceived" />
                            </li>
                            <li><span></span>
                                <label>
                                    Balance</label>
                                <asp:TextBox ID="txtBalance" runat="server" ReadOnly="True"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <div class="footer" id="dvFooter" runat="server">
        <div class="main">
            <div class="address">
                <h2>
                    <asp:Label runat="server" ID="lblSalesReport" Text="Sales Report"></asp:Label>
                </h2>
                <p>
                    <asp:CheckBox ID="chkMalee" runat="server" TabIndex="13" />Bakery
                </p>
                <p>
                    <b>From Date:&nbsp;&nbsp; </b><span>
                        <asp:TextBox ID="txtstartDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="150px"
                            CssClass="txtBox " MaxLength="10"></asp:TextBox>
                        <asp:ImageButton ID="ibnstartDate" runat="server" Width="16px" TabIndex="14" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                        </asp:ImageButton>
                    </span>
                </p>
                <p>
                    <b>To Date: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><span>
                        <asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="150px"
                            CssClass="txtBox " MaxLength="10"></asp:TextBox>
                        <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" TabIndex="15" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                        </asp:ImageButton>
                    </span>
                </p>
                <p>
                    <span style="margin-top: 2px;">&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnViewSalesReport" CssClass="view" ToolTip="View"
                            OnClick="btnViewSalesReport_Click" TabIndex="16" />
                    </span><span>
                        <asp:LinkButton runat="server" ID="btnRePrint" OnClick="btnRePrint_Click" CssClass="bttn"
                            TabIndex="17"><span>RePrint</span></asp:LinkButton>
                    </span><span>
                        <asp:LinkButton runat="server" ID="btn_exitprint" Visible="false" OnClick="btn_exitprint_Click"
                            CssClass="bttn"><span>Exit</span></asp:LinkButton></span>
                    <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtstartDate"
                        PopupButtonID="ibnstartDate" Format="dd-MMM-yyyy">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                        PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                    </cc1:CalendarExtender>
                </p>
            </div>
            <asp:UpdatePanel ID="upFooter" runat="server">
                <ContentTemplate>
                    <div class="shadow-footer">
                    </div>
                    <div class="user-login">
                        <img src="../images/user-login.png" alt="" />
                        <span width="200px">
                            <h2>
                                User Login</h2>
                            <h3>
                                <asp:Label runat="server" ID="lbluserlogin" Text="Administrator"></asp:Label></h3>
                            <br />
                            <br />
                            <p>
                            </p>
                            <p>
                                <b>Working Date:</b>
                                <h3>
                                    <asp:Label runat="server" ID="lblCurrentWorkingDate" Text="Temp"></asp:Label>
                                </h3>
                                <br />
                                <br />
                                <p>
                                </p>
                                <p>
                                    <b>Loged in at</b><br />
                                    <asp:Label ID="lbllogintimedate" runat="server" Text="time"></asp:Label>
                                </p>
                                <p>
                                </p>
                            </p>
                        </span>
                    </div>
                    <div class="shadow-footer">
                    </div>
                    <div class="free-sku">
                        <h2>
                            UPSell Item</h2>
                        <asp:Label ID="lbl_licencesmsg" runat="server" Width="260px" />
                        <asp:Label ID="lbl_printno" runat="server" Text="No of Print" ForeColor="DarkRed"
                            Font-Bold="true" />
                        <asp:TextBox ID="txt_chkprint" runat="server" Width="25px" Height="15px" Text="1"
                            Font-Bold="true" CausesValidation="false" /><asp:CheckBox ID="chkPrint" runat="server"
                                Checked="True" />
                        <cc1:FilteredTextBoxExtender TargetControlID="txt_chkprint" FilterMode="ValidChars"
                            ID="FtbeChkPrint" FilterType="Numbers" runat="server" />
                        <div class="grid2">
                            <asp:Panel ID="Panel4" runat="server" Width="100%" Height="100%">
                                <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                    BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous"></PagerSettings>
                                    <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black">
                                    </RowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                            <ItemStyle Width="80px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                            </ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                            <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                            </ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                            <ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                            </ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        <p>
                            <asp:LinkButton runat="server" ID="btnSaveOrder" TabIndex="10" OnClick="btnSaveOrder_Click"
                                ToolTip="Save" AccessKey="s" CssClass="save-n-print"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCalculate" TabIndex="11" OnClick="btnVoid_Click"
                                AccessKey="v" CssClass="void">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCancel" TabIndex="12" OnClick="btnCancel_Click1"
                                CssClass="exit" AccessKey="x"></asp:LinkButton>
                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
    </form>
</body>
</html>
