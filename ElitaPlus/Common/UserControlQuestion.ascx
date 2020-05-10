<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlQuestion.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlQuestion" EnableTheming="true" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<div style="width: 100%">
    <div style="width: 100%">
        <h2 class="dataGridHeader" runat="server" id="QuestionSet">
            <asp:Label runat="server" ID="lblQuestions" Text="QUESTION_SET" /></h2>

        <div id="question" style="width: 99.53%; height: 100%">
            <table id="tblQuestionDetail" style="width: 100%; height: 100%" class="dataGrid">
                <tr>
                    <td>
                        <asp:GridView ID="GridQuestions" runat="server" CssClass="formGrid" Width="100%" ShowHeader="false" GridLines="None" BorderStyle="None"
                            BorderWidth="0" BorderColor="Transparent" AutoGenerateColumns="False" AllowPaging="false"  CssClass="dataGrid"
                            AllowSorting="False" EnableModelValidation="True">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Question">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Text")%>'></asp:Label>&nbsp;<asp:Label ID="lblRequired" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAnswerType" runat="server" Text='<%#Eval("AnswerType")%>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code")%>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Answer">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNo" runat="server" Visible="False" Width="200px" OnTextChanged="txtNo_OnTextChanged"></asp:TextBox>
                                        <asp:TextBox ID="txtDate" runat="server" Visible="False" Width="200px" OnTextChanged="txtDate_OnTextChanged"></asp:TextBox>
                                        <asp:ImageButton ID="btnDate" TabIndex="2" runat="server" Visible="False"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                        <asp:DropDownList ID="ddlList" runat="server" Width="200px" Visible="False" OnSelectedIndexChanged="ddlList_OnSelectedIndexChanged"></asp:DropDownList>
                                        <asp:RadioButtonList ID="rblChoice" runat="server" Width="200px" Visible="False" CssClass="formGrid" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblChoice_OnSelectedIndexChanged"></asp:RadioButtonList>
                                        <asp:CheckBox ID="chkCheck" runat="server" Visible="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ImageButton ID="btnDate2" TabIndex="2" runat="server" Visible="False"
                            ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="ImageButton1" TabIndex="2" runat="server" Visible="False"
                ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
        </div>
    </div>
</div>
