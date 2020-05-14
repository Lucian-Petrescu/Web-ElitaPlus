<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ImagingIndexingForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ImagingIndexingForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />            
        </Scripts>        
    </asp:ScriptManager>

    <div class="dataContainer">
        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0" style="padding-left: 0px;">
            <tr>
                <td align="right" class="borderLeft">
                    <asp:Label ID="lblImage" runat="server">IMAGE</asp:Label>:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtImage" runat="server" SkinID="mediumTextBox" ReadOnly="true" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Label ID="lblScanDate" runat="server">SCAN_DATE</asp:Label>:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtScanDate" runat="server" SkinID="mediumTextBox" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft">
                    <asp:Label ID="lblClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtClaimNumber" runat="server" SkinID="mediumTextBox" ReadOnly="true" />
                    <asp:HiddenField ID="hfClaimId" runat="server" />
                    <asp:ImageButton ID="imgBtnClaimLookup" runat="server" src="../App_Themes/Default/Images/icon_lookup.png"
                        Height="15" Width="15"  />
                        <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Label ID="lblDocumentType" runat="server">DOCUMENT_TYPE</asp:Label>:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlDocumentType" runat="server" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft">
                    <asp:Label ID="Label1" runat="server">MOBILE_NUMBER</asp:Label>:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtMobileNumber" runat="server" SkinID="mediumTextBox" ReadOnly="true"  />
                </td>
                <td align="left">
                    
                </td>
                <td align="right">
                    <asp:Label ID="Label2" runat="server">CERTIFICATE/POLICY_NUMBER</asp:Label>:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCertificateNumber" runat="server" SkinID="mediumTextBox" ReadOnly="true" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dataContainer">
        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0" style="padding-left: 0px;">
            <tr>
                <td align="right" class="borderLeft">
                    <iframe class="pdfContainer" align="left" runat="server" id="pdfIframe"></iframe>
                </td>
            </tr>
        </table>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnSave" runat="server" SkinID="PrimaryRightButton" Text="Save "></asp:Button>
        <asp:Button ID="btnRedirect" runat="server" SkinID="PrimaryRightButton" Text="Redirect" ></asp:Button>
        <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
    </div>
    <div id="AddNewContainer" style="width: 80%;">
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="LinkButton1" 
           PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="ModalBackground" 
            CancelControlID="LinkButton1" BehaviorID="addNewModal" PopupDragHandleControlID="pnlPopup" Y="50" 
            RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalpopup" Style="display: none; width: 75%; ">
            <div id="light" class="overlay_message_content" style="width: 75%; top: 25px; overflow: hidden;"> 
                <p class="modalTitle">
                    <asp:Label ID="lblModalTitle" runat="server" Text="CLAIM SEARCH"></asp:Label>
                    <%--<asp:ImageButton ImageUrl="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        ID="modalClose" CssClass="floatR" AlternateText="Close" OnClientClick="$find('addNewModal').hide(); return false;" />--%>
                </p>
                <Elita:MessageController runat="server" ID="moMessageController" />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="dataGrid">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:Label>:
                        </td>
                         <td align="left">
                            <asp:Label ID="LabelSearchClaimStatus" runat="server">CLAIM_STATUS</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TextBoxSearchClaimNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxSearchCustomerName" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                         <td align="left">
                            <asp:DropDownList ID="cboClaimStatus" runat="server" AutoPostBack="False" SkinID="SmallDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelSearchServiceCenter" runat="server">SERVICE_CENTER_NAME</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelSearchAuthorizationNumber" runat="server">AUTHORIZATION_NUMBER</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelSearchAuthorizedAmount" runat="server">AUTHORIZED_AMOUNT</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="25%">
                            <asp:TextBox ID="moServiceCenterText" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left" width="25%">
                            <asp:TextBox ID="TextBoxSearchAuthorizationNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="TextBoxSearchAuthorizedAmount" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 12px" colspan="3">
                            <asp:Label ID="lblSortBy" runat="server">Sort By</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:DropDownList ID="cboSortBy" runat="server" AutoPostBack="False" SkinID="SmallDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right" colspan="3" runat="server" >
                            <asp:Button ID="btnCancelSearch" runat="server" SkinID="AlternateRightButton" Text="Cancel"></asp:Button>
                            <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateRightButton" Text="Clear"></asp:Button>
                            <asp:Button ID="btnSearch" runat="server" SkinID="PrimaryRightButton" Text="Search"></asp:Button>
                        </td>
                    </tr>
                    <tr id="trPageSize" runat="server" visible="false">
                        <td valign="top" align="left">
                            <%--<asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                            <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
                                <asp:ListItem Selected="True" Value="5">5</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="35">35</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="45">45</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                            </asp:DropDownList>--%>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="dvBottom" runat="server" style="overflow: hidden;">
                        <asp:GridView ID="ClaimSearchGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" SkinID="DetailPageGridView" EnableViewState="true">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField Visible="True" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radioSelect" runat="server"  GroupName = "claimNum" 
                                        onclick = "SetUniqueRadioButton(this)" value='<%# Eval("claim_id") %>'>
                                        </asp:RadioButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CLAIM_NUMBER">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimNumber" runat="server"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STATUS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CUSTOMER_NAME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SERVICE_CENTER">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceCenter" runat="server"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AUTHORIZATION_NUMBER">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAuthorizationNumber" runat="server"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AUTHORIZATION_AMOUNT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAuthorizedAmount" runat="server"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimId" runat="server"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                        </asp:GridView>                   
                <div id = "btnDiv"  runat="server" class = "btnZone" style="margin: 0px 0px 0px 10px;">
                     <asp:Button ID="btnNewItemAdd" runat="server"  Text="ADD" SkinID="PrimaryRightButton" />
                     <asp:Button ID="btnNewItemCancel" runat="server" Text="CANCEL" SkinID="AlternateRightButton" />
                </div>                               
            </div> 
            </div>          
          </asp:Panel>
    </div>
    <div id="workQueueRedirectReason">
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopupRedirect" TargetControlID="LinkButton2" 
            PopupControlID="pnlPopupRedirect" DropShadow="True" BackgroundCssClass="ModalBackground"
            CancelControlID="LinkButton2" BehaviorID="addNewModalRedirect" PopupDragHandleControlID="pnlPopupRedirect" 
            RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopupRedirect" runat="server" Style="display: none;">
            <div id="lightRedirect" class="overlay_message_content" style="width: 500px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="inline"
                    ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <p class="modalTitle">
                            <asp:Label ID="lblRedirectModalTitle" runat="server"></asp:Label>
                            <%--<asp:ImageButton ImageUrl="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                                ID="btnRedirectClose" CssClass="floatR" AlternateText="Close" /> --%>
                        </p>
                        <div class="dataContainer" style="margin-bottom: 0px">
                        <div runat="server" id="modalMessageBoxRedirect" class="errorMsg" style="display: none;
                            width: 80%">
                            <p>
                                <img id="imgRedirectReasonMsg" width="16" height="13" align="middle" runat="server"
                                    src="~/App_Themes/Default/Images/icon_error.png" />
                                <asp:Literal runat="server" ID="msgRedirectReasons" />
                            </p>
                        </div>
                        </div>
                        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                           <tr>
                                <td>
                                    <span class="mandatory">*</span><asp:Label ID="lblQueueToRedirect" runat="server"> : </asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWorkQueueList" runat="server" AutoPostBack="true"  >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                            <tr id="trLblRedirectRsn" runat="server">
                                <td>
                                    <span class="mandatory">*</span><asp:Label ID="lblRedirectReasons" runat="server"> : </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rdbtRedirectRsn" runat="server" CssClass="formGrid" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlWorkQueueList" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnRedirectContinue" runat="server" CssClass="primaryBtn floatR"  Text="UPDATE" CausesValidation="false" />
                            <asp:Button ID="btnRedirectCancel" runat="server" CssClass="popWindowCancelbtn floatR" Text="CANCEL" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server"/>

</asp:Content>
