<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountingReversalForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingReversalForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="4">
                <table cellpadding="2" cellspacing="0" border="0" style="border: #999999 1px solid;
                    background-color: #f1f1f1; height: 98%; width: 100%">
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label1" runat="server" Text="BATCH_NUMBER:"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtBatchNumber" runat="server" ReadOnly="true" Width="150px"></asp:TextBox>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label2" runat="server" Text="TRANSMISSION_DATE:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTransDate" runat="server" ReadOnly="true" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label3" runat="server" Text="DEBIT_AMOUNT:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDebitAmount" runat="server" ReadOnly="true" Width="150px"></asp:TextBox>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label4" runat="server" Text="CREDIT_AMOUNT:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreditAmount" runat="server" ReadOnly="true" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr style="display:none">
            <td class="BLANKROW" colspan="4">
                <hr />
            </td>
        </tr>
        <tr style="display:none">
            <td class="BLANKROW">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table cellpadding="4" cellspacing="0" border="0">
                    <tr style="display:none">
                        <td class="LABELCOLUMN">
                            <asp:Label ID="lblEvent" runat="server" Text="ACCOUNTING_EVENT:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButton ID="rdoEventAll" runat="server" TextAlign="left" Text="All" Checked="true"
                                GroupName="rdosGroup" />
                        </td>
                        <td  class="LABELCOLUMN" width="1%">
                            <asp:Label ID="Label5" runat="server" Text="Or"></asp:Label>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:RadioButton ID="rdoEventSpecific" runat="server" TextAlign="left" Text="EVENT_TYPE"
                                GroupName="rdosGroup" Style="white-space: nowrap" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAccountingEvent" runat="server" AutoPostBack="false" Width="298"
                                Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="BLANKROW" colspan="5">
                            <hr />
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label6" runat="server" Text="DATES:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButton ID="rdoAllDates" runat="server" TextAlign="left" Text="All" Checked="true"
                                GroupName="rdosDate" />
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label7" runat="server" Text="Or"></asp:Label>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:RadioButton ID="rdoSpecificDate" runat="server" TextAlign="left" Text="SPECIFIC_DATE"
                                GroupName="rdosDate" Style="white-space: nowrap" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="btnDate" runat="server" Visible="True" ImageUrl="~/Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:RadioButton ID="rdoGreaterDate" runat="server" TextAlign="left" Text="DATE_GREATER_THAN"
                                GroupName="rdosDate" Style="white-space: nowrap" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:RadioButton ID="rdoLessDate" runat="server" TextAlign="left" Text="DATE_LESS_THAN"
                                GroupName="rdosDate" Style="white-space: nowrap" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="BLANKROW" colspan="5">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label8" runat="server" Text="REVERSAL_DESCRIPTION:"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtDescription" MaxLength="50" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
      
       //Set the fields to disabled to start. Modify when clicked
       document.getElementById('<%=ddlAccountingEvent.ClientId %>').disabled = true;
       document.getElementById('<%=txtDate.ClientId %>').disabled = true;                

      
        function selectEvent(){
            
            if (document.getElementById('<%=rdoEventAll.ClientId %>').checked){
                document.getElementById('<%=ddlAccountingEvent.ClientId %>').disabled = true;
            } else {
                document.getElementById('<%=ddlAccountingEvent.ClientId %>').disabled = false;                
            }
        }
        
        function selectDate(){
            
            if (!document.getElementById('<%=rdoAllDates.ClientId %>').checked){
                document.getElementById('<%=txtDate.ClientId %>').disabled = false;
            } else {
                document.getElementById('<%=txtDate.ClientId %>').disabled = true;                
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="cntButtons" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="Back">
    </asp:Button>
    <asp:Button ID="btnExecute" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE LONGTEXTIMGBUTTON"
        Text="PROCESS_RECORDS" Style="width: auto"></asp:Button>
</asp:Content>
