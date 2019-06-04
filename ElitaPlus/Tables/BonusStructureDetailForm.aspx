<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BonusStructureDetailForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.BonusStructureDetailForm" EnableSessionState="True" Theme="Default"
     MasterPageFile="../Navigation/masters/ElitaBase.Master"  %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
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
                                <tr>
                                    <td colspan="2">
                                        <uc1:MultipleColumnDDLabelControl runat="server" ID="ServiceCenterDropControl" />
                                    </td>
                                   
                                </tr>                             
                                <tr>
                                     <td colspan="2">
                                    <uc1:MultipleColumnDDLabelControl runat="server" ID="DealerDropControl" />
                                    </td>
                                </tr>
                                <tr> 
                                    <td colspan="2">
                                           &nbsp; &nbsp; 
                                <asp:Label ID="moProductCodelabel" runat="server">Product_code</asp:Label>
                                  &nbsp; &nbsp;      
                                <asp:DropDownList ID="moProductCode" runat="server" SkinID="MediumDropDown" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                           </tr>

                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moBonusMethodComputationLabel" runat="server">COMPUTE_BONUS_METHOD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moBonusMethodComputationDD" runat="server" SkinID="MediumDropDown" onchange="bonusMethodChanged()"></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moAVGTAT" runat="server">AVG_TURNAROUND_TIME</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moAVGTATText"  runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                 <td align="right" nowrap="nowrap">
                                    *<asp:Label ID="moPercentageOrAmountLabel" runat="server">PERCENTAGE_OR_AMOUNT</asp:Label>
                                     <asp:Label ID="moPercentageOrAmountLabelDef" runat="server" style="display:none">PERCENTAGE_OR_AMOUNT</asp:Label>:                                
                                </td>
                                <td align="left" nowrap="nowrap">                                  
                                    <asp:TextBox ID="moPercentageOrAmountText"  runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>

                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moPriorityLabel" runat="server">PRIORITY</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moPriorityText"  runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moreplacementpercentage" runat="server">MAXIMUM_REPLACEMENT_PERCENTAGE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moreplacementpercentageText"  runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="mobonusamountperiod" runat="server">BONUS_AMOUNT_PERIOD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="mobonusamountperiodText"  runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                     <asp:Label ID="lblMonths" runat="server">MONTHS</asp:Label>
                  
                                </td>
                            </tr>
                            <tr>
                                
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moeffective" runat="server">effective</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moeffectiveText" AutoPostBack="false" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                     <asp:ImageButton ID="btneffective" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                        align="absmiddle" alt="" Width="20" Height="20" />
                                </td>

                                  <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moexpiration" runat="server">expiration</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moexpirationText" AutoPostBack="false" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                     <asp:ImageButton ID="btnExpiration" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                        align="absmiddle" alt="" Width="20" Height="20" />
                                </td>
                            </tr>
                             <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moIsNewBonusStructureLabel" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:Label ID="moBonusStructureIdLabel" runat="server" Visible="False"></asp:Label>
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
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
            SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
            SkinID="CenterButton"></asp:Button>
        <input id = "HiddenDelDeletePromptResponse" type ="Hidden" runat="server" designtimedragdrop="261" />
    </div>
    <script type="text/javascript">
        function bonusMethodChanged() {
            var ele = document.getElementById("<%=moBonusMethodComputationDD.ClientID%>");
            var labelAmount = document.getElementById("<%=moPercentageOrAmountLabel.ClientID%>");
            var txtAmount = document.getElementById("<%=moPercentageOrAmountText.ClientID%>");
            var selectedValue = ele.options[ele.selectedIndex].text;
            var labelAmountDef = document.getElementById("<%=moPercentageOrAmountLabelDef.ClientID%>");
            labelAmount.innerHTML = (ele.selectedIndex == 0) ? labelAmountDef.innerHTML : selectedValue;
            txtAmount.disabled = (selectedValue == "No Bonus") ? true : false;
            if(selectedValue == "No Bonus"){
                txtAmount.value = 0;
                txtAmount.enabled = false
              
            }
            
        }
    </script>
</asp:Content>

