<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimItemForm.aspx.vb" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.ClaimItemForm" %>

<%@ Register TagPrefix="Elita" TagName="UserControlClaimInfo" Src="UserControlClaimInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
      <div>

            <Elita:UserControlClaimInfo ID="moClaimInfoController" runat="server" align="center"></Elita:UserControlClaimInfo>
          <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0"><tr>
                                         <td align="right">
                                             <asp:Label ID="moClaimNumberLabel" runat="server">Claim_Number</asp:Label>:&nbsp; 
                                         </td>
                                         <td class="bor padRight" width="177px">
                                             <asp:Label ID="moClaimNumber" runat="server" SkinID="SummaryLabel"></asp:Label>
                                           &nbsp;
                                         </td>
                                         <td align="right">
                                            &nbsp;&nbsp; <asp:Label ID="moDealerLabel" runat="server" Font-Bold="false">Dealer</asp:Label>:&nbsp; 
                                         </td>
                                         <td>
                                         
                                             <asp:Label ID="moDealer" runat="server" SkinID="SummaryLabel"></asp:Label>
                                         </td>
                                     </tr></table>
      </div>
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script type="text/javascript">
    $(document).on("click", "[src*=plus]", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../App_Themes/Default/Images/minus.png");
    });
    $(document).on("click", "[src*=minus]", function () {
        $(this).attr("src", "../App_Themes/Default/Images/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
  
         <div class="dataContainer">
             <div class="stepformZone">
             <div id="tabs" class="style-tabs">
                 <ul>
                     <li><a href="#tbClaimItem" rel="noopener noreferrer">
                         <asp:Label ID="lblClaimItem" runat="server" CssClass="tabHeaderText">CLAIM_ITEM</asp:Label></a></li>
                     <li><a href="#tbReplacementItems" rel="noopener noreferrer">
                         <asp:Label ID="lblRelacementItem" runat="server" CssClass="tabHeaderText">REPLACEMENT_ITEMS</asp:Label></a></li>

                 </ul>

                   <div id="tbClaimItem" class="Page">
                     
                         <div>
                             <asp:Panel ID="EditPanel_WRITE" runat="server">
                                
                                 <table id="moTableOuter" class="formGrid" width="90%">
                                     
                                     <tr>
                                         <td align="right">
                                             <asp:Label ID="moManufacturerLabel" runat="server">Manufacturer</asp:Label>:&nbsp;</td>
                                         <td>
                                             <asp:TextBox ID="moManufacturerText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox></td>
                                         <td align="right">
                                             <asp:Label ID="moModelLabel" runat="server">Model</asp:Label>:&nbsp;</td>
                                         <td>
                                             <asp:TextBox ID="moModelText" runat="server" SkinID="SmallTextBox"></asp:TextBox></td>
                                     </tr>
                                     <tr>

                                         <td align="right">
                                             <asp:Label ID="moSerialNumberLabel" runat="server">SERIAL_NO_LABEL</asp:Label>
                                             <asp:Label ID="moSerialNumberIMEILabel" runat="server">Serial_Number</asp:Label>:&nbsp; 
                                         </td>
                                         <td>
                                             <asp:TextBox ID="moSerialNumberText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox></td>
                                         <td align="right">
                                             <asp:Label ID="Label1" runat="server">Device_Type</asp:Label>:&nbsp;</td>
                                         <td>
                                             <asp:TextBox ID="moDeviceTypeText" runat="server" SkinID="SmallTextBox"></asp:TextBox></td>
                                     </tr>
                                     <tr>
                                         <td align="right">
                                             <asp:Label ID="moIMEINumberLabel" runat="server">IMEI_Number</asp:Label>:&nbsp; 
                                         </td>
                                         <td>
                                             <asp:TextBox ID="moIMEINumberText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox></td>
                                         <td></td>
                                         <td></td>
                                     </tr>
                                     <tr>
                                         <td colspan="4">
                                             <hr style="width: 100%; height: 1px">
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                         </div>
                   </div>

                     <div id="tbReplacementItems" class="Page">
                         <div>
                             <table width="100%" class="dataGrid">
                                 <tr>
                                     <td class="bor" align="left"></td>
                                     <td class="bor" align="right">
                                         <asp:Label ID="lblCdRecordCount" runat="server"></asp:Label>
                                     </td>
                                 </tr>
                             </table>
                         </div>
                         <asp:GridView ID="GridViewReplacementItems" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false" SkinID="DetailPageGridView" AllowSorting="false" PageSize="50" DataKeyNames="claim_equipment_id">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" src="../App_Themes/Default/Images/plus.png">
                                        <asp:Panel ID="pnlConseqDamageIssue" runat="server" Style="display: none">
                                            <asp:GridView ID="GridViewReplacementItemsStatus" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false" SkinID="DetailPageGridView" AllowSorting="false">
                                                <Columns>
                                                     <asp:BoundField DataField="claim_equipment_status_xcd" HeaderText="STATUS_DESCRIPTION" ItemStyle-Width="15%"></asp:BoundField>
                                                    <asp:BoundField DataField="claim_equipment_status_date" HeaderText="STATUS_DATE" ItemStyle-Width="20%"></asp:BoundField>
                                                   
                                                     </Columns>
                                                
                                                <PagerStyle></PagerStyle>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="authorization_number" HeaderText="CLAIM_AUTHORIZATION_NUMBER" ItemStyle-Width="30%"></asp:BoundField>
                                <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundField>
                                <asp:BoundField DataField="Make" HeaderText="Make" ItemStyle-Width="15%"></asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="Serial_Number" HeaderText="SERIAL_NUMBER" ItemStyle-Width="17%"></asp:BoundField>
                                 <asp:BoundField DataField="IMEI_NUMBER" HeaderText="IMEI_Number" ItemStyle-Width="17%"></asp:BoundField>
                                <asp:BoundField DataField="Device_Type" HeaderText="Device_Type" ItemStyle-Width="17%"></asp:BoundField>
                                <asp:BoundField DataField="void_reason_xcd" HeaderText="VOID_REASON" ItemStyle-Width="17%"></asp:BoundField>
                            </Columns>
                            <PagerStyle></PagerStyle>
                        </asp:GridView>
                     </div>
             </div>

         </div>  
       </div>

  

    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" SkinID="AlternateLeftButton" Text="BACK"></asp:Button>
    </div>

</asp:Content>
