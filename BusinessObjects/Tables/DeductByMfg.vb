'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/7/2008)  ********************

Public Class DeductByMfg
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
            Dim dal As New DeductByMfgDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New DeductByMfgDAL
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
            If Row(DeductByMfgDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeductByMfgDAL.COL_NAME_DEDUCT_BY_MFG_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(DeductByMfgDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeductByMfgDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeductByMfgDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ManufacturerId As Guid
        Get
            CheckDeleted()
            If Row(DeductByMfgDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DeductByMfgDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeductByMfgDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(DeductByMfgDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DeductByMfgDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeductByMfgDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property Deductible As DecimalType
        Get
            CheckDeleted()
            Dim deduct As Decimal = 0D
            If Row(DeductByMfgDAL.COL_DEDUCTIBLE) Is DBNull.Value Then
                Return New DecimalType(deduct)
            Else
                Return New DecimalType(CType(Row(DeductByMfgDAL.COL_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DeductByMfgDAL.COL_DEDUCTIBLE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DeductByMfgDAL
                dal.Update(Row)
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

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal dealerId As Guid, _
                                             ByVal manufacturerId As Guid, ByVal CompanyGroupId As Guid) As DeductByMfgSearchDV
        Try
            Dim dal As New DeductByMfgDAL
            Return New DeductByMfgSearchDV(dal.LoadList(dealerId, manufacturerId, CompanyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(DeductByMfgDAL.COL_NAME_DEDUCT_BY_MFG_ID) = id.ToByteArray
        row(DeductByMfgDAL.COL_NAME_DEALER_ID) = Guid.Empty.ToByteArray
        'row(DeductByMfgDAL.COL_NAME_RISK_TYPE_ID) = Guid.Empty.ToByteArray
        row(DeductByMfgDAL.COL_NAME_MANUFACTURER_ID) = Guid.Empty.ToByteArray
        row(DeductByMfgDAL.COL_NAME_MODEL) = String.Empty
        'row(DeductByMfgDAL.COL_NAME_MFG_WARRANTY) = DBNull.Value
        row(DeductByMfgDAL.COL_DEDUCTIBLE) = DBNull.Value
        dt.Rows.Add(row)

        Return (dv)

    End Function
#End Region

#Region "SearchDV"
    Public Class DeductByMfgSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_MFG_COVERAGE_ID As String = DeductByMfgDAL.COL_NAME_DEDUCT_BY_MFG_ID
        Public Const COL_DEALER_ID As String = DeductByMfgDAL.COL_NAME_DEALER_ID
        Public Const COL_MANUFACTURER_ID As String = DeductByMfgDAL.COL_NAME_MANUFACTURER_ID
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_MANUFACTURER_NAME As String = "manufacturer"
        Public Const COL_MODEL As String = "model"
        ' Public Const COL_MFG_WARRANTY As String = "mfg_warranty"
        'Public Const COL_RISK_TYPE_ID As String = "risk_type_id"
        Public Const COL_RISK_TYPE_ENGLISH As String = "risk_type_english"
        Public Const COL_DEDUCTIBLE As String = "deductible"


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
            ' MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_MFG_COVERGAE_RISK_TYPE_AND_MODEL_ERROR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As DeductByMfg = CType(objectToValidate, DeductByMfg)

            ' If Not obj.RiskTypeId.Equals(Guid.Empty) AndAlso Not obj.Model Is Nothing Then
            'Return False
            'End If

            Return True
        End Function
    End Class


#End Region
End Class
