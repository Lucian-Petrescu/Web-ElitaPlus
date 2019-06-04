<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ReportExtractInputControl.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportExtractInputControl" %>
<script language="JavaScript" src="../Navigation/Scripts/ReportCeScripts.js"></script>

<asp:Panel ID="moFormatPanel" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td td colspan="4">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                    <tr>
                        <td style="text-align: center">
                            <%--<img src="../Navigation/images/ftype_txt2.gif"><br>--%>
                            <asp:Image ImageUrl="../Navigation/images/ftype_txt2.gif" ID="imgView" runat="server" /><br />
                            <asp:RadioButton ID="RadiobuttonView" onclick="toggleReportFormatViewSelection(true);"
                                AutoPostBack="false" TextAlign="right" Text="VIEW REPORT" runat="server" Checked="true">
                            </asp:RadioButton>
                        </td>
                        <td style="text-align: center">
                            <%--<img src="../Navigation/images/ftype_pdf2.gif"><br>--%>
                            <asp:Image ImageUrl="../Navigation/images/ftype_pdf2.gif" ID="imgPdf" runat="server" />
                            <asp:RadioButton ID="RadiobuttonPDF" onclick="toggleReportFormatPDFSelection(true);"
                                AutoPostBack="false" TextAlign="right" Text="CREATE PDF" runat="server" Checked="FALSE">
                            </asp:RadioButton>
                        </td>
                        <td colspan="2" style="text-align: center">
                            <%--<img src="../Navigation/images/ftype_txt2.gif"><br>--%>
                            <asp:Image ImageUrl="../Navigation/images/ftype_txt2.gif" ID="imgTxt" runat="server" />
                            <asp:RadioButton ID="RadiobuttonTXT" onclick="toggleReportFormatTXTSelection(true);"
                                AutoPostBack="false" TextAlign="right" Text="EXPORT DATA" runat="server" Checked="FALSE">
                            </asp:RadioButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" height="20">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr runat="server" height="20">
            <td colspan="4">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                    <tr>
                        <td nowrap class="TD_LABEL" visible="true" width="1%" style="text-align: right">
                            <asp:Label ID="moDestLabel" runat="server" Visible="true" Style="text-align: right">Destination:</asp:Label>
                        </td>
                        <td nowrap id="TDest" visible="true" style="text-align: left" width="25%">
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="moDestDrop" runat="server" Visible="true" AutoPostBack="True">
                            </asp:DropDownList>
                             &nbsp;&nbsp;
                            <asp:Label ID="lblUpdateFileName" runat="server" Visible="false" Style="text-align: right">Rename_File_Name:</asp:Label>                                                        
                            <asp:checkbox ID="chkUpdateFileName" runat="server" Visible="false" />                            
                        </td>
                        <td class="TD_LABEL" style="text-align: right" width="1%">
                            <asp:Label ID="lblLang" runat="server" Visible="False" Style="text-align: right">Language:</asp:Label>
                        </td>
                        <td id="RLang" visible="false" style="text-align: left" runat="server" width="25%">
                            &nbsp;
                            <asp:DropDownList ID="DPLang" runat="server" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                           
                    <tr>
                        <td colspan="4" height="10">
                        </td>
                    </tr>
                    <tr id="RSched" runat="server" height="20" visible="true">
                        <td nowrap class="TD_LABEL" style="text-align: right" width="1%">
                            <asp:Label ID="moSchedLabel" runat="server" Visible="true" Style="text-align: right">Schedule</asp:Label>:
                        </td>
                        <td nowrap id="TSched" visible="true" style="text-align: left" width="25%">
                            &nbsp;
                            <asp:CheckBox ID="moSchedCheck" runat="server" Visible="true" BorderWidth="0" />
                        </td>
                        <td id="TdSchedDate1" nowrap class="TD_LABEL" runat="server" style="display: none; text-align: right;vertical-align:middle;"
                            width="1%">
                            *<asp:Label ID="moSchedDateLabel" runat="server" Style="text-align: right;vertical-align:middle">SCHEDULE_DATE</asp:Label>:
                        </td>
                        <td id="TdSchedDate2" style="text-align: left; display: none" runat="server" width="25%">
                            &nbsp;
                            <asp:TextBox ID="moSchedDateText" runat="server" Width="150px" CssClass="FLATTEXTBOX"></asp:TextBox>
                            <asp:ImageButton ID="BtnSchedDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>          
</asp:Panel>
<asp:Button ID="btnViewHidden" Style="background-color: #fef9ea; display:none" runat="server"></asp:Button>
<asp:Button ID="btnErrorHidden" Style="background-color: #fef9ea; display:none" runat="server"></asp:Button>
<input id="HiddenRptPromptResponse" type="hidden" name="HiddenRptPromptResponse" runat="server" />
<input id="isProgressVisible" type="hidden" name="isProgressVisible" runat="server" />
<input id="moReportCeStatus" type="hidden" name="moReportCeStatus" runat="server" />
<input id="moReportCeViewer" type="hidden" name="moReportCeViewer" runat="server" />
<input id="moReportCeErrorMsg" type="hidden" name="moReportCeViewer" runat="server" />
<input id="moReportCeCloseTimer" type="hidden" name="moReportCeCloseTimer" runat="server" />
<input id="moReportCeAction" type="hidden" name="moReportCeAction" runat="server" />
<input id="moReportCeFtp" type="hidden" name="moReportCeFtp" runat="server" />
<input id="moModifiedFileName" type="hidden" name="moModifiedFileName" runat="server" />
