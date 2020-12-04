<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertificateExtendedFieldHistory.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CertificateExtendedFieldHistory"
    EnableSessionState="True" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls"
    TagPrefix="iewc" %>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
     <table id="TableFixed" style="width:50%;border:0;" class="summaryGrid">
        <tr>
            <td style="text-align:left">
                <asp:Label ID="FieldNameLabel" runat="server" SkinID="SummaryLabel">Field_NAME</asp:Label>:
            </td>
            <td style="text-align:left;font-weight: bold" >
                <asp:Label runat="server" ID="FieldName"></asp:Label>
            </td>
            <td style="text-align:left" runat="server" id="FieldValueLabelTD">
                <asp:Label ID="FieldValueLabel" runat="server" SkinID="SummaryLabel">FIELD_VALUE</asp:Label>:
            </td>
            <td style="text-align:left;font-weight: bold"  >
                <asp:Label runat="server" ID="FieldValue"></asp:Label>

            </td>
            <td style="text-align:left" colspan="4">                
            </td>
           
        </tr>
  
    </table>
    <br />
    <table>
        <tr id="ExtendedFieldValueHist" runat="server">
            <td style="text-align: left">
                <h2 class="dataGridHeader">
                    <a id="ExtendedFieldValueExpander" href="#">
                        <img alt="sort indicator" src="../App_Themes/Default/Images/sort_indicator_des.png" /></a>
                    <asp:Label ID="ExtendedFieldHistory" runat="server">EXTENDED_FIELD_HISTORY</asp:Label>
                </h2>
            </td>
        </tr>
        <tr id="ExtendedFieldValueHistInfo">
            <td style="border-bottom: none">
                <div style="width: 100%; height: 100%; min-height: 100%">
                    <asp:GridView ID="grdExtendedFieldValueHist" runat="server" AutoGenerateColumns="false"
                        Width="100%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999"
                        SkinID="DetailPageGridView" AllowPaging="false" AllowSorting="false">
                        <Columns>
                            <asp:BoundField DataField="field_name" HeaderText="Field_Name" HeaderStyle-Width="100" />
                            <asp:BoundField DataField="field_value" HeaderText="FIELD_VALUE" HeaderStyle-Width="100" />
                            <asp:BoundField DataField="created_by" HeaderText="CREATED_BY" HeaderStyle-Width="100" />
                            <asp:BoundField DataField="created_date" HeaderText="CREATED_DATE" HeaderStyle-Width="200" />
                            <asp:BoundField DataField="hist_created_by" HeaderText="MODIFIED_BY" HeaderStyle-Width="100" />
                            <asp:BoundField DataField="hist_created_date" HeaderText="MODIFIED_DATE" HeaderStyle-Width="200" />

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <div class="btnZone">
        <div class="">

            <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
        </div>
    </div>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $('#ExtendedFieldValueHistInfo').slideUp();
            $("#ExtendedFieldValueHistInfo").css("display", "none");
        });

        $("#ExtendedFieldValueExpander").click(function () {
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $('#ExtendedFieldValueHistInfo').slideToggle(500, function () {
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                $('#ExtendedFieldValueExpander').html(function () {
                    //change text based on condition
                    return $('#ExtendedFieldValueHistInfo').is(":visible") ? "<img src='../App_Themes/Default/Images/sort_indicator_asc.png'>" : "<img src='../App_Themes/Default/Images/sort_indicator_des.png'>";
                });
            });
        });
    </script>
</asp:Content>
