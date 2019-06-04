<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="WarrantyMasterSearchForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.WarrantyMasterSearchForm" %>
<%@ Register src="../Common/MultipleColumnDDLabelControl.ascx" tagname="MultipleColumnDDLabelControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
        <tr>
            <td valign="top" colspan="2">
                <table id="moTableSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                    height: 76px" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                    bgcolor="#f1f1f1" border="0">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td valign="top" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                            <tr>
                                                <td align="left" valign="middle" colspan="4">
                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                </td>
                                            </tr>                                            
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="height: 1px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle">
                                                    <asp:Label ID="lblSearchSKU" runat="server">SKU_NUMBER</asp:Label>:
                                                </td>
                                                <td style="vertical-align: middle">
                                                    <asp:Label ID="lblSearchManufacturer" runat="server">MANUFACTURER_NAME</asp:Label>:
                                                </td>
                                                <td style="vertical-align: middle;">
                                                    <asp:Label ID="lblSearchModel" runat="server">MODEL</asp:Label>:
                                                </td>
                                                <td style="vertical-align: middle;">
                                                    <asp:Label ID="Label1" runat="server">WARRANTY_TYPE</asp:Label>:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle;" align="left" width="20%">
                                                    <asp:TextBox ID="tbSearchSKU" runat="server" Columns="25" Width="65%" AutoPostBack="False"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align: middle;" align="left" width="30%">
                                                    <asp:TextBox ID="tbSearchManufacturer" runat="server" Columns="35" AutoPostBack="False" ></asp:TextBox>
                                                </td>
                                                <td style="vertical-align: middle;text-align:left" width="30%" align="left">
                                                    <asp:TextBox ID="tbSearchModel" runat="server" Columns="35" AutoPostBack="False"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align: middle;text-align:left" width="20%" align="left">
                                                    <asp:DropDownList runat="server" AutoPostBack="false" ID="ddlWarrantyType" Width="90px">
                                                        <asp:ListItem Value="" Selected></asp:ListItem>
                                                        <asp:ListItem Value="0" >PRP</asp:ListItem>
                                                        <asp:ListItem Value="1" >PSP</asp:ListItem>
                                                        <asp:ListItem Value="2" >MFG</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>                                            
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="height: 1px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap align="left">
                                                    <asp:CheckBox ID="chkOrderByRiskType" runat="server" Checked="false" />
                                                    <asp:Label ID="Label2" runat="server">ORDER_BY</asp:Label>&nbsp;
                                                    <asp:Label ID="Label3" runat="server">RISK_TYPE</asp:Label>
                                                </td>
                                                <td style="text-align: right" colspan="3">                                                    
                                                        <asp:Button ID="moBtnClear" runat="server" Width="90px" 
                                                            Text="Clear" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" Height="20px"></asp:Button>&nbsp;
                                                        <asp:Button ID="moBtnSearch" runat="server" Width="90px" 
                                                            Text="Search" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" Height="20px"></asp:Button>&nbsp;                                                     
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="server">
            <td valign="top" align="left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
            <td style="height: 22px; text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataGrid ID="moWarrantyMasterGrid" runat="server" Width="100%"
                    AllowPaging="True" AutoGenerateColumns="False" AllowSorting="false" CssClass="DATAGRID">
                    <SelectedItemStyle CssClass="SELECTED"></SelectedItemStyle>
                    <EditItemStyle CssClass="EDITROW_WRAP"></EditItemStyle>
                    <AlternatingItemStyle  CSSClass="ALTROW"></AlternatingItemStyle>
                    <ItemStyle BackColor="White" CSSClass="ROW"></ItemStyle>
                    <HeaderStyle   CSSClass="HEADER" ></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="EditRecord" ImageUrl="../Navigation/images/icons/edit2.gif" />                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                       <asp:TemplateColumn Visible="False" HeaderText="Id">
                            <ItemTemplate>
                                &gt;
                                <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("WARRANTY_MASTER_ID"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DEALER">
                            <ItemTemplate>
                                <asp:Label ID="lblDealer" runat="server" Visible="True" Text='<%# Container.DataItem("DEALER_CODE")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SKU_NUMBER" SortExpression="SKU_NUMBER">
                            <ItemTemplate>
                                <asp:Label ID="lblSKUNum" runat="server" Visible="True" Text='<%# Container.DataItem("SKU_NUMBER")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SKU_DESCRIPTION">
                            <ItemTemplate>
                                <asp:Label ID="lblSKUDesc" runat="server" Visible="True" Text='<%# Container.DataItem("SKU_DESCRIPTION")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MANUFACTURER_NAME">
                            <ItemTemplate>
                                <asp:Label ID="lblMfgName" runat="server" Visible="True" Text='<%# Container.DataItem("MANUFACTURER_NAME")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MODEL">
                            <ItemTemplate>
                                <asp:Label ID="lblMode" runat="server" Visible="True" Text='<%# Container.DataItem("MODEL_NUMBER")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="WARRANTY_DURATION_PARTS">
                            <ItemTemplate>
                                <asp:Label ID="lblParts" runat="server" Visible="True" Text='<%# Container.DataItem("WARRANTY_DURATION_PARTS")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="WARRANTY_DURATION_LABOR">
                            <ItemTemplate>
                                <asp:Label ID="lblLabor" runat="server" Visible="True" Text='<%# Container.DataItem("WARRANTY_DURATION_LABOR")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="WARRANTY_TYPE">
                            <ItemTemplate>
                                <asp:Label ID="lblWarrType" runat="server" Visible="True" Text='<%# Container.DataItem("WARRANTY_TYPE")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="RISK_TYPE" SortExpression="RISK_TYPE">
                            <ItemTemplate>
                                <asp:Label ID="lblRiskType" runat="server" Visible="True" Text='<%# Container.DataItem("RISK_TYPE")%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="ddlRiskType" runat="server" Visible="True" Width="200px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>                                                    
                        <asp:TemplateColumn HeaderText="IS_DELETED">
                            <ItemTemplate>
                                <asp:Label ID="lblIsDeleted" runat="server" Visible="True" Text='<%# Container.DataItem("IS_DELETED")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle CssClass="PAGER" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntButtons" runat="server" ContentPlaceHolderID="ContentPanelButtons">
    <asp:Button ID="SaveButton_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="Save" Visible="false" Enabled="true"></asp:Button>
    <asp:Button ID="CancelButton" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Cancel" Visible="false"></asp:Button>   
</asp:Content>


