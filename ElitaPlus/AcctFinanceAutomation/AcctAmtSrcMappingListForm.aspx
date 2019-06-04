<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master" CodeBehind="AcctAmtSrcMappingListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AcctAmtSrcMappingListForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
     <table id="Table1" class="searchGrid" border="0" cellpadding="0" cellspacing="0" width= "100%" runat="server">
        <tbody>
            <tr>
                <td style="width:10%; white-space:nowrap;">
                    <asp:Label ID="lblDealer" runat="server">Dealer</asp:Label>:
                    <asp:DropDownList ID="ddlDealer" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:275px;"></asp:DropDownList>
                </td>
                <td  style="padding-left:0px;">
                    <asp:Button runat="server" ID="btnClear" SkinID="AlternateLeftButton" Text="CLEAR" />
                    <asp:Button runat="server" ID="btnSearch" SkinID="SearchLeftButton" Text="SEARCH" />                    
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults" visible="false">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="lblResultMapped">AMT_SRC_MAPPED</asp:Label>:
            <asp:Label runat="server" ID="lblMappedCnt" Font-Bold="true"></asp:Label>
        </h2>
        <asp:GridView ID="gridFieldMapped" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="True" HeaderText="FIELD_TYPE" >
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="LinkButton1" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument='<%# New Guid(CType(Container.DataItem("ACCT_AMT_SRC_ID"), Byte())).ToString  %>'
                            Text='<%#Container.DataItem("AmountSource")%>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COV_ENTITY_BY_REGION">
                    <ItemTemplate>
                        <asp:Label ID="lblEntityByRegion" Text='<%#Container.DataItem("ENTITY_BY_REGION")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="ENTITY_BY_REGION_COVERAGE_TYPE" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <asp:Label ID="lblEntityByRegionCovType" Text='<%#Container.DataItem("EBR_COVType_Desc")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField Visible="True" HeaderText="RECONCILE_WITH_INVOICE_AMT" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <asp:Label ID="lblInvRecon" Text='<%#Container.DataItem("reconcil_with_invoice_desc")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="USE_FORMULA_FOR_CLIP">
                    <ItemTemplate>
                        <asp:Label ID="lblUseFormulaForCLIP" Text='<%#Container.DataItem("use_formula_for_clip")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="GENERIC_MAPPING">
                    <ItemTemplate>
                        <asp:Label ID="lblGenericMapping" Text='<%# Container.DataItem("GenericMapping") %>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PRODUCT_MAPPED_INDIVIDIALLY">
                    <ItemTemplate>
                        <asp:Label ID="lblProdMapped" runat="server" Text='<%# Container.DataItem("ProductMapped") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="lblResultNotMapped">AMT_SRC_NOT_MAPPED</asp:Label>:
            <asp:Label runat="server" ID="lblNotMappedCnt" Font-Bold="true"></asp:Label>
        </h2>
        <asp:GridView ID="gridFieldNotMapped" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="True" HeaderText="FIELD_TYPE">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="btnSelect" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument='<%# New Guid(CType(Container.DataItem("FIELD_TYPE_ID"), Byte())).ToString%>'
                            Text='<%# Container.DataItem("AmountSource") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>
        </asp:GridView>
    </div>
    
</asp:Content>
