<%@ Page Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Title="Add New Dictionary Labels" Theme="Default" CodeBehind="ADD_New_Dictionary_Labels.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.ADD_New_Dictionary_Labels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <style>
        .TABLEMAIN_MASTER TD.VAlignMiddleTD
        {
            vertical-align: middle;
        }
    </style>
    <table cellspacing="3" cellpadding="2" width="100%" border="0">
        <input id="HiddenSaveChangesPromptResponse" style="width: 116px; height: 10px" type="hidden"
            size="14" name="HiddenSaveChangesPromptResponse" runat="server">
        <tr>
            <td>
                <div class="dataContainer" style="margin-left: 0%">
                    <div class="Page" runat="server" id="grdLabels_WRITE1" style="height: 420px; overflow: auto;
                        scrollbar-face-color: #BDBDBD; width: 98%;">
                        <asp:DataGrid ID="grdLabels" runat="server" Width="100%" AutoGenerateColumns="False"
                            BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                            CellPadding="1" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated"
                            OnItemCommand="ItemCommand" PageSize="999">
                            <SelectedItemStyle Wrap="False" BackColor="LightSteelBlue" />
                            <EditItemStyle Wrap="False"></EditItemStyle>
                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1" />
                            <ItemStyle Wrap="True" BackColor="White" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Small" ForeColor="#12135B">
                            </HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                            runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("new_dict_item_id"))%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="UI_PROG_CODE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moUiProgCode" runat="server" Width="250px" Text='<%# Container.DataItem("ui_prog_code")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TRANSLATION">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moTranslation" runat="server" Width="350px" Text='<%# Container.DataItem("english_translation")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MSG_CODE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMSGCODE" runat="server" Width="70px" MaxLength="5" Text='<%# Container.DataItem("MSG_CODE")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MSG_TYPE">
                                    <ItemStyle CssClass="VAlignMiddleTD" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlMSGType" runat="server" Visible="True" Width="125px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MSG_PARAMETER_COUNT">
                                    <ItemStyle CssClass="VAlignMiddleTD" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlMSGParamCnt" runat="server" Width="80px" Visible="True">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CREATED_DATE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCreatedDate" runat="server" Width="130px" Text='<%# Container.DataItem("created_date")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MODIFIED_DATE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moModifiedDate" runat="server" Width="130px" Text='<%# Container.DataItem("modified_date")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CREATED_BY">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCreatedBy" runat="server" Width="80px" Text='<%# Container.DataItem("created_by")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MODIFIED_BY">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moModifiedBy" runat="server" Width="80px" Text='<%# Container.DataItem("modified_by")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false" HeaderText="IMPORTED">
                                    <ItemTemplate>
                                        <asp:TextBox ID="moImported" runat="server" Width="2%" Text='<%# Container.DataItem("imported")%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="moDictItemId" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("dict_item_id"))%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </div>
            </td>
        </tr>
        <asp:Panel ID="pnlLabelsAdded" runat="server" Visible="False">
            <tr>
                <td>
                    <table width="95%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label3" runat="server" Width="191px" Height="10px" Visible="true">Labels Added:</asp:Label>
                                <asp:TextBox ID="txtMessageLabelsAdded" runat="server" Width="58px" Visible="true"
                                    ToolTip="Number of rows affected by the button action." ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlLabelCount" runat="server" Visible="False">
            <tr>
                <td>
                    <table width="95%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label2" runat="server" Width="191px" Height="10px" Visible="true">New Label Count:</asp:Label>
                                <asp:TextBox ID="txtMessageCount" runat="server" Width="58px" Visible="true" ToolTip="Number of rows in the New Labels source table."
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
          <%--  <tr>
                <td align="left">
                    <asp:Button ID="btnAppend" Style="background-image: url(../Navigation/images/icons/load_icon.gif);
                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                        Width="230px" Text="Import" Height="22px" SkinID="AlternateLeftButton"></asp:Button>&nbsp;
                    <asp:Button ID="btnWipeout" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                        Width="190px" Text="CLEAR_NEW_LABELS_TABLE" Height="22px" SkinID="AlternateLeftButton">
                    </asp:Button>
                </td>
            </tr>--%>
    </table>
    <div class="btnZone">
        <asp:Panel ID="Panel1" runat="server" Visible="True">
            <asp:Button ID="btnAppend" Style="background-image: url(../Navigation/images/icons/load_icon.gif);
                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                Width="230px" Text="Import"  SkinID="AlternateLeftButton"></asp:Button>&nbsp;
            <asp:Button ID="btnWipeout" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                Width="190px" Text="CLEAR_NEW_LABELS_TABLE" SkinID="AlternateLeftButton"></asp:Button>
        </asp:Panel>&nbsp;
        <asp:Panel ID="moButtonPanel" runat="server" Visible="True">
            <span style="font-size: 7pt; font-family: Verdana"></span>
            <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                Width="90px" Text="Return" SkinID="AlternateLeftButton"></asp:Button>&nbsp;
            <asp:Button ID="moBtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                Width="100px" Text="Save" SkinID="AlternateLeftButton" TabIndex="4"></asp:Button><span
                    style="font-size: 7pt; font-family: Verdana">&nbsp; </span>
            <asp:Button ID="moBtnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                Width="100px" Text="Undo" SkinID="AlternateLeftButton" TabIndex="5"></asp:Button><span
                    style="font-size: 7pt; font-family: Verdana">&nbsp;</span>
            <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                Width="81px" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;&nbsp;
        </asp:Panel>
    </div>
    <script type="text/javascript">
        var aFiles
        function AddNewRow() {
            // create new file listing
            for (var i = 0; i < aFiles.length; i++);

            {

                // For new Row have the below code    
                var tr = document.createElement('TR');
                for (var j = 0; j < fileValues.length; j++) {

                    // Create the new TD
                    td = document.createElement('TD');
                    td.innerText = (fileValues[j]);
                    // Now append the Td to the row
                    tr.appendChild(td);

                }

                // Now append the newly created row to the Datagrid
                oFileBox[0].appendChild(tr);
            }
        }  
    </script>
</asp:Content>
