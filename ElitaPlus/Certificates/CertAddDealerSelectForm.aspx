<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="CertAddDealerSelectForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CertAddDealerSelectForm"
    Title="Untitled Page" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;background-color:#f1f1f1; height:98%; width:100%">
                    <tr><td style="height:7px;" colspan="2"></td></tr>
                    <tr>
                        <td class="TD_LABEL" rowspan="2">
                            &nbsp;<br />*<asp:Label ID="Label1" runat="server">DEALER</asp:Label>:
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
		                            <td style="white-space: nowrap;"><asp:Label ID="Label2" runat="server">By_Code</asp:Label></td>
		                            <td>&nbsp;&nbsp;</td>
                                    <td style="white-space: nowrap;"><asp:Label ID="Label4" runat="server">By_Description</asp:Label></td>
	                            </tr>
	                            <tr>
	                                <td><asp:DropDownList ID="ddlDealerCode" runat="server" Width="103px"></asp:DropDownList></td>
	                                <td>&nbsp;&nbsp;</td>
                                    <td><asp:DropDownList ID="ddlDealer" runat="server" Width="250px"></asp:DropDownList></td>
	                            </tr>
                            </table>               
                        </td>
                    </tr>
                    <tr><td colspan="2"><hr style="HEIGHT: 1px"/></td></tr>
                    <tr>
                        <td style="text-align:right; padding-right:5px;" colspan="4">
                            <asp:button id="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear"></asp:button>&nbsp;&nbsp;
			                <asp:button id="btnAdd" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH LONGTEXTIMGBUTTON" Text="ADD_CERTIFICATE" Width="130"></asp:button><br />
                        </td>
                    </tr>
                    <tr><td style="height:5px;" colspan="2"></td></tr>
                </table>
            </td>
        </tr>
        <asp:Literal runat="server" ID="spanFiller"></asp:Literal>
    </table>
    <script type="text/javascript">
        function UpdateList(dest){
            var objS = event.srcElement
            var val = objS.options[objS.selectedIndex].value
            var objD = document.getElementById(dest)
            for(i=0; i<objD.options.length; i++){
                if (objD.options[i].value == val){
                    objD.selectedIndex = i;
                    break;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
</asp:Content>
