
Namespace DownLoad

Partial Class DownLoadWindowForm
    Inherits DownLoadBase

#Region "Constants"

        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Common/DownLoadWindowForm.aspx"
        'Public Shared REPORTS_UICODE As String = "Reports"
        'Public Const SESSION_PARAMETERS_KEY As String = "REPORTCE_BASE_SESSION_PARAMETERS_KEY"

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try

                If Not Page.IsPostBack Then
                    Dim params As DownLoadBase.DownLoadParams
                    params = CType(Session(Me.SESSION_PARAMETERS_DOWNLOAD_KEY), DownLoadParams)

                    Select Case params.downLoadCode
                        Case DownLoadBase.DownLoadParams.DownLoadTypeCode.FILE
                            ProcessSendFile(params)
                        Case DownLoadBase.DownLoadParams.DownLoadTypeCode.GRID
                            ProcessGrid(params)
                    End Select
                    Session.Remove(Me.SESSION_PARAMETERS_DOWNLOAD_KEY)
                End If
            Catch exT As System.Threading.ThreadAbortException
                'Catch ex As Exception

            End Try
        End Sub

#End Region

#End Region

#Region "Process Request"

        Private Sub ProcessSendFile(ByVal params As DownLoadBase.DownLoadParams)
            Dim sourceFileName As String = params.fileName
            SendFile(sourceFileName, params.DeleteFileAfterDownload)
        End Sub

        Private Sub ProcessGrid(ByVal params As DownLoadBase.DownLoadParams)
            Dim oDataGrid As New DataGrid
            Dim sourceFileName As String = params.fileName
            Dim ds As DataSet = params.data

            CreateExcelHeader(sourceFileName)
            oDataGrid.DataSource = ds
            oDataGrid.DataBind()
            Me.Controls.Add(oDataGrid)
        End Sub

#End Region

    End Class

End Namespace


