<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="ServerForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.ServerForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
        .StatusBoxNeutral
        {
            border: 1px solid #999;
            padding: 5px;
            background-color: #f1f1f1;
        }
        .StatusBoxGood
        {
            border: 1px solid #00FF00;
            padding: 5px;
            background-color: #f1f1f1;
        }
        .StatusBoxBad
        {
            border: 1px solid #FF0000;
            padding: 5px;
            background-color: #f1f1f1;
        }
    </style>
    <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="TABS">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="SERVER">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: center; width: 100%">
                            <table cellpadding="4" cellspacing="0" border="0" style="border: #999999 1px solid;
                                background-color: #f1f1f1; height: 98%; width: 100%">
                                <tr>
                                    <td style="height: 7px;" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="DescriptionLabel" runat="server">DESCRIPTION</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="DescriptionTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="FtpHostNameLabel" runat="server">FTP_HOSTNAME</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="FtpHostNameTextBox" runat="server" Width="99%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="HubRegionLabel" runat="server">HUB_REGION</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="HubRegionTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="FelitaFtpHostNameLabel" runat="server">FELITA_FTP_HOSTNAME</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="FelitaFtpHostNameTextBox" runat="server" Width="99%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="MachinePrefixLabel" runat="server">MACHINE_PREFIX</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="MachinePrefixTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="FtpHostPathLabel" runat="server">FTP_HOST_PATH</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="FtpHostPathTextBox" runat="server" Width="99%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="EnvironmentLabel" runat="server">ENVIRONMENT</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="EnvironmentextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="FtpTriggerExtensionLabel" runat="server">FTP_TRIGGER_EXTENSION</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="FtpTriggerExtensionTextBox" runat="server" Width="99%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="LdapIpLabel" runat="server">LDAP_IP</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="LdapIpTextBox" runat="server" Width="95%" AutoPostBack="False" CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="FtpSplitPathLabel" runat="server">FTP_SPLIT_PATH</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="FtpSplitPathTextBox" runat="server" Width="99%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="CrystalSdkLabel" runat="server">CRYSTAL_SDK</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="CrystalSdkTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="ServiceOrderImageHostNameLabel" runat="server">SERVICEORDER_IMAGE_HOSTNAME</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="ServiceorderImageHostnameTextBox" runat="server" Width="99%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="SmartStreamHostNameLabel" runat="server">SMARTSTREAM_HOSTNAME</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="SmartstreamHostnameTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="SmartStreamAPUploadLabel" runat="server">SMARTSTREAM_AP_UPLOAD</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="SmartStreamAPUploadTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="SmartStreamGLUploadLabel" runat="server">SMARTSTREAM_GL_UPLOAD</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="SmartStreamGLUploadTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="SmartStreamGLStatusLabel" runat="server">SMARTSTREAM_GL_STATUS</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="SmartStreamGLStatusTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="DatabaseNameLabel" runat="server">DATABASE_NAME</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="DatabaNameTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="UniqueDBLabel" runat="server">UNIQUE_DB_NAME</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="UniqueDBTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="CrystalViewerLabel" runat="server">CRYSTAL_VIEWER</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="CrystalViewerTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="BatchHostnameLabel" runat="server">BATCH_HOSTNAME</asp:Label>
                                    </td>
                                    <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="BatchHostnameTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="EuropeanPrivacyLabel" runat="server">EUROPEAN_PRIVACY</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:DropDownList ID="EuropeanPrivacyDrop" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="AcctBalHostnameLabel" runat="server">ACCT_BAL_HOSTNAME</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="AcctBalHostnameTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="NoOfParallelProcessesLabel" runat="server">NO_OF_PARALLEL_PROCESSES</asp:Label>
                                    </td>
                                     <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="NoOfParallelProcessesTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                                        <asp:Label ID="CommitFrequencyLabel" runat="server">COMMIT_FREQUENCY</asp:Label>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 50%">
                                        <asp:TextBox ID="CommitFrequencyTextBox" runat="server" Width="95%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK">
                            </asp:Button>
                            <asp:Button ID="btnApply_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE"
                                Text="SAVE"></asp:Button>
                            <asp:Button ID="btnUndo_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO"
                                Text="UNDO"></asp:Button>
                            <asp:Button ID="btnNew_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
                                Text="NEW"></asp:Button>
                            <asp:Button ID="btnCopy_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
                                Text="New_With_Copy" Width="136px"></asp:Button>
                            <asp:Button ID="btnDelete_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR"
                                Text="DELETE"></asp:Button>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="DIAGNOSTICS">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: center; width: 100%">
                            <table cellpadding="4" cellspacing="0" border="0" style="border: #999999 1px solid;
                                background-color: #f1f1f1; height: 98%; width: 100%">
                                <tr>
                                    <td>
                                        Batch Services<hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="StatusBoxNeutral" id="dvStatus">
                                            <asp:Label ID="status_LABEL" runat="server" Text="STATUS"></asp:Label>: &nbsp;
                                            <asp:Label ID="statusResult_LABEL" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 3px;">
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>
                                        User:
                                        <asp:TextBox ID="batchUser_TEXT" runat="server"></asp:TextBox>&nbsp;&nbsp; Password:
                                        <asp:TextBox ID="batchPass_TEXT" runat="server" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;
                                        Group:
                                        <asp:TextBox ID="batchGroup_TEXT" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Test Current Instance via Oracle:&nbsp;&nbsp;
                                        <asp:ImageButton Style="cursor: hand;" ID="btnExecuteCurrent" ImageUrl="../Navigation/images/icons/yes_icon.gif"
                                            runat="server" CausesValidation="false"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Test Specific Batch Server:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdBatch" runat="server" Width="100%" OnRowCreated="ItemCreated"
                                            AllowPaging="False" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                            CssClass="DATAGRID">
                                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                            <RowStyle CssClass="ROW"></RowStyle>
                                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateField Visible="true" ShowHeader="false">
                                                    <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton Style="cursor: hand;" ID="btnExecute" ImageUrl="../Navigation/images/icons/yes_icon.gif"
                                                            runat="server" CausesValidation="false"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField Visible="true" DataField="description" HeaderText="Environment" />
                                                <asp:BoundField Visible="true" DataField="batch_hostname" HeaderText="Batch Hostname" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack2" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
<input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
</asp:Content>
