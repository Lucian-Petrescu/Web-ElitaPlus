<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertificateListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertificateListForm" 
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo.ascx" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

        function isNotDblClick()
        {
            if (window.latestClick != "clicked")
            {
                window.latestClick = "clicked";
                return true;
            } else
            {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td class="bor">
                <asp:Label ID="lblCertificateby" runat="server">CERTIFICATE_SEARCH_BY</asp:Label><br />
                <asp:DropDownList ID="CertificateSearchDropDown" runat="server" SkinID="SmallDropDown"
                    OnSelectedIndexChanged="CertificateSearchDropDown_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <br />
            </td>
            <td colspan="3"  width="85%">
                <table width="100%" border="0">
                    <asp:Panel ID="CERTNUM" runat="server" Visible="true">
                        <tr>
                            <td>
                                <asp:Label ID="moCertificateLabel" runat="server">Certificate</asp:Label><br />
                                <asp:TextBox ID="moCertificateText" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server">STATUS</asp:Label><br />
                                <asp:DropDownList ID="moStatusDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="moDealerLabel" runat="server">Dealer</asp:Label><br />
                                <asp:DropDownList ID="moDealerDrop" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="moCustomerNameLabel" runat="server">Customer_Name</asp:Label><br />
                                <asp:TextBox ID="moCustomerNameText" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="moAddressLabel" runat="server">Address</asp:Label>
                                <br />
                                <asp:TextBox ID="moAddressText" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="moZipLabel" runat="server">Zip</asp:Label><br />
                                <asp:TextBox ID="moZipText" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="moTaxIdLabel" runat="server">Tax ID</asp:Label>
                                <br />
                                <asp:TextBox ID="moTaxIdText" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceNum" runat="server">INVOICE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="moInvoiceNum" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblAcctNum" runat="server">ACCOUNT_NUMBER</asp:Label><br />
                                <asp:TextBox ID="moAccountNum" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcboSearchSortBy" runat="server">Sort By</asp:Label><br />
                                <asp:DropDownList ID="cboSortBy" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblInforceCert" runat="server">INFORCE_DATE</asp:Label><br />                           
                                <asp:TextBox ID="txtInforceDate" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>                           
                                <asp:ImageButton ID="btnInforceDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="PHONENUM" runat="server" Visible="false">
                        <tr>
                            <td>
                                <asp:Label ID="lblPhoneType" runat="server">PHONE_NUMBER_TYPE</asp:Label><br />
                                <asp:DropDownList ID="ddlPhoneType" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblPhone" runat="server">PHONE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtPhone" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblDealer" runat="server">Dealer</asp:Label><br />
                                <asp:DropDownList ID="ddlDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>

                            <td>
                                <asp:Label ID="lblCustName" runat="server">Customer_Name</asp:Label><br />
                                <asp:TextBox ID="txtCustName" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblAddress" runat="server">Address</asp:Label><br />
                                <asp:TextBox ID="txtAddress" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>

                            <td>
                                <asp:Label ID="lblZip" runat="server">Zip</asp:Label><br />
                                <asp:TextBox ID="txtZip" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCertNum" runat="server">Certificate</asp:Label><br />
                                <asp:TextBox ID="txtCertNum" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>

                            <td>
                                <asp:Label ID="lblPhoneNumSrchByStatus" runat="server">STATUS</asp:Label><br />
                                <asp:DropDownList ID="ddlPhoneNumSrchByStatus" runat="server" SkinID="SmallDropDown"
                                    AutoPostBack="False">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server">Sort By</asp:Label><br />
                                <asp:DropDownList ID="ddlSortBy" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblPhoneNumSrchInforceCert" runat="server">INFORCE_DATE</asp:Label><br />
                                <asp:TextBox ID="txtPhoneNumSrchInforceCert" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>
                                <asp:ImageButton ID="btnPhoneNumSrchInforceCert" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="SERIALNUM" runat="server" Visible="false">
                        <tr>
                            <td>
                                <asp:Label ID="moSerialNumberLabel" runat="server">Serial_Number</asp:Label><br />
                                <asp:TextBox ID="moSerialNumberText" runat="server" AutoPostBack="False" SkinID="LargeTextBox"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSrlNumSrchInforceCert" runat="server">INFORCE_DATE</asp:Label><br />
                                <asp:TextBox ID="txtSrlNumSrchInforceCert" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>
                                <asp:ImageButton ID="btnSrlNumSrchInforceCert" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="LICTAG" runat="server" Visible="false">
                        <tr>
                            <td>
                                <asp:Label ID="moVehicleLicenceTagLabel" runat="server">VEHICLE_LICENSE_TAG</asp:Label><br />
                                <asp:TextBox ID="moVehicleLicenceTagText" runat="server" SkinID="LargeTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLicTagSrchInforceCert" runat="server">INFORCE_DATE</asp:Label><br />
                                <asp:TextBox ID="txtLicTagSrchInforceCert" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>
                                <asp:ImageButton ID="btnLicTagSrchInforceCert" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="SERVICELINE" runat="server" Visible="false">
                        <tr>
                            <td width="33%">
                                <asp:Label ID="Label2" runat="server">SERVICE_LINE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="moServiceLineNumber" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                            <td width="33%">
                                <asp:Label ID="lblStatus_SLN" runat="server" >STATUS</asp:Label><br />
                                <asp:DropDownList ID="moStatusDrop_SLN" runat="server" SkinID="SmallDropDown"
                                    AutoPostBack="False">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="33%">
                                <asp:Label ID="moDealerLabel_SLN" runat="server">Dealer</asp:Label><br />
                                <asp:DropDownList ID="moDealerDrop_SLN" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcboSearchSortBy_SLN" runat="server">Sort By</asp:Label><br />
                                <asp:DropDownList ID="cboSortBy_SLN" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblInforce_SLN" runat="server">INFORCE_DATE</asp:Label><br />
                                <asp:TextBox ID="txtInforceDate_SLN" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>
                                <asp:ImageButton ID="btnInforceDate_SLN" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td colspan="3" align="right">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                            </label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_CERTIFICATES" Visible="true" ></asp:Label>
        </h2>
        <div>
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
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
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
                <Columns>
                    <asp:TemplateField HeaderText="Certificate" SortExpression="cert_number">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="SelectAction" ID="btnEditCertificate" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SERVICE_LINE_NUMBER" SortExpression="SERVICE_LINE_NUMBER" ReadOnly="true"
                        HeaderText="SERVICE_LINE_NUMBER" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="HOME_PHONE" SortExpression="HOME_PHONE" ReadOnly="true"
                        HeaderText="HOME_PHONE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="WORK_PHONE" SortExpression="WORK_PHONE" ReadOnly="true"
                        HeaderText="WORK_PHONE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField SortExpression="serial_number" HeaderText="SERIAL_NO_LABEL" DataField="serial_number"
                        HtmlEncode="false" />
                    <asp:BoundField SortExpression="imei_number" HeaderText="imei_number" DataField="imei_number"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="CUSTOMER_NAME" SortExpression="CUSTOMER_NAME" ReadOnly="true"
                        HeaderText="Customer_Name" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="ADDRESS1" SortExpression="ADDRESS1" ReadOnly="true" HeaderText="Address"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="postal_code" SortExpression="postal_code" ReadOnly="true"
                        HeaderText="Zip" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField SortExpression="identification_number" DataField="identification_number"
                        HeaderText="Tax_Id" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="DEALER" SortExpression="DEALER" ReadOnly="true" HeaderText="Dealer"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="INVOICE_NUMBER" SortExpression="INVOICE_NUMBER" ReadOnly="true"
                        HeaderText="INVOICE_NUMBER" HtmlEncode="false" />
                    <asp:BoundField HeaderText="ACCOUNT_NUMBER" DataField="MEMBERSHIP_NUMBER" ReadOnly="true"
                        SortExpression="MEMBERSHIP_NUMBER" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="Product_Code" SortExpression="Product_Code" ReadOnly="true"
                        HeaderText="Product_code" HtmlEncode="false" />
                    <asp:BoundField HeaderText="vehicle_license_tag" DataField="vehicle_license_tag"
                        SortExpression="vehicle_license_tag" HtmlEncode="false" />
                    <asp:BoundField DataField="Status_Code" ReadOnly="true" HeaderText="Status" SortExpression="Status_Code"
                        HtmlEncode="false" />
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
       <div runat="server" id="divHasRestrictedRecords" visible="false" style="color:#0066cc;text-align:left;font-size:14px;">
     <asp:Label ID="lblHasRestrictedRecords" runat="server">HAS_RESTRICTED_RECORDS</asp:Label>

</div>
    </div>
    <!-- end new layout -->
    <script language="javascript" type="text/javascript">
        function resizeScroller(item)
        {
            var browseWidth, browseHeight;

            if (document.layers)
            {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all)
            {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600")
            {
                newHeight = browseHeight - 220;
            }
            else
            {
                newHeight = browseHeight - 975;
            }

            if (newHeight < 470)
            {
                newHeight = 470;
            }

            item.style.height = String(newHeight) + "px";

            return newHeight;
        }

        //resizeScroller(document.getElementById("scroller"));
    </script>
</asp:Content>
