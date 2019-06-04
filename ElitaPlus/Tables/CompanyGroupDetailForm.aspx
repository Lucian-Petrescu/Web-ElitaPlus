<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompanyGroupDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CompanyGroupDetailForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>
<%@ Register Assembly = "AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Cc1" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
 <style type="text/css">
        .dataEditBox
        {
            border-left: 10px solid #0066CC;
        }
    </style>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
<asp:ScriptManager ID = "ScriptManager1" runat ="server" EnablePageMethods="true" ScriptMode="Auto" >
   <Scripts>
   <asp:ScriptReference Path ="~/Navigation/scripts/ComunaSuggest.js" />
      </Scripts>
</asp:ScriptManager>
<div class="dataEditBox">
              <table class="formGrid" width="100%" cellspacing="0" cellpadding="0" border="0" style="padding-left: 0px";>
                        <tbody>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCompanyGroupName" runat="server">COMPANY_GROUP_NAME</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCompanyGroupName" TabIndex="1" runat="server" SkinID="MediumTextBox"
                                       ></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCompanyGroupCode" runat="server">COMPANY_GROUP_CODE</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCompanyGroupCode" TabIndex="2" runat="server" SkinID="MediumTextBox"
                                       ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelClaimNumbering" runat="server">CLAIM_NUMBERING</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropdownList ID="ddlClaimNumbering" TabIndex="3" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList>
                                </td>
                                <td nowrap="nowrap" align="right">
                                   <asp:Label ID="LabelCaseNumbering" runat="server">CASE_NUMBERING</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                      <asp:DropdownList ID="ddlCaseNumbering" TabIndex="4" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelAuthorizationNumbering" runat="server">AUTHORIZATION_NUMBERING</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropdownList ID="ddlAuthorizationNumbering" TabIndex="5" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelInteractionNumbering" runat="server">INTERACTION_NUMBERING</asp:Label>
                                   
                                </td>
                                <td nowrap="nowrap">
                                   <asp:DropdownList ID="ddlInteractionNumbering" TabIndex="6" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList>  
                                </td>
                            </tr>
                             <tr>
                                <td nowrap="nowrap" align="right">
                                   <asp:Label ID="LabelInvoiceNumbering" runat="server">INVOICE_NUMBERING</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                  <asp:DropdownList ID="ddlinvoicenumbering" TabIndex="7" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblInvoicegrpnumbering" runat="server">INVOICE_GROUP_NUMBERING</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropdownList ID="ddlInvoicegrpnumbering" TabIndex="8" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList> 
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblftpsite" runat="server">FTP_SITE</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                      <asp:DropdownList ID="ddlftpsite" TabIndex="9" runat="server" 
                                        width="180px"></asp:DropdownList>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblpmtgrpnumbering" runat="server">PAYMENT_GROUP_NUMBERING</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                     <asp:DropdownList ID="ddlpmtgrpnumbering" TabIndex="10" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList>
                                </td>

                            </tr>
                              <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblyrstoinactiveusedvehicles" runat="server">YEARS_TO_INACTIVE_USED_VEHICLES</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                      <asp:TextBox ID="txtyrstoinactiveusedvehicles" TabIndex="11" runat="server" 
                                        width="180px"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblinactivenewvehiclesbasedon" runat="server">INACTIVATE_NEWVEHICLE_BASEDON</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                     <asp:DropdownList ID="ddlinactivenewvehiclesbasedon" TabIndex="12" runat="server" skinID="Mediumdropdown"
                                        width="180px"></asp:DropdownList>
                                </td>

                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblAccountingbycompany" runat="server">ACCOUNTING_BY_COMPANY</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                      <asp:DropdownList ID="ddlacctbycomp" TabIndex="13" runat="server" SkinID="MediumDropDown"
                                        width="180px"></asp:DropdownList>
                                </td>
                                <!-- req 5547-->
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblClaimFastApprovalId" runat="server">CLAIM_FAST_APPROVAL_ID</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                      <asp:DropdownList ID="ddlFastApproval" TabIndex="14" runat="server" SkinID="MediumDropDown"
                                        width="180px"></asp:DropdownList>
                                </td>
                             </tr>
                             <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelUseCommEntityTypeId" runat="server">USE_COMM_ENTITY_TYPE_ID</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                      <asp:DropdownList ID="ddlUseCommEntityTypeId" TabIndex="15" runat="server" SkinID="SmallDropDown"
                                        width="180px"></asp:DropdownList>
                                </td>
                             </tr>
                            </tbody>
                            </table>
                              <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server" designtimedragdrop="261" />
                    
                    <div class="btnZone">
                       <asp:Button ID="btnSave_WRITE" runat="server" TabIndex="16" Text="Save" SkinID="PrimaryRightButton">
                      </asp:Button>
                       <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="17" SkinID="AlternateLeftButton">
                      </asp:Button>
                       <asp:Button ID="btnNew_WRITE" runat="server" TabIndex="18" Text="New" SkinID="AlternateLeftButton">
                       </asp:Button>
                      
                    <%-- <asp:Button ID="btnCopy_WRITE" runat="server" TabIndex="46" Text="NEW_WITH_COPY"
                      CausesValidation="False" SkinID="AlternateRightButton"></asp:Button>--%>
                     <asp:Button ID="btnUndo_Write" runat="server" TabIndex="19" Text="Undo" SkinID="AlternateRightButton">
                     </asp:Button> 
                     <asp:Button ID="btnDelete_WRITE" runat="server" TabIndex="20" Text="Delete" SkinID="AlternateRightButton">
                     </asp:Button> &nbsp;&nbsp;
                 </div>
                 </div>


</asp:Content>