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
                  <asp:label ID="lblnotify" runat="server"></asp:label>   
                   </td>
               </tr>
               <tr>
               <td>
                   <asp:label ID="lblNote" runat="server">ATTENTION_NOTE</asp:label>
               </td>
           </tr>
           <tr>
               <td>
                   <asp:Button ID="cachebtn" runat="server" Text="CLEANUPCACHEFORM"/>
                   </td>
           </tr>
       </table>
    </div>
</asp:Content>
