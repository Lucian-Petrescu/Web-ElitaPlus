
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EndorsementForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.EndorsementForm" 
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>

<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
<script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <uc1:UserControlCertificateInfo ID="moCertificateInfoController" runat="server">
            </uc1:UserControlCertificateInfo>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            if ($("[id$='btnAdd_WRITE']").attr('disabled') == 'disabled') {
                if ($("[id$='BtnSaveCov_WRITE']").attr('disabled') == 'disabled')
                {
                    $("[id$='WorkingPanel']").find('*').attr('disabled', 'disabled');
                    $('.btnZone').removeAttr('disabled');
                    $("[id$='btnBack']").removeAttr('disabled');
                }
            }
        });

    </script>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js"></asp:ScriptReference>
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <asp:Panel ID="WorkingPanel" runat="server" Height="100%" Width="100%">
            <table id="Table5" class="formGrid" style="height: 84px" cellspacing="1" cellpadding="1" width="90%" align="center" border="0">
                <tr>
                    <td valign="middle" align="right" width="1%" colspan="1" height="20" rowspan="1">
                        <asp:Label ID="LabelCustomerName" runat="server" Font-Bold="false">CUSTOMER_NAME</asp:Label>&nbsp;
                    </td>
                    <td valign="middle" width="50%" colspan="1" height="20" rowspan="1">
                        <asp:TextBox ID="TextboxCustomerName" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    </td>
                    </tr><tr runat="server" id="moCustName1">
                                    <td align="right">
                                        <asp:Label ID="moCustomerFirstNameLabel" runat="server" Visible="false">CUSTOMER_FIRST_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCustomerFirstNameText" TabIndex="1" runat="server" Visible="false" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                        <td align="right">
                                            <asp:Label ID="moCustomerMiddleNameLabel" runat="server" Visible="false">CUSTOMER_MIDDLE_NAME</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="moCustomerMiddleNameText" TabIndex="1" runat="server" Visible="false" SkinID="MediumTextBox"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr runat="server" id="moCustName2">
                                    <td align="right">
                                        <asp:Label ID="moCustomerLastNameLabel" runat="server" Visible="false">CUSTOMER_LAST_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCustomerLastNameText" TabIndex="1" runat="server" Visible="false" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                    </tr> 
                    <td valign="middle" align="right" nowrap="nowrap" width="1%" colspan="1" height="20" rowspan="1">
                        <asp:Label ID="LabelHomePhone" runat="server" Font-Bold="false">HOME_PHONE</asp:Label>&nbsp;
                    </td>
                    <td valign="middle" width="50%" height="20">
                        <asp:TextBox ID="TextboxHomePhone" TabIndex="3" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                    </td>
                
                <tr>
                    <td valign="middle" align="right" colspan="1" height="20" rowspan="1">
                        <asp:Label ID="LabelEmailAddress" runat="server" Font-Bold="false">EMAIL</asp:Label>&nbsp;
                    </td>
                    <td valign="middle" height="20">
                        <asp:TextBox ID="TextboxEmailAddress" TabIndex="2" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td valign="middle" align="right" colspan="1" height="20" rowspan="1" nowrap="nowrap">
                        <asp:Label ID="LabelWorkPhone" runat="server" Font-Bold="false">WORK_PHONE</asp:Label>&nbsp;
                    </td>
                    <td valign="middle" height="20">
                        <asp:TextBox ID="TextboxWorkPhone" TabIndex="4" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right" nowrap="nowrap" colspan="1" height="20" rowspan="1">
                        <asp:Label ID="LabelLangPref" runat="server" Font-Bold="false">LANGUAGE_PREF</asp:Label>&nbsp;
                    </td>
                    <td valign="middle" height="20">
                        <asp:TextBox ID="TextboxLangPref" TabIndex="5" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        <asp:DropDownList ID="moLangPrefDropdown" SkinID="SmallDropDown" TabIndex="5" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <uc1:UserControlAddress ID="moAddressControllerEndorsement" runat="server"></uc1:UserControlAddress>
                <asp:Panel ID="moTaxIdPanel" runat="server" Height="100%" Width="100%">
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="moDocumentTypeLabel" runat="server">DOCUMENT_TYPE</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moDocumentTypeText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                            <asp:DropDownList ID="cboDocumentTypeId" runat="server" SkinID="SmallDropDown" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="moNewTaxIdLabel" runat="server">DOCUMENT_NUMBER</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moNewTaxIdText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="moIDTypeLabel" runat="server">ID_TYPE</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moIDTypeText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="moRGNumberLabel" runat="server">RG_NUMBER</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moRGNumberText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="moDocumentAgencyLabel" runat="server">DOCUMENT_AGENCY</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moDocumentAgencyText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="moDocumentIssueDateLabel" runat="server" Width="100%">DOCUMENT_ISSUE_DATE</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moDocumentIssueDateText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonDocumentIssueDate" runat="server" SkinID="ImageButton" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" colspan="4">
                            <hr style="height: 1px">
                        </td>
                    </tr>
                </asp:Panel>
                <tr id="moWarrantyInformation1" runat="server">
                    <td align="right" width="13%" colspan="1" height="20">
                        <asp:Label ID="LabelManufacturerTerm" runat="server" Font-Bold="false">MANUFACTURER_TERM</asp:Label>
                    </td>
                    <td height="20" width="37%">
                        <asp:TextBox ID="TextboxManufacturerTerm" TabIndex="45" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        &nbsp;<asp:Label ID="LabelClaimsExist" runat="server" Font-Bold="False" ForeColor="#CC0000" Visible="False">CLAIMS_EXIST</asp:Label>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr id="moWarrantyInformation11" runat="server">
                    <td align="right" width="13%" colspan="1" height="20">
                        <asp:Label ID="LabelWarrantySalesDate" runat="server" Font-Bold="false">WARRANTY_SALES_DATE</asp:Label>&nbsp;
                    </td>
                    <td height="20" width="37%">
                        <asp:TextBox ID="TextboxWarrantySalesDate" TabIndex="60" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        <asp:ImageButton ID="ImageButtonWarrantySaleDate" runat="server" SkinID="ImageButton" ImageUrl="../Common/Images/calendarIcon2.jpg">
                        </asp:ImageButton>
                    </td>
                </tr>
                <tr id="moWarrantyInformation2" runat="server">
                    <td valign="middle" align="right" colspan="1" height="20" rowspan="1">
                        <asp:Label ID="LabelProductSaleDate" runat="server" Font-Bold="false">PRODUCT_SALES_DATE</asp:Label>&nbsp;
                    </td>
                    <td valign="middle" height="20">
                        <asp:TextBox ID="TextboxProductSaleDate" TabIndex="65" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        <asp:ImageButton ID="ImageButtonProductSaleDate" runat="server" ImageAlign="Middle" SkinID="ImageButton" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr id="moWarrantyInformation3" runat="server">
                    <td valign="middle" align="right" colspan="1" height="20" rowspan="1">
                        <asp:Label ID="LabelSalesPrice" runat="server" Font-Bold="false" Width="116px">SALES_PRICE</asp:Label>&nbsp;
                    </td>
                    <td valign="middle" height="20">
                        <asp:TextBox ID="TextboxSalesPrice" TabIndex="70" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr id="moWarrantyInformation4" runat="server">
                    <td valign="middle" colspan="4">
                        <hr style="height: 1px">
                    </td>
                </tr>
                <asp:Panel ID="pnlCovEdit" runat="server" Height="100%" Width="100%" Visible="True">
                    <tr>
                        <td colspan="4" valign="top">                            
                            <div id="tabs" class="style-tabs">
                              <ul>
                                <li><a href="#tabsCoverages" rel="noopener noreferrer"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Coverages</asp:Label></a></li>
                              </ul>
          
                                <div id="tabsCoverages">
                                  <table id="tblOpportunities" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="2" cellpadding="2" rules="cols" width="100%" border="0">
                                        <tr>
                                            <td align="middle" colspan="4">
                                                <asp:GridView ID="grdCoverages" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCreated="RowCreated" OnRowCommand="RowCommand" AllowPaging="false" AllowSorting="false" SkinID="DetailPageGridView">
                                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                                    <RowStyle Wrap="False"></RowStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="EditButton_WRITE" runat="server" ImageUrl="~/Navigation/images/edit.png" CommandName="EditRecord" SkinID="ImageButton" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("CERT_ITEM_COVERAGE_ID"))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="risk_type_description" HeaderText="Risk_Type">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRiskType" runat="server" Visible="True" Text='<%# Container.DataItem("risk_type_description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="coverage_type_description" HeaderText="Coverage_Type">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="false"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCoverageType" runat="server" Visible="True" Text='<%# Container.DataItem("coverage_type_description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Begin_Date" HeaderText="Begin_Date">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBeginDate" runat="server" Visible="True" Text='<%# Me.GetDateFormattedString(CType(Container.DataItem("Begin_Date"), Date))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtBeginDate" SkinID="SmallTextBox" runat="server"></asp:TextBox>
                                                                <asp:ImageButton ID="moBeginDateImage" runat="server" SkinID="ImageButton" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="false"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="End_Date" HeaderText="End_Date">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="false"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndDate" runat="server" Visible="True" Text='<%# Me.GetDateFormattedString(CType(Container.DataItem("end_Date"), Date))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings PageButtonCount="15" Mode="Numeric"></PagerSettings>
                                                    <PagerStyle HorizontalAlign="Center"></PagerStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="BtnSaveCov_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save">
                                                </asp:Button>&nbsp;&nbsp;<asp:Button ID="BtnCancelCov" runat="server" SkinID="PrimaryLeftButton" Text="Cancel">
                                                </asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td style="height: 5px;">
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtSaleDate" runat="server" SkinID="SmallTextBox" Visible="False"></asp:TextBox>
            <div class="btnZone">
                <asp:Button ID="btnBack" TabIndex="185" runat="server" SkinID="PrimaryLeftButton" Text="Back"></asp:Button>&nbsp;
                <asp:Button ID="btnAdd_WRITE" runat="server" SkinID="PrimaryRightButton" TabIndex="190" Text="Save"></asp:Button>&nbsp;
                <asp:Button ID="btnEdit_WRITE" runat="server" SkinID="AlternateRightButton" TabIndex="200" Font-Bold="false" Text="Edit" CausesValidation="False"></asp:Button>&nbsp;
                <asp:Button ID="btnUndo_Write" TabIndex="195" runat="server" SkinID="AlternateRightButton" Text="Undo"></asp:Button>
            </div>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261">
            <input id="hdOrinBeginDate" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261">
        </asp:Panel>
    </div>
</asp:Content>
e" TabIndex="195" runat="server" SkinID="AlternateRightButton"
                    Text="Undo"></asp:Button>
            </div>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                runat="server" designtimedragdrop="261" />
            <input id="hdOrinBeginDate" type="hidden" name="HiddenSaveChangesPromptResponse"
                runat="server" designtimedragdrop="261" />
        </asp:Panel>
    </div>
</asp:Content>
