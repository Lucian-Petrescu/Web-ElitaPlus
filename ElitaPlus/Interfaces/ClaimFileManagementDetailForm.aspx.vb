Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports System.ServiceModel
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.FileManagerAdminService

Namespace Interfaces

    Partial Class ClaimFileManagementDetailForm
        Inherits ElitaPlusSearchPage

        Public Class ClaimFileInfoParams
            Private ReadOnly mRecordState As RecordStateType
            Private ReadOnly mRecordCount As Integer

            Public Sub New(ByVal RecordState As RecordStateType, ByVal RecordCount As Integer)
                mRecordState = RecordState
                mRecordCount = RecordCount
            End Sub

            Public ReadOnly Property RecordState As RecordStateType
                Get
                    Return mRecordState
                End Get
            End Property

            Public ReadOnly Property RecordCount As Integer
                Get
                    Return mRecordCount
                End Get
            End Property
        End Class

        Public Class ClaimFileDetailPageParams
            Private ReadOnly mDataItemInfo As DataItemLocator
            Private ReadOnly mFileInfoParams As ClaimFileInfoParams

            Public Sub New(ByVal FileIdentifier As String, ByVal FileInfoParams As ClaimFileInfoParams)
                System.Diagnostics.Debug.Assert(Not String.IsNullOrWhiteSpace(FileIdentifier))
                System.Diagnostics.Debug.Assert(FileInfoParams IsNot Nothing)

                mDataItemInfo = New DataItemLocator() With
                {
                    .Identifier = FileIdentifier,
                    .Type = DataItemType.FileInfo,
                    .ForceRefresh = False
                }

                mFileInfoParams = FileInfoParams

            End Sub

            Public ReadOnly Property DataItemLocatorInfo As DataItemLocator
                Get
                    Return mDataItemInfo
                End Get
            End Property

            Public ReadOnly Property FileInfoParams As ClaimFileInfoParams
                Get
                    Return mFileInfoParams
                End Get
            End Property
        End Class

#Region "Constants"
        Public Const PageUrl As String = "ClaimFileManagementDetailForm.aspx"
        Public PageTitle As String = "CLAIM_MANAGEMENT_FILE_DETAILS"
        Private Const ServiceEndPointName = "CustomBinding_FileManagerAdmin"

        Public Enum GridDefenitionEnum
            FileDetailsRecordIdentifier = 0
            LineNumber
        End Enum

        Public Const STATUS_FAILED As String = "F"
        Public Const STATUS_REPROCESS As String = "R"

        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_FINANCIAL_LINE_INFO As String = "GridFinancialLineInfo"
        Private Const GRID_ADDITIONAL_INFO As String = "GridAdditionalInfo"
        Private Const GRID_ERRORS_CLAIM_RECORD As String = "GridErrorsClaimRecord"
        Private Const GRID_ERRORS_FINANCIAL_LINE_INFO As String = "GridErrorsFinancialLineInfo"
        Private Const GRID_ERRORS_ADDITIONAL_INFO As String = "GridErrorsAdditionalInfo"

        Private Const GRID_COLUMN_SAVE_IMAGE As Integer = 1
        Private Const GRID_COLUMN_ERRORS As Integer = 3
        Private Const GRID_FLI_COLUMN_ERRORS As Integer = 0
        Private Const GRID_AIF_COLUMN_ERRORS As Integer = 0

        Private Const RECORD_QUALIFIER_FLI As String = "FLI"
        Private Const RECORD_QUALIFIER_AIF As String = "AIF"

        Private Const GRID_RECORD_IDENTIFIER As String = "RecordIdentifier"
        Private Const GRID_RECORD_CERTIFICATE_QUALIFIER As String = "RecordCertificateQualifier"
        Private Const GRID_RECORD_CERTIFICATE_CODE As String = "RecordCertificateCode"
        Private Const GRID_RECORD_CERTIFICATE_SUB_QUALIFIER As String = "RecordCertificateSubQualifier"
        Private Const GRID_RECORD_CERTIFICATE_SUB_CODE As String = "RecordCertificateSubCode"
        Private Const GRID_RECORD_CERTIFICATE_ADDITIONAL_QUALIFIER As String = "RecordCertificateAdditionalQualifier"
        Private Const GRID_RECORD_CERTIFICATE_ADDITIONAL_CODE As String = "RecordCertificateAdditionalCode"
        Private Const GRID_RECORD_LOSS_DATE As String = "RecordLossDate"
        Private Const GRID_RECORD_LOSS_DATE_FORMAT As String = "RecordLossDateFormat"
        Private Const GRID_RECORD_LOSS_TIME_START As String = "RecordLossTimeStart"
        Private Const GRID_RECORD_LOSS_TIME_END As String = "RecordLossTimeEnd"
        Private Const GRID_RECORD_LOSS_TIMEZONE As String = "RecordLossTimezone"
        Private Const GRID_RECORD_ITEM_QUALIFIER As String = "RecordItemQualifier"
        Private Const GRID_RECORD_ITEM_CODE As String = "RecordItemCode"
        Private Const GRID_RECORD_ITEM_SUB_QUALIFIER As String = "RecordItemSubQualifier"
        Private Const GRID_RECORD_ITEM_SUB_CODE As String = "RecordItemSubCode"
        Private Const GRID_RECORD_ITEM_ADDITIONAL_QUALIFIER As String = "RecordItemAdditionalQualifier"
        Private Const GRID_RECORD_ITEM_ADDITIONAL_CODE As String = "RecordItemAdditionalCode"
        Private Const GRID_RECORD_COVERAGE_TYPE_QUALIFIER As String = "RecordCoverageTypeQualifier"
        Private Const GRID_RECORD_COVERAGE_TYPE_CODE As String = "RecordCoverageTypeCode"
        Private Const GRID_RECORD_COVERAGE_TYPE_SUB_QUALIFIER As String = "RecordCoverageTypeSubQualifier"
        Private Const GRID_RECORD_COVERAGE_TYPE_SUB_CODE As String = "RecordCoverageTypeSubCode"
        Private Const GRID_RECORD_COVERAGE_TYPE_ADDITIONAL_QUALIFIER As String = "RecordCoverageTypeAdditionalQualifier"
        Private Const GRID_RECORD_COVERAGE_TYPE_ADDITIONAL_CODE As String = "RecordCoverageTypeAdditionalCode"
        Private Const GRID_RECORD_CLAIM_ITEM_SKU As String = "RecordClaimItemSku"
        Private Const GRID_RECORD_CLAIM_ITEM_MAKE As String = "RecordClaimItemMake"
        Private Const GRID_RECORD_CLAIM_ITEM_MODEL As String = "RecordClaimItemModel"
        Private Const GRID_RECORD_CLAIM_ITEM_COLOR As String = "RecordClaimItemColor"
        Private Const GRID_RECORD_CLAIM_ITEM_CAPACITY As String = "RecordClaimItemCapacity"
        Private Const GRID_RECORD_CLAIM_ITEM_CARRIER As String = "RecordClaimItemCarrier"
        Private Const GRID_RECORD_CLAIM_ITEM_QUALIFIER As String = "RecordClaimItemQualifier"
        Private Const GRID_RECORD_CLAIM_ITEM_CODE As String = "RecordClaimItemCode"
        Private Const GRID_RECORD_CLAIM_ITEM_ADDITIONAL_QUALIFIER As String = "RecordClaimItemAdditionalQualifier"
        Private Const GRID_RECORD_CLAIM_ITEM_ADDITIONAL_CODE As String = "RecordClaimItemAdditionalCode"
        Private Const GRID_RECORD_PROBLEM_STATEMENT As String = "RecordProblemStatement"
        Private Const GRID_RECORD_ERROR_CODE As String = "RecordErrorCode"
        Private Const GRID_RECORD_ERROR_MESSAGE As String = "RecordErrorMesage"

        Private Const GRID_FLI_QUALIFIER As String = "FliItemQualifier"
        Private Const GRID_FLI_CODE As String = "FliItemCode"
        Private Const GRID_FLI_SUB_QUALIFIER As String = "FliItemSubQualifier"
        Private Const GRID_FLI_SUB_CODE As String = "FliItemSubCode"
        Private Const GRID_FLI_ADDITIONAL_QUALIFIER As String = "FliItemAdditionalQualifier"
        Private Const GRID_FLI_ADDITIONAL_CODE As String = "FliItemAdditionalCode"
        Private Const GRID_FLI_CURRENCY As String = "FliCurrency"
        Private Const GRID_FLI_AMOUNT As String = "FliAmount"
        Private Const GRID_FLI_DECIMAL_SEPARATOR As String = "FliDecimalSeparator"
        Private Const GRID_FLI_ORIGINAL_CURRENCY As String = "FliOriginalCurrency"
        Private Const GRID_FLI_ORIGINAL_AMOUNT As String = "FliOriginalAmount"
        Private Const GRID_FLI_CONVERSION_RATE As String = "FliConversionRate"
        Private Const GRID_FLI_CONVERSION_DATE As String = "FliConversionDate"
        Private Const GRID_FLI_CONVERSION_DATE_FORMAT As String = "FliConversionDateFormat"
        Private Const GRID_FLI_CONVERSION_TIME As String = "FliConversionTime"
        Private Const GRID_FLI_CONVERSION_TIME_TIMEZONE As String = "FliConversionTimeTimezone"
        Private Const GRID_FLI_ERROR_CODE As String = "FliErrorCode"
        Private Const GRID_FLI_ERROR_MESSAGE As String = "FliErrorMesage"

        Private Const GRID_AIF_QUALIFIER As String = "AifQualifier"
        Private Const GRID_AIF_INFORMATION As String = "AifInformation"
        Private Const GRID_AIF_ERROR_CODE As String = "AifErrorCode"
        Private Const GRID_AIF_ERROR_MESS As String = "AifErrorMesage"

#End Region

#Region "MyState"
        Class MyState
            REM ---------
            Public CallingPageParams As ClaimFileDetailPageParams = Nothing
            Public PagingInfo As PagingFilter = Nothing
            Public SearchDV As IEnumerable(Of FileDetailsRecordDto) = Nothing
            REM ---------

            Public ReadOnly Property IsEditable As Boolean
                Get
                    If (CallingPageParams.FileInfoParams IsNot Nothing) Then
                        Return CallingPageParams.FileInfoParams.RecordState = RecordStateType.Rejected
                    End If

                    Return False
                End Get
            End Property
        End Class
#End Region

#Region "Page State"
        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "Button Event Handlers"
        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            ' Return from the Back Button
            GoBack()
        End Sub

        Private Sub SetSession()
            With Me.State
                .PagingInfo = New PagingFilter With {.PageIndex = Grid.PageIndex, .PageSize = Grid.PageSize}
                .SearchDV = Me.State.SearchDV
            End With
        End Sub
#End Region

#Region "Handlers-Buttons-Methods"
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                GoBack()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                For Each oRow As GridViewRow In Grid.Rows
                    SaveFileDetailRecord(oRow)
                Next

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub

        Private Sub GoBack()
            Dim retType As New ClaimFileManagementSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)

            Me.ReturnToCallingPage(retType)
        End Sub

        Protected Sub ShowHideGridInfo_Click(sender As Object, e As EventArgs)
            Dim ImgShowHide As ImageButton = TryCast(sender, ImageButton)
            Dim Row As GridViewRow = TryCast(ImgShowHide.NamingContainer, GridViewRow)
            Dim Panel As Panel = Row.FindControl("pnlShowHideGridInfo")

            If (ImgShowHide.CommandArgument = "Show") Then
                Panel.Visible = True
                ImgShowHide.CommandArgument = "Hide"
                ImgShowHide.ImageUrl = "~/Navigation/images/icons/minus.png"

            Else
                Panel.Visible = False
                ImgShowHide.CommandArgument = "Show"
                ImgShowHide.ImageUrl = "~/Navigation/images/icons/plus.png"

            End If
        End Sub

#End Region

#Region "Page Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Interfaces")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTitle)

                UpdateBreadCrum()

                If Not IsPostBack Then
                    ThePage.PopulateControlFromBOProperty(lblRecordCountValue, State.CallingPageParams.FileInfoParams.RecordCount.ToString())

                    SetGridItemStyleColor(Me.Grid)
                    State.PagingInfo = New PagingFilter With {.PageIndex = DEFAULT_PAGE_INDEX, .PageSize = DEFAULT_PAGE_SIZE}

                    TranslateGridHeader(Grid)
                    PopulateGrid(True)
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)

            End Try

            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Function FindGridControl(ByVal Item As Control, ByVal ItemName As String) As Control

            Dim ReturnItem As Control = Nothing

            For Each ChildItem As Control In Item.Controls
                ReturnItem = FindGridControl(ChildItem, ItemName)

                If (TypeOf (ReturnItem) Is GridView) Then
                    If (CType(ReturnItem, GridView).ID.Equals(ItemName)) Then
                        Exit For
                    End If
                End If
            Next

            Return ReturnItem

        End Function

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not CallingParameters Is Nothing Then
                    Dim PageParameters As ClaimFileDetailPageParams = CType(Me.CallingParameters, ClaimFileDetailPageParams)

                    If Not PageParameters Is Nothing Then
                        State.CallingPageParams = PageParameters
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub
#End Region

#Region "Methods"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CLAIM_FILE_DETAILS")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_FILE_DETAILS")
                End If
            End If
        End Sub

        Private Sub EnableDisableUserControls()
            Dim IsVisible As Boolean = False

            If (State.SearchDV IsNot Nothing) Then
                IsVisible = State.SearchDV.Any()
            End If

            ControlMgr.SetVisibleControl(Me, trPageSize, IsVisible)
            ControlMgr.SetVisibleControl(Me, cboPageSize, IsVisible)
            ControlMgr.SetVisibleControl(Me, Grid, IsVisible)
            ControlMgr.SetVisibleControl(Me, Grid.BottomPagerRow, IsVisible)
            ControlMgr.SetVisibleControl(Me, lblRecordCount, IsVisible)
            ControlMgr.SetVisibleControl(Me, lblRecordCountValue, IsVisible)

            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, State.IsEditable)

            If (IsVisible) Then
                EnableDisableGridControls()
            End If
        End Sub

        Private Sub EnableDisableGridControls()

            Dim TextBoxIsEnabled As Boolean = State.IsEditable
            Dim oTextBox As TextBox = Nothing

            For Each GridRow As GridViewRow In Grid.Rows
                EnableDisableGridTextBoxControls(GridRow, TextBoxIsEnabled)
            Next

            Grid.Columns(GRID_COLUMN_SAVE_IMAGE).Visible = TextBoxIsEnabled
        End Sub

        Private Sub EnableDisableGridTextBoxControls(ByRef oControl As Control, ByVal IsEnabled As Boolean)
            Dim oTextBox As TextBox = Nothing

            For Each ChildControl As Control In oControl.Controls
                EnableDisableGridTextBoxControls(ChildControl, IsEnabled)

                oTextBox = TryCast(ChildControl, TextBox)

                If (oTextBox IsNot Nothing) Then
                    oTextBox.Enabled = IsEnabled
                End If
            Next

        End Sub
#End Region

#Region "Grid Handles"
        Protected WithEvents gvAdditionalInfo As GridView = Nothing
        Protected WithEvents gvFinancialLineInfo As GridView = Nothing


        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Dim PageSize As Integer = CType(cboPageSize.SelectedValue, Int32)

                State.PagingInfo = New PagingFilter With {.PageIndex = DEFAULT_PAGE_INDEX, .PageSize = PageSize}
                PopulateGrid(True)

            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PagingInfo = New PagingFilter With {.PageIndex = e.NewPageIndex, .PageSize = State.PagingInfo.PageSize}
                PopulateGrid(True)

            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub

        Public Sub Grid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                If (e.CommandName = "SaveRecord") Then
                    Dim Index As Int16 = CInt(e.CommandArgument)
                    Dim oRow As GridViewRow = Grid.Rows(Index)

                    SaveFileDetailRecord(oRow)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)

            End Try

        End Sub

        Private Sub SaveFileDetailRecord(ByVal oRow As GridViewRow)

            Dim oLabel As Label = TryCast(oRow.FindControl(GRID_RECORD_IDENTIFIER), Label)

            If (oLabel IsNot Nothing) Then
                Dim RecordIdentifier As String = oLabel.Text

                Dim oFileDetailsRecord As FileDetailsRecordDto = Nothing

                If (Not String.IsNullOrWhiteSpace(RecordIdentifier)) Then
                    oFileDetailsRecord = (
                        From FileDetailsRecord In State.SearchDV
                        Where FileDetailsRecord.Locator.Identifier = RecordIdentifier
                        Select FileDetailsRecord).FirstOrDefault
                End If

                If (oFileDetailsRecord IsNot Nothing) Then
                    ' Update the oFileInfo object with the values in the oRow grid variable
                    UpdateFileDetailRecord(oFileDetailsRecord, oRow)

                    ' Save the resulting oFileInfo using the web service call "SaveFileInfoRecord"
                    If (SaveFileDetailRecord(oFileDetailsRecord)) Then
                        Me.MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    Else
                        Me.MasterPage.MessageController.AddSuccess(MSG_RECORD_NOT_SAVED, True)
                    End If

                End If
            End If
        End Sub

        Private Function UpdateFileDetailRecord(ByRef FileDetailsRecord As FileDetailsRecordDto, ByVal Row As GridViewRow) As Boolean

            Dim oGridView As GridView = Nothing
            Dim TextValue As String = Nothing

            Dim ClaimInfo As ReportClaim = TryCast(FileDetailsRecord.RecordContents, ReportClaim)

            If (ClaimInfo IsNot Nothing) Then
                TextValue = TextBoxValue(Row, GRID_RECORD_CERTIFICATE_QUALIFIER)
                PopulateBO(ClaimInfo.CertificateIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CERTIFICATE_CODE)
                PopulateBO(ClaimInfo.CertificateIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CERTIFICATE_SUB_QUALIFIER)
                PopulateBO(ClaimInfo.CertificateSubIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CERTIFICATE_SUB_CODE)
                PopulateBO(ClaimInfo.CertificateSubIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CERTIFICATE_ADDITIONAL_QUALIFIER)
                PopulateBO(ClaimInfo.CertificateAdditionalIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CERTIFICATE_ADDITIONAL_CODE)
                PopulateBO(ClaimInfo.CertificateAdditionalIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_LOSS_DATE)
                PopulateBO(ClaimInfo.LossDate, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_LOSS_DATE_FORMAT)
                PopulateBO(ClaimInfo.LossDate.DateFormat, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_LOSS_TIME_START)
                PopulateBO(ClaimInfo.LossTimeStart.Time, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_LOSS_TIME_END)
                PopulateBO(ClaimInfo.LossTimeEnd.Time, TextValue)

                ' make sure that time sone information is consistent
                TextValue = TextBoxValue(Row, GRID_RECORD_LOSS_TIMEZONE)
                PopulateBO(ClaimInfo.LossTimeStart.TimeZoneCode, TextValue)
                PopulateBO(ClaimInfo.LossTimeEnd.TimeZoneCode, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_ITEM_QUALIFIER)
                PopulateBO(ClaimInfo.ItemIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_ITEM_CODE)
                PopulateBO(ClaimInfo.ItemIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_ITEM_SUB_QUALIFIER)
                PopulateBO(ClaimInfo.ItemSubIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_ITEM_SUB_CODE)
                PopulateBO(ClaimInfo.ItemSubIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_ITEM_ADDITIONAL_QUALIFIER)
                PopulateBO(ClaimInfo.ItemAdditionalIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_ITEM_ADDITIONAL_CODE)
                PopulateBO(ClaimInfo.ItemAdditionalIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_COVERAGE_TYPE_QUALIFIER)
                PopulateBO(ClaimInfo.CoverageTypeIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_COVERAGE_TYPE_CODE)
                PopulateBO(ClaimInfo.CoverageTypeIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_COVERAGE_TYPE_SUB_QUALIFIER)
                PopulateBO(ClaimInfo.CoverageTypeSubIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_COVERAGE_TYPE_SUB_CODE)
                PopulateBO(ClaimInfo.CoverageTypeSubIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_COVERAGE_TYPE_ADDITIONAL_QUALIFIER)
                PopulateBO(ClaimInfo.CoverageTypeAdditionalIdentifier.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_COVERAGE_TYPE_ADDITIONAL_CODE)
                PopulateBO(ClaimInfo.CoverageTypeAdditionalIdentifier.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_SKU)
                PopulateBO(ClaimInfo.ClaimedItem.Sku, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_MAKE)
                PopulateBO(ClaimInfo.ClaimedItem.Make, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_MODEL)
                PopulateBO(ClaimInfo.ClaimedItem.Model, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_COLOR)
                PopulateBO(ClaimInfo.ClaimedItem.Color, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_CAPACITY)
                PopulateBO(ClaimInfo.ClaimedItem.Capacity, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_CARRIER)
                PopulateBO(ClaimInfo.ClaimedItem.Carrier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_QUALIFIER)
                PopulateBO(ClaimInfo.ClaimedItem.Identification.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_CODE)
                PopulateBO(ClaimInfo.ClaimedItem.Identification.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_ADDITIONAL_QUALIFIER)
                PopulateBO(ClaimInfo.ClaimedItem.AdditionalIdentification.Qualifier, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_CLAIM_ITEM_ADDITIONAL_CODE)
                PopulateBO(ClaimInfo.ClaimedItem.AdditionalIdentification.Code, TextValue)

                TextValue = TextBoxValue(Row, GRID_RECORD_PROBLEM_STATEMENT)
                PopulateBO(ClaimInfo.ProblemStatement, TextValue)

                ' should be an update really, but there is no way currently to identify the individual items
                PopulateBOFinancialLineItems(ClaimInfo, Row)

                ' (again) should be an update really, but there is no way currently to identify the individual items
                PopulateBOAdditionalInformationItems(ClaimInfo, Row)
            End If

        End Function

        Private Function PopulateBO(ByRef OriginalString As String, ByVal TextValue As String) As Boolean
            Dim UpdateOriginalValue As Boolean = False

            If (Not String.IsNullOrWhiteSpace(TextValue)) Then
                If (String.IsNullOrWhiteSpace(OriginalString)) Then
                    UpdateOriginalValue = True
                Else
                    If (Not OriginalString.Equals(TextValue)) Then
                        UpdateOriginalValue = True
                    End If
                End If

                If (UpdateOriginalValue) Then
                    OriginalString = TextValue
                End If
            End If

            Return UpdateOriginalValue

        End Function

        Private Function PopulateBO(ByRef OriginalAmount As FormattedAmount, ByVal TextValue As String) As Boolean
            Dim UpdateOriginalValue As Boolean = False

            If (Not String.IsNullOrWhiteSpace(TextValue)) Then
                Dim NewAmount As Decimal = Decimal.MaxValue

                If (Decimal.TryParse(TextValue, NewAmount)) Then
                    If (OriginalAmount.Amount Is Nothing) Then
                        UpdateOriginalValue = True
                    Else
                        If (OriginalAmount.Amount <> NewAmount) Then
                            UpdateOriginalValue = True
                        End If
                    End If
                End If

                If (UpdateOriginalValue) Then
                    ' coordinate the amount and amountinfo values together
                    OriginalAmount.Amount = NewAmount
                    OriginalAmount.AmountInfo = NewAmount.ToString()
                End If
            End If

            Return UpdateOriginalValue

        End Function

        Private Function PopulateBO(ByRef OriginalDate As FormattedDate, ByVal TextValue As String) As Boolean
            Dim UpdateOriginalValue As Boolean = False

            If (Not String.IsNullOrWhiteSpace(TextValue)) Then
                Dim NewDate As Date = DateTime.MaxValue

                If (Date.TryParse(TextValue, NewDate)) Then
                    If (OriginalDate.Date Is Nothing) Then
                        UpdateOriginalValue = True
                    Else
                        If (Not NewDate.Equals(OriginalDate.Date)) Then
                            UpdateOriginalValue = True
                        End If
                    End If
                End If

                If (UpdateOriginalValue) Then
                    ' coordinate the date and dateinfo values together
                    OriginalDate.Date = NewDate
                    OriginalDate.DateInfo = NewDate.ToString("yyyyMMdd")
                End If
            End If

            Return UpdateOriginalValue

        End Function

        Private Function PopulateBO(ByRef OriginalTimeSpan As TimeSpan, ByVal TextValue As String) As Boolean
            Dim UpdateOriginalValue As Boolean = False

            If (Not String.IsNullOrWhiteSpace(TextValue)) Then
                Dim NewTimeSpan As TimeSpan = TimeSpan.MaxValue

                If (TimeSpan.TryParse(TextValue, NewTimeSpan)) Then
                    If (OriginalTimeSpan <> NewTimeSpan) Then
                        UpdateOriginalValue = True
                    End If
                End If

                If (UpdateOriginalValue) Then
                    OriginalTimeSpan = NewTimeSpan
                End If

            End If

            Return UpdateOriginalValue

        End Function

        Private Function PopulateBOFinancialLineItems(ByRef ClaimInfo As ReportClaim, ByVal Row As GridViewRow) As Boolean

            Dim Grid As GridView = CType(Row.FindControl(GRID_FINANCIAL_LINE_INFO), GridView)
            Dim TextValue As String = Nothing
            Dim Info As FinancialInfo = Nothing
            Dim InfoList As New List(Of FinancialInfo)

            For Each GridRow As GridViewRow In Grid.Rows
                Info = New FinancialInfo With {
                    .RecordQualifier = RECORD_QUALIFIER_FLI,
                    .FinancialItemIdentification = New Identifier,
                    .FinancialItemSubIdentification = New Identifier,
                    .FinancialItemAdditionalIdentification = New Identifier,
                    .Amount = New FormattedAmount,
                    .OriginalAmount = New FormattedAmount,
                    .ConversionRate = New FormattedAmount,
                    .ConversionDate = New FormattedDate,
                    .ConversionTime = New FormattedTime
                }

                TextValue = TextBoxValue(GridRow, GRID_FLI_QUALIFIER)
                PopulateBO(Info.FinancialItemIdentification.Qualifier, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_CODE)
                PopulateBO(Info.FinancialItemIdentification.Code, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_SUB_QUALIFIER)
                PopulateBO(Info.FinancialItemSubIdentification.Qualifier, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_SUB_CODE)
                PopulateBO(Info.FinancialItemSubIdentification.Code, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_ADDITIONAL_QUALIFIER)
                PopulateBO(Info.FinancialItemAdditionalIdentification.Qualifier, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_ADDITIONAL_CODE)
                PopulateBO(Info.FinancialItemAdditionalIdentification.Code, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_CURRENCY)
                PopulateBO(Info.Amount.CurrencyCode, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_AMOUNT)
                PopulateBO(Info.Amount, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_ORIGINAL_CURRENCY)
                PopulateBO(Info.OriginalAmount.CurrencyCode, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_ORIGINAL_AMOUNT)
                PopulateBO(Info.OriginalAmount, TextValue)

                ' sync the decimal separator with both amount and original amount
                TextValue = TextBoxValue(GridRow, GRID_FLI_DECIMAL_SEPARATOR)
                PopulateBO(Info.Amount.AmountDecimalSeparator, TextValue)
                PopulateBO(Info.OriginalAmount.AmountDecimalSeparator, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_CONVERSION_RATE)
                PopulateBO(Info.ConversionRate, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_CONVERSION_DATE)
                PopulateBO(Info.ConversionDate, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_CONVERSION_DATE_FORMAT)
                PopulateBO(Info.ConversionDate.DateFormat, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_CONVERSION_TIME)
                PopulateBO(Info.ConversionTime.Time, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_FLI_CONVERSION_TIME_TIMEZONE)
                PopulateBO(Info.ConversionTime.TimeZoneCode, TextValue)

                InfoList.Add(Info)
            Next

            ClaimInfo.FinancialItems = InfoList.ToArray()

        End Function

        Private Sub PopulateBOAdditionalInformationItems(ByRef ClaimInfo As ReportClaim, ByVal Row As GridViewRow)

            Dim Grid As GridView = CType(Row.FindControl(GRID_ADDITIONAL_INFO), GridView)
            Dim TextValue As String = Nothing
            Dim Info As AdditionalInfo = Nothing
            Dim InfoList As New List(Of AdditionalInfo)

            For Each GridRow As GridViewRow In Grid.Rows
                Info = New AdditionalInfo With {
                    .RecordQualifier = RECORD_QUALIFIER_AIF
                }

                TextValue = TextBoxValue(GridRow, GRID_AIF_QUALIFIER)
                PopulateBO(Info.Qualifier, TextValue)

                TextValue = TextBoxValue(GridRow, GRID_AIF_INFORMATION)
                PopulateBO(Info.Information, TextValue)

                InfoList.Add(Info)
            Next

            ClaimInfo.AdditionalItems = InfoList.ToArray()

        End Sub

        Private Function TextBoxValue(ByRef Row As GridViewRow, ByVal TextBoxName As String) As String

            Dim oTextBox As TextBox = CType(Row.FindControl(TextBoxName), TextBox)
            Dim Value As String = Nothing

            If (oTextBox IsNot Nothing And Not String.IsNullOrWhiteSpace(oTextBox.Text)) Then
                Value = oTextBox.Text
            End If

            Return Value

        End Function

        Private Sub Grid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                If (e.Row.DataItem IsNot Nothing And (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator)) Then
                    Dim DetailRecord As FileDetailsRecordDto = CType(e.Row.DataItem, FileDetailsRecordDto)

                    If (DetailRecord IsNot Nothing And DetailRecord.RecordContents IsNot Nothing) Then
                        Dim ClaimInfo As ReportClaim = CType(DetailRecord.RecordContents, ReportClaim)

                        With e.Row
                            Dim oTextBox As TextBox = Nothing
                            Grid.Columns(GRID_COLUMN_ERRORS).Visible = False

                            If (ClaimInfo.Errors IsNot Nothing) Then
                                If (ClaimInfo.Errors.Any()) Then
                                    Dim ErrorsGrid As GridView = CType(.FindControl(GRID_ERRORS_CLAIM_RECORD), GridView)

                                    If (ErrorsGrid IsNot Nothing) Then
                                        TranslateGridHeader(ErrorsGrid)

                                        With ErrorsGrid
                                            .DataSource = ClaimInfo.Errors
                                            .DataBind()
                                        End With
                                    End If

                                    Grid.Columns(GRID_COLUMN_ERRORS).Visible = True
                                End If
                            End If

                            If (ClaimInfo.CertificateIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_CERTIFICATE_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CertificateIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_CERTIFICATE_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CertificateIdentifier.Code)
                            End If

                            If (ClaimInfo.CertificateIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_CERTIFICATE_SUB_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CertificateSubIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_CERTIFICATE_SUB_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CertificateSubIdentifier.Code)
                            End If

                            If (ClaimInfo.CertificateIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_CERTIFICATE_ADDITIONAL_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CertificateAdditionalIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_CERTIFICATE_ADDITIONAL_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CertificateAdditionalIdentifier.Code)
                            End If

                            If (ClaimInfo.LossDate IsNot Nothing) Then
                                If (ClaimInfo.LossDate.Date IsNot Nothing) Then
                                    oTextBox = CType(.FindControl(GRID_RECORD_LOSS_DATE), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.LossDate.Date.ToString())
                                End If

                                If (Not String.IsNullOrWhiteSpace(ClaimInfo.LossDate.DateFormat)) Then
                                    oTextBox = CType(.FindControl(GRID_RECORD_LOSS_DATE_FORMAT), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.LossDate.DateFormat)
                                End If
                            End If

                            If (ClaimInfo.LossTimeStart IsNot Nothing) Then
                                If (TimeSpan.Zero < ClaimInfo.LossTimeStart.Time) Then
                                    oTextBox = CType(.FindControl(GRID_RECORD_LOSS_TIME_START), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.LossTimeStart.Time.ToString())
                                End If

                                oTextBox = CType(.FindControl(GRID_RECORD_LOSS_TIMEZONE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.LossTimeStart.TimeZoneCode)
                            End If

                            If (ClaimInfo.LossTimeEnd IsNot Nothing) Then
                                If (TimeSpan.Zero < ClaimInfo.LossTimeEnd.Time) Then
                                    oTextBox = CType(.FindControl(GRID_RECORD_LOSS_TIME_END), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.LossTimeEnd.Time.ToString())
                                End If
                            End If

                            If (ClaimInfo.ItemIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_ITEM_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ItemIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_ITEM_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ItemIdentifier.Code)
                            End If

                            If (ClaimInfo.ItemSubIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_ITEM_SUB_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ItemSubIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_ITEM_SUB_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ItemSubIdentifier.Code)
                            End If

                            If (ClaimInfo.ItemAdditionalIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_ITEM_ADDITIONAL_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ItemAdditionalIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_ITEM_ADDITIONAL_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ItemAdditionalIdentifier.Code)
                            End If

                            If (ClaimInfo.CoverageTypeIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_COVERAGE_TYPE_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CoverageTypeIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_COVERAGE_TYPE_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CoverageTypeIdentifier.Code)
                            End If

                            If (ClaimInfo.CoverageTypeSubIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_COVERAGE_TYPE_SUB_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CoverageTypeSubIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_COVERAGE_TYPE_SUB_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CoverageTypeSubIdentifier.Code)
                            End If

                            If (ClaimInfo.CoverageTypeAdditionalIdentifier IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_COVERAGE_TYPE_ADDITIONAL_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CoverageTypeAdditionalIdentifier.Qualifier)

                                oTextBox = CType(.FindControl(GRID_RECORD_COVERAGE_TYPE_ADDITIONAL_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.CoverageTypeAdditionalIdentifier.Code)
                            End If

                            If (ClaimInfo.ClaimedItem IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_SKU), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Sku)

                                oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_MAKE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Make)

                                oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_MODEL), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Model)

                                oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_COLOR), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Color)

                                oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_CAPACITY), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Capacity)

                                oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_CARRIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Carrier)

                                If (ClaimInfo.ClaimedItem.Identification IsNot Nothing) Then
                                    oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_QUALIFIER), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Identification.Qualifier)

                                    oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_CODE), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.Identification.Code)
                                End If

                                If (ClaimInfo.ClaimedItem.AdditionalIdentification IsNot Nothing) Then
                                    oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_ADDITIONAL_QUALIFIER), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.AdditionalIdentification.Qualifier)

                                    oTextBox = CType(.FindControl(GRID_RECORD_CLAIM_ITEM_ADDITIONAL_CODE), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ClaimedItem.AdditionalIdentification.Code)
                                End If
                            End If

                            If (ClaimInfo.ProblemStatement IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_RECORD_PROBLEM_STATEMENT), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ClaimInfo.ProblemStatement)
                            End If

                            ' Populate the Financial Line Items Grid (FLI)
                            If (ClaimInfo.FinancialItems IsNot Nothing) Then

                                gvFinancialLineInfo = CType(.FindControl(GRID_FINANCIAL_LINE_INFO), GridView)

                                If (gvFinancialLineInfo IsNot Nothing) Then
                                    TranslateGridHeader(gvFinancialLineInfo)

                                    With gvFinancialLineInfo
                                        .DataSource = ClaimInfo.FinancialItems
                                        .DataBind()
                                    End With
                                End If
                            End If

                            'Populate the Additional Information Grid (AIF)
                            If (ClaimInfo.AdditionalItems IsNot Nothing) Then
                                gvAdditionalInfo = CType(.FindControl(GRID_ADDITIONAL_INFO), GridView)

                                If (gvAdditionalInfo IsNot Nothing) Then
                                    TranslateGridHeader(gvAdditionalInfo)

                                    With gvAdditionalInfo
                                        .DataSource = ClaimInfo.AdditionalItems
                                        .DataBind()
                                    End With
                                End If
                            End If

                        End With
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)

            End Try
        End Sub

        Protected Sub gvAdditionalInfo_DataBound(sender As Object, e As EventArgs) Handles gvAdditionalInfo.DataBound
            Dim oGrid As GridView = TryCast(sender, GridView)

            If (oGrid IsNot Nothing) Then
                Dim oPanel As Panel = TryCast(oGrid.Parent, Panel)

                If (oPanel IsNot Nothing) Then
                    Dim oLabel As Label = TryCast(oPanel.FindControl("AdditionalInformationItems"), Label)

                    If (oLabel IsNot Nothing) Then
                        oLabel.Text = TranslationBase.TranslateLabelOrMessage(oLabel.Text)

                    End If
                End If
            End If
        End Sub

        Protected Sub gvAdditionalInfo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAdditionalInfo.RowDataBound
            Try
                Dim AdditionalInfoGrid As GridView = TryCast(sender, GridView)

                If (AdditionalInfoGrid IsNot Nothing And e.Row.DataItem IsNot Nothing) Then
                    Dim oTextBox As TextBox = Nothing
                    Dim AdditionalInfoItem As AdditionalInfo = TryCast(e.Row.DataItem, AdditionalInfo)

                    If (AdditionalInfoItem IsNot Nothing) Then
                        With e.Row
                            AdditionalInfoGrid.Columns(GRID_AIF_COLUMN_ERRORS).Visible = False

                            If (AdditionalInfoItem.Errors IsNot Nothing) Then
                                If (AdditionalInfoItem.Errors.Any()) Then
                                    Dim ErrorsGrid As GridView = TryCast(.FindControl(GRID_ERRORS_ADDITIONAL_INFO), GridView)

                                    If (ErrorsGrid IsNot Nothing) Then
                                        TranslateGridHeader(ErrorsGrid)

                                        With ErrorsGrid
                                            .DataSource = AdditionalInfoItem.Errors
                                            .DataBind()
                                        End With
                                    End If

                                    AdditionalInfoGrid.Columns(GRID_AIF_COLUMN_ERRORS).Visible = True
                                End If
                            End If

                            oTextBox = CType(.FindControl(GRID_AIF_QUALIFIER), TextBox)
                            ThePage.PopulateControlFromBOProperty(oTextBox, AdditionalInfoItem.Qualifier)

                            oTextBox = CType(.FindControl(GRID_AIF_INFORMATION), TextBox)
                            ThePage.PopulateControlFromBOProperty(oTextBox, AdditionalInfoItem.Information)

                        End With
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)

            End Try
        End Sub

        Protected Sub gvFinanciallineInfo_DataBound(sender As Object, e As EventArgs) Handles gvFinancialLineInfo.DataBound
            Dim oGrid As GridView = TryCast(sender, GridView)

            If (oGrid IsNot Nothing) Then
                Dim oPanel As Panel = TryCast(oGrid.Parent, Panel)

                If (oPanel IsNot Nothing) Then
                    Dim oLabel As Label = TryCast(oPanel.FindControl("FinancialLineItems"), Label)

                    If (oLabel IsNot Nothing) Then
                        oLabel.Text = TranslationBase.TranslateLabelOrMessage(oLabel.Text)

                    End If
                End If
            End If
        End Sub

        Protected Sub gvFinanciallineInfo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFinancialLineInfo.RowDataBound
            Try
                Dim FinancialInfoGrid As GridView = TryCast(sender, GridView)

                If (FinancialInfoGrid IsNot Nothing And e.Row.DataItem IsNot Nothing) Then
                    Dim oTextBox As TextBox = Nothing
                    Dim FinancialInfoItem As FinancialInfo = TryCast(e.Row.DataItem, FinancialInfo)

                    If (FinancialInfoItem IsNot Nothing) Then
                        With e.Row
                            FinancialInfoGrid.Columns(GRID_FLI_COLUMN_ERRORS).Visible = False

                            If (FinancialInfoItem.Errors IsNot Nothing) Then
                                If (FinancialInfoItem.Errors.Any()) Then
                                    Dim ErrorsGrid As GridView = TryCast(.FindControl(GRID_ERRORS_FINANCIAL_LINE_INFO), GridView)

                                    If (ErrorsGrid IsNot Nothing) Then
                                        TranslateGridHeader(ErrorsGrid)

                                        With ErrorsGrid
                                            .DataSource = FinancialInfoItem.Errors
                                            .DataBind()
                                        End With
                                    End If

                                    FinancialInfoGrid.Columns(GRID_FLI_COLUMN_ERRORS).Visible = True
                                End If
                            End If

                            If (FinancialInfoItem.FinancialItemIdentification IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_FLI_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.FinancialItemIdentification.Qualifier)

                                oTextBox = CType(.FindControl(GRID_FLI_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.FinancialItemIdentification.Code)
                            End If

                            If (FinancialInfoItem.FinancialItemSubIdentification IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_FLI_SUB_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.FinancialItemSubIdentification.Qualifier)

                                oTextBox = CType(.FindControl(GRID_FLI_SUB_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.FinancialItemSubIdentification.Code)
                            End If

                            If (FinancialInfoItem.FinancialItemAdditionalIdentification IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_FLI_ADDITIONAL_QUALIFIER), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.FinancialItemAdditionalIdentification.Qualifier)

                                oTextBox = CType(.FindControl(GRID_FLI_ADDITIONAL_CODE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.FinancialItemAdditionalIdentification.Code)
                            End If

                            If (FinancialInfoItem.Amount IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_FLI_CURRENCY), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.Amount.CurrencyCode)

                                oTextBox = CType(.FindControl(GRID_FLI_AMOUNT), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ConvertToString(FinancialInfoItem.Amount.Amount))

                                oTextBox = CType(.FindControl(GRID_FLI_DECIMAL_SEPARATOR), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.Amount.AmountDecimalSeparator)
                            End If

                            If (FinancialInfoItem.OriginalAmount IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_FLI_ORIGINAL_CURRENCY), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.OriginalAmount.CurrencyCode)

                                oTextBox = CType(.FindControl(GRID_FLI_ORIGINAL_AMOUNT), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ConvertToString(FinancialInfoItem.OriginalAmount.Amount))
                            End If

                            If (FinancialInfoItem.ConversionRate IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_FLI_CONVERSION_RATE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, ConvertToString(FinancialInfoItem.ConversionRate.Amount))
                            End If

                            If (FinancialInfoItem.ConversionDate IsNot Nothing) Then
                                oTextBox = CType(.FindControl(GRID_FLI_CONVERSION_DATE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.ConversionDate.Date.ToString())

                                oTextBox = CType(.FindControl(GRID_FLI_CONVERSION_DATE_FORMAT), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.ConversionDate.DateFormat)
                            End If

                            If (FinancialInfoItem.ConversionTime IsNot Nothing) Then
                                If (TimeSpan.Zero < FinancialInfoItem.ConversionTime.Time) Then
                                    oTextBox = CType(.FindControl(GRID_FLI_CONVERSION_TIME), TextBox)
                                    ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.ConversionTime.Time.ToString())
                                End If

                                oTextBox = CType(.FindControl(GRID_FLI_CONVERSION_TIME_TIMEZONE), TextBox)
                                ThePage.PopulateControlFromBOProperty(oTextBox, FinancialInfoItem.ConversionTime.TimeZoneCode)
                            End If

                        End With
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)

            End Try
        End Sub

        Private Function ConvertToString(ByVal Amount As Decimal?) As String
            If (Amount Is Nothing Or Amount = 0) Then
                Return String.Empty
            Else
                Return Amount.ToString()
            End If
        End Function

        Private Function ConcatenatedErrorCodeMessage(ByVal Errors As FileManagerAdminService.Error()) As String

            Dim result As String = String.Empty

            For Each oError As FileManagerAdminService.Error In Errors

                result &= String.Format("({0}) {1};", oError.Code, oError.Message)

            Next

            Return result

        End Function

#End Region

#Region "Populate"
        Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)
            Try
                If (State.SearchDV Is Nothing OrElse refreshData) Then
                    LoadFileDetailRecords(refreshData)

                    If (State.SearchDV IsNot Nothing) Then
                        Grid.DataSource = State.SearchDV

                        Grid.PageSize = State.PagingInfo.PageSize
                        Grid.PageIndex = State.PagingInfo.PageIndex
                        Grid.VirtualItemCount = State.CallingPageParams.FileInfoParams.RecordCount

                        Grid.DataBind()
                    End If

                    EnableDisableUserControls()
                End If

            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub

        Private Sub LoadFileDetailRecords(Optional ByVal refreshData As Boolean = False)
            Try
                Dim SearchInfo As SearchFilter = Nothing

                If (State.CallingPageParams IsNot Nothing And
                    State.CallingPageParams.FileInfoParams IsNot Nothing And
                    State.PagingInfo IsNot Nothing) Then

                    SearchInfo = New SearchFilter With
                    {
                        .RecordState = State.CallingPageParams.FileInfoParams.RecordState,
                        .PagingFilter = State.PagingInfo
                    }
                End If

                State.SearchDV = WcfClientHelper.Execute(Of FileManagerAdminClient, FileManagerAdmin, FileDetailsRecordDto())(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As FileManagerAdminClient)
                        Return c.SearchFileDetailsRecords(State.CallingPageParams.DataItemLocatorInfo, SearchInfo)
                    End Function)

            Catch ex As FaultException
                ThrowWebServiceFaultException(ex)

            End Try
        End Sub

        Private Function SaveFileDetailRecord(ByVal FileDetailsRecord As FileDetailsRecordDto, Optional ByVal refreshData As Boolean = False) As Boolean

            Dim wsResponse As Boolean = False

            Try
                wsResponse = WcfClientHelper.Execute(Of FileManagerAdminClient, FileManagerAdmin, Boolean)(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As FileManagerAdminClient)
                        Return c.SaveFileDetailsRecord(
                            FileDetailsRecord.Locator,
                            FileDetailsRecord)
                    End Function)

            Catch ex As FaultException
                ThrowWebServiceFaultException(ex)

            End Try

            Return wsResponse

        End Function

        '' <summary>
        '' Gets New Instance of File Admin Service Client with Credentials Configured
        '' </summary>
        '' <returns>Instance of <see cref="FileAdminClient"/></returns>
        Private Shared Function GetClient() As FileManagerAdminClient
            Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__FILE_MANAGEMENT_ADMIN_SERVICE), False)

            Dim client = New FileManagerAdminClient(ServiceEndPointName, oWebPasswd.Url)

            client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            client.ClientCredentials.UserName.Password = oWebPasswd.Password

            Return client
        End Function

        Private Sub ThrowWebServiceFaultException(fex As FaultException)

            Log(fex)
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR) & " - " & fex.Message, False)

        End Sub

#End Region

    End Class

End Namespace
