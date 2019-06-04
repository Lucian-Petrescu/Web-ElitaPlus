<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ArgentinaFixedCommissionReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ArgentinaFixedCommissionReportForm" MasterPageFile="~/Reports/ElitaReportBase.Master"    
    Theme="Default" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UserControlAvailableSelected_New" Src="../Common/UserControlAvailableSelected_New.ascx" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">     	             
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
        <div class="dataContainer">
            <div class="stepformZone">
                <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                    width="100%" align="center">                                                  
                    <tr>
                        <td colspan= "2">
                            <table class="formGrid" border="0"  cellspacing="0" cellpadding="0" width="100%">
                                    <tr id="Tr1" runat="server">
                                    <td align="right">*
                                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="200"></asp:TextBox>
                                        <asp:ImageButton ID="BtnBeginDate" runat="server" 
                                            ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>                                                                                                    
                                    <td align="left" colspan="2">*
                                        <asp:Label ID="lblEndDate" runat="server">END_DATE:</asp:Label>    
                                    
                                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="200"></asp:TextBox>
                                        <asp:ImageButton ID="BtnEndDate" runat="server" 
                                            ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>
                                </tr>   
                                <tr><td colspan="4"><hr style="height: 1px" /> </td></tr>                                                                              
                                <tr id="Tr2" runat="server">
                                   <td align="right">
                                        <asp:Label ID="Label1" runat="server">SELECT_ALL_DEALERS:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="" TextAlign="left" onClick="return TogglelDropDownsSelectionsForDealer('rdealer');" runat="server" Checked="True"></asp:RadioButton>
                                    </td>
                                    <td align="left">
                                      <uc1:MultipleColumnDDLabelControl_New ID="multipleDropControl" runat="server">
                                     </uc1:MultipleColumnDDLabelControl_New>
                                     </td>
                                    <td></td>
                                </tr>       
                                <tr id="Tr4" runat="server">
                                    
                                    <td align="right">
                                        <asp:Label ID="lblDealerGroup" runat="server">DEALER_GROUP:</asp:Label>
                                   </td>
                                    <td align="left">
                                      <asp:DropDownList ID="moDealerGroupList" runat="server" AutoPostBack="false" Width="200" onChange="return TogglelDropDownsSelectionsForDealer('dealerGroup');">
                                      </asp:DropDownList>
                                     </td>
                                    <td colspan="2"></td>
                                </tr>                                                                                           
                               <tr id="Tr5" runat="server">
                                   
                                    <td align="right">
                                        <asp:Label ID="lblDealer" runat="server">DEALER:</asp:Label>
                                   </td>
                                    <td align="left" colspan="2">
                                       <uc2:UserControlAvailableSelected_New ID="UsercontrolAvailableSelectedDealers" runat="server">
                                       </uc2:UserControlAvailableSelected_New>
                                     </td> <td width="20%"></td>
                                </tr>
                             </table>
                        </td>
                    </tr> 
                    <tr><td colspan="4"><hr style="height: 1px" /> </td></tr>            
                </table>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton">
            </asp:Button>
        </div>

    <script language='JavaScript' src="../Navigation/Scripts/AvailableSelected_New.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
             function TogglelDropDownsSelectionsForDealer(source) {

                 if (source == "rdealer") {
                     document.getElementById("ctl00_BodyPlaceHolder_multipleDropControl_moMultipleColumnDrop").selectedIndex = 0;
                     document.getElementById("ctl00_BodyPlaceHolder_multipleDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
                     document.getElementById("ctl00_BodyPlaceHolder_moDealerGroupList").selectedIndex = 0;   // "Dealers Group" DropDown control
                 }
                 else if (source == "dealerGroup") {
                     document.getElementById("ctl00_BodyPlaceHolder_multipleDropControl_moMultipleColumnDrop").selectedIndex = 0;
                     document.getElementById("ctl00_BodyPlaceHolder_multipleDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
                     document.getElementById("ctl00_BodyPlaceHolder_rdealer").checked = false;

                 }
                 RemoveAllSelectedDealersForReports("ctl00_BodyPlaceHolder_UsercontrolAvailableSelectedDealers");
             }

    </script>
    </asp:Content>