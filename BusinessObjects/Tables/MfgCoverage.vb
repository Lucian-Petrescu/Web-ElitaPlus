'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/7/2008)  ********************

Public Class MfgCoverage
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

    Public Sub New(ByVal familyDS As DataSet, ByVal equipmentId As Guid)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.LoadByEquipmentId(equipmentId)
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
            Dim dal As New MfgCoverageDAL
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

    Protected Sub LoadByEquipmentId(ByVal id As Guid)
        Try
            Dim dal As New MfgCoverageDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.COL_EQUIPMENT_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByEquipmentId(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.COL_EQUIPMENT_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New MfgCoverageDAL
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


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(MfgCoverageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgCoverageDAL.COL_NAME_MFG_COVERAGE_ID), Byte()))
            End If
        End Get
    End Property

    Public Property EquipmentTypeId() As Guid
        Get
            If Row(MfgCoverageDAL.COL_EQUIPMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgCoverageDAL.COL_EQUIPMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_EQUIPMENT_TYPE_ID, Value)
        End Set
    End Property

    Public Property EquipmentId() As Guid
        Get
            If Row(MfgCoverageDAL.COL_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgCoverageDAL.COL_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_EQUIPMENT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(MfgCoverageDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgCoverageDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property
    <RiskTypeOrMfgRequired("")>
    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(MfgCoverageDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgCoverageDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(MfgCoverageDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(MfgCoverageDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValidConditionally(""), RiskTypeOrMfgRequired("")>
    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If Row(MfgCoverageDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MfgCoverageDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=99, Min:=0)>
    Public Property MfgWarranty() As Integer
        Get
            CheckDeleted()
            If Row(MfgCoverageDAL.COL_NAME_MFG_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(MfgCoverageDAL.COL_NAME_MFG_WARRANTY), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_NAME_MFG_WARRANTY, Value)
        End Set
    End Property
    <ValidNumericRange("", Max:=99, Min:=1)>
    Public Property MfgMainPartsWarranty() As LongType
        Get
            CheckDeleted()
            If Row(MfgCoverageDAL.COL_NAME_MFG_MAIN_PARTS_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(MfgCoverageDAL.COL_NAME_MFG_MAIN_PARTS_WARRANTY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(MfgCoverageDAL.COL_NAME_MFG_MAIN_PARTS_WARRANTY, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New MfgCoverageDAL
                dal.Update(Me.Row)
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
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal manufacturerId As Guid, ByVal CompanyGroupId As Guid, ByVal riskType As Guid, ByVal model As String) As MfgCoverageSearchDV
        Try
            Dim dal As New MfgCoverageDAL
            Return New MfgCoverageSearchDV(dal.LoadList(manufacturerId, CompanyGroupId, riskType, model, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(MfgCoverageDAL.COL_NAME_MFG_COVERAGE_ID) = id.ToByteArray
        row(MfgCoverageDAL.COL_NAME_COMPANY_GROUP_ID) = Guid.Empty.ToByteArray
        row(MfgCoverageDAL.COL_NAME_RISK_TYPE_ID) = Guid.Empty.ToByteArray
        row(MfgCoverageDAL.COL_NAME_MANUFACTURER_ID) = Guid.Empty.ToByteArray
        row(MfgCoverageDAL.COL_NAME_MODEL) = String.Empty
        row(MfgCoverageDAL.COL_NAME_MFG_WARRANTY) = DBNull.Value
        row(MfgCoverageDAL.COL_NAME_MFG_MAIN_PARTS_WARRANTY) = DBNull.Value
        dt.Rows.Add(row)

        Return (dv)

    End Function
#End Region

#Region "SearchDV"
    Public Class MfgCoverageSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_MFG_COVERAGE_ID As String = MfgCoverageDAL.COL_NAME_MFG_COVERAGE_ID
        Public Const COL_DEALER_ID As String = MfgCoverageDAL.COL_NAME_DEALER_ID
        Public Const COL_MANUFACTURER_ID As String = MfgCoverageDAL.COL_NAME_MANUFACTURER_ID
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_MANUFACTURER_NAME As String = "manufacturer"
        Public Const COL_MODEL As String = "model"
        Public Const COL_MFG_WARRANTY As String = "mfg_warranty"
        Public Const COL_RISK_TYPE_ID As String = "risk_type_id"
        Public Const COL_RISK_TYPE_ENGLISH As String = "risk_type_english"
        Public Const COL_DEDUCTIBLE As String = "deductible"
        Public Const COL_RISK_TYPE_DESCRIPTION As String = "description"
        Public Const COL_EQUIPMENT_TYPE_DESCRIPTION As String = "equipment_type"
        Public Const COL_EQUIPMENT_DESCRIPTION As String = "equipment_description"
        Public Const COL_MFG_MAIN_PARTS_WARRANTY As String = "mfg_main_parts_warranty"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class


#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_MFG_COVERGAE_RISK_TYPE_AND_MODEL_ERROR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As MfgCoverage = CType(objectToValidate, MfgCoverage)

            If Not obj.RiskTypeId.Equals(Guid.Empty) AndAlso Not obj.Model Is Nothing Then
                Return False
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class RiskTypeOrMfgRequired
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_MFG_OR_RISK_TYPE_REQD_ERROR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As MfgCoverage = CType(objectToValidate, MfgCoverage)

            If obj.RiskTypeId.Equals(Guid.Empty) AndAlso obj.ManufacturerId.Equals(Guid.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class

#End Region
End Class


