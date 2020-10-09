<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/ElitaBase.Master" CodeBehind="BillingPaymentHistoryListForm.aspx.vb" Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.BillingPaymentHistoryListForm" %>

<%@ Register TagPrefix="uc1" TagName="moCertificateInfoController" Src="UserControlCertificateInfo_New.ascx" %>

<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="80%" class="summaryGrid">
        <uc1:moCertificateInfoController ID="moCertificateInfoController" runat="server" align="center" />
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:Panel runat="server" ID="WorkingPanel">
        <div class="dataContainer">

            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td valign="middle" width="100%">
                        <hr style="height: 1px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                         <asp:HiddenField ID="hdnDisabledTab" runat="server" />
                        <div id="tabs" class="style-tabs">
                            <ul>
                                <li><a href="#tabsBillingHistory" rel="noopener noreferrer">
                                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">BILLING_HISTORY</asp:Label></a></li>
                                <li><a href="#tabsPaymentCollected" rel="noopener noreferrer">
                                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">Payment_Collected</asp:Label></a></li>
                            </ul>

                            <div id="tabsBillingHistory">
                                    <table border="0" cellpadding="0" cellspacing="0" style="padding: 6px 10px 10px 10px; margin: 0px;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="formGrid" style="padding: 0px 0px 0px 0px;" backcolor="white">
                                            <tr>
                                                <td nowrap colspan="2" style="text-align: right; vertical-align: middle">
                                                    <asp:Label runat="server" ID="moBillingTotalLabel" Font-Bold="false" Text="TOTAL_BILLED_AMOUNT" />:&nbsp;
                                            <asp:TextBox runat="server" ReadOnly="true" ID="moBillingTotalText" Text="" TabIndex="1" CssClass="FLATTEXTBOX" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 22px; padding-left: 0px;" valign="top" align="left">
                                                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                                            <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true">
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
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
                                                <td style="height: 22px; text-align: right" align="center">
                                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr id="moBillingInformation" runat="server">
                                    <td style="text-align: center" align="center">
                                        <asp:DataGrid ID="Grid" runat="server" AllowPaging="true" AllowSorting="False" Width="100%"
                                            CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#F7F7F7" BorderStyle="Solid" OnItemCommand="ItemCommand"
                                            BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="false" Height="16px" SkinID="DetailPageDataGrid">
                                            <SelectedItemStyle Wrap="false" BackColor="LavenderBlush" />
                                            <EditItemStyle Wrap="false" BackColor="AliceBlue" />
                                            <AlternatingItemStyle Wrap="false" BackColor="#F1F1F1" />
                                            <ItemStyle Wrap="false" BackColor="White" />
                                            <FooterStyle BorderStyle="Double" />
                                            <Columns>
                                                <asp:TemplateColumn Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" Height="15px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" ImageUrl="~/App_Themes/Default/Images/edit.png" runat="server"
                                                            CommandName="SelectAction" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn Visible="false" HeaderText="ID" />
                                                <asp:TemplateColumn HeaderText="COVERAGE_SEQUENCE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="xx" Width="10" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCovSq" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="INSTALLMENT_NUMBER">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtInstallNumb" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="DATE_PROCESSED">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDtPro" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="BILLING_DATE">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDateAdded" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="FROM_DATE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtFrmDt" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="TO_DATE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtToDt" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="INCOMING_AMOUNT" SortExpression="INCOMING_AMOUNT">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemTemplate />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="BILLED_AMOUNT">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtBilledAmount" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="OPEN_AMOUNT">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtOpAmt" runat="server" Visible="true" Enabled="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Billing_Status">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="BillingStatusDD" AutoPostBack="true" OnSelectedIndexChanged="EnblDisblRjctCodeDD" runat="server" Visible="true" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="REJECT_CODE" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="RejectCodeDD" runat="server" Width="275px" Visible="true" Enabled="true" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="SOURCE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="PAID?" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnpaid" runat="server" Text="Pay" Width="40px" Visible="false" CommandName="PaidAction" />
                                                        <asp:Label ID="lblPaid" runat="server" Width="40px" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="INVOICE_NUMBER" SortExpression="INVOICE_NUMBER">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemTemplate />
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="10" Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                            </div>

                            <div id="tabsPaymentCollected">
                                <table border="0" cellpadding="0" cellspacing="0" style="padding: 6px 10px 10px 10px; margin: 0px;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="formGrid" style="padding: 0px 0px 0px 0px;" backcolor="white">
                                            <tr>
                                                <td nowrap colspan="2" style="text-align: right; vertical-align: middle">
                                                    <asp:Label runat="server" ID="moCollectionTotalLabel" Font-Bold="false" Text="TOTAL_PAYMENT_AMOUNT" />:&nbsp;
                                            <asp:TextBox runat="server" ReadOnly="true" ID="moCollectedTotalText" Text="" TabIndex="1" Width="150px" CssClass="FLATTEXTBOX" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 22px; padding-left: 0px;" valign="top" align="left">
                                                    <asp:Label ID="lblPageSize2" runat="server">Page_Size</asp:Label>: &nbsp;
                                            <asp:DropDownList ID="cboPageSize2" runat="server" Width="50px" AutoPostBack="true">
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
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
                                                <td style="height: 22px; text-align: right" align="center">
                                                    <asp:Label ID="lblRecordCount2" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr id="moCollectionInformation" runat="server">
                                    <td style="text-align: center" align="center">
                                        <asp:DataGrid ID="CollectionGrid" runat="server" AllowPaging="true" AllowSorting="true"
                                            CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#F7F7F7" BorderStyle="Solid" OnItemCommand="CollectionGrid_ItemCommand"
                                            BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="false" Height="16px" SkinID="DetailPageDataGrid">

                                            <SelectedItemStyle Wrap="false" BackColor="LavenderBlush" />
                                            <EditItemStyle Wrap="False" BackColor="AliceBlue" />
                                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1" />
                                            <ItemStyle Wrap="False" BackColor="White" />
                                            <FooterStyle BorderStyle="Double" />
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                                                            ImageUrl="../Navigation/images/icons/yes_icon.gif" runat="server" CausesValidation="false"></asp:ImageButton>
                                                        <asp:HiddenField ID="Payment_Type_XCD" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="ID" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="xx" Width="10" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="COVERAGE_SEQUENCE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="xx" Width="10" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="INSTALLMENT_NUMBER">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ItemTemplate />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="COLLECTED_DATE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="DATE_PROCESSED">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="PAYMENT_AMOUNT">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemTemplate />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="COLLECTED_AMOUNT">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemTemplate />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="PAYMENT_STATUS">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="DATE_SEND">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="PAYMENT_METHOD">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="PAYMENT_INSTRUMENT_NUMBER">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="REJECT_DATE">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtRejectDate" runat="server" Visible="True" Enabled="false">
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="processor_reject_code" HeaderText="REJECT_CODE"
                                                    ItemStyle-HorizontalAlign="Center" ReadOnly="true"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="REJECT_REASON">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="RejectCodeDD" runat="server" Width="275px" Visible="true" />
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="10" Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                     <tr style="background-color: #f2f2f2">
                                        <td>
                                            <asp:Button ID="btnAddRejectPayment" SkinID="PrimaryRightButton"  CausesValidation="false" runat="server" Text="REJECT_PAYMENT" />                                            
                                        </td>
                                    </tr>
                            </table>
                            </div>
                        </div>                      
                    </td>
                </tr>
            </table>
            <input id="HiddenPayResp" type="hidden" name="HiddenPayResp" runat="server" />
        </div>

        <div class="btnZone">
            <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton" />
            <asp:Button ID="btnSave_WRITE" runat="server" Text="SAVE" SkinID="AlternateLeftButton" />
            <asp:Button ID="btnUndo_WRITE" runat="server" Text="UNDO" SkinID="AlternateLeftButton" />
            <asp:Button ID="btnAddCheckPayment" SkinID="PrimaryRightButton" CausesValidation="false" runat="server" Text="ACCEPT_PAYMENT_BY_CHECK" />
        </div>
    </asp:Panel>
</asp:Content>
