<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimIssueActionAnswerForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimIssueActionAnswerForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="Elita" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>

</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="divSerialNumber" runat="server" visible="False">
        <div class="dataContainer">
            <table width="100%" border="0" class="formGrid" id="Table1" runat="server">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="LabelSerialNumber" runat="server">ISSUE_SERIAL_NUMBER</asp:Label>:
                                </td>
                                <td colspan="3" align="left">
                                    <asp:TextBox ID="TextBoxSerialNumber" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="LabelImei" runat="server">IMEI</asp:Label>:
                                </td>
                                <td colspan="3" align="left">
                                    <asp:TextBox ID="TextBoxImei" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="Label1" runat="server">ISSUE_ACTION_SERIAL_NUMBER</asp:Label>
                                </td>
                                <td align="left" style="width: 15%">
                                    <asp:RadioButton ID="rdoSerialNumberYes" Text="YES" runat="server" GroupName="SerialNumber" Checked="True"></asp:RadioButton>
                                </td>
                                <td align="left" style="width: 15%">
                                    <asp:RadioButton ID="rdoSerialNumberNo" Text="NO" runat="server" GroupName="SerialNumber"></asp:RadioButton>
                                </td>
                                <td align="left" style="width: 30%"></td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnBackSerialNumber" runat="server" CausesValidation="False" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
                <asp:Button ID="btnSaveSerialNumber" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>
            </div>
        </div>
    </div>
    <div id="divYesNo" runat="server" visible="False">
        <div class="dataContainer">
            <table width="100%" border="0" class="formGrid" id="Table2" runat="server">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="LabelQuestion" runat="server"></asp:Label>
                                </td>
                                <td align="left" style="width: 15%">
                                    <asp:RadioButton ID="rdoYes" runat="server" GroupName="YesNo"></asp:RadioButton>
                                </td>
                                <td align="left" style="width: 15%">
                                    <asp:RadioButton ID="rdoNo" runat="server" GroupName="YesNo" Checked="true"></asp:RadioButton>
                                </td>
                                <td align="left" style="width: 30%">
                                    <asp:RadioButton ID="rdoNoUCC" runat="server" GroupName="YesNo"></asp:RadioButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnBackYesNo" runat="server" CausesValidation="False" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
                <asp:Button ID="btnSaveYesNo" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>
            </div>
        </div>
    </div>
    <div id="divDeviceSelection" runat="server" visible="False">
        <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelDeviceMake" runat="server">MAKE</asp:Label>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDeviceMake" runat="server" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelDeviceModel" runat="server">MODEL</asp:Label>:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxDeviceModel" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelDeviceColor" runat="server">COLOR</asp:Label>:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxDeviceColor" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelDeviceMemory" runat="server">MEMORY</asp:Label>:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxDeviceMemory" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="CLEAR"></asp:Button>
                                <asp:Button ID="btnSkuSearch" runat="server" SkinID="SearchButton" Text="SEARCH_SKU"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="dataContainer">
            <h2 class="dataGridHeader">
                <asp:Label ID="Label2" runat="server">SEARCH_RESULTS_FOR_SKU</asp:Label></h2>
            <div>
                <table width="100%" class="dataGrid">
                    <tr id="trPageSize" runat="server">
                        <td class="bor" align="left">
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="Label3"
                                runat="server">:</asp:Label>
                            &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
                            SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Selected="True" Value="200">200</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td class="bor" align="right">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%">
                <table id="tblDeviceSelection" class="dataGrid" border="0" rules="cols" width="100%">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="GridViewDeviceSelection" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                                SkinID="DetailPageGridView" AllowSorting="false">
                                <SelectedRowStyle Wrap="True" />
                                <EditRowStyle Wrap="True" />
                                <AlternatingRowStyle Wrap="True" />
                                <RowStyle Wrap="True" />
                                <Columns>
                                    <asp:BoundField DataField="Make" HeaderText="Make" />
                                    <asp:BoundField DataField="Model" HeaderText="Model" />
                                    <asp:BoundField DataField="Color" HeaderText="Color" />
                                    <asp:BoundField DataField="Memory" HeaderText="Memory" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="sku_number" />
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="inventory_check">
                                        <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBoxInventory" runat="server" Visible="True" Checked='<%# DataBinder.Eval(Container.DataItem, "CheckInventory") %>'></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ReplacementCost" HeaderText="replacement_cost" />
                                    <asp:BoundField DataField="NumberOfDevice" HeaderText="number_of_device" />
                                    <asp:TemplateField HeaderText="select_device">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdoDevice" runat="server" Enabled='<%# DataBinder.Eval(Container.DataItem, "EnableRdoDevice") %>'
                                                Visible="True" AutoPostBack="true" OnCheckedChanged="rdoDevice_CheckedChanged" Checked='<%# DataBinder.Eval(Container.DataItem, "SelectRdoDevice") %>'></asp:RadioButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ShippingFromName" Visible="false" />
                                    <asp:BoundField DataField="ShippingFromDescription" Visible="false" />
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                <PagerStyle />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnSearchInventory" runat="server" SkinID="SearchButton" Text="SEARCH_INVENTORY" Enabled="false"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnDeviceSelected" runat="server" CausesValidation="False" Text="DEVICE_SELECTED"
                SkinID="PrimaryRightButton"></asp:Button>
            <asp:Button ID="btnDeviceNotSelected" runat="server" CausesValidation="False" Text="DEVICE_NOT_SELECTED"
                SkinID="AlternateRightButton"></asp:Button>
            <asp:Button ID="btnBackDeviceSelection" runat="server" CausesValidation="False" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
        </div>
    </div>
    <div id="divPaymentInstrument" runat="server" visible="False">
        <div class="dataContainer">
            <table cellspacing="0" cellpadding="0" width="65%" border="0" class="formGrid" style="padding-left: 0px;">
                <tr>
                    <td nowrap="nowrap" align="right">
                        <asp:Label ID="Label4" runat="server">REIMBURSEMENT_AMOUNT</asp:Label>:
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="TextBoxReimbursementAmount" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" align="right">
                        <asp:Label ID="lblSelect" runat="server">SELECT</asp:Label>&nbsp;
                        <asp:Label ID="LabelPaymentType" runat="server">PAYMENT_INSTRUMENT</asp:Label>:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPaymentList" TabIndex="1" SkinID="SmallDropDown" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td align="right"></td>
                    <td align="left"></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <Elita:UserControlBankInfo ID="moBankInfoController" runat="server"></Elita:UserControlBankInfo>
            </table>
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnSave_WRITE" runat="server" Text="Save" SkinID="PrimaryRightButton" />
                <asp:Button ID="btnBackPaymentInstrument" runat="server" Text="Back" SkinID="AlternateLeftButton" />
            </div>
        </div>

    </div>
    <div id="divProblemDescription" runat="server" visible="False">
        <div class="dataContainer">
            <table width="100%" border="0" class="formGrid" id="Table3" runat="server">
                <tr>
                    <td align="right" style="width: 40%">
                        <asp:Label ID="LabelProblemDescription" runat="server">Problem_Description</asp:Label>:
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="TextboxProblemDescription" runat="server" SkinID="LargeTextBox" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnBackProblemDescription" runat="server" CausesValidation="False" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
                <asp:Button ID="btnSaveProblemDescription" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>
            </div>
        </div>
    </div>
    <div id="divIssueResponse" runat="server" visible="False">
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnBackIssueResponse" runat="server" Text="Back" SkinID="AlternateLeftButton" />
            </div>
        </div>
    </div>
    <div id="divClaimFulfillmentIssue" runat="server" visible="False">
        <div class="dataContainer">
            <table width="100%" border="0" class="dataGrid" id="Table4" runat="server">
                <tr>
                    <td>
                        <asp:GridView ID="GridQuestions" runat="server" CssClass="formGrid" Width="100%" ShowHeader="false" GridLines="None" BorderStyle="None"
                            BorderWidth="0" BorderColor="Transparent" AutoGenerateColumns="False" AllowPaging="false" SkinID="DetailPageGridView"
                            AllowSorting="False" EnableModelValidation="True">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField Visible="False" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueID" Width="5px" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Issue_Id"))%>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSoftQuestionID" Width="5px" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Soft_Question_Id"))%>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign ="Left" ItemStyle-Width="60%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestionDesc" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="38%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlList" runat="server" Width="100px"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnIssueQuestionBack" runat="server" CausesValidation="False" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
                <asp:Button ID="btnIssueQuestionSave" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>
