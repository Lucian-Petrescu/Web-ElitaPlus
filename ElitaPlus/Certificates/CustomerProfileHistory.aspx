<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerProfileHistory.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CustomerProfileHistory"
    EnableSessionState="True" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls"
    TagPrefix="iewc" %>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
 
    <table id="TableFixed" style="width:98%;border:0;" class="summaryGrid">
        <tr>
            <td style="text-align:left">
                <asp:Label ID="CustomerNameLabel" runat="server" SkinID="SummaryLabel">CUSTOMER_NAME</asp:Label>:
            </td>
            <td style="text-align:left;font-weight: bold" runat="server"
                id="CustomerNameTD">
                <strong></strong>
            </td>
            <td style="text-align:left" runat="server" id="IdentificationNumberLabelTD">
                <asp:Label ID="IdentificationNumberLabel" runat="server" SkinID="SummaryLabel">IDENTIFICATION_NUMBER</asp:Label>:
            </td>
            <td style="text-align:left"  runat="server" id="IdentificationNumberTD"></td>
            <td style="text-align:left" runat="server" id="GenderLabelTD">
                <asp:Label ID="GenderLabel" runat="server" SkinID="SummaryLabel">GENDER</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="GenderTD"></td>
            <td style="text-align:left" runat="server" id="MaritalStatusLabelTD">
                <asp:Label ID="MaritalStatusLabel" runat="server" SkinID="SummaryLabel">MARITAL_STATUS</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="MaritalStatusTD"></td>
        </tr>
        <tr>
            <td style="text-align:left">
                <asp:Label ID="PlaceOfBirthLabel" runat="server" SkinID="SummaryLabel">PLACE_OF_BIRTH</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="PlaceOfBirthTD"></td>
            <td style="text-align:left">
                <asp:Label ID="DateOfBirthLabel" runat="server" SkinID="SummaryLabel">DATE_OF_BIRTH</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="DateOfBirthTD"></td>
            <td style="text-align:left">
                <asp:Label ID="NationalityLabel" runat="server" SkinID="SummaryLabel">NATIONALITY</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="NationalityTD"></td>
            <td style="text-align:left">
                <asp:Label ID="EmailLabel" runat="server" SkinID="SummaryLabel">EMAIL</asp:Label>:
            </td>
            <td style="text-align:left"  runat="server" id="EmailTD"></td>

        </tr>
        <tr>
            <td style="text-align:left">
                <asp:Label ID="HomePhoneLabel" runat="server" SkinID="SummaryLabel">HOME_PHONE</asp:Label>:
            </td>
            <td style="text-align:left"  runat="server" id="HomePhoneTD"></td>
            <td style="text-align:left">
                <asp:Label ID="WorkPhoneLabel" runat="server" SkinID="SummaryLabel">WORK_PHONE</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="WorkPhoneTD"></td>
            <td style="text-align:left">
                <asp:Label ID="AddressLabel" runat="server" SkinID="SummaryLabel">ADDRESS</asp:Label>:
            </td>
            <td  runat="server" id="AddressTD" style="text-align:left;text-wrap: normal; word-wrap: break-word; word-break: break-all; white-space: pre-wrap; width: 30%;"></td>
            <td style="text-align:left">
                <asp:Label ID="CityLabel" runat="server" SkinID="SummaryLabel">CITY</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="CityTD"></td>
        </tr>
        <tr>

            <td style="text-align:left">
                <asp:Label ID="StateLabel" runat="server" SkinID="SummaryLabel">STATE</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="StateTD"></td>
            <td style="text-align:left">
                <asp:Label ID="PostalCodeLabel" runat="server" SkinID="SummaryLabel">POSTAL_CODE</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="PostalCodeTD"></td>
            <td style="text-align:left">
                <asp:Label ID="CountryCodeLabel" runat="server" SkinID="SummaryLabel">COUNTRY</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="CountryCodeTD"></td>
        </tr>
        <tr>

            <td style="text-align:left">
                <asp:Label ID="CorporateNameLabel" runat="server" SkinID="SummaryLabel">CORPORATE_NAME</asp:Label>:
            </td>
            <td style="text-align:left" runat="server" id="CorporateNameTD"></td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="8" style="text-align:left">
                <table id="CustomerBankInfoHead" style="width:98%;border:0;" >
                    <tr>
                        <td colspan="8">
                            <h2 class="searchGridSubHeader">
                                <a id="CustomerBankInfoExpander" href="#">
                                    <img alt="sort indicator" src="../App_Themes/Default/Images/sort_indicator_des.png" /></a>

                                <asp:Label ID="CustomerBankInformation" runat="server">CUSTOMER_BANK_INFORMATION</asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="text-align:right">
                            <table id="CustomerBankInfo" style="width:98%;border:0;">
                                <tr>
                                    <td style="text-align:left">
                                        <asp:Label ID="NameOnAccountLabel" runat="server">NAME_ON_ACCOUNT</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="NameOnAccountTD"></td>
                                    <td style="text-align:left">
                                        <asp:Label ID="BankNameLabel" runat="server">BANK_NAME</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="BankNameTD"></td>
                                    <td style="text-align:left">
                                        <asp:Label ID="BankIDLabel" runat="server">BANK_ID</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="BankIDTD"></td>
                                    <td style="text-align:left">
                                        <asp:Label ID="AccountTypeLable" runat="server">ACCOUNT_TYPE</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="AccountTypeTD"></td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:Label ID="BankLookupCodeLabel" runat="server">BANK_LOOKUP_CODE</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="BankLookupCodeTD"></td>
                                    <td style="text-align:left">
                                        <asp:Label ID="BankSortCodeLabel" runat="server">BANK_SORT_CODE</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="BankSortCodeTD"></td>
                                    <td style="text-align:left">
                                        <asp:Label ID="BranchNameLable" runat="server">BRANCH_NAME</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="BranchNameTD"></td>
                                    <td style="text-align:left">
                                        <asp:Label ID="BankSubCodeLable" runat="server">BANK_SUB_CODE</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="BankSubCodeTD"></td>
                                </tr>
                                <tr>
                                    <td style="text-align:left">
                                        <asp:Label ID="AccountNumberLable" runat="server">ACCOUNT_NUMBER</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="AccountNumberTD"></td>
                                    <td style="text-align:left">
                                        <asp:Label ID="IBANNumberLable" runat="server">IBAN_NUMBER</asp:Label>:
                                    </td>
                                    <td style="text-align:left"  runat="server" id="IBANNumberTD"></td>
                                    <td colspan="4"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>


    <br />
    <table>
        <tr id="PerInfoHistory" runat="server">
            <td style="text-align:left">
                <h2 class="dataGridHeader">
                    <a id="PersonalHistExpander" href="#">
                        <img alt="sort indicator" src="../App_Themes/Default/Images/sort_indicator_des.png" /></a>
                    <asp:Label ID="CustomerPersonelHistory" runat="server">PERSONAL_HISTORY</asp:Label>

                </h2>
            </td>
        </tr>
        <tr id="PersonelHistInfo">
            <td style="border-bottom: none">
                <div style="width: 100%; height: 100%; min-height: 100%">
                    <asp:GridView ID="CustPersonalHistory" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999"
                        DataKeyNames="customer_id" SkinID="DetailPageGridView" AllowPaging="false" AllowSorting="false">
                        <Columns>
                            <asp:BoundField DataField="salutation" HeaderText="SALUTATION" />
                            <asp:BoundField DataField="first_name" HeaderText="FIRST_NAME" />
                            <asp:BoundField DataField="middle_name" HeaderText="MIDDLE_NAME" />
                            <asp:BoundField DataField="last_name" HeaderText="LAST_NAME" />
                            <asp:BoundField DataField="corporate_name" HeaderText="CORPORATE_NAME" />
                            <asp:BoundField DataField="gender" HeaderText="GENDER" />
                            <asp:BoundField DataField="birth_date" HeaderText="BIRTH_DATE" />
                            <asp:BoundField DataField="marital_status" HeaderText="MARITAL_STATUS" />
                            <asp:BoundField DataField="nationality" HeaderText="NATIONALITY" />
                            <asp:BoundField DataField="place_of_birth" HeaderText="PLACE_OF_BIRTH" />
                            <asp:BoundField DataField="modified_by" HeaderText="MODIFIED_BY" />
                            <asp:BoundField DataField="change_date" HeaderText="DATE_OF_CHANGE" />
                            <asp:BoundField DataField="customer_id" Visible="false" />

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr id="AddrInfohistory" runat="server">
            <td style="text-align:left">
                <h2 class="dataGridHeader">
                    <a id="AddressHistExpander" href="#">
                        <img alt="sort indicator" src="../App_Themes/Default/Images/sort_indicator_des.png" /></a>
                    <asp:Label ID="CustomerAddressHistory" runat="server">ADDRESS_HISTORY</asp:Label>

                </h2>
            </td>
        </tr>
        <tr id="AddressHistInfo">
            <td style="border-bottom: none">
                <div style="width: 100%; height: 100%; min-height: 100%">
                    <asp:GridView ID="CustAddressHistory" runat="server" AutoGenerateColumns="false"
                        Width="100%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999"
                        DataKeyNames="address_id" SkinID="DetailPageGridView" AllowPaging="false" AllowSorting="false">
                        <Columns>
                            <asp:BoundField DataField="address1" HeaderText="ADDRESS1" />
                            <asp:BoundField DataField="address2" HeaderText="ADDRESS2" />
                            <asp:BoundField DataField="address3" HeaderText="ADDRESS3" />
                            <asp:BoundField DataField="city" HeaderText="CITY" />
                            <asp:BoundField DataField="country" HeaderText="COUNTRY" />
                            <asp:BoundField DataField="state" HeaderText="STATE" />
                            <asp:BoundField DataField="postal_code" HeaderText="POSTAL_CODE" />
                            <asp:BoundField DataField="zip_locator" HeaderText="ZIP_LOCATOR" />
                            <asp:BoundField DataField="modified_by" HeaderText="MODIFIED_BY" />
                            <asp:BoundField DataField="change_date" HeaderText="DATE_OF_CHANGE" />
                            <asp:BoundField DataField="address_id" Visible="false" />

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr id="ConatctInfohistory" runat="server">
            <td style="text-align:left">
                <h2 class="dataGridHeader">
                    <a id="ContactHistExpander" href="#">
                        <img alt="sort indicator" src="../App_Themes/Default/Images/sort_indicator_des.png" /></a>
                    <asp:Label ID="Label1" runat="server">CONTACT_HISTORY</asp:Label>

                </h2>
            </td>
        </tr>
        <tr id="ContactHistInfo">
            <td style="border-bottom: none">
                <div style="width: 100%; height: 100%; min-height: 100%">
                    <asp:GridView ID="CustContactHistory" runat="server" AutoGenerateColumns="false"
                        Width="100%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999"
                        SkinID="DetailPageGridView" AllowPaging="false" AllowSorting="false">
                        <Columns>
                            <asp:BoundField DataField="email" HeaderText="EMAIL" />
                            <asp:BoundField DataField="home_phone" HeaderText="HOME_PHONE" />
                            <asp:BoundField DataField="work_phone" HeaderText="WORK_PHONE" />
                            <asp:BoundField DataField="modified_by" HeaderText="MODIFIED_BY" />
                            <asp:BoundField DataField="change_date" HeaderText="DATE_OF_CHANGE" />

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr id="BankDetailHist" runat="server">
            <td style="text-align:left">
                <h2 class="dataGridHeader">
                    <a id="BankDetailHistExpander" href="#">
                        <img alt="sort indicator" src="../App_Themes/Default/Images/sort_indicator_des.png" /></a>
                    <asp:Label ID="BankDetailHistory" runat="server">BANK_DETAIL_HISTORY</asp:Label>
                </h2>
            </td>
        </tr>
        <tr id="BankDetailHistInfo">
            <td style="border-bottom: none">
                <div style="width: 100%; height: 100%; min-height: 100%">
                    <asp:GridView ID="CustBankDetailHistory" runat="server" AutoGenerateColumns="false"
                        Width="100%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999"
                        SkinID="DetailPageGridView" AllowPaging="false" AllowSorting="false">
                        <Columns>
                            <asp:BoundField DataField="account_name" HeaderText="NAME_ON_ACCOUNT" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="bank_name" HeaderText="BANK_NAME" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="bank_id" HeaderText="BANK_ID" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="account_type_id" HeaderText="ACCOUNT_TYPE" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="bank_lookup_code" HeaderText="BANK_LOOKUP_CODE" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="bank_sort_code" HeaderText="BANK_SORT_CODE" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="branch_name" HeaderText="BRANCH_NAME" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="bank_sub_code" HeaderText="BANK_SUB_CODE" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="account_number" HeaderText="ACCOUNT_NUMBER" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="mandate_id" HeaderText="MANDATE_ID" HeaderStyle-Width="50" />
                            <asp:BoundField DataField="iban_number" HeaderText="IBAN_NUMBER" HeaderStyle-Width="20" ItemStyle-Width="10" ItemStyle-Wrap="true" />
                            <asp:BoundField DataField="created_by" HeaderText="CREATED_BY" HeaderStyle-Width="100" />
                            <asp:BoundField DataField="created_date" HeaderText="CREATED_DATE" HeaderStyle-Width="20" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <div class="btnZone">
        <div class="">

            <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#PersonelHistInfo').slideUp();
            $("#PersonelHistInfo").css("display", "none");

            $('#CustomerBankInfo').slideUp();
            $("#CustomerBankInfo").css("display", "none");

        });

        $("#CustomerBankInfoExpander").click(function () {
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $('#CustomerBankInfo').slideToggle(500, function () {
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                $('#CustomerBankInfoExpander').html(function () {
                    //change text based on condition
                    return $('#CustomerBankInfo').is(":visible") ? "<img src='../App_Themes/Default/Images/sort_indicator_asc.png'>" : "<img src='../App_Themes/Default/Images/sort_indicator_des.png'>";
                });
            });
        });

        $("#PersonalHistExpander").click(function () {
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $('#PersonelHistInfo').slideToggle(500, function () {
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                $('#PersonalHistExpander').html(function () {
                    //change text based on condition
                    return $('#PersonelHistInfo').is(":visible") ? "<img src='../App_Themes/Default/Images/sort_indicator_asc.png'>" : "<img src='../App_Themes/Default/Images/sort_indicator_des.png'>";
                });
            });

        });

        $(document).ready(function () {
            $('#AddressHistInfo').slideUp();
            $("#AddressHistInfo").css("display", "none");
        });

        $("#AddressHistExpander").click(function () {
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $('#AddressHistInfo').slideToggle(500, function () {
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                $('#AddressHistExpander').html(function () {
                    //change text based on condition
                    return $('#AddressHistInfo').is(":visible") ? "<img src='../App_Themes/Default/Images/sort_indicator_asc.png'>" : "<img src='../App_Themes/Default/Images/sort_indicator_des.png'>";
                });
            });

        });

        $(document).ready(function () {
            $('#ContactHistInfo').slideUp();
            $("#ContactHistInfo").css("display", "none");
        });

        $("#ContactHistExpander").click(function () {
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $('#ContactHistInfo').slideToggle(500, function () {
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                $('#ContactHistExpander').html(function () {
                    //change text based on condition
                    return $('#ContactHistInfo').is(":visible") ? "<img src='../App_Themes/Default/Images/sort_indicator_asc.png'>" : "<img src='../App_Themes/Default/Images/sort_indicator_des.png'>";
                });
            });

        });

        $(document).ready(function () {
            $('#BankDetailHistInfo').slideUp();
            $("#BankDetailHistInfo").css("display", "none");
        });

        $("#BankDetailHistExpander").click(function () {
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $('#BankDetailHistInfo').slideToggle(500, function () {
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                $('#BankDetailHistExpander').html(function () {
                    //change text based on condition
                    return $('#BankDetailHistInfo').is(":visible") ? "<img src='../App_Themes/Default/Images/sort_indicator_asc.png'>" : "<img src='../App_Themes/Default/Images/sort_indicator_des.png'>";
                });
            });

        });
    </script>
</asp:Content>
