<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TemplateForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.TemplateForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <%--<script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>--%>
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />

    <style type="text/css">
        
        /* commented because IE 11 doesn't care about :not() and therefore has a different behaviour than Firefox, Chrome and others */
        /*li.ui-state-default.ui-state-hidden[role=tab]:not(.ui-tabs-active) {
            display: none;
        }*/
    </style>

    <script type="text/javascript">    
           
        $(function () {
            debugger;
            showHideSMSConfigTab();            
        });

        function showHideSMSConfigTab() {
            debugger;
            var selectedTab = $('#tabs').tabs().tabs('option', 'active');

            var templateType = $('#<%= ddlTemplateType.ClientID %>').val();
            //alert(templateType);
            if (templateType == "OC_TEMP_TYPE-EMAIL") {
                if (selectedTab == 2) {
                    $('a[href="#tab_Parameters"]').click();
                }                
                $($("#tabs").find("li")[2]).addClass('ui-state-hidden');                 
            } else {
                $($("#tabs").find("li")[2]).removeClass('ui-state-hidden');
            }
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>    
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblTemplateCode" runat="server">TEMPLATE_CODE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtTemplateCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblTemplateDescription" runat="server">TEMPLATE_DESCRIPTION</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtTemplateDescription" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblHasCustomizedParams" runat="server">HAS_CUSTOMIZED_PARAMS_XCD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboHasCustomizedParams" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblAllowManualUse" runat="server">ALLOW_MANUAL_USE_XCD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboAllowManualUse" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblAllowManualResend" runat="server">ALLOW_MANUAL_RESEND_XCD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboAllowManualResend" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblEffectiveDate" runat="server">EFFECTIVE_DATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtEffectiveDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="btnEffectiveDate" ImageAlign="AbsMiddle" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                    </asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblTemplateType" runat="server">TEMPLATE_TYPE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlTemplateType" runat="server" SkinID="MediumDropDown" onchange="showHideSMSConfigTab(false); "></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblExpirationDate" runat="server">EXPIRATION_DATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtExpirationDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="btnExpirationDate" ImageAlign="AbsMiddle" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                    </asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblIsNewTemplate" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:Label ID="lblTemplateId" runat="server" Visible="False"></asp:Label>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnDisabledTab" runat="server" />
        <div id="tabs" class="style-tabs">
	        <ul>
		        <li><a href="#tab_Parameters"><asp:Label ID="lblParametersTab" runat="server" CssClass="tabHeaderText">PARAMETERS_TAB</asp:Label></a></li>
		        <li><a href="#tab_Recipients"><asp:Label ID="lblRecipientsTab" runat="server" CssClass="tabHeaderText">RECIPIENTS_TAB</asp:Label></a></li>
                <li><a href="#tab_SmsConfig"><asp:Label ID="Label1" runat="server" CssClass="tabHeaderText">SMS_CONFIG</asp:Label></a></li>
	        </ul>
            <div id="tab_Parameters">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <asp:GridView ID="ParametersGrid" runat="server" Width="100%" OnRowCreated="ParametersGrid_RowCreated"
                        OnRowCommand="ParametersGrid_RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1"
                        AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField Visible="False" HeaderText="OC_TEMPLATE_PARAMS_ID">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_NAME">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamName" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtParamName" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_VALUE_SOURCE_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamValueSource" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboParamValueSource" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_DATA_TYPE_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamDataType" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboParamDataType" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DATE_FORMAT_STRING">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDateFormatString" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDateFormatString" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ALLOW_EMPTY_VALUE_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAllowEmptyValue" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboAllowEmptyValue" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex%>"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                    <br />
                    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
                    <asp:Button ID="btnNewParameter_WRITE" runat="server" CausesValidation="False" Text="NEW_PARAMETER"
                        SkinID="PrimaryLeftButton"></asp:Button>
                </div>
            </div>
            <div id="tab_Recipients">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <asp:GridView ID="RecipientsGrid" runat="server" Width="100%" OnRowCreated="RecipientsGrid_RowCreated"
                        OnRowCommand="RecipientsGrid_RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1"
                        AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField Visible="False" HeaderText="OC_TEMPLATE_RECIPIENT_ID">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RECIPIENT_SOURCE_FIELD_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRecipientSourceField" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboRecipientSourceField" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex%>"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnNewRecipient_WRITE" runat="server" CausesValidation="False" Text="NEW_RECIPIENT"
                        SkinID="PrimaryLeftButton"></asp:Button>
                </div>
            </div>
            <div id="tab_SmsConfig">
                <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding:0px;">
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="lblSMSAppKey" runat="server">APPLICATION_KEY</asp:Label>
                        </td>
                        <td align="left" nowrap="nowrap">
                            <asp:TextBox ID="txtSMSAppKey" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="lblSMSShortCode" runat="server">SHORT_CODE</asp:Label>
                        </td>
                        <td align="left" nowrap="nowrap">
                            <asp:TextBox ID="txtSMSShortCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="lblSMSTriggerId" runat="server">TRIGGER_ID</asp:Label>
                        </td>
                        <td align="left" nowrap="nowrap">
                            <asp:TextBox ID="txtSMSTriggerId" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE"
            SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
            SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
            SkinID="CenterButton"></asp:Button>
    </div>
</asp:Content>
