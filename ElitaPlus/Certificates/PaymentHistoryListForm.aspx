<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentHistoryListForm.aspx.vb" Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.PaymentHistoryListForm" MasterPageFile="~/Navigation/masters/ElitaBase.Master"%>

<%@ Register TagPrefix="uc1" TagName="moCertificateInfoController" Src="UserControlCertificateInfo_New.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="80%" class="summaryGrid">
                <uc1:moCertificateInfoController id="moCertificateInfoController" runat="server" align="center"></uc1:moCertificateInfoController>
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:Panel runat="server" ID="WorkingPanel">

    <div class="dataContainer">

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="middle" width="100%">
                <hr style="height: 1px">
            </td>
        </tr>
        <tr>
            <td>
               <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
               <asp:HiddenField ID="hdnDisabledTab" runat="server"></asp:HiddenField>
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
                                            <td nowrap="" colspan="2" style="text-align: right; vertical-align: middle">
                                                <asp:Label runat="server" ID="moBillingTotalLabel" Font-Bold="false" Text="TOTAL_PAYMENT_AMOUNT"></asp:Label>:&nbsp;
                                    <asp:TextBox runat="server" ReadOnly="true" ID="moBillingTotalText" Text="" TabIndex="1" CssClass="FLATTEXTBOX"></asp:TextBox>
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
                                    <asp:DataGrid ID="Grid" runat="server" AllowPaging="true" AllowSorting="true" CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#F7F7F7" BorderStyle="Solid" BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="false" Height="16px" SkinID="DetailPageDataGrid">

                                        <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                        <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                        <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                        <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                        <FooterStyle BorderStyle="Double"></FooterStyle>

                                        <Columns>
                                            <asp:TemplateColumn HeaderText="SERIAL_NUMBER" SortExpression="SERIAL_NUMBER">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="COVERAGE_SEQUENCE" SortExpression="COVERAGE_SEQ">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="xx" Width="10"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PAID_FROM" SortExpression="to_char(cp.DATE_PAID_FROM,'YYYYMMDD')">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PAID_TO" SortExpression="to_char(cp.DATE_PAID_FOR,'YYYYMMDD')">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="DATE_OF_PAYMENT" SortExpression="to_char(cp.DATE_OF_PAYMENT,'YYYYMMDD')">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="DATE_PROCESSED" SortExpression="to_char(cp.DATE_PROCESSED,'YYYYMMDD')">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PAYMENT AMOUNT" SortExpression="PAYMENT_AMOUNT">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="INCOMING_AMOUNT" SortExpression="INCOMING_AMOUNT">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="INSTALLMENT_NUMBER" SortExpression="INSTALLMENT_NUM">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PAYMENT_INFO" SortExpression="PAYMENT_INFO">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                                <ItemTemplate></ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="SOURCE" SortExpression="SOURCE">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="PAYMENT_REFERENCE_NUMBER" SortExpression="PAYMENT_REFERENCE_NUMBER">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                                <ItemTemplate></ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="10" Mode="NumericPages"></PagerStyle>
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

                                            <td nowrap="" colspan="2" style="text-align: right; vertical-align: middle">
                                                <asp:Label runat="server" ID="moCollectionTotalLabel" Font-Bold="false" Text="TOTAL_PAYMENT_AMOUNT"></asp:Label>:&nbsp;
                                    <asp:TextBox runat="server" ReadOnly="true" ID="moCollectedTotalText" Text="" TabIndex="1" Width="150px" CssClass="FLATTEXTBOX"></asp:TextBox>
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
                                                <asp:Label ID="lblRecordCount2" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr id="moCollectionInformation" runat="server">
                                <td style="text-align: center" align="center">
                                    <asp:DataGrid ID="CollectionGrid" runat="server" AllowPaging="true" AllowSorting="true" CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#F7F7F7" BorderStyle="Solid" BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="false" Height="16px" SkinID="DetailPageDataGrid">

                                        <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                        <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                        <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                        <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                        <FooterStyle BorderStyle="Double"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="BILLING_START_DATE" SortExpression="to_char(ccp.BILLING_START_DATE,'YYYYMMDD')">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                                <ItemTemplate></ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="COLLECTED_DATE" SortExpression="to_char(ccp.COLLECTED_DATE,'YYYYMMDD')">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                                <ItemTemplate></ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="DATE_PROCESSED" SortExpression="to_char(cp.CREATED_DATE,'YYYYMMDD')">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="COLLECTED_AMOUNT" SortExpression="COLLECTED_AMOUNT">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                <ItemTemplate></ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="INCOMING_AMOUNT" SortExpression="INCOMING_AMOUNT">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                <ItemTemplate></ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="INSTALLMENT_NUMBER" SortExpression="INSTALLMENT_NUM">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                                <ItemTemplate></ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="10" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                 </div>               
            </td>
        </tr>         
    </table>
    </div>
    
    <div class="btnZone">
        <input id="HiddenPayPromptResponse" type="hidden" name="HiddenPayPromptResponse" runat="server">
        <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
    </div>         
</asp:Panel>
</asp:Content>
