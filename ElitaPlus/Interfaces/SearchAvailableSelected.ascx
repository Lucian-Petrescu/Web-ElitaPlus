<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SearchAvailableSelected.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SearchAvailableSelected" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<div align="center" ms_positioning="GridLayout" 
    style="background-color: #d5d6e4; WIDTH: 100%; HEIGHT: 98%;">
    <table id="moOutTable" 
        style="background-color:#d5d6e4; WIDTH: 98%; HEIGHT: 98%;" 
        cellspacing="1" cellpadding="1" align="center"
     runat="server" > 
     <tr>
	    <td>
           <table id="tblDynamic" style="WIDTH: 98%; HEIGHT: 100%; ">
                <tr>
                    <td style="width: 13%;" align="right">
                        <asp:Label ID="lblMake" Width="100%" runat="server">MAKE:</asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="cboMake" runat="server" AutoPostBack="False" Width="85%" />
                    </td>
                    <td style="width: 13%;" align="right">
                        <asp:Label ID="lblModel"  Width="100%" runat="server">MODEL:</asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:textbox id="txtModel" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
					    Width="85%"></asp:textbox>
                    </td>
                    <td style="width: 7%;" align="right">
                        <asp:Label ID="lblDescription" Width="100%" runat="server">DESCRIPTION:</asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:textbox id="txtDescription" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
					    Width="85%"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 13%;" align="right">
                        <asp:Label ID="lblEquipmentClass" runat="server" Width="100%">EQUIPMENT_CLASS:</asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:dropdownlist id="cboEquipmentClass" runat="server" AutoPostBack="False" Width="85%"></asp:dropdownlist>    
                    </td>
                    <td style="width: 13%;" align="right">
                        <asp:Label ID="lblEquipmenttype" Width="100%" runat="server">EQUIPMENT_TYPE:</asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:dropdownlist id="cboEquipmenttype" runat="server" AutoPostBack="False" Width="85%"></asp:dropdownlist>    
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					    runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="85px" Text="Search" height="20px"/>
                        &nbsp
                        <asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
                        runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="85px" Text="Clear" height="20px"></asp:button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="padding-top: 10px">
		<td >
            <uc1:UserControlAvailableSelected   ID="UserControlAvailableSelectedEquipmentCodes"
               runat="server">
            </uc1:UserControlAvailableSelected>
        </td>
    </tr>
    <tr>
        <td align="left">
            &nbsp&nbsp&nbsp
            <asp:Button ID="btnSave" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
            cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="SAVE"
            CssClass="FLATBUTTON" Height="20px"></asp:Button>
            &nbsp&nbsp&nbsp
            <asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
			runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="CANCEL"></asp:button>
        </td>
     </tr>
</table>
</div>