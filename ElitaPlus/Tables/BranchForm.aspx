<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BranchForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BranchForm"EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
                    <td id="Td1" runat="server" colspan="2">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="multipleDropControl" />
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblBranchCode" runat="server">Branch_Code</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtBranchCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
								 <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="lblBranchName" runat="server" Font-Bold="false">Branch_Name</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                          <asp:TextBox ID="txtBranchName" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblBranchType" runat="server">Branch_Type</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                      <asp:DropDownList ID="ddlBranchType" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblStoreMgr" runat="server">Store_Manager</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtStoreManager" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
								<uc1:usercontroladdress id="moAddressController" runat="server"></uc1:usercontroladdress>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblContactPhone" runat="server">Contact_Phone</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtContactPhone" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblContactExt" runat="server">Contact_Phone_Ext</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtContactExt" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblContactFax" runat="server">Contact_Fax</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtContactFax" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblContactEmail" runat="server">Contact_Email</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtContactEmail" AutoPostBack="true" TextMode="MultiLine" style="overflow:auto" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                           <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblMarket" runat="server">Market</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtMarket" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblMarketingRegion" runat="server">REGION</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtMarketingRegion" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblOpenDate" runat="server">BEGIN_DATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtOpendate" TabIndex="17" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="BtnOpenDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCloseDate" runat="server">CLOSE_DATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtClosedate" TabIndex="17" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="BtnCloseDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                </td>
                            </tr>
                              <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblBranchID" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:Label ID="moIsNewBranchLabel" runat="server" Visible="False"></asp:Label>
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

