<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VSCVehicleExtractForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.VSCVehicleExtractForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"
    Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
        .auto-style1 {
            height: 22px;
        }
    </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
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
