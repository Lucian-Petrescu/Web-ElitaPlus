<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="navigation_menu.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.navigation_menu" %>
<link type="text/css" href="styles/Navigation.css" rel="stylesheet" id="NavigationStyle" />
<%--<link href="styles/Navigation.css" rel="stylesheet" type="text/css" title="NavigationStyle" />--%>
<script language="JavaScript" src="../Navigation/scripts/GlobalHeader.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function Logout() {
        document.all["txtTabID"].value = "LOGOUT"
        Form1.submit()

    }

    function Login() {
        document.all["txtTabID"].value = "LOGIN"
        Form1.submit()

    }

</script>
<input id="txtTabID" type="hidden" name="txtTabID" runat="server" />
<input id="displayMode" type="hidden" name="displayMode" runat="server" />
<div id="GlobalNavHeader">
    <span class="ElitaNavDiv">
        <asp:Menu ID="mnu"  runat="server" RenderingMode="Table" ItemWrap="false" Orientation="Horizontal" 
            DisappearAfter="750" SkipLinkText="" StaticPopOutImageUrl="images/trans_spacer.gif"
            DynamicHorizontalOffset="10" CssClass="MenuLink">
            <DataBindings>
                <asp:MenuItemBinding Selectable="True" DataMember="TAB_AUTHORIZATION" TextField="translation"
                    NavigateUrlField="code" />
                <asp:MenuItemBinding Selectable="True" DataMember="FORM_AUTHORIZATION" TextField="translation"
                    NavigateUrlField="code" TargetField="relative_url" />
            </DataBindings>
             
            <LevelMenuItemStyles>
                <asp:MenuItemStyle CssClass="TopMenuLink" />
            </LevelMenuItemStyles>
            <DynamicMenuStyle CssClass="SubMenuStyle" />
            <DynamicMenuItemStyle CssClass="MenuItemStyle" />
            <DynamicHoverStyle CssClass="MenuStyleHover" />
             <StaticHoverStyle CssClass ="SubMenuStyleHover" />
            <LevelSubMenuStyles>
                <asp:SubMenuStyle CssClass="SubMenuLink" />
                <asp:SubMenuStyle CssClass="SubMenuLink" />
                <asp:SubMenuStyle CssClass="SubMenuLink SubMenuLink2" />
            </LevelSubMenuStyles>

            <StaticItemTemplate>
                <a <%# IF(Container.Dataitem.NavigateUrl.ToString.Trim.Equals("HOME PAGE"),"href='javascript:setCell(""null""," + """" + Container.Dataitem.NavigateUrl + """, ""null"");'","") %>>
                    <%#Container.DataItem.Text%></a>
            </StaticItemTemplate>
            <DynamicItemTemplate>
                <a <%# IF(NOT Container.Dataitem.Target.ToString.Trim.Equals("none"),"href='javascript:setCell(""" + Container.Dataitem.Target + """"  + "," + """" + Container.Dataitem.NavigateUrl.Split(New Char() {"|"c})(0) + """" + "," + """" + Container.Dataitem.NavigateUrl.Split(New Char() {"|"c})(1) + """);'","") %>>
                    <%#Container.DataItem.Text%></a>
            </DynamicItemTemplate>
        </asp:Menu>
        <asp:XmlDataSource ID="xmlSource" runat="server" EnableCaching="false" XPath="/*/*">
        </asp:XmlDataSource>
    </span>
    <span class="minMaxWindow" style="text-align:right;">
        <asp:Image ID="imgDisplayMode" runat="server" ImageUrl="~/Navigation/images/icons/icon_max.png"
            Width="25" Height="21" AlternateText="Maximize display view" Visible="true">
        </asp:Image>
    </span>
</div>
<div class="navSeparator" />
<script type="text/javascript">


    var enabled = true;

    function setCell(rel, sel, qstr) {
        //if rel is null and NOT the home page, exit.  Also, exit if not enabled.
        if (sel == 'HEAD' || !enabled || (!rel && sel != 'HOME PAGE')) { return; }

        if (sel == 'HOME PAGE') {
            window.frames('Navigation_Content').location.href = 'HomeForm.aspx';
        }
        else {

            var sUrl;

            sUrl = '<%= Request.ApplicationPath %>/' + rel + '/' + sel + '.aspx'
            if (qstr != null) {
                if (qstr.length > 0) {
                    sUrl = sUrl + '?' + qstr;
                }
            }

            window.frames['Navigation_Content'].document.body.style.cursor = 'wait';
            window.frames['Navigation_Content'].location = sUrl;
        }
    }


    function toggleHeaderView() {
        if ((document.getElementById("<%= displayMode.ClientId %>").value == "visible") || (document.getElementById("<%= displayMode.ClientId %>").value == "")) {
            document.getElementById("Navigation_Header").style.display = "none";
            document.getElementById("<%= displayMode.ClientId %>").value = "hidden"
            document.getElementById("<%= imgDisplayMode.ClientId  %>").src = "./images/icons/icon_min.png"
            document.getElementById("<%= imgDisplayMode.ClientId  %>").alt = "Restore display view"

        }
        else if (document.getElementById("<%= displayMode.ClientId %>").value == "hidden") {
            document.getElementById("Navigation_Header").style.display = "";
            document.getElementById("<%= displayMode.ClientId %>").value = "visible"
            document.getElementById("<%= imgDisplayMode.ClientId  %>").src = "./images/icons/icon_max.png"
            document.getElementById("<%= imgDisplayMode.ClientId  %>").alt = "Maximize display view"

        }
    }


    function ToggleMenus() {

        var arrSS = new Array();
        var ss;

        //debugger;
        arrSS = document.styleSheets;

        for (i = 0; i < arrSS.length; i++) {
            if (arrSS[i].id == "NavigationStyle" || arrSS[i].title == "NavigationStyle") {
                ss = arrSS[i]
                break;
            }
        }

        if (ss.cssRules) {
            theRules = ss.cssRules
        } else if (ss.rules) {
            theRules = ss.rules
        } else {
            return;
        }


        var targetrule;
        for (i = 0; i < theRules.length; i++) {
            if (theRules[i].selectorText.toLowerCase() == ".submenulink a" || theRules[i].selectorText.toLowerCase() == ".submenulink a:hover") {
                targetrule = theRules[i];
                if (enabled) {
                    targetrule.style.color = '#fff';
                    $('#menuLangDll_msdd').removeAttr('disabled');
                } else {
                    targetrule.style.color = '#999';
                    $('#menuLangDll_msdd').attr('disabled', 'disabled');
                }
            }
        }
    }


    function refreshMenus() {
        document.location = document.location;       
    }

    $(document).ready(function () {
        // Handler for .ready() called.
        $(".MenuItemStyle").removeAttr("href");
        $(".TopMenuLink").removeAttr("href");

    });
    
</script>
