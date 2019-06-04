
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/22/2010)  ********************

Public Class SpecialService
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New SpecialServiceDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New SpecialServiceDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
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

#Region "Constants"

    Dim EMPTY_GRID_ID As String = "00000000000000000000000000000000"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(SpecialServiceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_SPECIAL_SERVICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SpecialServiceDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SpecialServiceDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatoryCovergeLoss(""), ValidCovergeLoss("")> _
    Public Property CoverageLossId() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_COVERAGE_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_COVERAGE_LOSS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_COVERAGE_LOSS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidateAvailableSvcCenter("")> _
    Public Property AvailableForServCenterId() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_AVAILABLE_FOR_SERV_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_AVAILABLE_FOR_SERV_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_AVAILABLE_FOR_SERV_CENTER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AddItemAllowed() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_ADD_ITEM_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_ADD_ITEM_ALLOWED), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_ADD_ITEM_ALLOWED, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AddItemAfterExpired() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_ADD_ITEM_AFTER_EXPIRED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_ADD_ITEM_AFTER_EXPIRED), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_ADD_ITEM_AFTER_EXPIRED, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidateAuthorizedAmountFrom("")> _
    Public Property PriceGroupFieldId() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_PRICE_GROUP_FIELD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_PRICE_GROUP_FIELD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_PRICE_GROUP_FIELD_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AllowedOccurrencesId() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_ALLOWED_OCCURRENCES_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_ALLOWED_OCCURRENCES_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_ALLOWED_OCCURRENCES_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CombinedWithRepair() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.COL_NAME_COMBINED_WITH_REPAIR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.COL_NAME_COMBINED_WITH_REPAIR), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.COL_NAME_COMBINED_WITH_REPAIR, Value)
        End Set
    End Property

    '<ValueMandatory("")> _
    Public Property ServiceClassId() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.DB_COL_NAME_SERVICE_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.DB_COL_NAME_SERVICE_CLASS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.DB_COL_NAME_SERVICE_CLASS_ID, Value)
        End Set
    End Property

    '<ValueMandatory("")> _
    Public Property ServiceTypeId() As Guid
        Get
            CheckDeleted()
            If Row(SpecialServiceDAL.DB_COL_NAME_SERVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SpecialServiceDAL.DB_COL_NAME_SERVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SpecialServiceDAL.DB_COL_NAME_SERVICE_TYPE_ID, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New SpecialServiceDAL
                'dal.Update(Me.Row)
                MyBase.UpdateFamily(Me.Dataset)
                dal.UpdateFamily(Me.Dataset)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Dim bDirty As Boolean

            bDirty = MyBase.IsDirty OrElse Me.IsChildrenDirty

            Return bDirty
        End Get
    End Property

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal LanguageId As Guid, ByVal compIds As ArrayList, _
                           ByVal dealerId As Guid, ByVal CoverageTypeId As Guid) As SpecialServiceSearchDV
        Try
            Dim dal As New SpecialServiceDAL
            Return New SpecialServiceSearchDV(dal.LoadList(LanguageId, compIds, dealerId, _
                                              CoverageTypeId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ValidateCoverageLoss(ByVal DealerId As Guid, ByVal CoverageLossId As Guid) As DataSet
        Try
            Dim dal As New SpecialServiceDAL
            'Dim ds As DataSet
            Return dal.ValidateCoverageLoss(DealerId, CoverageLossId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ChkIfSplSvcConfigured(ByVal CompanyGroupId As Guid, ByVal CoverageTypeId As Guid, ByVal DealerId As Guid, ByVal language_id As Guid, ByVal ProductCode As String, Optional ByVal LoadNoneActive As Boolean = False) As Boolean
        Try
            Dim dal As New SpecialServiceDAL
            Dim ds As DataSet
            ds = dal.GetAvailSplSvcList(CompanyGroupId, CoverageTypeId, DealerId, ProductCode, LoadNoneActive)
            If ds.Tables(SpecialServiceDAL.TABLE_NAME).Rows.Count > 0 Then
                Return True
            End If

            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Req-603 For Web Service Request Process
    Public Shared Function getSpecialServices(ByVal ClaimNumber As String, ByVal CertificateNumber As String, ByVal CoverageTypeId As Guid) As DataSet
        Try
            Dim dal As New SpecialServiceDAL
            Dim ds As DataSet
            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim CompanyGrupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(language_id), "Y")

            If Not String.IsNullOrEmpty(ClaimNumber) Then
                Return dal.getspecialServicebyClaimNumber(ClaimNumber, CoverageTypeId, language_id, yesId, CompanyGrupId)
            ElseIf Not String.IsNullOrEmpty(CertificateNumber) Then
                Return dal.getSpecialServiceByCertificate(CertificateNumber, CoverageTypeId, language_id, yesId, CompanyGrupId)
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getServiceTypesForServiceClass(ByVal ServiceClassId As Guid, ByVal LanguageId As Guid) As DataSet
        Try
            Dim dal As New SpecialServiceDAL
            Dim ds As DataSet
            ds = dal.getServiceTypebyServiceClass(ServiceClassId, LanguageId)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getPriceGroupsList(ByVal LanguageId As Guid) As DataSet
        Try
            Dim dal As New SpecialServiceDAL
            Dim ds As DataSet
            ds = dal.GetPriceGroupsList(LanguageId)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "SpecialServiceSearchDV"
    Public Class SpecialServiceSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_SpecialService_ID As String = "SPECIAL_SERVICE_ID"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_COVERAGE_TYPE As String = "coverage_type"
        Public Const COL_CAUSE_OF_LOSS As String = "cause_of_loss"
        Public Const COL_COMPANY_CODE As String = "COMPANY_CODE"
        Public Const COL_LAYOUT As String = "layout"


#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Children Related"

    'METHODS ADDED MANUALLY. BEGIN
#Region "ProductCodes"


    Public ReadOnly Property ProductSpecialServiceChildren() As ProductSpecialServiceList
        Get
            Return New ProductSpecialServiceList(Me)
        End Get
    End Property

    Public Sub UpdateProductCodes(ByVal selectedProductCodeGuidStrCollection As Hashtable)
        If selectedProductCodeGuidStrCollection.Count = 0 Then
            If Not Me.IsDeleted Then Me.Delete()
        Else
            'first Pass
            Dim bo As ProductSpecialService
            For Each bo In Me.ProductSpecialServiceChildren
                If Not selectedProductCodeGuidStrCollection.Contains(bo.ProductCodeId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedProductCodeGuidStrCollection
                If Me.ProductSpecialServiceChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim PSplSvcBO As ProductSpecialService = ProductSpecialServiceChildren.GetNewChild()
                    PSplSvcBO.ProductCodeId = New Guid(entry.Key.ToString)
                    PSplSvcBO.SpecialServiceId = Me.Id
                    PSplSvcBO.Save()
                End If
            Next
        End If
    End Sub
    Public Sub AttachProductCodes(ByVal selectedProductCodeGuidStrCollection As ArrayList)
        Dim PSplSvcIdStr As String
        For Each PSplSvcIdStr In selectedProductCodeGuidStrCollection
            Dim PSplSvcBO As ProductSpecialService = Me.ProductSpecialServiceChildren.GetNewChild
            PSplSvcBO.ProductCodeId = New Guid(PSplSvcIdStr)
            PSplSvcBO.SpecialServiceId = Me.Id
            PSplSvcBO.Save()
        Next
    End Sub

    Public Sub DetachProductCodes(ByVal selectedProductCodeGuidStrCollection As ArrayList)
        Dim PSplSvcIdStr As String
        For Each PSplSvcIdStr In selectedProductCodeGuidStrCollection
            Dim PSplSvcBO As ProductSpecialService = Me.ProductSpecialServiceChildren.Find(New Guid(PSplSvcIdStr))
            PSplSvcBO.Delete()
            PSplSvcBO.Save()
        Next
    End Sub

    Public Function GetAvailableProductCodes(ByVal dealerid As Guid) As DataView
        'Dim dv As DataView = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.'ActiveUser.Companies)
        '       Dim sequenceCondition As String = GetProductCodesLookupListSelectedSequenceFilter(dv, False)
        Dim dv As DataView                
        Dim sequenceCondition As String
        If Not GuidControl.GuidToHexString(dealerid) = EMPTY_GRID_ID Then
            'dv = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            'Else
            dv = LookupListNew.GetProductCodeByDealerLookupList(dealerid)
            sequenceCondition = GetProductCodesLookupListSelectedSequenceFilter(dv, False)

            If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
                dv.RowFilter = sequenceCondition
            Else
                dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
            End If

        End If
        Return dv
    End Function

    Public Function GetSelectedProductCodes(ByVal dealerid As Guid) As DataView

        Dim dv As DataView
        Dim sequenceCondition As String

        If Not GuidControl.GuidToHexString(dealerid) = EMPTY_GRID_ID Then
            dv = LookupListNew.GetProductCodeByDealerLookupList(dealerid)
            sequenceCondition = GetProductCodesLookupListSelectedSequenceFilter(dv, True)        

            If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
                dv.RowFilter = sequenceCondition
            Else
                dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
            End If
        End If
        Return dv
    End Function

    Protected Function GetProductCodesLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim PSplSvcBO As ProductSpecialService
        Dim inClause As String = "(-1"
        For Each PSplSvcBO In Me.ProductSpecialServiceChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, PSplSvcBO.ProductCodeId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function


#End Region

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class ValueMandatoryCovergeLoss
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_COV_LOSSCAUSE_COMB_IS_NOT_VALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As SpecialService = CType(objectToValidate, SpecialService)

            If obj.CoverageLossId = Guid.Empty Then
                Return False
            Else
                Return True
            End If
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
     Public NotInheritable Class ValidCovergeLoss
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_COV_LOSSCAUSE_COMB_EXISTS_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As SpecialService = CType(objectToValidate, SpecialService)
            Dim ds As DataSet

            ds = obj.ValidateCoverageLoss(obj.DealerId, obj.CoverageLossId)
            If ds.Tables(SpecialServiceDAL.TABLE_NAME).Rows.Count > 0 Then
                If obj.Id = GuidControl.ByteArrayToGuid(ds.Tables(SpecialServiceDAL.TABLE_NAME).Rows(0)(SpecialServiceDAL.COL_NAME_SPECIAL_SERVICE_ID)) Then
                    Return True
                Else                    
                    Return False                    
                End If
            Else
                Return True
            End If

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateAvailableSvcCenter
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_AVAILABLE_SVC_CNTR_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As SpecialService = CType(objectToValidate, SpecialService)
            Dim ds As DataSet, strAvailableServiceCenter As String
            strAvailableServiceCenter = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, New Guid(valueToCheck.ToString))
            If strAvailableServiceCenter = Codes.YESNO_N Then
                'Return True
            ElseIf strAvailableServiceCenter = Codes.YESNO_Y Then
                If LookupListNew.GetCodeFromId(LookupListNew.GetPriceGroupDPLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), _
                    obj.PriceGroupFieldId) = Codes.PRICEGROUP_SPL_SVC_MANUAL Then Return False
            End If
            Return True
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateAuthorizedAmountFrom
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_AUTHORIZED_AMOUNT_FROM_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As SpecialService = CType(objectToValidate, SpecialService)
            Dim ds As DataSet, strAvailableServiceCenter As String

            If LookupListNew.GetCodeFromId(LookupListNew.GetPriceGroupDPLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), _
                   obj.PriceGroupFieldId) = Codes.PRICEGROUP_SPL_SVC_PRICE_LIST Then
                If GuidControl.Equals(obj.ServiceClassId, Guid.Empty) Or GuidControl.Equals(obj.ServiceTypeId, Guid.Empty) Then Return False

            End If
            Return True
        End Function

    End Class

#End Region

End Class


