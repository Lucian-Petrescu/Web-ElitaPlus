<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NotificationDetailForm.aspx.vb" Theme="Default"
	Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.NotificationDetailForm" EnableSessionState="True"
	MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
  <script language="JavaScript" type="text/javascript">
      window.latestClick = '';

      // CloseDropDown('OK');
      function isNotDblClick() {
          if (window.latestClick != "clicked") {
              window.latestClick = "clicked";
              return true;
          } else {
              return false;
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
		<script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
	<div class="dataContainer">
		    <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNOTIFICATION_NAME" runat="server">NOTIFICATION_NAME</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNOTIFICATION_NAME" runat="server" SkinID="LargeTextBox" Width="97%"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblSerialNo" runat="server" >SERIAL_NO:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSerialNo" runat="server" SkinID="LargeTextBox" Width="91%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNOTIFICATION_TYPE" runat="server">NOTIFICATION_TYPE</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNotificationType" runat="server" 
                            SkinID="SmallDropDown">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblAUDIANCE_TYPE" runat="server" >AUDIANCE_TYPE</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAudianceType" runat="server" SkinID="SmallDropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblBegin_date" runat="server">NOTIFICATION_BEGIN_DATE</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="200px"
                            AutoPostBack="False"></asp:TextBox>
                        <asp:ImageButton ID="ImageButtonBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                            Width="20px" />
                    </td>
                    <td align="right">
                        <asp:Label ID="lblEnd_date" runat="server">NOTIFICATION_END_DATE</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="200px"
                            AutoPostBack="False"></asp:TextBox>
                        <asp:ImageButton ID="ImageButtonEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                            Width="20px" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblOutageBegin_date" runat="server">OUTAGE_BEGIN_DATE</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOutageBeginDate" runat="server" SkinID="MediumTextBox" Width="200px"
                            AutoPostBack="False"></asp:TextBox>
                        <asp:ImageButton ID="ImageButtonOutageBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                            Width="20px" />
                    </td>
                    <td align="right">
                        <asp:Label ID="lblOutageEnd_date" runat="server">OUTAGE_END_DATE</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOutageEndDate" runat="server" SkinID="MediumTextBox" Width="200px"
                            AutoPostBack="False"></asp:TextBox>
                        <asp:ImageButton ID="ImageButtonOutageEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                            Width="20px" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        <asp:Label ID="lblContactInfo" runat="server" >CONTACT_INFO</asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtContactInfo" runat="server" SkinID="LargeTextBox"
                            TextMode="MultiLine" Width="97%" Height ="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        <asp:Label ID="lblNOTIFICATION_DETAILS" runat="server" >NOTIFICATION_DETAILS</asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNOTIFICATION_DETAILS" runat="server" SkinID="LargeTextBox"
                            TextMode="MultiLine" Width="97%" Height ="300px"></asp:TextBox>
                    </td>
                </tr>
    </table>
		<input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261" />

	</div>
	<div class="btnZone">
		<asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False"
			SkinID="AlternateLeftButton"></asp:Button>
		<asp:Button ID="btnSave_WRITE" runat="server" Text="Save" CausesValidation="False"
			SkinID="PrimaryRightButton"></asp:Button>
		<asp:Button ID="btnUndo_Write" runat="server" Text="Undo" CausesValidation="False"
			SkinID="AlternateRightButton"></asp:Button>
		<asp:Button ID="btnNew_WRITE" runat="server" Text="New" CausesValidation="False"
			SkinID="AlternateLeftButton"></asp:Button>
		<asp:Button ID="btnCopy_WRITE" runat="server" Text="NEW_WITH_COPY" CausesValidation="False"
			SkinID="AlternateRightButton"></asp:Button>
		<asp:Button ID="btnDelete_WRITE" runat="server" Text="Delete" CausesValidation="False"
			SkinID="CenterButton"></asp:Button>
	</div>
</asp:Content>
