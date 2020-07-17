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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <div class="contentZoneHome">
        <table class="summaryGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
            width="100%">
            <asp:Panel ID="moCoverageEditPanel" runat="server">
                <tr>
                    <td>
                        <table width="100%" border="0" style="padding: 0px; margin: 0px">
                            <tr>
                                <td style="text-align: left;">
                                    <table width="100%">
                                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" border="0" class="searchGrid" style="padding-top: 20px;">
                            <tr>
                                <td style="padding-left:72px;width:255px">*
                                    <asp:Label ID="LabelCode" runat="server">Code</asp:Label>:&nbsp;
                                </td>
                                <td>*
                                    <asp:Label ID="LabelDescription" runat="server">Description</asp:Label>:&nbsp;
                                </td>
                            </tr>
                            <tr>                                
                                <td style="padding-left:68px;">&nbsp;
                                    <asp:TextBox ID="TextBoxCode" TabIndex="1" runat="server" MaxLength="20" SkinID="MediumTextBox" Width="150px"></asp:TextBox>
                                </td>

                                <td>&nbsp;
                                    <asp:TextBox ID="TextBoxDescription" TabIndex="2" runat="server" MaxLength="50" SkinID="MediumTextBox"
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>                                
                                <td style="padding-left:72px;">*
                                        <asp:Label ID="moEffectiveLabel" runat="server">Effective</asp:Label>:&nbsp;
                                </td>
                                <td>*
                                        <asp:Label ID="moExpirationLabel" runat="server">Expiration</asp:Label>:&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left:68px;">&nbsp;
                                        <asp:TextBox ID="moEffectiveText_WRITE" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" Enabled="false"></asp:TextBox>
                                    <asp:ImageButton ID="BtnEffectiveDate_WRITE" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/calendar.png" Enabled="false"></asp:ImageButton>
                                </td>
                                <td>&nbsp;
                                        <asp:TextBox ID="moExpirationText_WRITE" TabIndex="4" runat="server" SkinID="MediumTextBox"
                                            Width="300px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
            </asp:Panel>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <!--<div class="stepformZone">-->
        <div id="scroller" style="overflow: auto; width: 100%;">
            <h2 class="dataGridHeader">
                <asp:Label ID="lblActiveSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_COMMPLANDISTRIBUTION" Visible="true"></asp:Label>
            </h2>
            <asp:GridView ID="moGridView" runat="server" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
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
                        <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPayeeType" Text='<%# Container.DataItem("PAYEE_TYPE_XCD")%>' runat="server"> </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cboPayeeType" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="true" HeaderText="ENTITY_TYPE">
                        <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblEntityType" Text='<%# GetGuidStringFromByteArray(Container.DataItem("ENTITY_ID"))%>' runat="server"> </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cboEntityType" runat="server" Width="100"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="True" HeaderText="COMMISSION_AMOUNT">
                        <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="moLowPriceLabel" Text='<%# GetAmountFormattedToVariableString(Container.DataItem("COMMISSION_AMOUNT"))%>'
                                runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="moLowPriceText" runat="server" Visible="True" Width="75"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="True" HeaderText="COMMISSION_PERCENT">
                        <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="moCommission_PercentLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("COMMISSION_PERCENTAGE"), "N4")%>'
                                runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="moCommission_PercentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="true" HeaderText="COMMISSION_SOURCE">
                        <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCommPercentSourceXcd" Text='<%# Container.DataItem("COMMISSIONS_SOURCE_XCD") %>' runat="server"></asp:Label>
                            <asp:Label ID="lblCommPercentSourceXcdCode" Text='<%# Container.DataItem("COMMISSIONS_SOURCE_XCD") %>' runat="server" Visible="False"> </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cboCommPercentSourceXcd" runat="server" Visible="True" Width="100"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="true" HeaderText="FIELD_TYPE">
                        <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblActEntitySourceXcd" Text='<%# Container.DataItem("ACCT_FIELD_TYPE_XCD") %>' runat="server"> </asp:Label>
                            <asp:Label ID="lblActEntitySourceXcdCode" Text='<%# Container.DataItem("ACCT_FIELD_TYPE_XCD")%>' runat="server" Visible="False"> </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cboActEntitySourceXcd" runat="server" Visible="True" Width="100"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="True" HeaderText="POSITION">
                        <ItemStyle HorizontalAlign="center" Width="4%"></ItemStyle>
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
                <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>

        <div class="btnZone">
            <asp:Button ID="BtnNewRate_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;
                            <asp:Button ID="BtnSaveRate_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="BtnCancelRate" runat="server" SkinID="AlternateLeftButton" Text="Cancel"></asp:Button>
        </div>

        <!--</div>-->
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />

    </div>


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
