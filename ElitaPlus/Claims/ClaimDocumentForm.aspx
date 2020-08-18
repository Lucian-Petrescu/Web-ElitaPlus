<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimDocumentForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimDocumentForm" Theme="Default"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblCustomerName" runat="server" SkinID="SummaryLabel">CUSTOMER_NAME</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" colspan="5"> 
                <asp:Label ID="lblCustomerNameValue" runat="server" Font-Bold="true"><%#NO_DATA%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblClaimNumber" runat="server" SkinID="SummaryLabel">CLAIM_#</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblClaimNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblDealerName" runat="server" SkinID="SummaryLabel">DEALER_NAME</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblDealerNameValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblClaimStatus" runat="server" SkinID="SummaryLabel">CLAIM_STATUS</asp:Label>:
            </td>
            <td id="ClaimStatusTD" runat="server" align="left" nowrap="nowrap" class="padRight">
                <asp:Label ID="lblClaimStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblWorkPhoneNumber" runat="server" SkinID="SummaryLabel">WORK_CELL_NUMBER</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblWorkPhoneNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblDealerGroup" runat="server" SkinID="SummaryLabel">DEALER_GROUP</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblDealerGroupValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblSubscriberStatus" runat="server" SkinID="SummaryLabel">SUBSCRIBER_STATUS</asp:Label>:
            </td>
            <td id="SubStatusTD" align="left" nowrap="nowrap" class="padRight" runat="server">
                <asp:Label ID="lblSubscriberStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblCertificateNumber" runat="server" SkinID="SummaryLabel">CERTIFICATE_NUMBER</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblCertificateNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblSerialNumberImei" runat="server" SkinID="SummaryLabel">SERIAL_NUMBER_IMEI</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblSerialNumberImeiValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblDateOfLoss" runat="server" SkinID="SummaryLabel">DATE_OF_LOSS</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="padRight">
                <asp:Label ID="lblDateOfLossValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="lblGrdHdr"></asp:Label>
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
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <input id="HiddenIsDeleteImagesAllowed" type="hidden" name="HiddenIsDeleteImagesAllowed" runat="server">
        <asp:GridView ID="ClaimDocumentsGridView" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" SkinID="DetailPageGridView" EnableViewState="true" OnRowCommand="ClaimDocumentsGridView_RowCommand">
            <SelectedRowStyle Wrap="True" />
            <EditRowStyle Wrap="True" />
            <AlternatingRowStyle Wrap="True" />
            <RowStyle Wrap="True" />
            <HeaderStyle />
            <Columns>
                <asp:TemplateField HeaderText="FILE_NAME" SortExpression="FILE_NAME">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnImageLink" CommandName="SelectActionImage"
                            Text="Image Link"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FILE_TYPE" SortExpression="FILE_TYPE" ReadOnly="true"
                    HtmlEncode="false" HeaderText="FILE_TYPE" HeaderStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="SIZE" SortExpression="FILE_SIZE_BYTES">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="FileSizeLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SCAN_DATE" SortExpression="SCAN_DATE" ReadOnly="true"
                    HtmlEncode="false" HeaderText="SCAN_DATE" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="USER_NAME" SortExpression="USER_NAME" ReadOnly="true"
                    HtmlEncode="false" HeaderText="USER_NAME" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DOCUMENT_TYPE" SortExpression="DOCUMENT_TYPE" ReadOnly="true"
                    HtmlEncode="false" HeaderText="DOCUMENT_TYPE" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="STATUS" SortExpression="SCAN_DATE" ReadOnly="true" HtmlEncode="false"
                    HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="COMMENTS" ReadOnly="true" HtmlEncode="false" HeaderText="COMMENTS"
                    HeaderStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="ADD_REMOVE_IMAGE">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnAddRemoveImage"
                            Text="Image Link"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
            <PagerStyle />
        </asp:GridView>
    </div>
    <asp:Panel runat="server" ID="AddImagePanel" CssClass="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="AddImageHealder">ADD_IMAGE</asp:Label>
        </h2>
        <div class="stepformZone">
            <table width="100%" class="formGrid" border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label runat="server" ID="DocumentTypeLabel" Text="DOCUMENT_TYPE"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList runat="server" ID="DocumentTypeDropDown" SkinID="MediumDropDown" />
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label runat="server" ID="ScanDateLabel" Text="SCAN_DATE"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox runat="server" ID="ScanDateTextBox" ReadOnly="true" SkinID="MediumTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label runat="server" ID="FileNameLabel" Text="FileName"></asp:Label>
                        </td>
                        <td colspan="3" nowrap="nowrap">
                            <input id="ImageFileUpload" style="width: 80%" type="file" name="ImageFileUpload"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label runat="server" ID="CommentLabel" Text="COMMENT"></asp:Label>
                        </td>
                        <td colspan="3" nowrap="nowrap">
                            <asp:TextBox runat="server" ID="CommentTextBox" Width="80%" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btnZone">
                                <asp:LinkButton ID="ClearButton" runat="server" SkinID="AlternateRightButton" Text="Cancel"></asp:LinkButton>
                                <asp:Button ID="AddImageButton" runat="server" SkinID="PrimaryLeftButton" Text="Add_Image"></asp:Button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" class="altBtn" Text="Back"></asp:Button>
    </div>
    <div id="modalClaimImages" class="overlay">
        <div id="Div3" class="overlay_message_content" style="width: 1100px; left: 8%">
            <p class="modalTitle">
                <asp:Label ID="lblClaimImage" runat="server" Text="CLAIM_IMAGE"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('modalClaimImages');">
                    <img id="img3" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a></p>
            <iframe class="pdfContainer" align="left" runat="server" id="pdfIframe"></iframe>
        </div>
        <div id="Div5" class="black_overlay">
        </div>
    </div>
</asp:Content>
