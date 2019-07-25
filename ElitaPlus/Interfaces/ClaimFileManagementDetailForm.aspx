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

                                            <asp:TemplateField HeaderText="ITEM_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliItemQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ITEM_CODE">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliItemCode" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ITEM_SUB_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliItemSubQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ITEM_SUB_CODE">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliItemSubCode" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ITEM_ADDITIONAL_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliItemAdditionalQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ITEM_ADDITIONAL_CODE">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliItemAdditionalCode" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CURRENCY">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliCurrency" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AMOUNT">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliAmount" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DECIMAL_SEPARATOR">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="FliDecimalSeparator" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ORIGINAL_CURRENCY">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliOriginalCurrency" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ORIGINAL_AMOUNT">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="FliOriginalAmount" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CURRENCY_CONVERSION">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>

			                                        <asp:TextBox ID="FliConversionRate" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_DATE">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="FliConversionDate" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_DATE_FORMAT">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="FliConversionDateFormat" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CONVERSION_TIME">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="FliConversionTime" runat="server" Visible="True"/>
		                                        </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TIME_ZONE_CODE">
                                                <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                        <ItemTemplate>
			                                        <asp:TextBox ID="FliConversionTimeTimezone" runat="server" Visible="True"/>
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

                                            <asp:TemplateField HeaderText="ITEM_QUALIFIER">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="AifQualifier" runat="server" Visible="True"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="INFORMATION">
                                                <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="AifInformation" runat="server" Visible="True"></asp:TextBox>
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

                        <asp:TemplateField HeaderText="CERTIFICATE_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCertificateQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCertificateCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_SUB_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCertificateSubQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_SUB_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCertificateSubCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_ADDITIONAL_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCertificateAdditionalQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CERTIFICATE_ADDITIONAL_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCertificateAdditionalCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOSS_DATE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordLossDate" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOSS_DATE_FORMAT">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordLossDateFormat" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="START_TIME">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordLossTimeStart" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="END_TIME">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordLossTimeEnd" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TIME_ZONE_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordLossTimezone" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordItemQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordItemCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_SUB_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordItemSubQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_SUB_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordItemSubCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_ADDITIONAL_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordItemAdditionalQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ITEM_ADDITIONAL_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordItemAdditionalCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCoverageTypeQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCoverageTypeCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_SUB_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCoverageTypeSubQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_SUB_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCoverageTypeSubCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_ADDITIONAL_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCoverageTypeAdditionalQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COVERAGE_TYPE_ADDITIONAL_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordCoverageTypeAdditionalCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SKU">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemSku" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MAKE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemMake" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MODEL">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemModel" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COLOR">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemColor" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CAPACITY">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemCapacity" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CARRIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemCarrier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIM_ITEM_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIM_ITEM_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIM_ITEM_ADDITIONAL_QUALIFIER">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemAdditionalQualifier" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIM_ITEM_ADDITIONAL_CODE">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordClaimItemAdditionalCode" runat="server" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SOURCE_SYSTEM">
                            <HeaderStyle Wrap="True" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="1%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="RecordProblemStatement" runat="server" Visible="True"></asp:TextBox>
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
</asp:Content>
