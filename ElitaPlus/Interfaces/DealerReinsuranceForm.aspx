<%@ Register TagPrefix="uc1" TagName="DealerFileProcessedController" Src="DealerFileProcessedController_New.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerReinsuranceForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.DealerReinsuranceForm" 
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript"  type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"/>
	
    <script type="text/javascript">
       
        function SetDualDropDownsValue(ctlCodeDropDown, ctlDecDropDown, change_Dec_Or_Code)
        {
            var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
            var objDecDropDown = document.getElementById(ctlDecDropDown);   // "By Description" DropDown control 
				
            //Select Code or Dec drop down
            if (change_Dec_Or_Code=='C')
            {
                objCodeDropDown.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
            }
            else
            {
                objDecDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
      <uc1:DealerFileProcessedController id="moDealerController" runat="server">
    </uc1:DealerFileProcessedController>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
  </asp:Content>