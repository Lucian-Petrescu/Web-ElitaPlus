Public Class ConfigQuestionSet
    Inherits BusinessObjectBase

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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ConfigQuestionSetDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ConfigQuestionSetDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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

    Public ReadOnly Property Id As Guid
        Get
            If Row(ConfigQuestionSetDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_CONFIG_QUESTION_SET_ID), Byte()))
            End If
        End Get
    End Property

    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property DealerGroupId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property

    <ValidProductCodeDealer("Dealer")>
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValidProductCodeDealer("Product Code")>
    Public Property ProductCodeId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property

    Public Property DeviceTypeId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_DEVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_DEVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_DEVICE_TYPE_ID, Value)
        End Set
    End Property

    Public Property CoverageTypeId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ConfigQuestionSetDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory("Purpose")>
    Public Property PurposeXCD As String
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_PURPOSE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ConfigQuestionSetDAL.COL_NAME_PURPOSE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_PURPOSE_XCD, Value)
        End Set
    End Property

    <ValueMandatory("Question Set Code")>
    Public Property QuestionSetCode As String
        Get
            CheckDeleted()
            If Row(ConfigQuestionSetDAL.COL_NAME_QUESTION_SET_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ConfigQuestionSetDAL.COL_NAME_QUESTION_SET_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ConfigQuestionSetDAL.COL_NAME_QUESTION_SET_CODE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ConfigQuestionSetDAL
                'Check for Duplicate Question Set Configuration
                Dim rtnMessage As String
                rtnMessage = dal.CheckForDuplicateConfiguration(ConfigQuestionSetID:=Id, CompanyGroupID:=CompanyGroupId, CompanyID:=CompanyId,
                                                                CoverageTypeID:=CoverageTypeId, DealerGroupID:=DealerGroupId, DealerID:=DealerId,
                                                                DeviceTypeID:=DeviceTypeId, ProductCodeID:=ProductCodeId, RiskTypeID:=RiskTypeId,
                                                                LanguageID:=ElitaPlusIdentity.Current.ActiveUser.LanguageId, strPurposeXCD:=PurposeXCD, strQuestionSetCode:=QuestionSetCode)
                If rtnMessage <> "NO_ERROR" Then
                    Dim vErrors() As ValidationError = {New ValidationError(rtnMessage, [GetType](), Nothing, "QuestionSetCode", Nothing)}
                    Throw New BOValidationException(vErrors, "ConfigQuestionSet")
                End If
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
            'Catch ex As BOValidationException
            '    Dim vErrors() As ValidationError = {New ValidationError(ex.Message, Me.GetType(), Nothing, "QuestionSetCode", Nothing)}
            '    Throw New BOValidationException(vErrors, "ConfigQuestionSet")
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub DeleteConfiguration(configQuestionSetId As Guid)
        Try
            Dim dal As New ConfigQuestionSetDAL
            dal.DeleteConfiguration(configQuestionSetId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "SearchDV"
    Public Class ConfigQuestionSetSearchDV
        Inherits DataView

        Public Const COL_QUESTION_SET_CONFIG_ID As String = "CONFIG_QUESTION_SET_ID"
        Public Const COL_COMPANY_GROUP_ID As String = "company_group_id"
        Public Const COL_COMPANY_GROUP_DESC As String = "company_group_desc"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_COMPANY_DESC As String = "company_desc"
        Public Const COL_DEALER_GROUP_ID As String = "dealer_group_id"
        Public Const COL_DEALER_GROUP_DESC As String = "dealer_group_desc"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER_DESC As String = "dealer_desc"
        Public Const COL_PRODUCT_CODE_ID As String = "product_code_id"
        Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_COVERAGE_TYPE_ID As String = "coverage_type_id"
        Public Const COL_COVERAGE_TYPE_DESC As String = "coverage_type_desc"
        Public Const COL_RISK_TYPE_ID As String = "risk_type_id"
        Public Const COL_RISK_TYPE_DESC As String = "risk_type_desc"
        Public Const COL_PURPOSE_XCD As String = "PURPOSE_XCD"
        Public Const COL_PURPOSE_DESC As String = "PURPOSE_XCD_DESC"
        Public Const COL_QUESTION_SET_CODE As String = "QUESTION_SET_CODE"
        Public Const COL_QUESTION_SET_DESC As String = "QUESTION_SET_DESC"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As ConfigQuestionSetSearchDV, NewBO As ConfigQuestionSet)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_QUESTION_SET_CONFIG_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_COMPANY_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_COMPANY_GROUP_DESC, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_COMPANY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_COMPANY_DESC, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_DEALER_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_DEALER_GROUP_DESC, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_DEALER_DESC, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_PRODUCT_CODE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_PRODUCT_CODE, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_COVERAGE_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_COVERAGE_TYPE_DESC, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_RISK_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_RISK_TYPE_DESC, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_PURPOSE_XCD, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_PURPOSE_DESC, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_QUESTION_SET_CODE, GetType(String))
                dt.Columns.Add(ConfigQuestionSetSearchDV.COL_QUESTION_SET_DESC, GetType(String))
            Else
                dt = dv.Table
            End If

            row = dt.NewRow
            row(ConfigQuestionSetSearchDV.COL_QUESTION_SET_CONFIG_ID) = NewBO.Id.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_COMPANY_GROUP_ID) = NewBO.CompanyGroupId.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_COMPANY_ID) = NewBO.CompanyId.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_DEALER_GROUP_ID) = NewBO.DealerGroupId.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_PRODUCT_CODE_ID) = NewBO.ProductCodeId.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_COVERAGE_TYPE_ID) = NewBO.CoverageTypeId.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_RISK_TYPE_ID) = NewBO.RiskTypeId.ToByteArray
            row(ConfigQuestionSetSearchDV.COL_PURPOSE_XCD) = NewBO.PurposeXCD
            row(ConfigQuestionSetSearchDV.COL_QUESTION_SET_CODE) = NewBO.QuestionSetCode

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New ConfigQuestionSetSearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(CompGrpID As Guid, CompanyID As Guid, DealerGrpID As Guid, DealerID As Guid,
                                   ProductCode As String, RiskTypeID As Guid, CoverageTypeID As Guid,
                                   strPurposeXCD As String, strQuestionSetCode As String) As ConfigQuestionSetSearchDV
        Try
            Dim dal As New ConfigQuestionSetDAL
            Return New ConfigQuestionSetSearchDV(dal.LoadList(CompGrpID:=CompGrpID, CompanyID:=CompanyID, DealerGrpID:=DealerGrpID, DealerID:=DealerID,
                                                              ProductCode:=ProductCode, CoverageTypeID:=CoverageTypeID, RiskTypeID:=RiskTypeID,
                                                              strPurposeXCD:=strPurposeXCD, strQuestionSetCode:=strQuestionSetCode,
                                                              LanguageID:=ElitaPlusIdentity.Current.ActiveUser.LanguageId, networkID:=ElitaPlusIdentity.Current.ActiveUser.NetworkId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"
    Public NotInheritable Class ValidProductCodeDealer
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "Either Company Group / Company / Dealer Group / Dealer / Product Code / Coverage Type / Risk Type Or Device Type Is Required")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ConfigQuestionSet = CType(objectToValidate, ConfigQuestionSet)

            If (Guid.Empty = obj.CompanyGroupId) AndAlso (Guid.Empty = obj.CompanyId) AndAlso (Guid.Empty = obj.DealerGroupId) AndAlso (Guid.Empty = obj.DealerId) AndAlso (Guid.Empty = obj.ProductCodeId) AndAlso (Guid.Empty = obj.CoverageTypeId) AndAlso (Guid.Empty = obj.RiskTypeId) AndAlso (Guid.Empty = obj.DeviceTypeId) Then
                Return False
            Else
                Return True
            End If

        End Function
    End Class

#End Region

End Class