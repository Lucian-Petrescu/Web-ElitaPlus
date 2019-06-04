
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminMaintainDropdownByEntityForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AdminMaintainDropdownByEntityForm"  MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default"  %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server" >
       <uc1:ErrorController id="ErrorControl" runat="server" Visible="False"></uc1:ErrorController>
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
      <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
          
                         <tr>
                                        <td align="right" width="20%" style="white-space:nowrap;">
                                            <asp:Label ID="lblDropdownList" runat="server">DROPDOWN_LIST</asp:Label>
                                        </td>
                                        <td width="25%" style="white-space:nowrap;">
                                            <asp:DropDownList ID="moDropdownList" SkinID="MediumDropDown" AutoPostBack="true" runat="server" Width="205px" >
                                            </asp:DropDownList>
                                        </td>
                                       
                                    </tr>
                                     <tr>
                                        <td align="right" width="20%" class="borderLeft" nowrap="nowrap">
                                            <asp:Label ID="LabelEntityType" runat="server">ENTITY_TYPE</asp:Label>
                                        </td>
                                        <td align="left" width="25%" nowrap="nowrap">
                                            <asp:DropDownList ID="moDropdownEntityType" SkinID="MediumDropDown" runat="server" Width="205px" AutoPostBack="True">                                           
                                            </asp:DropDownList>                                       

                                        </td>
                                          <td align="right" class="auto-style1" nowrap="nowrap">
                                            <asp:Label ID="LabelEntity" runat="server">ENTITY</asp:Label>
                                        </td>
                                        <td align="left" width="25%" nowrap="nowrap">
                                            <asp:DropDownList ID="moDropdownEntity" SkinID="MediumDropDown" runat="server" Width="205px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr><td colspan="4">&nbsp;</td></tr>
                                   <tr>
                                        <td style="text-align:right;vertical-align:text-bottom; padding-right:4px;padding-bottom:5px" colspan="4">
                                             <asp:Button ID="btnSearch" SkinID="SearchButton"  runat="server" Text="Search" TabIndex="4" /> &nbsp;&nbsp;
                                            <asp:Button ID="btnClearSearch" SkinID="AlternateLeftButton" runat="server" Text="Clear" TabIndex="5" />&nbsp;&nbsp;                                          
                                          
                                        </td>
                                   </tr>

         </table>

   
 </asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_ENTITY_LIST" Visible="true" ></asp:Label>
        </h2>
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
       <div style="width: 100%">

               <asp:GridView ID="GridDropdownsByEntity" runat="server" Width="100%" 
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="30">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                  <Columns>
                                                       <asp:TemplateField HeaderImageUrl="../Navigation/images/icons/check.gif">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBoxItemSel" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="List_Code" SortExpression="ListCode">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox Width="100%" ID="TextBoxListCode"  ReadOnly="true" runat="server" Text='<%# Container.DataItem("ListCode") %>'
                                                                CssClass="FLATTEXTBOX">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="List_Description"  SortExpression="Description">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxListDescription" ReadOnly="true"  runat="server" Text='<%# Container.DataItem("Description") %>'
                                                                CssClass="FLATTEXTBOX" Width="100%">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Entity_Type"  SortExpression="entity_reference">

                                                       <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelEntityTypeCode" runat="server" visible="false" Text='<%# Container.DataItem("entity_reference") %>'></asp:Label>
                                                             <asp:Label ID="LabelEntityRefId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("entity_reference_id")) %>'>
                                                            </asp:Label>
                                                            <asp:TextBox ID="TextBoxEntityType" ReadOnly="true" runat="server"  Text='<%# Container.DataItem("EntityType") %>'
                                                                CssClass="FLATTEXTBOX" Width="100%">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Entity_Code" SortExpression="EntityCode">
                                                       <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxEntityCode" runat="server" ReadOnly="true"  Text='<%# Container.DataItem("EntityCode") %>'
                                                                CssClass="FLATTEXTBOX" Width="100%">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                       <asp:TemplateField HeaderText="Entity_Description" SortExpression="EntityDecsription">

                                                       <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxEntityDescription" runat="server" ReadOnly="true"  Text='<%# Container.DataItem("EntityDecsription") %>'
                                                                CssClass="FLATTEXTBOX" Width="100%">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="LIST_ITEM_COUNT">

                                                       <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxListItemCount" runat="server" ReadOnly="true"  Text='<%# Container.DataItem("ListitemCount") %>'
                                                                CssClass="FLATTEXTBOX" Width="100%">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                 
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Items">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnView" runat="server" CommandArgument="<%#Container.DisplayIndex %>" Text="View" CommandName="ViewEdit"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                   
                                                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle  />
            </asp:GridView>



            
       </div>
     </div>
      <div class="btnZone">
            <asp:Button ID="bntAdd" runat="server" SkinID="PrimaryRightButton" CausesValidation="false" TabIndex="6" Text="Add_NEW" />&nbsp;&nbsp;
        <asp:Button ID="btnDelete" runat="server" SkinID="AlternateLeftButton"   CausesValidation="false"  Width="90px" Text="Delete" Height="20px" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat"
           ></asp:Button>
    </div>
  
    </asp:Content>

