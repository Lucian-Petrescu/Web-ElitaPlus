<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaintainUser.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.business.MaintainUser" EnableSessionState="True"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>

  <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
  <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
  <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
  <script type="text/javascript">
    $(function () {
        $("#tabs").tabs({
            activate: function() {
                var selectedTab = $('#tabs').tabs('option', 'active');
                $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
            },
            active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
        });
    });
  </script>
  <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" class="formGrid"
            style="padding: 0px; margin: 0px">
            <tr>
                <td align="right" height="20" class="borderLeft">
                    <asp:Label ID="moNetworkLabel" runat="server">Network_ID</asp:Label>
                </td>
                <td align="left" height="20">
                    <asp:TextBox ID="txtNetworkID" runat="server" Width="200px" SkinID="MediumTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" height="20" class="borderLeft">
                    <asp:Label ID="moUserLabel" runat="server">User</asp:Label>
                </td>
                <td align="left" height="20">
                    <asp:TextBox ID="txtUserName" runat="server" Width="400px" SkinID="MediumTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" height="20" class="borderLeft">
                    <asp:Label ID="moLanguageLabel" runat="server">Language</asp:Label>
                </td>
                <td align="left" height="20">
                    <asp:DropDownList ID="cboLanguagesId" runat="server" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" height="20" class="borderLeft">
                    <asp:Label ID="Label5" runat="server">Status</asp:Label>:
                </td>
                <td align="left" height="20">
                    <asp:RadioButton ID="rdoActive" runat="server" GroupName="IsActive" Text="Active">
                    </asp:RadioButton><asp:RadioButton ID="rdoInActive" runat="server" GroupName="IsActive"
                        Text="InActive"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <div id="tabs" class="style-tabs" style="position:static;">
          <ul>
            <li style="position:static;"><a href="#tabsUserRoles"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">USER_ ROLES</asp:Label></a></li>
            <li style="position:static;"><a href="#tabsUserCompany"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">USER_COMPANY</asp:Label></a></li>
            <li style="position:static;"><a href="#tabsPermission"><asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">PERMISSION</asp:Label></a></li>
            <li style="position:static;"><a href="#tabsUserSecurity"><asp:Label ID="Label9" runat="server" CssClass="tabHeaderText">USER_SECURITY</asp:Label></a></li>
          </ul>
          <div id="tabsUserRoles">
            <table id="Table5" cellspacing="0" cellpadding="0" width="300" align="center" border="0"
                    class="formGrid">
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server">Available</asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server">Selected</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstAvailable" Style="width: 300px; height: 150px" runat="server">
                            </asp:ListBox>
                        </td>
                        <td>
                            <table id="Table6" style="width: 75px; height: 27px" cellspacing="1" cellpadding="1"
                                width="75" border="0">
                                <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnAddToSelected" runat="server" Width="55px" Text=">>" >
                                        </asp:Button>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnRemove" runat="server" Text="<<" Width="55px" ></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:ListBox ID="lstSelected" Style="width: 300px; height: 150px" runat="server">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
          </div>
          <div id="tabsUserCompany">
            <table id="Table7" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
                    class="formGrid">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server">Available</asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="cboCompanyAvailable" Width="300px" runat="server" SkinID="SmallDropDown">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnAddCompany" runat="server" Width="41px" Text="Add" >
                            </asp:Button>
                        </td>
                    </tr>
                </table>
                <div class="Page" runat="server" id="moUserCompaniesTabPanel_WRITE" style="display: block;
                    height: 260px; overflow: auto">
                    <asp:GridView ID="Grid" runat="server" Width="100%" OnRowDataBound="Grid_ItemDataBound"
                        OnRowCommand="Grid_ItemCommand" AllowPaging="false" AllowSorting="False" CellPadding="1"
                        AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                        <EditRowStyle Wrap="True"></EditRowStyle>
                        <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                        <RowStyle Wrap="True"></RowStyle>
                        <Columns>
                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="EditAction"
                                        ImageUrl="../Navigation/images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="DeleteAction"
                                        ImageUrl="../Navigation/images/icon_delete.png" CommandArgument="<%#Container.DisplayIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false"></asp:TemplateField>
                            <asp:TemplateField Visible="false"></asp:TemplateField>
                            <asp:TemplateField SortExpression="description" HeaderText="Company Name">
                                <HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="payment_limit" HeaderText="Payment Limit">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="moPaymentLimitLabel" runat="server" Visible="True" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="moPaymentLimitText" runat="server" Visible="True" Width="70%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="authorization_limit" HeaderText="Authorization Limit">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="moAuthorizationLimitLabel" runat="server" Visible="True" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="moAuthorizationLimitText" runat="server" Visible="True" Width="70%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="liability_override_limit" HeaderText="Liability Override Limit">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="moLiabilityOverrideLimitLabel" runat="server" Visible="True" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="moLiabilityOverrideLimitText" runat="server" Visible="True" Width="70%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="btnZone">
                        <asp:Button ID="BtnSaveCompany" runat="server" SkinID="AlternateLeftButton" Text="Save">
                        </asp:Button>
                        <asp:Button ID="BtnCancelCompany" runat="server" SkinID="AlternateLeftButton" Text="Cancel">
                        </asp:Button>
                    </div>
                </div>
          </div>
          <div id="tabsPermission">
            <table id="Table3" cellspacing="0" cellpadding="0" width="300" align="center" border="0"
                    class="formGrid">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server">Available</asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server">Selected</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstAvailablePermission" Style="width: 300px; height: 150px" runat="server"></asp:ListBox>
                        </td>
                        <td>
                            <table id="Table4" style="width: 75px; height: 27px" cellspacing="1" cellpadding="1"
                                width="75" border="0">
                                <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnAddPermissionToSelected" runat="server" Width="55px" Text=">>" ></asp:Button>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnRemovePermission" runat="server" Text="<<" Width="55px" ></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:ListBox ID="lstSelectedPermission" Style="width: 300px; height: 150px" runat="server"></asp:ListBox>
                        </td>
                    </tr>
                </table>
          </div>
          <div id="tabsUserSecurity">
              <table id="Table9" width="100%" cellspacing="0" cellpadding="0" border="0" class="formGrid">
                    <tbody>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label runat="server" ID="SpClaimCodeLabel" Text="SP_CLAIM_CODE"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList runat="server" ID="SpClaimCodeDropDown"  AutoPostBack="true" SkinID="MediumDropDown" />
                            </td>
                        </tr>
                        <tr id="CertificateFile" runat="server" visible="false">
                            <td align="right" nowrap="nowrap">
                                <asp:Label runat="server" ID="CertificateFileLabel" Text="CERTIFICATE_FILE_UPLOAD"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <input id="CertificateFileUpload" style="width: 80%" type="file" accept=".cer,.crt" name="CertificateFileUpload"
                                runat="server" />
                            </td>
                        </tr>
                        <tr id="SpClaimCountry" runat="server">
                            <td align="right" nowrap="nowrap">
                                <asp:Label runat="server" ID="SpClaimCountryLabel" Text="COUNTRY"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox runat="server" ID="SpClaimCountryTextBox"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvSpClaimCountryTextBox" runat="server" ControlToValidate="SpClaimCountryTextBox" 
                                    ErrorMessage="Value is required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="SpClaimDealer" runat="server">
                            <td align="right" nowrap="nowrap">
                                <asp:Label runat="server" ID="Label10" Text="DEALER"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblSpClaimDealer" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Any" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>                                   
                                </asp:RadioButtonList>
                                 <asp:TextBox runat="server" ID="txtDealerCode"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtDealerCode" runat="server" ControlToValidate="txtDealerCode" 
                                    ErrorMessage="Value is required"></asp:RequiredFieldValidator>
                                <asp:Label runat="server" ID="lblDealerErrorMsg" ForeColor="Red"></asp:Label>
                            </td>
                            <%--<td nowrap="nowrap">
                                
                            </td>--%>
                        </tr>
                        <tr id="SpClaimValue" runat="server">
                            <td align="right" nowrap="nowrap">
                                <asp:Label runat="server" ID="SpClaimValueLabel"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox runat="server" ID="SpClaimValueTextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSpClaimValueTextBox" runat="server" ControlToValidate="SpClaimValueTextBox" 
                                    ErrorMessage="Value is required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="SpClaimEffectiveDate" runat="server">
                           <td align="right">
                                <asp:Label ID="ValidFromLabel" runat="server">SP_CLAIM_VALID_FROM</asp:Label>
                           </td>
                           <td align="left">
                                <asp:TextBox runat="server" ID="ValidFromText" SkinID="SmallTextBox"></asp:TextBox>
                                  <asp:ImageButton ID="ValidFromImageButton" runat="server" Style="vertical-align: bottom"
                                    ImageUrl="../Common/Images/calendarIcon2.jpg"/>
                           </td>
                        </tr>
                        <tr id="SpClaimExpirationDate" runat="server">
                           <td align="right">
                                <asp:Label ID="ValidToLabel" runat="server">SP_CLAIM_VALID_TO</asp:Label>
                           </td>
                           <td align="left">
                                <asp:TextBox ID="ValidToText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                <asp:ImageButton ID="ValidToImageButton" runat="server" Style="vertical-align: bottom"
                                    ImageUrl="../Common/Images/calendarIcon2.jpg"/>
                           </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Button ID="ClearButton" runat="server" SkinID="AlternateRightButton" Text="CLEAR"></asp:Button>
                            </td>
                             <td>
                                 <asp:Button ID="AddSpClaimButton" runat="server" SkinID="AlternateLeftButton" Text="Add_SP_Claim"></asp:Button>
                            </td>
                        </tr>
                     </tbody>
               </table>
              <div class="Page" style="display: block; overflow: auto">
                   <asp:GridView ID="GridViewUserSecurity" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="GridViewUserSecurity_ItemDataBound" OnRowCommand="GridViewUserSecurity_ItemCommand" AllowPaging="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False" />
                        <EditRowStyle Wrap="False" />
                        <AlternatingRowStyle Wrap="False" />
                        <RowStyle Wrap="False" />
                        <HeaderStyle />
                        <Columns>
                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="EditAction"
                                        ImageUrl="../Navigation/images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="SpUserClaimsIdLabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("sp_user_claims_id"), "true")%>'
                                        runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="SpClaimTypeIdLabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("sp_claim_type_id"))%>'
                                        runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="SP_CLAIM_CODE_DESCRIPTION">
                                <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="SpClaimCodeDescriptionLabel" Text='<%# Container.DataItem("sp_claim_code_description")%>'
                                        runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField Visible="True" HeaderText="SP_CLAIM_VALUE">
                               <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="SpClaimValueLabel" Text='<%# Container.DataItem("sp_claim_value")%>'
                                        runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="SP_CLAIM_VALID_FROM">
                                <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="SpClaimEffectiveDateLabel" Text='<%# GetDateFormattedStringNullable(Container.DataItem("EFFECTIVE_DATE"))%>'
                                        runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SP_CLAIM_VALID_TO">
                                <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="SpClaimExpirationDateLabel" runat="server" Visible="True" text='<%# GetDateFormattedStringNullable(Container.DataItem("expiration_date"))%>'/>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="SpClaimExpirationDateText" runat="server" Visible="True" text='<%# GetDateFormattedStringNullable(Container.DataItem("expiration_date"))%>' Width="70%"></asp:TextBox>
                                     <asp:ImageButton ID="SpClaimExpirationDateImageButton" runat="server" Style="vertical-align: bottom"
                                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="btnZone">
                        <asp:Button ID="UpdateSpClaimButton" runat="server" SkinID="AlternateLeftButton" Text="Save">
                        </asp:Button>
                        <asp:Button ID="CancelUpdateSpClaimButton" runat="server" SkinID="AlternateLeftButton" Text="Cancel">
                        </asp:Button>
                    </div>
                </div>
           </div>
        </div>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
            runat="server" />
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0" class="formGrid"
            style="padding: 0px; margin: 0px">
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2" height="10" class="borderLeft">
                    <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                        width="100%">
                        <tr>
                            <td align="right" rowspan="1" width="30%">
                                <asp:RadioButton ID="rdoInternal" runat="server" AutoPostBack="True" GroupName="IsInternal"
                                    Text="Internal_User"></asp:RadioButton>&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                <asp:RadioButton ID="rdoExternal" runat="server" AutoPostBack="True" GroupName="IsInternal"
                                    Text="External_User"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <table id="tblExternalUserDetails" cellspacing="1" cellpadding="1" width="100%" border="0"
                                    runat="server">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="moDealerGroupLabel" runat="server">Dealer_group</asp:Label>:
                                        </td>
                                        <td align="left" height="20">
                                            <asp:DropDownList ID="cboDealerGroupId" onclick="RestOtherDropDown(1);" runat="server"
                                                Width="388px" SkinID="SmallDropDown" AutoPostBack="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="moDealerLabel" runat="server">Dealer</asp:Label>:
                                        </td>
                                        <td align="left" height="20">
                                            <asp:DropDownList ID="cboDealerId" onclick="RestOtherDropDown(2);" runat="server"
                                                Width="388px" SkinID="SmallDropDown" AutoPostBack="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="LabelSearchServiceCenter" runat="server">SERVICE_CENTER_NAME</asp:Label>:
                                        </td>
                                        <td align="left" height="20">
                                            <asp:DropDownList ID="cboServiceCenterId" onclick="RestOtherDropDown(3);" runat="server"
                                                SkinID="SmallDropDown" AutoPostBack="False" Width="388px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblOther" runat="server">OTHER</asp:Label>:
                                        </td>
                                        <td align="left" height="20">
                                            <asp:CheckBox ID="chkOther" onclick="RestOtherDropDown(4);" runat="server"></asp:CheckBox><asp:Label
                                                ID="Label7" runat="server" Width="372px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="btnZone">
            <asp:Button ID="btnSave_WRITE" runat="server" Text="Save" SkinID="PrimaryRightButton"
                ToolTip="Save" CausesValidation="false"></asp:Button>
            <asp:Button ID="btnApply_WRITE" runat="server" Text="Apply" SkinID="AlternateRightButton"
                ToolTip="Save"></asp:Button>
            <asp:Button ID="btnCopy_WRITE" runat="server" Text="New_With_Copy" SkinID="AlternateRightButton"
                CausesValidation="False"></asp:Button>
            <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton"
                ToolTip="Back" CausesValidation="False"></asp:Button>
            <asp:Button ID="btnNew_WRITE" runat="server" Text="New" SkinID="AlternateLeftButton"
                CausesValidation="False"></asp:Button>
            <asp:Button ID="btnDelete_WRITE" runat="server" Text="Delete" SkinID="CenterButton"
                CausesValidation="False"></asp:Button>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#ctl00_BodyPlaceHolder_txtDealerCode").attr("disabled", "disabled");

            $('#<%= rblSpClaimDealer.ClientId%>').change(function () {
                var rbvalue = $("input[name='<%=rblSpClaimDealer.UniqueID%>']:radio:checked").val();

                if (rbvalue == "Other")
                {
                    $("#ctl00_BodyPlaceHolder_txtDealerCode").removeAttr("disabled");
                }
                else {
                    $("#ctl00_BodyPlaceHolder_txtDealerCode").attr("disabled", "disabled");
                    
                }
               
                
            });

            //allow only alphabets
            $('#<%= txtDealerCode.ClientId%>').keypress(function (e) {
                var key = e.keyCode;
                
                if (key >= 48 && key <= 57) {
                    e.preventDefault();
                }
            });

            //do not allow the keywork "Any,any,ANY"
            $('#<%= txtDealerCode.ClientId%>').keyup(function () {
                var notallowedWord = ["Any", "ANY", "any"];

                for (var i = 0; i < notallowedWord.length; i++) {
                    if (this.value == notallowedWord[i]) {
                        $('#<%= lblDealerErrorMsg.ClientId%>').text("Not Allowed!");
                        return;
                    }
                }
                $('#<%= lblDealerErrorMsg.ClientId%>').text("");
            });

            //allow only 2 characters in country text box
            $('#<%= SpClaimCountryTextBox.ClientId%>').keypress(function (e) {
                var key = e.keyCode;
                
                if($(this).val().length >= 2){ 
                    e.preventDefault();
                }
            });

            
        });

        function RestOtherDropDown(action) {
            //RestOtherDropDown(1)
            //RestOtherDropDown(2)
            //RestOtherDropDown(3)
            //RestOtherDropDown(4);
            switch (action) {
                case 1: //Dealer Group selected
                    {
                        var objDropDown1ToReset = document.getElementById('<%=cboDealerId.ClientID%>');
                        objDropDown1ToReset.selectedIndex = -1;
                        var objDropDown2ToReset = document.getElementById('<%=cboServiceCenterId.ClientID%>');
                        objDropDown2ToReset.selectedIndex = -1;
                        var objCheckBoxToReset = document.getElementById('<%=cboDealerGroupId.ClientID%>');
                        objCheckBoxToReset.checked = false;
                        break;
                    }
                case 2: //Dealer selected
                    {
                        var objDropDown1ToReset = document.getElementById('<%=cboDealerGroupId.ClientID%>');
                        objDropDown1ToReset.selectedIndex = -1;
                        var objDropDown2ToReset = document.getElementById('<%=cboServiceCenterId.ClientID%>');
                        objDropDown2ToReset.selectedIndex = -1;
                        var objCheckBoxToReset = document.getElementById('<%=chkOther.ClientID%>');
                        objCheckBoxToReset.checked = false;
                        break;
                    }
                case 3: //Service center selected
                    {
                        var objDropDown1ToReset = document.getElementById('<%=cboDealerGroupId.ClientID%>');
                        objDropDown1ToReset.selectedIndex = -1;
                        var objDropDown2ToReset = document.getElementById('<%=cboDealerId.ClientID%>');
                        objDropDown2ToReset.selectedIndex = -1;
                        var objCheckBoxToReset = document.getElementById('<%=chkOther.ClientID%>');
                        objCheckBoxToReset.checked = false;
                        break;
                    }
                case 4: //Other checkbox
                    {
                        var objDropDown1ToReset = document.getElementById('<%=cboDealerGroupId.ClientID%>');
                        objDropDown1ToReset.selectedIndex = -1;
                        var objDropDown2ToReset = document.getElementById('<%=cboDealerId.ClientID%>');
                        objDropDown2ToReset.selectedIndex = -1;
                        var objDropDown3ToReset = document.getElementById('<%=cboServiceCenterId.ClientID%>');
                        objDropDown3ToReset.selectedIndex = -1;
                        break;
                    }
            }

        }
        
        
           
    </script>
</asp:Content>
