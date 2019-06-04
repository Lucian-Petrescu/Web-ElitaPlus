<%@ Page Language="vb" MasterPageFile="../Navigation/masters/ElitaBase.Master" AutoEventWireup="false"
    CodeBehind="vscUploadForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.vscUploadForm"
    Title="Common Upload" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
	<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
				<tr>
					<td style="text-align: center" align="center">
						<table style="width: 75%;" class="formGrid">
							<tr>
								<td style="text-align: right">
                                    <asp:label id="Label1" runat="server">TABLE_NAME</asp:label>:

								</td>
                                <td style="text-align:left">
                                    <asp:dropdownlist id="moTableDrop" runat="server"></asp:dropdownlist>
                                </td>
							</tr>
                            <tr>
                                <td  style="text-align:right">*
								<asp:label id="Label2" runat="server">Filename</asp:label>:</td>
								<td style="text-align:left" >
                                 <asp:FileUpload ID="tableInput" CssClass="popupBtn" runat="server"></asp:FileUpload>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCopyTable_WRITE" runat="server" Text="UPLOAD_FILE" SkinID="ActionButton">
                            </asp:Button>
							</td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                            <tr>
                                <td>
                                    <table class="dataGrid">
                    <asp:Panel ID="panelIntProgControl" runat="server">
                        <uc1:InterfaceProgressControl ID="moInterfaceProgressControl" runat="server"></uc1:InterfaceProgressControl>
                        <asp:Button ID="Button1" Style="display: none" runat="server" Width="0"
                            Height="0"></asp:Button>
                    </asp:Panel>
						</table>
					</td>
				</tr>
			</table>
        </asp:Content>
        <asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
            </asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server" ID="cntMain">
    <div class="dataContainer">
			
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:BoundField HeaderText="PROCESSING_LOG" DataField="Error_Message" />
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
            </asp:GridView>
        </div>
        </div>
        </asp:Content>

