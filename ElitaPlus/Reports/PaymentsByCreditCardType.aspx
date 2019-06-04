<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentsByCreditCardType.aspx.vb"
     Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingSummaryByCreditCard" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
    Theme="Default" %>
<%@ Register src="../Common/UserControlAvailableSelected_new.ascx" tagname="UserControlAvailableSelected" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server"> 
    <style>
        .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
        .auto-style1 {
            cursor: hand;
            width: 26px;
            text-decoration: none;
            font-family: Verdana, Arial, Helvetica, Verdana, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: normal;
            height: 20px;
            border: 1px solid #999999;
            background-color: #dee3e7;
            background-repeat: no-repeat;
        }
        .auto-style2 {
            width: 25px;
            cursor: hand;
            text-decoration: none;
            font-family: Verdana, Arial, Helvetica, Verdana, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: normal;
            height: 20px;
            border: 1px solid #999999;
            background-color: #dee3e7;
            background-image: url('../Navigation/images/icons/up.gif');
            background-repeat: no-repeat;
        }
        .auto-style3 {
            width: 25px;
            cursor: hand;
            text-decoration: none;
            font-family: Verdana, Arial, Helvetica, Verdana, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: normal;
            height: 20px;
            border: 1px solid #999999;
            background-color: #dee3e7;
            background-image: url('../Navigation/images/icons/down.gif');
            background-repeat: no-repeat;
        }
    </style>    	        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
            <div class="stepformZone">
                <table class="formGrid" align="left" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                    width="100%">
                     <tr>
                        <td nowrap align="right" colspan="1" style="width: 1%">
                        </td>
                        <td align="center" colspan="3" width="99%">
                            <table border="0" cellpadding="0" cellspacing="0" align="left" style="width: 70%">
                                <tr align="center">
                                    <td align="right" nowrap valign="baseline">
                                        *&nbsp;<asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
                                    </td>
                                    <td align="left" nowrap>
                                        &nbsp;
                                        <asp:TextBox ID="moBeginDateText" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"
                                            TabIndex="1"></asp:TextBox>
                                        <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                                    </td>
                                    <td align="right" nowrap valign="baseline">
                                        *
                                        <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>:
                                    </td>
                                    <td align="left" nowrap>
                                        &nbsp;
                                        <asp:TextBox ID="moEndDateText" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"
                                            TabIndex="1"></asp:TextBox>
                                        <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>    
                    <tr>
                        <td class="BLANKROW" colspan="4">
                            <hr />
                        </td>
                    </tr>                                                                       
                    <tr>
                        <td nowrap align="right" colspan="1" style="width: 1%">
                        </td>
                        <td align="center" colspan="3" width="99%">
                            <table class="formGrid" border="0" cellpadding="0" cellspacing="0" align="left" style="width: 80%">
                             <tr id="Tr1" runat="server">
                                  <td colspan="1" style="vertical-align: top;" align="right" width="15%">*
                                      <asp:Label ID="lblCompany" runat="server">COMPANY</asp:Label>:
                                 </td>
                                 <td align="center" nowrap width="85%">
					                <table id="tblCompany" cellspacing="0" cellpadding="6" align="center" border="0">
						                <tr>
							                <td style="height: 13px" align="left">
								                <asp:Label ID="AvailableCompanyLabel" runat="server">AVAILABLE</asp:Label></td>
							                <td style="height: 13px"></td>
							                <td style="height: 13px" align="left">
								                <asp:Label ID="SelectedCompanyLabel" runat="server">SELECTED</asp:Label></td>
						                </tr>
						                <tr>
							                <td style="height: 118px" align="left">
								                <asp:ListBox ID="AvailableCompanyList" runat="server" Width="316px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
							                <td style="height: 118px" align="center">
								                <asp:Button ID="AddCompany" Style="cursor: hand" runat="server" Text=">>" UseSubmitBehavior="false" OnClick="AddCompany_Click"></asp:Button><br /><br />
								                <asp:Button ID="RemoveCompany" Style="cursor: hand" runat="server" Text="<<" UseSubmitBehavior="false" OnClick="RemoveCompany_Click"></asp:Button></td>
							                <td style="height: 118px" align="left">
								                <asp:ListBox ID="SelectedCompanyList" runat="server" Width="316px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
						                </tr>
					                </table> 									
                                 </td>
                             </tr>     
                            </table>
                        </td>    
                    </tr>
                    <tr><td class="BLANKROW" colspan="4">
                            <hr />
                        </td>
                    </tr>                 
                    <tr>
                        <td nowrap align="right" colspan="1" style="width: 1%">
                        </td>
                        <td align="center" colspan="3" width="99%">
                            <table class="formGrid" border="0" cellpadding="0" cellspacing="0" align="left" style="width: 80%">
                             <tr>
                                <td colspan="1" style="vertical-align: top;" align="right" width="15%">*
                                    <asp:Label ID="lblDealer" runat="server">DEALER</asp:Label>:
                                </td>                               
                                 <td align="center" nowrap width="85%">
					                <table id="tblDealer" cellspacing="0" cellpadding="6" align="center" border="0">
						                <tr>
							                <td style="height: 13px" align="left">
								                <asp:Label ID="Label1" runat="server">AVAILABLE</asp:Label></td>
							                <td style="height: 13px"></td>
							                <td style="height: 13px" align="left">
								                <asp:Label ID="Label2" runat="server">SELECTED</asp:Label></td>
						                </tr>
						                <tr>
							                <td style="height: 118px" align="left">
								                <asp:ListBox ID="AvailableDealerList" runat="server" Width="316px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
							                <td style="height: 118px" align="center">
								                <asp:Button ID="AddDealer" Style="cursor: hand" runat="server" Text=">>" UseSubmitBehavior="false" OnClick="AddDealer_Click"></asp:Button><br /><br />
								                <asp:Button ID="RemoveDealer" Style="cursor: hand" runat="server" Text="<<" UseSubmitBehavior="false" OnClick="RemoveDealer_Click"></asp:Button></td>
							                <td style="height: 118px" align="left">
								                <asp:ListBox ID="SelectedDealerList" runat="server" Width="316px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
						                </tr>
					                </table> 									
                                 </td>
                             </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td class="BLANKROW" colspan="4">
                            <hr />
                        </td>
                    </tr> 
                    <tr>
                        <td nowrap align="right" colspan="1" style="width: 1%">
                        </td>
                        <td align="center" colspan="3" width="99%">
                            <table class="formGrid" border="0" cellpadding="0" cellspacing="0" align="left" style="width: 80%"> 
                               <tr>
                                <td  colspan="1" style="vertical-align: top;" align="right" width="15%">*
                                    <asp:Label ID="lblCreditCard" runat="server">CREDIT_CARD</asp:Label>:
                                </td>                               
                                 <td align="center" nowrap width="85%">
					                <table id="tblGrid" cellspacing="0" cellpadding="6" align="center" border="0">
						                <tr>
							                <td style="height: 13px" align="left">
								                <asp:Label ID="Label3" runat="server">AVAILABLE</asp:Label></td>
							                <td style="height: 13px"></td>
							                <td style="height: 13px" align="left">
								                <asp:Label ID="Label4" runat="server">SELECTED</asp:Label></td>
						                </tr>
						                <tr>
							                <td style="height: 118px" align="left">
								                <asp:ListBox ID="AvailableCreditCardList" runat="server" Width="316px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
							                <td style="height: 118px" align="center">
								                <asp:Button ID="AddCreditCard" Style="cursor: hand" runat="server" Text=">>" UseSubmitBehavior="false" OnClick="AddCreditCard_Click"></asp:Button><br /><br />
								                <asp:Button ID="RemoveCreditCard" Style="cursor: hand" runat="server" Text="<<" UseSubmitBehavior="false" OnClick="RemoveCreditCard_Click"></asp:Button></td>
							                <td style="height: 118px" align="left">
								                <asp:ListBox ID="SelectedCreditCardList" runat="server" Width="316px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
						                </tr>
					                </table> 									
                                 </td>
                             </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td class="BLANKROW" colspan="4">
                            <hr />
                    </td></tr>                                   
                </table>
            </div>
        </div>
        <div class="btnZone">
            
   <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Text="Generate Report Request" Width="200px" />
                                                     
            <%--<asp:Button ID="Button1" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton">
            </asp:Button>--%>
        </div>

     <script type="text/javascript">

         $(document).ready(function () {
             $("form > *").change(function () {
                 enableReport();
             });
         });


         function enableReport() {
             //debugger
             var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
                btnGenReport.disabled = false;
            }



		</script>
</asp:Content>
