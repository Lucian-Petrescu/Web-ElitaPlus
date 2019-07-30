<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CleanupCacheForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CleanupCacheForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True" Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    </asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div >
       <table>
           <tr>
              <td>
                  <asp:label ID="lblnotify" runat="server" SkinID="SummaryLabel"></asp:label>   
                   </td>
               </tr>
               <tr>
               <td>
                  
               </td>
           </tr>
           <tr>
               <td><div class="btnZone">
                   <asp:Button ID="btncache" runat="server" Text="CLEANUPCACHEFORM" SkinID="AlternateLeftButton"/>
                   </div>
                   </td>
           </tr>
       </table>
    </div>
</asp:Content>
