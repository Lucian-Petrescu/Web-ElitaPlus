Public Class CertificateExtendedFieldHistory
    Inherits ElitaPlusPage
#Region "Constants"
    Public Const URL As String = "~/Certificates/CertificateExtendedFieldHistory.aspx"
#End Region
#Region "Page State"
    Class MyState

        Public MyCertificate As Certificate
        Public CertExtendedFieldHistDV As Certificate.CertExtendedFieldsDv = Nothing
        Public SelectedCertExtId As Guid = Guid.Empty
        Public FieldName As String = String.Empty
        Public FieldValue As String = String.Empty
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub
    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Me.IsPostBack Then

                PopulateFieldDetails()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CERT_EXT_FIELDS")
                MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CERT_EXT_FIELDS")
                Me.TranslateGridHeader(Me.grdExtendedFieldValueHist)
                PopulateExtendedFieldValueHistory()
                Me.MasterPage.MessageController.Clear()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then

                'Get the id from the parent
                Me.State.SelectedCertExtId = CType(Me.CallingParameters, Guid)
                Me.State.MyCertificate = Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE)
                Me.State.FieldName = Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERT_FIELD_NAME)
                Me.State.FieldValue = Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERT_FIELD_VALUE)

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster, False)
        End Try
    End Sub

    Private Sub PopulateFieldDetails()

        Me.FieldName.Text = Me.State.FieldName
        Me.FieldValue.Text = Me.State.FieldValue
    End Sub
    Public Sub PopulateExtendedFieldValueHistory()
        Me.State.CertExtendedFieldHistDV = Me.State.MyCertificate.GetCertExtendedFieldHistory(Me.State.MyCertificate.Id, Me.State.SelectedCertExtId)
        Me.grdExtendedFieldValueHist.AutoGenerateColumns = False
        Me.grdExtendedFieldValueHist.DataSource = Me.State.CertExtendedFieldHistDV
        Me.grdExtendedFieldValueHist.DataBind()
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

End Class