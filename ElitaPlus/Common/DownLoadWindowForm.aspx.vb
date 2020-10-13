
Imports System.Diagnostics
Imports System.Threading

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
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As Object

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try

                If Not Page.IsPostBack Then
                    Dim params As DownLoadParams
                    params = CType(Session(SESSION_PARAMETERS_DOWNLOAD_KEY), DownLoadParams)

                    Select Case params.downLoadCode
                        Case DownLoadParams.DownLoadTypeCode.FILE
                            ProcessSendFile(params)
                        Case DownLoadParams.DownLoadTypeCode.GRID
                            ProcessGrid(params)
                    End Select
                    Session.Remove(SESSION_PARAMETERS_DOWNLOAD_KEY)
                End If
            Catch exT As ThreadAbortException
                'Catch ex As Exception

            End Try
        End Sub

#End Region

#End Region

#Region "Process Request"

        Private Sub ProcessSendFile(params As DownLoadParams)
            Dim sourceFileName As String = params.fileName
            SendFile(sourceFileName, params.DeleteFileAfterDownload)
        End Sub

        Private Sub ProcessGrid(params As DownLoadParams)
            Dim oDataGrid As New DataGrid
            Dim sourceFileName As String = params.fileName
            Dim ds As DataSet = params.data

            CreateExcelHeader(sourceFileName)
            oDataGrid.DataSource = ds
            oDataGrid.DataBind()
            Controls.Add(oDataGrid)
        End Sub

#End Region

    End Class

End Namespace


