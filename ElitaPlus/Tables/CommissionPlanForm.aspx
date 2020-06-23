<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommissionPlanForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CommissionPlanForm"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <script type="text/javascript" language="javascript">
        var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
        var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
        var US = /^(((\d{1,3})(,\d{3})*)|(\d+))(\.\d{1,4})?$/;
        var EU = /^(((\d{1,3})(\.\d{3})*)|(\d+))(,\d{1,4})?$/;

        function UpdateMarkup() {

            var BrokerMakup = document.getElementById('<%=txtBrokerMakupPct.ClientID%>');
            var BrokerMakup2 = document.getElementById('<%=txtBrokerMakupPct2.ClientID%>');
            var BrokerMakup3 = document.getElementById('<%=txtBrokerMakupPct3.ClientID%>');
            var BrokerMakup4 = document.getElementById('<%=txtBrokerMakupPct4.ClientID%>');
            var BrokerMakup5 = document.getElementById('<%=txtBrokerMakupPct5.ClientID%>');

            var brokerMakupEntity = document.getElementById('<%=txtBrokerMakupEntity.ClientID%>');
            var brokerMakupEntity2 = document.getElementById('<%=txtBrokerMakupEntity2.ClientID%>');
            var brokerMakupEntity3 = document.getElementById('<%=txtBrokerMakupEntity3.ClientID%>');
            var brokerMakupEntity4 = document.getElementById('<%=txtBrokerMakupEntity4.ClientID%>');
            var brokerMakupEntity5 = document.getElementById('<%=txtBrokerMakupEntity5.ClientID%>');

            var markup = document.getElementById('<%=txtBrokerMakupPct.ClientID%>');
            var markup2 = document.getElementById('<%=txtBrokerMakupPct2.ClientID%>');
            var markup3 = document.getElementById('<%=txtBrokerMakupPct3.ClientID%>');
            var markup4 = document.getElementById('<%=txtBrokerMakupPct4.ClientID%>');
            var markup5 = document.getElementById('<%=txtBrokerMakupPct5.ClientID%>');


            if (ValidCulture(BrokerMakup) && ValidCulture(BrokerMakup2) && ValidCulture(BrokerMakup3) && ValidCulture(BrokerMakup4) && ValidCulture(BrokerMakup5)) {

                if (BrokerMakup.value == '') { BrokerMakup.value = 0; }
                if (BrokerMakup2.value == '') { BrokerMakup2.value = 0; }
                if (BrokerMakup3.value == '') { BrokerMakup3.value = 0; }
                if (BrokerMakup4.value == '') { BrokerMakup4.value = 0; }
                if (BrokerMakup5.value == '') { BrokerMakup5.value = 0; }

                if (decSep == '.') {

                    if (EU.test(BrokerMakup.value) && EU.test(BrokerMakup2.value) && EU.test(BrokerMakup3.value) && EU.test(BrokerMakup4.value) && EU.test(BrokerMakup5.value)) {

                        if (brokerMakupEntity.value != '')
                            markup.value = parseFloat(setJsFormat(BrokerMakup.value, ','));
                        if (brokerMakupEntity2.value != '')
                            markup2.value = parseFloat(setJsFormat(BrokerMakup2.value, ','));
                        if (brokerMakupEntity3.value != '')
                            markup3.value = parseFloat(setJsFormat(BrokerMakup3.value, ','));
                        if (brokerMakupEntity4.value != '')
                            markup4.value = parseFloat(setJsFormat(BrokerMakup4.value, ','));
                        if (brokerMakupEntity5.value != '')
                            markup5.value = parseFloat(setJsFormat(BrokerMakup5.value, ','));
                    } else {
                        if (brokerMakupEntity.value != '')
                            markup.value = parseFloat(setJsFormat(BrokerMakup.value, decSep));
                        if (brokerMakupEntity2.value != '')
                            markup2.value = parseFloat(setJsFormat(BrokerMakup2.value, decSep));
                        if (brokerMakupEntity3.value != '')
                            markup3.value = parseFloat(setJsFormat(BrokerMakup3.value, decSep));
                        if (brokerMakupEntity4.value != '')
                            markup4.value = parseFloat(setJsFormat(BrokerMakup4.value, decSep));
                        if (brokerMakupEntity5.value != '')
                            markup5.value = parseFloat(setJsFormat(BrokerMakup5.value, decSep));

                    }
                } else {
                    if (US.test(BrokerMakup.value) && US.test(BrokerMakup2.value) && US.test(BrokerMakup3.value) && US.test(BrokerMakup4.value) && US.test(BrokerMakup5.value)) {   //alert("D");
                        if (brokerMakupEntity.value != '')
                            markup.value = parseFloat(setJsFormat(BrokerMakup.value, '.'));
                        if (brokerMakupEntity2.value != '')
                            markup2.value = parseFloat(setJsFormat(BrokerMakup2.value, '.'));
                        if (brokerMakupEntity3.value != '')
                            markup3.value = parseFloat(setJsFormat(BrokerMakup3.value, '.'));
                        if (brokerMakupEntity4.value != '')
                            markup4.value = parseFloat(setJsFormat(BrokerMakup4.value, '.'));
                        if (brokerMakupEntity5.value != '')
                            markup5.value = parseFloat(setJsFormat(BrokerMakup5.value, '.'));
                    } else {
                        if (brokerMakupEntity.value != '')
                            markup.value = parseFloat(setJsFormat(BrokerMakup.value, decSep));

                        if (brokerMakupEntity2.value != '')
                            markup2.value = parseFloat(setJsFormat(BrokerMakup2.value, decSep));
                        if (brokerMakupEntity3.value != '')
                            markup3.value = parseFloat(setJsFormat(BrokerMakup3.value, decSep));
                        if (brokerMakupEntity4.value != '')
                            markup4.value = parseFloat(setJsFormat(BrokerMakup4.value, decSep));
                        if (brokerMakupEntity5.value != '')
                            markup5.value = parseFloat(setJsFormat(BrokerMakup5.value, decSep));

                    }
                }
                markupTotal = document.getElementById('<%=txtBrokerPctTotal.ClientID%>');
                markupTotal.value = convertNumberToCulture('0.00', decSep, groupSep);
                if (brokerMakupEntity.value != '')
                    markupTotal.value = parseFloat(setJsFormat(markupTotal.value, '.')) + parseFloat(setJsFormat(markup.value, '.'));
                if (brokerMakupEntity2.value != '')
                    markupTotal.value = parseFloat(setJsFormat(markupTotal.value, '.')) + parseFloat(setJsFormat(markup2.value, '.'));
                if (brokerMakupEntity3.value != '')
                    markupTotal.value = parseFloat(setJsFormat(markupTotal.value, '.')) + parseFloat(setJsFormat(markup3.value, '.'));
                if (brokerMakupEntity4.value != '')
                    markupTotal.value = parseFloat(setJsFormat(markupTotal.value, '.')) + parseFloat(setJsFormat(markup4.value, '.'));
                if (brokerMakupEntity5.value != '')
                    markupTotal.value = parseFloat(setJsFormat(markupTotal.value, '.')) + parseFloat(setJsFormat(markup5.value, '.'));

                markupTotal.value = round_num(markupTotal.value, 4);
                if (markup.value != '')
                    markup.value = convertNumberToCulture(markup.value, decSep, groupSep);
                if (markup2.value != '')
                    markup2.value = convertNumberToCulture(markup2.value, decSep, groupSep);
                if (markup3.value != '')
                    markup3.value = convertNumberToCulture(markup3.value, decSep, groupSep);
                if (markup4.value != '')
                    markup4.value = convertNumberToCulture(markup4.value, decSep, groupSep);
                if (markup5.value != '')
                    markup5.value = convertNumberToCulture(markup5.value, decSep, groupSep);
                if (markupTotal.value != '')
                    markupTotal.value = convertNumberToCulture(markupTotal.value, decSep, groupSep);
            } else {
                alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
            }

        }
        function UpdateBroker() {
            var BrokerComm = document.getElementById('<%=txtBrokerCommPct.ClientID%>');
            var BrokerComm2 = document.getElementById('<%=txtBrokerCommPct2.ClientID%>');
            var BrokerComm3 = document.getElementById('<%=txtBrokerCommPct3.ClientID%>');
            var BrokerComm4 = document.getElementById('<%=txtBrokerCommPct4.ClientID%>');
            var BrokerComm5 = document.getElementById('<%=txtBrokerCommPct5.ClientID%>');

            var brokerMakupEntity = document.getElementById('<%=txtBrokerMakupEntity.ClientID%>');
            var brokerMakupEntity2 = document.getElementById('<%=txtBrokerMakupEntity2.ClientID%>');
            var brokerMakupEntity3 = document.getElementById('<%=txtBrokerMakupEntity3.ClientID%>');
            var brokerMakupEntity4 = document.getElementById('<%=txtBrokerMakupEntity4.ClientID%>');
            var brokerMakupEntity5 = document.getElementById('<%=txtBrokerMakupEntity5.ClientID%>');

            var Comm = document.getElementById('<%=txtBrokerCommPct.ClientID%>');
            var Comm2 = document.getElementById('<%=txtBrokerCommPct2.ClientID%>');
            var Comm3 = document.getElementById('<%=txtBrokerCommPct3.ClientID%>');
            var Comm4 = document.getElementById('<%=txtBrokerCommPct4.ClientID%>');
            var Comm5 = document.getElementById('<%=txtBrokerCommPct5.ClientID%>');

            if (ValidCulture(BrokerComm) && ValidCulture(BrokerComm2) && ValidCulture(BrokerComm3) && ValidCulture(BrokerComm4) && ValidCulture(BrokerComm5)) {

                if (BrokerComm.value == '') { BrokerComm.value = 0; }
                if (BrokerComm2.value == '') { BrokerComm2.value = 0; }
                if (BrokerComm3.value == '') { BrokerComm3.value = 0; }
                if (BrokerComm4.value == '') { BrokerComm4.value = 0; }
                if (BrokerComm5.value == '') { BrokerComm5.value = 0; }

                if (decSep == '.') {

                    if (EU.test(BrokerComm.value) && EU.test(BrokerComm2.value) && EU.test(BrokerComm3.value) && EU.test(BrokerComm4.value) && EU.test(BrokerComm5.value)) {

                        if (brokerMakupEntity.value != '')
                            Comm.value = parseFloat(setJsFormat(BrokerComm.value, ','));
                        if (brokerMakupEntity2.value != '')
                            Comm2.value = parseFloat(setJsFormat(BrokerComm2.value, ','));
                        if (brokerMakupEntity3.value != '')
                            Comm3.value = parseFloat(setJsFormat(BrokerComm3.value, ','));
                        if (brokerMakupEntity4.value != '')
                            Comm4.value = parseFloat(setJsFormat(BrokerComm4.value, ','));
                        if (brokerMakupEntity5.value != '')
                            Comm5.value = parseFloat(setJsFormat(BrokerComm5.value, ','));
                    } else {
                        if (brokerMakupEntity.value != '')
                            Comm.value = parseFloat(setJsFormat(BrokerComm.value, decSep));
                        if (brokerMakupEntity2.value != '')
                            Comm2.value = parseFloat(setJsFormat(BrokerComm2.value, decSep));
                        if (brokerMakupEntity3.value != '')
                            Comm3.value = parseFloat(setJsFormat(BrokerComm3.value, decSep));
                        if (brokerMakupEntity4.value != '')
                            Comm4.value = parseFloat(setJsFormat(BrokerComm4.value, decSep));
                        if (brokerMakupEntity5.value != '')
                            Comm5.value = parseFloat(setJsFormat(BrokerComm5.value, decSep));

                    }
                } else {
                    if (US.test(BrokerComm.value) && US.test(BrokerComm2.value) && US.test(BrokerComm3.value) && US.test(BrokerComm4.value) && US.test(BrokerComm5.value)) { //  alert("D");
                        if (brokerMakupEntity.value != '')
                            Comm.value = parseFloat(setJsFormat(BrokerComm.value, '.'));
                        if (brokerMakupEntity2.value != '')
                            Comm2.value = parseFloat(setJsFormat(BrokerComm2.value, '.'));
                        if (brokerMakupEntity3.value != '')
                            Comm3.value = parseFloat(setJsFormat(BrokerComm3.value, '.'));
                        if (brokerMakupEntity4.value != '')
                            Comm4.value = parseFloat(setJsFormat(BrokerComm4.value, '.'));
                        if (brokerMakupEntity5.value != '')
                            Comm5.value = parseFloat(setJsFormat(BrokerComm5.value, '.'));
                    } else {
                        if (brokerMakupEntity.value != '')
                            Comm.value = parseFloat(setJsFormat(BrokerComm.value, decSep));
                        if (brokerMakupEntity2.value != '')
                            Comm2.value = parseFloat(setJsFormat(BrokerComm2.value, decSep));
                        if (brokerMakupEntity3.value != '')
                            Comm3.value = parseFloat(setJsFormat(BrokerComm3.value, decSep));
                        if (brokerMakupEntity4.value != '')
                            Comm4.value = parseFloat(setJsFormat(BrokerComm4.value, decSep));
                        if (brokerMakupEntity5.value != '')
                            Comm5.value = parseFloat(setJsFormat(BrokerComm5.value, decSep));

                    }
                }
                CommTotal = document.getElementById('<%=txtCommPctTotal.ClientID%>');
                CommTotal.value = convertNumberToCulture('0.00', decSep, groupSep);
                if (brokerMakupEntity.value != '')
                    CommTotal.value = parseFloat(setJsFormat(CommTotal.value, '.')) + parseFloat(setJsFormat(Comm.value, '.'));
                if (brokerMakupEntity2.value != '')
                    CommTotal.value = parseFloat(setJsFormat(CommTotal.value, '.')) + parseFloat(setJsFormat(Comm2.value, '.'));
                if (brokerMakupEntity3.value != '')
                    CommTotal.value = parseFloat(setJsFormat(CommTotal.value, '.')) + parseFloat(setJsFormat(Comm3.value, '.'));
                if (brokerMakupEntity4.value != '')
                    CommTotal.value = parseFloat(setJsFormat(CommTotal.value, '.')) + parseFloat(setJsFormat(Comm4.value, '.'));
                if (brokerMakupEntity5.value != '')
                    CommTotal.value = parseFloat(setJsFormat(CommTotal.value, '.')) + parseFloat(setJsFormat(Comm5.value, '.'));
                if (CommTotal.value != '')
                    CommTotal.value = round_num(CommTotal.value, 4);


                if (Comm.value != '')
                    Comm.value = convertNumberToCulture(Comm.value, decSep, groupSep);
                if (Comm2.value != '')
                    Comm2.value = convertNumberToCulture(Comm2.value, decSep, groupSep);
                if (Comm3.value != '')
                    Comm3.value = convertNumberToCulture(Comm3.value, decSep, groupSep);
                if (Comm4.value != '')
                    Comm4.value = convertNumberToCulture(Comm4.value, decSep, groupSep);
                if (Comm5.value != '')
                    Comm5.value = convertNumberToCulture(Comm5.value, decSep, groupSep);
                if (CommTotal.value != '')
                    CommTotal.value = convertNumberToCulture(CommTotal.value, decSep, groupSep);
            } else {
                alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
            }

        }

        function ValidCulture(webControl) {

            var ReturnValue = false

            var validNum = /^(((\d{1,3})([\.,]\d{3})*)|(\d+))([\.,]\d{1,4})?$/;

            if (webControl.value == '')
                ReturnValue = true;

            if (US.test(webControl.value)) {
                ReturnValue = true;

            } else {
                if (EU.test(webControl.value)) {

                    ReturnValue = true;
                }
            }
            return ReturnValue;
        }

        function TotalError() {
            var ReturnValue = true

            if ('<%= HasDealerConfigeredForSourceXcd() %>' == 'False') {
                var markupTotal = document.getElementById('<%=txtBrokerPctTotal.ClientID%>');
                var CommTotal = document.getElementById('<%=txtCommPctTotal.ClientID%>');
                var markupTest = markupTotal.value
                var commTest = CommTotal.value

                if (EU.test(markupTotal.value)) {
                    markupTest = setJsFormat(markupTotal.value, ',');
                    commTest = setJsFormat(CommTotal.value, ',');
                }

                if (markupTest != 100.00) {
                    alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COMM_BREAK_MARKUP_ERR)%>');
                    ReturnValue = false
                }

                if (commTest != 100.00) {
                    alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COMM_BREAK_COMM_PCT_ERR)%>');
                    ReturnValue = false
                }
            }

            return ReturnValue;
        }

        $(function () {
            $("[id*=cboCommPercentSourceXcd]").change(function () {
                var row = $(this).closest("tr");
                var value = $(this).val();
                if (value == "ACCTBUCKETSOURCE_COMMBRKDOWN-D") {
                    row.find("[id*=moCommission_PercentText]").val("0.0000");
                    row.find("[id*=moCommission_PercentText]").attr("disabled", "true");
                } else {
                    row.find("[id*=moCommission_PercentText]").removeAttr("disabled");
                }
            });

            $("[id*=moLowPriceText]").change(function () {
                var row = $(this).closest("tr");
                var value = $(this).val();
                if (value > 0)  {
                    row.find("[id*=moCommission_PercentText]").val("0.0000");                    
                }
            });

            $("[id*=moCommission_PercentText]").change(function () {
                var row = $(this).closest("tr");
                var value = $(this).val();
                if (value > 0) {
                    row.find("[id*=moLowPriceText]").val("0.0000");
                }
            });
        });
    </script>
    <style type="text/css">
        .style-tabs-old .ui-widget-header {
            background: white;
            border: none;
        }

        .style-tabs-old {
            background-color: white;
        }

        .tabHeaderTextOld {
            font-size: 12px;
            color: black;
            font-style: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">

        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0" class="formGrid" style="padding: 0px; margin: 0px">
            <asp:Panel ID="moPeriodPanel_WRITE" runat="server">
                <tr>
                    <td align="center" colspan="6" class="borderLeft" width="30%">
                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" width="10%">*
                        <asp:Label ID="moEffectiveLabel" runat="server">Effective</asp:Label>:&nbsp;
                    </td>
                    <td width="20%">&nbsp;
                        <asp:TextBox ID="moEffectiveText_WRITE" TabIndex="10" runat="server" SkinID="MediumTextBox" Width="150px"></asp:TextBox>
                        <asp:ImageButton ID="BtnEffectiveDate_WRITE" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                    </td>
                    <td width="10%" align="right">*
                        <asp:Label ID="moExpirationLabel" runat="server">Expiration</asp:Label>:&nbsp;
                    </td>
                    <td width="20%">&nbsp;
                        <asp:TextBox ID="moExpirationText_WRITE" TabIndex="10" runat="server" SkinID="MediumTextBox"
                            Width="150px"></asp:TextBox>
                        <asp:ImageButton ID="BtnExpirationDate_WRITE" runat="server" ImageAlign="AbsMiddle"
                            ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                    </td>
                    <td align="right" width="10%">*
                        <asp:Label ID="LabelCode" runat="server">Code</asp:Label>:&nbsp;
                    </td>
                    <td width="20%">&nbsp;
                        <asp:TextBox ID="TextBoxCode" TabIndex="10" runat="server" MaxLength="20" SkinID="MediumTextBox" Width="150px"></asp:TextBox>
                    </td>
                    <td width="10%" align="right">*
                        <asp:Label ID="LabelDescription" runat="server">Description</asp:Label>:&nbsp;
                    </td>
                    <td width="20%">&nbsp;
                        <asp:TextBox ID="TextBoxDescription" TabIndex="10" runat="server" MaxLength="20" SkinID="MediumTextBox"
                            Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>                   
                        <asp:Label ID="moCoverageIdLabel" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="moIsNewCoverageLabel" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="moIsNewRateLabel" runat="server" Visible="False"></asp:Label>
                        <input id="HiddenSaveChangesPromptResponse1" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server">
                        <asp:Label ID="moCoverageRateIdLabel" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="moIsNewCoverageConseqDamageLabel" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="moCoverageConseqDamageIdLabel" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </asp:Panel>
        </table>
    </div>
    <div class="dataContainer">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0" class="formGrid" style="padding: 0px; margin: 0px">
            <tr>
                <td>
                    <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />

                    <div id="tabs" class="style-tabs-old" style="border: none;">
                        <ul>
                            <li style="background: #d5d6e4"><a href="#tabsCommPlanDist">
                                <asp:Label ID="Label2" runat="server" CssClass="tabHeaderText">Commission_Distribution</asp:Label></a></li>
                            <li style="background: #d5d6e4; display:none;"><a href="#tabsCommissionBreakdown">
                                <asp:Label ID="Label1" runat="server" CssClass="tabHeaderTextOld">Commission_Breakdown</asp:Label></a></li>
                            <li style="background: #d5d6e4; display:none;"><a href="#tabmoEntityTabPanelWRITE">
                                <asp:Label ID="Label6" runat="server" CssClass="tabHeaderTextOld">Entities</asp:Label></a></li>                            
                        </ul>

                        <div id="tabsCommPlanDist">
                            <table id="tblOpportunities" class="dataGrid" border="0" rules="cols" width="98%">
                                <tr>
                                    <td colspan="2">
                                        <%--<Elita:MessageController runat="server" ID="moMsgControllerRate" Visible="false" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="Center" colspan="2">
                                        <div id="scroller" style="overflow: auto; width: 96%; height: 125px" align="center">
                                            <asp:GridView ID="moGridView" runat="server" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                                AllowPaging="False" PageSize="50" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                                SkinID="DetailPageGridView">
                                                <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                                <EditRowStyle Wrap="False"></EditRowStyle>
                                                <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                                <RowStyle Wrap="False"></RowStyle>
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                                CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                                runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="moCOMM_PLAN_DIST_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COMM_PLAN_DISTRIBUTION_ID"))%>'
                                                                runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="moCOVERAGE_RATE_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COMMISSION_PLAN_ID"))%>'
                                                                runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true" HeaderText="PAYEE_TYPE">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayeeType" Text='<%# Container.DataItem("PAYEE_TYPE_XCD")%>' runat="server"> </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cboPayeeType" runat="server" Width="100"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true" HeaderText="ENTITY_TYPE">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEntityType" Text='<%# GetGuidStringFromByteArray(Container.DataItem("ENTITY_ID"))%>' runat="server"> </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cboEntityType" runat="server" Width="100"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="True" HeaderText="COMMISSION_AMOUNT">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="moLowPriceLabel" Text='<%# GetAmountFormattedToVariableString(Container.DataItem("COMMISSION_AMOUNT"))%>'
                                                                runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="moLowPriceText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="True" HeaderText="Commission_Percent">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="moCommission_PercentLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("COMMISSION_PERCENTAGE"), "N4")%>'
                                                                runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="moCommission_PercentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true" HeaderText="COMMISSION_PERCENT_XCD">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCommPercentSourceXcd" Text='<%# Container.DataItem("COMMISSIONS_SOURCE_XCD")%>' runat="server"> </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cboCommPercentSourceXcd" runat="server" Visible="True" Width="100"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="True" HeaderText="POSITION">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="moRenewal_NumberLabel" Text='<%# GetAmountFormattedToString(Container.DataItem("POSITION"))%>'
                                                                runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="moRenewal_NumberText" runat="server" Visible="True" Width="75" MaxLength="3" Text="0"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="96%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnNewRate_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;
                            <asp:Button ID="BtnSaveRate_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="BtnCancelRate" runat="server" SkinID="AlternateLeftButton" Text="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="tabsCommissionBreakdown" style="border: 1px solid black; display:none;">
                            <div class="Page" runat="server" id="moCommBreakPanel" style="display: block; height: 370px; overflow: auto">

                                <asp:Panel ID="moGridPanel" runat="server" Width="100%">
                                    <table width="100%" class="formGrid" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnRowCreated="RowCreated"
                                                    SkinID="DetailPageGridView" AllowSorting="true" CellPadding="0">
                                                    <SelectedRowStyle Wrap="True" />
                                                    <EditRowStyle Wrap="True" />
                                                    <AlternatingRowStyle Wrap="True" />
                                                    <RowStyle Wrap="True" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="EditRecord"
                                                                    ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                                    runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="moCommissionToleranceId" Text='<%# GetGuidStringFromByteArray(Container.DataItem("commission_tolerance_id"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ALLOWED_MARKUP_PCT">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moAllowedMarkupPctLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("ALLOWED_MARKUP_PCT"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moAllowedMarkupPctText" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TOLERANCE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moToleranceLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("TOLERANCE"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moToleranceText" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="ENTITY_PCT">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moDealerMarkupPctLabel1" Text='<%# GetAmountFormattedPercentString(Container.DataItem("MARKUP_PERCENT1"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moDealerMarkupPctText1" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="ENTITY_PCT2">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moDealerMarkupPctLabe2" Text='<%# GetAmountFormattedPercentString(Container.DataItem("MARKUP_PERCENT2"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moDealerMarkupPctText2" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="ENTITY_PCT3">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moDealerMarkupPctLabel3" Text='<%# GetAmountFormattedPercentString(Container.DataItem("MARKUP_PERCENT3"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moDealerMarkupPctText3" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="ENTITY_PCT4">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moDealerMarkupPctLabel4" Text='<%# GetAmountFormattedPercentString(Container.DataItem("MARKUP_PERCENT4"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moDealerMarkupPctText4" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="ENTITY_PCT5">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moDealerMarkupPctLabel5" Text='<%# GetAmountFormattedPercentString(Container.DataItem("MARKUP_PERCENT5"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moDealerMarkupPctText5" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPctLabel1" Text='<%# GetAmountFormattedPercentString(Container.DataItem("COMMISSION_PERCENT1"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moBrokerMarkupPctText1" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT2">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPctLabel2" Text='<%# GetAmountFormattedPercentString(Container.DataItem("COMMISSION_PERCENT2"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moBrokerMarkupPctText2" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT3">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPctLabel3" Text='<%# GetAmountFormattedPercentString(Container.DataItem("COMMISSION_PERCENT3"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moBrokerMarkupPctText3" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT4">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPctLabel4" Text='<%# GetAmountFormattedPercentString(Container.DataItem("COMMISSION_PERCENT4"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moBrokerMarkupPctText4" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT5">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPctLabel5" Text='<%# GetAmountFormattedPercentString(Container.DataItem("COMMISSION_PERCENT5"))%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="moBrokerMarkupPctText5" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT1_SOURCE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPct1SourceLabel" Text='<%# Container.DataItem("COMMISSION_PERCENT1_SOURCE")%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                            <asp:TextBox ID="moBrokerMarkupPct1SourceText" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                        </EditItemTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT2_SOURCE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPct2SourceLabel" Text='<%# Container.DataItem("COMMISSION_PERCENT2_SOURCE")%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                            <asp:TextBox ID="moBrokerMarkupPct2SourceText" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                        </EditItemTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT3_SOURCE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPct3SourceLabel" Text='<%# Container.DataItem("COMMISSION_PERCENT3_SOURCE")%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                            <asp:TextBox ID="moBrokerMarkupPct3SourceText" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                        </EditItemTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT4_SOURCE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPct4SourceLabel" Text='<%# Container.DataItem("COMMISSION_PERCENT4_SOURCE")%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                            <asp:TextBox ID="moBrokerMarkupPct4SourceText" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                        </EditItemTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="True" HeaderText="COMMISSION_PCT5_SOURCE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="true"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="75px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="moBrokerMarkupPct5SourceLabel" Text='<%# Container.DataItem("COMMISSION_PERCENT5_SOURCE")%>'
                                                                    runat="server">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                            <asp:TextBox ID="moBrokerMarkupPct5SourceText" runat="server" Visible="True" Width="75px"></asp:TextBox>
                                        </EditItemTemplate>--%>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                                    <PagerStyle />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="moDetailPanel_WRITE" runat="server" Visible="False">

                                    <table width="100%" class="formGrid" border="0" style="padding: 0px; margin: 0px;">
                                        <asp:Panel ID="moRestrictDetailPanel" runat="server">
                                            <tr>
                                                <td colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" align="right">*
                                        <asp:Label ID="moAllowedMarkupPctDetailLabel" runat="server" Font-Bold="False">ALLOWED_MARKUP_PCT</asp:Label>:
                                                </td>
                                                <td>&nbsp;
                                        <asp:TextBox ID="moAllowedMarkupPctDetailText" TabIndex="1" runat="server" Width="150px" SkinID="MediumTextBox"></asp:TextBox>
                                                </td>
                                                <td valign="middle" align="right">*
                                        <asp:Label ID="moToleranceDetailLabel" runat="server" Font-Bold="False">TOLERANCE</asp:Label>:
                                                </td>
                                                <td>&nbsp;
                                        <asp:TextBox ID="moToleranceDetailText" TabIndex="1" runat="server" Width="150px" SkinID="MediumTextBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" valign="middle" align="center">
                                                    <hr style="height: 1px" size="1" />
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                    <table width="100%" class="formGrid" border="0" style="padding: 0px; margin: 0px">
                                        <tr>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="False">PAYEE_TYPE</asp:Label>
                                            </td>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:Label ID="LabelCommEntity" runat="server" Font-Bold="False">Commission_Entity</asp:Label>
                                            </td>
                                            <td align="center" valign="middle" style="width: 10%; height: 11px">
                                                <asp:Label ID="Label4" runat="server">ENTITY/PAYEE</asp:Label>
                                            </td>
                                            <td align="center" valign="middle" style="width: 10%; height: 11px">
                                                <asp:Label ID="lblMarkup" runat="server" Font-Bold="False">MARKUP</asp:Label>&nbsp;<asp:Label ID="Label5" runat="server">PERCENT</asp:Label>
                                            </td>
                                            <td align="center" valign="middle" style="width: 10%; height: 11px">
                                                <asp:Label ID="lblComm" runat="server" Font-Bold="False">COMMISSION</asp:Label>&nbsp;<asp:Label ID="Label15" runat="server">PERCENT</asp:Label>
                                            </td>
                                            <td align="center" valign="middle" style="width: 10%; height: 11px">
                                                <asp:Label ID="lblAcctSourceBucket" runat="server" Font-Bold="False">ACCT SOURCE BUCKET</asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPayeeType1" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPeriodEntity1" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 10%; height: 13px">
                                                <asp:TextBox ID="txtBrokerMakupEntity" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                                    TabIndex="1" Width="95%"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">&nbsp;<asp:TextBox ID="txtBrokerMakupPct" TabIndex="1" onchange="UpdateMarkup();"
                                                runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">&nbsp;<asp:TextBox ID="txtBrokerCommPct" runat="server" OnChange="UpdateBroker();"
                                                SkinID="SmallTextBox" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">
                                                <asp:DropDownList ID="cboBrokerCommPctSourceXcd" TabIndex="294" runat="server" Width="100px"
                                                    SkinID="SmallDropDown" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">&nbsp;<asp:TextBox ID="AsCommId1" Visible="false" runat="server" SkinID="SmallTextBox"
                                                TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPayeeType2" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPeriodEntity2" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 10%; height: 11px">
                                                <asp:TextBox ID="txtBrokerMakupEntity2" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                                    TabIndex="1" Width="95%"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerMakupPct2" TabIndex="1" OnChange="UpdateMarkup();"
                                                runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerCommPct2" runat="server" OnChange="UpdateBroker();"
                                                SkinID="SmallTextBox" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">
                                                <asp:DropDownList ID="cboBrokerCommPct2SourceXcd" TabIndex="294" runat="server" Width="100px"
                                                    SkinID="SmallDropDown" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">&nbsp;<asp:TextBox ID="AsCommId2" Visible="false" runat="server" SkinID="SmallTextBox"
                                                TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPayeeType3" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPeriodEntity3" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 10%; height: 11px">
                                                <asp:TextBox ID="txtBrokerMakupEntity3" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                                    TabIndex="1" Width="95%"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerMakupPct3" TabIndex="1" OnChange="UpdateMarkup();"
                                                runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerCommPct3" runat="server" OnChange="UpdateBroker();"
                                                SkinID="SmallTextBox" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">
                                                <asp:DropDownList ID="cboBrokerCommPct3SourceXcd" TabIndex="294" runat="server" Width="100px"
                                                    SkinID="SmallDropDown" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">&nbsp;<asp:TextBox ID="AsCommId3" Visible="false" runat="server" SkinID="SmallTextBox"
                                                TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPayeeType4" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPeriodEntity4" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 10%; height: 11px">
                                                <asp:TextBox ID="txtBrokerMakupEntity4" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                                    TabIndex="1" Width="95%"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerMakupPct4" TabIndex="1" OnChange="UpdateMarkup();"
                                                runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerCommPct4" runat="server" OnChange="UpdateBroker();"
                                                SkinID="SmallTextBox" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">
                                                <asp:DropDownList ID="cboBrokerCommPct4SourceXcd" TabIndex="294" runat="server" Width="100px"
                                                    SkinID="SmallDropDown" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">&nbsp;<asp:TextBox ID="AsCommId4" Visible="false" runat="server" SkinID="SmallTextBox"
                                                TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPayeeType5" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" colspan="" style="height: 1px" valign="middle" width="25%">
                                                <asp:DropDownList ID="cboPeriodEntity5" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 10%; height: 11px">
                                                <asp:TextBox ID="txtBrokerMakupEntity5" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                                    TabIndex="1" Width="95%"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerMakupPct5" TabIndex="1" OnChange="UpdateMarkup();"
                                                runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerCommPct5" runat="server" OnChange="UpdateBroker();"
                                                SkinID="SmallTextBox" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">
                                                <asp:DropDownList ID="cboBrokerCommPct5SourceXcd" TabIndex="294" runat="server" Width="100px"
                                                    SkinID="SmallDropDown" Visible="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="width: 10%; height: 13px">&nbsp;<asp:TextBox ID="AsCommId5" Visible="false" runat="server" SkinID="SmallTextBox"
                                                TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" style="width: 10%; height: 11px" align="right">&nbsp;<asp:Label ID="lblTotal" runat="server">TOTAL</asp:Label>:&nbsp;
                                            </td>
                                            <td valign="middle" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtBrokerPctTotal" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                                TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td valign="middle" style="width: 10%; height: 11px">&nbsp;<asp:TextBox ID="txtCommPctTotal" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                                TabIndex="1"></asp:TextBox>
                                            </td>

                                            <td style="width: 10%; height: 13px" valign="middle"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="moDetailButtonPanel" runat="server" Visible="false">
                                    <div class="btnZone">
                                        <asp:Button ID="btnEntityBack" runat="server" Text="Back" SkinID="AlternateRightButton"></asp:Button>&nbsp;
                            <asp:Button ID="btnEntitySave" runat="server" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>&nbsp;
                            <asp:Button ID="btnEntityUndo" runat="server" Text="Undo" SkinID="PrimaryRightButton"></asp:Button>
                                    </div>
                                    <div class="btnZone">
                                        <asp:Button ID="BtnCancelGrid" runat="server" Text="Back" SkinID="AlternateRightButton"></asp:Button>&nbsp;
                            <asp:Button ID="BtnSaveGrid_WRITE" OnClientClick="if(!TotalError())return false;" runat="server" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>
                                        <asp:Button ID="BtnUndoGrid_WRITE" runat="server" Text="Undo" SkinID="PrimaryRightButton"></asp:Button>
                                        <asp:Button ID="BtnNewGrid_WRITE" runat="server" Text="New" SkinID="AlternateRightButton"></asp:Button>&nbsp;
                            <asp:Button ID="BtnDeleteGrid_WRITE" Visible="False" runat="server" SkinID="CenterButton" Text="Delete"></asp:Button>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <%--<div id="tabmoEntityTabPanelWRITE" style="border:1px solid black;">
                 <asp:Panel ID="moEntityTabPanel_WRITE" runat="server" Width="100%" Height="100%">
                        <table id="tblEntities" style="width: 95%; height: 100%" cellspacing="0" cellpadding="0"
                            rules="cols" border="0">
                            <tr>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:Label ID="Label26" runat="server" Font-Bold="False">PAYEE_TYPE</asp:Label>
                                </td>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:Label ID="LabelCommEntity" runat="server" Font-Bold="False">Commission_Entity</asp:Label>
                                </td>
                            </tr>
                            <tr>                                              
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPayeeType1" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPeriodEntity1" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>                                                                
                            </tr>
                            <tr>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPayeeType2" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPeriodEntity2" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPayeeType3" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPeriodEntity3" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPayeeType4" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>                                                            
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPeriodEntity4" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPayeeType5" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" colspan="" style="height: 1px" valign="middle" width="45%">
                                    <asp:DropDownList ID="cboPeriodEntity5" runat="server" TabIndex="4" Width="70%" SkinID="MediumDropDown"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            </table>
                            <div class="btnZone">
                                <asp:Button ID="btnEntityBack" runat="server" Text="Back" SkinID="AlternateRightButton"></asp:Button>&nbsp;
                                <asp:Button ID="btnEntitySave" runat="server" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>&nbsp;
                                <asp:Button ID="btnEntityUndo" runat="server" Text="Undo" SkinID="PrimaryRightButton"></asp:Button>
                            </div>        
                    <!-- Tab end -->
                </asp:Panel>
              </div>      --%>
                        
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />

    <div class="btnZone">
        <asp:Panel ID="moPeriodButtonPanel" runat="server">
            <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back"></asp:Button>
            <asp:Button ID="btnSave_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>
            <asp:Button ID="btnUndo_WRITE" runat="server" SkinID="AlternateRightButton" Text="Undo"></asp:Button>
            <asp:Button ID="btnNew_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>
            <asp:Button ID="btnCopy_WRITE" runat="server" SkinID="AlternateRightButton" Text="NEW_WITH_COPY" CausesValidation="False"></asp:Button>
            <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="CenterButton" Text="Delete"></asp:Button>
        </asp:Panel>
    </div>

    <%--<script type="text/javascript" language="javascript">  
        <asp: literal id="litScriptVars" runat="server"></asp: literal >

            function EnableControl(value1, value2) {

                var cboName;
                var cboName1;

                cboName = value1;
                cboName1 = value2;

                if (document.getElementById(cboName).value != commEntity) {
                    document.getElementById(cboName1).disabled = true;
                    document.getElementById(cboName1).value = '';
                }
                else {
                    document.getElementById(cboName1).disabled = false;
                }
            }
    </script>--%>

</asp:Content>
