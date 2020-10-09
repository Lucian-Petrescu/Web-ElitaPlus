Imports System.Text
Imports System.ComponentModel
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.IO
Namespace Interfaces
    Public Class FileProcessedController
        Inherits System.Web.UI.UserControl

#Region "Constants"
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_SELECT_COMPANY_GROUP As String = "SELECT_COMPANY_GROUP"
        Private Const PORT As Integer = 21
        Private Const DEALERLOADFORM_FORM001 As String = "DEALERLOADFORM_FORM001"

        Private Const FILE_VARIABLE_NAME As String = "moFileController_"

        Private Const SP_VALIDATE As Integer = 0
        Private Const SP_PROCESS As Integer = 1
        Private Const SP_DELETE As Integer = 2

        Public Const SESSION_LOCALSTATE_KEY As String = "FILE_PROCESSEDCONTROLLER_SESSION_LOCALSTATE_KEY"

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_SELECT_IDX As Integer = 1
        Public Const GRID_COL_FILE_PROCESSED_ID_IDX As Integer = 2
        Public Const GRID_COL_FILENAME_IDX As Integer = 3
        Public Const GRID_COL_RECEIVED_IDX As Integer = 4
        Public Const GRID_COL_COUNTED_IDX As Integer = 5
        Public Const GRID_COL_REJECTED_IDX As Integer = 6
        Public Const GRID_COL_VALIDATED_IDX As Integer = 7
        Public Const GRID_COL_BYPASSED_IDX As Integer = 8
        Public Const GRID_COL_LOADED_IDX As Integer = 9
        Public Const GRID_COL_LAYOUT_IDX As Integer = 10
#End Region

#Region "Variables"
        Private moState As MyState
        Private mIsLoaded As Boolean = False
        Private bAllowCompanyGroup As Boolean = True
        Private moRejectReportFileName As String = String.Empty
        Private moProcessedReportFileName As String = String.Empty
        Private mReferenceCaption As String = String.Empty
        Private mFileNameCaption As String = String.Empty
        Private mFileType As FileProcessedData.FileTypeCode = FileProcessedData.FileTypeCode.BestReplacement
        Private ErrorCtrl As ErrorController '' Points to Error Controller on Main From.
        Private IsReturningFromChild As Boolean = False
        Private mReturnType As ReturnType = Nothing
#End Region

#Region "Properties"
        Protected ReadOnly Property TheState() As MyState
            Get
                Try
                    If moState Is Nothing Then
                        moState = CType(Session(SESSION_LOCALSTATE_KEY), MyState)
                    End If
                    Return moState
                Catch ex As Exception
                    'When we are in design mode there is no session object
                    Return Nothing
                End Try
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        <Category("Behavior"), DefaultValue(True)>
        Public Property ShowProcessedReport As Boolean
            Get
                Return BtnProcessedExport.Visible
            End Get
            Set(value As Boolean)
                BtnProcessedExport.Visible = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(True)>
        Public Property ShowRejectReport As Boolean
            Get
                Return BtnRejectReport.Visible
            End Get
            Set(value As Boolean)
                BtnRejectReport.Visible = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(FileProcessedData.FileTypeCode.BestReplacement)>
        Public Property FileType As FileProcessedData.FileTypeCode
            Get
                Return mFileType
            End Get
            Set(value As FileProcessedData.FileTypeCode)
                mFileType = value
            End Set
        End Property

        <Category("Appearance"), DefaultValue("")>
        Public Property FileNameCaption As String
            Get
                Return mFileNameCaption
            End Get
            Set(value As String)
                mFileNameCaption = value
            End Set
        End Property

        <Category("Appearance"), DefaultValue("")>
        Public Property ReferenceCaption As String
            Get
                Return mReferenceCaption
            End Get
            Set(value As String)
                mReferenceCaption = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property RejectReportFileName As String
            Get
                Return moRejectReportFileName
            End Get
            Set(value As String)
                moRejectReportFileName = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ProcessedReportFileName As String
            Get
                Return moProcessedReportFileName
            End Get
            Set(value As String)
                moProcessedReportFileName = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowComapnyGroup() As Boolean
            Get
                Return moCompanyGroup.Visible
            End Get
            Set(value As Boolean)
                moCompanyGroup.Visible = value
                If mIsLoaded Then PopulateCompanyGroup(True, False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowComapny() As Boolean
            Get
                Return moCompany.Visible
            End Get
            Set(value As Boolean)
                moCompany.Visible = value
                If mIsLoaded Then PopulateCompany(True, False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowDealer() As Boolean
            Get
                Return moDealer.Visible
            End Get
            Set(value As Boolean)
                moDealer.Visible = value
                If mIsLoaded Then PopulateDealer(True, False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowReference() As Boolean
            Get
                Return moReference.Visible()
            End Get
            Set(value As Boolean)
                moReference.Visible = value
                If mIsLoaded Then PopulateReference(False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property AllowCompanyGroup() As Boolean
            Get
                Return bAllowCompanyGroup
            End Get
            Set(value As Boolean)
                bAllowCompanyGroup = value
                If mIsLoaded Then PopulateCompanyGroup(True, False)
            End Set
        End Property

        Public ReadOnly Property CompanyGroupId As Guid
            Get
                If (ShowComapnyGroup AndAlso Not moCompanyGroup.SelectedGuid = Guid.Empty) Then
                    Return moCompanyGroup.SelectedGuid
                ElseIf Not ShowComapnyGroup Then
                    Return ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property CompanyId As Guid
            Get
                If (ShowComapny) Then
                    If (moCompany.SelectedGuid = Guid.Empty) Then
                        Return Nothing
                    Else
                        Return moCompany.SelectedGuid
                    End If
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property DealerId As Guid
            Get
                If (ShowDealer) Then
                    If (moDealer.SelectedGuid = Guid.Empty) Then
                        Return Nothing
                    Else
                        Return moDealer.SelectedGuid
                    End If
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Property ReferenceId As Guid
            Get
                If (ShowReference) Then
                    If (moReference.SelectedGuid = Guid.Empty) Then
                        Return Nothing
                    Else
                        Return moReference.SelectedGuid
                    End If
                Else
                    Return Nothing
                End If
            End Get
            Set(value As Guid)

            End Set
        End Property

        Public ReadOnly Property CompanyGroupCode As String
            Get
                If (Not CompanyGroupId = Guid.Empty) Then
                    Return moCompanyGroup.SelectedCode
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property CompanyGroupDescription As String
            Get
                If (Not CompanyGroupId = Guid.Empty) Then
                    Return moCompanyGroup.SelectedDesc
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property CompanyCode As String
            Get
                If (Not CompanyId = Guid.Empty) Then
                    Return moCompany.SelectedCode
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property CompanyDescription As String
            Get
                If (Not CompanyId = Guid.Empty) Then
                    Return moCompany.SelectedDesc
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property DealerCode As String
            Get
                If (Not DealerId = Guid.Empty) Then
                    Return moDealer.SelectedCode
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property DealerDescription As String
            Get
                If (Not DealerId = Guid.Empty) Then
                    Return moDealer.SelectedDesc
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property ReferenceCode As String
            Get
                If (Not ReferenceId = Guid.Empty) Then
                    Return moReference.SelectedCode
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property ReferenceDescription As String
            Get
                If (Not ReferenceId = Guid.Empty) Then
                    Return moReference.SelectedDesc
                Else
                    Return Nothing
                End If
            End Get
        End Property
#End Region

#Region "Nested Classes"
        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public FileProcessedId As Guid

            Public CompanyGroupId As Guid
            Public CompanyGroupCode As String
            Public CompanyGroupDescription As String

            Public CompanyId As Guid
            Public CompanyCode As String
            Public CompanyDescription As String

            Public DealerId As Guid
            Public DealerCode As String
            Public DealerDescription As String

            Public ReferenceId As Guid
            Public ReferenceCode As String
            Public ReferenceDescription As String

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, selectedFileProcessedId As Guid,
                           oFileProcessedController As FileProcessedController)
                LastOperation = LastOp
                FileProcessedId = selectedFileProcessedId
                With oFileProcessedController
                    CompanyGroupId = .CompanyGroupId
                    CompanyGroupCode = .CompanyGroupCode
                    CompanyGroupDescription = .CompanyGroupDescription
                    CompanyId = .CompanyId
                    CompanyCode = .CompanyCode
                    CompanyDescription = .CompanyDescription
                    DealerId = .DealerId
                    DealerCode = .DealerCode
                    DealerDescription = .DealerDescription
                    ReferenceId = .ReferenceId
                    ReferenceCode = .ReferenceCode
                    ReferenceDescription = .ReferenceDescription
                End With
            End Sub
        End Class

        Public Class MyState
            Public DataView As DataView
            Public mSelectedFileProcessedId As Guid = Guid.Empty
            Public Property SelectedFileProcessedId As Guid
                Get
                    Return mSelectedFileProcessedId
                End Get
                Set(value As Guid)
                    mSelectedFileProcessedId = value
                End Set
            End Property

            Public PageIndex As Integer

            Public msUrlDetailPage As String
            Public msUrlPrintPage As String = ELPWebConstants.APPLICATION_PATH & "/Reports/ExportFileProcessedForm.aspx"

            Public intStatusId As Guid

            Public Sub New(UrlDetailPage As String)
                msUrlDetailPage = UrlDetailPage
            End Sub
        End Class

        Public Class FileProcessedEventArgs
            Inherits EventArgs

            Private mCompanyGroupId As Guid
            Private mCompanyId As Guid
            Private mDealerId As Guid
            Private mReferenceId As Guid
            Private mCompanyGroupCode As String
            Private mCompanyCode As String
            Private mDealerCode As String
            Private mReferenceCode As String

            Public Sub New()
                CompanyGroupId = Nothing
                CompanyGroupCode = Nothing
                CompanyId = Nothing
                CompanyCode = Nothing
                DealerId = Nothing
                DealerCode = Nothing
                ReferenceId = Nothing
                ReferenceCode = Nothing
            End Sub

            Friend Sub New(pObject As FileProcessedController)

                Me.New()

                If (pObject IsNot Nothing) Then
                    With pObject
                        CompanyGroupId = .CompanyGroupId
                        CompanyGroupCode = .CompanyGroupCode
                        CompanyId = .CompanyId
                        CompanyCode = .CompanyCode
                        DealerId = .DealerId
                        DealerCode = .DealerCode
                        ReferenceId = .ReferenceId
                        ReferenceCode = .ReferenceCode
                    End With
                End If
            End Sub

            Public Property CompanyGroupId() As Guid
                Get
                    Return mCompanyGroupId
                End Get
                Set(value As Guid)
                    mCompanyGroupId = value
                End Set
            End Property

            Public Property CompanyId() As Guid
                Get
                    Return mCompanyId
                End Get
                Set(value As Guid)
                    mCompanyId = value
                End Set
            End Property

            Public Property DealerId() As Guid
                Get
                    Return mDealerId
                End Get
                Set(value As Guid)
                    mDealerId = value
                End Set
            End Property

            Public Property ReferenceId() As Guid
                Get
                    Return mReferenceId
                End Get
                Set(value As Guid)
                    mReferenceId = value
                End Set
            End Property

            Public Property CompanyGroupCode() As String
                Get
                    Return mCompanyGroupCode
                End Get
                Set(value As String)
                    mCompanyGroupCode = value
                End Set
            End Property

            Public Property CompanyCode() As String
                Get
                    Return mCompanyCode
                End Get
                Set(value As String)
                    mCompanyCode = value
                End Set
            End Property

            Public Property DealerCode() As String
                Get
                    Return mDealerCode
                End Get
                Set(value As String)
                    mDealerCode = value
                End Set
            End Property

            Public Property ReferenceCode() As String
                Get
                    Return mReferenceCode
                End Get
                Set(value As String)
                    mReferenceCode = value
                End Set
            End Property
        End Class

        Public Class PopulateReferenceEventArgs
            Inherits FileProcessedEventArgs

            Private mReferenceDV As DataView

            Public Sub New()
                Me.New(Nothing)
            End Sub

            Friend Sub New(pObject As FileProcessedController)
                MyBase.New(pObject)
                mReferenceDV = Nothing
            End Sub

            Public Property ReferenceDV() As DataView
                Get
                    Return mReferenceDV
                End Get
                Set(value As DataView)
                    mReferenceDV = value
                End Set
            End Property
        End Class

        Public Class SetExpectedFileNameEventArgs
            Inherits FileProcessedEventArgs

            Private mFileName As String

            Public Sub New()
                Me.New(Nothing)
            End Sub

            Friend Sub New(pObject As FileProcessedController)
                MyBase.New(pObject)
                mFileName = Nothing
            End Sub

            Public Property FileName() As String
                Get
                    Return mFileName
                End Get
                Set(value As String)
                    mFileName = value
                End Set
            End Property

        End Class

        Public Class ExecuteActionEventArgs
            Inherits EventArgs

            Private mFileProcessedId As Guid
            Private mInterfaceStatusId As Guid

            Public Sub New()
                Me.New(Nothing)
            End Sub

            Friend Sub New(pFileProcessedId As Guid)
                mFileProcessedId = pFileProcessedId
            End Sub

            Public ReadOnly Property FileProcessedId() As Guid
                Get
                    Return mFileProcessedId
                End Get
            End Property

            Public Property InterfaceStatusId As Guid
                Get
                    Return mInterfaceStatusId
                End Get
                Set(value As Guid)
                    mInterfaceStatusId = value
                End Set
            End Property
        End Class

        Public Class FileProcessedDataEventArgs
            Inherits EventArgs

            Private mFileProcessedData As FileProcessedData

            Public Sub New(pFileProcessedData As FileProcessedData)
                mFileProcessedData = pFileProcessedData
            End Sub

            Public ReadOnly Property FileProcessedData() As FileProcessedData
                Get
                    Return mFileProcessedData
                End Get
            End Property
        End Class
#End Region

#Region "Events and Delegates"
        Public Delegate Sub PopulateReferenceEventHandler(sender As Object, e As PopulateReferenceEventArgs)
        Public Delegate Sub SetExpectedFileNameEventHandler(sender As Object, e As SetExpectedFileNameEventArgs)
        Public Delegate Sub ExecuteActionEventHandler(sender As Object, e As ExecuteActionEventArgs)
        Public Delegate Sub FileProcessedEventHandler(sender As Object, e As FileProcessedDataEventArgs)
        Public Event PopulateReferenceDataView As PopulateReferenceEventHandler
        Public Event SetExpectedFileName As SetExpectedFileNameEventHandler
        Public Event OnValidate As ExecuteActionEventHandler
        Public Event OnProcess As ExecuteActionEventHandler
        Public Event OnDelete As ExecuteActionEventHandler
        Public Event BeforeGetDataView As FileProcessedEventHandler

#End Region

#Region "Methods"
        Private Sub SetButtonCaptions()
            btnCopyFile_WRITE.Text = TranslationBase.TranslateLabelOrMessage("COPY") & " " & TranslationBase.TranslateLabelOrMessage(FileNameCaption)
            BtnDeleteFile_WRITE.Text = TranslationBase.TranslateLabelOrMessage("DELETE") & " " & TranslationBase.TranslateLabelOrMessage(FileNameCaption)
        End Sub

        Private Sub PopulateCompanyGroup(cascade As Boolean, clearSelectedValue As Boolean)
            Dim companyGroupDV As DataView
            Dim assignedCompanyGroupDV As DataView
            Dim selectedValue As Guid = Guid.Empty
            Dim tempGuid As Guid
            Dim tempGuid1 As Guid
            Dim removeCompanyGroup As Boolean
            If (ShowComapnyGroup) Then
                companyGroupDV = LookupListNew.GetCompanyGroupLookupList()
                companyGroupDV = companyGroupDV.ToTable().Copy().DefaultView
                If (AllowCompanyGroup) Then
                    assignedCompanyGroupDV = BusinessObjectsNew.User.GetAvailableCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.Id)
                    For i As Integer = (companyGroupDV.Count - 1) To 0 Step -1
                        tempGuid = New Guid(CType(companyGroupDV(i)("id"), Byte()))
                        removeCompanyGroup = True
                        For j As Integer = 0 To (assignedCompanyGroupDV.Count - 1)
                            tempGuid1 = New Guid(CType(assignedCompanyGroupDV(j)("company_group_id"), Byte()))
                            If (tempGuid = tempGuid1) Then
                                removeCompanyGroup = False
                                Exit For
                            End If
                        Next
                        If (removeCompanyGroup) Then companyGroupDV.Delete(i)
                    Next
                Else
                    For i As Integer = (companyGroupDV.Count - 1) To 0 Step -1
                        tempGuid = New Guid(CType(companyGroupDV(i)("id"), Byte()))
                        If (tempGuid <> ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) Then
                            companyGroupDV.Delete(i)
                        End If
                    Next
                End If
                If Not (moCompanyGroup.NothingSelected) Then
                    selectedValue = moCompanyGroup.SelectedGuid
                End If
                moCompanyGroup.NothingSelected = True
                moCompanyGroup.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, companyGroupDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY_GROUP), True, True)
                If (Not clearSelectedValue AndAlso selectedValue <> Guid.Empty) Then
                    moCompanyGroup.SelectedGuid = selectedValue
                End If
                clearSelectedValue = clearSelectedValue OrElse moCompanyGroup.SelectedGuid <> selectedValue
            Else
                moCompanyGroup.ClearMultipleDrop()
            End If
            If cascade Then PopulateCompany(cascade, clearSelectedValue)
        End Sub

        Private Sub PopulateCompany(cascade As Boolean, clearSelectedValue As Boolean)
            Dim selectedValue As Guid
            Dim companyDV As DataView = Nothing
            If (ShowComapny) Then
                If Not (moCompany.NothingSelected) Then
                    selectedValue = moCompany.SelectedGuid
                End If
                companyDV = ElitaPlusIdentity.Current.ActiveUser.LoadUserCompanyAssigned(CompanyGroupId, ElitaPlusIdentity.Current.ActiveUser.Id)
                moCompany.NothingSelected = True
                moCompany.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, companyDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, True)
                If (Not clearSelectedValue AndAlso selectedValue <> Guid.Empty) Then
                    moCompany.SelectedGuid = selectedValue
                End If
                clearSelectedValue = clearSelectedValue OrElse moCompany.SelectedGuid <> selectedValue
            Else
                moCompany.ClearMultipleDrop()
            End If
            If cascade Then PopulateDealer(cascade, clearSelectedValue)
        End Sub

        Private Sub PopulateDealer(cascade As Boolean, clearSelectedValue As Boolean)
            Dim selectedValue As Guid
            Dim dealerDV As DataView = Nothing
            If (ShowDealer) Then
                If Not (moDealer.NothingSelected) Then
                    selectedValue = moDealer.SelectedGuid
                End If
                dealerDV = LookupListNew.GetDealerLookupList(CompanyId)
                moDealer.NothingSelected = True
                moDealer.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, dealerDV, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True)
                If (Not clearSelectedValue AndAlso selectedValue <> Guid.Empty) Then
                    moDealer.SelectedGuid = selectedValue
                End If
                clearSelectedValue = clearSelectedValue OrElse moDealer.SelectedGuid <> selectedValue
            Else
                moDealer.ClearMultipleDrop()
            End If
            If cascade Then PopulateReference(clearSelectedValue)
        End Sub

        Private Sub PopulateReference(clearSelectedValue As Boolean)
            Try
                Dim selectedValue As Guid
                Dim e As PopulateReferenceEventArgs
                If (ShowReference) Then
                    If Not (moReference.NothingSelected) Then
                        selectedValue = moDealer.SelectedGuid
                    End If
                    e = New PopulateReferenceEventArgs(Me)
                    RaiseEvent PopulateReferenceDataView(Me, e)
                    moReference.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, e.ReferenceDV, "* " + TranslationBase.TranslateLabelOrMessage(ReferenceCaption), True, True)
                    If (Not clearSelectedValue AndAlso selectedValue <> Guid.Empty) Then
                        moReference.SelectedGuid = selectedValue
                    End If
                    clearSelectedValue = clearSelectedValue OrElse moReference.SelectedGuid <> selectedValue
                Else
                    moReference.ClearMultipleDrop()
                End If
                PopulateInterface()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Function GetDataView() As DataView
            Dim oFileData As FileProcessedData = New FileProcessedData
            Dim oDataView As DataView

            With oFileData
                .CompanyGroupId = CompanyGroupId
                .CompanyId = CompanyId
                .DealerId = DealerId
                .ReferenceId = ReferenceId
                .FileType = FileType
                Dim e As New FileProcessedDataEventArgs(oFileData)
                RaiseEvent BeforeGetDataView(Me, e)
                oDataView = FileProcessed.LoadList(oFileData)
            End With

            Return oDataView
        End Function

        Private Sub SetExpectedFile()
            Dim e As New SetExpectedFileNameEventArgs(Me)
            Dim sDirectory As String
            Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")

            sDirectory = AppConfig.FileClientDirectory
            RaiseEvent SetExpectedFileName(Me, e)
            Dim sbFileName As New StringBuilder
            sbFileName.Append(sDirectory).Append(e.FileName).Append(dateStr).Append(".TXT")
            moExpectedFileLabel_NO_TRANSLATE.Text = sbFileName.ToString()
        End Sub

        Private Sub PopulateGrid(oAction As String)
            Try
                SetExpectedFile()
                TheState.DataView = GetDataView()
                ThePage.BasePopulateGrid(moDataGrid, TheState.DataView, TheState.SelectedFileProcessedId, oAction)
                ThePage.SetPageAndSelectedIndexFromGuid(TheState.DataView, TheState.SelectedFileProcessedId, moDataGrid, TheState.PageIndex)
                EnableDisableEditControl()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Function IsSelectionValid() As Boolean
            If (CompanyGroupId = Guid.Empty) Then Return False
            If (ShowComapny AndAlso CompanyId = Guid.Empty) Then Return False
            If (ShowDealer AndAlso DealerId = Guid.Empty) Then Return False
            If (ShowReference AndAlso ReferenceId = Guid.Empty) Then Return False
            Return True
        End Function

        Public Sub PopulateInterface()
            Try
                ClearAll()
                If IsSelectionValid() Then
                    PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub ClearAll()
            moDataGrid.CurrentPageIndex = ThePage.NO_PAGE_INDEX
            TheState.DataView = Nothing
            moDataGrid.DataSource = Nothing
            moDataGrid.DataBind()
            moExpectedFileLabel_NO_TRANSLATE.Text = String.Empty
            TheState.SelectedFileProcessedId = Guid.Empty

            DisableButtons()
            ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, False, True)
            ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
        End Sub

        Private Sub ClearSelectedFile(oAction As String)
            moDataGrid.SelectedIndex = ThePage.NO_ITEM_SELECTED_INDEX
            DisableButtons()
            TheState.SelectedFileProcessedId = Guid.Empty
            PopulateGrid(oAction)
        End Sub

        Public Sub SetErrorController(oErrorCtrl As ErrorController)
            ErrorCtrl = oErrorCtrl
        End Sub

        Private Sub DisableButtons()
            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnLoad_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnDeleteFile_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
        End Sub

        Private Function UploadFile() As String
            Dim fileName As String
            Dim layoutFileName As String
            Dim fileLen As Integer = FileInput.PostedFile.ContentLength
            If fileLen = 0 Then
                Dim errors() As ValidationError = {New ValidationError(DEALERLOADFORM_FORM001, GetType(FileProcessed), Nothing, Nothing, Nothing)}
                Throw New BOValidationException(errors, GetType(FileProcessed).FullName)
            End If
            fileName = MiscUtil.ReplaceSpaceByUnderscore(FileInput.PostedFile.FileName)

            Dim fileBytes(fileLen - 1) As Byte
            Dim objStream As System.IO.Stream
            objStream = FileInput.PostedFile.InputStream
            objStream.Read(fileBytes, 0, fileLen)

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(fileName)
            layoutFileName = webServerPath & "\" &
                System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
            CreateFolder(webServerPath)
            If FileProcessedDAL.GetFileLayout(FileType) IsNot Nothing Then
                File.WriteAllBytes(webServerFile, fileBytes)
                File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(FileProcessedDAL.GetFileLayout(FileType)))
            Else
                Throw New GUIException("Missing File Layout Code", Assurant.ElitaPlus.Common.ErrorCodes.GUI_MISSING_FILE_LAYOUT_CODE)
            End If

            Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
            '' ''Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
            '' ''                     AppConfig.UnixServer.Password, PORT)
            Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId,
                                 AppConfig.UnixServer.Password)
            Try
                '' ''If (objUnixFTP.Login()) Then
                '' ''    objUnixFTP.UploadFile(webServerFile, False)
                '' ''    objUnixFTP.UploadFile(layoutFileName, False)
                '' ''End If
                objUnixFTP.UploadFile(webServerFile)
                objUnixFTP.UploadFile(layoutFileName)

                Return fileName
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            Finally
                '' ''objUnixFTP.CloseConnection()
            End Try
        End Function

        Private Sub EnableDisableButtons()
            If Not TheState.SelectedFileProcessedId.Equals(Guid.Empty) Then
                Dim oFile As FileProcessed = New FileProcessed(TheState.SelectedFileProcessedId)
                DisableButtons()
                With oFile
                    If .Received.Value = .Counted.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
                    If .Received.Value = .Validated.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)

                    If .Rejected.Value > 0 Then
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, True)
                    Else
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                    End If
                    If .Loaded.Value > 0 Then
                        ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, True)
                    Else
                        ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    End If
                    If .Validated.Value > 0 Then ControlMgr.SetEnableControl(ThePage, BtnLoad_WRITE, True)
                    If .Received.Value = .Loaded.Value Then
                        ControlMgr.SetEnableControl(ThePage, BtnDeleteFile_WRITE, True)
                        ControlMgr.SetEnableControl(ThePage, BtnLoad_WRITE, False)
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    End If

                    If (.Loaded.Value = .Counted.Value) OrElse (.Loaded.Value = 0) Then
                        ControlMgr.SetEnableControl(ThePage, BtnDeleteFile_WRITE, True)
                    End If
                End With
            Else
                Throw New GUIException("You must select a Replacement file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_GROUP_MUST_BE_SELECTED_ERR)

            End If
        End Sub

        Public Sub EnableDisableEditControl()
            Dim i As Integer
            Dim edt As ImageButton

            For i = 0 To (moDataGrid.Items.Count - 1)
                edt = CType(moDataGrid.Items(i).Cells(ThePage.EDIT_COL).FindControl(ThePage.EDIT_CONTROL_NAME), ImageButton)
                If edt IsNot Nothing Then
                    edt.Enabled = (moDataGrid.Items(i).Cells(GRID_COL_REJECTED_IDX).Text.Trim() <> "0")
                End If
            Next
        End Sub

        Private Sub ExecuteSp(oSP As Integer)
            Dim oInterfaceStatusWrk As New InterfaceStatusWrk
            Dim e As New ExecuteActionEventArgs(TheState.SelectedFileProcessedId)
            If Not TheState.SelectedFileProcessedId.Equals(Guid.Empty) Then
                Dim oFileProcessed As New FileProcessed(TheState.SelectedFileProcessedId)
                If oInterfaceStatusWrk.IsfileBeingProcessed(oFileProcessed.FileName) Then
                    Select Case oSP
                        Case SP_VALIDATE
                            RaiseEvent OnValidate(Me, e)
                        Case SP_PROCESS
                            RaiseEvent OnProcess(Me, e)
                        Case SP_DELETE
                            RaiseEvent OnDelete(Me, e)
                    End Select
                Else
                    Throw New GUIException("File is been Process", Assurant.ElitaPlus.Common.ErrorCodes.ERR_INTERFACE_FILE_IN_PROCESS)
                End If
            Else
                Throw New GUIException("You must select a file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
            TheState.intStatusId = e.InterfaceStatusId
        End Sub
#End Region

#Region "Drop-Down Event Handlers"

        Private Sub moCompanyGroup_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl) _
                Handles moCompanyGroup.SelectedDropChanged
            PopulateCompany(True, True)
        End Sub

        Private Sub moCompany_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl) _
                Handles moCompany.SelectedDropChanged
            PopulateDealer(True, True)
        End Sub

        Private Sub moDealer_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl) _
                Handles moDealer.SelectedDropChanged
            PopulateReference(True)
        End Sub

        Private Sub moReference_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl) _
            Handles moReference.SelectedDropChanged
            PopulateInterface()
        End Sub
#End Region

#Region "Grid Event Handlers"
        Private Sub moDataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles moDataGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                With e.Item
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_FILE_PROCESSED_ID_IDX), dvRow(FileProcessed.COL_NAME_FILE_PROCESSED_ID))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_FILENAME_IDX), dvRow(FileProcessed.COL_NAME_FILENAME))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_RECEIVED_IDX), dvRow(FileProcessed.COL_NAME_RECEIVED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_COUNTED_IDX), dvRow(FileProcessed.COL_NAME_COUNTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_REJECTED_IDX), dvRow(FileProcessed.COL_NAME_REJECTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_VALIDATED_IDX), dvRow(FileProcessed.COL_NAME_VALIDATED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_BYPASSED_IDX), dvRow(FileProcessed.COL_NAME_BYPASSED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_LOADED_IDX), dvRow(FileProcessed.COL_NAME_LOADED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_LAYOUT_IDX), dvRow(FileProcessed.COL_NAME_LAYOUT))
                End With
            End If
        End Sub

        Private Sub moDataGrid_PageIndexChanged(source As System.Object,
                e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                moDataGrid.CurrentPageIndex = e.NewPageIndex
                TheState.PageIndex = moDataGrid.CurrentPageIndex
                ClearSelectedFile(ThePage.POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles moDataGrid.ItemCreated
            ThePage.BaseItemCreated(sender, e)
        End Sub

        Protected Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moDataGrid.ItemCommand
            Try
                If e.CommandName = ThePage.EDIT_COMMAND_NAME Then
                    TheState.SelectedFileProcessedId = New Guid(e.Item.Cells(GRID_COL_FILE_PROCESSED_ID_IDX).Text)
                    TheState.PageIndex = moDataGrid.CurrentPageIndex
                    ThePage.callPage(TheState.msUrlDetailPage, New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, Me))
                ElseIf e.CommandName = ThePage.SELECT_COMMAND_NAME Then
                    moDataGrid.SelectedIndex = e.Item.ItemIndex
                    TheState.SelectedFileProcessedId = ThePage.GetGuidFromString(ThePage.GetSelectedGridText(moDataGrid, GRID_COL_FILE_PROCESSED_ID_IDX))
                    EnableDisableButtons()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
#End Region

#Region "Error-Management"
        Private Sub ShowError(msg As String)
            ErrorCtrl.AddError(msg)
            ErrorCtrl.Show()
            AppConfig.Log(New Exception(msg))
        End Sub
#End Region

#Region "Web Form Designer Generated Code and Page Handlers"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, Me.Load
            SetButtonCaptions()
            mIsLoaded = True
            If (Not IsPostBack) Then
                If (IsReturningFromChild) Then
                    If (ShowComapnyGroup AndAlso AllowCompanyGroup AndAlso Not mReturnType.CompanyGroupId = Guid.Empty) Then
                        PopulateCompanyGroup(False, True)
                        moCompanyGroup.SelectedGuid = mReturnType.CompanyGroupId
                    End If
                    moDataGrid.CurrentPageIndex = TheState.PageIndex
                    If (ShowComapny AndAlso Not mReturnType.CompanyId = Guid.Empty) Then
                        PopulateCompany(False, True)
                        moCompany.SelectedGuid = mReturnType.CompanyId
                    End If
                    If (ShowDealer AndAlso Not mReturnType.DealerId = Guid.Empty) Then
                        PopulateDealer(False, True)
                        moDealer.SelectedGuid = mReturnType.DealerId
                    End If
                    If (ShowReference AndAlso Not mReturnType.ReferenceId = Guid.Empty) Then
                        PopulateReference(True)
                        moReference.SelectedGuid = mReturnType.ReferenceId
                    End If
                    TheState.SelectedFileProcessedId = mReturnType.FileProcessedId
                Else
                    PopulateCompanyGroup(True, True)
                End If
            End If
            ThePage.SetGridItemStyleColor(moDataGrid)
            If IsReturningFromChild Then
                If IsSelectionValid() Then
                    PopulateGrid(ThePage.POPULATE_ACTION_SAVE)
                    EnableDisableButtons()
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            End If
        End Sub

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

        Public Sub InitController(UrlDetailPage As String)
            moState = New MyState(UrlDetailPage)
            Session(SESSION_LOCALSTATE_KEY) = moState
            PopulateCompanyGroup(True, True)
            ThePage.SetGridItemStyleColor(moDataGrid)
        End Sub

        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object)
            IsReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        Try
                            mReturnType = retObj
                        Catch ex As Exception
                            ThePage.HandleErrors(ex, ErrorCtrl)
                        End Try
                    End If
            End Select
        End Sub
#End Region

#Region "Progress Bar"
        Private Sub btnAfterProgressBar_Click(sender As System.Object, e As System.EventArgs) Handles btnAfterProgressBar.Click
            AfterProgressBar()
        End Sub

        Public Sub InstallInterfaceProgressBar()
            ThePage.InstallDisplayProgressBar()
        End Sub

        Private Sub ExecuteAndWait(oSP As Integer, Optional ByVal filename As String = "")
            Dim intStatus As InterfaceStatusWrk
            Dim params As InterfaceBaseForm.Params

            Try
                ExecuteSp(oSP)
                params = SetParameters(TheState.intStatusId, FILE_VARIABLE_NAME)
                Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                moInterfaceProgressControl.EnableInterfaceProgress(FILE_VARIABLE_NAME)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Function SetParameters(intStatusId As Guid, baseController As String) As InterfaceBaseForm.Params
            Dim params As New InterfaceBaseForm.Params

            With params
                .intStatusId = intStatusId
                .baseController = baseController
            End With
            Return params
        End Function

        Private Sub AfterProgressBar()
            ClearSelectedFile(ThePage.POPULATE_ACTION_SAVE)
            ThePage.DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
        End Sub
#End Region

#Region "Handlers-Buttons"
        Private Sub btnCopyFile_WRITE_Click(sender As System.Object, e As System.EventArgs) _
            Handles btnCopyFile_WRITE.Click
            Dim filename As String
            Try
                filename = UploadFile()
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub BtnValidate_WRITE_Click(sender As System.Object, e As System.EventArgs) _
            Handles BtnValidate_WRITE.Click
            ExecuteAndWait(SP_VALIDATE)
        End Sub

        Private Sub BtnLoad_WRITE_Click(sender As System.Object, e As System.EventArgs) _
            Handles BtnLoad_WRITE.Click
            ExecuteAndWait(SP_PROCESS)
        End Sub

        Private Sub BtnDeleteFile_WRITE_Click(sender As System.Object, e As System.EventArgs) _
            Handles BtnDeleteFile_WRITE.Click
            ExecuteAndWait(SP_DELETE)
        End Sub
#End Region

#Region "Unclean"

#Region "Handlers"

        Private Sub BtnRejectReport_Click(sender As System.Object, e As System.EventArgs) Handles BtnRejectReport.Click
            RejectReport(PrintDealerLoadRejectForm.REJECT_REPORT)
        End Sub

        Private Sub BtnProcessedExport_Click(sender As Object, e As System.EventArgs) Handles BtnProcessedExport.Click
            RejectReport(PrintDealerLoadRejectForm.PROCESSED_EXPORT)
        End Sub

        Private Sub RejectReport(reportType As Integer)
            Try
                If Not TheState.SelectedFileProcessedId.Equals(Guid.Empty) Then
                    Dim param As New ExportFileProcessedForm.MyState
                    param.FileProcessedId = TheState.SelectedFileProcessedId
                    param.ReportWindowName = FileNameCaption
                    If (reportType = PrintDealerLoadRejectForm.PROCESSED_EXPORT) Then
                        param.ReportFileName = ProcessedReportFileName
                        param.LoadStatus = "L"
                    Else
                        If (reportType = PrintDealerLoadRejectForm.REJECT_REPORT) Then
                            param.ReportFileName = RejectReportFileName
                            param.LoadStatus = "R"
                        End If
                    End If
                    param.oReturnType = New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, Me)
                    ThePage.callPage(TheState.msUrlPrintPage, param)
                Else
                    Throw New GUIException("You must select a file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region
    End Class
End Namespace
