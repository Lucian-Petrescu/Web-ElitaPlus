<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountingEventDetailForm.aspx.vb" Theme="Default"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingEventDetailForm" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
   <script language="JavaScript"  type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script> 
   <script language="javascript">
       function ChangeBU(v) {

           PageMethods.IncludeVendors(v.value, OnComplete);

       }

       function OnComplete(result) {

           var resultSplit;
           var SelectObject = document.getElementById('<%=moUsePayeeSettingsDROPDOWN.ClientID() %>');

           resultSplit = result.split("|");

           if (resultSplit[0] == 'Y') {

               for (index = 0; index < SelectObject.length; index++) {
                   if (SelectObject[index].value == resultSplit[1]) {
                       SelectObject.selectedIndex = index;
                   }
                   SelectObject.disabled = true;
               }
           } else {
               SelectObject.disabled = false;
           }

       }
    </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="moTableOuter" border="0" align="left">
        <tr>
            <td>
                <asp:Panel ID="EditPanel" runat="server">
                    <table class="searchGrid" border="0">
                        <asp:Panel ID="EditPanel_WRITE" runat="server" Height="98%" Width="100%">
                            <tbody>
                                <tr>
                                    <td nowrap align="right">
                                        <asp:Label ID="moAccountingCompanyLABEL" Text="ACCOUNTING_COMPANY:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="moAccountingCompanyDROPDOWN" runat="server" Enabled="False" Width="207px" SkinID="SmallDropDown"></asp:DropDownList>
                                    </td>
                                    <td nowrap align="right">
                                        <asp:Label ID="moAccountingEventLABEL" Text="ACCOUNTING_EVENT:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="moAccountingEventDROPDOWN" runat="server" Enabled="False" Width="207px" SkinID="SmallDropDown"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <asp:Label ID="moDebitCreditLABEL" runat="server" Text="DEBIT_CREDIT:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="moDebitCreditDROPDOWN" Width="207px" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap align="right">
                                        <asp:Label ID="moBusinessUnitLABEL" runat="server">ACCOUNTING_BUSINESS_UNIT:</asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="moBusinessUnitDDL" Width="207px" AutoPostBack="false" onChange="ChangeBU(this);" SkinID="SmallDropDown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <asp:Label ID="moFieldTypeIdLABEL" runat="server" Text="FIELD_TYPE"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="moFieldTypeDROPDOWN" Width="207px" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap align="right">
                                        <asp:Label ID="moCalculationLABEL" runat="server" Text="CALCULATION:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:TextBox ID="moCalculationTEXT" Width="200px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <asp:Label ID="moJournalTypeLABEL" runat="server" Text="JOURNAL_TYPE:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:TextBox ID="moJournalTypeTEXT" Width="200px" runat="server"></asp:TextBox>
                                    </td>
                                    <td nowrap align="right">
                                        <asp:Label ID="moAccountTypeLABEL" runat="server">Account_Type</asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="cboAccountType" runat="server" Width="207px" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <asp:Label ID="moUsePayeeSettingsLABEL" runat="server" Text="USE_PAYEE_SETTINGS:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="moUsePayeeSettingsDROPDOWN" AutoPostBack="false" Width="207px" SkinID="SmallDropDown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap align="right" id="hidForVendor2" runat="server">
                                        <asp:Label ID="moAccountCodeLABEL" runat="server" Text="ACCOUNT_CODE:"></asp:Label>
                                    </td>
                                    <td align="left" id="hidForVendor3" runat="server">
                                        &nbsp;
                                        <asp:TextBox ID="moAccountCodeTEXT" Width="200px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <asp:Label ID="moBusinessEntityLABEL" runat="server">BUSINESS_ENTITY</asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:DropDownList ID="moBusinessEntityDROPDOWN" Width="207px" AutoPostBack="false" SkinID="SmallDropDown" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap align="right">
                                        <asp:Label ID="moJournalIdLABEL" runat="server">JOURNAL_ID</asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:TextBox ID="moJournalIdTEXT" runat="server" Width="200px"></asp:TextBox>
                                    </td>                           
                                </tr>
                            </tbody>
                        </asp:Panel>
                    </table>
                </asp:Panel>
            </td>
        </tr>
     </table>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">    
    <div class="dataContainer">   
         <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
         <asp:HiddenField ID="hdnDisabledTab" runat="server" />     
        <div id="tabs" class="style-tabs" style="border:none;">
          <ul>
            <li><a href="#tabsCodeConfiguration"><asp:Label ID="Label5" runat="server" CssClass="tabHeaderText">CODE_CONFIGURATION</asp:Label></a></li>
            <li><a href="#tabsInclusionExclisionConfig"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">INCLUSION_EXCLUSION_CONFIG</asp:Label></a></li>            
          </ul>
          
          <div id="tabsCodeConfiguration" style="background:#d5d6e4;border-left: 1px solid black;border-right: 1px solid black;">
             <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchGrid">
                    <tr>
                        <td colspan="7">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align:left">
                            *&nbsp;<asp:Label ID="Label1" runat="server">SOURCE</asp:Label>
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label2" runat="server">VALUE</asp:Label>
                        </td>
                        <td style="width:50px;">&nbsp;</td>
                        <td></td>
                        <td style="text-align:left">
                            *&nbsp;<asp:Label ID="Label3" runat="server">SOURCE</asp:Label>
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="Label4" runat="server">VALUE</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moDescriptionLABEL" runat="server">DESCRIPTION</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moDescriptionDDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="moDescriptionTEXT" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td></td>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal6LABEL" runat="server">Analysis_Code_6</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal6DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="moAcctAnal6TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal1LABEL" runat="server">Analysis_Code_1</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal1DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="moAcctAnal1TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td><td>
                        </td>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal7LABEL" runat="server">Analysis_Code_7</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal7DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="moAcctAnal7TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal2LABEL" runat="server">Analysis_Code_2</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal2DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="moAcctAnal2TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal8LABEL" runat="server">Analysis_Code_8</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal8DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="moAcctAnal8TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal3LABEL" runat="server">Analysis_Code_3</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal3DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="moAcctAnal3TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal9LABEL" runat="server">Analysis_Code_9</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal9DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="moAcctAnal9TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal4LABEL" runat="server">Analysis_Code_4</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal4DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="moAcctAnal4TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal10LABEL" runat="server">Analysis_Code_10</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal10DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td >
                            <asp:TextBox ID="moAcctAnal10TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; white-space:nowrap; vertical-align:bottom;">
                            <asp:Label ID="moAcctAnal5LABEL" runat="server">Analysis_Code_5</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="moAcctAnal5DDL" Width="207px" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td >
                            <asp:TextBox ID="moAcctAnal5TEXT" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td colspan="4"></td>
                    </tr>
                </table>
          </div>
          
          <div id="tabsInclusionExclisionConfig" style="background:#d5d6e4;">
             <table id="tblIncExc" class="searchGrid" border="0" rules="cols" width="98%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblIncludeExcludeType" runat="server">CONFIG_TYPE:</asp:Label>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlIncludeExcludeType" Width="207px" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <div id="scroller" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="moGridView" runat="server" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                    AllowPaging="False" AllowSorting="false" CellPadding="1" AutoGenerateColumns="False"
                                    SkinID="DetailPageGridView" Width="100%">
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField Visible="True" HeaderText="DEALER">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDealer"  Text='<%# DataBinder.Eval(Container.DataItem, "DealerDescription")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID = "ddlDealer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="moDealerDropGrid_SelectedIndexChanged" Visible="true" Width="350px" SkinID="SmallDropDown"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="COVERAGE_TYPE">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCoverageType" Text='<%# DataBinder.Eval(Container.DataItem, "CoverageTypeDescription")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID = "ddlCoverageTYPE" runat="server" Visible="true" Width="220px" SkinID="SmallDropDown"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                                            </HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                                    CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                    Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                                            </HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                                    runat="server" CommandName="DeleteRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'>
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                    Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="96%">
                            <asp:Button ID="BtnNewIncExc_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;                            
                        </td>
                    </tr>
                </table>
          </div>
        </div>             
    </div>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" size="14" name="HiddenSaveChangesPromptResponse"
            runat="server" />
    <div class="btnZone" width="70%">        
        <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back"></asp:Button>
        <asp:Button ID="btnSave_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="Save"></asp:Button>
        <asp:Button ID="btnUndo_Write" runat="server" SkinID="AlternateLeftButton" Text="Undo"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Delete"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" SkinID="AlternateLeftButton" Text="NEW_WITH_COPY"></asp:Button>
    </div>
</asp:Content>
