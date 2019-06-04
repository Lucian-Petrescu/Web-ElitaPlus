Option Infer On
Option Explicit On

Imports System.Threading
Imports System.Web.Script.Services
Imports System.Web.Services
Imports Assurant.ElitaPlus.ElitaPlusWebApp.QuestionService
Imports Assurant.ElitaPlus.Security
Imports Newtonsoft.Json

Namespace GlobalConfig
    Partial Class QuestionSetForm
        Inherits ElitaPlusSearchPage

        Public Class Response
            Public Property QuestionSet As QuestionSet
        End Class

        ''' <summary>
        ''' Gets New Instance of Question Service Client with Crdentials Configured from Web Passwords
        ''' </summary>
        ''' <returns>Instance of <see cref="QuestionService.QuestionServiceClient"/></returns>
        Private Shared Function GetClient() As QuestionServiceClient
            Dim oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_QUESTION_SERVICE), False)
            Dim client = New QuestionServiceClient("CustomBinding_IQuestionService", oWebPasswd.Url)
            client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            client.ClientCredentials.UserName.Password = oWebPasswd.Password
            Return client
        End Function

        Private Function GetQuestionSet() As QuestionSet
            ' If QuestionSet object is already created/read then just return existing object
            If (Not State.QuestionSet Is Nothing) Then
                Return State.QuestionSet
            End If

            ' Create Blank Question Set when adding new Object
            If (String.IsNullOrWhiteSpace(State.QuestionSetCode)) Then

                Dim defaultQuestion = New Question() With {.Mandatory = False, .AnswerType = AnswerTypes.Text, .PreConditions = New QuestionPreCondition() {}, .Answers = New Answer() {}, .Channels = New QuestionChannel() {}}
                Dim defaultQuestionSetVersion = New QuestionSetVersion() With {.VersionNumber = 1, .Questions = {defaultQuestion}}
                State.QuestionSet = New QuestionSet() With { .Versions = New QuestionSetVersion(){defaultQuestionSetVersion }}
                Return State.QuestionSet

            End If

            ' Read Question Set from Backing Store
            Dim client = GetClient()
            Dim cultureCode = "en-US" ' Thread.CurrentPrincipal.GetCultureCode()
            State.QuestionSet = client.GetQuestionSet(State.QuestionSetCode, cultureCode)
            Return State.QuestionSet
        End Function

#Region "Constants"
        Public Const Url As String = "~/GlobalConfig/QuestionSetForm.aspx"
#End Region

#Region "Page State"
        Class MyState
            Public QuestionSet As QuestionSet
            Public QuestionSetCode As String
        End Class

        Protected Sub New()
            MyBase.New(New MyState)
        End Sub

        Private Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal parameter As Object) Handles MyBase.PageCall
            Try
                If parameter Is Nothing Then
                    State.QuestionSetCode = String.Empty
                Else
                    State.QuestionSetCode = DirectCast(parameter, String).Trim().ToUpperInvariant()
                End If
                State.QuestionSet = Nothing
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Page Events"

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            MasterPage.MessageController.Clear_Hide()
            Try
                If Not IsPostBack Then

                    MasterPage.MessageController.Clear()
                    UpdateBreadCrum()

                    Dim questionSet = GetQuestionSet()
                    Dim responseObject = New Response() With {.QuestionSet = questionSet}
                    Dim jsonString = JsonConvert.SerializeObject(responseObject)

                    '''TODO: Get Additional Lists like Answer Types, Channels, YesNo
                    Dim dropdowns = "'Channels':[{'Code':'CSR','Description':'Customer Service Representative'},{'Code':'CP','Description':'Customer Portal'}],'AnswerTypes':[{'Code':'2','Description':'Text'},{'Code':'1','Description':'Date'},{'Code':'4','Description':'Number'},{'Code':'0','Description':'Choice'},{'Code':'3','Description':'Value List'},{'Code':'5','Description':'Boolean'},{'Code':'6','Description':'Content'}]".Replace("'", """")

                    jsonString = "{" + dropdowns + "," + jsonString.Substring(1)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascriptStartup", "Elita.QuestionSet.init( " & jsonString & ")", True)


                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum()
            Dim pageTitle = TranslationBase.TranslateLabelOrMessage("QUESTION_SET")
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("ADMIN") & ElitaBase.Sperator & pageTitle
            MasterPage.PageTitle = pageTitle
            MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub

#End Region

        Private Sub BackButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BackButton.Click
            Try
                ReturnToCallingPage(New PageReturnType(Of Object)(DetailPageCommand.Back, Nothing, True))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        <WebMethod(), ScriptMethod()> _
        Public Shared Function GetAutoCompleteOptionsUIProgCodes(ByVal uiProgCode As String) As String()

        End Function

        <WebMethod(), ScriptMethod()> _
        Public Shared Function GetAutoCompleteOptionsTranslations(ByVal translationText As String) As String()

        End Function

        <WebMethod(EnableSession := True), ScriptMethod()> _
        Public Shared Function SaveQuestionSet(ByVal questionSet As String) As String()
            Dim questionSetObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Of QuestionSet)(questionSet)
            GetClient.SaveQuestionSet(questionSetObject, New Language(ElitaPlusIdentity.Current.ActiveUser.LanguageId ).CultureCode)
            Return Nothing 
        End Function
    End Class
End Namespace