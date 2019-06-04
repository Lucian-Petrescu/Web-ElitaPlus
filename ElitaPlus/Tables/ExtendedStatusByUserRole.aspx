<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExtendedStatusByUserRole.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ExtendedStatusByUserRole" 
     MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" ClientIDMode="Static"%>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
 
    <asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
        <meta http-equiv="Cache-control" content="no-cache" />
           <style type="text/css">
        .table-container {
        position: relative;
        width: auto;
        height: 100%;

        overflow-y: scroll;
         }
      .headcol {
        float: left;
      }
      table.scroll tbody {
        height: 100px;
        overflow-y: auto;
      }
     
      tr{
        height: 30px;
               
      }
     
      .right {
        overflow: auto;
      }
      .bottom{
        overflow: auto;
      }
      .container { width:auto; height: 100%; overflow:visible; background-color:#333;white-space:nowrap; }
   
    .no-border {
        border-left: 0px !important;
    }      
    .btnZone input.altBtn[disabled] { visibility:visible !important; display:inline !important;}
    
    </style> 
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

    <div style="display:none" id="messageControllerHtml">
        <Elita:MessageController runat="server" ID="MessageController2" Text=" " />
    </div>
    
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.js"></script>

       
        <table width="100%" border="0" class="searchGrid" id="searchTable">
        <tbody>
            <tr>
                <td width="50%">
             
                    <asp:Label ID="lblVerticalValues" runat="server">VERTICAL_VALUES</asp:Label><br />
                    <asp:DropDownList ID="moVerticalValues" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                         <asp:ListItem Text="Company" Value="1"></asp:ListItem>
                         <asp:ListItem Text="Role" Value="2"></asp:ListItem>
                         <asp:ListItem Text="Extended Status" Value="3"></asp:ListItem>
                </asp:DropDownList>

        
                        
                </td>
                <td width="50%">

                     <asp:Label ID="lblHorizontalValues" runat="server">HORIZONTAL_VALUES</asp:Label><br />
                    <asp:DropDownList ID="moHorizontalValues" runat="server" SkinID="MediumDropDown" AutoPostBack="False">                       
                </asp:DropDownList>

               
                </td>
            </tr>
            <tr>
                <td colspan="2">

                    <asp:Label ID="spnZAxis" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="cboGrants" runat="server" SkinID="MediumDropDown" AutoPostBack="False">                       
                </asp:DropDownList>

              
                </td>
            </tr>
        </tbody>
    </table>
    
    </asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
    <div class="dataContainer">
        <h2 class="dataGridHeader"></h2>
        <div>
            <div id="roleCompanyExtStatusAssignent"></div>
        </div>
    </div>
    
    
    <div class="btnZone">
        <asp:Button runat="server" skinid="AlternateLeftButton" OnClientClick="return false"  id="btnGrantAll" Text="GRANT_ALL" autopostback="false" Width="150px" />

        <asp:Button runat="server" skinID="AlternateLeftButton" OnClientClick="return false"  id="btnRevokeAll" Text="REVOKE_ALL" Width="150px" />
        <asp:Button  runat="server" skinID="AlternateLeftButton" OnClientClick="return false"  id="btnSave" Text="Save" Width="150px" OnClick="btnSave_Click" />

    </div>

    <label id="results"></label>
       
    <script language="javascript" type="text/javascript">
        var xml, xsl;
        var xaxis, yaxis, zaxisName, zaxisVal;

        $('#btnGrantAll').click(function () {
            $("input:checkbox").prop("checked", true);
        });

        $('#btnRevokeAll').click(function () {
            $("input:checkbox").prop("checked", false);
        });

        $("#btnSave").click(function () {
            //$("#results").html($("form").serialize());
            var items = [];
            $("input[name='chkGrant']:checked").each(function () {
                items.push($(this).val());
            });
            //$("#results").html(items.join());
            var xAxisName = getNodeName($("#moHorizontalValues").val());
            var yAxisName = getNodeName($("#moVerticalValues").val());
            var zAxisName = getZAxisName($("#moHorizontalValues").val(), $("#moVerticalValues").val());
            $("#btnSave").val("Saving Grants...").prop("disabled", true);
            $("#btnGrantAll").prop("disabled", true);
            $("#btnRevokeAll").prop("disabled", true);
            $.ajax({
                method: 'POST',
                url: 'ExtendedStatusByUserRole.aspx/SaveGrants',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ 'xAxisName': '" + xAxisName + "', 'yAxisName': '" + yAxisName + "', 'zAxisName': '" + zAxisName + "', 'xyAxisValue': '" +
items.join() + "', 'zAxisValue': '" + $('#cboGrants').val() + "' }",

                success: function (data) {
                    
                    if ($("div[id$='moMessageController']").length == 0) {
                        //$(".breadCrum").after("<div id='moMessageController'>" + $("#messageControllerHtml").html() + "</div>");
                        $("#divMsgPanel").append("<div id='moMessageController'>" + $("#messageControllerHtml").html() + "</div>")
                    }
                    var img = $("div[id$='MessageBox'] p img:eq(0)").clone();

                    var selection = items.join()

                    var msg = data.d.Message;
                    
                    $("div[id$='MessageBox'] p").empty().append(img).append(msg);

                    if (data.d.Status == "MSG_RECORD_SAVED_OK") {
                        $("div[id$='MessageBox']").removeClass().addClass("successMsg");
                        $("img[id$='moIconImage']").attr('src', '<%=ResolveUrl("~/App_Themes/Default/Images/icon_success.png")%>');
                    }
                    else{
                        $("div[id$='MessageBox']").removeClass().addClass("errorMsg");
                        $("img[id$='moIconImage']").attr('src', '<%=ResolveUrl("~/App_Themes/Default/Images/icon_error.png")%>');
                    }
                    $("#btnSave").prop("disabled", false).val("Save");
                    $("#btnGrantAll").prop("disabled", false);
                    $("#btnRevokeAll").prop("disabled", false);
                    //window.location.reload();
                }
            })
        });

        $('#moVerticalValues').on("change", function () {
            loadHorizontalValues();
        });

        $('#moHorizontalValues').on("change", function () {
            loadZAxis();
        });

        $('#cboGrants').on("change", function () {
            xaxis = $("#moHorizontalValues").val();
            yaxis = $("#moVerticalValues").val();
            zaxisName = getZAxisName(xaxis, yaxis);
            zaxisVal = $('#cboGrants').val();
            displayResult();
        });

        function loadZAxis() {
            xaxis = $("#moHorizontalValues").val();
            yaxis = $("#moVerticalValues").val();
            zaxisName = getZAxisName(xaxis, yaxis);
            $('#spnZAxis').html(zaxisName);
            $.ajax({
                method: 'POST',
                url: 'ExtendedStatusByUserRole.aspx/GetZAxisList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ 'zAxisName': '" + zaxisName + "' }",
                success: function (data) {
                    $('#cboGrants').empty();
                    $.each(data.d, function (i, item) {
                        var element = $("<option></option>").attr("value", item.Id).text(item.Description);
                        $(element).attr('enabled', item.Enabled)
                        $('#cboGrants').append(element);
                    });
                    zaxisVal = $('#cboGrants').val();
                    displayResult();
                }
            })
        }

        function getZAxisName(xAxis, yAxis) {

            for (i = 1; i <= 3; i++) {
                if (i != xAxis && i != yAxis)
                    return getNodeName(i.toString());
            }
        }

        function loadHorizontalValues() {
            var select = $('#moVerticalValues');
            $('#moHorizontalValues')
                .find('option')
                .remove();
            for (i = 1; i <= 3; i++) {
                if ($(select)[0].value != i) {
                    $('#moHorizontalValues')
                        .append($("<option></option>")
                        .attr("value", i)
                        .text(getNodeName(i.toString())));
                }
            }
            loadZAxis();
        }

        function loadXmlDocJq() {
            $.ajax({
                method: 'POST',
                url: 'ExtendedStatusByUserRole.aspx/GetGrantData',
                contentType: "application/json; charset=utf-8",
                dataType: "xml",
                cache: false,
                success: function (data) {
                    xml = data;
                    bindResult();
                }
            })
        }

        function loadXmlDocNoCache(filename) {
            if (window.ActiveXObject) {
                xhttp = new ActiveXObject("Msxml2.XMLHTTP");
            }
            else {
                xhttp = new XMLHttpRequest();
            }
            xhttp.open("GET", filename, false);
            try { xhttp.responseType = "msxml-document" } catch (err) { } // Helping IE11
            xhttp.send("");
            return xhttp.responseXML;
        }


        function loadXMLDoc(filename) {
            if (window.ActiveXObject) {
                xhttp = new ActiveXObject("Msxml2.XMLHTTP");
            }
            else {
                xhttp = new XMLHttpRequest();
            }
            xhttp.open("GET", filename, false);
            try { xhttp.responseType = "msxml-document" } catch (err) { } // Helping IE11
            xhttp.send("");
            return xhttp.responseXML;
        }

        

        function getNodePath(name) {
            var path;
            switch (name) {
                case "1":
                    path = "/RoleCompanyStatus/Companies/Company";
                    break;
                case "2":
                    path = "/RoleCompanyStatus/Roles/Role";
                    break;
                case "3":
                    path = "/RoleCompanyStatus/ExtendedStatuses/ExtendedStatus";
                    break;
            }
            return path;
        }


        function getNodeName(name) {
            var path;
            switch (name) {
                case "1":
                    path = "Company";
                    break;
                case "2":
                    path = "Role";
                    break;
                case "3":
                    path = "ExtendedStatus";
                    break;
            }
            return path;
        }

        function displayResult() {
            //xml = loadXmlDocNoCache("Data.xml?d=" + (new Date()).valueOf());
            
            if (!xsl) {
                xsl = loadXMLDoc("CrossTab.xslt");
            }
            loadXmlDocJq();
        }

        function bindResult() {
            if (window.ActiveXObject || xhttp.responseType == "msxml-document") {
                xslString = xsl.documentElement.xml;
            }
            else if (document.implementation && document.implementation.createDocument) {
                xslString = xsl.documentElement.outerHTML;
            }

            xslString = xslString.replace(new RegExp("~xaxis~", 'g'), getNodePath(xaxis));
            xslString = xslString.replace(new RegExp("~yaxis~", 'g'), getNodePath(yaxis));
            xslString = xslString.replace(new RegExp("~xaxisname~", 'g'), getNodeName(xaxis) + 'Id');
            xslString = xslString.replace(new RegExp("~yaxisname~", 'g'), getNodeName(yaxis) + 'Id');
            xslString = xslString.replace(new RegExp("~zaxisname~", 'g'), zaxisName + 'Id');
            xslString = xslString.replace(new RegExp("~zaxisvalue~", 'g'), zaxisVal);

        
            xslDoc = $.parseXML(xslString);
       

            // code for IE 
            if (window.ActiveXObject || xhttp.responseType == "msxml-document") {
                ex = xml.transformNode(xslDoc);
            
                document.getElementById("roleCompanyExtStatusAssignent").innerHTML = ex;
            }
                // code for Chrome, Firefox, Opera, etc.
            else if (document.implementation && document.implementation.createDocument) {
                xsltProcessor = new XSLTProcessor();
                xsltProcessor.importStylesheet(xslDoc);
               
                resultDocument = xsltProcessor.transformToFragment(xml, document);
                $("#roleCompanyExtStatusAssignent").empty();
                document.getElementById("roleCompanyExtStatusAssignent").appendChild(resultDocument);

            }
            $("input:checkbox[data-checked='checked']").each(function () {
                $(this).prop('checked', true);
            })
            $("input:checkbox[data-enabled='N']").each(function () {
                $(this).prop('disabled', true);
            })
            if (zaxisName == "ExtendedStatus") {
                if ($('#cboGrants option:selected').attr('enabled') == "N") {
                    $("input:checkbox").each(function () {
                        $(this).prop('disabled', true);
                    })
                }
            }
        }

        loadHorizontalValues();

    </script>
   
      </asp:Content>
