<%@ Control Language="vb" 
AutoEventWireup="false"
CodeBehind="UserControlVoidAuthorization.ascx.vb" 
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlVoidAuthorization"
EnableTheming="true" 
%>
<div class="dataContainer">

    <h2 class="dataGridHeader" runat="server">
        <asp:Label runat="server" ID="moVoidClaimAuthorizationLabel" Text="VOID_AUTHORIZATION" />
    </h2>
    <div class="dataGridHeader">
        <table border="0" class="searchGrid" runat="server">
            <tbody>
                <tr>
                    <td align="right" nowrap="nowrap" style=" width:20%">
                        <asp:Label runat="server" ID="lblVoidComment" Text="AUTH_VOID_COMMENT" />
                        :
                    </td>
                    <td align="left" nowrap="nowrap" style=" width:30%">
                        <asp:TextBox ID="txtAuthVoidComment" runat="server" SkinID="LargeTextBox"
                                     TextMode="MultiLine" style=" width:100%"></asp:TextBox>
                    </td>
                   
                    <td align="center" nowrap="nowrap" style=" width:20%">
                    <asp:CheckBox ID="chkCloseClaim" Text="CLOSE_CLAIM" AutoPostBack="false" runat="server" TextAlign="Left" ></asp:CheckBox>
                    </td>
                    
                    <td nowrap="nowrap" align="right" style=" width:10%">
                        <asp:Label ID="lblReasonClosed" runat="server">Reason_Closed</asp:Label>
                    </td>
                    <td nowrap="nowrap" style=" width:20%">
                        <asp:DropDownList ID="cboClosedReason" TabIndex="2" runat="server" SkinID="MediumDropDown"
                                          Enabled="false" AutoPostBack="True" style=" width:100%">
                        </asp:DropDownList>
                    </td>

                </tr>
            </tbody>
        </table>
    </div>

</div>