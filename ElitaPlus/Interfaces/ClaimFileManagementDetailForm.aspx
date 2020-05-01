<%@ Page Title="Claim File Management Detail Form" Language="vb" ValidateRequest="false" AutoEventWireup="false"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" CodeBehind="ClaimFileManagementDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ClaimFileManagementDetailForm" Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript">
        $(function () {
            $("[id*=imgShowHideGridInfo]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
       });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label>
                        &nbsp;
                       <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                           SkinID="SmallDropDown">
                           <asp:ListItem Value="5">5</asp:ListItem>
                           <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
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
                        <asp:Label ID="lblRecordCount" runat="server">RECORD_COUNT</asp:Label>:&nbsp
                        <asp:Label ID="lblRecordCountValue" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%" style="margin-left: 0%">
            <h2 class="dataGridHeader">
                <asp:Label runat="server" ID="Label1">SEARCH_RESULTS</asp:Label>
            </h2>
            <div class="Page" runat="server" id="moDataGridPage" style="height: 100%; overflow: auto; width: 100%;">

                <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="true" AllowCustomPaging="true" AllowSorting="false" 
                    CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="10">
                    <SelectedRowStyle Wrap="true" />
                    <EditRowStyle Wrap="true" />
                    <AlternatingRowStyle Wrap="true" />
                    <RowStyle Wrap="true" />
                    <HeaderStyle Wrap="False"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgShowHideGridInfo" runat="server" OnClick="ShowHideGridInfo_Click"
                                    ImageUrl="~/Navigation/images/icons/plus.png" CommandArgument="Show" />

                                <asp:Panel ID="pnlShowHideGridInfo" runat="server" visible="false" style="position: relative">

                                    <asp:Label runat="server" ID="FinancialLineItems" Text="FINANCIAL_LINE_ITEMS" ReadOnly="true"></asp:Label>:

                                    <asp:GridView ID="GridFinancialLineInfo" runat="server" AutoGenerateColumns="false" PageSize="5"
                                        AllowPaging="false" AllowSorting="false" Width="40%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ERROR_DETAIL" Visible="false">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
                                                    <asp:GridView ID="GridErrorsFinancialLineInfo" runat="server" Width="100%" AllowPaging="false" AllowSorting="false"
                                                        CellPadding="1" AutoGenerateColumns="false" SkinID="DetailPageGridView">
                                                        <SelectedRowStyle Wrap="true" />
                                                        <EditRowStyle Wrap="true" />
                                                        <AlternatingRowStyle Wrap="true" />
                                                        <RowStyle Wrap="true" />
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ERROR_CODE">
                                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="FliErrorCode" runat="server" Visible="True" Text='<%#DataBinder.Eval(Container.DataItem, "Code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ERROR_MESSAGE">
                                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="FliErrorMesage" runat="server" Visible="True" Text='<%#DataBinder.Eval(Container.DataItem, "Message") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FINANCIAL_ITEM_IDENTIFICATION">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FinancialItemIdentification" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FINANCIAL_ITEM_IDENTIFICATION_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FinancialItemIdentificationQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FINANCIAL_ITEM_SUB_IDENTIFICATION">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FinancialItemSubIdentification" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FINANCIAL_ITEM_SUB_IDENTIFICATION_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FinancialItemSubIdentificationQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FINANCIAL_ITEM_ADDITIONAL_IDENTIFICATION">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FinancialItemAdditionalIdentification" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FINANCIAL_ITEM_ADDITIONAL_IDENTIFICATION_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FinancialItemAdditionalIdentificationQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AMOUNT">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Amount" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CURRENCY_CODE">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="CurrencyCode" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AMOUNT_DECIMAL_SEPARATOR">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="AmountDecimalSeparator" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ORIGINAL_AMOUNT">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="OriginalAmount" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ORIGINAL_CURRENCY_CODE">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="OriginalCurrencyCode" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_RATE">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>

			                                        <asp:TextBox ID="ConversionRate" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_DATE">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="ConversionDate" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_DATE_FORMAT">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="ConversionDateFormat" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_TIME">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="ConversionTime" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_TIME_TIMEZONE">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="ConversionTimeTimezone" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:Label runat="server" ID="AdditionalInformationItems" Text="ADDITIONAL_INFORMATION_ITEMS" ReadOnly="true"></asp:Label>:

                                    <asp:GridView ID="GridAdditionalInfo" runat="server" AutoGenerateColumns="false" PageSize="5"
                                        AllowPaging="false" AllowSorting="false" Width="20%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ERROR_DETAIL" Visible="false">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
                                                    <asp:GridView ID="GridErrorsAdditionalInfo" runat="server" Width="100%" AllowPaging="false" AllowSorting="False"
                                                        CellPadding="1" AutoGenerateColumns="false" SkinID="DetailPageGridView">
                                                        <SelectedRowStyle Wrap="true" />
                                                        <EditRowStyle Wrap="true" />
                                                        <AlternatingRowStyle Wrap="true" />
                                                        <RowStyle Wrap="true" />
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ERROR_CODE">
                                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AifErrorCode" runat="server" Visible="True" Text='<%#DataBinder.Eval(Container.DataItem, "Code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ERROR_MESSAGE">
                                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AifErrorMesage" runat="server" Visible="True" Text='<%#DataBinder.Eval(Container.DataItem, "Message") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ADDITIONAL_INFORMATION">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="AdditionalInformation" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ADDITIONAL_INFORMATION_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="AdditionalInformationQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                           <ItemTemplate>
                               <asp:ImageButton runat="server" ID="imgSaveRecord" ImageUrl="~/Navigation/images/icons/save_icon.gif"
                                   CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>" Visible="true" />
                           </ItemTemplate>
                       </asp:TemplateField>

                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="RecordIdentifier" Text='<%#DataBinder.Eval(Container.DataItem, "Locator.Identifier") %>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ERROR_DETAIL" Visible="false">
                            <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                    <ItemTemplate>
                                <asp:GridView ID="GridErrorsClaimRecord" runat="server" Width="100%" AllowPaging="false" AllowSorting="false"
                                    CellPadding="1" AutoGenerateColumns="false" SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="true" />
                                    <EditRowStyle Wrap="true" />
                                    <AlternatingRowStyle Wrap="true" />
                                    <RowStyle Wrap="true" />
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="ERROR_CODE">
                                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="FliErrorCode" runat="server" Visible="True" Text='<%#DataBinder.Eval(Container.DataItem, "Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ERROR_MESSAGE">
                                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="FliErrorMesage" runat="server" Visible="True" Text='<%#DataBinder.Eval(Container.DataItem, "Message") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
		                    </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CertificateIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CertificateIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_SUB_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CertificateSubIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_SUB_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CertificateSubIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_ADDITIONAL_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CertificateAdditionalIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_ADDITIONAL_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CertificateAdditionalIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOSS_DATE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="LossDate" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOSS_DATE_FORMAT">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="LossDateFormat" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOSS_TIME_START">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="LossTimeStart" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOSS_TIME_END">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="LossTimeEnd" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOSS_TIME_ZONE_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="LossTimeZoneCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ItemQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ItemIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ItemIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_SUB_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ItemSubIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_SUB_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ItemSubIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_ADDITIONAL_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ItemAdditionalIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_ADDITIONAL_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ItemAdditionalIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CoverageTypeIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CoverageTypeIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_SUB_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CoverageTypeSubIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_SUB_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CoverageTypeSubIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_ADDITIONAL_IDENTIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CoverageTypeAdditionalIdentifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_ADDITIONAL_IDENTIFIER_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="CoverageTypeAdditionalIdentifierQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_SKU">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemSku" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_MAKE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemMake" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_MODEL">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemModel" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_COLOR">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemColor" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_CAPACITY">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemCapacity" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_CARRIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemCarrier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_IDENTIFICATION">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemIdentification" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_IDENTIFICATION_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemIdentificationQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_ADDITIONAL_IDENTIFICATION">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemAdditionalIdentification" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIMED_ITEM_ADDITIONAL_IDENTIFICATION_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ClaimedItemAdditionalIdentificationQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PROBLEM_STATEMENT">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="ProblemStatement" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <PagerSettings Mode="Numeric" Position="Bottom" />
                    <PagerStyle HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnBack" runat="server" Text="BACK" CausesValidation="False"
                SkinID="AlternateLeftButton"></asp:Button>&nbsp;
            <asp:Button ID="SaveButton_WRITE" runat="server" Text="SAVE" CausesValidation="False"
                SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        </div>
    </div>
</asp:Content>
