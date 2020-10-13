Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Linq
Imports System.Xml.Linq

Public Class ContractValidation
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "ContractValidation"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Private Const YESNO As String = "YESNO"
#End Region

#Region "Constructors"

    Public Sub New(ds As ContractValidationDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyContractValidation = ds
    End Sub

    Public Sub New(ds As ContractValidationDs, xml As String)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyContractValidation = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyContractValidation As ContractValidationDs
    Dim _uploadSessionId As String

    Dim _uploadSessionGuidId As Guid = Guid.Empty

    Dim claimBO As Claim
    Dim claimFamilyBO As Claim

    Private Sub MapDataSet(ds As ContractValidationDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ds As ContractValidationDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            'Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ContractValidation Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As ContractValidationDs)
        Try
            If ds.ContractValidation.Count = 0 Then Exit Sub
            With ds.ContractValidation.Item(0)
                uploadSessionId = .UPLOAD_SESSION_ID
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property UploadSessionId As String
        Get
            Return _uploadSessionId
        End Get
        Set
            _uploadSessionId = Value
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            'Load the pre-validated Contract records
            Dim dsPreValidatedContractRecords As DataSet = ContractUpload.GetPreValidatedContractsForUpload(UploadSessionId)
            If dsPreValidatedContractRecords IsNot Nothing AndAlso dsPreValidatedContractRecords.Tables(0) IsNot Nothing AndAlso dsPreValidatedContractRecords.Tables(0).Rows.Count > 0 Then
                Dim preValidatedContractRow As DataRow
                Dim objContract As Contract

                Dim contractErrors As ValidationError()
                Dim strValidationErrors As String
                Dim index As Integer

                For Each preValidatedContractRow In dsPreValidatedContractRecords.Tables(0).Rows
                    Dim preValidatedContractId As New Guid(CType(preValidatedContractRow("contract_upload_id"), Byte()))
                    Dim objContractUpload As New ContractUpload(preValidatedContractId)

                    BuildContract(objContract, objContractUpload)

                    If objContract IsNot Nothing Then
                        contractErrors = objContract.ValidationErrors()
                        If contractErrors.Length > 0 Then
                            strValidationErrors = "Record Number " & preValidatedContractRow.Item("record_number").ToString & ": "
                            For index = 0 To contractErrors.Length - 1
                                strValidationErrors &= (index + 1).ToString & "-" & CType(contractErrors.GetValue(index), Assurant.Common.Validation.ValidationError).PropertyName & "; Error Message= " & TranslationBase.TranslateLabelOrMessage(CType(contractErrors.GetValue(index), Assurant.Common.Validation.ValidationError).Message)
                                If index < contractErrors.Length - 1 Then
                                    strValidationErrors &= ". "
                                End If
                            Next
                            objContractUpload.ValidationErrors = strValidationErrors
                            'ContractUpload.UpdatePreValidatedContractRecord(preValidatedContractId, strValidationErrors)
                            objContractUpload.Save()
                        End If
                    End If

                Next

            End If

            Return XMLHelper.GetXML_OK_Response

            ''Return XML_VERSION_AND_ENCODING & "" 'outputXml.ToString

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function CheckDBNull(obj As Object) As Object
        If DBNull.Value.Equals(obj) Then
            If obj.GetType Is GetType(DecimalType) Then
                Return 0
            Else
                Return Nothing
            End If
        Else
            Return obj
        End If
    End Function

    Private Sub ComposeResult(ByRef outputXml As XElement, parentItemNumber As String, itemNumber As String, claimNumber As String, Optional ByVal errorCode As String = "", Optional ByVal propertyName As String = "")
        Dim result As String = ""
        Dim errXml As XElement
        Dim userInfo As String
        userInfo = "User:" & ElitaPlusIdentity.Current.ActiveUser.NetworkId & "; Date:" & Date.Now.ToString("s") & TimeZoneInfo.Local.ToString.Substring(4, 6)

        If errorCode Is Nothing OrElse errorCode = "" Then
            result = "OK"
        Else
            If propertyName IsNot Nothing AndAlso propertyName <> "" Then
                propertyName = propertyName & ": "
            End If

            result = "ERROR"
            errXml = <ERROR>
                         <CODE><%= errorCode %></CODE>
                         <MESSAGE><%= propertyName %><%= TranslationBase.TranslateLabelOrMessage(errorCode) %></MESSAGE>
                         <ERROR_INFO><%= userInfo %></ERROR_INFO>
                     </ERROR>
        End If

        Dim tranDataRecXml As XElement = <TRANSACTION_DATA_RECORD>
                                             <PARENT_ITEM_NUMBER>
                                                 <%= parentItemNumber %>
                                             </PARENT_ITEM_NUMBER>
                                             <ITEM_NUMBER>
                                                 <%= itemNumber %>
                                             </ITEM_NUMBER>
                                             <CLAIM_NUMBER>
                                                 <%= claimNumber %>
                                             </CLAIM_NUMBER>
                                             <RESULT>
                                                 <%= result %>
                                             </RESULT>
                                         </TRANSACTION_DATA_RECORD>


        If errXml IsNot Nothing AndAlso errXml.HasElements Then
            tranDataRecXml.Add(errXml)
        End If

        outputXml.Add(tranDataRecXml)
    End Sub

    Public Sub BuildContract(ByRef objContract As Contract, objContractUpload As ContractUpload)

        If objContractUpload IsNot Nothing Then
            objContract = New Contract
            With objContract

                objContract.DealerId = objContractUpload.DealerId
                objContract.ContractTypeId = objContractUpload.ContractTypeId
                objContract.Effective = objContractUpload.Effective
                objContract.Expiration = objContractUpload.Expiration
                objContract.CommissionsPercent = objContractUpload.CommissionsPercent
                objContract.MarketingPercent = objContractUpload.MarketingPercent
                objContract.AdminExpense = objContractUpload.AdminExpense
                objContract.ProfitPercent = objContractUpload.ProfitPercent
                objContract.LossCostPercent = objContractUpload.LossCostPercent
                objContract.CurrencyId = objContractUpload.CurrencyId
                objContract.TypeOfMarketingId = objContractUpload.TypeOfMarketingId
                objContract.TypeOfEquipmentId = objContractUpload.TypeOfEquipmentId
                objContract.TypeOfInsuranceId = objContractUpload.TypeOfInsuranceId
                objContract.MinReplacementCost = objContractUpload.MinReplacementCost
                objContract.WarrantyMaxDelay = objContractUpload.WarrantyMaxDelay
                objContract.NetCommissionsId = objContractUpload.NetCommissionsId
                objContract.NetMarketingId = objContractUpload.NetMarketingId
                objContract.NetTaxesId = objContractUpload.NetTaxesId
                objContract.Deductible = objContractUpload.Deductible
                objContract.WaitingPeriod = objContractUpload.WaitingPeriod
                objContract.FundingSourceId = objContractUpload.FundingSourceId
                objContract.EditModelId = objContractUpload.EditModelId
                objContract.DealerMarkupId = objContractUpload.DealerMarkupId
                objContract.AutoMfgCoverageId = objContractUpload.AutoMfgCoverageId
                objContract.RestrictMarkupId = objContractUpload.RestrictMarkupId
                objContract.Layout = objContractUpload.Layout
                objContract.SuspenseDays = objContractUpload.SuspenseDays
                objContract.CancellationDays = objContractUpload.CancellationDays
                objContract.Comment1 = objContractUpload.Comment1
                objContract.FixedEscDurationFlag = objContractUpload.FixedEscDurationFlag
                objContract.Policy = objContractUpload.Policy
                objContract.ReplacementPolicyId = objContractUpload.ReplacementPolicyId
                objContract.CoinsuranceId = objContractUpload.CoinsuranceId
                objContract.ParticipationPercent = objContractUpload.ParticipationPercent
                objContract.ID_Validation_Id = objContractUpload.IdValidationId
                objContract.ClaimControlID = objContractUpload.ClaimControlId
                objContract.RatingPlan = objContractUpload.RatingPlan
                objContract.CurrencyConversionId = objContractUpload.CurrencyConversionId
                objContract.CurrencyOfCoveragesId = objContractUpload.CurrencyOfCoveragesId
                objContract.RemainingMFGDays = objContractUpload.RemainingMfgDays
                objContract.Acsel_Prod_Code_Id = objContractUpload.AcselProdCodeId
                objContract.CancellationReasonId = objContractUpload.CancellationReasonId
                objContract.FullRefundDays = objContractUpload.FullRefundDays
                objContract.AutoSetLiabilityId = objContractUpload.AutoSetLiabilityId
                objContract.DeductiblePercent = objContractUpload.DeductiblePercent
                objContract.CoverageDeductibleId = objContractUpload.CoverageDeductibleId
                objContract.IgnoreIncomingPremiumID = objContractUpload.IgnoreIncomingPremiumId
                objContract.RepairDiscountPct = objContractUpload.RepairDiscountPct
                objContract.ReplacementDiscountPct = objContractUpload.ReplacementDiscountPct
                objContract.IgnoreCoverageAmtId = objContractUpload.IgnoreCoverageAmtId
                objContract.BackEndClaimsAllowedId = objContractUpload.BackendClaimsAllowedId
                objContract.EditMFGTermId = objContractUpload.EditMfgTermId
                objContract.AcctBusinessUnitId = objContractUpload.AcctBusinessUnitId
                objContract.IgnoreWaitingPeriodWsdPsd = objContractUpload.IgnoreWaitingPeriodWsdPsd
                objContract.InstallmentPaymentId = objContractUpload.InstallmentPaymentId
                objContract.DaysOfFirstPymt = objContractUpload.DaysOfFirstPymt
                objContract.DaysToSendLetter = objContractUpload.DaysToSendLetter
                objContract.DaysToCancelCert = objContractUpload.DaysToCancelCert
                objContract.DeductibleByManufacturerId = objContractUpload.DeductByMfgId
                objContract.PenaltyPct = objContractUpload.PenaltyPct
                objContract.ClipPercent = objContractUpload.ClipPercent
                objContract.BaseInstallments = objContractUpload.BaseInstallments
                objContract.BillingCycleFrequency = objContractUpload.BillingCycleFrequency
                objContract.MaxInstallments = objContractUpload.MaxInstallments
                objContract.InstallmentsBaseReducer = objContractUpload.InstallmentsBaseReducer
                objContract.IsCommPCodeId = objContractUpload.IsCommPCodeId
                objContract.PastDueMonthsAllowed = objContractUpload.PastDueMonthsAllowed
                objContract.CollectionReAttempts = objContractUpload.CollectionReAttempts
                objContract.IncludeFirstPmt = objContractUpload.IncludeFirstPmt
                objContract.CollectionCycleTypeId = objContractUpload.CollectionCycleTypeId
                objContract.CycleDay = objContractUpload.CycleDay
                objContract.OffsetBeforeDueDate = objContractUpload.OffsetBeforeDueDate
                objContract.InsPremiumFactor = objContractUpload.InsPremiumFactor
                objContract.ExtendCoverageId = objContractUpload.ExtendCoverageId
                objContract.ExtraMonsToExtendCoverage = objContractUpload.ExtraMonsToExtendCoverage
                objContract.ExtraDaysToExtendCoverage = objContractUpload.ExtraDaysToExtendCoverage
                objContract.AllowDifferentCoverage = objContractUpload.AllowDifferentCoverage
                objContract.AllowNoExtended = objContractUpload.AllowNoExtended
                objContract.NumOfClaims = objContractUpload.NumOfClaims
                objContract.ClaimLimitBasedOnId = objContractUpload.ClaimLimitBasedOnId
                objContract.DaysToReportClaim = objContractUpload.DaysToReportClaim
                objContract.MarketingPromotionId = objContractUpload.MarketingPromoId
                objContract.CustmerAddressRequiredId = objContractUpload.CustAddressRequiredId
                objContract.FirstPymtMonths = objContractUpload.FirstPymtMonths
                objContract.DeductibleBasedOnId = objContractUpload.DeductibleBasedOnId
                objContract.AllowMultipleRejectionsId = objContractUpload.AllowMultipleRejectionsId
                objContract.ProRataMethodId = objContractUpload.ProRataMethodId
                objContract.PayOutstandingPremiumId = objContractUpload.PayOutstandingPremiumId
                objContract.AllowPymtSkipMonths = objContractUpload.AllowPymtSkipMonths
                objContract.AuthorizedAmountMaxUpdates = objContractUpload.AuthorizedAmountMaxUpdates
                objContract.DaysToReactivate = objContractUpload.NumberOfDaysToReactivate
                objContract.RecurringPremiumId = objContractUpload.RecurringPremiumId
                objContract.RecurringWarrantyPeriod = objContractUpload.RecurringWarrantyPeriod
                objContract.BillingCycleTypeId = objContractUpload.BillingCycleTypeId
                objContract.DailyRateBasedOnId = objContractUpload.DailyRateBasedOnId
                objContract.AllowBillingAfterCancellation = objContractUpload.AllowBillingAfterCncltn
                objContract.AllowCollectionAfterCancellation = objContractUpload.AllowCollctnAfterCncltn
                objContract.ReplacementPolicyClaimCount = objContractUpload.ReplacementPolicyClaimCount
                objContract.FutureDateAllowForID = objContractUpload.FutureDateAllowForId
                objContract.AllowCoverageMarkupDistribution = objContractUpload.AllowCoverageMarkupDtbn
                objContract.NumOfRepairClaims = objContractUpload.NumOfRepairClaims
                objContract.NumOfReplacementClaims = objContractUpload.NumOfReplacementClaims
                objContract.PaymentProcessingTypeId = objContractUpload.PaymentProcessingTypeID
                objContract.ThirdPartyName = objContractUpload.ThirdPartyName
                objContract.ThirdPartyTaxID = objContractUpload.ThirdPartyTaxID
                objContract.RDOName = objContractUpload.RDOName
                objContract.RDOPercent = objContractUpload.RDOPercent
                objContract.RdoTaxId = objContractUpload.RDOTaxID
                objContract.PolicyTypeId = objContractUpload.PolicyTypeCode
                objContract.PolicyGenerationId = objContractUpload.PolicyGenerationCode
                objContract.LineOfBusinessId = objContractUpload.LineOfBusinessCode



            End With

        End If

    End Sub





#End Region

#Region "Extended Properties"

#End Region

End Class
