<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MessageProgressForm_New.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MessageProgressForm_New" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>MessageProgressBarForm_New</title>
    <base target="_self" />
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <link href="../App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form2" method="post" runat="server">
    <div class="dataContainer">
        <table border="0" align="center" class="loader" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                   <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="sep">
                    <img src="images/loader.gif" width="128" height="15" alt="Loader" align="middle" />
                    <br /><strong class="padRight20"><asp:Label ID="lblCounter" runat="server" Height="11"></asp:Label></strong>
                    <%--<input name="" value="Cancel" type="button" class="altBtn" onclick="cancelBubble(this);" />--%>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript" language="javascript">

        var intTimer;

        function LoadWaitMsg() {
            try {
                window.clearInterval(intTimer);
                elapsedTime = 0;
                document.body.style.cursor = 'default';

                intTimer = window.setInterval('ShowWaitMsg()', 1000);
                document.body.style.cursor = 'wait';

            }
            catch (exception) { }

        }

        LoadWaitMsg();

        function ClearWaitMessage() {
            try {
                if (intTimer != undefined) {
                    window.clearInterval(intTimer);
                    elapsedTime = 0;
                    document.body.style.cursor = 'default';

                    var objDiv = document.all.item("divMessage");
                    document.body.removeChild(objDiv);
                }
            }
            catch (exception) { }

        }
        function cancelBubble(e) {
            var evt = e ? e : window.event;
            if (evt.stopPropagation) evt.stopPropagation();
            if (evt.cancelBubble != null) evt.cancelBubble = true;
        }


    </script>
</body>
</html>
