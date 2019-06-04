<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimStatusDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimStatusDetailForm" Theme="Default"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
    <script type="text/javascript" language="javascript">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td align="left">
                <asp:Label ID="LabelCustomerName" runat="server" SkinID="SummaryLabel">Customer_Name:</asp:Label>
            </td>
            <td align="left" class="bor" style="font-weight: bold" runat="server" id="CustomerNameTD">
                <strong></strong>
            </td>
            <td align="left">
                <asp:Label ID="LabelClaimNumber" runat="server" SkinID="SummaryLabel">Claim_Number:</asp:Label>
            </td>
            <td align="left" class="bor" style="font-weight: bold" runat="server" id="ClaimNumberTD">
                <strong></strong>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="../Navigation/Scripts/GlobalHeader.js" />
        </Scripts>
    </asp:ScriptManager>
    &nbsp;<asp:Panel ID="pnlPageSize" runat="server" width="100%" BorderStyle="None" EnableViewState="False" HorizontalAlign="Left">
        <div class="dataContainer">    
          <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td valign="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="Label3"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
                            SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        <input id="HiddenIsPageDirty" type="hidden" runat="server" />
                        <input id="HiddenSavePagePromptResponse" type="hidden" runat="server" />
                    </td>
                </tr>
            </table>
          </div>
       </div> 
    </asp:Panel>
    <div class="dataContainer">
        <asp:DataGrid ID="DataGridDropdowns" runat="server" AllowPaging="True"  PagerStyle-HorizontalAlign="Center" AllowSorting="True"
            OnItemCreated="DataGridDropdowns_ItemCreated" PageSize="10" AutoGenerateColumns="False" SkinID="DetailPageDataGrid" Width="100%">
            <SelectedItemStyle Wrap="False"></SelectedItemStyle>
            <EditItemStyle Wrap="False"></EditItemStyle>
            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
            <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
            <Columns>
                <asp:TemplateColumn Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblClaimStatusId" runat="server" Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderImageUrl="../Navigation/images/icons/check.gif">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxItemSel" runat="server" Checked='<%# IIf(Container.DataItem("SELECTED") Is DBNull.Value, "", Container.DataItem("SELECTED")) = "Y"%>'
                             Enabled='<%# IIf(Container.DataItem("Enabled") Is DBNull.Value, "Y", Container.DataItem("Enabled")) = "Y"%>' data-enabled='<%# IIf(Container.DataItem("Enabled") Is DBNull.Value, "Y", Container.DataItem("Enabled")) = "Y"%>' />
                      
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="EXTENDED_CLAIM_STATUS">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="28%"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label Width="100%" ID="lblExtendedClaimStatus" runat="server" Text='<%# IIf(Container.DataItem("extended_claim_status") is dbNull.Value, "", Container.DataItem("extended_claim_status")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ExtendedClaimStatusDropDownList" runat="server" Visible="True" Enabled='<%# IIf(Container.DataItem("Enabled") Is DBNull.Value, "Y", Container.DataItem("Enabled")) = "Y"%>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="STATUS_DATE">
                    <HeaderStyle  HorizontalAlign="Center" Width="20%"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="left" BackColor="White"></ItemStyle>
                    <ItemTemplate>
                        <asp:TextBox ID="txtStatusDate" Width="80%" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                            Visible="True" Enabled='<%# IIf(Container.DataItem("Enabled") Is DBNull.Value, "Y", Container.DataItem("Enabled")) = "Y"%>'></asp:TextBox>
                        <asp:ImageButton ID="imgStatusDate" runat="server" TabIndex="-1" ImageUrl="../Common/Images/calendarIcon2.jpg"  Enabled='<%# IIf(Container.DataItem("Enabled") Is DBNull.Value, "Y", Container.DataItem("Enabled")) = "Y"%>'>
                        </asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="COMMENT">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="35%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:TextBox Width="400px" ID="txtComment" runat="server" MaxLength="300" Text='<%# IIf(Container.DataItem("status_comments") is dbNull.Value, "", Container.DataItem("status_comments")) %>'
                            cssclass="FLATTEXTBOX" Enabled='<%# IIf(Container.DataItem("Enabled") Is DBNull.Value, "Y", Container.DataItem("Enabled")) = "Y"%>'>
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="OWNER">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:DropDownList ID="dropDownOwner" runat="server" Width="100%" Enabled="false"
                            Visible="True">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblIsNew" runat="server" Visible="False" Text='<%# IIf(Container.DataItem("SELECTED") is dbNull.Value, "", Container.DataItem("SELECTED")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblClaimStatusGroupId" runat="server" Visible="False" Text='<%# CheckNull(Container.DataItem("claim_status_by_group_id")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>                                                     
                <asp:TemplateColumn Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblListItemId" runat="server" Visible="False" Text='<%# CheckNull(Container.DataItem("list_item_id")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupNumber" runat="server" Visible="False" Text='<%# IIf(Container.DataItem("group_number") is dbNull.Value, "", Container.DataItem("group_number")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle PageButtonCount="5" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
        </asp:DataGrid>
    </div>
    <div class="dataContainer">
        <h2 class="searchGridHeader"><%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage("CLAIM_AGING_DETAILS")%></h2>
        <table width="100%" class="searchGrid">
            <tbody>
                <asp:Repeater runat="server" ID="moClaimRepeater">
                    <ItemTemplate>
                        <tr>
                            <td align="left">                                
                                <input type="button" name="Expand" style="cursor: pointer" id="Expand" value="Show" onclick="ShowHideClaimAgingDetails('<%# Eval("claimId").ToString()%>');"></input>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="dataContainer">
        <div id="Child" style="display: none;"></div>
    </div>    
    <div class="btnZone">
        <div class="">
            <asp:Button ID="BackButton_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Back">
            </asp:Button>&nbsp;
            <asp:Button ID="SaveButton_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Save">
            </asp:Button>&nbsp;
            <asp:Button ID="CancelButton_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Cancel">
            </asp:Button>&nbsp;
            <asp:Button ID="NewButton_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New">
            </asp:Button>&nbsp;
            <asp:HiddenField ID="HdnFldFilterSize" runat="server" />
        </div>
        <input type="hidden" id="CMD" value="" runat="server" />
        <input type="hidden" id="CopyDealerId" value="" runat="server" />
    </div>
<script type="text/javascript" language="javascript">

        function setDirty() {
            var inpId = document.getElementById('<%= HiddenIsPageDirty.ClientID %>')
            inpId.value = "YES"
        }

        function LoadXmlDoc(fileName) {
            if (window.XMLHttpRequest) {
                xhttp = new XMLHttpRequest();
            }
            else {
                xhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhttp.open("GET", fileName, false);
            xhttp.send();
            return xhttp.responseXML;
        }

        function GetXmlDoc(xmlString) {
            if (window.DOMParser) {
                parser = new DOMParser();
                xmlDoc = parser.parseFromString(xmlString, "text/xml");
            }
            else // Internet Explorer
            {
                xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                xmlDoc.async = false;
                xmlDoc.loadXML(xmlString);
            }
            return xmlDoc;
        }

        function FormatDateTimeToLocal(completeDate, _id) {
            var _year = completeDate.substring(0, 4);
            var _month = completeDate.substring(5, 7);
            _month = _month - 1;
            var _day = completeDate.substring(8, 10);
            var _hour = completeDate.substring(11, 13);
            var _minute = completeDate.substring(14, 16);
            var _second = completeDate.substring(17, 19);
            var d = new Date(_year, _month, _day, _hour, _minute, _second, 0);
            var n = d.toLocaleDateString()
            var t = d.toLocaleTimeString();
            var _control = document.getElementById(_id);
            _control.innerText = (n + "  " + t);
        }

        function FormatDateTime(completeDate, _id) {
            var abbrMonths = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var _year = completeDate.substring(0, 4);
            var _month = completeDate.substring(5, 7);
            _month = _month - 1;
            var _day = completeDate.substring(8, 10);
            var _hour = completeDate.substring(11, 13);
            var _minute = completeDate.substring(14, 16);
            var _second = completeDate.substring(17, 19);
            var d = new Date(_year, _month, _day, _hour, _minute, _second, 0);

            function zeroPadding(val) {
                return val.toString().length === 1 ? "0" + val : val;
            }

            var _control = document.getElementById(_id);
            _control.innerText = zeroPadding(d.getDate()) + "-" + (abbrMonths[d.getMonth()]) + "-" + d.getFullYear() + " " +
                    zeroPadding(d.getHours()) + ":" + zeroPadding(d.getMinutes()) + ":" + zeroPadding(d.getSeconds());
        }

        function TransformToHtmlText(xmlDoc, xsltDoc) {
            debugger;
            //FOR ie11 only
            var xslt = new ActiveXObject("Msxml2.XSLTemplate");
            var xslDoc = new ActiveXObject("Msxml2.FreeThreadedDOMDocument");
            var serializer = new XMLSerializer();
            strXSLT = serializer.serializeToString(xsltDoc);
            xslDoc.loadXML(strXSLT);
            xslt.stylesheet = xslDoc;
            var xslProc = xslt.createProcessor();
            xslProc.input = xmlDoc;
            xslProc.transform();
            return xslProc.output;            
        }

        function ShowHideClaimAgingDetails(claimId) {
            if ($('#Expand')[0].value == 'Show') {
                $.ajax({
                    type: "POST",
                    url: "ClaimStatusDetailForm.aspx/GetClaimAgingDetails",
                    data: '{ claimId: "' + claimId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        xml = GetXmlDoc(msg.d);
                        xsl = LoadXmlDoc("ClaimAgingDetails.xslt");

                        // code for IE
                        if (window.ActiveXObject) {
                            ex = xml.transformNode(xsl);
                            $('#Child').html(ex);
                        }
                        //IE 11 only
                        else if (!(window.ActiveXObject) && "ActiveXObject" in window) {
                            resultHTML = TransformToHtmlText(xml, xsl);
                            $('#Child').html(resultHTML);
                        }
                        // code for Mozilla, Firefox, Opera, etc.
                        else if (document.implementation && document.implementation.createDocument) {
                            xsltProcessor = new XSLTProcessor();
                            xsltProcessor.importStylesheet(xsl);
                            resultDocument = xsltProcessor.transformToFragment(xml, document);
                            $('#Child').html(resultDocument);
                        }

                        $('#Child').show();
                        $('#Expand')[0].value = 'Hide';
                    }
                });
            }
            else {
                $('#Child').hide();
                $('#Expand')[0].value = 'Show';
            }
        }
    </script>
</asp:Content>