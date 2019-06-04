<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RepairAndLogisticsForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RepairAndLogisticsForm"
    EnableSessionState="True" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="UserControlAuthorizationInfo" Src="UserControlAuthorizationInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlLogisticalInfo" Src="~/Claims/UserControlLogisticalInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
      .repairTabs {
        overflow : hidden;
        height: 470px;
        width: 1050px;
        text-align: left;
        clear: both;
        display: inline-block; 
        border : #999 1px solid;
      }

      .repairTabs .ui-tabs-panel {
        padding: 0px;
      }

      hr {
        border: #999999 0.5px solid;           
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" class="summaryGrid">
        <tbody>
            <tr>
                <td align="right">
                    <asp:Label ID="LabelClaimNumber" runat="server" SkinID="SummaryLabel">Claim_Number:</asp:Label>
                </td>
                <td align="left" class="bor" style="font-weight: bold" runat="server" id="ClaimNumberTD">
                    <strong></strong>
                </td>
                <td align="right">
                    <asp:Label ID="LabelClaimStatus" runat="server" SkinID="SummaryLabel">Claim_Status:</asp:Label>
                </td>
                <td align="left" class="bor" style="font-weight: bold" runat="server" id="ClaimStatusTD">
                    <strong></strong>
                </td>
            </tr>
            <tr>
                <td style="width: 1%" valign="middle" nowrap align="right">
                    <asp:Label ID="LabelVerificationNumber" runat="server">Verification_Number:</asp:Label>
                </td>
                <td valign="middle" align="left" width="50%">
                    <asp:TextBox ID="TextboxVerificationNumber" TabIndex="-1" runat="server" Width="93%"
                        CssClass="FLATTEXTBOX"></asp:TextBox>
                </td>
                <td style="width: 1%" valign="middle" nowrap align="right">
                    <asp:Label ID="LabelMaxAuthorizedAmount" runat="server">Max_Authorized_Amount:</asp:Label>
                </td>
                <td valign="middle" align="left" width="50%">
                    <asp:TextBox ID="TextboxMaxAuthorizedAmount" TabIndex="-1" runat="server" Width="50%"
                        CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:Panel ID="WorkingPanel" runat="server">
        <div class="dataContainer">
            <table border="0">
                <tr>
                    <td valign="top" colspan="4">
                        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                         <div id="tabs" class="style-tabs repairTabs">
                            <ul>
                                <li><a href="#tabsGeneralInformation">
                                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">General_Information</asp:Label></a></li>
                                <li><a href="#tabsReplacedDevice">
                                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">Replaced_Device</asp:Label></a></li>
                                <li><a href="#tabsLOGISTICALINFORMATION">
                                    <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">LOGISTICAL_INFORMATION</asp:Label></a></li>
                            </ul>

                            <div id="tabsGeneralInformation">
                                <div class="Page" runat="server" id="ViewPanel_READ" style="display: block; height: 470px; overflow: auto">
                                    <table id="Table1" class="formGrid" border="0" width="95%">
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelCustomerName" runat="server">Customer_Name:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                                <asp:TextBox ID="TextboxCustomerName" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelPOS" runat="server">POS:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                                <asp:TextBox ID="TextboxPOS" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelCoverageType" runat="server">Coverage_Type:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                                <asp:TextBox ID="TextboxCoverageType" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelDateOfClaim" runat="server">Date_Of_Claim:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                                <asp:TextBox ID="TextboxDateOfClaim" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">&nbsp;
                                            </td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                            </td>
                                        </tr>
                                        <asp:Panel runat="server" ID="pnlNoUseClaimAuthorization">
                                            <tr>
                                                <td style="width: 1%" valign="middle" nowrap align="right">
                                                    <asp:Label ID="LabelServiceCenter" runat="server">Service_Center:</asp:Label>
                                                </td>
                                                <td valign="middle" align="left" width="50%">&nbsp;
                                                    <asp:TextBox ID="TextboxServiceCenter" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                                <td valign="middle" align="left" width="50%">&nbsp;
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelCurrentProductCode" runat="server">Current_Product_Code:</asp:Label>
                                            </td>
                                            <td style="height: 17px" valign="middle" align="left" width="50%">&nbsp;
                                                <asp:TextBox ID="TextCurrentProductCode" TabIndex="-1" runat="server" Width="93%"
                                                    CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right"></td>
                                            <td style="height: 17px" valign="middle" align="left" width="50%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="left">
                                                <asp:Label ID="LabelInfoOfClaimedDevice" runat="server">Information_Of_Claimed_Device</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelManufacturer" runat="server">Manufacturer:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxManufacturer" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelModel" runat="server">Model:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                                <asp:TextBox ID="TextboxModel" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelSKU" runat="server">SKU:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxSKU" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelSerialNumberIMEI" runat="server">Serial_Number:</asp:Label>
                                                <asp:Label ID="LabelSerialNumber" runat="server">Serial_No_Label:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxSerialNumber" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX_CURRENCY"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left">&nbsp;
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelIMEINumber" runat="server">IMEI_Number:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxIMEINumber" TabIndex="-1" runat="server" Width="93%" CssClass="FLATTEXTBOX_CURRENCY"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" align="right">
                                                <br />
                                                <asp:Label ID="LabelProblemDescription" runat="server">Problem_Description:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" colspan="3">&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="TextboxProblemDescription" Style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                                                    TabIndex="4" runat="server" Width="95%" CssClass="FLATTEXTBOX" TextMode="MultiLine"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" colspan="4">
                                                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                                                    <Columns>
                                                        <asp:TemplateField SortExpression="REPLACEMENT_PART_ID" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ReplacementPartIdLabel" runat="server" Visible="True" Text='<%# GetGuidStringFromByteArray(Container.DataItem("replacement_part_id"))%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Replaced_Parts_SKU" HeaderText="Replaced_Parts_SKU">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ReplacedPartsSkuLabel" runat="server" Visible="True" Text='<%# Container.DataItem("replaced_parts_sku")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Replaced_Parts_Description" HeaderText="Replaced_Parts_Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ReplacedPartsDescLabel" runat="server" Visible="True" Text='<%# Container.DataItem("replaced_parts_desc")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkAuthDetails" runat="server" Text="AUTHORIZATION_DETAILS"></asp:LinkButton>
                                                <div>
                                                    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="lnkAuthDetails"
                                                        PopupControlID="pnlUseClaimAuthorization" DropShadow="True" BackgroundCssClass="ModalBackground"
                                                        CancelControlID="btnCancel" BehaviorID="addNewModal" PopupDragHandleControlID="pnlUseClaimAuthorization"
                                                        Y="50" RepositionMode="RepositionOnWindowScroll">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <asp:Panel runat="server" ID="pnlUseClaimAuthorization" Style="display: none;">
                                                        <div id="lightRedirect" class="overlay_message_content">
                                                            <p class="modalTitle">
                                                                <asp:Label ID="lblModalTitle" runat="server" Text="AUTHORIZATION_DETAILS"></asp:Label>
                                                            </p>
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right" width="25%">
                                                                            <asp:Label ID="AuthorizationNumberLabel" runat="server" SkinID="SummaryLabel">AUTHORIZATION_NUMBER</asp:Label>:
                                                                        </td>
                                                                        <td align="left" width="25%" style="font-weight: bold" runat="server"
                                                                            id="AuthorizationNumberTD">
                                                                            <strong></strong>
                                                                        </td>
                                                                        <td align="right" width="25%">
                                                                            <asp:Label runat="server" ID="StatusLabel" SkinID="SummaryLabel">AUTHORIZATION_STATUS</asp:Label>:
                                                                        </td>
                                                                        <td align="left" width="25%" style="font-weight: bold" runat="server"
                                                                            id="AUTHORIZATIONStatusTD">
                                                                            <strong></strong>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label align="left" runat="server" ID="ServiceCenterNameLabel">SERVICE_CENTER_NAME</asp:Label>:
                                                                        </td>
                                                                        <td align="left" style="font-weight: bold" runat="server" id="ServiceCenterTD"></td>
                                                                        <td colspan="2"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="center" colspan="3"></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <Elita:UserControlAuthorizationInfo ID="moClaimAuthorizationInfoController" runat="server"
                                                                align="center" />
                                                            <div class="btnZone">
                                                                <div class="">
                                                                    <asp:LinkButton ID="btnCancel" runat="server" SkinID="AlternateRightButton" Text="Close" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div id="tabsReplacedDevice">
                                <div class="Page" runat="server" id="moReplacedDevicePanel" style="display: block; height: 470px; overflow: auto">
                                    <table id="Table3" class="formGrid" border="0">
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelDeviceReceptionDate" runat="server">Device_Reception_Date:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" width="50%">&nbsp;
                                                <asp:TextBox ID="TextboxDeviceReceptionDate" TabIndex="-1" runat="server" Width="50%"
                                                    CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelReplacementType" runat="server">REPLACEMENT_TYPE:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxReplacementType" TabIndex="-1" runat="server" Width="50%"
                                                    CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelManufacturerOfReplacedDevice" runat="server">Manufacturer_Of_Replaced_Device:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxManufacturerOfReplacedDevice" TabIndex="-1" runat="server"
                                                    Width="93%" CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelTotalLaborAmount" runat="server">Total_Labor_Amount:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxTotalLaborAmount" TabIndex="-1" runat="server" Width="125"
                                                    CssClass="FLATTEXTBOX_CURRENCY" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelModelOfReplacedDevice" runat="server">Model_Of_Replaced_Device:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxModelOfReplacedDevice" TabIndex="-1" runat="server" Width="93%"
                                                    CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelTotalPartsAmount" runat="server">Total_Parts_Amount:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxTotalPartsAmount" TabIndex="-1" runat="server" Width="125"
                                                    CssClass="FLATTEXTBOX_CURRENCY" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelSerialNrOfReplacedDevice" runat="server">Serial_Nr_Of_Replaced_Device:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxSerialNrOfReplacedDevice" TabIndex="-1" runat="server" Width="93%"
                                                    CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelServiceCharge" runat="server">Service_Charge:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxServiceCharge" TabIndex="-1" runat="server" Width="125" CssClass="FLATTEXTBOX_CURRENCY"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelIMEIOfReplacedDevice" runat="server">IMEI_Nr_Of_Replaced_Device:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxIMEIOfReplacedDevice" TabIndex="-1" runat="server" Width="93%"
                                                    CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelSKUOfReplacedDevice" runat="server">SKU_Of_Replaced_Device:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxSKUOfReplacedDevice" TabIndex="-1" runat="server" Width="93%"
                                                    CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelShippingAmount" runat="server">Shipping_Amount:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxShippingAmount" TabIndex="-1" runat="server" Width="125"
                                                    CssClass="FLATTEXTBOX_CURRENCY" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left">&nbsp;
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                <asp:Label ID="LabelServiceLevel" runat="server">Service_Level:</asp:Label>
                                            </td>
                                            <td valign="middle" align="left">&nbsp;
                                                <asp:TextBox ID="TextboxServiceLevel" TabIndex="-1" runat="server" Width="50%" CssClass="FLATTEXTBOX"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" valign="middle" nowrap align="right"></td>
                                            <td valign="middle" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%" valign="middle" align="right">
                                                <br />
                                                <asp:Label ID="LabelProblemFound" runat="server">Problem_Found</asp:Label>:
                                            </td>
                                            <td style="width: 83%" valign="middle" align="left" colspan="3">&nbsp;
                                                <asp:TextBox ID="TextboxProblemFound" Style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                                                    TabIndex="4"
                                                    runat="server" Width="93%" CssClass="FLATTEXTBOX" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div id="tabsLOGISTICALINFORMATION">
                                <div class="Page">
                                    <div id="Div7" runat="server">
                                        <Elita:UserControlLogisticalInfo runat="server" ID="ClaimLogisticalInfo"></Elita:UserControlLogisticalInfo>
                                    </div>
                                </div>
                            </div>
                        
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <hr>
                    </td>
                </tr>
                <tr>
                    <td valign="bottom" nowrap align="left" height="10" width="10%">
                    </td>
                    <td valign="bottom" nowrap align="left" height="10" width="90%">
                    </td>
                </tr>
            </table>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                runat="server" /><input id="HiddenLimitExceeded" type="hidden" name="HiddenLimitExceeded"
                    runat="server" />
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back">
                </asp:Button>&nbsp;
                <asp:Button ID="btnEdit_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Edit">
                </asp:Button>&nbsp;
                <asp:Button ID="btnSave_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Save">
                </asp:Button>&nbsp;
                <asp:Button ID="btnUndo_Write" runat="server" SkinID="AlternateLeftButton" Text="Undo">
                </asp:Button>&nbsp;
                <asp:Button ID="btnChangeCoverage" runat="server" SkinID="AlternateLeftButton" Text="CHANGE_COVERAGE">
                </asp:Button>&nbsp;
            </div>
        </div>
    </asp:Panel>
</asp:Content>
