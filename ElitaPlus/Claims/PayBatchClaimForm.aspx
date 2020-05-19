<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    CodeBehind="PayBatchClaimForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PayBatchClaimForm"
    Theme="Default" %>

<%@ Register TagPrefix="Elita" TagName="UserControlInvoiceRegionTaxes" Src="../Common/UserControlInvoiceRegionTaxes.ascx" %>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server"> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
    </style>
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet" />
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="searchGrid">
        <tr>
            <td style="height: 12px" align="left">
                <asp:Label ID="LabelServiceCenter" runat="server">SERVICE_CENTER</asp:Label>:
            </td>
            <td nowrap align="left" id="tdlblbatch" runat="server">
                <asp:Label ID="LabelBatchNumber" runat="server">BATCH #</asp:Label>:
            </td>
            <td nowrap align="left" id="td1" runat="server">
                <asp:Label ID="lblInvTyp" runat="server">INVOICE_TYPE:</asp:Label>
            </td>
            <td nowrap align="left">
                <asp:Label ID="LabelRepairDate" runat="server">REPAIR_DATE:</asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap align="left">
                <asp:TextBox ID="TextBoxServiceCenter" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>&nbsp;&nbsp;
            </td>
            <td nowrap align="left" id="tdtxtbatch" runat="server">
                <asp:TextBox ID="TextBoxBatchNumber" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
            <td nowrap align="left" id="td2" runat="server">
                <asp:TextBox ID="txtInvTyp" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
            <td nowrap align="left">
                <asp:TextBox ID="TextBoxRepairDate" runat="server" SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonRepirDate" runat="server" CausesValidation="False"
                    Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg" Enabled="false"
                    ImageAlign="AbsMiddle"></asp:ImageButton>&nbsp;
                <asp:Button ID="btnAddRepairDate_WRITE" runat="server" SkinID="AlternateLeftButton"
                    Text="ADD_REPAIR_DATE" Enabled="false"></asp:Button>
            </td>
        </tr>
        <tr>
            <td nowrap align="left" width="1%">
                <asp:Label ID="LabelInvoiceNumber" runat="server">INVOICE_NUMBER</asp:Label>:
            </td>
            <td nowrap align="left" colspan="1">
                <asp:Label ID="LabelInvoiceAmount" runat="server">INVOICE_AMT</asp:Label>:
            </td>
            <td nowrap align="left" colspan="1" id="tdlblstate" runat="server">
                <asp:Label ID="LabelState" runat="server">PERCEPTION_IIBB_PROVINCE</asp:Label>:
            </td>
            <td nowrap align="left" id="tdlbltax1" runat="server">
                <asp:Label ID="LabelPerceptionIVA" runat="server">PERCEPTION_IVA</asp:Label>:
            </td>
        </tr>
        <tr>
            <td style="height: 17px" nowrap align="left" width="275">
                <asp:TextBox ID="TextBoxInvoiceNumber" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
            <td nowrap align="left" colspan="1">
                <asp:TextBox ID="TextBoxInvoiceAmount" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
            <td nowrap align="left" colspan="1" id="tddpstate" runat="server">
                <asp:DropDownList ID="DropDownState" runat="server" AutoPostBack="False" SkinID="SmallDropDown"
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td style="height: 12px" nowrap align="left" id="tdtxttax1" runat="server">
                <asp:TextBox ID="TextBoxPerceptionIVA" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap align="left">
                <asp:Label ID="LabelCurrentAmount" runat="server">CURRENT_AMT</asp:Label>:
            </td>
            <td nowrap align="left">
                <asp:Label ID="LabelInvoiceDate" runat="server">INVOICE_DATE</asp:Label>:
            </td>
            <td nowrap align="left">
                <asp:Label ID="LabelDifference" runat="server">DIFFERENCE</asp:Label>:
            </td>
            <td nowrap align="left" id="tdlbltax2" runat="server">
                <asp:Label ID="LabeLPerceptionIIBB" runat="server">PERCEPTION_IIBB</asp:Label>:
            </td>
        </tr>
        <tr>
            <td style="height: 12px" nowrap align="left">
                <asp:TextBox ID="TextBoxCurrentAmount" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="height: 12px" nowrap align="left">
                <asp:TextBox ID="TextBoxInvoiceDate" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="height: 12px" nowrap align="left">
                <asp:TextBox ID="TextBoxDifference" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
            <td style="height: 12px" nowrap align="left" id="tdtxttax2" runat="server">
                <asp:TextBox ID="TextBoxPerceptionIIBB" runat="server" AutoPostBack="False" SkinID="SmallTextBox"
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="inpCurrentAmt" type="hidden" name="inpCurrentAmt" runat="server" />
    <input id="inpDifference" type="hidden" name="impDifference" runat="server" />
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for Pay Invoice</h2>
        <div style="width: 100%">
        <div class="dataContainer" id="InvoiceTabs" Visible="False" runat="server" style="width: 100%;">
            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
            <div id="tabs" class="style-tabs">
                <ul>
                    <li>
                        <a href="#tabsSearchInvoice">
                            <asp:Label ID="lblsearchInvoice" runat="server" CssClass="tabHeaderText">Claim Details</asp:Label>
                        </a>
                    </li>
                    <li>
                        <a href="#tabsIIBBtaxes">
                            <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">REGION_TAXES</asp:Label>
                        </a>
                    </li>
                </ul>
                <div id="tabsSearchInvoice">
                    <div>
                        <%--<asp:ToolkitScriptManager ID="ScriptManager1" runat="server" CombineScripts="false">
                        </asp:ToolkitScriptManager>--%>
                        <table width="100%" class="dataGrid">
                            <tr id="trPageSize" runat="server">
                                <td valign="top" align="left">
                                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 100%">
                        <asp:GridView ID="GridClaims" runat="server" Width="100%" AutoGenerateColumns="False"
                            CellPadding="1" AllowPaging="True" SkinID="DetailPageGridView">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <Columns>
                                <asp:BoundField Visible="False" HeaderText="invoice_trans_id"></asp:BoundField>
                                <asp:BoundField Visible="False" HeaderText="invoice_trans_detail_id"></asp:BoundField>
                                <asp:BoundField Visible="False" HeaderText="claim_id"></asp:BoundField>
                                <asp:BoundField SortExpression="claim_number" HeaderText="Claim_number">
                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField SortExpression="Authorization_Number" HeaderText="Authorization_Number">
                                    <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField SortExpression="Customer_name" HeaderText="Customer_name">
                                    <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField Visible="False" HeaderText="claim_modified_date"></asp:BoundField>
                                <asp:BoundField HeaderText="Reserve_amount" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="TOTAL_BONUS" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Salvage_Amount">
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" VerticalAlign="bottom"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="textSalvageAmount" runat="server" Width="65px" Height="18px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Deductible" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Amount_To_Be_Paid">
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" VerticalAlign="bottom"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="textAmountToBePaid" runat="server" Width="65px" Height="18px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Repair Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" VerticalAlign="bottom"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="textRepairDate" runat="server" Width="80px" Height="18px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButtonRepairDate" runat="server" CausesValidation="False"
                                            Visible="True" ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pickup_Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" VerticalAlign="bottom"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="textPickupDate" runat="server" Width="80px" Height="18px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButtonPickupDate" runat="server" CausesValidation="False"
                                            Visible="True" ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Close" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCloseClaim" runat="server" Checked="True"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spare_Parts" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSpareParts" runat="server" Checked="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField Visible="False"></asp:BoundField>
                            </Columns>
                            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                            <PagerStyle />
                        </asp:GridView>
                    </div>
                </div>
                <div id="tabsIIBBtaxes">
                    <Elita:UserControlInvoiceRegionTaxes runat="server" ID="IIBBTaxes" RequestIIBBTaxesData="IIBBTaxes_RequestIIBBTaxes">
                    </Elita:UserControlInvoiceRegionTaxes>
                </div>
            </div>
        </div>
            </div>
        <div>
            <table width="100%" class="searchGrid">
                <tr id="tr1" runat="server">
                    <td valign="top" width="10%" align="right">
                        <asp:Label ID="lblRejectReason" Visible="False" runat="server">PAYBATCHREJECTREASON:</asp:Label>
                    </td>
                    <td valign="top" width="50%" align="left">
                        <asp:TextBox ID="txtareaRejectReason" runat="server" AutoPostBack="False" TextMode="MultiLine" Visible="False">
                        </asp:TextBox>

                    </td>
                    <td valign="top" width="40%" align="left">&nbsp;&nbsp;
                    </td>
                </tr>
                <tr id="tr2" runat="server">
                    <td valign="top" width="10%" align="right">&nbsp;&nbsp;
                    </td>
                    <td valign="top" width="50%" align="left">
                        <asp:Button ID="btnRejectSave" runat="server" SkinID="AlternateLeftButton" Text="Save" Visible="False"></asp:Button>
                        <asp:Button ID="btnRejectCancel" runat="server" SkinID="AlternateLeftButton" Text="Cancel" Visible="False"></asp:Button>
                    </td>
                    <td valign="top" width="40%" align="left">&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnCancel" runat="server" SkinID="AlternateLeftButton"
            Text="Cancel"></asp:Button>
        <asp:Button ID="btnSave_WRITE" runat="server" SkinID="AlternateLeftButton"
            Text="SAVE"></asp:Button>
        <asp:Button ID="btnPay_WRITE" runat="server" SkinID="AlternateLeftButton"
            Text="Pay"></asp:Button>
        <asp:Button ID="btnReject_WRITE" runat="server" SkinID="AlternateLeftButton"
            Text="Reject"></asp:Button>

    </div>

    <script language="javascript" type="text/javascript">

        // debugger;
        var curAmt;
        var curSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
        var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
        function chkamt(fld, t, type) {
            var ctrl = document.getElementById(fld);
            var f = parseFloat(setJsFormat(ctrl.value, curSep));
            var t = t / 100;
            if (f > t) {
                document.getElementById(fld).focus();
                document.getElementById(fld).innerText = convertNumberToCulture(t, curSep, groupSep);
                updTot(fld, t);
                alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                    return false;
                }
                if (document.getElementById(fld) != document.getElementById(Tax1Amt) && document.getElementById(fld) != document.getElementById(Tax2Amt)) {

                    if (isNaN(f) || document.getElementById(fld).value == null || document.getElementById(fld).value == '') {
                        document.getElementById(fld).focus();
                        document.getElementById(fld).innerText = convertNumberToCulture(curAmt, curSep, groupSep);
                        alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                        return false;
                    }
                    if (parseFloat(document.getElementById(fld).value) < 0) {
                        //document.getElementById(fld).value = 0;
                        document.getElementById(fld).value = '0' + curSep + '00';
                        updTot(fld, 0);
                        return false;
                    }
                    if (document.getElementById(fld).value <= 0) {
                        //document.getElementById(fld).value = 0;
                        document.getElementById(fld).value = '0' + curSep + '00';
                    }
                }
                else {
                    if (isNaN(f) && document.getElementById(fld).value.length > 0) {
                        document.getElementById(fld).value = 0;
                        updTot(fld, 0);
                        alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                    return false;
                }
                if (document.getElementById(fld).value == null || document.getElementById(fld).value == '') {
                    updTot(fld, 0);
                    return false;
                }
                if (parseFloat(document.getElementById(fld).value) < 0) {
                    //document.getElementById(fld).value = 0;
                    document.getElementById(fld).value = '0' + curSep + '00';
                    updTot(fld, 0);
                    return false;
                }
                if (document.getElementById(fld).value <= 0) {
                    //document.getElementById(fld).value = 0;
                    document.getElementById(fld).value = '0' + curSep + '00';
                }
            }
            updTot(fld, f);
            document.getElementById(fld).innerText = convertNumberToCulture(f, curSep, groupSep);
            return true;
        }

        function updTot(ctrl, n) {

            if (parseFloat(n) == parseFloat(curAmt)) { return }
            var cur = document.getElementById(CurrentAmt);
            var curvalue = 0;
            if (document.getElementById(CurrentAmt).value.length > 0) {
                curvalue = parseFloat(setJsFormat(cur.value, curSep));
            }
            var inv = document.getElementById(InvoiceAmt);
            var invvalue = 0;
            if (document.getElementById(InvoiceAmt).value.length > 0) {
                invvalue = parseFloat(setJsFormat(inv.value, curSep));
            }
            var tax1value = 0;
            if (document.getElementById(Tax1Amt) != null) {
                var tax1 = document.getElementById(Tax1Amt);
                if (document.getElementById(Tax1Amt).value.length > 0) {
                    tax1value = parseFloat(setJsFormat(tax1.value, curSep));
                }
            }
            var tax2value = 0;
            if (document.getElementById(Tax2Amt) != null) {
                var tax2 = document.getElementById(Tax2Amt);
                if (document.getElementById(Tax2Amt).value.length > 0) {
                    tax2value = parseFloat(setJsFormat(tax2.value, curSep));
                }
            }
            var dif;
            var curnewvalue;

            if (document.getElementById(ctrl) != document.getElementById(Tax1Amt) && document.getElementById(ctrl) != document.getElementById(Tax2Amt)) {
                curnewvalue = (parseFloat(curvalue) + parseFloat(n)) - parseFloat(curAmt);
                curnewvalue = Math.round(curnewvalue * 100) / 100;
                dif = parseFloat(invvalue) - parseFloat(tax1value) - parseFloat(tax2value) - parseFloat(curnewvalue);
                dif = Math.round(dif * 100) / 100;
            }
            else {
                curnewvalue = parseFloat(curvalue);
                curnewvalue = Math.round(curnewvalue * 100) / 100;
                dif = parseFloat(invvalue) - parseFloat(tax1value) - parseFloat(tax2value) - parseFloat(curnewvalue);
                dif = Math.round(dif * 100) / 100;
            }

            document.getElementById(CurrentAmt).innerText = convertNumberToCulture(curnewvalue, curSep, groupSep);
            document.getElementById(inputCuurentAmt).innerText = convertNumberToCulture(curnewvalue, curSep, groupSep);
            var formattedDiff;
            if (dif >= 0) {
                formattedDiff = convertNumberToCulture(dif, curSep, groupSep);
            } else {
                formattedDiff = "-" + convertNumberToCulture(dif * (-1), curSep, groupSep);
            }
            document.getElementById(Diffamt).innerText = formattedDiff;
            document.getElementById(inputDifference).innerText = formattedDiff;
            if (!dif == 0) {
                if (document.getElementById(btnpay) != null) {
                    document.getElementById(btnpay).disabled = true;
                }
            } else {
                if (document.getElementById(btnpay) != null) {
                    document.getElementById(btnpay).disabled = false;
                }
            }
        }
        function setCur(n) {
            var ctrl = document.getElementById(n);
            var f = parseFloat(setJsFormat(ctrl.value, curSep));
            curAmt = f;
        }
        function calcAmountToBePaid(sID, aID, r, s) {
            var ctrl1 = document.getElementById(sID);
            var ctrl2 = document.getElementById(aID);
            var newSal = parseFloat(setJsFormat(ctrl1.value, curSep));
            var r = r / 100;
            var s = s / 100;


            if (newSal > r) {
                alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                    document.getElementById(sID).innerText = convertNumberToCulture(s, curSep, groupSep);
                    if (r >= 0) {
                        calcAmountToBePaid(sID, aID, r * 100, s * 100);
                        //setCur(aID);  
                        chkamt(aID, r * 100);
                    }
                    return false;
                }
                if (isNaN(newSal) || document.getElementById(sID).value == null || document.getElementById(sID).value == '') {
                    alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                document.getElementById(sID).innerText = convertNumberToCulture(s, curSep, groupSep);

                if (r >= 0) {
                    calcAmountToBePaid(sID, aID, r * 100, s * 100);
                    chkamt(aID, r * 100);
                }
                return false;
            }
            document.getElementById(aID).innerText = convertNumberToCulture((r - newSal), curSep, groupSep);

        }
    </script>
</asp:Content>
