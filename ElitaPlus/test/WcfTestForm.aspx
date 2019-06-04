<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WcfTestForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.WcfTestForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    			<asp:TextBox id="userTextBox" style="Z-INDEX: 102; LEFT: 424px; POSITION: absolute; TOP: 48px"
				runat="server"></asp:TextBox>
			
    <asp:Label id="userLabel" style="Z-INDEX: 103; LEFT: 288px; POSITION: absolute; TOP: 48px"
				runat="server">networkId</asp:Label>
			<asp:Label id="passwordLabel" style="Z-INDEX: 104; LEFT: 296px; POSITION: absolute; TOP: 80px"
				runat="server">password</asp:Label>
			<asp:TextBox id="passwordTextBox" style="Z-INDEX: 105; LEFT: 424px; POSITION: absolute; TOP: 88px"
				runat="server" TextMode="Password"></asp:TextBox>
			<asp:Button id="LoginButton" style="Z-INDEX: 106; LEFT: 448px; POSITION: absolute; TOP: 128px"
				runat="server" Text="Login"></asp:Button>
    <asp:Button ID="helloBtn" 
        style="Z-INDEX: 108; LEFT: 526px; POSITION: absolute; TOP: 129px" 
        runat="server" Text="Hello" />
		
    <asp:Label ID="statusLabel"  style="Z-INDEX: 107; LEFT: 280px; POSITION: absolute; TOP: 280px" runat="server"></asp:Label>
    <p>
        <asp:Button ID="ProcessReqBtn" runat="server" Text="Process  Req" />
    </p>
    </form>
</body>
</html>
