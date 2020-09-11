<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimIssueDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimIssueDetailForm" Theme="Default"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="~/Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlWizard" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" Assembly="Assurant.ElitaPlus.WebApp" Namespace="Assurant.ElitaPlus.ElitaPlusWebApp" %>
<%@ Reference Control ="~/Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register assembly="Microsoft.Web.UI.WebControls" namespace="Microsoft.Web.UI.WebControls" tagprefix="iewc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <tr>
                <td align="right" nowrap="nowrap" id="tdClaimNumber">
                    <asp:Label ID="lblClaimNumber"  runat="server" SkinID="SummaryLabel">CLAIM_#:</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:Label ID="Label_ClaimNumber" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" id="tdIssueCode">
                    <asp:Label ID="lblIssueCode" runat="server" SkinID="SummaryLabel">ISSUE_CODE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap" class="bor padRight">
                    <asp:Label ID="Label_IssueCode" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
                <td align="right" nowrap="nowrap" class="padLeft" id="tdCreatedBy">
                    <asp:Label ID="lblCreatedBy" runat="server" SkinID="SummaryLabel">CREATED_BY</asp:Label>
                </td>
                <td align="left" nowrap="nowrap" class="bor padRight">
                    <asp:Label ID="Label_CreatedBy" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
                <td align="right" nowrap="nowrap" class="padLeft" id="tdProcessedBy">
                    <asp:Label ID="lblProcessedBy" runat="server" SkinID="SummaryLabel">PROCESSED_BY</asp:Label>
                </td>
                <td align="left" nowrap="nowrap" class="bor padRight">
                    <asp:Label ID="Label_ProcessedBy" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
                <td align="right" runat="server" nowrap="nowrap" class="padLeft" id="tdIssueStatus">
                    <asp:Label ID="lblIssueStatus" runat="server" SkinID="SummaryLabel">ISSUE_STATUS</asp:Label>
                </td>
                <td align="left" runat="server" nowrap="nowrap" class="padRight" id="tdLabelIssueStatus">
                    <asp:Label ID="Label_IssueStatus" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" id="tdIssueDescription">
                    <asp:Label ID="lblIssueDescription" runat="server" SkinID="SummaryLabel">ISSUE_DESCRIPTION</asp:Label>
                </td>
                <td align="left" nowrap="nowrap" class="bor padRight">
                    <asp:Label ID="Label_IssueDescription" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
                <td align="right" nowrap="nowrap" class="padLeft" id="tdCreatedDate">
                    <asp:Label ID="lblCreatedDate" runat="server" SkinID="SummaryLabel">CREATED_DATE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap" class="bor padRight">
                    <asp:Label ID="Label_CreatedDate" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
                <td align="right" id="tdProcessedDate" nowrap="nowrap" class="padLeft">
                    <asp:Label ID="lblProcessedDate" runat="server" SkinID="SummaryLabel">PROCESSED_DATE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap" class="bor padRight">
                    <asp:Label ID="Label_ProcessedDate" runat="server" SkinID="SmallTextBox"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script type="text/javascript">
        function validate(ctrlRadioButtonList,ctrlCommentTextbox,ctrlMessageBox)
        {
            var flag = false;
            var lstRadioButtons = document.getElementById(ctrlRadioButtonList);
            var inputs = lstRadioButtons.getElementsByTagName("input");
            for (var x = 0; x < inputs.length; x++)
            {
                if (inputs[x].checked)
                {
                    flag = true;
                    var hdnSelectedReason = document.getElementById('<%=hdnSelectedReason.ClientID %>');
                    hdnSelectedReason.value = inputs[x].value;
                }
            }

            if (flag == true)
            {
                var txtComment = document.getElementById(ctrlCommentTextbox)
                if (txtComment.value.length == 0)
                {
                    flag = false;
                }  
            }

            if(flag == false)
            {
                var msgBox = document.getElementById(ctrlMessageBox);
                msgBox.style.display = 'block';
                return false;
            }
           
            return true;
        }

        function HideErrorAndModal(divId,ctrlradionButtonList,ctrlMessageBox,ctrlComments)
        {
            var msgBox = document.getElementById(ctrlMessageBox);
            msgBox.style.display = 'none';
            var txtComment = document.getElementById(ctrlComments);
            txtComment.value = '';
            var lstRadioButtons = document.getElementById(ctrlradionButtonList);
            var inputs = lstRadioButtons.getElementsByTagName("input");
            for (var x = 0; x < inputs.length; x++)
            {
                if (inputs[x].checked)
                {
                    inputs[x].checked = false;
                }
            }
            hideModal(divId);
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
    <div id="ModalReopenIssue" class="overlay">
        <div id="light" class="overlay_message_content" style="width: 500px">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="REOPEN_ISSUE"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalReopenIssue');" rel="noopener noreferrer">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <div class="dataContainer" style="margin-bottom:0px">
                <div runat="server" id="modalMessageBoxReopen" class="errorMsg" style="display: none;width:80%">
                    <p>
                        <img id="imgIssueMsg" width="16" height="13" align="middle" runat="server" src="~/App_Themes/Default/Images/icon_error.png" />
                        <asp:Literal runat="server" ID="MessageLiteral" />
                    </p>
                </div>
            </div>
            
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                     <span class="mandatory">*</span><asp:Label ID="lblReopenReason" runat="server" Text="REASON_REOPEN_ISSUE"></asp:Label>:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rdbtlstIssueReopen" runat="server" CssClass="formGrid" />
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                     <span class="mandatory">*</span><asp:Label ID="lblComments" runat="server" Text="COMMENTS"></asp:Label>:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtComments" runat="server" SkinID="MediumTextBox" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td id="tdBtnArea" nowrap="nowrap" runat="server" colspan="2">
                        <asp:Button ID="btnReopenContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                        <input id='btnReopenCancel' runat="server" type="button" name="Cancel" value="Cancel" onclick="HideErrorAndModal('ModalReopenIssue');"
                            class='popWindowCancelbtn floatR' />
                    </td>
                </tr>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
     <div id="ModalWaiveIssue" class="overlay">
        <div id="light" class="overlay_message_content" style="width: 500px">
            <p class="modalTitle">
                <asp:Label ID="Label1" runat="server" Text="WAIVE_ISSUE"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalWaiveIssue');" rel="noopener noreferrer">
                    <img id="Img2" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <div class="dataContainer" style="margin-bottom:0px">
                <div runat="server" id="modalMessageBoxWaive" class="errorMsg" style="display: none;width:80%">
                    <p>
                        <img id="img3" width="16" height="13" align="middle" runat="server" src="~/App_Themes/Default/Images/icon_error.png" />
                        <asp:Literal runat="server" ID="msgSelectwaiveReason" />
                    </p>
                </div>
            </div>
            
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                      <span class="mandatory">*</span><asp:Label ID="lblReasonWaiveIssue" runat="server" Text="REASON_WAIVE_ISSUE"></asp:Label>:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rdbtRsnWaiveIss" runat="server" CssClass="formGrid" />
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                     <span class="mandatory">*</span><asp:Label ID="lblWaiveComments" runat="server" Text="COMMENTS"></asp:Label>:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtWaiveComments" runat="server" SkinID="MediumTextBox" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td id="td1" nowrap="nowrap" runat="server" colspan="2">
                        <asp:Button ID="btnWaiveContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue"/>
                        <input id='btnWaiveCancel' runat="server" type="button" name="Cancel" value="Cancel" onclick="HideErrorAndModal('ModalWaiveIssue');"
                            class='popWindowCancelbtn floatR' />
                    </td>
                </tr>
            </table>
        </div>
        <div id="Div5" class="black_overlay">
        </div>
    </div>
    <!-- new layout start -->
    <div class="dataContainer">
        <div style="width: 100%">
           <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                <div id="tabs" class="style-tabs">
                    <ul>
                        <li><a href="#tabsQuestionDetail" rel="noopener noreferrer">
                            <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Questions</asp:Label></a></li>
                        <li><a href="#tabsProcessHistory" rel="noopener noreferrer">
                            <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">PROCESS_HISTORY</asp:Label></a></li>
                    </ul>

                    <div id="tabsQuestionDetail">
                        <asp:Panel ID="PanelQuestions" runat="server" Width="100%" Height="100%">
                            <div id="question width: 99.53%; height: 100%">
                                <table id="tblQuestionDetail" background="" border="0" cellpadding="2" cellspacing="2"
                                    rules="cols" style="width: 100%; height: 100%">
                                    <tr>
                                        <td align="left" valign="top">

                                            <contenttemplate>                                                                      
                                                                                  <asp:GridView ID="grdQuestions" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false" SkinID="DetailPageGridView" AllowSorting="False" EnableModelValidation="True">
                                                                                        <SelectedRowStyle Wrap="True" />
                                                                                        <EditRowStyle Wrap="True" />
                                                                                        <AlternatingRowStyle Wrap="True" />
                                                                                        <RowStyle Wrap="True" />
                                                                                        <HeaderStyle   />
                                                                                        <Columns>
                                                                                             <asp:TemplateField Visible="False">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIssueID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Issue_Id"))%>' Visible="False"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField Visible="False">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSoftQuestionID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Soft_Question_Id"))%>' Visible="False"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Question">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblQuestionDesc" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>                                                                  
                                                                                            <asp:TemplateField HeaderText="Value">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblEntityValue" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Answer">
                                                                                                <ItemTemplate>
                                                                                                    
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    
                                                                                                </EditItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                            
                                                                        </asp:GridView>
                                                                        </contenttemplate>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="btnZone">
                                                <div class="">
                                                    <asp:Button ID="btnProcess" runat="server" Text="Process" SkinID="PrimaryRightButton" Enabled="False"></asp:Button>
                                                    <asp:Button ID="btnWaive" runat="server" Text="Waive" CssClass="primaryBtn floatR" OnClientClick="return revealModal('ModalWaiveIssue');"></asp:Button>
                                                    <asp:Button ID="btnUndo" runat="server" Text="Undo" SkinID="AlternateRightButton" Enabled="False"></asp:Button>
                                                    <input id="btnReopen" runat="server" value="ReOpen" class="primaryBtn floatR" type="submit" visible="false"
                                                        onclick="return revealModal('ModalReopenIssue');" />

                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>

                    <div id="tabsProcessHistory">
                        <asp:Panel ID="PanelProcessesEditDetail" runat="server" Width="100%" Height="100%">
                            <div id="Div1" align="center" style="overflow: auto; width: 99.53%; height: 100%">
                                <table id="Table2" background="" border="0" cellpadding="2" cellspacing="2" rules="cols" style="width: 100%; height: 100%">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:GridView ID="grdProcessHistory" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="DetailPageGridView" AllowSorting="True" EnableModelValidation="True">
                                                <SelectedRowStyle Wrap="True" />
                                                <EditRowStyle Wrap="True" />
                                                <AlternatingRowStyle Wrap="True" />
                                                <RowStyle Wrap="True" />
                                                <HeaderStyle />
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClaimIssueID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Claim_Issue_Id"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClaimIssueStatusID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Claim_Issue_Status_C_Id"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssueStatusDesc" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblIssueStatusDesc" runat="server"></asp:Label>
                                                        </EditItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="comments" HeaderText="Comments" />
                                                    <asp:BoundField DataField="created_by_name" HeaderText="Processed By" />
                                                    <asp:BoundField DataField="created_date" HeaderText="Processed_Date" />
                                                </Columns>
                                                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                                <PagerStyle />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </div>                 
             <div class="btnZone">
                <div class="">
                    <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton">
                    </asp:Button>
                </div>                 
            </div>
        </div>
    </div>
    <!-- end new layout -->
    <asp:HiddenField ID="hdnSelectedReason" runat="server" /> 
</asp:Content>
