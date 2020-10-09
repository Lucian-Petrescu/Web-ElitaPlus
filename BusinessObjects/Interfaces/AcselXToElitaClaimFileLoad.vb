Imports System.Text
Imports System.Collections.Generic
Imports Assurant.Common.MessagePublishing

Public Class AcselXToElitaClaimFileLoad
    Inherits FileLoadBase(Of ClaimloadFileProcessed, ClaimloadReconWrk)

#Region "Constructor"
    Public Sub New(threadCount As Integer, transactionSize As Integer)
        MyBase.New(True) '' Custom Save Constructor
        YesId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
        NoId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
    End Sub

    Public Sub New()
        MyBase.New(True) '' Custom Save Constructor
        YesId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
        NoId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
    End Sub
#End Region

#Region "Fields"
    Private _claimloadFileProcessed As ClaimloadFileProcessed
    Private ReadOnly YesId As Guid
    Private ReadOnly NoId As Guid
    Private _claim As Claim
#End Region

#Region "Constants"
    Private Const DedCollIssue = "DEDCOLL"
    Private Const DedPaidQuestion = "DEDCOLLPAID"

    'Claim Issue Response Constants'
    Public Const COL_NAME_CLAIM_ISSUE_RESPONSE_ID As String = "claim_issue_response_id"
    Public Const COL_NAME_ANSWER_ID As String = "answer_id"
    Public Const COL_NAME_ANSWER_VALUE As String = "answer_value"
    Public Const COL_NAME_SUPPORTS_CLAIM_ID As String = "supports_claim_id"
    Public Const COL_NAME_CLAIM_ISSUE_ID As String = "claim_issue_id"

    Public Const RECORD_TYPE_DN = "DN"
    Public Const RECORD_TYPE_DC = "DC"

    Public Const ANSWER_VAL_YES = "Yes"
    Public Const ANSWER_VAL_NO = "No"

    'Claim Issue Statuses'
    Public Const STATUS_WAIVED As String = "WAIVED"
    Public Const STATUS_RESOLVED As String = "RESOLVED"
    Public Const STATUS_REJECTED As String = "REJECTED"
    Public Const STATUS_PENDING As String = "PENDING"
    Public Const STATUS_OPEN As String = "OPEN"
    Public Const STATUS_CLOSED As String = "CLOSED"


#End Region




#Region "Properties"
    Private Property ClaimLoadFileProcessed As ClaimloadFileProcessed
        Get
            Return _claimloadFileProcessed
        End Get
        Set
            _claimloadFileProcessed = value
        End Set
    End Property

    Private Property Claim As Claim
        Get
            Return _claim
        End Get
        Set
            _claim = value
        End Set
    End Property
#End Region


    Protected Overrides Function CreateFileLoadHeader(fileLoadHeaderId As System.Guid) As ClaimloadFileProcessed
        ClaimLoadFileProcessed = New ClaimloadFileProcessed(fileLoadHeaderId)
        Return ClaimLoadFileProcessed
    End Function

    Protected Overrides Function CreateFileLoadDetail(fileLoadDetailId As System.Guid, headerRecord As ClaimloadFileProcessed) As ClaimloadReconWrk
        Dim returnValue As ClaimloadReconWrk
        returnValue = New ClaimloadReconWrk(fileLoadDetailId, headerRecord.Dataset)
        Return returnValue
    End Function

    Public Overrides Sub AfterCreateFileLoadHeader()
        MyBase.AfterCreateFileLoadHeader()

    End Sub

    Protected Overrides Function ProcessDetailRecord(reconRecord As ClaimloadReconWrk, familyDataSet As System.Data.DataSet) As ProcessResult
        Try
            Dim claim As Claim
            Dim gClaimIssueResponseId As Guid = Guid.Empty
            Dim gAnswerId As Guid = Guid.Empty
            Dim objClaimIssue As ClaimIssue
            Dim softQuestionId As Guid
            ' Dim ClaimIssue As ClaimIssue
            Dim claimIssueId As Guid
            Dim issueId As Guid

            ' Create Instance of Claim based on Recon Record
            claim = ClaimFacade.Instance.GetClaimByDealerCodeandClaimNumber(Of Claim)(reconRecord.DealerCode, reconRecord.ClaimNumber) '' Create New DataSet
            familyDataSet = claim.Dataset

            'Get the Deductible Collection issue from the list of issues
            Dim claimIssue As ClaimIssue = Nothing
            For Each ci As ClaimIssue In claim.ClaimIssuesList
                If (ci.IssueCode = DedCollIssue) Then
                    claimIssueId = ci.ClaimIssueId
                    issueId = ci.IssueId
                    claimIssue = ci
                    Exit For
                End If
            Next

            Dim dvQuestions As DataView = claimIssue.ClaimIssueQuestionListByDealer(claim.Dealer.Id).Table.DefaultView

            For Each dr As DataRow In dvQuestions.Table.Rows
                softQuestionId = New Guid(CType(dr("soft_question_id"), Byte()))
                Dim oQuestion As New Question(softQuestionId)
                If (oQuestion.Code = DedPaidQuestion) Then
                    ''''
                    'get the claimissueresponse id
                    For i As Integer = 0 To claimIssue.ClaimIssueResponseList.Table.Rows.Count - 1
                        'DEF-4069
                        If Not (claimIssue.ClaimIssueResponseList.Table.Rows(i).RowState = DataRowState.Deleted) Then
                            If claimIssue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID) IsNot DBNull.Value Then
                                gAnswerId = New Guid(CType(claimIssue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID), Byte()))
                                Dim oAnswer As New Answer(gAnswerId)
                                If oAnswer.QuestionId = softQuestionId Then
                                    gClaimIssueResponseId = New Guid(CType(claimIssue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_CLAIM_ISSUE_RESPONSE_ID), Byte()))
                                    Exit For
                                End If
                            End If
                        End If
                    Next

                    ''''''''''''''''''''''''''''''''''''''
                    'save data to claim response table
                    Dim oClaimIssueResponse As ClaimIssueResponse
                    If Not GetClaimIssueResponseId(softQuestionId, claimIssue) = Guid.Empty Then
                        oClaimIssueResponse = claimIssue.GetClaimIssueResponseChild(GetClaimIssueResponseId(softQuestionId, claimIssue))
                    Else
                        oClaimIssueResponse = claimIssue.GetNewClaimIssueResponseChild()
                    End If


                    With oClaimIssueResponse
                        .ClaimIssueId = claimIssueId

                        Dim dvAnswers As DataView = oQuestion.AnswerChildren.Table.DefaultView

                        If (reconRecord.RecordType = RECORD_TYPE_DN) Then
                            dvAnswers.RowFilter = "ANSWER_VALUE='No'"
                            If dvAnswers.Count = 1 Then
                                .AnswerId = GuidControl.ByteArrayToGuid(dvAnswers.Item(0)("Answer_Id"))
                                .SupportsClaimId = GuidControl.ByteArrayToGuid(dvAnswers.Item(0)("supports_claim_id"))
                                .AnswerDescription = dvAnswers.Item(0)("Description")
                                .AnswerValue = GuidControl.GuidToHexString(GuidControl.ByteArrayToGuid(dvAnswers.Item(0)("list_item_id")))
                                claim.StatusCode = Codes.CLAIM_STATUS__DENIED
                            End If
                        Else
                            dvAnswers.RowFilter = "ANSWER_VALUE='Yes'"
                            If dvAnswers.Count = 1 Then
                                .AnswerId = GuidControl.ByteArrayToGuid(dvAnswers.Item(0)("Answer_Id"))
                                .SupportsClaimId = GuidControl.ByteArrayToGuid(dvAnswers.Item(0)("supports_claim_id"))
                                .AnswerDescription = dvAnswers.Item(0)("Description")
                                .AnswerValue = GuidControl.GuidToHexString(GuidControl.ByteArrayToGuid(dvAnswers.Item(0)("list_item_id")))
                            End If
                        End If

                        .Save()
                        Dim oClaimIssueStatus As ClaimIssueStatus = claimIssue.GetNewClaimIssueStatusChild()
                        Dim _claimissuestatuscode As String
                        With oClaimIssueStatus
                            .ClaimIssueId = claimIssue.ClaimIssueId
                            .ProcessedDate = DateTime.Now()
                            .ProcessedBy = ElitaPlusIdentity.Current.ActiveUser.UserName
                            _claimissuestatuscode = SetClaimIssueStatus(claimIssue, claim, False)
                            .ClaimIssueStatusCId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ISSUE_STATUS, _claimissuestatuscode)
                            .Save()
                        End With

                        Dim _eventtypeid As Guid
                        _eventtypeid = LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__DEVICE_RECEPTION_DATE_RECEIVED)
                        Dim _claimbase As ClaimBase = claimIssue.Claim
                        With claimIssue.Claim
                            Dim argumentsToAddEvent As String = "ClaimId:" & DALBase.GuidToSQLString(.Id) & ";ClaimIssueId:" & DALBase.GuidToSQLString(claimIssue.ClaimIssueId)
                            PublishedTask.AddEvent(
                                companyGroupId:= .Company.CompanyGroupId,
                                companyId:=claim.CompanyId,
                                countryId:= .Company.CountryId,
                                dealerId:=claim.Dealer.Id,
                                productCode:= .Certificate.ProductCode,
                                coverageTypeId:= .CertificateItemCoverage.CoverageTypeId,
                                sender:="ClaimIssue_StatusChange",
                                arguments:=argumentsToAddEvent,
                                eventDate:=DateTime.UtcNow,
                                eventTypeId:=_eventtypeid,
                                eventArgumentId:=Guid.Empty)
                        End With

                        claimIssue.StatusCode = SetClaimIssueStatus(claimIssue, claim, False)
                        claimIssue.ProcessedDate = DateTime.Now()
                        claimIssue.ProcessedBy = ElitaPlusIdentity.Current.ActiveUser.UserName
                        claimIssue.Save()
                        claim.Save()

                        If (reconRecord.RecordType = RECORD_TYPE_DC) Then
                            If (Not claim.AllIssuesResolvedOrWaived) Then
                                claim.StatusCode = Codes.CLAIM_STATUS__PENDING
                            Else
                                claim.StatusCode = Codes.CLAIM_STATUS__ACTIVE
                            End If
                            claim.Save()
                        End If

                    End With
                    Exit For
                End If
            Next

            Return (ProcessResult.Loaded)
        Catch ex As DataBaseAccessException
            AppConfig.Log(DirectCast(ex, Exception))
            If (ex.ErrorType = DataBaseAccessException.DatabaseAccessErrorType.BusinessErr) Then
                If (ex.Code Is Nothing OrElse ex.Code.Trim().Length = 0) Then
                    reconRecord.RejectReason = "Rejected During Load process"
                Else
                    reconRecord.RejectReason = TranslationBase.TranslateLabelOrMessage(ex.Code)
                    reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
                End If
            Else
                reconRecord.RejectReason = "Rejected During Load process"
            End If
            reconRecord.RejectCode = "000"
            Return ProcessResult.Rejected
        Catch ex As BOValidationException
            AppConfig.Log(DirectCast(ex, Exception))
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = ex.ToRejectReason()
            reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
            Return ProcessResult.Rejected
        Catch ex As Exception
            AppConfig.Log(ex)
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = "Rejected During Load process"
            Return ProcessResult.Rejected
        End Try
    End Function

    Private Function GetClaimIssueResponseId(gSoftQuestionId As Guid, claimissue As ClaimIssue) As Guid
        Dim gClaimIssueResponseId As Guid = Guid.Empty
        Dim gAnswerId As Guid = Guid.Empty
        For i As Integer = 0 To claimissue.ClaimIssueResponseList.Table.Rows.Count - 1
            'DEF-4069
            If Not (claimissue.ClaimIssueResponseList.Table.Rows(i).RowState = DataRowState.Deleted) Then
                If claimissue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID) IsNot DBNull.Value Then
                    gAnswerId = New Guid(CType(claimissue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID), Byte()))
                    Dim oAnswer As New Answer(gAnswerId)
                    If oAnswer.QuestionId = gSoftQuestionId Then
                        gClaimIssueResponseId = New Guid(CType(claimissue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_CLAIM_ISSUE_RESPONSE_ID), Byte()))
                        Exit For
                    End If
                End If
            End If
        Next
        Return gClaimIssueResponseId
    End Function

    Private Function SetClaimIssueStatus(claimIssue As ClaimIssue, claim As Claim, Optional ByVal bWaived As Boolean = False) As String
        Dim sStatus As String = STATUS_PENDING
        Dim sOutcome As String = ""
        Dim gSupportsClaimId As Guid
        Dim sSupportsClaim As String = ""
        Dim iQuestionCount As Integer = 0


        For i As Integer = 0 To claimIssue.ClaimIssueResponseList.Table.Rows.Count - 1
            'DEF-4069
            If Not claimIssue.ClaimIssueResponseList.Table.Rows(i).RowState = DataRowState.Deleted Then
                If claimIssue.ClaimIssueId.Equals(New Guid(CType(claimIssue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_CLAIM_ISSUE_ID), Byte()))) Then
                    If claimIssue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_SUPPORTS_CLAIM_ID) IsNot DBNull.Value Then
                        iQuestionCount = iQuestionCount + 1
                        gSupportsClaimId = New Guid(CType(claimIssue.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_SUPPORTS_CLAIM_ID), Byte()))
                        sSupportsClaim = LookupListNew.GetCodeFromId(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, gSupportsClaimId).ToString()
                        Select Case sSupportsClaim
                            Case "Y"
                                sOutcome += "Y"
                            Case "N"
                                sOutcome += "N"
                            Case Else
                                sOutcome += "P"
                        End Select
                    End If
                End If
            End If
        Next

        If sOutcome.Contains("Y") And iQuestionCount = claimIssue.ClaimIssueQuestionListByDealer(claim.Dealer.Id).Table.Rows.Count Then
            sStatus = STATUS_RESOLVED
        End If
        If sOutcome.Contains("P") Then
            sStatus = STATUS_PENDING
        End If
        If sOutcome.Contains("N") Then
            sStatus = STATUS_REJECTED
        End If

        Return sStatus 'LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ISSUE_STATUS, sStatus)
    End Function

    Protected Overrides Sub CustomSave(headerRecord As ClaimloadFileProcessed)
        MyBase.CustomSave(headerRecord)
        headerRecord.Save()
    End Sub
End Class
