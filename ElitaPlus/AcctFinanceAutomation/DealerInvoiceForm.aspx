<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" CodeBehind="DealerInvoiceForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DealerInvoiceForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET" />
    <script type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>               
            <tr>
                <td align="left">
                    <asp:Label ID="lblDealer" runat="server">Dealer</asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="Label1" runat="server">Accounting_Period</asp:Label>
                </td>
                <td align="left" >
                    <asp:Label ID="Label2" runat="server">Report_Type</asp:Label>
                </td>                       
            </tr>
            <tr>
                <td align="left">
                    <asp:DropDownList ID="ddlDealer" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:275px;"></asp:DropDownList>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlAcctPeriodYear" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;"></asp:DropDownList>
                    <asp:DropDownList ID="ddlAcctPeriodMonth" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;">                        
                    </asp:DropDownList>                    
                </td>
                <td align="left">
                    <asp:RadioButton ID="rdoSummary" runat="server" Text="SUMMARY" Checked="True" AutoPostBack="false" GroupName="ReportTypeGroup"></asp:RadioButton>
                    <asp:RadioButton ID="rdoDetail" runat="server" Text="DETAIL" AutoPostBack="false" GroupName="ReportTypeGroup"></asp:RadioButton>
                </td>
                <td align="left">
                    <input type="button" id="btnLoad" runat="server" onclick="LoadInvoiceTest();" class="FLATBUTTON" value="Load Invoice" style="width:120px;" />
                    <input type="button" id="btnPrint" runat="server" onclick="printDivNewWindow('divInvoice');" class="FLATBUTTON" value="Printable Version" style="width:120px;" disabled="disabled" />    
                    <input type="button" id="btnDownload" runat="server" onclick="downloadAttachment();" class="FLATBUTTON" value="Download XML" style="width:120px;" />    
                    <asp:Button ID="BtnReloadInvoice" runat="server" Text="RELOAD_INVOICE" SkinID="AlternateLeftButton" class="FLATBUTTON"  style="width:120px;"  ></asp:Button>
                    <input type="hidden" id="xmlDownloadTestResponse" runat="server" />
                    <%--<asp:Button ID="btnDownLoadXML_WRITE" Style="cursor: hand; background-repeat: no-repeat" runat="server" Text="DOWNLOAD_XML" Width="120px" CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>--%>
                </td>
            </tr>
        </tbody>                
    </table>        
    <script language="javascript" type="text/javascript">

        function hide(elements) {
            if (elements) {
                elements = elements.length ? elements : [elements];
                for (var index = 0; index < elements.length; index++) {
                    elements[index].style.display = 'none';
            }
        }
}
        function hideErrorMessagePanel() {
            // diasable the error message panel
            document.getElementById('divDownloadMessage').style.visibility = 'collapse';
            document.getElementById('divInvoice').innerHTML = "";
        }

        function uniqueId() { return (new Date()).getTime(); }

        function LoadInvoiceTest() {
            var dealerID = $("select[name*=ddlDealer]").val();
            var invMonth = $("select[name*=ddlAcctPeriodYear]").val() + $("select[name*=ddlAcctPeriodMonth]").val();

            hideErrorMessagePanel();

            var rpttype;

            if (document.getElementById('<%= rdoSummary.ClientID %>').checked) {
                rptType = 'S';
            } else if (document.getElementById('<%= rdoDetail.ClientID %>').checked) {
                rptType = 'D';
            } 

            var strURL = "DealerInvoicePreview.aspx?DealerID=" + dealerID + "&InvoiceMonth=" + invMonth + "&RptType=" + rptType + '&uid=' + uniqueId();
            $('#divInvoice').load(strURL, function () {
                $("input[name*=btnPrint]").prop("disabled", "");
            });
            //$('#divInvoice').load(strURL);            
        }

        function printDivNewWindow(divID) {
            document.getElementById('divDownloadMessage').style.visibility = 'collapse';            
            var newWindow = window.open();
            newWindow.document.write(document.getElementById(divID).innerHTML);
            newWindow.print();
        }

        function downloadAttachment(divID) {

            hideErrorMessagePanel();
            var dealerID = $("select[name*=ddlDealer]").val();
            var invMonth = $("select[name*=ddlAcctPeriodYear]").val() + $("select[name*=ddlAcctPeriodMonth]").val();

            var strURL = "InvoiceXmlDownload.aspx?DealerID=" + dealerID + "&InvoiceMonth=" + invMonth + "&Test=true" + "&timestamp=" + Date.now();
            
            $('#<%= xmlDownloadTestResponse.ClientID %>').load(strURL, function (responseTxt, statusTxt, xhr) {
                if (statusTxt === "success") {
                    strURL = "InvoiceXmlDownload.aspx?DealerID=" + dealerID + "&InvoiceMonth=" + invMonth + "&Test=false";
                    var newWindow = window.open(strURL);
                } else {
                    document.getElementById('divDownloadMessage').innerHTML = "<h2>" + responseTxt + "</h2>";
                    document.getElementById('divDownloadMessage').style.visibility = 'visible';
                }      
            });
        }
        
    </script>   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="divInvoice"></div>    
    <div id="divDownloadMessage" style="visibility:collapse">
        
    </div>    
</asp:Content>


