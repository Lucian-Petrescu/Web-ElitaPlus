<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ContractListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ContractListForm" 
 MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
   <script language="JavaScript"  type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script> 
   <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

 <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
      <tr>
            <td align="left"  width="60%">
                <table width="100%">
                <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                </table>
            </td>
            <td valign="middle" width="40%"> 
            <table>
                <tr>
                <td style="height: 40px" valign="middle" align="right" nowrap="nowrap" width="40%" >
                <br/>
                <asp:Button ID="btnClearSearch" SkinID="AlternateLeftButton" runat="server" Text="Clear"></asp:Button>&nbsp;
                <asp:Button ID="btnSearch" SkinID="SearchLeftButton" runat="server" Text="Search"></asp:Button>&nbsp;
                </td>
                </tr>
             </table>
            </td>
       </tr>
   </table>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
      <div class="dataContainer">
        <h2 class="dataGridHeader">
            Search results for Contracts</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td valign="top" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px"  SkinID="SmallDropDown" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
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
                    <td align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
               </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" SkinID="DetailPageGridView" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                AllowSorting="True" OnRowCreated="RowCreated" OnRowCommand="RowCommand">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Dealer Code">
                        <ItemStyle HorizontalAlign="Left" ></ItemStyle>
                        <ItemTemplate>
                           <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                             OnClientClick ="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"  CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:LinkButton>                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company_Code">
                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dealer_Name">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="35%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start_Date">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End_Date">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="Contract_Id"></asp:TemplateField>
                </Columns>
                      <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle/>
            </asp:GridView>
     </div>
       
    </div>                     
       <div class="btnZone">
            <asp:Button ID="btnAdd_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton"></asp:Button>&nbsp;
    </div>

</asp:Content>
                   




