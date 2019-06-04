<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ajaxTest.aspx.vb" MasterPageFile="~/Navigation/masters/content_default.Master"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ajaxTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="uc1" TagName="MakeModel" Src="../Common/MakeModel.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" ID="cntMain" runat="server">
    <div >
        Make page SLOWER!!!!!  <asp:Button runat=server ID="btnAdd200" Text="Add 200 Items" />&nbsp;<asp:Button runat=server ID="btnAdd1000" Text="Add 1000 Items" />&nbsp;<asp:Button runat=server ID="btnAdd10000" Text="Add 10000 Items" />&nbsp;<asp:dropdownlist runat=server ID="ddlDummy" AutoPostBack=false></asp:dropdownlist>
    </div>
    <div>
        Update Panels:
        <uc1:MakeModel ID="MakeModelCtrl" runat="server"></uc1:MakeModel>
        <br />
        <br />
        <br />
    </div>
    <div>
        Call Backs
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr valign="middle">
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="MakeLabel" runat="server">MAKE</asp:Label>:</td>
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="ModelLabel" runat="server">MODEL</asp:Label>:</td>
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="TrimLabel" runat="server">ENGINE_VERSION</asp:Label>:</td>
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="YearLabel" runat="server">YEAR</asp:Label>:</td>
            </tr>
            <tr>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="MakeDrop" runat="server" Width="93%" 
                         TabIndex="1" >
                    </asp:DropDownList></td>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="ModelDrop" runat="server" Width="93%" 
                         TabIndex="2">
                    </asp:DropDownList></td>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="TrimDrop" runat="server" Width="93%" 
                       TabIndex="3">
                    </asp:DropDownList></td>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="YearDrop" runat="server" Width="93%"
                         TabIndex="4">
                    </asp:DropDownList></td>
            </tr>
        </table>
<cc1:CascadingDropDown
ID="CascadingDropDown1"
runat="server"
TargetControlID="MakeDrop"
Category="MAKE"
ServiceMethod="FillMakes" />
<cc1:CascadingDropDown
ID="CascadingDropDown2"
runat="server"
TargetControlID="ModelDrop"
ParentControlID="MakeDrop"
ServiceMethod="GetModels" 
Category="MODEL" />
<cc1:CascadingDropDown
ID="CascadingDropDown3"
runat="server"
TargetControlID="TrimDrop"
ParentControlID="ModelDrop"
ServiceMethod="GetTrims"
Category="TRIM" />
<cc1:CascadingDropDown
ID="CascadingDropDown4"
runat="server"
TargetControlID="YearDrop"
ParentControlID="TrimDrop"
ServiceMethod="GetYears"
Category="YEAR"/>
        <br />
        <br />
        <br />

    </div>
    <div>
            Autocomplete Test:  
                <asp:TextBox ID="txtDealers" runat=server Width="250"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" OnClientItemSelected="comboSelected"  runat="server"  TargetControlID="txtDealers" ServiceMethod="GetCompletionList" MinimumPrefixLength=2>
                </cc1:AutoCompleteExtender>
    <script language=javascript type="text/javascript">
        function comboSelected( source, eventArgs )
            {
               //put the selected value in a hidden textbox - runat server so you can read it on postback
                alert( " Key : "+ eventArgs.get_text() +"  Value :  "+eventArgs.get_value());
            }
    </script>
    </div>
    
    
</asp:Content>
