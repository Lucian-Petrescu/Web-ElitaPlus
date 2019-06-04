'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/9/2015)  ********************

Public Class AfaAcctAmtSrcMapping
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
            Dim dal As New AfaAcctAmtSrcMappingDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New AfaAcctAmtSrcMappingDAL
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
        Me.Countfieldtouse = "CNTFIELDEPRISM_B" 'set default value
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(AfaAcctAmtSrcMappingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_ACCT_AMT_SRC_MAPPING_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AcctAmtSrcId() As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_ACCT_AMT_SRC_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_ACCT_AMT_SRC_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_ACCT_AMT_SRC_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property Operation() As String
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_OPERATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_OPERATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_OPERATION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property InvRateBucketId() As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_INV_RATE_BUCKET_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_INV_RATE_BUCKET_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_INV_RATE_BUCKET_ID, Value)
        End Set
    End Property



    Public Property AfaProductId() As Guid
        Get
            CheckDeleted()
            If row(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_PRODUCT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_PRODUCT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_AFA_PRODUCT_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property LossType() As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            If Not (Value = String.Empty AndAlso Row(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE) Is DBNull.Value) Then
                'don't overwrite the null value and trigger the object dirty
                Me.SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_LOSS_TYPE, Value)
            End If

        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property Countfieldtouse() As String
        Get
            CheckDeleted()
            If Row(AfaAcctAmtSrcMappingDAL.COL_NAME_COUNTFIELDTOUSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaAcctAmtSrcMappingDAL.COL_NAME_COUNTFIELDTOUSE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaAcctAmtSrcMappingDAL.COL_NAME_COUNTFIELDTOUSE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaAcctAmtSrcMappingDAL
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

    'Save to database even not the dataset creator
    Public Sub SaveWithoutCheckDSCreator()
        Try
            MyBase.Save()

            If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaAcctAmtSrcMappingDAL
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
    Public Shared Function getFormularByProdLossType(ByVal AcctAmtSrcID As Guid, ByVal prodID As Guid, ByVal LossType As String, ByVal CntToUse As String, Optional ByVal blnGetMatchedOnly As Boolean = False) As DataView
        Dim dal As New AfaAcctAmtSrcMappingDAL
        Dim ds As DataSet = dal.LoadFormularByProdLossType(AcctAmtSrcID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, prodID, LossType, CntToUse, blnGetMatchedOnly)
        Return ds.Tables(0).DefaultView
    End Function

    Public Shared Function getList(ByVal AcctAmtSrcID As Guid, ByVal prodID As Guid, ByVal LossType As String, ByVal CntToUse As String, Optional ByVal blnGetMatchedOnly As Boolean = False) As Collections.Generic.List(Of AfaAcctAmtSrcMapping)
        Dim dal As New AfaAcctAmtSrcMappingDAL
        Dim ds As DataSet = dal.LoadList(AcctAmtSrcID, prodID, LossType, CntToUse, blnGetMatchedOnly)
        Dim mappingList As New Collections.Generic.List(Of AfaAcctAmtSrcMapping)
        For Each dr As DataRow In ds.Tables(0).Rows
            mappingList.Add(New AfaAcctAmtSrcMapping(dr))
        Next
        Return mappingList

    End Function

    Public Shared Function getProductByDealer(ByVal dealerId As Guid, ByVal productCode As String) As DataView
        Try
            Dim dal As New AfaAcctAmtSrcMappingDAL
            Return dal.LoadProductByDealer(dealerId, productCode).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class