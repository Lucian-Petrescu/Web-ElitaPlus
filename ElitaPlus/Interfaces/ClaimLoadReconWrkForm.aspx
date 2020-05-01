<%@ Page Language="vb" MasterPageFile="~/Navigation/masters/content_default.Master" AutoEventWireup="false" CodeBehind="ClaimLoadReconWrkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimLoadReconWrkForm" title="ClaimLoadReconWrkForm"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" ID="cntMain">
    <table id="tblOuter2" style="margin: 5px;" height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Width="100%" BorderStyle="None">
                    <table id="tblMain1" cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center" bgcolor="#fef9ea" border="0">
                        <tr>
                            <td style="height:30px; text-align:center">
                                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME:</asp:Label>
                                <asp:TextBox ID="moFileNameText" runat="server" Width="200px" Visible="True" ReadOnly="True" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                    border-bottom: black 1px solid" cellspacing="1" cellpadding="1" width="100%"
                                    bgcolor="#d5d6e4" border="0">
                                    <tr>
                                        <td style="height: 19px"></td>
                                    </tr>
                                    <tr id="trPageSize" runat="server">
                                        <td style="width: 627px" valign="top" align="left">
                                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>:&nbsp;
                                            <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="35">35</asp:ListItem>
                                                <asp:ListItem Value="40">40</asp:ListItem>
                                                <asp:ListItem Value="45">45</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                            </asp:DropDownList>
                                            <INPUT id="HiddenSavePagePromptResponse" type="hidden" name="HiddenSavePagePromptResponse" Runat="server"/>
                                            <INPUT id="HiddenIsPageDirty" type="hidden" name="HiddenIsPageDirty" Runat="server"/>
                                            <input id="HiddenIsPartsPageDirty"  type="hidden"  runat="server" />
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="scroller" style="overflow:auto;" align="center">
                                                <table id="Table1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="white-space:nowrap">
                                                            <asp:UpdatePanel ID="updatePanel" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                                        BorderWidth="1px" AllowSorting="false" CellPadding="1" BackColor="#DEE3E7" BorderColor="#999999" BorderStyle="Solid" OnSelectedIndexChanged="BtnViewParts_Click">
                                                                        <SelectedRowStyle Wrap="False" BackColor="Transparent"></SelectedRowStyle>
                                                                        <EditRowStyle Wrap="False" BackColor="AliceBlue"></EditRowStyle>
                                                                        <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                                                                        <RowStyle Wrap="False" BackColor="White"></RowStyle>
                                                                        <HeaderStyle  Wrap="true" ForeColor="#003399"></HeaderStyle>
                                                                       
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="False">
                                                                                <ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblReconWrkID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("CLAIMLOAD_RECON_WRK_ID"))%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="RECORD_TYPE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRecordType" runat="server" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" Columns="2" MaxLength="2"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                                                            
                                                                            <asp:TemplateField HeaderText="REJECT_CODE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White" ></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRejectCode" runat="server" Columns="5" MaxLength="5" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" ReadOnly="true"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REJECT_REASON">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRejectReason" runat="server" Columns="35" ReadOnly="true" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DEALER_CODE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDealerCode" runat="server" Visible="True" Columns="5" MaxLength="5"></asp:TextBox>                                                                                    
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CERTIFICATE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtCertNum" runat="server" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" Columns="20" MaxLength="20"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CLAIM_TYPE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtClaimType" runat="server" Columns="4" MaxLength="4" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="AUTHORIZATION_NUMBER">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAuthNum" runat="server" onFocus="setHighlighter(this)" Columns="12" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EXTERNAL_CREATED_DATE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtExternalDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgExternalDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="COVERAGE_TYPE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtCoverageCode" runat="server" align="right" onFocus="setHighlighter(this)" Columns="4" MaxLength="4"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="RISK_TYPE_ENGLISH">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRiskTypeEnglish" runat="server" align="right" onFocus="setHighlighter(this)" Columns="30" MaxLength="60"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DATE_OF_LOSS">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtLossDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgLossDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CAUSE_OF_LOSS">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtLossCause" runat="server" onFocus="setHighlighter(this)" Columns="5" MaxLength="5"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PROBLEM_DESCRIPTION">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtProbDesc" runat="server" onFocus="setHighlighter(this)" Columns="50" MaxLength="125"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="COMMENTS">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtComments" runat="server" onFocus="setHighlighter(this)" Columns="50" MaxLength="1000"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SPECIAL_INSTRUCTIONS">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSpecialInstruct" runat="server" onFocus="setHighlighter(this)" Columns="50" MaxLength="125"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPAIR_DATE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRepairDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgRepairDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="VISIT_DATE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtVisitDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgVisitDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PICKUP_DATE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Right" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPickupDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgPickupDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REASON_CLOSED">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtReasonClosed" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                                                                        Visible="True" Columns="5" MaxLength="5"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MANUFACTURER">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtManufacturer" runat="server"  onFocus="setHighlighter(this)" Columns="35" MaxLength="50"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MODEL">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtModel" runat="server" onFocus="setHighlighter(this)" Columns="30" MaxLength="30"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SERIAL_NUMBER">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSerialNum" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                                                                        Visible="True" Columns="30" MaxLength="30"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ITEM_DESCRIPTION">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtItemDescription" runat="server" onFocus="setHighlighter(this)" Columns="30" MaxLength="200"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" ></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SERVICE_CENTER_CODE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtServiceCenter" runat="server" onFocus="setHighlighter(this)" Columns="16" MaxLength="16"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PRODUCT_CODE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtProductCode" runat="server" onFocus="setHighlighter(this)" Columns="5" MaxLength="5"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DEFECT_REASON">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDefectReason" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                                                                        Visible="True" Columns="35" MaxLength="50"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPAIR_CODE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRepairCode" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                                                                        Visible="True" Columns="35" MaxLength="50"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="NAME_OF_CALLER">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtCallerName" runat="server" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" Columns="35" MaxLength="50"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CONTACT_NAME">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtContactName" runat="server" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" Columns="35" MaxLength="50"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="INVOICE_NUMBER">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtInvNum" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="AMOUNT">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmount" runat="server" onFocus="setHighlighter(this)" Columns="12" MaxLength="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ESTIMATE_AMOUNT">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtEstimateAmt" runat="server" onFocus="setHighlighter(this)" Columns="12" MaxLength="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="POLICE_REPORT_NUM">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPoliceRptNum" runat="server" onFocus="setHighlighter(this)" Columns="35" MaxLength="50"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="OFFICER_NAME">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtOfficerName" runat="server" onFocus="setHighlighter(this)" Columns="35" MaxLength="200"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="POLICE_STATION">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPoliceStationCode" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="200"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DOCUMENT_TYPE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDocumentType" runat="server" onFocus="setHighlighter(this)" Columns="4" MaxLength="4"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="RG_NUMBER">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRGNumber" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DOCUMENT_AGENCY">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDocAgency" runat="server" onFocus="setHighlighter(this)" Columns="15" MaxLength="15"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DOCUMENT_ISSUE_DATE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDocIssueDate" runat="server" onFocus="setHighlighter(this)" Columns="20" 
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgDocIssueDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ID_TYPE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtIDType" runat="server" onFocus="setHighlighter(this)" Columns="10" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DEVICE_RECEPTION_DATE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDeviceReceptionDate" runat="server" onFocus="setHighlighter(this)" Columns="20" 
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                   <asp:ImageButton ID="imgDeviceReceptionDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPLACEMENT_TYPE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtReplacementType" runat="server" onFocus="setHighlighter(this)" Columns="10" MaxLength="3"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPLACEMENT_MANUFACTURER">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtManufacturerReplacementDevice" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="255"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPLACEMENT_MODEL">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtModelReplacementDevice" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="30"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPLACEMENT_SERIAL_NUMBER">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSerialNumberReplacementDevice" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="30"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPLACEMENT_SKU">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSKUReplacementDevice" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="18"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DEDUCTIBLE_COLLECTED">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDeductibleCollected" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="13"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="LABOR_AMOUNT">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtLaborAmount" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="13"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PARTS_AMOUNT">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPartsAmount" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="13"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SERVICE_CHARGE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtServiceCharge" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="13"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SHIPPING_AMOUNT">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtShippingAmount" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="13"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="TRIP_AMOUNT">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtTripAmount" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="13"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="OTHER_AMOUNT">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtOtherAmount" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="13"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="OTHER_EXPLANATION">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtOtherExplanation" runat="server" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" Columns="35" MaxLength="800"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SERVICE_LEVEL">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtServiceLevel" runat="server" onFocus="setHighlighter(this)" Columns="15" MaxLength="5"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DEALERS_REFERENCE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDealersReference" runat="server" onFocus="setHighlighter(this)" Columns="15" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="POS">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPOS" runat="server" onFocus="setHighlighter(this)" Columns="15" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PROBLEM_FOUND">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtProblemFound" runat="server" onFocus="setHighlighter(this)" Columns="30" MaxLength="200"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="FINAL_STATUS">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtFinalStatus" runat="server" onFocus="setHighlighter(this)" Columns="15" MaxLength="11"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="TECHNICAL_REPORT">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtTechnicalReport" runat="server" onFocus="setHighlighter(this)" Columns="30" MaxLength="500"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DELIVERY_DATE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDeliveryDate" runat="server" onFocus="setHighlighter(this)" Columns="10" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgDeliveryDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="BATCH_NUMBER">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtBatchNumber" runat="server" onFocus="setHighlighter(this)" Columns="15" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="COMMENT_TYPE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtCommentType" runat="server" onFocus="setHighlighter(this)" Columns="30" MaxLength="255"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="INVOICE_DATE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtInvoiceDate" runat="server" onFocus="setHighlighter(this)" Columns="10" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgInvoiceDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                                                            
                                                                            <asp:TemplateField Visible="False">
																				<ItemTemplate>
																					<asp:Label id="lblModifiedDate" runat="server" text='<%# Container.DataItem("modified_date")%>'></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CLAIM_NUMBER">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtClaimNumber" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EXTENDED_STATUS_CODE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtExtendedStatusCode" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EXTENDED_STATUS_DATE">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtExtendedStatusDate" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgExtendedStatusDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle">
                                                                                    </asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="TRACKING_NUMBER">
                                                                                <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtTrackingNumber" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="50"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DEDUCTIBLE_INCLUDED">
                                                                             <HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                             <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                             <ItemTemplate>
                                                                                <asp:TextBox ID="txtDeductbleIncluded" runat="server" onFocus="setHighlighter(this)" Columns="20" MaxLength="50"
                                                                                             onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                             </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:CommandField SelectText="Parts" ShowSelectButton="true" ButtonType="Button"
                                                                                ControlStyle-CssClass="FLATBUTTON" ShowCancelButton="false" />
                                                                        </Columns>
                                                                        <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                        <PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" CssClass="PAGER_LEFT"></PagerStyle>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3" height="28">
                                            <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                            <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo" CausesValidation="False">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" align="left" height="50">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
     <div>
        <asp:Button ID="hiddenTargetControlForModalPopup" runat="server" style="display:none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup"  
            TargetControlID="hiddenTargetControlForModalPopup"
            PopupControlID="pnlPopup" 
            BackgroundCssClass="modalBackground"
            DropShadow="True"
            PopupDragHandleControlID="programmaticPopupDragHandle"
            RepositionMode="RepositionOnWindowScroll" 
            >
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server"  CssClass="modalPopup" style="display:none;width:400px">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;text-align:center;">
                
            </asp:Panel>
            <div align="left" style="position: relative">
                <uc1:ErrorController ID="ErrController2" runat="server"></uc1:ErrorController>
            </div>
            <asp:UpdatePanel ID="updPnlParts" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gvPop" AutoGenerateColumns="false" BackColor="#DEE3E7"
                        BorderWidth="1px" BorderColor="#999999" BorderStyle="Solid">
                        <SelectedRowStyle Wrap="False" BackColor="Transparent"></SelectedRowStyle>
                        <EditRowStyle Wrap="False" BackColor="AliceBlue"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                        <RowStyle Wrap="False" BackColor="White"></RowStyle>
                        <HeaderStyle  Wrap="False" ForeColor="#003399"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Part_Number">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPartNumber" Text='<%#Bind("PART_NUMBER")%>' ReadOnly="true"
                                        Style="text-align: center; width: 100%"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtSKU" MaxLength="50" Text='<%#Bind("PART_SKU")%>'
                                        onchange="javascript:setPartsDirty();"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDesc" MaxLength="30" Text='<%#Bind("PART_DESCRIPTION")%>'
                                        onchange="javascript:setPartsDirty();"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>                                                    
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div style="background-color:AliceBlue">
                &nbsp;<asp:Button ID="btnClose" runat="server" Text="Back" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                    Width="90px" CssClass="FLATBUTTON" Height="20px" />&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="btnApply" runat="server" Text="Apply" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                        Width="100px" CssClass="FLATBUTTON" Height="20px"  CausesValidation="true" />
            </div>
            <br />
        </asp:Panel>
    </div>
    <script>		
			function setDirty(){
			    document.getElementById("<%= HiddenIsPageDirty.ClientID%>").value = "YES"
			}

			function setPartsDirty() {
			    document.getElementById("HiddenIsPartsPageDirty").value = "YES"
			}
							
			function resizeControl(item)
			{
				var browseWidth, browseHeight;
				
				if (document.all)
				{
					browseWidth=document.body.clientWidth;
					browseHeight = document.body.clientHeight;
					//alert( browseWidth + " : " + browseHeight);
				}
				newHeight = parent.document.getElementById("Navigation_Content").clientHeight - 325;
				//alert(screen.height + " : " + newHeight);
				//alert("tblMain: " + parent.document.getElementById("Navigation_Content").clientHeight); 
				if (newHeight < 0)
				    newHeight = newHeight * -1;
				
				browseWidth = browseWidth - 100;
				if (browseWidth < 0)
				    browseWidth = browseWidth * -1;

				item.style.height = newHeight + "px";
				item.style.width = browseWidth + "px";				
			}

			resizeControl(document.getElementById("scroller"));
		</script>
</asp:Content>