<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProducerForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ProducerForm" EnableSessionState="True" Theme="Default"
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
                <td align="left" nowrap="nowrap" colspan="4" width="100%">
                    <table class="formGrid" width="100%">
                        <tr>
                            <td>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="moMultipleColumnDrop" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCode" runat="server">Code</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCode" runat="server" SkinID="SmallTextBox" MaxLength="5" TabIndex="2"></asp:TextBox>
                                </td>
								 <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="lblDescription" runat="server" Font-Bold="false">Description</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                          <asp:TextBox ID="txtDescription" TabIndex="3" runat="server" SkinID="MediumTextBox" MaxLength="50"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblProducerType" runat="server">Producer_Type</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                      <asp:DropDownList ID="ddlProducerType" AutoPostBack="false" runat="server" TabIndex="4" SkinID="SmallDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblRegulatorRegistrationId" runat="server">Regulator_Registration_Id</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtRegulatorRegistrationId"  runat="server" TabIndex="5" SkinID="MediumTextBox" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblTaxIdNumber" runat="server">Tax_Id</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtTaxIdNumber"  runat="server" TabIndex="6" SkinID="MediumTextBox" MaxLength="50"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                </td>
                                <td align="left" nowrap="nowrap">
                                </td>
                            </tr>

								<uc1:usercontroladdress id="moAddressController" runat="server"></uc1:usercontroladdress>
                       
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

