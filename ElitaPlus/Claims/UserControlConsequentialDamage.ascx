<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlConsequentialDamage.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlConsequentialDamage" %>

<script type="text/javascript">
    $(document).on("click", "[src*=plus]", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../App_Themes/Default/Images/minus.png");
    });
    $(document).on("click", "[src*=minus]", function () {
        $(this).attr("src", "../App_Themes/Default/Images/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
<div>
    <table width="100%" class="dataGrid">
        <tr>
            <td class="bor" align="left">
            </td>
            <td class="bor" align="right">
                <asp:Label ID="lblCdRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<div style="overflow: auto; height: 200px;">
    <asp:GridView ID="GridViewConsequentialDamage" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
        SkinID="DetailPageGridView" AllowSorting="false" PageSize="50" DataKeyNames="case_conseq_damage_id">
        <Columns>
            <asp:TemplateField ItemStyle-Width="3%">
                <ItemTemplate>
                    <img alt="" style="cursor: pointer" src="../App_Themes/Default/Images/plus.png" />
                    <asp:Panel ID="pnlConseqDamageIssue" runat="server" Style="display: none">
                        <asp:GridView ID="GridViewConseqDamageIssue" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
                            SkinID="DetailPageGridView" AllowSorting="false" OnRowDataBound="GridViewConseqDamageIssue_RowDataBound"
                            OnRowCommand ="GridViewConseqDamageIssue_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Issue" ItemStyle-Width="29%">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandName="Select" ID="SelectActionButton" runat="server" CausesValidation="False"
                                            Text=""></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="created_date" HeaderText="CREATED_DATE" ItemStyle-Width="14.75%" />
                                <asp:BoundField DataField="processed_date" HeaderText="PROCESSED_DATE" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="processed_by" HeaderText="PROCESSED_BY" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="status_code" HeaderText="STATUS" ItemStyle-Width="16.25%" />
                            </Columns>
                            <PagerStyle />
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="conseq_damage" HeaderText="CONSEQ_DAMAGE" ItemStyle-Width="30%" />
            <asp:BoundField DataField="created_date" HeaderText="CREATED_DATE" ItemStyle-Width="15%" />
            <asp:BoundField DataField="requested_amount" HeaderText="REQUESTED_AMOUNT" ItemStyle-Width="15%" />
            <asp:BoundField DataField="approved_amount" HeaderText="APPROVED_AMOUNT" ItemStyle-Width="20%" />
            <asp:BoundField DataField="status" HeaderText="STATUS" ItemStyle-Width="17%" />
            <asp:BoundField DataField="case_conseq_damage_id" Visible="false" />
        </Columns>
        <PagerStyle />
    </asp:GridView>
</div>
