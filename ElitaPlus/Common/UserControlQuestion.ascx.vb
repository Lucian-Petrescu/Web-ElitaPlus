Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.ServiceModel
Imports System.Text
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimRecordingService
Imports Assurant.Elita.Questions.Contracts


Public Class UserControlQuestion
    Inherits System.Web.UI.UserControl

#Region "Constants"
    Public Class ViewStateItems
        Public Const ClaimRecordingResponse As String = "ClaimRecordingResponse"
        Public Const DateFormat As String = "DateFormat"
        Public Const UserNameSetting As String = "UserNameSetting"
        Public Const PasswordSetting As String = "PasswordSetting"
        Public Const ServiceUrlSetting As String = "ServiceUrlSetting"
        Public Const ServiceEndPointNameSetting As String = "ServiceEndPointNameSetting"
        Public Const BestReplacementDeviceSelected As String = "BestReplacementDeviceSelected"
        Public Const LogisticsStage As String = "LogisticsStage"
        Public Const CertificateId As String = "CertificateId"
        Public Const ClaimBase As String = "ClaimBase"
        Public Const QuestionDataSource As String = "QuestionDataSource"
        Public Const ErrorQuestionCodes As String = "ErrorQuestionCodes"
        Public Const ErrAnswerMandatory As String = "ErrAnswerMandatory"
        Public Const ErrTextAnswerLength As String = "ErrTextAnswerLength"

    End Class
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim parentControl = Parent

        Debug.WriteLine($"Loading UserControlQuestion ID: {Me.ID}")
        While (Not TypeOf parentControl Is ElitaPlusPage And parentControl IsNot Nothing)
            parentControl = parentControl.Parent
        End While
        ElitaHostPage = DirectCast(parentControl, ElitaPlusPage)
    End Sub

    Private Sub UserControlQuestion_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Debug.WriteLine($"Unloading UserControlQuestion ID: {Me.ID}")
    End Sub
#Region "Properties"

    Public Property ElitaHostPage As ElitaPlusPage
    Public Property DateFormat As String
        Get
            Return DirectCast(ViewState(ViewStateItems.DateFormat), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.DateFormat) = value
        End Set
    End Property
    Public Property UserNameSetting As String
        Get
            Return DirectCast(ViewState(ViewStateItems.UserNameSetting), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.UserNameSetting) = value
        End Set
    End Property
    Public Property PasswordSetting As String
        Get
            Return DirectCast(ViewState(ViewStateItems.PasswordSetting), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.PasswordSetting) = value
        End Set
    End Property
    Public Property ServiceUrlSetting As String
        Get
            Return DirectCast(ViewState(ViewStateItems.ServiceUrlSetting), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.ServiceUrlSetting) = value
        End Set
    End Property
    Public Property ServiceEndPointNameSetting As String
        Get
            Return DirectCast(ViewState(ViewStateItems.ServiceEndPointNameSetting), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.ServiceEndPointNameSetting) = value
        End Set
    End Property
    Public Property HostMessageController As IMessageController

    Public Property SubmitWsBaseClaimRecordingResponse As BaseClaimRecordingResponse
        Get
            Return DirectCast(ViewState(ViewStateItems.ClaimRecordingResponse), BaseClaimRecordingResponse)
        End Get
        Set(value As BaseClaimRecordingResponse)
            ViewState(ViewStateItems.ClaimRecordingResponse) = value
        End Set
    End Property
    Public Property BestReplacementDeviceSelected As BestReplacementDeviceInfo
        Get
            Return DirectCast(ViewState(ViewStateItems.BestReplacementDeviceSelected), BestReplacementDeviceInfo)
        End Get
        Set(value As BestReplacementDeviceInfo)
            ViewState(ViewStateItems.BestReplacementDeviceSelected) = value
        End Set
    End Property
    Public Property LogisticsStage As Integer
        Get
            Return DirectCast(ViewState(ViewStateItems.LogisticsStage), Integer)
        End Get
        Set(value As Integer)
            ViewState(ViewStateItems.LogisticsStage) = value
        End Set
    End Property
    Public Property CertificateId As Guid
        Get
            Return DirectCast(ViewState(ViewStateItems.CertificateId), Guid)
        End Get
        Set(value As Guid)
            ViewState(ViewStateItems.CertificateId) = value
        End Set
    End Property
    Public Property ClaimBo As ClaimBase
        Get
            Return DirectCast(ViewState(ViewStateItems.ClaimBase), ClaimBase)
        End Get
        Set(value As ClaimBase)
            ViewState(ViewStateItems.ClaimBase) = value
        End Set
    End Property

    Public Property ErrorQuestionCodes As StringBuilder
        Get
            Return DirectCast(ViewState(ViewStateItems.ErrorQuestionCodes), StringBuilder)
        End Get
        Set(value As StringBuilder)
            ViewState(ViewStateItems.ErrorQuestionCodes) = value
        End Set
    End Property
    Public Property ErrAnswerMandatory As StringBuilder
        Get
            Return DirectCast(ViewState(ViewStateItems.ErrAnswerMandatory), StringBuilder)
        End Get
        Set(value As StringBuilder)
            ViewState(ViewStateItems.ErrAnswerMandatory) = value
        End Set
    End Property

    Public Property ErrTextAnswerLength As StringBuilder
        Get
            Return DirectCast(ViewState(ViewStateItems.ErrTextAnswerLength), StringBuilder)
        End Get
        Set(value As StringBuilder)
            ViewState(ViewStateItems.ErrTextAnswerLength) = value
        End Set
    End Property


#Region "Questions DataSource"
    'Public Property QuestionDataSource As Object
    '    Get
    '        Return GridQuestions.DataSource
    '    End Get
    '    Set(value As Object)
    '        GridQuestions.DataSource = value
    '    End Set
    'End Property
    Public Property QuestionDataSource As Question()
        Get
            Return DirectCast(ViewState(ViewStateItems.QuestionDataSource), Question())
        End Get
        Set(value As Question())
            ViewState(ViewStateItems.QuestionDataSource) = value
        End Set
    End Property
#End Region
#End Region

#Region "User Control Events"
#Region "DisplayViewEvent"
    Public Event DisplayViewEvent As EventHandler
    Protected Sub OnDisplayViewEvent()
        RaiseEvent DisplayViewEvent(Me, EventArgs.Empty)
    End Sub
#End Region
#Region "ReturnBackEvent"
    Public Event ReturnBackEvent As EventHandler
    Protected Sub OnReturnBackEvent()
        RaiseEvent ReturnBackEvent(Me, EventArgs.Empty)
    End Sub
#End Region
#Region "CallPageEvent"
    Public Delegate Sub CallPageHandler(ByVal url As String, ByVal guidId As Guid)

    Public Event CallPageEvent As CallPageHandler
    Protected Sub OnCallPageEvent(ByVal url As String, ByVal guidId As Guid)
        RaiseEvent CallPageEvent(url, guidId)
    End Sub
#End Region
#Region "ProtectionInfoChanged"
    Public Delegate Sub ProtectionInfoHandler(protectionInfo As ProtectionInfo)

    Public Event ProtectionInfoChanged As ProtectionInfoHandler
    Protected Sub OnProtectionInfoChanged(protectionInfo As ProtectionInfo)
        RaiseEvent ProtectionInfoChanged(protectionInfo)
    End Sub
#End Region

#End Region

#Region "AnswerTypes"
    Public Class AnswerTypes
        Public Const ChoiceAnswer As String = "CHOICE"
        Public Const ListOfValues As String = "LISTOFVALUES"
        Public Const NumberAnswer As String = "NUMBER"
        Public Const TextAnswer As String = "TEXT"
        Public Const DateAnswer As String = "DATE"
        Public Const LegalConsent As String = "LEGALCONSENT"
        Public Const BooleanAnswer As String = "BOOLEAN"
        Public Const ContentAnswer As String = "CONTENT"
    End Class
#End Region

#Region "CSS Names"
    Public Class CssStateNames

        Public Const StateActive As String = "StatActive"
        Public Const StateClosed As String = "StatClosed"
    End Class
#End Region

#Region "ProtectionInfo"
    Public Class ProtectionInfo
        Public Property CustomerName As String
        Public Property DealerName As String
        Public Property CallerName As String
        Public Property ProtectionStatus As String
        Public Property ClaimedMake As String
        Public Property EnrolledModel As String
        Public Property ClaimStatus As String
        Public Property EnrolledMake As String
        Public Property ClaimNumber As String
        Public Property ClaimedModel As String
        Public Property TypeOfLoss As String
        Public Property DateOfLoss As String
        Public Property ClaimStatusCss As String
        Public Property ShowCustomerAddress As Boolean
        Public Property CustomerAddress As String
    End Class
#End Region

#Region "User Control Methods"
    Public Sub SetQuestionTitle(ByVal text As String)
        lblQuestions.Text = text
    End Sub
    Public Sub QuestionDataBind()
        GridQuestions.DataSource = QuestionDataSource
        GridQuestions.DataBind()
    End Sub

    'Public Sub SetEnableSaveExitButton(ByVal value As Boolean)


    '    ControlMgr.SetEnableControl(ElitaHostPage, btn_Quest_SaveExit, value)
    'End Sub

#End Region


#Region "Questions View"
#Region "Questions View - Load Data"
    Protected Sub GridQuestions_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridQuestions.RowDataBound

        Dim source As IEnumerable(Of Question) = CType(sender, GridView).DataSource
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim isReEvaulateOnChange As Boolean
            If DirectCast(e.Row.DataItem, Question).ReEvaulateOnChange Is Nothing Then
                isReEvaulateOnChange = False
            Else
                isReEvaulateOnChange = DirectCast(e.Row.DataItem, Question).ReEvaulateOnChange
            End If

            Dim oAnswerType As String = CType(e.Row.FindControl("lblAnswerType"), Label).Text

            Dim lblRequired As Label = CType(e.Row.FindControl("lblRequired"), Label)
            Dim isMandatory As Boolean = DirectCast(e.Row.DataItem, Question).Mandatory
            If isMandatory Then
                lblRequired.Visible = True
            End If
            CType(e.Row.FindControl("lblQuestion"), Label).Text = DirectCast(e.Row.DataItem, Question).Text

            Select Case oAnswerType.Trim().ToUpper()
                Case AnswerTypes.ChoiceAnswer
                    Dim rbl As RadioButtonList = CType(e.Row.FindControl("rblChoice"), RadioButtonList)
                    rbl.Visible = True
                    Dim question As ChoiceAnswer = DirectCast(DirectCast(e.Row.DataItem, Question).Answer, ChoiceAnswer)
                    Dim answer As AnswerValue
                    Dim listItems As List(Of ListItem) = New List(Of ListItem)()
                    For Each answer In question.Answers.ToArray.OrderBy("SequenceNumber", LinqExtentions.SortDirection.Ascending)
                        listItems.Add(New ListItem(answer.Text, answer.Code))
                    Next
                    If isReEvaulateOnChange Then
                        rbl.AutoPostBack = isReEvaulateOnChange
                    End If
                    ElitaPlusPage.BindListControlToArray(rbl, listItems.ToArray(), False)

                    If (Not source Is Nothing) Then
                        Dim da As ChoiceAnswer = CType(source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, Question).Code).Answer, ChoiceAnswer)
                        If (Not da Is Nothing AndAlso Not da.Answer Is Nothing AndAlso da.Answer.Text <> Nothing) Then
                            rbl.SelectedValue = da.Answer.Code.ToString()
                        End If
                    End If

                Case AnswerTypes.ListOfValues
                    Dim ddl As DropDownList = CType(e.Row.FindControl("ddlList"), DropDownList)
                    ddl.Visible = True

                    Dim question As ListOfValuesAnswer = DirectCast(DirectCast(e.Row.DataItem, Question).Answer, ListOfValuesAnswer)
                    Dim answer As AnswerValue
                    Dim listItems As List(Of ListItem) = New List(Of ListItem)()
                    For Each answer In question.Answers.ToArray.OrderBy("SequenceNumber", LinqExtentions.SortDirection.Ascending)
                        listItems.Add(New ListItem(answer.Text, answer.Code))
                    Next
                    If isReEvaulateOnChange Then
                        ddl.AutoPostBack = isReEvaulateOnChange
                    End If
                    ElitaPlusPage.BindListControlToArray(ddl, listItems.ToArray(), True)

                    If (Not source Is Nothing) Then
                        Dim da As ListOfValuesAnswer = CType(source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, Question).Code).Answer, ListOfValuesAnswer)
                        If (Not da Is Nothing AndAlso Not da.Answer Is Nothing AndAlso da.Answer.Text <> Nothing) Then
                            ddl.SelectedValue = da.Answer.Code.ToString()
                        End If
                    End If

                Case AnswerTypes.NumberAnswer, AnswerTypes.TextAnswer
                    Dim txt As TextBox = CType(e.Row.FindControl("txtNo"), TextBox)
                    txt.Visible = True
                    If oAnswerType.Trim().ToUpper().Equals(AnswerTypes.NumberAnswer) Then
                        txt.TextMode = TextBoxMode.Number
                    End If
                    If isReEvaulateOnChange Then
                        txt.AutoPostBack = isReEvaulateOnChange
                    End If
                    If (oAnswerType.Trim().ToUpper = AnswerTypes.NumberAnswer) Then

                        If (Not source Is Nothing) Then
                            Dim da As NumberAnswer = CType(source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, Question).Code).Answer, NumberAnswer)

                            If (Not da Is Nothing AndAlso Not da.Answer Is Nothing AndAlso da.Answer.HasValue) Then
                                txt.Text = da.Answer.Value.ToString()
                            End If
                        End If
                    Else
                        If (Not source Is Nothing) Then
                            Dim da As TextAnswer = CType(source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, Question).Code).Answer, TextAnswer)

                            If (Not da Is Nothing AndAlso Not String.IsNullOrEmpty(da.Answer)) Then
                                txt.Text = da.Answer.ToString()
                            End If
                        End If
                    End If

                Case AnswerTypes.DateAnswer
                    Dim textBoxDate As TextBox = CType(e.Row.FindControl("txtDate"), TextBox)
                    Dim imageTxtDate As ImageButton = CType(e.Row.FindControl("btnDate"), ImageButton)
                    ElitaPlusPage.SharedAddCalendarNew(imageTxtDate, textBoxDate)
                    imageTxtDate.Visible = True
                    textBoxDate.Visible = True
                    If isReEvaulateOnChange Then
                        textBoxDate.AutoPostBack = isReEvaulateOnChange
                    End If
                    If (Not source Is Nothing) Then
                        Dim da As DateAnswer = CType(source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, Question).Code).Answer, DateAnswer)

                        If (Not da Is Nothing AndAlso Not da.Answer Is Nothing AndAlso da.Answer.HasValue) Then
                            textBoxDate.Text = da.Answer.Value.ToString("dd-MMM-yyyy")
                        End If
                    End If

                Case AnswerTypes.BooleanAnswer, AnswerTypes.LegalConsent
                    Dim tCheckBox As CheckBox = CType(e.Row.FindControl("chkCheck"), CheckBox)
                    tCheckBox.Visible = True
                    If (oAnswerType.Trim().ToUpper = AnswerTypes.BooleanAnswer) Then

                        If (Not source Is Nothing) Then
                            Dim da As BooleanAnswer = CType(source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, Question).Code).Answer, BooleanAnswer)

                            If (Not da Is Nothing AndAlso Not da.Answer Is Nothing AndAlso da.Answer.HasValue) Then
                                tCheckBox.Checked = da.Answer.Value
                            End If
                        End If
                    Else
                        If (Not source Is Nothing) Then
                            Dim da As LegalConsentAnswer = CType(source.First(Function(f) f.Code = DirectCast(e.Row.DataItem, Question).Code).Answer, LegalConsentAnswer)

                            If (Not da Is Nothing AndAlso Not da.Answer Is Nothing AndAlso da.Answer.HasValue) Then
                                tCheckBox.Checked = da.Answer.Value
                            End If
                        End If
                    End If
                Case AnswerTypes.ContentAnswer
                    ' do nothing skip
                Case Else
                    Throw New NotSupportedException(oAnswerType.Trim().ToUpper())
            End Select
        End If
    End Sub
#Region "ReEvaluate Questions Data"
    Private Sub ReevaluateQuestion()
        Dim answerType As String
        Dim code As String

        Dim questionObject As Question
        Dim row As GridViewRow
        Dim questionSubmitobj
        Dim errorQuestionCodes As New StringBuilder

        If SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
            If TypeOf SubmitWsBaseClaimRecordingResponse Is QuestionResponse Then
                questionSubmitobj = DirectCast(SubmitWsBaseClaimRecordingResponse, QuestionResponse)
            ElseIf TypeOf SubmitWsBaseClaimRecordingResponse Is CallerAuthenticationResponse Then
                Exit Sub
                ' This is temporary as Question parameter is in not coming for caller authentication
                'questionSubmitobj = DirectCast(State.SubmitWsBaseClaimRecordingResponse, CallerAuthenticationResponse)
            Else
                Exit Sub
            End If

            For Each row In GridQuestions.Rows
                answerType = CType(row.FindControl("lblAnswerType"), Label).Text
                code = CType(row.FindControl("lblCode"), Label).Text

                If TypeOf questionSubmitobj Is QuestionResponse Then
                    questionObject = DirectCast(questionSubmitobj, QuestionResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
                ElseIf TypeOf questionSubmitobj Is CallerAuthenticationResponse Then
                    questionObject = DirectCast(questionSubmitobj, CallerAuthenticationResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
                End If

                Select Case answerType.Trim().ToUpper()
                    Case AnswerTypes.ChoiceAnswer
                        Dim answer As String = CType(row.FindControl("rblChoice"), RadioButtonList).SelectedValue
                        Dim choiceAnswer As ChoiceAnswer = questionObject.Answer
                        choiceAnswer.Answer = choiceAnswer.Answers.FirstOrDefault(Function(y) y.Code = answer)
                    Case AnswerTypes.ListOfValues
                        Dim answer As String = CType(row.FindControl("ddlList"), DropDownList).SelectedValue
                        Dim listofvalues As ListOfValuesAnswer = questionObject.Answer
                        listofvalues.Answer = listofvalues.Answers.FirstOrDefault(Function(y) y.Code = answer)
                    Case AnswerTypes.NumberAnswer, AnswerTypes.TextAnswer
                        Dim answer As String = CType(row.FindControl("txtNo"), TextBox).Text
                        If (Not String.IsNullOrEmpty(answer)) Then
                            If answerType.Trim().ToUpper().Equals("NUMBER") Then
                                Dim numbervalue As NumberAnswer = New NumberAnswer()
                                Try
                                    numbervalue.Answer = Convert.ToDecimal(answer)
                                Catch ex As Exception
                                    errorQuestionCodes.AppendLine(questionObject.Text)
                                End Try
                                questionObject.Answer = numbervalue
                            Else
                                Dim txtValue As TextAnswer = New TextAnswer()
                                txtValue.Answer = answer
                                questionObject.Answer = txtValue
                            End If
                        End If
                    Case AnswerTypes.BooleanAnswer, AnswerTypes.LegalConsent
                        Dim answer As Boolean = CType(row.FindControl("chkCheck"), CheckBox).Checked

                        If answerType.Trim().ToUpper().Equals("BOOLEAN") Then
                            Dim booleanValue As BooleanAnswer = New BooleanAnswer()
                            booleanValue.Answer = answer
                            questionObject.Answer = booleanValue
                        Else
                            Dim legalConsentValue As LegalConsentAnswer = New LegalConsentAnswer()
                            legalConsentValue.Answer = answer
                            questionObject.Answer = legalConsentValue
                        End If
                    Case AnswerTypes.DateAnswer
                        Dim answer As String = CType(row.FindControl("txtDate"), TextBox).Text
                        If (Not String.IsNullOrEmpty(answer)) Then
                            Dim dateAnswer As DateAnswer = New DateAnswer()
                            Try
                                'Fix for Japan date control-------------------------------
                                Dim formatProvider = LocalizationMgr.CurrentFormatProvider
                                If formatProvider.Name.Equals("ja-JP") Then
                                    Dim dateFragments() As String = answer.Split("-")
                                    dateAnswer.Answer = New DateTime(Integer.Parse(dateFragments(2)), Integer.Parse(dateFragments(1)), Integer.Parse(dateFragments(0))).Date
                                Else
                                    dateAnswer.Answer = Convert.ToDateTime(answer, formatProvider)
                                End If
                                '--------------------------------------------------------
                            Catch ex As Exception
                                errorQuestionCodes.AppendLine(questionObject.Text)
                            End Try
                            questionObject.Answer = dateAnswer
                        End If
                    Case AnswerTypes.ContentAnswer
                        ' do nothing skip
                    Case Else
                        Throw New NotSupportedException(answerType.Trim().ToUpper())
                End Select
            Next

            If (Not String.IsNullOrEmpty(errorQuestionCodes.ToString())) Then
                HostMessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_TO_QUESTION_INVALID_ERR, True)
                Exit Sub
            End If


            Try
                questionSubmitobj.Questions = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, Question())(
                                                                            GetClient(),
                                                                            New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                            Function(ByVal c As ClaimRecordingServiceClient)
                                                                                Return c.ReEvaluateQuestions(questionSubmitobj.Questions, questionSubmitobj.Parameters)
                                                                            End Function)

            Catch ex As FaultException
                LocalThrowWsFaultExceptions(ex)
                Exit Sub
            End Try

            If questionSubmitobj IsNot Nothing Then
                SubmitWsBaseClaimRecordingResponse = questionSubmitobj
            End If
            OnDisplayViewEvent()
        End If
    End Sub
    Protected Sub ddlList_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ReevaluateQuestion()
        Catch ex As Exception
            LocalHandleErrors(ex, HostMessageController)
        End Try
    End Sub
    Protected Sub rblChoice_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ReevaluateQuestion()
        Catch ex As Exception
            LocalHandleErrors(ex, HostMessageController)
        End Try
    End Sub

    Protected Sub txtNo_OnTextChanged(sender As Object, e As EventArgs)
        Try
            ReevaluateQuestion()
        Catch ex As Exception
            LocalHandleErrors(ex, HostMessageController)
        End Try
    End Sub
    Protected Sub txtDate_OnTextChanged(sender As Object, e As EventArgs)
        Try
            ReevaluateQuestion()
        Catch ex As Exception
            LocalHandleErrors(ex, HostMessageController)
        End Try
    End Sub
#End Region

#End Region
#Region "Question View - Button Click"

    Public Sub GetQuestionAnswer()

        Dim row As GridViewRow
        Dim answerType As String
        Dim code As String
        Dim questionObjects As Question() = QuestionDataSource

        ErrAnswerMandatory = New StringBuilder()
        ErrorQuestionCodes = New StringBuilder()
        ErrTextAnswerLength = new StringBuilder()
        Try
            For Each row In GridQuestions.Rows

                answerType = CType(row.FindControl("lblAnswerType"), Label).Text
                code = CType(row.FindControl("lblCode"), Label).Text

                'If TypeOf questionSubmitobj Is QuestionResponse Then
                '    questionObject = DirectCast(questionSubmitobj, QuestionResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
                'ElseIf TypeOf questionSubmitobj Is CallerAuthenticationResponse Then
                '    questionObject = DirectCast(questionSubmitobj, CallerAuthenticationResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
                'End If

                Dim questionObject = questionObjects.FirstOrDefault(Function(x) x.Code = code)

                Select Case answerType.Trim().ToUpper()
                    Case AnswerTypes.ChoiceAnswer
                        Dim answer As String = CType(row.FindControl("rblChoice"), RadioButtonList).SelectedValue
                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
                            ErrAnswerMandatory.AppendLine(questionObject.Text & " </br>")
                        Else
                            Dim choiceAnswer As ChoiceAnswer = questionObject.Answer
                            choiceAnswer.Answer = choiceAnswer.Answers.FirstOrDefault(Function(y) y.Code = answer)
                        End If
                    Case AnswerTypes.ListOfValues
                        Dim answer As String = CType(row.FindControl("ddlList"), DropDownList).SelectedValue
                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
                            ErrAnswerMandatory.AppendLine(questionObject.Text & " </br>")
                        Else
                            Dim listOfValues As ListOfValuesAnswer = questionObject.Answer
                            listOfValues.Answer = listOfValues.Answers.FirstOrDefault(Function(y) y.Code = answer)
                        End If
                    Case AnswerTypes.NumberAnswer, AnswerTypes.TextAnswer
                        Dim answer As String = CType(row.FindControl("txtNo"), TextBox).Text
                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
                            ErrAnswerMandatory.AppendLine(questionObject.Text & " </br>")
                        ElseIf (Not String.IsNullOrEmpty(answer)) Then
                            If answerType.Trim().ToUpper().Equals(AnswerTypes.NumberAnswer) Then
                                Dim numberValue As NumberAnswer = New NumberAnswer()
                                Try
                                    numberValue.Answer = Convert.ToDecimal(answer)
                                Catch ex As Exception
                                    ErrorQuestionCodes.AppendLine(questionObject.Text)
                                End Try
                                questionObject.Answer = numberValue
                            Else
                                If(Not String.IsNullOrWhiteSpace(questionObject.Length) AndAlso answer.Length > questionObject.Length)
                                    ErrTextAnswerLength.AppendLine(questionObject.Text & " </br>")
                                else
                                    Dim txtValue As TextAnswer = New TextAnswer()
                                    txtValue.Answer = answer
                                    questionObject.Answer = txtValue
                                End If
                            End If
                        End If
                    Case AnswerTypes.BooleanAnswer, AnswerTypes.LegalConsent
                        Dim answer As Boolean = CType(row.FindControl("chkCheck"), CheckBox).Checked

                        If answerType.Trim().ToUpper().Equals(AnswerTypes.BooleanAnswer) Then
                            Dim booleanValue As BooleanAnswer = New BooleanAnswer()
                            booleanValue.Answer = answer
                            questionObject.Answer = booleanValue
                        Else
                            ' In case of Legal Consent then Answer should be True to move ahead
                            If questionObject.Mandatory AndAlso Not (answer) Then
                                ErrAnswerMandatory.AppendLine(questionObject.Text & " </br>")
                            Else
                                Dim legalConsentValue As LegalConsentAnswer = New LegalConsentAnswer()
                                legalConsentValue.Answer = answer
                                questionObject.Answer = legalConsentValue
                            End If
                        End If
                    Case AnswerTypes.DateAnswer
                        Dim answer As String = CType(row.FindControl("txtDate"), TextBox).Text
                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
                            ErrAnswerMandatory.AppendLine(questionObject.Text & " </br>")
                        ElseIf (Not String.IsNullOrEmpty(answer)) Then
                            Dim dateAnswer As DateAnswer = New DateAnswer()
                            Try
                                'Fix for Japan date control-------------------------------
                                Dim formatProvider = LocalizationMgr.CurrentFormatProvider
                                If formatProvider.Name.Equals("ja-JP") Then
                                    Dim dateFragments() As String = answer.Split("-")
                                    dateAnswer.Answer = New DateTime(Integer.Parse(dateFragments(2)), Integer.Parse(dateFragments(1)), Integer.Parse(dateFragments(0))).Date
                                Else
                                    dateAnswer.Answer = Convert.ToDateTime(answer, formatProvider)
                                End If
                                '--------------------------------------------------------
                            Catch ex As Exception
                                ErrorQuestionCodes.AppendLine(questionObject.Text)
                            End Try
                            questionObject.Answer = dateAnswer
                        End If
                    Case AnswerTypes.ContentAnswer
                        ' do nothing skip
                End Select
            Next
        Catch ex As ThreadAbortException
        Catch ex As Exception
            LocalHandleErrors(ex, HostMessageController)
        End Try
    End Sub

    'Protected Sub BtnQuestContinue(sender As Object, e As EventArgs) Handles btn_Quest_Cont.Click

    '    Dim answerType As String
    '    Dim code As String
    '    Dim errorQuestionCodes As New StringBuilder
    '    Dim errAnswerMandatory As New StringBuilder
    '    Dim questionSubmitobj
    '    Dim questionObject As Question
    '    Dim row As GridViewRow

    '    Try
    '        If SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
    '            If TypeOf SubmitWsBaseClaimRecordingResponse Is QuestionResponse Then
    '                questionSubmitobj = DirectCast(SubmitWsBaseClaimRecordingResponse, QuestionResponse)
    '            ElseIf TypeOf SubmitWsBaseClaimRecordingResponse Is CallerAuthenticationResponse Then
    '                questionSubmitobj = DirectCast(SubmitWsBaseClaimRecordingResponse, CallerAuthenticationResponse)
    '            Else
    '                Exit Sub
    '            End If


    '            For Each row In GridQuestions.Rows
    '                answerType = CType(row.FindControl("lblAnswerType"), Label).Text
    '                code = CType(row.FindControl("lblCode"), Label).Text

    '                If TypeOf questionSubmitobj Is QuestionResponse Then
    '                    questionObject = DirectCast(questionSubmitobj, QuestionResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
    '                ElseIf TypeOf questionSubmitobj Is CallerAuthenticationResponse Then
    '                    questionObject = DirectCast(questionSubmitobj, CallerAuthenticationResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
    '                End If

    '                Select Case answerType.Trim().ToUpper()
    '                    Case AnswerTypes.ChoiceAnswer
    '                        Dim answer As String = CType(row.FindControl("rblChoice"), RadioButtonList).SelectedValue
    '                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
    '                            errAnswerMandatory.AppendLine(questionObject.Text & " </br>")
    '                        Else
    '                            Dim choiceAnswer As ChoiceAnswer = questionObject.Answer
    '                            choiceAnswer.Answer = choiceAnswer.Answers.FirstOrDefault(Function(y) y.Code = answer)
    '                        End If
    '                    Case AnswerTypes.ListOfValues
    '                        Dim answer As String = CType(row.FindControl("ddlList"), DropDownList).SelectedValue
    '                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
    '                            errAnswerMandatory.AppendLine(questionObject.Text & " </br>")
    '                        Else
    '                            Dim listOfValues As ListOfValuesAnswer = questionObject.Answer
    '                            listOfValues.Answer = listOfValues.Answers.FirstOrDefault(Function(y) y.Code = answer)
    '                        End If
    '                    Case AnswerTypes.NumberAnswer, AnswerTypes.TextAnswer
    '                        Dim answer As String = CType(row.FindControl("txtNo"), TextBox).Text
    '                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
    '                            errAnswerMandatory.AppendLine(questionObject.Text & " </br>")
    '                        ElseIf (Not String.IsNullOrEmpty(answer)) Then
    '                            If answerType.Trim().ToUpper().Equals(AnswerTypes.NumberAnswer) Then
    '                                Dim numberValue As NumberAnswer = New NumberAnswer()
    '                                Try
    '                                    numberValue.Answer = Convert.ToDecimal(answer)
    '                                Catch ex As Exception
    '                                    errorQuestionCodes.AppendLine(questionObject.Text)
    '                                End Try
    '                                questionObject.Answer = numberValue
    '                            Else
    '                                Dim txtValue As TextAnswer = New TextAnswer()
    '                                txtValue.Answer = answer
    '                                questionObject.Answer = txtValue
    '                            End If
    '                        End If
    '                    Case AnswerTypes.BooleanAnswer, AnswerTypes.LegalConsent
    '                        Dim answer As Boolean = CType(row.FindControl("chkCheck"), CheckBox).Checked

    '                        If answerType.Trim().ToUpper().Equals(AnswerTypes.BooleanAnswer) Then
    '                            Dim booleanValue As BooleanAnswer = New BooleanAnswer()
    '                            booleanValue.Answer = answer
    '                            questionObject.Answer = booleanValue
    '                        Else
    '                            ' In case of Legal Consent then Answer should be True to move ahead
    '                            If questionObject.Mandatory AndAlso Not (answer) Then
    '                                errAnswerMandatory.AppendLine(questionObject.Text & " </br>")
    '                            Else
    '                                Dim legalConsentValue As LegalConsentAnswer = New LegalConsentAnswer()
    '                                legalConsentValue.Answer = answer
    '                                questionObject.Answer = legalConsentValue
    '                            End If
    '                        End If
    '                    Case AnswerTypes.DateAnswer
    '                        Dim answer As String = CType(row.FindControl("txtDate"), TextBox).Text
    '                        If questionObject.Mandatory AndAlso String.IsNullOrEmpty(answer) Then
    '                            errAnswerMandatory.AppendLine(questionObject.Text & " </br>")
    '                        ElseIf (Not String.IsNullOrEmpty(answer)) Then
    '                            Dim dateAnswer As DateAnswer = New DateAnswer()
    '                            Try
    '                                'Fix for Japan date control-------------------------------
    '                                Dim formatProvider = LocalizationMgr.CurrentFormatProvider
    '                                If formatProvider.Name.Equals("ja-JP") Then
    '                                    Dim dateFragments() As String = answer.Split("-")
    '                                    dateAnswer.Answer = New DateTime(Integer.Parse(dateFragments(2)), Integer.Parse(dateFragments(1)), Integer.Parse(dateFragments(0))).Date
    '                                Else
    '                                    dateAnswer.Answer = Convert.ToDateTime(answer, formatProvider)
    '                                End If
    '                                '--------------------------------------------------------
    '                            Catch ex As Exception
    '                                errorQuestionCodes.AppendLine(questionObject.Text)
    '                            End Try
    '                            questionObject.Answer = dateAnswer
    '                        End If
    '                End Select
    '            Next

    '            If (Not String.IsNullOrEmpty(errAnswerMandatory.ToString())) Then
    '                HostMessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_IS_REQUIRED_ERR, True)
    '                Exit Sub
    '            ElseIf (Not String.IsNullOrEmpty(errorQuestionCodes.ToString())) Then
    '                HostMessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_ANSWER_TO_QUESTION_INVALID_ERR, True)
    '                Exit Sub
    '            End If


    '            Dim wsRequest
    '            If SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
    '                If TypeOf SubmitWsBaseClaimRecordingResponse Is QuestionResponse Then
    '                    wsRequest = New QuestionRequest()
    '                    wsRequest.QuestionSetCode = questionSubmitobj.QuestionSetCode
    '                    wsRequest.Version = questionSubmitobj.Version
    '                ElseIf TypeOf SubmitWsBaseClaimRecordingResponse Is CallerAuthenticationResponse Then
    '                    wsRequest = New CallerAuthenticationRequest()
    '                End If
    '            End If



    '            wsRequest.CaseNumber = questionSubmitobj.CaseNumber
    '            wsRequest.CompanyCode = questionSubmitobj.CompanyCode
    '            wsRequest.InteractionNumber = questionSubmitobj.InteractionNumber
    '            wsRequest.Questions = questionSubmitobj.Questions


    '            Try
    '                Dim wsResponse = WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService, BaseClaimRecordingResponse)(
    '                                                                            GetClient(),
    '                                                                            New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
    '                                                                            Function(ByVal c As ClaimRecordingServiceClient)
    '                                                                                Return c.Submit(wsRequest)
    '                                                                            End Function)
    '                If wsResponse IsNot Nothing Then
    '                    SubmitWsBaseClaimRecordingResponse = wsResponse

    '                    If ClaimBo Is Nothing _
    '                        AndAlso Not SubmitWsBaseClaimRecordingResponse.ClaimNumber Is Nothing _
    '                        AndAlso Not SubmitWsBaseClaimRecordingResponse.CompanyCode Is Nothing _
    '                        AndAlso Not String.IsNullOrEmpty(SubmitWsBaseClaimRecordingResponse.ClaimNumber) _
    '                        AndAlso Not String.IsNullOrEmpty(SubmitWsBaseClaimRecordingResponse.CompanyCode) Then
    '                        Dim oClaimBase As ClaimBase = ClaimFacade.Instance.GetClaimByClaimNumber(Of ClaimBase)(SubmitWsBaseClaimRecordingResponse.CompanyCode, SubmitWsBaseClaimRecordingResponse.ClaimNumber)
    '                        If Not oClaimBase Is Nothing Then
    '                            Dim protectionInfo As New ProtectionInfo
    '                            With protectionInfo
    '                                .ClaimNumber = oClaimBase.ClaimNumber
    '                                .ClaimStatus = LookupListNew.GetClaimStatusFromCode(ElitaPlusIdentity.Current.ActiveUser.LanguageId, oClaimBase.StatusCode)
    '                                .ClaimStatusCss = If(oClaimBase.Status = BasicClaimStatus.Active, CssStateNames.StateActive, CssStateNames.StateClosed)
    '                                .DateOfLoss = oClaimBase.LossDate.Value.ToString(Me.DateFormat)
    '                                .TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, oClaimBase.CertificateItem.RiskTypeId)
    '                            End With
    '                            OnProtectionInfoChanged(protectionInfo)
    '                        End If
    '                    End If
    '                End If
    '            Catch ex As FaultException(Of PolicyCancelRequestDeniedFault)
    '                Dim dsCase As DataSet = CaseBase.LoadCaseByCaseNumber(wsRequest.CaseNumber, wsRequest.CompanyCode)
    '                Dim caseId As Guid
    '                If (Not dsCase Is Nothing AndAlso dsCase.Tables.Count > 0 AndAlso dsCase.Tables(0).Rows.Count > 0) Then
    '                    caseId = GuidControl.ByteArrayToGuid(dsCase.Tables(0).Rows(0)("case_id"))
    '                End If
    '                OnCallPageEvent(CaseDetailsForm.URL, caseId)

    '            Catch ex As FaultException
    '                LocalThrowWsFaultExceptions(ex)
    '                Exit Sub
    '            End Try

    '            If TypeOf questionSubmitobj Is CallerAuthenticationResponse Then
    '                Dim oCertificate As Certificate = New Certificate(CertificateId)
    '                Dim protectionInfo As New ProtectionInfo
    '                With protectionInfo
    '                    .CustomerName = oCertificate.CustomerName
    '                    .ShowCustomerAddress = True
    '                    .CustomerAddress = getCustomerAddress(oCertificate.AddressChild)
    '                End With
    '                OnProtectionInfoChanged(protectionInfo)
    '            End If

    '            OnDisplayViewEvent()
    '        End If
    '    Catch ex As ThreadAbortException
    '    Catch ex As Exception
    '        LocalHandleErrors(ex, HostMessageController)
    '    End Try
    'End Sub
    'Private Function getCustomerAddress(ByVal custAddress As BusinessObjectsNew.Address) As String
    '    Dim sbCustmerAddress As New StringBuilder
    '    If Not custAddress Is Nothing Then
    '        If Not String.IsNullOrWhiteSpace(custAddress.Address1) Then
    '            sbCustmerAddress.Append(custAddress.Address1).Append(", ")
    '        End If
    '        If Not String.IsNullOrWhiteSpace(custAddress.Address2) Then
    '            sbCustmerAddress.Append(custAddress.Address2).Append(", ")
    '        End If
    '        If Not String.IsNullOrWhiteSpace(custAddress.Address3) Then
    '            sbCustmerAddress.Append(custAddress.Address3).Append(", ")
    '        End If
    '        If Not String.IsNullOrWhiteSpace(custAddress.City) Then
    '            sbCustmerAddress.Append(custAddress.City).Append(", ")
    '        End If
    '        If Not String.IsNullOrWhiteSpace(custAddress.PostalCode) Then
    '            sbCustmerAddress.Append(custAddress.PostalCode).Append(", ")
    '        End If
    '        If Not custAddress.RegionId.Equals(Guid.Empty) Then
    '            Dim oRegion As New Assurant.ElitaPlus.BusinessObjectsNew.Region(custAddress.RegionId)
    '            If Not String.IsNullOrWhiteSpace(oRegion.Description) Then
    '                sbCustmerAddress.Append(oRegion.Description).Append(", ")
    '            End If
    '        End If
    '        If Not custAddress.CountryId.Equals(Guid.Empty) Then
    '            Dim country As String = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COUNTRIES, custAddress.CountryId)
    '            If Not String.IsNullOrWhiteSpace(country) Then
    '                sbCustmerAddress.Append(country).Append(", ")
    '            End If
    '        End If

    '    End If

    '    If Not sbCustmerAddress Is Nothing AndAlso sbCustmerAddress.Length > 0 Then
    '        sbCustmerAddress.Length = sbCustmerAddress.Length - 2
    '    End If

    '    Return sbCustmerAddress.ToString()

    'End Function

    'Protected Sub BtnQuestSave(sender As Object, e As EventArgs) Handles btn_Quest_SaveExit.Click

    '    Dim questionSubmitObj
    '    Dim row As GridViewRow

    '    Try
    '        If SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
    '            If TypeOf SubmitWsBaseClaimRecordingResponse Is QuestionResponse Then
    '                questionSubmitObj = DirectCast(SubmitWsBaseClaimRecordingResponse, QuestionResponse)
    '            ElseIf TypeOf SubmitWsBaseClaimRecordingResponse Is CallerAuthenticationResponse Then
    '                questionSubmitObj = DirectCast(SubmitWsBaseClaimRecordingResponse, CallerAuthenticationResponse)
    '            Else
    '                Exit Sub
    '            End If

    '            For Each row In GridQuestions.Rows

    '                Dim answerType As String = CType(row.FindControl("lblAnswerType"), Label).Text
    '                Dim code As String = CType(row.FindControl("lblCode"), Label).Text
    '                Dim questionObject

    '                ' replaced questionSubmitObj.GetType() Is GetType(QuestionResponse)
    '                If TypeOf questionSubmitObj Is QuestionResponse Then
    '                    questionObject = DirectCast(questionSubmitObj, QuestionResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
    '                ElseIf TypeOf questionSubmitObj Is CallerAuthenticationResponse Then
    '                    questionObject = DirectCast(questionSubmitObj, CallerAuthenticationResponse).Questions.FirstOrDefault(Function(x) x.Code = code)
    '                End If

    '                Select Case answerType.Trim().ToUpper()
    '                    Case AnswerTypes.ChoiceAnswer
    '                        Dim answer As String = CType(row.FindControl("rblChoice"), RadioButtonList).SelectedValue
    '                        Dim choiceAnswer As ChoiceAnswer = questionObject.Answer
    '                        choiceAnswer.Answer = choiceAnswer.Answers.FirstOrDefault(Function(y) y.Code = answer)
    '                    Case AnswerTypes.ListOfValues
    '                        Dim answer As String = CType(row.FindControl("ddlList"), DropDownList).SelectedValue
    '                        Dim listOfValues As ListOfValuesAnswer = questionObject.Answer
    '                        listOfValues.Answer = listOfValues.Answers.FirstOrDefault(Function(y) y.Code = answer)
    '                    Case AnswerTypes.NumberAnswer, AnswerTypes.TextAnswer
    '                        Dim answer As String = CType(row.FindControl("txtNo"), TextBox).Text
    '                        If answerType.Trim().ToUpper().Equals("NUMBER") Then
    '                            Dim numberValue As NumberAnswer = New NumberAnswer()
    '                            numberValue.Answer = Convert.ToDecimal(answer)
    '                            questionObject.Answer = numberValue
    '                        Else
    '                            Dim txtValue As TextAnswer = New TextAnswer()
    '                            txtValue.Answer = answer
    '                            questionObject.Answer = txtValue
    '                        End If
    '                    Case AnswerTypes.BooleanAnswer, AnswerTypes.LegalConsent
    '                        Dim answer As Boolean = CType(row.FindControl("chkCheck"), CheckBox).Checked
    '                        If answerType.Trim().ToUpper().Equals("BOOLEAN") Then
    '                            questionObject.Answer = New BooleanAnswer() With {.Answer = answer}
    '                        Else
    '                            questionObject.Answer = New LegalConsentAnswer() With {.Answer = answer}
    '                        End If
    '                    Case AnswerTypes.DateAnswer
    '                        Dim answer As String = CType(row.FindControl("txtDate"), TextBox).Text
    '                        If Not String.IsNullOrEmpty(answer) Then
    '                            Dim dateAnswer As DateAnswer = New DateAnswer()
    '                            'Fix for Japan date control-------------------------------
    '                            Dim formatProvider = LocalizationMgr.CurrentFormatProvider
    '                            If formatProvider.Name.Equals("ja-JP") Then
    '                                Dim dateFragments() As String = answer.Split("-")
    '                                dateAnswer.Answer = New DateTime(Integer.Parse(dateFragments(2)), Integer.Parse(dateFragments(1)), Integer.Parse(dateFragments(0))).Date
    '                            Else
    '                                REM dateAnswer.Answer = Convert.ToDateTime(answer, formatProvider)
    '                                Dim resultDate As DateTime
    '                                If DateTime.TryParse(answer, formatProvider, DateTimeStyles.None, resultDate) Then
    '                                    dateAnswer.Answer = resultDate
    '                                    questionObject.Answer = dateAnswer
    '                                End If
    '                                '--------------------------------------------------------
    '                            End If
    '                        End If
    '                End Select
    '            Next

    '            Dim wsRequest
    '            If SubmitWsBaseClaimRecordingResponse IsNot Nothing Then
    '                If TypeOf SubmitWsBaseClaimRecordingResponse Is QuestionResponse Then
    '                    wsRequest = New QuestionRequest()
    '                    wsRequest.QuestionSetCode = questionSubmitObj.QuestionSetCode
    '                    wsRequest.Version = questionSubmitObj.Version
    '                ElseIf TypeOf SubmitWsBaseClaimRecordingResponse Is CallerAuthenticationResponse Then
    '                    wsRequest = New CallerAuthenticationRequest()
    '                End If
    '            End If

    '            wsRequest.CaseNumber = questionSubmitObj.CaseNumber
    '            wsRequest.CompanyCode = questionSubmitObj.CompanyCode
    '            wsRequest.InteractionNumber = questionSubmitObj.InteractionNumber
    '            wsRequest.Questions = questionSubmitObj.Questions

    '            Try
    '                WcfClientHelper.Execute(Of ClaimRecordingServiceClient, IClaimRecordingService)(
    '                                            GetClient(),
    '                                            New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
    '                                            Sub(ByVal c As ClaimRecordingServiceClient)
    '                                                c.Save(wsRequest)
    '                                            End Sub)
    '            Catch ex As FaultException
    '                LocalThrowWsFaultExceptions(ex)
    '                Exit Sub
    '            End Try
    '            OnReturnBackEvent()
    '        End If
    '    Catch ex As ThreadAbortException
    '    Catch ex As Exception
    '        LocalHandleErrors(ex, HostMessageController)
    '    End Try
    'End Sub
#End Region
#End Region

#Region "Forward to Host Post"
    Private Sub LocalHandleErrors(ByVal exc As Exception, ByVal ErrorCtrl As ErrorController)

    End Sub
    Private Sub LocalThrowWsFaultExceptions(fex As FaultException)

    End Sub
    Private Function GetClient() As ClaimRecordingServiceClient
        Dim client = New ClaimRecordingServiceClient(ServiceEndPointNameSetting, ConfigurationManager.AppSettings(ServiceUrlSetting))
        client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserNameSetting)
        client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(PasswordSetting)
        Return client
    End Function


#End Region
End Class