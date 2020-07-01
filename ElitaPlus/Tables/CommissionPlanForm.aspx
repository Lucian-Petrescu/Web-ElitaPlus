<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommissionPlanForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CommissionPlanForm"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <script type="text/javascript" language="javascript">
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
                if (value > 0) {
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
            <asp:Panel ID="moCoverageEditPanel" runat="server">
                <tr>
                    <td align="center" colspan="6" class="borderLeft" width="30%">
                        <table>
                            <tr>
                                <td>
                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" width="10%">*
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
                    <div id="tabsCommPlanDist">
                        <table id="tblOpportunities" class="dataGrid" border="0" rules="cols" width="98%">
                            <tr>
                                <td colspan="2">
                                    <%--<Elita:MessageController runat="server" ID="moMsgControllerRate" Visible="false" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="Center" colspan="2">
                                    <div id="scroller" style="overflow: auto; width: 96%; height: 300px" align="center">
                                        <asp:GridView ID="moGridView" runat="server" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                            AllowPaging="False" PageSize="50" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                            SkinID="DetailPageGridView" Style="height: 125px;">
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
                                                        <asp:Label ID="lblCommPercentSourceXcdCode" Text='<%# Container.DataItem("COMMISSIONS_SOURCE_XCD")%>' runat="server" Visible="False"> </asp:Label>
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
                </td>
            </tr>
        </table>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />

    <div class="btnZone">
        <asp:Panel ID="moPeriodButtonPanel" runat="server">
            <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back" Visible="true"></asp:Button>
            <asp:Button ID="btnSave_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>
            <asp:Button ID="btnUndo_WRITE" runat="server" SkinID="AlternateRightButton" Text="Undo"></asp:Button>
            <asp:Button ID="btnNew_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>
            <asp:Button ID="btnCopy_WRITE" runat="server" SkinID="AlternateRightButton" Text="NEW_WITH_COPY" CausesValidation="False"></asp:Button>
            <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="CenterButton" Text="Delete"></asp:Button>
        </asp:Panel>
    </div>

</asp:Content>
