'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/25/2017)  ********************
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Threading
Imports System.Web.UI
Imports System.Windows.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Public Class CaseBase
    Inherits BusinessObjectBase
#Region "Constants"
    Private Const SearchException As String = "SEARCH_CRITERION_001" ' Case List Search Exception - If no criterion(except companyid) is selected
    Private Const MinimumSearchCriterion As String = "MINIMUM_SEARCH_CRITERION"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
    End Sub
    Public Sub New(caseNumber As String, companyCode As String)
        MyBase.New()
        Dataset = New DataSet
        Load(caseNumber, companyCode)
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

    Protected Sub Load(id As Guid)
        Try
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
    Protected Overridable Sub Load(caseNumber As String, companyCode As String)
        Try
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(CaseDAL.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(CaseDAL.TableName) >= 0 Then
                Row = FindRow(caseNumber, CaseDAL.ColNameCaseNumber, Dataset.Tables(CaseDAL.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New CaseDAL
                dal.LoadCaseByCaseNumber(Dataset, caseNumber, companyCode)
                Row = FindRow(caseNumber, CaseDAL.ColNameCaseNumber, Dataset.Tables(CaseDAL.TableName))
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(CaseDAL.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameCaseId), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCompanyId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameCompanyId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCompanyId, value)
        End Set
    End Property
    <ValidStringLength("", Max:=70)>
    Public Property CompanyDesc As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCompanyDesc) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCompanyDesc), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCompanyDesc, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=120)>
    Public Property CaseNumber As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseNumber) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCaseNumber), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseNumber, value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CaseOpenDate As DateType
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseOpenDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CaseDAL.ColNameCaseOpenDate), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseOpenDate, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property CasePurposeCode As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCasePurposeCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCasePurposeCode), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCasePurposeCode, value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property CaseStatusCode As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseStatusCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCaseStatusCode), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseStatusCode, value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property InitialInteractionId As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameInitialInteractionId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameInitialInteractionId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameInitialInteractionId, value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property InitialCallerName As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameInitialCallerName) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameInitialCallerName), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameInitialCallerName, value)
        End Set
    End Property

    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameClaimId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameClaimId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameClaimId, value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameClaimNumber) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameClaimNumber), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameClaimNumber, value)
        End Set
    End Property


    Public Property CertId As Guid
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCertId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseDAL.ColNameCertId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCertId, value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property CertNumber As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCertNumber) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCertNumber), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCertNumber, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property LastActivityDate As DateType
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameLastActivityDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CaseDAL.ColNameLastActivityDate), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameLastActivityDate, value)
        End Set
    End Property



    Public Property CaseCloseDate As DateType
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseCloseDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CaseDAL.ColNameCaseCloseDate), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseCloseDate, value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property CaseCloseCode As String
        Get
            CheckDeleted()
            If Row(CaseDAL.ColNameCaseCloseCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseDAL.ColNameCaseCloseCode), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CaseDAL.ColNameCaseCloseCode, value)
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
    Public Shared Function GetCaseList(companyId As Guid, caseNumber As String, caseStatus As String, callerFirstName As String, callerLastName As String, caseOpenDateFrom As String, caseOpenDateTo As String, casePurpose As String, certificateNumber As String, caseClosedReason As String,
                                       languageId As Guid, networkId As String) As CaseSearchDv
        Try
            Dim dal As New CaseDAL
            Dim fromdate As Date?
            Dim todate As Date?

            If (caseNumber.Equals(String.Empty) AndAlso
                caseStatus.Equals(String.Empty) AndAlso
                callerFirstName.Equals(String.Empty) AndAlso
                callerLastName.Equals(String.Empty) AndAlso
                caseOpenDateFrom.Equals(String.Empty) AndAlso
                caseOpenDateTo.Equals(String.Empty) AndAlso
                casePurpose.Equals(String.Empty) AndAlso
                certificateNumber.Equals(String.Empty) AndAlso
                caseClosedReason.Equals(String.Empty)
                ) Then
                Dim errors() As ValidationError = {New ValidationError(SearchException, GetType(CaseBase), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(CaseBase).FullName)
            End If

            If Not (caseOpenDateFrom.Equals(String.Empty) AndAlso caseOpenDateTo.Equals(String.Empty)) Then
                If caseOpenDateFrom.Equals(String.Empty) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.VALUE_MANDATORY_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                ElseIf Not DateHelper.IsDate(caseOpenDateFrom) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If

                If caseOpenDateTo.Equals(String.Empty) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_TO") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.VALUE_MANDATORY_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                ElseIf Not DateHelper.IsDate(caseOpenDateTo) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_TO") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If

                If DateHelper.GetDateValue(caseOpenDateFrom) > DateHelper.GetDateValue(caseOpenDateTo) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.VALID_DATE_RANGE_ERR), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If

                If DateHelper.GetDateValue(caseOpenDateFrom).AddDays(7) < DateHelper.GetDateValue(caseOpenDateTo) Then
                    Dim errors() As ValidationError = {New ValidationError(TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_FROM") & " : " & TranslationBase.TranslateLabelOrMessage("CASE_OPEN_DATE_RANGE_ERR"), GetType(CaseBase), Nothing, "Case Open Date", Nothing)}
                    Throw New BOValidationException(errors, GetType(CaseBase).FullName)
                End If

                If (CultureInfo.CurrentCulture.Name.Equals("ja-JP")) OrElse (CultureInfo.CurrentCulture.Name.Equals("zh-CN")) Then
                    fromdate = DateHelper.convertDateFrmt(caseOpenDateFrom)
                    todate = DateHelper.convertDateFrmt(caseOpenDateTo)
                Else
                    fromdate = DateTime.Parse(caseOpenDateFrom.ToString(),
                                          Thread.CurrentThread.CurrentCulture,
                                          DateTimeStyles.NoCurrentDateDefault)

                    todate = DateTime.Parse(caseOpenDateTo.ToString(),
                                        Thread.CurrentThread.CurrentCulture,
                                        DateTimeStyles.NoCurrentDateDefault)
                End If
            End If
            Return New CaseSearchDv(dal.LoadCaseList(companyId, caseNumber, caseStatus, callerFirstName, callerLastName, fromdate, todate, casePurpose, certificateNumber, caseClosedReason, languageId, networkId).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
   Public Shared Function GetAgentList(companyId As Guid, dealerId As Guid, customerFirstName As String, customerLastName As String,
                                        caseNumber As String, claimNumber As String, certificateNumber As String,
                                        serialNumber As String, invoiceNumber As String, phoneNumber As String, zipcode As String,
                                        certificateStatus As String, email As String,
                                        taxId As String, serviceLineNumber As String, accountNumber As String,
                                        globalCustomerNumber As String, dateofbirth As String,
                                        languageId As Guid,branchCode As String,branchName As String) As AgentSearchDv

        If (customerFirstName.Equals(String.Empty) AndAlso
            customerLastName.Equals(String.Empty) AndAlso
            caseNumber.Equals(String.Empty) AndAlso
            claimNumber.Equals(String.Empty) AndAlso
            certificateNumber.Equals(String.Empty) AndAlso
            serialNumber.Equals(String.Empty) AndAlso
            invoiceNumber.Equals(String.Empty) AndAlso
            phoneNumber.Equals(String.Empty) AndAlso
            zipcode.Equals(String.Empty) AndAlso
            certificateStatus = "" AndAlso
            email.Equals(String.Empty) AndAlso
            taxId.Equals(String.Empty) AndAlso
            accountNumber.Equals(String.Empty) AndAlso
            globalCustomerNumber.Equals(String.Empty) AndAlso
            branchCode.Equals(String.Empty) AndAlso
            branchName.Equals(String.Empty) AndAlso
            Not dateofbirth.Equals(String.Empty)) Then
            Dim errors() As ValidationError = {New ValidationError(MinimumSearchCriterion, GetType(CaseBase), Nothing, "Search", Nothing)}
            Throw New BOValidationException(errors, GetType(CaseBase).FullName)

        End If

        Try

            Dim dal As New CaseDAL
            Return New AgentSearchDv(dal.LoadAgentSearchList(companyId,
                                                             dealerId,
                                                             customerFirstName,
                                                             customerLastName,
                                                             caseNumber,
                                                             claimNumber,
                                                             certificateNumber,
                                                             serialNumber,
                                                             invoiceNumber,
                                                             phoneNumber,
                                                             zipcode,
                                                             certificateStatus,
                                                             email,
                                                             taxId,
                                                             serviceLineNumber,
                                                             accountNumber,
                                                             globalCustomerNumber,
                                                             dateofbirth,
                                                             Authentication.CurrentUser.NetworkId,
                                                             languageId,
                                                             branchCode,
                                                             branchName).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetAgentSearchConfigList(companyId As Guid, dealerId As Guid, searchType As String) As DataSet
        Try
            Dim dal As New CaseDAL
            Return (dal.LoadAgentSearchConfigList(companyId, dealerId, searchType))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetDealerListByCompanyForExternalUser()
        Try
            Dim Description, Code As String
            Dim dal As New DealerDAL
            Dim ds As DataSet
            ds = dal.LoadList(Description, Code, Authentication.CurrentUser.ScDealerId, Authentication.CurrentUser.Companies)

            Dim lstItm As New ListItem()
            Dim lstItems As List(Of ListItem) = New List(Of ListItem)()
            Dim dlrlist As ListItem()
            If ds IsNot Nothing Then
                If ds.Tables(0) IsNot Nothing Then
                    For Each dr As DataRow In ds.Tables(0).Rows
                        If dr("DEALER_ID") IsNot DBNull.Value Then
                            lstItm.ListItemId = New Guid(CType(dr("DEALER_ID"), Byte()))
                        End If
                        If dr("COMPANY_CODE") IsNot DBNull.Value Then
                            lstItm.ExtendedCode = dr("COMPANY_CODE").ToString()
                        End If
                        If dr("DEALER") IsNot DBNull.Value Then
                            lstItm.Code = dr("DEALER").ToString()
                        End If
                        If dr("DEALER_NAME") IsNot DBNull.Value Then
                            lstItm.Translation = dr("DEALER_NAME").ToString()
                        End If
                        lstItems.Add(lstItm)
                    Next
                End If
            End If

            dlrlist = lstItems.ToArray()
            Return dlrlist
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetExclSecFieldsList(companyId As Guid, dealerId As Guid) As DataSet
        Try
            Dim dal As New CaseDAL
            Return dal.LoadExclSecFieldsList(companyId, dealerId)
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function DisplaySecField(exclSecFieldsDt As DataTable, callerAuthenticationNeeded As Boolean,
                                           secFieldTableName As String, secField As String, isCallerAuthenticated As Boolean) As Boolean

        If exclSecFieldsDt IsNot Nothing AndAlso callerAuthenticationNeeded AndAlso Not isCallerAuthenticated Then
            If secField Is String.Empty OrElse (exclSecFieldsDt.AsEnumerable().Where(Function(p) p.Field(Of String)("table_name") = secFieldTableName AndAlso p.Field(Of String)("column_name") = secField).Count > 0) Then
                Return False
            End If
            '(ExclSecFieldsDt.AsEnumerable().Where(Function(p) p.Field(Of String)("table_name") = "ELP_CUSTOMER" and p.Field(Of String)("column_name") = secField).Count > 0 ) then                       
        End If
        Return True

    End Function
    Public Shared Function LoadExclSecFieldsConfig(companyId As Guid, dealerId As Guid) As List(Of ExclSecFields)
        Try

            Dim exclSecFieildsDv As DataSet

            exclSecFieildsDv = GetExclSecFieldsList(Guid.Empty, dealerId)
            'Populate User Roles
            Dim oUser As User
            Dim selectedDs As DataView = oUser.GetUserRoles(New User(Authentication.CurrentUser.NetworkId).Id)
            Dim objList As New List(Of ExclSecFields)
            objList = (From secFields In exclSecFieildsDv.Tables(0).AsEnumerable() Join role In selectedDs.Table.AsEnumerable()
                On secFields.Field(Of String)("role") Equals role.Field(Of String)("code")
                       Where secFields.Field(Of String)("purpose_xcd") = "EXCLUDE_PURPOSE-NOT_AUTHORIZED"
                       Select New ExclSecFields With {.Table_Name = secFields.Field(Of String)("table_name"),
                           .Column_Name = secFields.Field(Of String)("column_name")
                           }).ToList()
            Return objList

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class ExclSecFields
        Public Property Table_Name As String
        Public Property Column_Name As String
    End Class




    Public Shared Function GetClaimCaseList(claimId As Guid, languageId As Guid) As CaseSearchDv
        Try
            Dim dal As New CaseDAL
            Return New CaseSearchDv(dal.LoadClaimCaseList(claimId, languageId).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCaseDeniedReasonsList(caseId As Guid, languageId As Guid) As CaseDeniedReasonsDv
        Try
            Dim dal As New CaseDAL

            Return New CaseDeniedReasonsDv(dal.LoadCaseDeniedReasonsList(caseId, languageId).Tables(0))

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetQuestionSetCode(companyGroupId As Guid, companyId As Guid, dealerId As Guid, dealerGroupID As Guid, productCodeId As Guid,
                                              riskTypeId As Guid, deviceTypeId As Guid,
                                              coverageTypeId As Guid, coverageConseqDamageId As Guid, purposeCode As String) As String
        Try
            Dim dal As New CaseDAL

            Return dal.GetQuestionSetCode(companyGroupId, companyId, dealerId, dealerGroupID, productCodeId, riskTypeId,
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

    Public Shared Function GetCaseFieldsList(claimId As Guid, languageId As Guid) As DataSet
        Try
            Dim dal As New CaseDAL
            Return dal.LoadCaseFieldsList(claimId, languageId)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetCaseNotesList(caseId As Guid) As CaseNotesDv
        Try
            Dim dal As New CaseDAL
            Return New CaseNotesDv(dal.LoadCaseNotesList(caseId).Tables(0))
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub UpdateCaseFieldValues(caseId As Guid, ByRef caseFieldXcds() As String, ByRef caseFieldValues() As String)
        Try
            Dim dal As New CaseDAL
            dal.UpdateCaseFieldValues(caseId, caseFieldXcds, caseFieldValues)
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region
#Region "CaseSearchDV"
    Public Class CaseSearchDv
        Inherits DataView

#Region "Constants"
        Public Const ColCaseId As String = "case_id"
        Public Const ColCaseNumber As String = "case_number"
        Public Const ColCaseStatusCode As String = "case_status_code"
        Public Const ColCaseOpenDate As String = "case_open_date"
        Public Const ColCaseCloseDate As String = "case_close_date"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
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

        Public Sub New(table As DataTable)
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

        Public Sub New(table As DataTable)
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

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
End Class


