<%@ Control Language="vb"
    AutoEventWireup="false"
    CodeBehind="UserControlServiceCenterSelection.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlServiceCenterSelection"
    EnableTheming="true" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="~/Common/MultipleColumnDDLabelControl_New.ascx" %>

<div class="dataContainer">

    <h2 class="dataGridHeader" runat="server">
        <asp:Label runat="server" ID="moSearchServiceCenterLabel" Text="SEARCH_SERVICE_CENTER" />
    </h2>
    <div class="dataGridHeader">
        <table border="0" class="searchGrid" runat="server">
            <tbody>
                <tr>
                    <td align="right" nowrap="nowrap">
                        <asp:Label runat="server" ID="moSearchByLabel" Text="SEARCH_BY" />
                        :
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList runat="server" ID="moSearchByDrop" AutoPostBack="True" />
                    </td>
                    <td class="padLeft60" nowrap="nowrap" runat="server" id="tdCountryLabel">
                        <asp:Label runat="server" ID="moCountryLabel" Text="COUNTRY" />
                        :
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="moCountryDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td nowrap="nowrap" runat="server" id="tdCityLabel">
                        <asp:Label runat="server" ID="moCityLabel" Text="CITY" />
                        :
                    </td>
                    <td nowrap="nowrap" runat="server" id="tdCityTextBox">
                        <asp:TextBox ID="moCityTextbox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td nowrap="nowrap" runat="server" id="tdPostalCodeLabel">
                        <asp:Label runat="server" ID="moPostalCodeLabel" Text="CUST_POSTAL_CODE" />
                        :
                    </td>
                    <td nowrap="nowrap" runat="server" id="tdPostalCodeText">
                        <asp:TextBox ID="moPostalCodeTextbox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td runat="server" id="tdClearButton">
                        <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                    </td>
                    <td runat="server" id="tdSearchButton">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchButton"></asp:Button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="dataGridHeader">
        <table width="100%" class="dataGrid">
            <tr id="trPageSize" runat="server">
                <td class="bor" align="left">
                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true" SkinID="SmallDropDown">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="30" Selected="True">30</asp:ListItem>
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

        <asp:GridView ID="GridServiceCenter" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" SkinID="DetailPageGridView"
            AllowSorting="True" EnableModelValidation="True">
            <SelectedRowStyle Wrap="True" />
            <EditRowStyle Wrap="True" />
            <AlternatingRowStyle Wrap="True" />
            <RowStyle Wrap="True" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblServiceCenterId" runat="server" Text='<%#Eval("ServiceCenterId")%>' Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:RadioButton ID="rdoServiceCenter" runat="server" OnCheckedChanged="rdoServiceCenter_OnCheckedChanged" AutoPostBack="true" Checked='<%#Eval("Selected")%>'></asp:RadioButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_NAME">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_ADDRESS">
                    <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address1")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_CITY">
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_ZIP">
                    <ItemTemplate>
                        <asp:Label ID="lblZip" runat="server" Text='<%#Eval("PostalCode")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("ServiceCenterCode")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_MANUF_AUTH">
                    <ItemTemplate>
                        <img id="moManufacturerAuthFlagImage" runat="server" width="14" height="13" src="..\App_Themes\Default\Images\tickIcon.png" complete="complete"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_PREF">
                    <ItemTemplate>
                        <asp:Label ID="lblPref" runat="server" Text='<%#Eval("Pref")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SERVICE_CENTER_RANK">
                    <ItemTemplate>
                        <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Rank")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
            <PagerStyle />
        </asp:GridView>
    </div>
</div>
