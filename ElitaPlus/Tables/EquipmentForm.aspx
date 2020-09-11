 <%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EquipmentForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.EquipmentForm"  Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="url" TagName="UserControlSearchAvailable" Src="../Interfaces/SearchAvailableSelected.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAttrtibutes" Src="~/Common/UserControlAttrtibutes.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">       
    <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (9/20/2004)  ******************** -->
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
     <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/Tabs.js"></script>
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server"> 
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%" Width="100%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 95%" cellspacing="0" cellpadding="6" rules="cols" width="95%" align="center" bgcolor="#fef9ea" border="0">                      
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="EditPanel" runat="server" Width="100%">
                                    <table id="Table4" style="width: 100%" cellspacing="1" cellpadding="0" width="710" border="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PanelMasterFields_Write" runat="server" Width="100%">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td nowrap="" align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="moManufacturerLabel" runat="server" Font-Bold="false">Manufacturer</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left">
                                                                <asp:DropDownList ID="moManufacturerDrop" TabIndex="1" runat="server" Width="210px" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td nowrap="" align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="moIsMasterModelLabel" runat="server" Font-Bold="false">IS_MASTER</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left">
                                                                <asp:DropDownList ID="moIsMasterModelDrop" TabIndex="2" runat="server" Width="60px" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="moModelLabel" runat="server" Font-Bold="false">MODEL</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:TextBox ID="moModelText" TabIndex="3" Width="210px" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="moDescriptionLabel" runat="server" Font-Bold="False">DESCRIPTION</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:TextBox ID="moDescriptionText" TabIndex="4" Width="210px" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="moMasterEquipmentlLabel" runat="server" Font-Bold="false">MASTER_MODEL</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="moMasterEquipmentDrop" TabIndex="5" runat="server" Width="210px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="moManufacturerWarrentyLabel" runat="server" Font-Bold="false">MFG_WARRANTY</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:TextBox ID="moManufacturerWarrentyText" TabIndex="6" Width="210px" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap="" align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="moEquipmentClassLabel" runat="server" Font-Bold="false">EQUIPMENT_CLASS</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left">
                                                                <asp:DropDownList ID="moEquipmentClassDrop" TabIndex="7" runat="server" Width="210px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="moEquipmentTypeLabel" runat="server" Font-Bold="false">EQUIPMENT_TYPE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="moEquipmentTypeDrop" TabIndex="8" runat="server" Width="210px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap="" align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="moRepairableLabel" runat="server" Font-Bold="false">REPAIRABLE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left">
                                                                <asp:DropDownList ID="moRepairableDrop" TabIndex="9" runat="server" Width="60px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="lblColor" runat="server" Font-Bold="false">COLOR</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="moColor" TabIndex="10" runat="server" Width="60px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="lblMemory" runat="server" Font-Bold="false">MEMORY</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="moMemory" TabIndex="11" runat="server" Width="60px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="lblCarrier" runat="server" Font-Bold="False">CARRIER</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="moCarrier" TabIndex="12" runat="server" Width="60px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>


                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="center">
                                                <hr>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="left" colspan="4">
                                                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
                                                 <asp:HiddenField ID="hdnDisabledTabs" runat="server" Value="2"></asp:HiddenField>
                                                    <div id="tabs" class="style-tabs-old" style="border:none;">
                                                       <%-- <div id="tabs" class="style-tabs-old style-tabs-oldBG" style="border:none;background:#fef9ea">--%>
                                                        <ul>
                                                            <li style="background:#d5d6e4"><a href="#tabNotes" rel="noopener noreferrer"><asp:Label ID="Label1" runat="server" CssClass="tabHeaderTextOld">Notes</asp:Label></a></li>                                                                
                                                            <li style="background:#d5d6e4"><a href="#tabAttributes" rel="noopener noreferrer"><asp:Label ID="Label17" runat="server" CssClass="tabHeaderTextOld">Attributes</asp:Label></a></li>                                                                
                                                            <li style="background:#d5d6e4"><a href="#tabImages" rel="noopener noreferrer"><asp:Label ID="Label18" runat="server" CssClass="tabHeaderTextOld">Images</asp:Label></a></li>                                                                
                                                            <li style="background:#d5d6e4"><a href="#tabRelatedEquipment" rel="noopener noreferrer"><asp:Label ID="Label19" runat="server" CssClass="tabHeaderTextOld">RELATED_EQUIPMENT</asp:Label></a></li>                                                                
                                                        </ul>
                                                    <div id="tabNotes" style="background:#d5d6e4;border:1px solid; border-color:black;">
                                                            <!-- Tab begin -->
                                                            <div id="commentScroller" style="overflow: auto; width: 99.53%; height: 200px" align="center">
                                                                <table id="tblCommentDetail" style="width: 100%; height: 100%" cellspacing="2" cellpadding="2" rules="cols" border="0">
                                                                    <tr>
                                                                        <td valign="top" align="left">
                                                                            <asp:DataGrid ID="DataGridCommentDetail" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True" AllowSorting="True">
                                                                                <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                                                <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                                <HeaderStyle></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="EditButton" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif" CommandName="ViewRecord"></asp:ImageButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn HeaderText="Notes">
                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                                                                </Columns>
                                                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                    <tr valign="bottom">
                                                                        <td align="left">
                                                                            <asp:Button ID="btnAddNewCommentFromGrid_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="PanelCommentEditDetail" runat="server" Width="100%" Height="100%">
                                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" height="100%">
                                                                                    <tr valign="top">
                                                                                        <td>
                                                                                            <asp:Panel ID="PanelEditDetail_Write" runat="server" Width="100%" Height="100%">
                                                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td width="30%" nowrap="" style="text-align: right; vertical-align: top">
                                                                                                            <asp:Label ID="moCommentLabel" runat="server" Font-Bold="false">Notes</asp:Label>:
                                                                                                        </td>
                                                                                                        <td width="70%">
                                                                                                            <asp:TextBox ID="moCommentTextBox" runat="server" TextMode="MultiLine" Height="60" Width="50%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr valign="bottom">
                                                                                        <td>
                                                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Button ID="btnCommentBackChild" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" TabIndex="140" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                                                                                        <asp:Button ID="btnCommentOkChild_Write" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" TabIndex="145" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;&nbsp;
                                                                                                        <asp:Button ID="btnCommentCancelChild" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" TabIndex="150" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>
                                                                                                        <asp:Button ID="btnCommentDeleteChild_Write" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" TabIndex="165" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <!-- Tab end -->
                                                    </div>
                                                    <div id="tabAttributes" style="background:#d5d6e4;border:1px solid; border-color:black;">
                                                        <!-- Tab begin -->
                                                        <Elita:UserControlAttrtibutes runat="server" ID="AttributeValues"></Elita:UserControlAttrtibutes>
                                                        <!-- Tab end -->
                                                    </div>
                                                    <div id="tabImages" style="background:#d5d6e4;border:1px solid; border-color:black;">
                                                        <!-- Tab begin -->
                                                        <div id="imageScroller" style="overflow: auto; width: 99.53%; height: 200px" align="center">
                                                            <table id="tblImageDetail" style="width: 100%; height: 100%" cellspacing="2" cellpadding="2" rules="cols" border="0">
                                                                <tr>
                                                                    <td valign="top" align="left">
                                                                        <asp:DataGrid ID="DataGridImageDetail" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True" AllowSorting="True">
                                                                            <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                                            <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                            <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                            <HeaderStyle></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="EditButton" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif" CommandName="ViewRecord"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:BoundColumn HeaderText="Code">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="47%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn HeaderText="Description">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="47%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn HeaderText="Image Type">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="47%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                                                            </Columns>
                                                                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                                        </asp:DataGrid>
                                                                    </td>
                                                                </tr>
                                                                <tr valign="bottom">
                                                                    <td align="left">
                                                                        <asp:Button ID="btnAddNewImageFromGrid_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="PanelImageEditDetail" runat="server" Width="100%" Height="100%">
                                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%" height="100%">
                                                                                <tr valign="top">
                                                                                    <td>
                                                                                        <asp:Panel ID="Panel2" runat="server" Width="100%" Height="100%">
                                                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                                <tr>
                                                                                                    <td width="1%" nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label2" runat="server" Font-Bold="false">Risk_Type</asp:Label>:
                                                                                                    </td>
                                                                                                    <td width="50%">
                                                                                                        <asp:DropDownList ID="Dropdownlist1" runat="server" Width="70%">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td colspan="2">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label3" runat="server" Font-Bold="false">Effective_Date</asp:Label>:
                                                                                                    </td>
                                                                                                    <td nowrap="" width="50%">
                                                                                                        <asp:TextBox ID="Textbox1" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                                                        </asp:ImageButton>
                                                                                                    </td>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label4" runat="server" Font-Bold="false">SELECT_TAX_TYPE</asp:Label>:
                                                                                                    </td>
                                                                                                    <td width="49%">
                                                                                                        <asp:DropDownList ID="DropDownList2" runat="server" Width="80%">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label5" runat="server" Font-Bold="false">Low_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td nowrap="">
                                                                                                        <asp:TextBox ID="Textbox2" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label6" runat="server" Font-Bold="false">High_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Textbox3" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <hr style="width: 100%; height: 1px" size="1">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label7" runat="server" Font-Bold="false">Home_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td nowrap="">
                                                                                                        <asp:TextBox ID="Textbox4" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label8" runat="server" Font-Bold="false">Carry_In_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Textbox5" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label10" runat="server" Font-Bold="false">Send_In_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td nowrap="">
                                                                                                        <asp:TextBox ID="Textbox6" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label11" runat="server" Font-Bold="false">Pick_Up_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Textbox7" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label12" runat="server" Font-Bold="false">Cleaning_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td nowrap="">
                                                                                                        <asp:TextBox ID="Textbox8" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label13" runat="server" Font-Bold="false">Estimate_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Textbox9" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label14" runat="server" Font-Bold="false">Replacement_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td nowrap="">
                                                                                                        <asp:TextBox ID="TextBox10" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label15" runat="server" Font-Bold="false">Hourly_Rate</asp:Label>:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Textbox11" TabIndex="10" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                        <asp:Label ID="Label16" runat="server" Font-Bold="false">Discounted_Price</asp:Label>:
                                                                                                    </td>
                                                                                                    <td nowrap="">
                                                                                                        <asp:TextBox ID="Textbox12" TabIndex="11" runat="server" CssClass="FLATTEXTBOX" Width="80%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td nowrap="" style="text-align: right">
                                                                                                    </td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr valign="bottom">
                                                                                    <td>
                                                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Button ID="Button4" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="140" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                                                                                    <asp:Button ID="Button5" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="145" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;&nbsp;
                                                                                                    <asp:Button ID="Button6" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="150" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>
                                                                                                    <asp:Button ID="Button9" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="165" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <!-- Tab end -->
                                                    </div>
                                                    <div id="tabRelatedEquipment" style="background:#d5d6e4;border:1px solid; border-color:black;">
                                                        <!-- Tab begin -->
                                                        <asp:Panel id="RelatedEquipmentScroller" style="overflow: auto; width: 99.53%; height: 255px" align="center" runat="server">
                                                            <table id="tblRelatedEquipmentDetail" style="width: 100%; height: 100%" cellspacing="2" cellpadding="2" rules="cols" border="0">
                                                                <tr>
                                                                    <td valign="top" align="left">
                                                                        <asp:GridView id="GVRelatedEquipmentDetail" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" PageSize="10" CellPadding="1" AllowPaging="True" AllowSorting="True" OnRowCommand="GVRelatedEquipmentDetail_RowCommand" OnRowCreated="GVRelatedEquipmentDetail_ItemCreated" CssClass="DATAGRID">
                                                                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                                            <RowStyle CssClass="ROW"></RowStyle>
                                                                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText=" ">
                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif" CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText=" ">
                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="DeleteButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/trash.gif" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRelatedEquipmentId" runat="server" Text='<%#GetGuidStringFromByteArray(Container.DataItem("related_equipment_id")) %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="EQUIPMENT_TYPE">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="17%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblequipmentType" runat="server" Text='<%#Container.DataItem("equipment_type") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DESCRIPTION">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDescription" runat="server" Text='<%#Container.DataItem("equipment_description") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MANUFACTURER">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblManufacturer" runat="server" Text='<%#Container.DataItem("equipment_mfg") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MODEL">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="17%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Container.DataItem("equipment_model") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="IN_OEM_BOX">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblisInOemBox" runat="server" Text='<%#Container.DataItem("in_oem_box") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="moInOemBoxDrop" runat="server" Visible="true"> 
                                                                                        </asp:DropDownList>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="IS_COVERED">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:label ID="lblIsCovered" runat="server" Text='<%#Container.DataItem("is_covered") %>'></asp:label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="moIsCoveredDrop" runat="server" Visible="true">
                                                                                        </asp:DropDownList>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerSettings PageButtonCount="15" Mode="Numeric"></PagerSettings>
                                                                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr valign="bottom">
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Button ID="btnRelatedEquipmentSelectChild_Write" Style="background-image: url(../Navigation/images/icons/edit2.gif);
                                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="145" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="EDIT"></asp:Button>&nbsp;&nbsp;
                                                                                    <asp:Button ID="btnRelatedEquipmentOkChild_Write" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="145" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="SAVE"></asp:Button>&nbsp;&nbsp;
                                                                                    <asp:Button ID="btnRelatedEquipmentCancelChild_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="150" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="UNDO"></asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                        </table>
                                                        </asp:Panel>
                                                        <asp:Panel id="divSearchAvailableSelected" style="overflow: auto; width: 99.53%; height: 255px" align="center" runat="server">
                                                              <table id="tblUserControl" style="width: 100%; height: 100%" cellspacing="2" cellpadding="2" rules="cols" border="0">
                                                                <tr>
                                                                    <td>
                                                                    <url:UserControlSearchAvailable id="UserControlSearchAvailableEquipment" runat="server">
                                                                    </url:UserControlSearchAvailable>
                                                                    </td>
                                                                </tr>
                                                              </table>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
                                <hr style="width: 100%; height: 1px" size="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" nowrap="" align="left" height="20">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false" Width="81px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="136px" Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                </asp:Button>
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>/td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
                                <hr style="width: 100%; height: 1px" size="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" nowrap align="left" height="20">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                    Width="81px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="136px"
                                    Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                </asp:Button>
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                        runat="server" designtimedragdrop="261">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>