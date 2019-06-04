<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AgentSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.AgentSearchForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';
        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .StatInactive {
            text-align: left;
            color: #CC0000;
            font-size: 13px;
            font-weight: bold;
        }

        #sub-left {
            float: left;
        }

        #sub-right {
            float: right;
        }

        .clear-both {
            clear: both;
        }
    </style>
    <style type="text/css">
        table.dataRep {
            border-left: 10px solid #0066CC;
            padding: 0px;
            margin: 0px;
            border-bottom: 0px solid #E1E1E1;
        }

            table.dataRep td {
                padding: 4px 6px;
                margin: 0px;
                vertical-align: top;
                font-size: 13px;
                border-bottom: 0px solid #E1E1E1;
            }

                table.dataRep td.noBor {
                    border: none;
                }

                table.dataRep td.bor {
                    border-bottom: 1px solid #999;
                }

            table.dataRep th {
                padding: 3px 6px;
                margin: 0px;
                vertical-align: top;
                font-size: 13px;
                color: #333;
                text-align: left;
                background: url('Images/dataGrdHeadBg.png') repeat-x bottom left;
                border-right: 1px solid #CCC;
                border-bottom: 1px solid #999;
            }

            table.dataRep td.StatActive {
                text-align: left;
                color: #339900;
                font-size: 13px;
                font-weight: bold;
            }

            table.dataRep td.StatPending {
                text-align: left;
                color: #CC6600;
                font-size: 13px;
                font-weight: bold;
            }

        table.dataGrid td.StatInactive {
            text-align: left;
            color: #CC0000;
            font-size: 13px;
            font-weight: bold;
        }

        table.dataRep td.covActive {
            text-align: left;
            color: #009933;
            font-size: 13px;
            font-weight: bold;
        }

        table.dataRep td a {
            color: #0066CC;
        }

            table.dataRep td a:hover {
                color: #0066CC;
            }

        table.dataRep th a {
            color: #333;
            text-decoration: none;
        }

        table.dataGrid th a:hover {
            color: #0066CC;
            text-decoration: underline;
        }

        table.dataRep tr.over {
            background: url('Images/row_hover.png') repeat bottom left;
        }

        table.dataRep tr.out {
            background-color: #fff;
        }

        table.dataRep tr.selected {
            background: #CCC;
        }



        table.dataRep td select {
            background: url('Images/inputBg.png') repeat-x;
            border: 1px solid #999;
            padding: 1px;
            margin: 0px;
            color: #333;
        }
    </style>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>

            <td class="bor" visible="false">Agent_Search_By
               &nbsp;
            </td>

            <td colspan="3" width="85%">
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="LabelCompany" runat="server">Company</asp:Label><br />
                            <asp:DropDownList ID="ddlCompany" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LabelDealer" runat="server">Dealer</asp:Label><br />
                            <asp:DropDownList ID="ddlDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LabelPolicyStatus" runat="server">POLICY_STATUS</asp:Label><br />
                            <asp:DropDownList ID="ddlPolicyStatus" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelCase" runat="server">CASE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxCaseText" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Label ID="LabelCertificateNumber" runat="server">CERTIFICATE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxCertificateNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelClaimNumber" runat="server">CLAIM_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxClaimNumber" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelCustomerFirstName" runat="server">CUSTOMER_FIRST_NAME</asp:Label><br />
                            <asp:TextBox ID="TextBoxCustomerFirstName" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelCustomerLastName" runat="server">CUSTOMER_LAST_NAME</asp:Label><br />
                            <asp:TextBox ID="TextBoxCustomerLastName" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelPhoneNumber" runat="server">PHONE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxPhoneNumber" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelSerialNumber" runat="server">SERIAL_IMEI_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxSerialNumber" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Label ID="LabelInvoiceNumber" runat="server">INVOICE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxInvoiceNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelEmail" runat="server">EMAIL</asp:Label><br />
                            <asp:TextBox ID="TextBoxEmail" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelZip" runat="server">Zip</asp:Label><br />
                            <asp:TextBox ID="TextBoxZip" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblTaxId" runat="server">TAX_ID</asp:Label><br />
                            <asp:TextBox ID="txtTaxId" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblServiceLineNumber" runat="server">SERVICE_LINE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="txtServiceLineNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAccountNumber" runat="server">ACCOUNT_NUMBER</asp:Label><br />
                            <asp:TextBox ID="txtAccountNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblGlobalCustomerNumber" runat="server">GLOBAL_CUSTOMER_NUMBER</asp:Label><br />
                            <asp:TextBox ID="txtGlobalCustomerNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="right" style="padding-right: 80px;">
                            <asp:Button ID="btnNewCase" runat="server" SkinID="SearchButton" Visible="false" Text="Start New Case"></asp:Button>
                        </td>
                        <td align="left">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                <asp:Button ID="btnClassicView" runat="server" SkinID="AlternateLeftButton" Text="Classic View"></asp:Button>
                            </label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_AGENTS" Visible="true" ></asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">&nbsp;
                        
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_OnItemDataBound" OnItemCommand="rep_OnItemCommand" >
                <ItemTemplate>
                    <table class="dataRep" style="width: 100%; border-collapse: collapse" cellspacing="0" rules="all" border="1">
                        <tbody>
                            <tr>
                                <td>
                                    <div id="sub-title">
                                        <div id="sub-left">
                                            <asp:Label ID="lblsummary1" runat="server" Font-Bold="true" Text='<%# Eval("summary1") %>' />
                                            <br />
                                            <%# Eval("summary2") %>
                                        </div>
                                        <div id="sub-right" style="text-align: right">
                                            <asp:HiddenField ID ="hfItemType" runat="server" Value='<%# Eval("itemtype") %>'></asp:HiddenField>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            <br />
                                            <asp:LinkButton CommandName="SelectActionCert" ID="btnEditCert" runat="server"  Visible="false"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonNewCase" runat="server"  Visible="false"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                                            <asp:LinkButton CommandName="SelectActionCase" ID="btnEditCase" runat="server"  Visible="false"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                                            <asp:LinkButton CommandName="SelectAction" ID="btnEditAgent" runat="server" 
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton><br />
                                             <asp:LinkButton CommandName="SelectActionCancelReq" ID="btnCancelReqAgent" runat="server" 
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                                        </div>
                                        <div class="clear-both"></div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <!-- end new layout -->
 </asp:Content>
