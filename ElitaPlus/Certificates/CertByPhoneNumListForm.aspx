<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="CertByPhoneNumListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertByPhoneNumListForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th { text-align:center} 
        tr.LeftAlign td {text-align:left;padding-right:25px;}  
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center; padding-left:8px; padding-right:8px;">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;background-color:#f1f1f1; height:50px; width:100%; padding:0px;">
                    <tr class="LeftAlign">
                        <td style="width:15%;">
                            <asp:Label ID="lblPhoneType" runat="server">PHONE_NUMBER_TYPE</asp:Label>:<br />
                            <asp:dropdownlist id="ddlPhoneType" runat="server" Width="100%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="1"></asp:dropdownlist>
                        </td>
                        <td style="width:16%;">
                            <asp:Label ID="lblPhone" runat="server">PHONE_NUMBER</asp:Label>:<br />
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="100%" TabIndex="2"></asp:TextBox>
                        </td>
                        <td style="width:31%;">
                            <asp:Label ID="lblCustName" runat="server">Customer_Name</asp:Label>:<br />
                            <asp:TextBox ID="txtCustName" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="100%" TabIndex="3"></asp:TextBox>
                        </td>
                        <td style="padding-right:9px;">
                            <asp:Label ID="lblAddress" runat="server">Address</asp:Label>:<br />
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="100%" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="LeftAlign">
                        <td colspan="2">
                            <asp:Label ID="lblZip" runat="server">Zip</asp:Label>:<br />
                            <asp:TextBox ID="txtZip" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="100%" TabIndex="5"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblCertNum" runat="server">Certificate</asp:Label>:<br />
                            <asp:TextBox ID="txtCertNum" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="100%" TabIndex="6"></asp:TextBox>
                        </td>
                        <td style="padding-right:3px;">
                            <asp:Label ID="lblDealer" runat="server">Dealer</asp:Label>:<br />
                            <asp:dropdownlist id="ddlDealer" runat="server" Width="100%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="7"></asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;padding-right:2px;padding-bottom:5px" colspan="2">
                            <asp:Label ID="Label1" runat="server">Sort By</asp:Label>:<br />
                            <asp:DropDownList id="ddlSortBy" runat="server" Width="100%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="8"></asp:DropDownList>
                        </td>
                        <td style="text-align:right;vertical-align:text-bottom; padding-right:4px;padding-bottom:5px" colspan="2"><br />
                            <asp:Button ID="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear" TabIndex="10" />&nbsp;&nbsp;
                            <asp:Button ID="btnSearch" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" runat="server" Text="Search" TabIndex="9" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style="padding-top:5px; padding-bottom:5px;" colspan="2"><hr style="height:1px" /></td></tr>
        <tr id="trPageSize" runat="server" visible="False">
            <td align="left">
                <asp:label id="lblPageSize" runat="server">Page_Size:</asp:label>&nbsp;
                <asp:dropdownlist id="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                </asp:dropdownlist>
             </td>
             <td style="text-align:right">
                <asp:label id="lblRecordCount" Runat="server"></asp:label>
             </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" 
                    CellPadding="1" AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
					<HeaderStyle CssClass="HEADER"/>
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="18px" CssClass="CenteredTD" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CommandName="Select"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField Visible="False">
						    <ItemTemplate>
								<asp:Label id="lblCertID" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("Cert_Id"))%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField DataField="HOME_PHONE" SortExpression="HOME_PHONE" ReadOnly="true" HeaderText="HOME_PHONE" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="WORK_PHONE" SortExpression="WORK_PHONE" ReadOnly="true" HeaderText="WORK_PHONE" HeaderStyle-HorizontalAlign="Center" />
						<asp:BoundField DataField="Status_Code" ReadOnly="true" HeaderText=""/>
						<asp:BoundField DataField="CUSTOMER_NAME" SortExpression="CUSTOMER_NAME" ReadOnly="true" HeaderText="Customer_Name" HeaderStyle-HorizontalAlign="Center"/>
						<asp:BoundField DataField="ADDRESS1" SortExpression="ADDRESS1" ReadOnly="true" HeaderText="Address" HeaderStyle-HorizontalAlign="Center"/>
						<asp:BoundField DataField="postal_code" SortExpression="postal_code" ReadOnly="true" HeaderText="Zip" HeaderStyle-HorizontalAlign="Center"/>
						<asp:BoundField DataField="CERT_NUMBER" SortExpression="CERT_NUMBER" ReadOnly="true" HeaderText="Certificate" HeaderStyle-HorizontalAlign="Center"/>
						<asp:BoundField DataField="DEALER" SortExpression="DEALER" ReadOnly="true" HeaderText="Dealer" HeaderStyle-HorizontalAlign="Center"/>
						<asp:BoundField DataField="Product_Code" SortExpression="Product_Code" ReadOnly="true" HeaderText="Product_code" HeaderStyle-HorizontalAlign="Center"/>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                </asp:GridView>
            </td>
        </tr>
        <asp:Literal runat="server" ID="spanFiller"></asp:Literal>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <script language="javascript" type="text/javascript">
        var h = parent.document.getElementById("Navigation_Content").clientHeight; //find the height of the iFrame client area
        if (document.getElementById('moTableOuter')){
            document.getElementById('moTableOuter').height = h - 60;
        }
        
        if (document.getElementById('tblMain')){
            document.getElementById('tblMain').height = h - 65;
        }                    
    </script>
</asp:Content>
