<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimIssueForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimIssueForm"
 Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
 <%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server"> 
    <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right" nowrap="nowrap" ><asp:Label ID="lblCustomerName" runat="server" SkinID="SummaryLabel">CUSTOMER_NAME</asp:Label>:</td>
        <td align="left" nowrap="nowrap" colspan="5"><asp:Label ID="lblCustomerNameValue" runat="server" Font-Bold="true"><%#NO_DATA%></asp:Label></td>       
    </tr>   
    <tr>
      <td align="right" nowrap="nowrap"><asp:Label ID="lblClaimNumber" runat="server" SkinID="SummaryLabel">CLAIM_#</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblClaimNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblDealerName" runat="server" SkinID="SummaryLabel">DEALER_NAME</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblDealerNameValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblClaimStatus" runat="server" SkinID="SummaryLabel">CLAIM_STATUS</asp:Label>:</td>
        <td id="ClaimStatusTD" runat="server" align="left" nowrap="nowrap" class="padRight"><asp:Label ID="lblClaimStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
    </tr>
    <tr>
        <td align="right" nowrap="nowrap" ><asp:Label ID="lblWorkPhoneNumber" runat="server" SkinID="SummaryLabel">WORK_CELL_NUMBER</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight" ><asp:Label ID="lblWorkPhoneNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblDealerGroup" runat="server" SkinID="SummaryLabel">DEALER_GROUP</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblDealerGroupValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td> 
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblSubscriberStatus" runat="server" SkinID="SummaryLabel">SUBSCRIBER_STATUS</asp:Label>:</td>
        <td id="SubStatusTD" align="left" nowrap="nowrap" class="padRight"  runat="server"><asp:Label ID="lblSubscriberStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>       
       
    </tr>
    <tr>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblCertificateNumber" runat="server" SkinID="SummaryLabel">CERTIFICATE_NUMBER</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblCertificateNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblSerialNumberImei" runat="server" SkinID="SummaryLabel">SERIAL_NUMBER_IMEI</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblSerialNumberImeiValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblDateOfLoss" runat="server" SkinID="SummaryLabel">DATE_OF_LOSS</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="padRight"><asp:Label ID="lblDateOfLossValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
    </tr>  
</table>  
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server"> 
<Elita:MessageController runat="server" ID="mcIssueStatus" Visible="false"/>   
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<script type="text/javascript">
    function validate()
    {
        var ddlIssueCode = document.getElementById('<%=ddlIssueCode.ClientID %>');
        if (ddlIssueCode.options[ddlIssueCode.selectedIndex].value == '00000000-0000-0000-0000-000000000000')
        {
            var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
            msgBox.style.display = 'block';
            return false;
        }
        

        return true;
    }

    function RefreshDropDownsAndSelect(ctlCodeDropDown, ctlDecDropDown, isSingleSelection, change_Dec_Or_Code)
    {
        RefreshDualDropDownsSelection(ctlCodeDropDown, ctlDecDropDown, isSingleSelection, change_Dec_Or_Code);
        var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
        var objDecDropDown = document.getElementById(ctlDecDropDown); 
        var hdnSelectedIssue = document.getElementById('<%=hdnSelectedIssueCode.ClientID %>');
        hdnSelectedIssue.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
        if (objCodeDropDown.options[objCodeDropDown.selectedIndex].value != '00000000-0000-0000-0000-000000000000')
        {
            var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
            msgBox.style.display = 'none';            
        }

    }

    function HideErrorAndModal(divId)
    {
        var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
        msgBox.style.display = 'none';
        var objCodeDropDown = document.getElementById('<%=ddlIssueCode.ClientID %>'); // "By Code" DropDown control
        var objDecDropDown = document.getElementById('<%=ddlIssueDescription.ClientID %>');
        objCodeDropDown.selectedIndex = 0;
        objDecDropDown.selectedIndex = 0;
        hideModal(divId);
    }

    function RevealModalWithMessage(divId)
    {
        var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
        msgBox.style.display = 'block';
        revealModal(divId);
    }

</script>
<div class="dataContainer">
    <h2 class="dataGridHeader">
    <asp:label runat="server" ID="lblGrdHdr" ></asp:label>
    <span class="">
        <a onclick="RevealModalWithMessage('ModalIssue');" href="javascript:void(0)" rel="noopener noreferrer">
        <asp:Label ID="lblFileNewIssue" runat="server" ></asp:Label>  
        </a> </span>
    </h2>
    <div id="dvGridPager" runat="server">
        <table width="100%" class="dataGrid">
            <tr id="trPageSize" runat="server">
                <td class="bor" align="left">
                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                        runat="server">:</asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                        SkinID="SmallDropDown">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                    </asp:DropDownList>
                </td>
               <td class="bor" align="right">
                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 100%">
        <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
            SkinID="DetailPageGridView" AllowSorting="true">
            <SelectedRowStyle Wrap="True" />
            <EditRowStyle Wrap="True" />
            <AlternatingRowStyle Wrap="True" />
            <RowStyle Wrap="True" />
            <HeaderStyle />
            <Columns>
                <asp:TemplateField HeaderText="Issue" SortExpression="ISSUE_DESCRIPTION">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="Select" ID="EditButton_WRITE" runat="server" CausesValidation="False"
                            Text=""></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>     
                <asp:BoundField DataField="CREATED_DATE" SortExpression="CREATED_DATE" ReadOnly="true" HtmlEncode="false"
                    HeaderText="CREATED_DATE" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CREATED_BY" SortExpression="CREATED_BY" ReadOnly="true" HtmlEncode="false"
                    HeaderText="CREATED_BY" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PROCESSED_DATE" SortExpression="PROCESSED_DATE" ReadOnly="true" HtmlEncode="false"
                    HeaderText="PROCESSED_DATE" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PROCESSED_BY" SortExpression="PROCESSED_BY" ReadOnly="true" HtmlEncode="false"
                    HeaderText="PROCESSED_BY" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="STATUS_CODE" ReadOnly="true" HeaderText="Status" SortExpression="Status" HtmlEncode="false" />
            </Columns>
            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
            <PagerStyle />
        </asp:GridView>
    </div>
</div>
<div id="ModalIssue" class="overlay">
    <div id="light" class="overlay_message_content" style="width:500px" >
        <p class="modalTitle">
            <asp:Label ID="lblModalTitle" runat="server" Text="NEW_CLAIM_ISSUE"></asp:Label>
            <a href="javascript:void(0)" onclick="HideErrorAndModal('ModalIssue');" rel="noopener noreferrer">
                <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server" width="16"
                    height="18" align="absmiddle" class="floatR" /></a></p>
        <div class="dataContainer">
            <div runat="server" id="modalMessageBox" class="errorMsg" style="display: none">
                <p>
                   <img id="imgIssueMsg" runat="server" width="16" height="13" align="middle" src="~/App_Themes/Default/Images/icon_error.png" />
                    <asp:Literal runat="server" ID="MessageLiteral" />
                </p>
            </div>
        </div>
        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblIssueCode" runat="server" Text="ISSUE_CODE"></asp:Label>:
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlIssueCode" runat="server" SkinID="MediumDropDown" AutoPostBack="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblIssueDescription" runat="server" Text="ISSUE_DESCRIPTION"></asp:Label>:
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlIssueDescription" runat="server" SkinID="MediumDropDown"
                        AutoPostBack="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="seperator">
                    <img id="Img3" src="~/App_Themes/Default/Images/icon_dash.png" runat="server" width="6" height="5" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCreatedDate" runat="server" Text="CREATED_DATE"></asp:Label>:
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtCreatedDate" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCreatedBy" runat="server" Text="CREATED_BY"></asp:Label>:
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtCreatedBy" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" SkinID="PrimaryRightButton" Text="SAVE" OnClientClick="return validate();" />
                     <input id='btnCancel' runat="server"  type="button" name="Cancel" value="Cancel" onclick="HideErrorAndModal('ModalIssue');"
                        class='popWindowCancelbtn floatR' />
                </td>
            </tr>
        </table>
    </div>
    <div id="fade" class="black_overlay">
    </div>
</div>

<div class="btnZone">
<asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK"  />
</div>
<asp:HiddenField ID="hdnSelectedIssueCode" runat="server" />  
</asp:Content>

