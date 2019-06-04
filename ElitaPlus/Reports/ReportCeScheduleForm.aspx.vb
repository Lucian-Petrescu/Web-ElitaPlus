Namespace Reports

    Partial Public Class ReportCeScheduleForm
        Inherits ElitaPlusPage

#Region "Constants"
        Public Shared URL As String = "~/Reports/ReportCeScheduleForm.aspx"
        Public Const PAGETITLE As String = "SCHEDULE"
        Public Const PAGETAB As String = "REPORTS"

        ' Property Name
        'Public Const CODE_PROPERTY As String = "Code"
        'Public Const DESCRIPTION_PROPERTY As String = "Description"
        'Public Const HOST_PROPERTY As String = "Host"
        'Public Const PORT_PROPERTY As String = "Port"
        'Public Const USERNAME_PROPERTY As String = "UserName"
        'Public Const PASSWORD_PROPERTY As String = "Password"
        'Public Const ACCOUNT_PROPERTY As String = "Account"
        'Public Const DIRECTORY_PROPERTY As String = "Directory"
#End Region

#Region "Handlers"

#Region "Handler-Init"



        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()
                '   ClearLabelsErrSign()
                '   RecoverEncryptedValue()
                If Not Page.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
                    '                                                    Me.MSG_TYPE_CONFIRM, True)
                    'If Me.State.MyBo Is Nothing Then
                    '    Me.State.MyBo = New FtpSite
                    'End If
                    'PopulateAll()
                    'EnableDisableFields()
                End If

                'BindBoPropertiesToLabels()
                'CheckIfComingFromConfirm()
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try

            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#End Region

    End Class

End Namespace