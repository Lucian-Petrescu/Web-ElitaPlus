Imports System.Text
Imports System.ComponentModel
Imports System.Diagnostics
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.IO
Imports System.Threading

Namespace Interfaces
    Public Class FileProcessedControllerNew
        Inherits UserControl

#Region "Constants"
        Private Const LabelSelectDealer As String = "SELECT_DEALER"
        Private Const LabelSelectCompany As String = "SELECT_COMPANY"
        Private Const LabelSelectCompanyGroup As String = "SELECT_COMPANY_GROUP"
        Private Const LabelSelectCountry As String = "SELECT_COUNTRY"
        Private Const DealerloadformForm001 As String = "DEALERLOADFORM_FORM001"

        Private Const FileVariableName As String = "moFileController_"

        Private Const SpValidate As Integer = 0
        Private Const SpProcess As Integer = 1
        Private Const SpDelete As Integer = 2

        Public Const SessionLocalstateKey As String = "FILE_PROCESSEDCONTROLLER_SESSION_LOCALSTATE_KEY"

        Public Const GridColFileProcessedIdIdx As Integer = 0
        Public Const GridColEditIdx As Integer = 1
        Public Const GridColSelectIdx As Integer = 2
        Public Const GridColFilenameIdx As Integer = 3
        Public Const GridColReceivedIdx As Integer = 4
        Public Const GridColCountedIdx As Integer = 5
        Public Const GridColRejectedIdx As Integer = 6
        Public Const GridColValidatedIdx As Integer = 7
        Public Const GridColBypassedIdx As Integer = 8
        Public Const GridColLoadedIdx As Integer = 9
        Public Const GridColLayoutIdx As Integer = 10
        Public Const GridColStatusIdx As Integer = 11
        Public Const GridColStatusDescIdx As Integer = 12

        Public Const GridLinkBtnBypassed As String = "BtnShowBypassed"
        Public Const GridLinkBtnRejected As String = "BtnShowRejected"
        Public Const GridLinkBtnValidated As String = "BtnShowValidated"
        Public Const GridLinkBtnLoaded As String = "BtnShowLoaded"
        Public Const GridLinkBtnStatus As String = "BtnShowStatus"

        Public Const ShowCommandRejected As String = "ShowRecordRej"
        Public Const ShowCommandValidated As String = "ShowRecordVal"
        Public Const ShowCommandLoaded As String = "ShowRecordLod"
        Public Const ShowCommandBypassed As String = "ShowRecordByp"
        Public Const ShowCommandStatus As String = "ShowStatusDesc"

        Public Const FileStatusPending As String = "PENDING"
        Public Const FileStatusRunning As String = "RUNNING"
        Public Const FileStatusSuccess As String = "SUCCESS"
        Public Const FileStatusFailure As String = "FAILURE"
#End Region
#Region "Enums"
        Public Enum RecordMode
            <Description("rejected records")>
            Rejected = 1
            <Description("validated records")>
            Validated = 2
            <Description("loaded records")>
            Loaded = 3
            <Description("bypassed records")>
            Bypassed = 4
            <Description("None")>
            None
        End Enum
#End Region

#Region "Variables"
        Private _moState As MyState
        Private _mIsLoaded As Boolean = False
        Private _bAllowCompanyGroup As Boolean = True
        Private _moRejectReportFileName As String = String.Empty
        Private _moProcessedReportFileName As String = String.Empty
        Private _mReferenceCaption As String = String.Empty
        Private _mFileNameCaption As String = String.Empty
        Private _mFileType As FileProcessedData.FileTypeCode = FileProcessedData.FileTypeCode.BestReplacement
        'Private ErrorCtrl As ErrorController '' Points to Error Controller on Main From.
        Private _isReturningFromChild As Boolean = False
        Private _mReturnType As ReturnType = Nothing
        Private _messageCtrl As MessageController
#End Region

#Region "Properties"
        Protected ReadOnly Property TheState() As MyState
            Get
                Try
                    If _moState Is Nothing Then
                        _moState = CType(Session(SessionLocalstateKey), MyState)
                    End If
                    Return _moState
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
                Return _mFileType
            End Get
            Set(value As FileProcessedData.FileTypeCode)
                _mFileType = value
            End Set
        End Property

        <Category("Appearance"), DefaultValue("")>
        Public Property FileNameCaption As String
            Get
                Return _mFileNameCaption
            End Get
            Set(value As String)
                _mFileNameCaption = value
            End Set
        End Property

        <Category("Appearance"), DefaultValue("")>
        Public Property ReferenceCaption As String
            Get
                Return _mReferenceCaption
            End Get
            Set(value As String)
                _mReferenceCaption = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property RejectReportFileName As String
            Get
                Return _moRejectReportFileName
            End Get
            Set(value As String)
                _moRejectReportFileName = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ProcessedReportFileName As String
            Get
                Return _moProcessedReportFileName
            End Get
            Set(value As String)
                _moProcessedReportFileName = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowCountry() As Boolean
            Get
                Return moCountry.Visible
            End Get
            Set(value As Boolean)
                moCountry.Visible = value
                If _mIsLoaded Then PopulateCountry(True, False)
            End Set
        End Property
        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowCompanyGroup() As Boolean
            Get
                Return moCompanyGroup.Visible
            End Get
            Set(value As Boolean)
                moCompanyGroup.Visible = value
                If _mIsLoaded Then PopulateCompanyGroup(True, False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowCompany() As Boolean
            Get
                Return moCompany.Visible
            End Get
            Set(value As Boolean)
                moCompany.Visible = value
                If _mIsLoaded Then PopulateCompany(True, False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowDealer() As Boolean
            Get
                Return moDealer.Visible
            End Get
            Set(value As Boolean)
                moDealer.Visible = value
                If _mIsLoaded Then PopulateDealer(True, False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property ShowReference() As Boolean
            Get
                Return moReference.Visible()
            End Get
            Set(value As Boolean)
                moReference.Visible = value
                If _mIsLoaded Then PopulateReference(False)
            End Set
        End Property

        <Category("Behavior"), DefaultValue(False)>
        Public Property AllowCompanyGroup() As Boolean
            Get
                Return _bAllowCompanyGroup
            End Get
            Set(value As Boolean)
                _bAllowCompanyGroup = value
                If _mIsLoaded Then PopulateCompanyGroup(True, False)
            End Set
        End Property
        Public ReadOnly Property CountryId As Guid
            Get
                If (ShowCountry And Not moCountry.SelectedGuid = Guid.Empty) Then
                    Return moCountry.SelectedGuid
                ElseIf Not ShowCountry Then
                    'Return ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    Return Authentication.CurrentUser.Country(Authentication.CurrentUser.CompanyId).Id
                Else
                    Return Nothing
                End If
            End Get
        End Property
        Public ReadOnly Property CompanyGroupId As Guid
            Get
                If (ShowCompanyGroup And Not moCompanyGroup.SelectedGuid = Guid.Empty) Then
                    Return moCompanyGroup.SelectedGuid
                    'ElseIf Not Me.ShowCompanyGroup Then
                    '    Return ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property CompanyId As Guid
            Get
                If (ShowCompany) Then
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
        Public ReadOnly Property CountryCode As String
            Get
                If (Not CountryId = Guid.Empty) Then
                    Return moCountry.SelectedCode
                Else
                    Return Nothing
                End If
            End Get
        End Property
        Public ReadOnly Property CountryDescription As String
            Get
                If (Not CountryId = Guid.Empty) Then
                    Return moCountry.SelectedDesc
                Else
                    Return Nothing
                End If
            End Get
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
            Public LastOperation As DetailPageCommand
            Public FileProcessedId As Guid
            Public RecMode As RecordMode

            Public CountryId As Guid
            Public CountryCode As String
            Public CountryDescription As String

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

            Public Sub New(lastOp As DetailPageCommand, selectedFileProcessedId As Guid,
                           recMode As String, oFileProcessedController As FileProcessedControllerNew)
                LastOperation = lastOp
                FileProcessedId = selectedFileProcessedId
                Me.RecMode = recMode
                With oFileProcessedController
                    CountryId = .CountryId
                    CountryCode = .CountryCode
                    CountryDescription = .CountryDescription
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
            Public MSelectedFileProcessedId As Guid = Guid.Empty
            Public Property SelectedFileProcessedId As Guid
                Get
                    Return MSelectedFileProcessedId
                End Get
                Set(value As Guid)
                    MSelectedFileProcessedId = value
                End Set
            End Property

            Public PageIndex As Integer

            Public MsUrlDetailPage As String
            Public MoFileTypeCode As FileProcessedData.FileTypeCode
            Public MsUrlPrintPage As String

            Public IntStatusId As Guid

            Public Sub New(urlDetailPage As String, urlPrintPage As String,
        oFileTypeCode As FileProcessedData.FileTypeCode)
                MsUrlDetailPage = urlDetailPage
                MsUrlPrintPage = urlPrintPage
                MoFileTypeCode = oFileTypeCode
            End Sub
        End Class

        Public Class FileProcessedEventArgs
            Inherits EventArgs
            Private _mCountryId As Guid
            Private _mCompanyGroupId As Guid
            Private _mCompanyId As Guid
            Private _mDealerId As Guid
            Private _mReferenceId As Guid
            Private _mCompanyGroupCode As String
            Private _mCompanyCode As String
            Private _mDealerCode As String
            Private _mReferenceCode As String

            Public Sub New()
                CountryId = Nothing
                CompanyGroupId = Nothing
                CompanyGroupCode = Nothing
                CompanyId = Nothing
                CompanyCode = Nothing
                DealerId = Nothing
                DealerCode = Nothing
                ReferenceId = Nothing
                ReferenceCode = Nothing
            End Sub

            Friend Sub New(pObject As FileProcessedControllerNew)

                Me.New()

                If (pObject IsNot Nothing) Then
                    With pObject
                        CountryId = .CountryId
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
            Public Property CountryId() As Guid
                Get
                    Return _mCountryId
                End Get
                Set(value As Guid)
                    _mCountryId = value
                End Set
            End Property
            Public Property CompanyGroupId() As Guid
                Get
                    Return _mCompanyGroupId
                End Get
                Set(value As Guid)
                    _mCompanyGroupId = value
                End Set
            End Property

            Public Property CompanyId() As Guid
                Get
                    Return _mCompanyId
                End Get
                Set(value As Guid)
                    _mCompanyId = value
                End Set
            End Property

            Public Property DealerId() As Guid
                Get
                    Return _mDealerId
                End Get
                Set(value As Guid)
                    _mDealerId = value
                End Set
            End Property

            Public Property ReferenceId() As Guid
                Get
                    Return _mReferenceId
                End Get
                Set(value As Guid)
                    _mReferenceId = value
                End Set
            End Property

            Public Property CompanyGroupCode() As String
                Get
                    Return _mCompanyGroupCode
                End Get
                Set(value As String)
                    _mCompanyGroupCode = value
                End Set
            End Property

            Public Property CompanyCode() As String
                Get
                    Return _mCompanyCode
                End Get
                Set(value As String)
                    _mCompanyCode = value
                End Set
            End Property

            Public Property DealerCode() As String
                Get
                    Return _mDealerCode
                End Get
                Set(value As String)
                    _mDealerCode = value
                End Set
            End Property

            Public Property ReferenceCode() As String
                Get
                    Return _mReferenceCode
                End Get
                Set(value As String)
                    _mReferenceCode = value
                End Set
            End Property
        End Class

        Public Class PopulateReferenceEventArgs
            Inherits FileProcessedEventArgs

            Private _mReferenceDv As DataView

            Public Sub New()
                Me.New(Nothing)
            End Sub

            Friend Sub New(pObject As FileProcessedControllerNew)
                MyBase.New(pObject)
                _mReferenceDv = Nothing
            End Sub

            Public Property ReferenceDv() As DataView
                Get
                    Return _mReferenceDv
                End Get
                Set(value As DataView)
                    _mReferenceDv = value
                End Set
            End Property
        End Class

        Public Class SetExpectedFileNameEventArgs
            Inherits FileProcessedEventArgs

            Private _mFileName As String

            Public Sub New()
                Me.New(Nothing)
            End Sub

            Friend Sub New(pObject As FileProcessedControllerNew)
                MyBase.New(pObject)
                _mFileName = Nothing
            End Sub

            Public Property FileName() As String
                Get
                    Return _mFileName
                End Get
                Set(value As String)
                    _mFileName = value
                End Set
            End Property

        End Class

        Public Class ExecuteActionEventArgs
            Inherits EventArgs

            Private _mFileProcessedId As Guid
            Private _mInterfaceStatusId As Guid

            Public Sub New()
                Me.New(Nothing)
            End Sub

            Friend Sub New(pFileProcessedId As Guid)
                _mFileProcessedId = pFileProcessedId
            End Sub

            Public ReadOnly Property FileProcessedId() As Guid
                Get
                    Return _mFileProcessedId
                End Get
            End Property

            Public Property InterfaceStatusId As Guid
                Get
                    Return _mInterfaceStatusId
                End Get
                Set(value As Guid)
                    _mInterfaceStatusId = value
                End Set
            End Property
        End Class

        Public Class FileProcessedDataEventArgs
            Inherits EventArgs

            Private _mFileProcessedData As FileProcessedData

            Public Sub New(pFileProcessedData As FileProcessedData)
                _mFileProcessedData = pFileProcessedData
            End Sub

            Public ReadOnly Property FileProcessedData() As FileProcessedData
                Get
                    Return _mFileProcessedData
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
        Private Sub PopulateCountry(cascade As Boolean, clearSelectedValue As Boolean)
            Dim userCountriesDv As DataView
            Dim selectedValue As Guid = Guid.Empty

            If (ShowCountry) Then
                userCountriesDv = LookupListNew.GetUserCountriesLookupList()
                If Not (moCountry.NothingSelected) Then
                    selectedValue = moCountry.SelectedGuid
                End If
                moCountry.NothingSelected = True
                moCountry.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, userCountriesDv, "* " & TranslationBase.TranslateLabelOrMessage(LabelSelectCountry), True, True)
                If (Not clearSelectedValue And selectedValue <> Guid.Empty) Then
                    moCountry.SelectedGuid = selectedValue
                End If
                clearSelectedValue = clearSelectedValue Or moCountry.SelectedGuid <> selectedValue
            Else
                moCountry.ClearMultipleDrop()
            End If
            If cascade Then PopulateCompanyGroup(cascade, clearSelectedValue)
        End Sub
        Private Sub PopulateCompanyGroup(cascade As Boolean, clearSelectedValue As Boolean)
            Dim companyGroupDv As DataView
            Dim assignedCompanyGroupDv As DataView
            Dim selectedValue As Guid = Guid.Empty
            Dim tempGuid As Guid
            Dim tempGuid1 As Guid
            Dim removeCompanyGroup As Boolean
            If (ShowCompanyGroup) Then
                companyGroupDv = LookupListNew.GetCompanyGroupLookupList()
                companyGroupDv = companyGroupDv.ToTable().Copy().DefaultView
                If (AllowCompanyGroup) Then
                    assignedCompanyGroupDv = BusinessObjectsNew.User.GetAvailableCompanyGroup(Authentication.CurrentUser.Id) 'ElitaPlusIdentity.Current.ActiveUser.Id)
                    For i As Integer = (companyGroupDv.Count - 1) To 0 Step -1
                        tempGuid = New Guid(CType(companyGroupDv(i)("id"), Byte()))
                        removeCompanyGroup = True
                        For j As Integer = 0 To (assignedCompanyGroupDv.Count - 1)
                            tempGuid1 = New Guid(CType(assignedCompanyGroupDv(j)("company_group_id"), Byte()))
                            If (tempGuid = tempGuid1) Then
                                removeCompanyGroup = False
                                Exit For
                            End If
                        Next
                        If (removeCompanyGroup) Then companyGroupDv.Delete(i)
                    Next
                Else
                    For i As Integer = (companyGroupDv.Count - 1) To 0 Step -1
                        tempGuid = New Guid(CType(companyGroupDv(i)("id"), Byte()))
                        If (tempGuid <> Authentication.CurrentUser.CompanyGroup.Id) Then
                            companyGroupDv.Delete(i)
                        End If
                    Next
                End If
                If Not (moCompanyGroup.NothingSelected) Then
                    selectedValue = moCompanyGroup.SelectedGuid
                End If
                moCompanyGroup.NothingSelected = True
                moCompanyGroup.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, companyGroupDv, "* " & TranslationBase.TranslateLabelOrMessage(LabelSelectCompanyGroup), True, True)
                If (Not clearSelectedValue And selectedValue <> Guid.Empty) Then
                    moCompanyGroup.SelectedGuid = selectedValue
                End If
                clearSelectedValue = clearSelectedValue Or moCompanyGroup.SelectedGuid <> selectedValue
            Else
                moCompanyGroup.ClearMultipleDrop()
            End If
            If cascade Then PopulateCompany(cascade, clearSelectedValue)
        End Sub

        Private Sub PopulateCompany(cascade As Boolean, clearSelectedValue As Boolean)
            Dim selectedValue As Guid
            Dim companyDv As DataView
            If (ShowCompany) Then
                If Not (moCompany.NothingSelected) Then
                    selectedValue = moCompany.SelectedGuid
                End If
                companyDv = BusinessObjectsNew.User.LoadUserCompanyAssigned(CompanyGroupId, Authentication.CurrentUser.Id)
                moCompany.NothingSelected = True
                moCompany.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, companyDv, "* " & TranslationBase.TranslateLabelOrMessage(LabelSelectCompany), True, True)
                If (Not clearSelectedValue And selectedValue <> Guid.Empty) Then
                    moCompany.SelectedGuid = selectedValue
                End If
                clearSelectedValue = clearSelectedValue Or moCompany.SelectedGuid <> selectedValue
            Else
                moCompany.ClearMultipleDrop()
            End If
            If cascade Then PopulateDealer(cascade, clearSelectedValue)
        End Sub

        Private Sub PopulateDealer(cascade As Boolean, clearSelectedValue As Boolean)
            Dim selectedValue As Guid
            Dim dealerDv As DataView
            If (ShowDealer) Then
                If Not (moDealer.NothingSelected) Then
                    selectedValue = moDealer.SelectedGuid
                End If
                dealerDv = LookupListNew.GetDealerLookupList(CompanyId)
                moDealer.NothingSelected = True
                moDealer.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, dealerDv, "* " + TranslationBase.TranslateLabelOrMessage(LabelSelectDealer), True, True)
                If (Not clearSelectedValue And selectedValue <> Guid.Empty) Then
                    moDealer.SelectedGuid = selectedValue
                End If
                clearSelectedValue = clearSelectedValue Or moDealer.SelectedGuid <> selectedValue
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
                    moReference.SetControl(True, MultipleColumnDDLabelControl.MODES.NEW_MODE, True, e.ReferenceDv, "* " + TranslationBase.TranslateLabelOrMessage(ReferenceCaption), True, True)
                    If (Not clearSelectedValue And selectedValue <> Guid.Empty) Then
                        moReference.SelectedGuid = selectedValue
                    End If
                    'clearSelectedValue = clearSelectedValue Or moReference.SelectedGuid <> selectedValue
                Else
                    moReference.ClearMultipleDrop()
                End If
                PopulateInterface()
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub

        Private Function GetDataView() As DataView
            Dim oFileData As FileProcessedData = New FileProcessedData
            Dim oDataView As DataView

            With oFileData
                .CountryId = CountryId
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
            Dim dateStr As String = DateTime.Now.ToString("yyMMdd")

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
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub

        Private Function IsSelectionValid() As Boolean
            If (ShowCountry And CountryId = Guid.Empty) Then Return False
            If (ShowCompanyGroup And CompanyGroupId = Guid.Empty) Then Return False
            If (ShowCompany And CompanyId = Guid.Empty) Then Return False
            If (ShowDealer And DealerId = Guid.Empty) Then Return False
            If (ShowReference And ReferenceId = Guid.Empty) Then Return False
            Return True
        End Function

        Public Sub PopulateInterface()
            Try
                ClearAll()
                If IsSelectionValid() Then
                    PopulateGrid(ElitaPlusSearchPage.POPULATE_ACTION_NONE)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub

        Private Sub ClearAll()
            moDataGrid.PageIndex = NO_PAGE_INDEX
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
            moDataGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            DisableButtons()
            TheState.SelectedFileProcessedId = Guid.Empty
            PopulateGrid(oAction)
        End Sub

        Public Sub SetErrorController(oErrorCtrl As MessageController)
            _messageCtrl = oErrorCtrl
        End Sub

        Private Sub DisableButtons()
            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnLoad_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnDeleteFile_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
        End Sub

        Private Sub UploadFile()
            Dim fileName As String
            Dim layoutFileName As String
            Dim fileLen As Integer = FileInput.PostedFile.ContentLength
            If fileLen = 0 Then
                Dim errors() As ValidationError = {New ValidationError(DealerloadformForm001, GetType(FileProcessed), Nothing, Nothing, Nothing)}
                Throw New BOValidationException(errors, GetType(FileProcessed).FullName)
            End If
            fileName = ReplaceSpaceByUnderscore(FileInput.PostedFile.FileName)

            Dim fileBytes(fileLen - 1) As Byte
            Dim objStream As Stream
            objStream = FileInput.PostedFile.InputStream
            objStream.Read(fileBytes, 0, fileLen)

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & Path.GetFileName(fileName)
            layoutFileName = webServerPath & "\" &
                Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
            CreateFolder(webServerPath)
            If FileProcessedDAL.GetFileLayout(FileType) IsNot Nothing Then
                File.WriteAllBytes(webServerFile, fileBytes)
                File.WriteAllBytes(layoutFileName, Encoding.ASCII.GetBytes(FileProcessedDAL.GetFileLayout(FileType)))
            Else
                Throw New GUIException("Missing File Layout Code", ElitaPlus.Common.ErrorCodes.GUI_MISSING_FILE_LAYOUT_CODE)
            End If

            Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
            '' ''Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
            '' ''                     AppConfig.UnixServer.Password, PORT)
            Dim objUnixFtp As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId,
                                 AppConfig.UnixServer.Password)
            Try
                '' ''If (objUnixFTP.Login()) Then
                '' ''    objUnixFTP.UploadFile(webServerFile, False)
                '' ''    objUnixFTP.UploadFile(layoutFileName, False)
                '' ''End If
                objUnixFtp.UploadFile(webServerFile)
                objUnixFtp.UploadFile(layoutFileName)

                Return
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            Finally
                '' ''objUnixFTP.CloseConnection()
            End Try
        End Sub

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
                        'ControlMgr.SetEnableControl(ThePage, BtnDeleteFile_WRITE, True)
                        ControlMgr.SetEnableControl(ThePage, BtnLoad_WRITE, False)
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    End If

                    If (.Loaded.Value = 0) Then '(.Loaded.Value = .Counted.Value) Or
                        ControlMgr.SetEnableControl(ThePage, BtnDeleteFile_WRITE, True)
                    End If
                End With
                'Disable Validate, Delete and Process button based on the File Status --> PENDING/RUNNING
                If (ThePage.GetSelectedGridText(moDataGrid, GridColStatusIdx).Trim.ToUpper = FileStatusPending) _
                        Or (ThePage.GetSelectedGridText(moDataGrid, GridColStatusIdx).Trim.ToUpper = FileStatusRunning) Then
                    ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    ControlMgr.SetEnableControl(ThePage, BtnLoad_WRITE, False)
                    ControlMgr.SetEnableControl(ThePage, BtnDeleteFile_WRITE, False)
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                End If
            Else
                Throw New GUIException("You must select a file", ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
            End If
        End Sub

        Public Sub EnableDisableEditControl()
            Dim i As Integer
            Dim edt As ImageButton

            For i = 0 To (moDataGrid.Rows.Count - 1)
                edt = CType(moDataGrid.Rows(i).Cells(ElitaPlusSearchPage.EDIT_COL).FindControl(ElitaPlusSearchPage.EDIT_CONTROL_NAME), ImageButton)
                If edt IsNot Nothing Then
                    edt.Enabled = (moDataGrid.Rows(i).Cells(GridColRejectedIdx).Text.Trim() <> "0")
                End If
            Next
        End Sub

        Private Sub ExecuteSp(oSp As Integer)
            Dim e As New ExecuteActionEventArgs(TheState.SelectedFileProcessedId)
            If Not TheState.SelectedFileProcessedId.Equals(Guid.Empty) Then
                Dim oFileProcessed As New FileProcessed(TheState.SelectedFileProcessedId)
                If InterfaceStatusWrk.IsfileBeingProcessed(oFileProcessed.FileName) Then
                    Select Case oSp
                        Case SpValidate
                            RaiseEvent OnValidate(Me, e)
                            _messageCtrl.AddInformation(Message.MSG_VALIDATE_PROCESS_STARTED, True)
                        Case SpProcess
                            RaiseEvent OnProcess(Me, e)
                            _messageCtrl.AddInformation(Message.MSG_LOAD_PROCESS_STARTED, True)
                        Case SpDelete
                            RaiseEvent OnDelete(Me, e)
                    End Select
                Else
                    Throw New GUIException("File is been Process", ElitaPlus.Common.ErrorCodes.ERR_INTERFACE_FILE_IN_PROCESS)
                End If
            Else
                Throw New GUIException("You must select a file", ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
            TheState.IntStatusId = e.InterfaceStatusId
        End Sub
#End Region

#Region "Drop-Down Event Handlers"
        Private Sub moCountry_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                Handles moCountry.SelectedDropChanged
            PopulateCompanyGroup(True, True)
        End Sub
        Private Sub moCompanyGroup_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                Handles moCompanyGroup.SelectedDropChanged
            PopulateCompany(True, True)
        End Sub

        Private Sub moCompany_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                Handles moCompany.SelectedDropChanged
            PopulateDealer(True, True)
        End Sub

        Private Sub moDealer_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                Handles moDealer.SelectedDropChanged
            PopulateReference(True)
        End Sub

        Private Sub moReference_SelectedDropChanged(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
            Handles moReference.SelectedDropChanged
            PopulateInterface()
        End Sub
#End Region

#Region "Grid Event Handlers"
        Private Sub moDataGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            'If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                With e.Row
                    ThePage.PopulateControlFromBOProperty(.Cells(GridColFileProcessedIdIdx), dvRow(FileProcessed.COL_NAME_FILE_PROCESSED_ID))
                    ThePage.PopulateControlFromBOProperty(.Cells(GridColFilenameIdx), dvRow(FileProcessed.COL_NAME_FILENAME))
                    ThePage.PopulateControlFromBOProperty(.Cells(GridColReceivedIdx), dvRow(FileProcessed.COL_NAME_RECEIVED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GridColCountedIdx), dvRow(FileProcessed.COL_NAME_COUNTED))

                    Dim oLinkButton As LinkButton
                    oLinkButton = CType(.FindControl(GridLinkBtnBypassed), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(FileProcessed.COL_NAME_BYPASSED)))
                    If (oLinkButton.Text.Trim = "0") Then
                        oLinkButton.Enabled = False
                    End If

                    oLinkButton = CType(.FindControl(GridLinkBtnRejected), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(FileProcessed.COL_NAME_REJECTED)))
                    If (oLinkButton.Text.Trim = "0") Then
                        oLinkButton.Enabled = False
                    End If
                    oLinkButton = CType(.FindControl(GridLinkBtnValidated), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(FileProcessed.COL_NAME_VALIDATED)))
                    If (oLinkButton.Text.Trim = "0") Then
                        oLinkButton.Enabled = False
                    End If
                    oLinkButton = CType(.FindControl(GridLinkBtnLoaded), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(FileProcessed.COL_NAME_LOADED)))
                    If (oLinkButton.Text.Trim = "0") Then
                        oLinkButton.Enabled = False
                    End If

                    ThePage.PopulateControlFromBOProperty(.Cells(GridColLayoutIdx), dvRow(FileProcessed.COL_NAME_LAYOUT))

                    oLinkButton = CType(.FindControl(GridLinkBtnStatus), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, dvRow(FileProcessed.COL_NAME_STATUS))
                    If (oLinkButton.Text.Trim = "") Then
                        oLinkButton.Enabled = False
                    End If
                    ThePage.PopulateControlFromBOProperty(.Cells(GridColStatusDescIdx), dvRow(FileProcessed.COL_NAME_STATUS_DESC))
                End With
            End If
        End Sub

        Private Sub moDataGrid_PageIndexChanged(sender As Object, e As EventArgs) Handles moDataGrid.PageIndexChanged
            Try
                'moDataGrid.CurrentPageIndex = e.NewPageIndex
                TheState.PageIndex = moDataGrid.PageIndex
                ClearSelectedFile(ElitaPlusSearchPage.POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub
       Private Sub moDataGrid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                moDataGrid.PageIndex = e.NewPageIndex
                TheState.PageIndex = moDataGrid.PageIndex
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub

        Public Sub RowCreated(sender As Object, e As GridViewRowEventArgs) Handles moDataGrid.RowCreated
            ThePage.BaseItemCreated(sender, e)
        End Sub

        Private Sub moDataGrid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles moDataGrid.RowCommand
            Try
                Dim index As Integer
                index = CInt(e.CommandArgument)
                If (e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME OrElse e.CommandName = ShowCommandRejected) Then
                    TheState.SelectedFileProcessedId = New Guid(moDataGrid.Rows(index).Cells(GridColFileProcessedIdIdx).Text)
                    TheState.PageIndex = moDataGrid.PageIndex
                    ThePage.callPage(TheState.MsUrlDetailPage, New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, RecordMode.Rejected, Me))
                ElseIf e.CommandName = ShowCommandValidated Then
                    TheState.SelectedFileProcessedId = New Guid(moDataGrid.Rows(index).Cells(GridColFileProcessedIdIdx).Text)
                    TheState.PageIndex = moDataGrid.PageIndex
                    ThePage.callPage(TheState.MsUrlDetailPage, New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, RecordMode.Validated, Me))
                ElseIf e.CommandName = ShowCommandLoaded Then
                    TheState.SelectedFileProcessedId = New Guid(moDataGrid.Rows(index).Cells(GridColFileProcessedIdIdx).Text)
                    TheState.PageIndex = moDataGrid.PageIndex
                    ThePage.callPage(TheState.MsUrlDetailPage, New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, RecordMode.Loaded, Me))
                ElseIf e.CommandName = ShowCommandBypassed Then
                    TheState.SelectedFileProcessedId = New Guid(moDataGrid.Rows(index).Cells(GridColFileProcessedIdIdx).Text)
                    TheState.PageIndex = moDataGrid.PageIndex
                    ThePage.callPage(TheState.MsUrlDetailPage, New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, RecordMode.Bypassed, Me))
                ElseIf e.CommandName = ElitaPlusSearchPage.SELECT_COMMAND_NAME Then
                    moDataGrid.SelectedIndex = index
                    TheState.SelectedFileProcessedId = GetGuidFromString(ThePage.GetSelectedGridText(moDataGrid, GridColFileProcessedIdIdx))
                    EnableDisableButtons()
                ElseIf e.CommandName = ShowCommandStatus Then
                    TheState.SelectedFileProcessedId = New Guid(moDataGrid.Rows(index).Cells(GridColFileProcessedIdIdx).Text)
                    TheState.PageIndex = moDataGrid.PageIndex
                    txtExtendedContent.Text = HttpUtility.HtmlEncode(moDataGrid.Rows(index).Cells(GridColStatusDescIdx).Text)
                    mdlPopup.Show()
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub
#End Region

#Region "Error-Management"
        'Private Sub ShowError(ByVal msg As String)
        '    _messageCtrl.AddError(msg)
        '    _messageCtrl.Show()
        '    AppConfig.Log(New Exception(msg))
        'End Sub
#End Region

#Region "Web Form Designer Generated Code and Page Handlers"
        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load, Me.Load
            SetButtonCaptions()
            _mIsLoaded = True
            If (Not IsPostBack) Then
                If (_isReturningFromChild) Then

                    'PopulateCountry()
                    If (ShowCountry AndAlso Not _mReturnType.CountryId = Guid.Empty) Then
                        PopulateCountry(False, True)
                        moCountry.SelectedGuid = _mReturnType.CountryId
                    End If

                    moDataGrid.PageIndex = TheState.PageIndex
                    If (ShowCompanyGroup AndAlso AllowCompanyGroup AndAlso Not _mReturnType.CompanyGroupId = Guid.Empty) Then
                        PopulateCompanyGroup(False, True)
                        moCompanyGroup.SelectedGuid = _mReturnType.CompanyGroupId
                    End If
                    If (ShowCompany AndAlso Not _mReturnType.CompanyId = Guid.Empty) Then
                        PopulateCompany(False, True)
                        moCompany.SelectedGuid = _mReturnType.CompanyId
                    End If
                    If (ShowDealer AndAlso Not _mReturnType.DealerId = Guid.Empty) Then
                        PopulateDealer(False, True)
                        moDealer.SelectedGuid = _mReturnType.DealerId
                    End If
                    If (ShowReference AndAlso Not _mReturnType.ReferenceId = Guid.Empty) Then
                        PopulateReference(True)
                        moReference.SelectedGuid = _mReturnType.ReferenceId
                    End If
                    TheState.SelectedFileProcessedId = _mReturnType.FileProcessedId
                Else
                    PopulateCountry(True, True)
                End If
            End If
            ThePage.SetGridItemStyleColor(moDataGrid)
            If _isReturningFromChild Then
                If IsSelectionValid() Then
                    PopulateGrid(ElitaPlusSearchPage.POPULATE_ACTION_SAVE)
                    EnableDisableButtons()
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            End If
        End Sub

        'This call is required by the Web Form Designer.
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

        Public Sub InitController(urlDetailPage As String, urlPrintPage As String,
        oFileTypeCode As FileProcessedData.FileTypeCode)

            _moState = New MyState(urlDetailPage, urlPrintPage, oFileTypeCode)
            Session(SessionLocalstateKey) = _moState
            PopulateCountry(True, True)
            ThePage.TranslateGridHeader(moDataGrid)
            ThePage.SetGridItemStyleColor(moDataGrid)
        End Sub

        Public Sub Page_PageReturn(returnFromUrl As String, returnPar As Object)
            _isReturningFromChild = True
            Dim retObj As ReturnType = CType(returnPar, ReturnType)
            Select Case retObj.LastOperation
                Case DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        Try
                            _mReturnType = retObj
                        Catch ex As Exception
                            ThePage.HandleErrors(ex, _messageCtrl)
                        End Try
                    End If
            End Select
        End Sub
#End Region

#Region "Progress Bar"
        Private Sub btnAfterProgressBar_Click(sender As Object, e As EventArgs) Handles btnAfterProgressBar.Click
            AfterProgressBar()
        End Sub

        Public Sub InstallInterfaceProgressBar()
            ThePage.InstallDisplayProgressBar()
        End Sub

        Private Sub ExecuteAndWait(oSp As Integer)
            Dim params As InterfaceBaseForm.Params

            Try
                ExecuteSp(oSp)
                params = SetParameters(TheState.IntStatusId, FileVariableName)
                Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                moInterfaceProgressControl.EnableInterfaceProgress(FileVariableName)
            Catch ex As ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
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
            ClearSelectedFile(ElitaPlusSearchPage.POPULATE_ACTION_SAVE)
            ThePage.DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
        End Sub
#End Region

#Region "Handlers-Buttons"
        Private Sub btnCopyFile_WRITE_Click(sender As Object, e As EventArgs) _
            Handles btnCopyFile_WRITE.Click
            Try
                UploadFile()
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub

        Private Sub BtnValidate_WRITE_Click(sender As Object, e As EventArgs) _
            Handles BtnValidate_WRITE.Click
            ExecuteAndWait(SpValidate)
        End Sub

        Private Sub BtnLoad_WRITE_Click(sender As Object, e As EventArgs) _
            Handles BtnLoad_WRITE.Click
            ExecuteAndWait(SpProcess)
        End Sub

        Private Sub BtnDeleteFile_WRITE_Click(sender As Object, e As EventArgs) _
            Handles BtnDeleteFile_WRITE.Click
            ExecuteAndWait(SpDelete)
        End Sub
        Private Sub btnExtendedContentPopupCancel_Click(sender As Object, e As EventArgs) Handles btnExtendedContentPopupCancel.Click
            mdlPopup.Hide()
        End Sub
#End Region

#Region "Unclean"

#Region "Handlers"

        Private Sub BtnRejectReport_Click(sender As Object, e As EventArgs) Handles BtnRejectReport.Click
            RejectReport(ExportFileProcessedNewForm.REJECT_REPORT)
        End Sub

        Private Sub BtnProcessedExport_Click(sender As Object, e As EventArgs) Handles BtnProcessedExport.Click
            RejectReport(ExportFileProcessedNewForm.PROCESSED_EXPORT)
        End Sub

        Private Sub RejectReport(reportType As Integer)
            Try
                If Not TheState.SelectedFileProcessedId.Equals(Guid.Empty) Then
                    Dim param As New ExportFileProcessedNewForm.MyState
                    param.FileProcessedId = TheState.SelectedFileProcessedId
                    param.ReportFileName = FileNameCaption
                    param.moFileTypeCode = TheState.moFileTypeCode
                    param.oReturnType = New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, RecordMode.None, Me)

                    If (reportType = ExportFileProcessedNewForm.PROCESSED_EXPORT) Then
                        param.ReportFileName = ProcessedReportFileName
                        param.LoadStatus = "L"
                        param.reportType = reportType
                        If Not ReferenceId = Guid.Empty Then
                            param.ServiceCenterId = ReferenceId
                        End If
                    Else
                        If (reportType = ExportFileProcessedNewForm.REJECT_REPORT) Then
                            param.ReportFileName = RejectReportFileName
                            param.LoadStatus = "R"
                            param.reportType = reportType
                            If Not ReferenceId = Guid.Empty Then
                                param.ServiceCenterId = ReferenceId
                            End If
                        End If
                    End If
                    '            param.oReturnType = New ReturnType(DetailPageCommand.Nothing_, TheState.SelectedFileProcessedId, Me)
                    ThePage.callPage(TheState.MsUrlPrintPage, param)
                Else
                    Throw New GUIException("You must select a file", ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, _messageCtrl)
            End Try
        End Sub

#End Region

#End Region
    End Class
End Namespace

