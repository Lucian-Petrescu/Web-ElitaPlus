<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/ElitaBase.Master" Theme="Default"
    CodeBehind="ClaimHistoryForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimHistoryForm" EnableSessionState="True"%>

 <%@ Register TagPrefix="Elita" Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl" %>

  <asp:Content ID="Content4" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
  </asp:Content>
  <asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
  </asp:Content>   

 <asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
            <tr>
                <td>
                    <asp:Label ID="LabelClaimNumber" runat="server">Claim_Number</asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelCreatedDate" runat="server" Visible="False">Date_Added</asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelCreatedBy" runat="server"  Visible="False">Added_By</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBoxSearchClaimNumber" runat="server" ReadOnly="True" AutoPostBack="False" width="240px" 
                        SkinID="MediumTextBox" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextboxModifiedDate" runat="server" ReadOnly="True" tabIndex="1" Visible="False" 
                        width="240px" Enabled="False"></asp:TextBox>
                </td>
                <td width="100%">
                    <asp:TextBox ID="TextboxModifiedBy" runat="server" ReadOnly="True" 
                        tabIndex="1" Visible="False" width="240px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
        </table> 
     </asp:Content>           
     <asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder" runat="server">         
         <asp:Panel ID="PanelHistoryDetails" runat="server" BorderStyle="None" EnableViewState="False" HorizontalAlign="Left">
          <div class="dataContainer">                                                            
            <asp:ImageButton ID="ImgCloseButton" runat="server" ImageAlign="Right" ImageUrl="../Navigation/images/icons/closeIcon.gif" style="width: 15px" Visible="False" />

        <table id="Table5"  width="70%"  border="0" align="center" class="formGrid">
         
            <tr>
                <td width="25%"></td>
                <td width="20%" style="font-weight: bold; text-align: center;">
                     <asp:Label ID="LabelOld" runat="server">OLD</asp:Label>
                </td>
                <td width="20%" style="font-weight: bold; text-align: center;">
                    <asp:Label ID="LabelNew" runat="server">NEW</asp:Label>
                </td>
                <td width="35%"></td>
            </tr>
            <tr align="right">
                <td align="right" style="text-align: right; " valign="top">
                        <asp:Label ID="LabelStatusCode" runat="server">Claim_Status</asp:Label>
                    </td>
                <td align="left" valign="middle">
                    <asp:TextBox ID="TextboxClaimStatusCodeOld" runat="server" SkinID="SmallTextBox" Enabled="False" Visible="True"></asp:TextBox>
                </td>
            
                <td align="left">
                    <asp:TextBox ID="TextboxClaimStatusCodeNew" runat="server" SkinID="SmallTextBox" Enabled="False" Visible="True"></asp:TextBox>
                </td>
                <td width="25%"></td>
            </tr>
            <tr>
                <td align="right" style="text-align: right; " valign="middle">
                    <asp:Label ID="LabelAuthorizedAmount" runat="server">Authorized_Amount</asp:Label>
                    </td>
                <td align="left" valign="middle">
                    <asp:TextBox ID="TextboxAuthorizedAmountOld" runat="server"
                        SkinID="SmallTextBox" Enabled="False" tabIndex="2" ></asp:TextBox>
                </td>
           
                <td align="left" >
                    <asp:TextBox ID="TextboxAuthorizedAmountNew" runat="server" AutoPostBack="True" 
                        SkinID="SmallTextBox" Enabled="False" tabIndex="2"></asp:TextBox>
                </td>
                <td width="25%"></td>
            </tr>
            <tr>
                <td align="right" style="text-align: right;" valign="middle">
                    <asp:Label ID="LabelClaimClosedDate" runat="server">Date_Claim_Closed</asp:Label>
                     </td>
                <td align="left" valign="middle">
                    <asp:TextBox ID="TextboxClaimClosedDateOld" runat="server" AutoCompleteType="Disabled"  SkinID="SmallTextBox" Enabled="False" tabIndex="-1" ></asp:TextBox>
                </td>
                <td align="left" >
                    <asp:TextBox ID="TextboxClaimClosedDateNew" runat="server" AutoCompleteType="Disabled" SkinID="SmallTextBox" Enabled="False" tabIndex="-1" ></asp:TextBox>
                </td>
                <td width="25%"></td>
            </tr>
            <tr>
                <td style="text-align: right; ">
                    <asp:Label ID="LabelRepairDate" runat="server">Repair_Date</asp:Label>
                     </td>
                <td align="left" valign="middle">
                    <asp:TextBox ID="TextboxRepairDateOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="-1"></asp:TextBox>
                </td>
               
                <td align="left">
                    <asp:TextBox ID="TextboxRepairDateNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="2" ></asp:TextBox>
                </td>
                <td width="25%"></td>
            </tr>
            <tr>
                <td align="right" valign="middle">
                    <asp:Label ID="LabelLiabilityLimit" runat="server">Liability_Limit</asp:Label>
                      </td>
                <td align="left">
                    <asp:TextBox ID="TextboxLiabilityLimitOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
             
                <td align="left">
                    <asp:TextBox ID="TextboxLiabilityLimitNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
                <td width="25%"></td>
            </tr>
            <tr >
                <td align="right" style="text-align: right; " valign="middle">
                    <asp:Label ID="LabelCertID" runat="server">Coverage_Type</asp:Label>
                    </td>
                <td align="left">
                    <asp:TextBox ID="TextboxCertItemCoverageIDOld" runat="server" 
                        SkinID="SmallTextBox" Enabled="False" tabIndex="-1"></asp:TextBox>
                </td>
            
                <td align="left" style="text-align: left;">
                    <asp:TextBox ID="TextboxCertItemCoverageIDNew" runat="server" SkinID="SmallTextBox" Enabled="False" ></asp:TextBox>
                </td>
                <td width="25%"></td>
            </tr>
            <tr>
                <td align="right" style="text-align: right; " valign="middle">
                     <asp:Label ID="LabelDeductible" runat="server">Deductible</asp:Label>
                    </td>
                <td align="left">
                    <asp:TextBox ID="TextboxDeductibleOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
          
                <td  align="left">
                    <asp:TextBox ID="TextboxDeductibleNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
                <td width="25%"></td>
                </tr>
                  <tr>
                <td align="right" style="text-align: right; " valign="middle">
                     <asp:Label ID="LabelServiceCenter" runat="server">Service_Center</asp:Label>
                    </td>
                <td align="left">
                    <asp:TextBox ID="TextboxServiceCenterOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
          
                <td style="text-align: left;">
                    <asp:TextBox ID="TextboxServiceCenterNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
                <td width="25%"></td>
                </tr>
                <tr>
                <td align="right" style="text-align: right; " valign="middle">
                     <asp:Label ID="LabelBatchNumber" runat="server">BATCH_NUMBER</asp:Label>
                    </td>
                <td align="left">
                    <asp:TextBox ID="TextboxBatchNumberOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
          
                <td style="text-align: left;">
                    <asp:TextBox ID="TextboxBatchNumberNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
                <td width="25%"></td>
                </tr>
                <tr>
                <td align="right" style="text-align: right; " valign="middle">
                     <asp:Label ID="LabelLawsuit" runat="server">Lawsuit</asp:Label>
                    </td>
                <td align="left">
                    <asp:TextBox ID="TextboxLawsuitOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TextboxLawsuitNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="1"></asp:TextBox>
                </td>
                <td width="25%"></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="LabelClaimModifyDateNew" runat="server">DATE_LAST_MAINTAINED</asp:Label>
                         </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="TextboxClaimModifyDateOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="-1" ></asp:TextBox>
                    </td>
              
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextboxClaimModifyDateNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="-1" ></asp:TextBox>
                    </td>
                    <td width="25%"></td>
                </tr>
                <tr>
                    <td align="right" style="text-align: right; " valign="middle">
                        <asp:Label ID="LabelClaimModifyByNew" runat="server">OPERATOR_LAST_MAINTAINED</asp:Label>
                        </td>
                    <td align="left" valign="middle">
                        <asp:TextBox ID="TextboxClaimModifyByOld" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="-1" ></asp:TextBox>
                    </td>
                     
                    <td align="left">
                        <asp:TextBox ID="TextboxClaimModifyByNew" runat="server" SkinID="SmallTextBox" Enabled="False" tabIndex="-1" ></asp:TextBox>
                    </td>
                    <td width="25%"></td>
                </tr>
        </table>
        </div>
        </asp:Panel>

        <asp:Panel ID="pnlPageSize" runat="server" width="100%" BorderStyle="None" EnableViewState="False" HorizontalAlign="Left">
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
                    </td>
                    <td align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
       </div> 
       </asp:Panel>

     <div class="dataContainer">
     <asp:DataGrid ID="Grid" runat="server" AllowPaging="True"  PagerStyle-HorizontalAlign="Center"
            AutoGenerateColumns="False" OnItemCommand="ItemCommand" 
            SkinID="DetailPageDataGrid" Width="100%">
             <Columns>
               
                <asp:BoundColumn DataField="CLAIM_NUMBER" HeaderText="Claim_Number" 
                    Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="2%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>

                <asp:TemplateColumn HeaderText="Date_Added" >
                    <ItemTemplate >
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="SelectAction" />
                    </ItemTemplate>
                </asp:TemplateColumn> 
                 <asp:BoundColumn DataField="CLAIM_MODIFIED_BY_NEW" HeaderText="Added_By">
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_CODE_OLD" HeaderText="Old_Claim_Status">
                    <HeaderStyle  HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_CODE_NEW" HeaderText="New_Claim_Status">
                    <HeaderStyle  HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AUTHORIZED_AMOUNT_OLD" 
                    HeaderText="Old_Authorized_Amt">
                    <HeaderStyle  HorizontalAlign="Center" Width="20%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AUTHORIZED_AMOUNT_NEW" 
                    HeaderText="New_Authorized_Amt">
                    <HeaderStyle  HorizontalAlign="Center" Width="20%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_CLOSED_DATE_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_CLOSED_DATE_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REPAIR_DATE_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REPAIR_DATE_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MODIFIED_DATE" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MODIFIED_BY" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LIABILITY_LIMIT_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LIABILITY_LIMIT_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CERT_ITEM_COVERAGE_ID_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CERT_ITEM_COVERAGE_ID_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_DATE_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_DATE_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_BY_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_BY_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTIBLE_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTIBLE_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIPTION_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIPTION_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SERVICE_CENTER_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SERVICE_CENTER_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BATCH_NUMBER_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BATCH_NUMBER_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IS_LAWSUIT_ID_NEW" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
               <asp:BoundColumn DataField="IS_LAWSUIT_ID_OLD" Visible="False">
                    <HeaderStyle  HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
            </Columns>
            <PagerStyle PageButtonCount="5" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
        </asp:DataGrid>
        </div> 
        <asp:TextBox ID="FlagPanel" runat="server" Visible="False" Width="2px">n</asp:TextBox>

        <asp:TextBox ID="mLabelStatusCode" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1" Visible="False" 
            width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                          
        <asp:TextBox ID="mLabelAuthorizedAmount" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1" 
            Visible="False"   width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                        
        <asp:TextBox ID="mLabelClaimClosedDate" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                        
        <asp:TextBox ID="mLabelRepairDate" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False" width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                          
        <asp:TextBox ID="mLabelLiabilityLimit" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                        
        <asp:TextBox ID="mLabelCertID" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
          
        <asp:TextBox ID="mLabelDeductible" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>

        <asp:TextBox ID="mLabelServiceCenter" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                       
        <asp:TextBox ID="mLabelBatchNumber" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>

        <asp:TextBox ID="mLabelLawsuit" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>

        <asp:TextBox ID="mLabelClaimModifyDateNew" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"   width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                        
        <asp:TextBox ID="mLabelClaimModifyByNew" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1" 
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
            
        <asp:TextBox ID="mLabelNew" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
                      
        <asp:TextBox ID="mLabelOld" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>
       
       <asp:TextBox ID="mLabelRecordCount" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>

       <asp:TextBox ID="mLabelPageSize" runat="server"  
            SkinID="MediumTextBox" ReadOnly="True" tabIndex="-1"  
            Visible="False"  width="1px" style="POSITION: absolute; left: -25px;"></asp:TextBox>

 <script type ="text/jscript" language="JavaScript">
 function resizePanel() 
 {
         try{
             document.getElementById("ctl00_ContentPanelMainContentBody_PanelHistoryDetails").style.height = document.getElementById("Table5").offsetHeight;
             document.getElementById("LineImagebutton").style.width = document.getElementById("Table5").offsetWidth;
             var h = parent.document.getElementById("Navigation_Content").clientHeight; //find the height of the iFrame client area
             document.getElementById('moTableOuter').height = h - 60;
             document.getElementById('tblMain').height = h - 65;
             document.getElementById("tableBtnBack").align = left;
           }
  
         catch(err){
             }
 }
 
 //resizePanel();
  </script> 

 <div class="btnZone">
  <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
  </div> 
</asp:Content>
    
  