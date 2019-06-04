<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master" CodeBehind="AcctAmtSrcMappingDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AcctAmtSrcMappingDetailForm" %>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table width="100%" class="formGrid" border="0" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td align="right" nowrap="noWrap" style="width:5%; padding-left:10px;">
                            <asp:Label runat="server" ID="lblAmtSrc" Text="FIELD_TYPE" />:
                        </td>
                        <td align="left" nowrap="noWrap" style="width:30%">
                            <asp:TextBox runat="server" ID="txtAmtSrc" SkinID="LargeTextBox" style="width:300px;" enabled="false" />
                        </td>
                        <td align="right" nowrap="noWrap" style="width:5%">
                            <asp:Label runat="server" ID="Label2" Text="RECONCILE_WITH_INVOICE_AMT" />:
                        </td>
                        <td align="left" nowrap="noWrap" style="width:60%">
                            <asp:DropDownList ID="ddlReconWithInvoice" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:300px;">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap" style="padding-left:10px;">
                            <asp:Label runat="server" ID="lblEntityByRegion" Text="ENTITY_BY_REGION" />:
                        </td>
                        <td align="left" nowrap="noWrap">
                            <asp:DropDownList ID="ddlEntityByRegion" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;">
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="Label1" Text="ENTITY_BY_REGION_COVERAGE_TYPE" />:
                        </td>
                        <td align="left" nowrap="noWrap">
                            <asp:DropDownList ID="ddlEntityByRegionCovType" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:300px;">
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>
                        <td align="right" nowrap="noWrap" style="padding-left:10px;">
                            <asp:Label runat="server" ID="lblUserFormulaForClip" Text="USE_FORMULA_FOR_CLIP" />:
                        </td>
                        <td align="left" nowrap="noWrap">
                            <asp:DropDownList ID="ddlUseFormulaForClip" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>                   
                </tbody>
            </table>
        </div>
        <asp:GridView ID="gridMapping" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <SelectedRowStyle Wrap="False"></SelectedRowStyle>
            <EditRowStyle Wrap="False"></EditRowStyle>
            <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle Wrap="False"></HeaderStyle>
            <Columns>
                <asp:TemplateField Visible="True" HeaderText="PRODUCT" ItemStyle-width="20%">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblProduct" Text='<%# Databinder.Eval(Container.DataItem, "ProdDesc")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID = "ddlProduct" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button runat="server" ID="AddNew_WRITE" Text="New" SkinID="AlternateLeftButton" CommandName="AddRecord"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COVERAGE_TYPE" ItemStyle-width="10%">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblLossType" Text='<%# Databinder.Eval(Container.DataItem, "LossTypeDesc")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID = "ddlLossType" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COUNT_FIELD" ItemStyle-width="10%">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblCountField" Text='<%# Databinder.Eval(Container.DataItem, "CountFieldDesc")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID = "ddlCountField" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="FORMULAR">
                    <ItemTemplate>
                        <asp:Label ID="lblGenericMapping" Text='<%# Container.DataItem("Formular") %>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="1%"></ItemStyle>
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                            CommandName="EditRecord" CommandArgument='<%# Databinder.Eval(Container.DataItem, "DetailID") %>'></asp:ImageButton>
                    </ItemTemplate> 
                    <EditItemTemplate>
                        <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                            Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>                  
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="1%"></ItemStyle>
                    <ItemTemplate>
                        <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                            runat="server" CommandName="DeleteRecord" CommandArgument='<%# Databinder.Eval(Container.DataItem, "DetailID") %>'>
                        </asp:ImageButton>
                    </ItemTemplate> 
                    <EditItemTemplate>
                        <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                            Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                    </EditItemTemplate>                   
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
        <br />
        <asp:GridView ID="gridEditFormular" runat="server" Width="100%" Visible="false" AllowPaging="false" AllowSorting="false" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle Wrap="False" HorizontalAlign="Left"></HeaderStyle>
            <Columns>
                <asp:TemplateField Visible="True" HeaderText="OPERATOR" ItemStyle-width="20%">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblDetailID" Text='<%# Databinder.Eval(Container.DataItem, "id") %>' runat="server" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlOperation" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="+" Value="+"></asp:ListItem>
                            <asp:ListItem Text="-" Value="-"></asp:ListItem>
                        </asp:DropDownList>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="RATE_BUCKET" ItemStyle-width="20%">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlBucket" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:300px;">                            
                        </asp:DropDownList>   
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"/>
        <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"/>        
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
</asp:Content>
