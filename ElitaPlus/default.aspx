<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp._default"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>default</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="./Navigation/scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body oncontextmenu="return false" onload="window.blur();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <input id="nextPageURL" type="hidden" name="nextPageURL" runat="server"/> 
            <input id="sameWindow" type="hidden" name="sameWindow" runat="server"/>
		</form>
		<script>
		    var winHandle = null;

		    var windowClose = window.close;
		    // Re-implement window.close
		    window.close = function() {
		        window.opener = '';
		        window.open("", "_self");
		        windowClose();
		    }

/* Handle on Load */
function HandleOnLoad(url, windowProperties, title)
{
	//winHandle = FindWindow(NULL,"StartPage");
	/*if (StartPage && !StartPage.closed)
	{
		StartPage.close();
	}*/
	if (winHandle && winHandle.closed) 
	{
		// do nothing
		window.close();
	}
	else 
	{
		var winleft, wintop;
		
		width= screen.width - 12;
		height = screen.height - 50;
		winleft = 0;
		wintop = 0;
		
		/*if(screen.width == "800" && screen.height == "600")
		{
			width = screen.width;
			height = screen.height;
		}
		else
		{
			width = "1000";
			height = "690";
			//winleft = (screen.width - 1020) / 2;
			//wintop = (screen.height - 730) / 2;
			winleft = (screen.width - 1000) / 2;
			wintop = (screen.height - 750) / 2;
		}*/
		
		windowProperties += ',left=' + winleft + ',top=' + wintop + ',width=' + width + ',height=' + height

//		winHandle = window.open(url, title, windowProperties);
//		var myName = prompt("Name");
		var myName = Math.random();
		var smyName = myName.toString();
		if (myName != 0)
			smyName = "j" + smyName.substring(2,smyName.length);

		if (document.getElementById("sameWindow").value == "True") 
		{
			winHandle = window.open(url,  window.parent.name, windowProperties);
		}
		else 
		{
			winHandle = window.open(url,  smyName, windowProperties);
		}
		
		//winHandle.focus();
	}
}

var windowProperties = "toolbar = no, location = no, maximize= yes, status = yes, resizable = yes, scrollbars = no"
var windowTitle = "ElitaPlusStartPage"

//debugger;

	HandleOnLoad(document.getElementById("nextPageURL").value,windowProperties,windowTitle);


		</script>
	</body>
</HTML>
