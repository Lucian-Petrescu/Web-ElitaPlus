'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/25/2017)  ********************
Imports System.Globalization
Imports System.Threading

Public Class CaseBase
    Inherits BusinessObjectBase
#Region "Constants"
    Private Const SearchException As String = "SEARCH_CRITERION_001" ' Case List Search Exception - If no criterion(except companyid) is selected
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
    End Sub
    Public Sub New(ByVal caseNumber As String, ByVal companyCode As String)
        MyBase.New()
        Dataset = New DataSet
        Load(CaseNumber, CompanyCode)
    End Sub
    Protected Sub Load()
        Try
            If Dataset.Tables.IndexOf(CaseDAL.TableName) < 0 Then
                Dim dal As New CaseDAL
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(CaseDAL.TableName).NewRow
            Dataset.Tables(CaseDAL.TableName).Rows.Add(newRow)
            Row = newRow
            SetValue(CaseDAL.TableKeyName, Guid.NewGuid)
            Initialize()
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(CaseDAL.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(CaseDAL.TableName) >= 0 Then
                Row = FindRow(id, CaseDAL.TableKeyName, Dataset.Tables(CaseDAL.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New CaseDAL
                dal.Load(Dataset, id)
                Row = FindRow(id, CaseDAL.TableKeyName, Dataset.Tables(CaseDAL.TableName))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Protected Overridable Sub Load(ByVal caseNumber As String, ByVal companyCode As String)
        Try
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(CaseDAL.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(CaseDAL.TableName) >= 0 Then
                Row = FindRow(CaseNumber, CaseDAL.ColNameCaseNumber, Dataset.Tables(CaseDAL.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New CaseDAL
                dal.LoadCaseByCaseNumber(Dataset, CaseNumber, CompanyCode)
                Row = FindRow(CaseNumber, CaseDAL.ColNameCaseNumber, Dataset.Tables(CaseDAL.TableName))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CaseDAL.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameCaseId), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCompanyId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameCompanyId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCompanyId, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=70)>
    Public Property CompanyDesc() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCompanyDesc) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCompanyDesc), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCompanyDesc, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=120)>
    Public Property CaseNumber() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseNumber) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCaseNumber), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseNumber, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CaseOpenDate() As DateType
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseOpenDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CaseDAL.ColNameCaseOpenDate), Date))
            End If
        End Get
        Set(ByVal value As DateType)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseOpenDate, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property CasePurposeCode() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCasePurposeCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCasePurposeCode), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCasePurposeCode, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property CaseStatusCode() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseStatusCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCaseStatusCode), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseStatusCode, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property InitialInteractionId() As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameInitialInteractionId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameInitialInteractionId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CaseDAL.ColNameInitialInteractionId, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property InitialCallerName() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameInitialCallerName) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameInitialCallerName), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameInitialCallerName, Value)
        End Set
    End Property

    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameClaimId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameClaimId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CaseDAL.ColNameClaimId, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameClaimNumber) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameClaimNumber), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameClaimNumber, Value)
        End Set
    End Property


    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCertId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameCertId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCertId, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property CertNumber() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCertNumber) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCertNumber), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCertNumber, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property LastActivityDate() As DateType
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameLastActivityDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CaseDAL.ColNameLastActivityDate), Date))
            End If
        End Get
        Set(ByVal value As DateType)
            CheckDeleted()
            SetValue(CaseDAL.ColNameLastActivityDate, Value)
        End Set
    End Property



    Public Property CaseCloseDate() As DateType
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseCloseDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CaseDAL.ColNameCaseCloseDate), Date))
            End If
        End Get
        Set(ByVal value As DateType)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseCloseDate, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property CaseCloseCode() As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseCloseCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCaseCloseCode), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseCloseCode, Value)
        End Set
    End Property





#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CaseDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetCaseList(ByVal companyId As Guid, ByVal caseNumber As String, ByVal caseStatus As String, ByVal callerFirstName As String, ByVal callerLastName As String, ByVal caseOpenDateFrom As String, ByVal caseOpenDateTo As String, ByVal casePurpose As String, ByVal certificateNumber As String, ByVal caseClosedReason As String, ByVal languageId As Guid) As CaseSearchDV
        Try
            Dim dal As New CaseDAL
            Dim fromdate As Date?
            Dim todate As Date?

            If (CaseNumber.Equals(String.Empty) AndAlso
                CaseStatus.Equals(String.Empty) AndAlso
                CallerFirstName.Equals(String.Empty) AndAlso
                CallerLastName.Equals(String.Empty) AndAlso
                CaseOpenDateFrom.Equals(String.Empty) AndAlso
                CaseOpenDateTo.Equals(String.Empty) AndAlso
                CasePurpose.Equals(String.Empty) AndAlso
                CertificateNumber.Equals(String.Empty) AndAlso
                CaseClosedReason.Equals(String.Empty)
                    ) Then
                Dim errors() As ValidationError = {New ValidationError(SearchException, GetType(CaseBase), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(CaseBase).FullName)
            End If

            If Not (CaseOpenDateFrom.Equals(String.Empty) AndAlso CaseOpenDateTo.Equals(String.Empty)) Then
                If CaseOpenDateFrom.Equals(String.Empty) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.VALUE_MANDATORY_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                ElseIf Not DateHelper.IsDate(CaseOpenDateFrom) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If

                If CaseOpenDateTo.Equals(String.Empty) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_TO") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.VALUE_MANDATORY_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                ElseIf Not DateHelper.IsDate(CaseOpenDateTo) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_TO") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If

                If DateHelper.GetDateValue(CaseOpenDateFrom) > DateHelper.GetDateValue(CaseOpenDateTo) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.VALID_DATE_RANGE_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If

                If DateHelper.GetDateValue(CaseOpenDateFrom).AddDays(7) < DateHelper.GetDateValue(CaseOpenDateTo) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_RANGE_ERR"), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If
                fromdate = DateTime.Parse(CaseOpenDateFrom.ToString(),
                                                    Thread.CurrentThread.CurrentCulture,
                                                    DateTimeStyles.NoCurrentDateDefault)

                todate = DateTime.Parse(CaseOpenDateTo.ToString(),
                                                    Thread.CurrentThread.CurrentCulture,
                                                    DateTimeStyles.NoCurrentDateDefault)
            End If


            Return New CaseSearchDV(dal.LoadCaseList(CompanyId, CaseNumber, CaseStatus, CallerFirstName, CallerLastName, fromdate, todate, CasePurpose, CertificateNumber, CaseClosedReason, LanguageId).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetAgentList(ByVal companyId As Guid, ByVal dealerId As Guid, ByVal customerFirstName As String, ByVal customerLastName As String,
                                        ByVal caseNumber As String, ByVal claimNumber As String, ByVal certificateNumber As String,
                                        ByVal serialNumber As String, ByVal invoiceNumber As String, ByVal phoneNumber As String, ByVal zipcode As String,
                                        ByVal certificateStatus As String, ByVal email As String,
                                        ByVal taxId As String, ByVal serviceLineNumber As String, ByVal accountNumber As String, ByVal globalCustomerNumber As String,
                                        ByVal languageId As Guid) As AgentSearchDV
        Try
            Dim dal As New CaseDAL
            Return New AgentSearchDV(dal.LoadAgentSearchList(CompanyId,
                                                             DealerId,
                                                             CustomerFirstName,
                                                             CustomerLastName,
                                                             CaseNumber,
                                                             ClaimNumber,
                                                             CertificateNumber,
                                                             SerialNumber,
                                                             InvoiceNumber,
                                                             PhoneNumber,
                                                             Zipcode,
                                                             CertificateStatus,
                                                             Email,
                                                             TaxId,
                                                             ServiceLineNumber,
                                                             AccountNumber,
                                                             GlobalCustomerNumber,
                                                             LanguageId).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimCaseList(ByVal claimId As Guid, ByVal languageId As Guid) As CaseSearchDV
        Try
            Dim dal As New CaseDAL
            Return New CaseSearchDV(dal.LoadClaimCaseList(ClaimId, LanguageId).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCaseDeniedReasonsList(ByVal caseId As Guid, ByVal languageId As Guid) As CaseDeniedReasonsDV
        Try
            Dim dal As New CaseDAL

            Return New CaseDeniedReasonsDV(dal.LoadCaseDeniedReasonsList(CaseId, LanguageId).Tables(0))

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetQuestionSetCode(companyGroupId As Guid, companyId As Guid, dealerId As Guid, productCodeId As Guid,
                                     riskTypeId As Guid, deviceTypeId As Guid,
                                     coverageTypeId As Guid, coverageConseqDamageId As Guid, purposeCode As String) As String
        Try
            Dim dal As New CaseDAL

            Return dal.GetQuestionSetCode(companyGroupId, companyId, dealerId, productCodeId, riskTypeId,
                                          deviceTypeId, coverageTypeId, coverageConseqDamageId, purposeCode)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadCaseByClaimId(claimId As Guid) As DataSet
        Try
            Dim dal As New CaseDAL

            Return dal.LoadCaseByClaimId(claimId)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCaseFieldsList(ByVal claimId As Guid, ByVal languageId As Guid) As DataSet
        Try
            Dim dal As New CaseDAL
            Return dal.LoadCaseFieldsList(claimId, languageId)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetCaseNotesList(ByVal caseId As Guid) As CaseNotesDV
        Try
            Dim dal As New CaseDAL
            Return New CaseNotesDV(dal.LoadCaseNotesList(CaseId).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "CaseSearchDV"
    Public Class CaseSearchDv
        Inherits DataView

#Region "Constants"
        Public Const ColCaseId As String = "case_id"
        Public Const ColCaseNumber As String = "case_number"
        Public Const ColCaseStatusCode As String = "case_status_code"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#Region "AgentSearchDV"
    Public Class AgentSearchDv
        Inherits DataView

#Region "Constants"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "CaseDeniedReasonsDV"

    Public Class CaseDeniedReasonsDv
        Inherits DataView
#Region "Constants"
        Public Const ColDeniedReason As String = "denied_reason"
        Public Const ColCreatedBy As String = "created_by"
        Public Const ColCreatedDate As String = "created_date"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "CaseNotesDV"

    Public Class CaseNotesDv
        Inherits DataView
#Region "Constants"
        Public Const ColNotes As String = "notes"
        Public Const ColCreatedDate As String = "created_date"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
End Class


