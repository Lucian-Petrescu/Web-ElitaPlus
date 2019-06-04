
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminMaintainDropdownByEntityDetailsForm.aspx.vb"  Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AdminMaintainDropdownByEntityDetailsForm" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server" >
    <uc1:ErrorController id="ErrorControl" runat="server" Visible="False"></uc1:ErrorController>
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
    
    <table  width="100%" border="0" class="searchGrid" id="searchTable" runat="server">

                                            <tr class="LeftAlign">
                                                <td style="width:31%;">
                                                    <asp:Label ID="Label1" runat="server">List_Code</asp:Label>:<br />
                                                     <asp:TextBox ID="TextBoxListCode" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="80%" TabIndex="2"></asp:TextBox>
                                                </td>
                                                <td style="width:31%;">
                                                    <asp:Label ID="Label3" runat="server">List_Description</asp:Label>:<br />
                                                     <asp:TextBox ID="TextBoxListDescription" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="80%" TabIndex="2"></asp:TextBox>
                                                </td>
                        
                                            </tr>
                                             <tr class="LeftAlign">
                                                <td style="width:31%;">
                                                    <asp:Label ID="Label4" runat="server">Entity_Type</asp:Label>:<br />
                                                     <asp:TextBox ID="TextBoxEntityType" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="80%" TabIndex="2"></asp:TextBox>
                                                </td>
                                                <td style="width:31%;">
                                                    <asp:Label ID="Label8" runat="server">Entity_Description</asp:Label>:<br />
                                                    <asp:TextBox ID="TextBoxEntityDescription" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="80%" TabIndex="2"></asp:TextBox>
                                                </td>
                        
                                          </tr>
                                      
                                      </table>
    </asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <div class="dataContainer">
           <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SELECT_ENTITY_LIST" Visible="true" ></asp:Label>
        </h2>
                                       <table width="100%" >
             
                                          
                                        <tr>
                                            <td colspan="4"><uc1:UserControlAvailableSelected id="UserControlAvailableSelectedEntityListItem" runat="server"></uc1:UserControlAvailableSelected></td>
                                        </tr>
                                         
                                      
                                      </table>
         
        
     <div class="btnZone">
         <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON"  SkinID="AlternateLeftButton" CausesValidation="false"
                                Font-Bold="false" height="20px" 
                                style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                tabIndex="185" Text="Return" Width="90px"></asp:Button>&#160;
										<asp:Button ID="btnSave_WRITE" runat="server" SkinID="PrimaryRightButton"  CausesValidation="false"
                                Font-Bold="false" height="20px" 
                                style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                Text="Save" Width="90px"></asp:Button>&#160;
										<asp:Button ID="btnCancel" runat="server"  CausesValidation="false" Font-Bold="false" height="20px"   SkinID="AlternateRightButton"
                                style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                Text="Cancel" Width="90px"></asp:Button>
         </div>
          </div>
     </asp:Content>
