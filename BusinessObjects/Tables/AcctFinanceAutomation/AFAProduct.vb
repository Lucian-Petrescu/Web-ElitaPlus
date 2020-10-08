'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/15/2015)  ********************

Public Class AfAProduct
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
            Dim dal As New AFAProductDAL
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
            Dim dal As New AFAProductDAL
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
            If Row(AFAProductDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AFAProductDAL.COL_NAME_AFA_PRODUCT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(AFAProductDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AFAProductDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAProductDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40), ProductCodeValidator("")> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(AFAProductDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAProductDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAProductDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(AFAProductDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAProductDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAProductDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property ProductType As String
        Get
            CheckDeleted()
            If Row(AFAProductDAL.COL_NAME_PRODUCT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAProductDAL.COL_NAME_PRODUCT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAProductDAL.COL_NAME_PRODUCT_TYPE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AFAProductDAL
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
    Public Shared Function getList(dealerId As Guid, productCode As String) As AFAProductSearchDV
        Try
            Dim dal As New AFAProductDAL
            Return New AFAProductSearchDV(dal.LoadList(dealerId, productCode).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"
    Public Class AFAProductSearchDV
        Inherits DataView

        Public Const COL_AFA_PRODUCT_ID As String = "afa_product_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_CODE As String = "code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_PRODUCT_TYPE As String = "product_type"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class


    Public Shared Sub AddNewRowToSearchDV(ByRef dv As AFAProductSearchDV, NewBO As AfaProduct)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(AFAProductSearchDV.COL_AFA_PRODUCT_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(AFAProductSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(AFAProductSearchDV.COL_CODE, GetType(String))
                dt.Columns.Add(AFAProductSearchDV.COL_DESCRIPTION, GetType(String))
                dt.Columns.Add(AFAProductSearchDV.COL_PRODUCT_TYPE, GetType(String))

            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(AFAProductSearchDV.COL_AFA_PRODUCT_ID) = NewBO.Id.ToByteArray
            row(AFAProductSearchDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            row(AFAProductSearchDV.COL_CODE) = NewBO.Code
            row(AFAProductSearchDV.COL_DESCRIPTION) = NewBO.Description
            row(AFAProductSearchDV.COL_PRODUCT_TYPE) = NewBO.ProductType

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New AFAProductSearchDV(dt)
        End If
    End Sub

#End Region


#Region "Custom Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ProductCodeValidator
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_PRODUCT_CODE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As AfAProduct = CType(objectToValidate, AfAProduct)
            Dim dal As New AFAProductDAL

            If (Not obj.Code Is Nothing) AndAlso (obj.Code.Trim <> String.Empty) Then

                If Not dal.IsProdCodeUnique(obj.DealerId, obj.Code, obj.Id) Then
                    Return False
                End If
            End If
            Return True

        End Function
    End Class
#End Region


End Class