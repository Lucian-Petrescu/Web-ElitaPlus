<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ScheduleForm.aspx.vb" Theme="Default" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ScheduleForm"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
            <asp:Label runat="server" ID="moScheduleCodeLabel">Code</asp:Label> <br />
            </td>
            <td>
            <asp:Label runat="server" ID="moScheduleDescriptionLabel">Description</asp:Label> <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:TextBox ID="moScheduleCode" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:TextBox ID="moScheduleDescription" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
            </td>            
        </tr>
    </table>
 </asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
 <div id="Div1" runat="server" class="dataContainer">
            <div style="width: 100%">
                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"  
                AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="True" AutoGenerateColumns="False">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <HeaderStyle  />
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="moScheduleDetail_ID" runat="server" >
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="Day">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moDayOfWeekLabel" runat="server" >
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList id="moDayOfWeek" runat="server" visible="True"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="From_Time">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moFromTimeLabel" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="moFromTimeText" runat="server" Visible="True" Width="75"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="To_Time">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moToTimeLabel" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="moToTimeText" runat="server" Visible="True" Width="75" 
                                CommandName="EnterToTime" ></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="moSchedule_ID" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>" ></asp:ImageButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>" 
                                Text="Cancel" ></asp:LinkButton>
                             </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                            </HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                    runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" ></asp:ImageButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>" 
                                 Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                            </EditItemTemplate>
                        </asp:TemplateField>
                     </Columns>
                <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle/>
                </asp:GridView>   
                </div>
               <div class="btnZone">
                    <asp:Button ID="BtnNew_WRITE" runat="server" Text="Add_New" SkinID="PrimaryLeftButton"></asp:Button>
               </div>
 </div>
 <div class="btnZone">                       
    <asp:Button ID="btnBack" runat="server" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
    <asp:Button ID="btnApply_WRITE" runat="server" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>
    <asp:Button ID="btnUndo_WRITE" runat="server" Text="UNDO" SkinID="AlternateLeftButton"></asp:Button>
    <asp:Button ID="btnButtomNew_WRITE" runat="server" Text="New" SkinID="PrimaryRightButton"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" runat="server" Text="New_With_Copy" SkinID="AlternateRightButton"></asp:Button>
  </div>
  <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
  <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261" />
</asp:Content>