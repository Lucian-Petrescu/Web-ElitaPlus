'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/9/2015)  ********************

Public Class AfaAcctAmtSrcMapping
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
            Dim dal As New AfaAcctAmtSrcMappingDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New AfaAcctAmtSrcMappingDAL
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
        Countfieldtouse = "CNTFIELDEPRISM_B" 'set default value
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(AfaAcctAmtSrcMappingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_ACCT_AMT_SRC_MAPPING_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AcctAmtSrcId As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_ACCT_AMT_SRC_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_ACCT_AMT_SRC_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_ACCT_AMT_SRC_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property Operation As String
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_OPERATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_OPERATION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_OPERATION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property InvRateBucketId As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_INV_RATE_BUCKET_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_INV_RATE_BUCKET_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_INV_RATE_BUCKET_ID, Value)
        End Set
    End Property



    Public Property AfaProductId As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_PRODUCT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_PRODUCT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_PRODUCT_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property LossType As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Not (Value = String.Empty AndAlso Row(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE) Is DBNull.Value) Then
                'don't overwrite the null value and trigger the object dirty
                SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE, Value)
            End If

        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property Countfieldtouse As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcMappingDAL.COL_NAME_COUNTFIELDTOUSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcMappingDAL.COL_NAME_COUNTFIELDTOUSE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_COUNTFIELDTOUSE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaAcctAmtSrcMappingDAL
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

    'Save to database even not the dataset creator
    Public Sub SaveWithoutCheckDSCreator()
        Try
            MyBase.Save()

            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaAcctAmtSrcMappingDAL
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
    Public Shared Function getFormularByProdLossType(AcctAmtSrcID As Guid, prodID As Guid, LossType As String, CntToUse As String, Optional ByVal blnGetMatchedOnly As Boolean = False) As DataView
        Dim dal As New AfaAcctAmtSrcMappingDAL
        Dim ds As DataSet = dal.LoadFormularByProdLossType(AcctAmtSrcID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, prodID, LossType, CntToUse, blnGetMatchedOnly)
        Return ds.Tables(0).DefaultView
    End Function

    Public Shared Function getList(AcctAmtSrcID As Guid, prodID As Guid, LossType As String, CntToUse As String, Optional ByVal blnGetMatchedOnly As Boolean = False) As Collections.Generic.List(Of AfaAcctAmtSrcMapping)
        Dim dal As New AfaAcctAmtSrcMappingDAL
        Dim ds As DataSet = dal.LoadList(AcctAmtSrcID, prodID, LossType, CntToUse, blnGetMatchedOnly)
        Dim mappingList As New Collections.Generic.List(Of AfaAcctAmtSrcMapping)
        For Each dr As DataRow In ds.Tables(0).Rows
            mappingList.Add(New AfaAcctAmtSrcMapping(dr))
        Next
        Return mappingList

    End Function

    Public Shared Function getProductByDealer(dealerId As Guid, productCode As String) As DataView
        Try
            Dim dal As New AfaAcctAmtSrcMappingDAL
            Return dal.LoadProductByDealer(dealerId, productCode).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class