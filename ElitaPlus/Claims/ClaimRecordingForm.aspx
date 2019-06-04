<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimRecordingForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimRecordingForm" MasterPageFile="~/Navigation/masters/ElitaBase.Master"
    Theme="Default" EnableSessionState="True" %>

<%@ Register TagPrefix="Elita" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddressInfo" Src="../Common/UserControlAddress_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlQuestion" Src="../Common/UserControlQuestion.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlDeliverySlot" Src="../Common/UserControlDeliverySlot.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .ModalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.5;
        }

        .black_show {
            cursor: wait;
            position: absolute;
            top: 0;
            left: 0;
            background-color: #777777;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .75;
            filter: alpha(opacity=90);
        }

        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:ProtectionAndEventDetails ID="moProtectionEvtDtl" runat="server" align="center" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
    <script language="javascript" type="text/javascript">
        function checkRadioBtnCaller(id) {
            var gv = document.getElementById('<%=GridCallers.ClientID %>');

            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
            }
        }

        function checkRadioBtnDevice(id) {
            var gv = document.getElementById('<%=GridItems.ClientID %>');

            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
            }
        }
    </script>
    <script language="jscript" type="text/jscript">

        $(document).ready(function () {
            $(".callers").change(function () {
                //console.log($(this));
                $(".callers").each(function () {
                    //debugger;
                    $(this)[0].firstChild.checked = false;
                });
                //debugger;
                $(this)[0].firstChild.checked = true;
            });
        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div id="ModalCancel" class="overlay">
        <div class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server" width="16" height="18" align="absmiddle" class="floatR" alt="" />
                </a>
            </p>
            <table class="formGrid" width="98%">
                <tbody>
                    <tr>
                        <td>
                            <img id="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png" height="28" />
                        </td>
                        <td id="tdModalMessage" colspan="2" runat="server">
                            <asp:Label ID="lblCancelMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td id="tdBtnArea" runat="server" colspan="2">
                            <asp:Button ID="btnCancelYes" class="primaryBtn floatR" runat="server" Text="Yes"></asp:Button>
                            <input id="btnModalCancelNo" class="popWindowAltbtn floatR" runat="server" type="button"
                                value="No" onclick="hideModal('ModalCancel');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <asp:MultiView ID="mvClaimsRecording" runat="server" ActiveViewIndex="0">
        <asp:View ID="vCaller" runat="server">
            <div class="dataContainer" runat="server" id="Div1">
                <table width="100%">
                    <tr>
                        <td width="100%">
                            <hr style="height: 1px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%; height: 100%" class="dataGrid">
                                <tr id="trPurpose" runat="server">
                                    <td class="bor" align="left">
                                        <asp:Label ID="lblPurpose" runat="server">Purpose_Type</asp:Label><asp:Label ID="Label4"
                                            runat="server">:</asp:Label>
                                        &nbsp;
                                                                    <asp:DropDownList ID="moPurposecode" runat="server" SkinID="MediumDropDown">
                                                                    </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trCallers" runat="server">
                                    <td style="text-align: center">
                                        <asp:DataGrid ID="GridCallers" runat="server" AllowPaging="false" AllowSorting="False" Width="100%"
                                            CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#F7F7F7" BorderStyle="Solid"
                                            BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="false" Height="16px"
                                            OnItemDataBound="GridCallers_ItemDataBound" SkinID="DetailPageDataGrid">
                                            <SelectedItemStyle Wrap="false" BackColor="LavenderBlush" />
                                            <EditItemStyle Wrap="false" BackColor="AliceBlue" />
                                            <AlternatingItemStyle Wrap="false" CssClass="DataGridAlternatingItemStyle" BackColor="#F1F1F1" />
                                            <ItemStyle Wrap="false" BackColor="White" />
                                            <FooterStyle BorderStyle="Double" />
                                            <Columns>
                                                <asp:BoundColumn Visible="False" DataField="FIRST_NAME"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="LAST_NAME"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="WORK_PHONE"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="EMAIL"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="RELATIONSHIP"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="0%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rdoCaller" runat="server" Class="callers" Enabled="True" Visible="True" Width="" AutoPostBack="true" onclick="checkRadioBtnCaller(this);"></asp:RadioButton>
                                                        <asp:Label ID="lblItemIndex" Text="<%#Container.ItemIndex%>" runat="server" Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="FIRST_NAME">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFirstName" Text='<%#Eval("FIRST_NAME")%>' runat="server"></asp:Label>
                                                        <asp:TextBox Enabled="false" ID="txtFirstName" Width="90%" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox><br>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="LAST_NAME">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLastName" Text='<%#Eval("LAST_NAME")%>' runat="server"></asp:Label>
                                                        <asp:TextBox Enabled="false" ID="txtLastName" Width="90%" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox><br>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="WORK_PHONE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPhoneNumber" Text='<%#Eval("WORK_PHONE")%>' runat="server"></asp:Label>
                                                        <asp:TextBox Enabled="false" ID="txtPhoneNumber" Width="90%" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox><br>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="EMAIL">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" Text='<%#Eval("EMAIL")%>' runat="server"></asp:Label>
                                                        <asp:TextBox Enabled="false" ID="txtEmail" Width="90%" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox><br>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="RELATIONSHIP_TYPE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRelationship" Text='<%#Eval("RELATIONSHIP")%>' runat="server"></asp:Label>
                                                        <asp:DropDownList Enabled="false" ID="cboRelationship" Width="90%" runat="server" SkinID="SmallDropDown"></asp:DropDownList><br>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnZone">
                <asp:Button ID="btn_Caller_Cont" runat="server" ValidationGroup="CallerGroup" SkinID="PrimaryRightButton" Text="Continue" />
                <asp:Button ID="btn_Caller_SaveExit" TabIndex="190" runat="server" Text="Save_Exit" SkinID="PrimaryRightButton" Visible="false" />
                <asp:LinkButton ID="btn_Caller_Cancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
            </div>
        </asp:View>
        <asp:View ID="vDevice" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader" runat="server" id="Device_Selection">
                    <asp:Label runat="server" ID="lblDevice" Text="DEVICE_INFO" /></h2>

                <div id="Device" style="width: 99.53%; height: 100%">
                    <table id="tblDeviceInfo" border="0" rules="cols" width="100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="GridItems" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false" SkinID="DetailPageGridView" AllowSorting="False" EnableModelValidation="True">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />
                                    <HeaderStyle />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdoItems" runat="server" Class="callers" Enabled="True" Visible="True" onclick="checkRadioBtnDevice(this);"></asp:RadioButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manufacturer" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManufacturer" runat="server" Text='<%# Eval("Manufacturer")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="device_Type" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeviceType" runat="server" Text='<%# Eval("DeviceType")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="purchased_date" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurchasedDate" runat="server" Text='<%# Eval("PurchasedDate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="purchase_price" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurchasePrice" runat="server" Text='<%# Eval("PurchasePrice")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IMEI_NUM" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblImeiNo" runat="server" Text='<%# Eval("ImeiNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SERIAL_NUM" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REGISTERED_ITEM" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegisteredItem" runat="server" Text='<%# Eval("RegisteredItemName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                    </table>
                    <br />
                    <table>
                        <tbody>
                            <tr style="background-color: #f2f2f2">
                                <td>
                                    <asp:Button ID="btnNewCertRegItem_WRITE" runat="server" SkinID="PrimaryRightButton"
                                        Text="Register_New_Item" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="btnZone">
                <asp:Button ID="btn_Device_Cont" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                <asp:Button ID="btn_Device_SaveExit" TabIndex="190" runat="server" Text="Save_Exit" SkinID="PrimaryRightButton" Visible="True" />
                <asp:LinkButton ID="btn_Device_Cancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
            </div>
        </asp:View>

        <asp:View ID="vQuestion" runat="server">
            <div class="dataContainer">
                <Elita:UserControlQuestion runat="server" ID="questionUserControl" />
                <div class="btnZone" runat="server">
                    <asp:Button ID="btn_Quest_Cont" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                    <asp:Button ID="btn_Quest_SaveExit" TabIndex="190" runat="server" Text="Save_Exit" SkinID="PrimaryRightButton" Visible="true" />
                    <asp:LinkButton ID="btn_Quest_Cancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                        OnClientClick="return revealModal('ModalCancel');" />
                </div>
            </div>

        </asp:View>

        <asp:View ID="vTroubleShooting" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader" runat="server" id="hTroubleShooting">
                    <asp:Label runat="server" ID="lblTroubleShooting" Text="CLR_TROUBLESHOOTING" /></h2>
                <div style="width: 100%">
                    <table id="tblTroubleShooting" class="formGrid" width="100%" border="0" runat="server">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td align="right" style="width: 40%">
                                            <asp:Label ID="Label1" runat="server">TROUBLE_SHOOTING_Q1</asp:Label>
                                        </td>
                                        <td align="left" style="width: 15%">
                                            <asp:RadioButton ID="rdoTbYes" runat="server" GroupName="TroubleShooting" Checked="True"></asp:RadioButton>
                                        </td>
                                        <td align="left" style="width: 15%">
                                            <asp:RadioButton ID="rdoTbNo" runat="server" GroupName="TroubleShooting"></asp:RadioButton>
                                        </td>
                                        <td align="left" style="width: 30%">
                                            <asp:RadioButton ID="rdoTbSkipped" runat="server" GroupName="TroubleShooting"></asp:RadioButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="btnZone">
                <asp:Button ID="ButtonTBcontinue" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                <asp:Button ID="ButtonTBSaveExit" TabIndex="190" runat="server" Text="Save_Exit" SkinID="PrimaryRightButton" Visible="false" />
                <asp:LinkButton ID="LinkButtonTBCancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
            </div>
        </asp:View>
        <asp:View ID="vBestReplacementDevice" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="Label2" runat="server">SELECT_BEST_REPLACEMENT_DEVICE</asp:Label></h2>
                <div>
                    <table width="100%" class="dataGrid">
                        <tr id="trPageSize" runat="server">
                            <td class="bor" align="left">
                            </td>
                            <td class="bor" align="right">
                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%" class="repContainer">
                    <asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_OnItemDataBound" OnItemCommand="rep_OnItemCommand" OnInit="rep_OnInit">
                                    <ItemTemplate>
                                        <table class="dataRep" style="width: 100%; border-collapse: collapse" cellspacing="0" rules="all" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <div id="sub-title">
                                                            <div id="sub-left">
                                                                <asp:Panel runat="server" ID="panelCurrentDevice">
                                                                    <asp:Label ID="lblCurrentDevice" runat="server" Font-Bold="true" CssClass="highlightText"/>
                                                                    <br />
                                                                </asp:Panel>
                                                                <asp:Label ID="lblDeviceDescription1" runat="server" Font-Bold="true"/>
                                                                <br />
                                                                <asp:Label ID="lblDeviceDescription2" runat="server" />

                                                            </div>
                                                            <div id="sub-right" style="text-align: right; vertical-align: top;">
                                                                <asp:Label ID="lblQuantity" runat="server" Font-Bold="true"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblDeviceShipped" runat="server"></asp:Label> &nbsp;
                                                                <asp:LinkButton CommandName="SelectDeviceAction" ID="btnSelectDevice" runat="server" Visible="true"
                                                                    OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                                                            </div>
                                                            <div class="clear-both"></div>
                                                        </div>
                                                        <asp:Literal runat="server" ID="ltlDeviceSeparator" Mode="PassThrough"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                </div>
            </div>
            <div class="btnZone">
            
                <asp:Button ID="btnBestReplacementNotSelectedContinue" runat="server" SkinID="PrimaryRightButton" Text="BR_NOT_SELECTED_CONTINUE" />
            <asp:LinkButton ID="LinkButtonBestReplacementCancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
            </div>
        </asp:View>
        <asp:View ID="vFulfillmentOptions" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="lblFulfillmentOptions" runat="server" Text="FULFILLMENT_OPTIONS"></asp:Label></h2>
                <div style="width: 100%">
                    <table id="tblFulfillmentOptions" border="0" rules="cols" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblFulfillmentOptionNoRecordsFound" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewFulfillmentOptions" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
                                    AllowSorting="false" ShowHeader="false" GridLines="None" BorderStyle="None"
                                    BorderWidth="0" BorderColor="Transparent" SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />
                                    <Columns>
                                        <asp:BoundField DataField="Code" Visible="false" />
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdoFulfillmentOptions" runat="server" Visible="True"
                                                    OnCheckedChanged="rdoFulfillmentOptions_CheckedChanged" AutoPostBack="true" Checked='<%#Eval("Selected")%>'></asp:RadioButton>
                                                <asp:Label ID="lblFulfillmentOptionCode" runat="server" Visible="false" Text='<%#Eval("Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="95%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFulfillmentOption" runat="server" Font-Bold="true" Text='<%#Eval("Name")%>'></asp:Label>
                                                &nbsp;
                                                <br />
                                                <asp:Label ID="lblFulfillmentOptionDesc"
                                                    runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                <br />
                                                <asp:GridView ID="GridViewFulfillmentOptionFee" runat="server" Width="40%" AutoGenerateColumns="False" AllowPaging="false"
                                                    AllowSorting="false" Visible="false" CssClass="formGrid" SkinID="DetailPageGridView"
                                                    OnRowDataBound="GridViewFulfillmentOptionFee_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ServiceTypeCode" HeaderText="FEE" ItemStyle-Width="70%" />
                                                        <asp:BoundField DataField="NetPrice" HeaderText="AMOUNT" ItemStyle-Width="30%" />
                                                    </Columns>
                                                    <PagerStyle />
                                                </asp:GridView>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                    <PagerStyle />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>

                <Elita:UserControlQuestion runat="server" ID="fulfillmentOptionQuestions" />

            </div>
            <div class="btnZone">
                <asp:Button ID="btnFulfillmentOptionsContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                <asp:LinkButton ID="LinkButtonFulfillmentOptionsCancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
            </div>
        </asp:View>
        <asp:View ID="vLogisticsOptions" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="lblLogisticsOptions" runat="server" Text="LOGISTICS_OPTIONS"></asp:Label></h2>
                <div style="width: 100%">
                    <table id="tblLogisticsOptions" class="dataGrid" border="0" rules="cols" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblLogisticsOptionNoRecordsFound" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblLogisticStageName" Font-Bold="true" runat="server"></asp:Label><br />
                                <asp:Label ID="lblLogisticStageDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewLogisticsOptions" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
                                    AllowSorting="false" ShowHeader="false" GridLines="None" BorderStyle="None"
                                    BorderWidth="0" BorderColor="Transparent" SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdoLogisticsOption" runat="server" Visible="True"
                                                    OnCheckedChanged="rdoLogisticsOption_CheckedChanged" AutoPostBack="true" Checked='<%#Eval("Selected")%>'></asp:RadioButton>
                                                <asp:Label ID="lblLogisticsOptionCode" runat="server" Visible="false" Text='<%#Eval("Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="95%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table id="tblLogisticsOptionsDetail" class="formGrid" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblLogisticsOption" runat="server" Font-Bold="true" Text='<%#Eval("Name")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblLogisticsOptionDesc"
                                                                runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trShippingAddress">
                                                        <td align="left">
                                                            <table id="tblShippingAddress" class="formGrid" cellpadding="0" cellspacing="0">
                                                                <tbody>
                                                                    <tr runat="server" id="trStoreNumber">
                                                                        <td align="left" colspan="4">
                                                                            <asp:Label runat="server" ID="lblStoreNumber" />&nbsp;&nbsp;
                                                                            <asp:TextBox ID="txtStoreNumber" runat="server" MaxLength="20" SkinID="smallTextBox"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="4">
                                                                            <asp:Label runat="server" ID="lblLoShippingAddress" />
                                                                        </td>
                                                                    </tr>
                                                                    <Elita:UserControlAddressInfo ID="ucAddressControllerLogisticsOptions" runat="server"></Elita:UserControlAddressInfo>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trDeliveryOptions">
                                                        <td align="left">
                                                            <table id="tblDeliveryOptions" class="formGrid" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblDeliveryOptions" runat="server" Font-Bold="true"></asp:Label>&nbsp;
                                                                        <asp:Label ID="lblDeliveryDate" runat="server"></asp:Label>:&nbsp;&nbsp;
                                                                        <asp:Button ID="btnEstimateDeliveryDate" runat="server" SkinID="AlternateRightButton" Enabled="false" OnClick="btnEstimateDeliveryDate_OnClick" CommandArgument="<%#Container.DisplayIndex %>" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <%--<asp:DropDownList ID="ddlDeliveryDates" runat="server" SkinID="SmallDropDown" Enabled="false"></asp:DropDownList>&nbsp;--%>
                                                                        <Elita:UserControlDeliverySlot ID="UCDeliverySlotLogisticOptions" runat="server" Visible="false"></Elita:UserControlDeliverySlot>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                </table>
                                                
                                                <br/>
                                                <Elita:UserControlQuestion runat="server" ID="logisticsOptionsQuestions"/>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                    <PagerStyle />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                
                

            </div>
            <div class="btnZone">
                <asp:Button ID="btnLogisticsOptionsBack" runat="server" SkinID="AlternateLeftButton" Text="Back" />
                <asp:Button ID="btnLogisticsOptionsContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                <asp:LinkButton ID="LinkButtonLogisticsOptionsCancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
            </div>
        </asp:View>
        <asp:View ID="vShippingAddress" runat="server">
            <div id="dvStep4" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader" runat="server" id="ShippingAddress">
                        <asp:Label runat="server" ID="LabelShippingAddress" Text="DCM_SHIPPING_ADDRESS" /></h2>
                    <div class="stepformZone">
                        <table id="tblShippingAddressDetail" class="formGrid" cellpadding="0" cellspacing="0" style="border: 1px solid;" width="80%">
                            <tr>
                                <td align="right" nowrap="nowrap" width="30%">
                                    <asp:Label runat="server" ID="LabelSelectShippingAddress" Text="DCM_SELECT_SHIPPING_ADDRESS" />
                                    :
                                </td>
                                <td align="left" nowrap="nowrap" width="70%">
                                    <asp:RadioButton ID="RadioButtonBillingAddress" runat="server" AutoPostBack="True" Text="DCM_BILLING_ADDRESS"
                                        GroupName="SELECT_TYPE"></asp:RadioButton>&nbsp;&nbsp;
                                     <asp:RadioButton ID="RadioButtonOtherAddress" runat="server" AutoPostBack="True"
                                        Text="DCM_OTHER_ADDRESS" GroupName="SELECT_TYPE"></asp:RadioButton>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%"></td>
                                <td width="70%" align="left">
                                    <table id="tblAddress" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody align="left">
                                            <Elita:UserControlAddressInfo ID="moAddressController" runat="server" Visible="false"></Elita:UserControlAddressInfo>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trDeliveryDates" runat="server">
                                <td align="right" width="30%">
                                    <asp:Label runat="server" ID="lblGetDeliveryDates" text="DELIVERY_DATE_TEXT" />
                                </td>
                                <td align="left" nowrap="nowrap" width="70%">
                                    <asp:Button ID="btnGetDeliveryDate" runat="server" SkinID="AlternateLeftButton" Text="GET_DELIVERY_DATE" />
                                </td>
                            </tr>
                            <tbody>
                                <Elita:UserControlDeliverySlot ID="UserControlDeliverySlot" runat="server" Visible="false"></Elita:UserControlDeliverySlot>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="btnZone">
                <asp:Button ID="ButtonShippingAddressContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                <asp:Button ID="ButtonShippingAddressSaveExit" TabIndex="190" runat="server" Text="Save_Exit" SkinID="PrimaryRightButton" Visible="false" />
                <asp:LinkButton ID="LinkButtonShippingAddressCancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
            </div>
        </asp:View>
    </asp:MultiView>

</asp:Content>
