<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QuestionSetForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.GlobalConfig.QuestionSetForm" EnableSessionState="True"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <link rel="stylesheet" href="https://jqueryui.com/jquery-wp-content/themes/jquery/css/base.css?v=1" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="../Navigation/Styles/font-awesome.css" />
    <script type="text/javascript" src="../Scripts/knockout-3.1.0.debug.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-3.1.1.js"></script>
    <script type="text/javascript" src="../Scripts/knockout.mapping-latest.debug.js"></script>
    <script type="text/javascript" src="../Navigation/Scripts/ViewQuestionModel.js"></script>

    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
            $("#versions").accordion();
            $("div.tabs").tabs();
            var startIndex, endIndex;
            $("div.questionsList")
                .accordion(
                {
                    header: "> div > div.header",
                    collapsible: true,
                    heightStyle: "content"
                })
                .sortable(
                {
                    axis: "y",
                    handle: "i.sortHandle",
                    start: function (event, ui) {
                        ui.helper.css('top', ui.originalPosition.top);
                        startIndex = $(ui.item).parent().children().index(ui.item);
                    },
                    stop: function (event, ui) {
                        ui.item.children("div").triggerHandler("focusout");
                        $(this).accordion("refresh");
                        endIndex = $(ui.item).parent().children().index(ui.item);
                        ko.contextFor(ui.item[0]).$data.Move(endIndex - startIndex, ko.contextFor(ui.item[0]).$parent);
                    }
                });
        })

    </script>

    <style type="text/css">
        .fa-lg {
            color: GrayText;
            padding-right: 10px;
            padding-top: 2px;
            float: right;
        }

        input {
            width: 100%;
        }

        select {
            width: 100%;
        }

        /*table tr td {
            text-align: right;
        }*/

        .questionDetailsDiv {
            border-color: gray;
            border: solid;
            border-width: thin;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table align="left" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <tr>
                <td style="left: 0px; top: 0px; padding-left: 0px">
                    <div style="width: 100%; height: 98%">
                        <table class="searchGrid" border="0" width="100%">
                            <tbody>
                                <tr>
                                    <td width="1" style="width: 1px;"></td>
                                    <td align="right" nowrap="nowrap">
                                        <span style="font-weight: normal">
                                            <span class="mandatory">*</span>
                                            Code:
                                        </span>
                                    </td>
                                    <td align="left">
                                        <input class="medium" type="text" data-bind="value: QuestionSet.Code" />
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <span style="font-weight: normal">Current Version:
                                        </span>
                                    </td>
                                    <td align="left">
                                        <select class="medium" data-bind="options: QuestionSet.Versions, optionsCaption: 'Select', value: QuestionSet.CurrentVersion, optionsText: 'VersionNumber', optionsValue: 'VersionNumber'"></select>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="1" style="width: 1px;"></td>
                                    <td align="right" nowrap="nowrap">
                                        <span style="font-weight: normal">
                                            <span class="mandatory">*</span>
                                            Description:
                                        </span>
                                    </td>
                                    <td align="left">
                                        <input class="medium" type="text" data-bind="value: QuestionSet.Description" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div id="tabs" class="borderLeft">
            <ul>
                <li><a href="#questions">Questions</a></li>
                <li><a href="#activations">Activations</a></li>
            </ul>
            <div id="questions">
                <div data-bind="foreach: QuestionSet.Versions" style="border-color: black;" id="versions">
                    <div style="padding-left: 10px; padding-bottom: 3px; padding-top: 3px; margin-bottom: 2px;">
                        Version <span data-bind="text: VersionNumber"></span>
                        <i class="fa fa-plus fa-lg" aria-hidden="true" data-bind="click: AddQuestion" title="Add Question"></i>
                    </div>

                    <div data-bind="if: Questions" style="padding-left: 16px; padding-right: 16px; padding-top: 10px; padding-right: 10px">
                        <div data-bind="foreach: Questions" class="questionsList" style="border-color: black; overflow-y: scroll; height: 400px">
                            <div style="padding-bottom: 3px; padding-top: 3px; margin-bottom: 2px;">
                                <div class="header" style="width: 100%">
                                    <span data-bind="text: Translation"></span>
                                    <i class="fa fa-sort fa-lg sortHandle" aria-hidden="true"></i>
                                    <i class="fa fa-trash-o fa-lg" data-bind="click: $parent.DeleteQuestion" aria-hidden="true" title="Delete Question"></i>
                                </div>
                                <div class="tabs">
                                    <ul>
                                        <li><a data-bind="attr: { href: '#D-' + $parent.VersionNumber() + '-' + Code() }">Details</a></li>
                                        <li><a data-bind="attr: { href: '#A-' + $parent.VersionNumber() + '-' + Code() }">Answers</a></li>
                                        <li><a data-bind="attr: { href: '#P-' + $parent.VersionNumber() + '-' + Code() }">Pre-Conditions</a></li>
                                    </ul>
                                    <div data-bind="attr: { id: 'D-' + $parent.VersionNumber() + '-' + Code() }" class="questionDetailsDiv">

                                        <table class="searchGrid" border="0" width="100%" style="border-left: none">
                                            <tbody>
                                                <tr>
                                                    <td width="1" style="width: 1px;"></td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal">
                                                            <span class="mandatory">*</span>
                                                            Code:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <input class="medium" type="text" data-bind="value: Code" />
                                                    </td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal"><span class="mandatory">*</span>Answer Type:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <select class="medium" data-bind="options: $root.AnswerTypes, optionsCaption: 'Select', optionsValue: 'Code', optionsText: 'Description', value: AnswerType"></select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="1" style="width: 1px;"></td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal">
                                                            <span class="mandatory">*</span>
                                                            UI Program Code:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <input class="medium" type="text" data-bind="value: UiProgCode" />
                                                    </td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal"><span class="mandatory">*</span>Mandatory:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <input type="checkbox" data-bind="checked: Mandatory" /> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="1" style="width: 1px;"></td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal">
                                                            <span class="mandatory">*</span>
                                                            Translation:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <input class="medium" type="text" data-bind="value: Translation" />
                                                    </td>
                                                </tr>
                                                <tr data-bind="visible: DisplayScalePrecision">
                                                    <td width="1" style="width: 1px;"></td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal">
                                                            <span class="mandatory">*</span>
                                                            Scale:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <input class="small" type="text" data-bind="value: Scale" />
                                                    </td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal"><span class="mandatory">*</span>Precision:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <input class="small" type="text" data-bind="value: Precision" />
                                                    </td>
                                                </tr>
                                                <tr data-bind="visible: DisplayLength">
                                                    <td width="1" style="width: 1px;"></td>
                                                    <td align="right" nowrap="nowrap">
                                                        <span style="font-weight: normal">
                                                            <span class="mandatory">*</span>
                                                            Length:
                                                        </span>
                                                    </td>
                                                    <td align="left">
                                                        <input class="small" type="text" data-bind="value: Length" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div data-bind="if: Channels, attr: { id: 'C-' + $parent.VersionNumber() + '-' + Code() }">

                                            <table width="100%" border="0">

                                                <tbody>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">Channels</td>
                                                        <td width="80%">
                                                            <table>
                                                                <tr data-bind="foreach: Channels">
                                                                    <td align="left">&nbsp;&nbsp;<span data-bind="text: ChannelCode"></span></td>
                                                                    <%--<td align="left">
                                                                        <input type="checkbox" value="Code" data-bind="checked: Selected" /></td>
                                                                    <td></td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div data-bind="attr: { id: 'A-' + $parent.VersionNumber() + '-' + Code() }">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <input type="button" value="Add Answer" style="width: 100px" data-bind="click: AddAnswer" /></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" border="1" width="100%">
                                            <thead>
                                                <tr style="font-weight: bold">
                                                    <td width="18%" style="text-align: center">Code</td>
                                                    <td width="38%" style="text-align: center">UI Prog. Code</td>
                                                    <td width="41%" style="text-align: center">Text</td>
                                                    <td width="3%" style="text-align: center"></td>
                                                </tr>
                                            </thead>
                                            <tbody data-bind="foreach: Answers">
                                                <tr>
                                                    <td>
                                                        <input type="text" data-bind="value: Code" /></td>
                                                    <td>
                                                        <input type="text" data-bind="value: UiProgCode" /></td>
                                                    <td>
                                                        <input type="text" data-bind="value: Translation" /></td>
                                                    <td><i class="fa fa-trash-o fa-lg" aria-hidden="true" data-bind="click: $parent.DeleteAnswer" title="Delete Answer"></i></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div data-bind="attr: { id: 'P-' + $parent.VersionNumber() + '-' + Code() }">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <input type="button" value="Add Pre-Condition" style="width: 180px" data-bind="click: AddPreCondition" /></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" border="1" width="100%">
                                            <thead>
                                                <tr style="font-weight: bold">
                                                    <td width="50%" style="text-align: center">Question</td>
                                                    <td width="47%" style="text-align: center">Answer</td>
                                                    <td width="3%" style="text-align: center"></td>
                                                </tr>
                                            </thead>
                                            <tbody data-bind="foreach: PreConditions">
                                                <tr>
                                                    <td width="30%">
                                                        <select data-bind="options: $parent, optionsCaption: 'Select', optionsValue: 'Code', optionsText: 'Text', value: QsCode"></select>
                                                    </td>

                                                    <td>
                                                        <select data-bind="options: $parent.Answers, optionsCaption: 'Select', optionsValue: 'Code', optionsText: 'Text', value: AnsCode"></select></td>
                                                    <td><i class="fa fa-trash-o fa-lg" aria-hidden="true" data-bind="click: $parent.DeletePreCondition" title="Delete Pre-Condition"></i></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="activations">
            </div>
        </div>
    </div>



    <!--<i class="fa fa-plus fa-lg" aria-hidden="true" data-bind="click: AddVersion" title="Add Version"></i>-->
    <br />
    <div class="btnZone">
         <input type="button" value="Save" style="width:120px" SkinID="AlternateLeftButton" data-bind="click: save" />
        <asp:Button runat="server" ID="BackButton" Text="Back" color="black" width="120px" SkinID="AlternateLeftButton"  />
        <div style="width:100%">
            <pre data-bind="text: ko.toJSON(QuestionSet)" />
        </div>
    </div>

</asp:Content>



