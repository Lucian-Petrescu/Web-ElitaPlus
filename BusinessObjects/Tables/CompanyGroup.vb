'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/8/2006)  ********************

Public Class CompanyGroup
    Inherits BusinessObjectBase

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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CompanyGroupDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CompanyGroupDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
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
            If row(CompanyGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(CompanyGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CompanyGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=5)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(CompanyGroupDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CompanyGroupDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    Public Sub InitCompanyGroupTable()
        Dataset.Tables(CompanyGroupDAL.TABLE_NAME).Rows.Clear()
    End Sub

    <ValueMandatory("")> _
    Public Property ClaimNumberingById As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_CLAIM_NUMBERING_BY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_CLAIM_NUMBERING_BY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_CLAIM_NUMBERING_BY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
Public Property AccountingByCompany As String
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_ACCT_BY_COMPANY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyGroupDAL.COL_NAME_ACCT_BY_COMPANY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_ACCT_BY_COMPANY, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InvoiceNumberingById As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_INVOICE_NUMBERING_BY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_INVOICE_NUMBERING_BY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_INVOICE_NUMBERING_BY_ID, Value)
        End Set
    End Property

    Public Property FtpSiteId As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_FTP_SITE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_FTP_SITE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_FTP_SITE_ID, Value)
        End Set
    End Property

    'REQ-1142
    <ValidNumericRange("", Max:=999, Min:=1)> _
    Public Property InactiveUsedVehiclesOlderThan As LongType
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_INACTIVE_USED_VEHICLES_Older_THAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CompanyGroupDAL.COL_NAME_INACTIVE_USED_VEHICLES_Older_THAN), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_INACTIVE_USED_VEHICLES_Older_THAN, Value)
        End Set
    End Property

    Public Property InactiveNewVehiclesBasedOn As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_INACTIVE_NEW_VEHICLES_BASED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_INACTIVE_NEW_VEHICLES_BASED_ON), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_INACTIVE_NEW_VEHICLES_BASED_ON, Value)
        End Set
    End Property
    'REQ-1142 end
    'REQ-863 
    <ValueMandatory("")> _
    Public Property InvoiceGrpNumberingById As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_INVOICE_GROUP_NUMBERING_BY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_INVOICE_GROUP_NUMBERING_BY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_INVOICE_GROUP_NUMBERING_BY_ID, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property AuthorizationNumberingById As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_AUTHORIZATION_NUMBERING_BY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_AUTHORIZATION_NUMBERING_BY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_AUTHORIZATION_NUMBERING_BY_ID, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property PaymentGrpNumberingById As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_PAYMENT_GROUP_NUMBERING_BY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_PAYMENT_GROUP_NUMBERING_BY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_PAYMENT_GROUP_NUMBERING_BY_ID, Value)
        End Set
    End Property
    'end Req-863

    'req 5547
    <ValueMandatory("")> _
    Public Property ClaimFastApprovalId As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_CLAIM_FAST_APPROVAL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_CLAIM_FAST_APPROVAL_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_CLAIM_FAST_APPROVAL_ID, Value)
        End Set
    End Property
    'end req 5547

    'REQ-5773 Start
    Public Property UseCommEntityTypeId As Guid
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_USE_COMM_ENTITY_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyGroupDAL.COL_NAME_USE_COMM_ENTITY_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_USE_COMM_ENTITY_TYPE_ID, Value)
        End Set
    End Property
    'REQ-5773 End

    Public ReadOnly Property AssociatedCoveragesType As CoverageByCompanyGroup.CovCompGrpList
        Get
            Return New CoverageByCompanyGroup.CovCompGrpList(Me)
        End Get
    End Property

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property


    <ValueMandatory("")>
    Public Property CaseNumberingByXcd As String
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_CASE_NUMBERING_BY_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyGroupDAL.COL_NAME_CASE_NUMBERING_BY_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_CASE_NUMBERING_BY_XCD, Value)
        End Set
    End Property



    <ValueMandatory("")>
    Public Property InteractionNumberingByXcd As String
        Get
            CheckDeleted()
            If Row(CompanyGroupDAL.COL_NAME_INTERACTION_NUMBERING_BY_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CompanyGroupDAL.COL_NAME_INTERACTION_NUMBERING_BY_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyGroupDAL.COL_NAME_INTERACTION_NUMBERING_BY_XCD, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CompanyGroupDAL
                dal.UpdateFamily(Dataset) 'New Code Added Manually
                'dal.Update(Me.Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "Children Related"

    'METHODS ADDED MANUALLY. BEGIN
#Region "CoverageType"
    Public Shared Function GetAvailableCoverageType(ByVal companyGroupId As Guid) As DataView
        Dim dal As New CoverageByCompanyGroupDAL
        Dim ds As DataSet

        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        ds = dal.LoadAvailableCoverageType(companyGroupId, oLanguageId, companyGroupId)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Shared Function GetSelectedCoverageType(ByVal companyGroupId As Guid) As DataView
        Dim dal As New CoverageByCompanyGroupDAL
        Dim ds As DataSet
        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        ds = dal.LoadSelectedCoverageType(companyGroupId, oLanguageId)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function


    Public Sub AttachCoverageType(ByVal selectedCoverageCompGrpGuidStrCollection As ArrayList)
        Dim covTypeIdStr As String
        For Each covTypeIdStr In selectedCoverageCompGrpGuidStrCollection
            Dim ctCompGrp As CoverageByCompanyGroup = AssociatedCoveragesType.GetNewChild
            ctCompGrp.CoverageTypeId = New Guid(covTypeIdStr)
            ctCompGrp.CompanyGroupId = Id
            ctCompGrp.Save()
        Next
    End Sub

    Public Sub DetachCoverageType(ByVal selectedCoverageCompGrpGuidStrCollection As ArrayList)
        Dim ctCovTypeIdStr As String
        For Each ctCovTypeIdStr In selectedCoverageCompGrpGuidStrCollection
            Dim ctCovCompGrp As CoverageByCompanyGroup = AssociatedCoveragesType.FindById(New Guid(ctCovTypeIdStr))
            ctCovCompGrp.Delete()
            ctCovCompGrp.Save()
        Next
    End Sub

#End Region

#Region "RiskType"

    Public Sub AttachCompanyes(ByVal selectedCompanyGuidStrCollection As ArrayList)
        Dim companyID As String
        For Each companyID In selectedCompanyGuidStrCollection
            'update to new soft question GUID
            Dim newBO As Company = New Company(New Guid(companyID), Dataset)
            If Not newBO Is Nothing Then
                newBO.CompanyGroupId = Id
                newBO.Save()
            End If
        Next
    End Sub

    Public Sub DetachRiskTypes(ByVal selectedCompanyGuidStrCollection As ArrayList)
        Dim companyID As String
        For Each companyID In selectedCompanyGuidStrCollection
            'update to new soft question GUID
            Dim newBO As Company = New Company(New Guid(companyID), Dataset)
            If Not newBO Is Nothing Then
                newBO.CompanyGroupId = Guid.Empty
                newBO.Save()
            End If
        Next
    End Sub

    Public Shared Function GetAvailableCompanies(ByVal CountryId As Guid) As DataSet
        Dim ds As DataSet = New DataSet
        Dim oDAL As CompanyDAL = New CompanyDAL
        'oDAL.LoadAvailableCompanyForCountry(ds, CountryId)
        Return ds
    End Function

    Public Shared Function GetSelectedCompanies(ByVal companyGrpID As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim oDAL As CompanyDAL = New CompanyDAL
        'oDAL.LoadCompanyForCountry(ds, companyGrpID)
        Return ds
    End Function



#End Region

    'METHODS ADDED MANUALLY. END


#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList() As CompanyGroupDV
        Try
            Dim dal As New CompanyGroupDAL

            Return New CompanyGroupDV(dal.LoadList().Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function LoadList(ByVal descriptionMask As String, ByVal codeMask As String, Optional ByVal getCovTypeChidrens As Boolean = False, Optional ByVal btnsearchclick As Boolean = False) As DataView
        Try
            Dim dal As New CompanyGroupDAL
            Dim ds As DataSet
            'REQ-863
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(CompanyGroup), Nothing, "Search", Nothing)}
            If btnsearchclick AndAlso (descriptionMask.Equals(String.Empty) AndAlso codeMask.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(CompanyGroup).FullName)
            Else
                'End Req-863
                ds = dal.LoadList(descriptionMask, codeMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId, getCovTypeChidrens)
                Return (ds.Tables(CompanyGroupDAL.TABLE_NAME).DefaultView)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As CompanyGroup) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(CompanyGroupDAL.COL_NAME_DESCRIPTION) = bo.Description 'String.Empty
            row(CompanyGroupDAL.COL_NAME_CODE) = bo.Code 'String.Empty
            row(CompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID) = bo.Id.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function


    Public ReadOnly Property GetCompanyGroupDescription(ByVal companyGroID As Guid) As String
        Get
            'Dim moCoverage As New CertItemCoverage
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Dim companyGrpDV As DataView = LookupListNew.GetCompanyGroupLookupList()
            Dim companyGrpDesc As String = LookupListNew.GetDescriptionFromId(companyGrpDV, companyGroID)

            Return companyGrpDesc
        End Get
    End Property

#Region "SoftQuestionGroupDV"
    Public Class CompanyGroupDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMPANY_GROUP_ID As String = CompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID '"company_group_id"
        Public Const COL_DESCRIPTION As String = CompanyGroupDAL.COL_NAME_DESCRIPTION '"description"
        Public Const COL_CODE As String = CompanyGroupDAL.COL_NAME_CODE '"code_id"
        Public Const COL_CLAIM_NUMBERING_ID As String = CompanyGroupDAL.COL_NAME_CLAIM_NUMBERING_BY_ID '"claim_numbering_by_id"
        Public Const COL_NAME_CLAIM_NUMBERING_DESCRIPTION As String = "claim_numbering_description"
        Public Const COL_NAME_FTP_SITE As String = "ftp_site"
        Public Const COL_NAME_INACTIVE_USED_VEHICLES_Older_THAN = CompanyGroupDAL.COL_NAME_INACTIVE_USED_VEHICLES_Older_THAN
        Public Const COL_NAME_INACTIVE_NEW_VEHICLES_BASED_ON = CompanyGroupDAL.COL_NAME_INACTIVE_NEW_VEHICLES_BASED_ON
        Public Const COL_NAME_INVOICE_NUMBERING_DESCRIPTION As String = "invoice_numbering_description"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#End Region

End Class



