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
           <tr runat="server">
               <td runat="server">
                   <asp:TextBox ID="txtNote" TextMode="MultiLine" SkinID="MediumTextBox" runat="server" Text="Attention Note: This Activity is Must be done by Super Users Only because it impacts system performance" Height="57px" Width="247px"></asp:TextBox>
               </td>
           </tr>
           <tr runat="server">
               <td runat="server">
                   <asp:Button ID="cachebtn" runat="server" Text="CLEANUPCACHEFORM"/>
                   </td>
           </tr>
       </table>
    </div>
</asp:Content>
