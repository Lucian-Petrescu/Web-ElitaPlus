<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlCallerInfo.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlCallerInfo" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<asp:GridView ID="GridViewCaller" runat="server" Width="100%" ShowHeaderWhenEmpty="true"
    AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" CssClass="dataGrid"
    PageSize="30">
    <SelectedRowStyle Wrap="True"></SelectedRowStyle>
    <EditRowStyle Wrap="True"></EditRowStyle>
    <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
    <RowStyle Wrap="True"></RowStyle>
    <Columns>
        <asp:TemplateField Visible="True" ShowHeader="false">
            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" Width="0%"></ItemStyle>
            <ItemTemplate>
                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/radioButtonOff.png"
                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField Visible="True" HeaderText="FIRST_NAME">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="lblFirstName" Text='<%#Eval("FIRST_NAME")%>' runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextboxFirstName" runat="server" CssClass="small" MaxLength="50" Width="90%"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField Visible="True" HeaderText="LAST_NAME">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="lblLastName" Text='<%#Eval("LAST_NAME")%>' runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextboxLastName" runat="server" CssClass="small" MaxLength="50" Width="90%"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField Visible="True" HeaderText="WORK_PHONE">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="lblWorkPhone" Text='<%#Eval("WORK_PHONE")%>' runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextboxWorkPhone" runat="server" CssClass="small" MaxLength="50" Width="90%"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField Visible="True" HeaderText="EMAIL">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="lblEmail" Text='<%#Eval("EMAIL")%>' runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextboxEmail" runat="server" CssClass="small" MaxLength="50" Width="90%"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField Visible="True" HeaderText="RELATIONSHIP_TYPE">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="lblRelationship" Text='<%#Eval("RELATIONSHIP")%>' runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlRelationship" runat="server" CssClass="small" Width="90%"></asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
</asp:GridView>
