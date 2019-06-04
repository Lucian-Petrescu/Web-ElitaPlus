<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BonusStructureForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.BonusStructureForm" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">
      function TABLE1_onclick() { }

    </script>
   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MessagePlaceHolder" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" id="searchTable"  runat="server" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
 
            <tr>
                <td id="Td1" colspan="4">
                    <table>
                        <tbody>
                            <tr>
                               <td colspan="4">
                                     <uc1:MultipleColumnDDLabelControl ID="moServiceCenterMultipleDropControl" runat="server" />  
                                    </td> 
                             
                                </tr>
                            <tr>
                                
                                 <td colspan="4">
                                     &nbsp;  <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDropControl" runat="server" />                                   
                                </td>
                                  </tr>

                            <tr> <td colspan="4"> &nbsp; </td></tr>

                                <tr>
                             
                                     <td colspan="2" nowrap="nowrap">
                                         &nbsp;
                                    <asp:Label ID="moProductCodelabel" runat="server">Product_code</asp:Label>:
                                 &nbsp;
                                   <asp:DropDownList ID="moProductCode" runat="server" SkinID="MediumDropDown" AutoPostBack="true"></asp:DropDownList>
                                   
                                </td>
                          
                              
                                <td  colspan="2" align="right">
                                    <span style="padding-left: 10px; white-space: nowrap;">
                                        <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                        </asp:Button>
                                        <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                    </span>

                                </td>

                              </tr>
                           
                        </tbody>
                    </table>
                </td>
            </tr>
  
    </table>

</asp:Content>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
           <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_BONUS_STRUCTURE</asp:Label>
        </h2>

                <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
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
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
         <div style="width: 100%">
              <asp:GridView ID="Grid" runat="server" Width="100%"  OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                                     AutoGenerateColumns="False" AllowPaging="True"
                                     AllowSorting="true" SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />

                                    <Columns>
                                        
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" CausesValidation="False" runat="server" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                    Visible="true" CommandName="SelectAction" ImageAlign="AbsMiddle" CommandArgument="<%#Container.DisplayIndex %>">
                                                </asp:ImageButton></ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField SortExpression="service_center" HeaderText="SERVICE_CENTER" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>


                                         <asp:TemplateField SortExpression="DEALER_NAME"    HeaderText="DEALER_NAME">                                         
                                           <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                      </asp:TemplateField>

                                       <%--DataField="product"--%>
                                        <asp:TemplateField  SortExpression="product_code" HeaderText="PRODUCT_CODE">
                                            <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                      </asp:TemplateField>

                                        <%--DataField="compute_bonus_method"--%>

                                        <asp:TemplateField  SortExpression="compute_bonus_method_code"  HeaderText="COMPUTE_BONUS_METHOD">
                                          <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>


                                    <%-- DataField="sc_avg_tat"--%>

                                         <asp:TemplateField  SortExpression="sc_avg_tat"  HeaderText="AVG_TURNAROUND_TIME">
                                            <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>



                                        <%--DataField="pecoramount"--%>

                                        <asp:TemplateField  SortExpression="pecoramount"  HeaderText="PERCENTAGE_OR_AUTH_AMOUNT">
                                         <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                  


                                       <%-- DataField="priority"--%>

				                         <asp:TemplateField  SortExpression="priority"  HeaderText="Priority">
                                         <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>



                                       <%-- DataField="sc_replacement_pct"--%>

                                           <asp:TemplateField  SortExpression="sc_replacement_pct" HeaderText="MAXIMUM_REPLACEMENT_PERCENTAGE">
                                         <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>

                                            
                                        
                                        <%--DataField="bonus_amount_period_month"--%>
                                        
                                         <asp:TemplateField  SortExpression="bonus_amount_period_month"  HeaderText="BONUS_AMOUNT_PERIOD">
                                          <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                      
                                                                      
                                       <%-- DataField="effective"--%>
                                        <asp:TemplateField  SortExpression="effective"  HeaderText="EFFECTIVE_DATE">
                                             <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>


                                        <%--DataField="expiration"--%>
                                        <asp:TemplateField  SortExpression="expiration" HeaderText="EXPIRATION_DATE">
                                              <HeaderStyle HorizontalAlign="Center" />
                                               <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>


                                        <asp:TemplateField Visible="False" HeaderText="BONUS_STRUCTURE_ID"></asp:TemplateField>

                                    </Columns>
                                    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                    <PagerStyle />
                                </asp:GridView>                    
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnNew_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="NEW" />                   
            </div>
        </div>

   </asp:Content>
<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>--%>
