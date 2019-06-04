<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"  
    CodeBehind="AMLRegulatoryReportForm.aspx.vb" Theme="Default"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.AMLRegulatoryReportForm" %>

<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="" style="padding-left:15px;"
                width="55%">
            </table>
            <table class="formGrid" border="0" cellspacing="1" cellpadding="0" style="padding-left: 15px;">
                <tr>
                    <td>
                        <asp:Label ID="lblAuthority" runat="server">Authority:</asp:Label>
                        &nbsp;&nbsp;<asp:DropDownList ID="ddlAuthority" runat="server" AutoPostBack="false"
                            Width="212px">
                                </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
<%--    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton"></asp:Button>
    </div>--%>
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js" type="text/javascript"></script>

    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Text="Generate Report Request" Width="200px" />
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $("form > *").change(function () {
                enableReport();
            });
        });


        function enableReport() {
            var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
            btnGenReport.disabled = false;
        }

    </script>
</asp:Content>
