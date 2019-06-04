<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FileReconciliationReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.FileReconciliationReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
    Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
   <style>
            .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
   </style> 
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>  
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">       

    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                        <tr>
                            <td id="Td2" runat="server" colspan="2">
                                <table>
                                    <tbody>
                                        <uc1:MultipleColumnDDLabelControl_New ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td3" runat="server" colspan="2">
                                <table>
                                    <tbody>
                                        <uc1:MultipleColumnDDLabelControl_New ID="moDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" style="padding-left: 1px;">
                                <table class="formGrid" border="0" cellpadding="2" cellspacing="3">
                                    <tr><td>&nbsp;</td></tr>
                                    <tr id="Tr2" runat="server">
                                        <td align="left">*
                                            <asp:Label ID="lblFileType" runat="server">FILE_TYPE</asp:Label>:
                                        </td>
                                         <td>
                                            <asp:DropDownList ID="ddlFileType" runat="server" Width="170px" onchange="FileTypeChanged();">  </asp:DropDownList>
                                        </td>                                                                                                   
                                    </tr>                                
                                    <tr><td>&nbsp;</td></tr>                                                                                    
                                    <tr id="Tr3" runat="server">
                                        <td align="left">*
                                            <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE</asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="175px" onchange="DateChanged();"></asp:TextBox>
                                            <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                        </td>                                                                                                    
                                        <td align="right" >*
                                            <asp:Label ID="lblEndDate" runat="server">END_DATE</asp:Label>:    
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="175px" onchange="DateChanged();"></asp:TextBox>
                                            <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr><td>&nbsp;</td></tr>                                                                                    
                                    <tr id="Tr4" runat="server">
                                        <td align="left">
                                            <asp:Label ID="lblCutOffDate" runat="server">CUT_OFF_DATE</asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCutOffDate" runat="server" SkinID="MediumTextBox" Width="175px" onchange="DateChanged();"></asp:TextBox>
                                            <asp:ImageButton ID="BtnCutOffDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                        </td> 
                                        <td align="left">
                                            <asp:Label ID="lblIncVoidedRecords" runat="server">INCLUDE_VOIDED_RECORDS</asp:Label>    
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkIncVoidedRecords" Runat="server" TextAlign="Left" />
                                        </td>
                                    </tr>
                                  </table>
                                </td>
                            </tr>
                </table>
            </div>
          </div>
    <div class="btnZone">
            <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Text="Generate Report Request" Width="200px" />
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $("form > *").change(function () {
                enableReport();
            });
        });

        function DateChanged() {
            enableReport();
        }

        function enableReport() {
            var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
            btnGenReport.disabled = false;
        }

        function FileTypeChanged() {
            var objFileType = document.getElementById("ctl00_BodyPlaceHolder_ddlFileType");
            var FileTypeText = objFileType.options[objFileType.selectedIndex].text;
            if (FileTypeText == 'Dealer File')
            {
                document.getElementById("ctl00_BodyPlaceHolder_lblIncVoidedRecords").style.visibility = "visible";
                document.getElementById("ctl00_BodyPlaceHolder_chkIncVoidedRecords").style.visibility = "visible";
                document.getElementById("ctl00_BodyPlaceHolder_chkIncVoidedRecords").checked = false
            }
            else
            {
                document.getElementById("ctl00_BodyPlaceHolder_lblIncVoidedRecords").style.visibility = "hidden";
                document.getElementById("ctl00_BodyPlaceHolder_chkIncVoidedRecords").style.visibility = "hidden";
            }
        }
    </script>
</asp:Content>