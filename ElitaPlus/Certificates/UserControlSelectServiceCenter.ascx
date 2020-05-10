<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlSelectServiceCenter.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlSelectServiceCenter" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="~/Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<script language="jscript" type="text/jscript">
    function SelectServiceCenter(theID)
    {
        var selectedServiceCenterId = '<%=hdnSelectedServiceCenterId.ClientID%>'; ;
        document.getElementById(selectedServiceCenterId).value = theID;
    }

    function openClose(theID)
    {
        if (document.getElementById(theID).style.display == "block")
        {
            document.getElementById(theID).style.display = "none";
            document.getElementById("tick_" + theID).innerHTML = "+";
        }
        else
        {
            document.getElementById(theID).style.display = "block";
            document.getElementById("tick_" + theID).innerHTML = "-";
        }
    }

    function showHidePage(newPageNumber, recordCount, pageSize)
    {
        var newPage = parseInt(newPageNumber);
        var currentPageId = '<%=CurrentPage.ClientID%>';
        var currentPage = parseInt(document.getElementById(currentPageId).value);
        var tempLocation;
        if (newPageNumber == document.getElementById(currentPageId).value) { return; }
        document.getElementById('pg1_' + document.getElementById(currentPageId).value).style.cursor = 'pointer';
        document.getElementById('pg1_' + document.getElementById(currentPageId).value).setAttribute('class', '');
        document.getElementById('pg2_' + document.getElementById(currentPageId).value).style.cursor = 'pointer';
        document.getElementById('pg2_' + document.getElementById(currentPageId).value).setAttribute('class', '');
        document.getElementById(currentPageId).value = newPageNumber;
        document.getElementById('pg1_' + document.getElementById(currentPageId).value).style.cursor = 'text';
        document.getElementById('pg1_' + document.getElementById(currentPageId).value).setAttribute('class', 'selected_page');
        document.getElementById('pg2_' + document.getElementById(currentPageId).value).style.cursor = 'text';
        document.getElementById('pg2_' + document.getElementById(currentPageId).value).setAttribute('class', 'selected_page');
        for (i = 1; i <= pageSize; i++)
        {
            tempLocation = (((currentPage - 1) * pageSize) + i);
            if (tempLocation < recordCount)
            {
                document.getElementById('tr_' + tempLocation).style.display = "none";
                document.getElementById('trd_' + tempLocation).style.display = "none";
            }
            tempLocation = (((newPage - 1) * pageSize) + i);
            if (tempLocation < recordCount)
            {
                document.getElementById('tr_' + tempLocation).style.display = "block";
                document.getElementById('trd_' + tempLocation).style.display = "block";
            }
        }
        SelectServiceCenter('XXXX');
    }

    if (document.getElementById('pg1_1') != null)
    {
        document.getElementById('pg1_1').style.cursor = 'text';
        document.getElementById('pg1_1').setAttribute('class', 'selected_page');
    }

    if (document.getElementById('pg2_1') != null)
    {
        document.getElementById('pg2_1').style.cursor = 'text';
        document.getElementById('pg2_1').setAttribute('class', 'selected_page');
    }
</script>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<Elita:MessageController runat="server" ID="MessageController" Visible="false" />
<div class="dataContainer">
    <div class="stepformZone">
        <table class="formGrid" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td align="left" nowrap="nowrap">
                        <asp:Label runat="server" ID="moSearchByLabel" Text="SEARCH_BY" />
                        :
                    </td>
                    <td align="left" nowrap="nowrap" colspan="3">
                        <asp:RadioButton ID="RadioButtonByZip" runat="server" AutoPostBack="True" Text="BY_ZIP"
                            GroupName="SEARCH_TYPE"></asp:RadioButton>
                        <asp:RadioButton ID="RadioButtonByCity" runat="server" AutoPostBack="True" Text="BY_CITY"
                            GroupName="SEARCH_TYPE"></asp:RadioButton>
                        <asp:RadioButton ID="RadioButtonAll" runat="server" AutoPostBack="True" Text="ALL"
                            GroupName="SEARCH_TYPE"></asp:RadioButton>
                        <asp:RadioButton ID="RadioButtonNO_SVC_OPTION" runat="server" AutoPostBack="True" Text="NO_SVC_OPTION" GroupName="SEARCH_TYPE"></asp:RadioButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" nowrap="nowrap">
                        <asp:Label runat="server" ID="moCountryLabel" Text="COUNTRY" />
                        :
                    </td>
                    <td style="white-space: nowrap" >
                        <asp:DropDownList ID="moCountryDrop" runat="server" CssClass="small" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td style="white-space:nowrap" runat="server" id="tdCityLabel">
                        <asp:Label runat="server" ID="moCityLabel" Text="CITY" />
                        :
                    </td>
                    <td style="white-space:nowrap" runat="server" id="tdCityTextBox">
                        <asp:TextBox ID="TextboxCity" runat="server" CssClass="medium"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="formGrid" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td runat="server" id="tdServiceCenterLabel" colspan="3">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="moMultipleColumnDrop" />
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="formGrid" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td runat="server" id="tdClearButton">
                        <asp:Button ID="btnClearSearch" runat="server" CssClass="altBtn" Text="Clear">
                        </asp:Button>
                    </td>
                    <td runat="server" id="tdSearchButton">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchBtn"></asp:Button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
<div class="dataContainer" runat="server" id="DataGridPlaceHolder">
    <asp:Xml ID="xmlSource" runat="server"></asp:Xml>
</div>
<div class="btnZone" style="margin-top: 2px">
    <div>
        <asp:Button ID="btnSelectServiceCenter" TabIndex="5" runat="server" CssClass="primaryBtn floatR"
            Text="Select" />
    </div>
</div>
<asp:HiddenField ID="hdnSelectedServiceCenterId" runat="server" Value="XXXX" />
<asp:HiddenField ID="CurrentPage" runat="server" Value="1" />
