'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/18/2005)  ********************

Public Class PartsInfo
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
            Dim dal As New PartsInfoDAL
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
            Dim dal As New PartsInfoDAL
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
            If row(PartsInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PartsInfoDAL.COL_NAME_PARTS_INFO_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(PartsInfoDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PartsInfoDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PartsInfoDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PartsDescriptionId() As Guid
        Get
            CheckDeleted()
            If row(PartsInfoDAL.COL_NAME_PARTS_DESCRIPTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PartsInfoDAL.COL_NAME_PARTS_DESCRIPTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PartsInfoDAL.COL_NAME_PARTS_DESCRIPTION_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=NEW_MAX_LONG, Min:=0)> _
    Public Property Cost() As DecimalType
        Get
            CheckDeleted()
            If Row(PartsInfoDAL.COL_NAME_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PartsInfoDAL.COL_NAME_COST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(PartsInfoDAL.COL_NAME_COST, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InStockID() As Guid
        Get
            CheckDeleted()
            If Row(PartsInfoDAL.COL_NAME_IN_STOCK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PartsInfoDAL.COL_NAME_IN_STOCK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PartsInfoDAL.COL_NAME_IN_STOCK_ID, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New PartsInfoDAL
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
    Public Shared Function getTotalCost(ByVal claimID As Guid) As DecimalType
        Dim retVal As Decimal = 0
        Try
            Dim dal As New PartsInfoDAL

            Dim claimParts As DataSet = dal.LoadSelectedList(claimID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            If claimParts.Tables.Count > 0 Then
                For Each row As DataRow In claimParts.Tables(0).Rows
                    retVal = retVal + CType(row(PartsInfoDAL.COL_NAME_COST), Decimal)
                Next
            End If

            Return New DecimalType(retVal)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getAvailList(ByVal riskGroupID As Guid, ByVal claimID As Guid) As PartsInfoDV
        Try
            Dim dal As New PartsInfoDAL
            Return New PartsInfoDV(dal.LoadAvailList(riskGroupID, claimID, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    ' for populating the grid to edit,we make sure the part we are editing is populated in this data set we are returning.
    Public Shared Function getAvailListWithCurrentPart(ByVal riskGroupID As Guid, ByVal claimID As Guid, ByVal partsDescId As Guid) As PartsInfoDV
        Try
            Dim dal As New PartsInfoDAL

            Return New PartsInfoDV(dal.LoadAvailListWithCurrentPart(riskGroupID, claimID, partsDescId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getSelectedList(ByVal claimID As Guid) As PartsInfoDV

        Try
            Dim dal As New PartsInfoDAL

            Return New PartsInfoDV(dal.LoadSelectedList(claimID, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Class PartsInfoDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PARTS_INFO_ID As String = "parts_info_id"
        Public Const COL_NAME_CLAIM_ID As String = "claim_id"
        Public Const COL_NAME_PARTS_DESCRIPTION As String = "description"
        Public Const COL_NAME_IN_STOCK_ID As String = "in_stock_id"
        Public Const COL_NAME_IN_STOCK_DESCRIPTION As String = "in_stock_description"
        Public Const COL_NAME_COST As String = "cost"
        Public Const COL_NAME_PARTS_DESCRIPTION_ID As String = "parts_description_id"
        Public Const COL_NAME_CODE As String = "code"
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property PartsDesc(ByVal row) As String
            Get
                Return row(COL_NAME_PARTS_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property InStockDesc(ByVal row) As String
            Get
                Return row(COL_NAME_IN_STOCK_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Cost(ByVal row As DataRow) As DecimalType
            Get
                Return New DecimalType(CType(row(COL_NAME_COST), Decimal))
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property
    End Class

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal partsInfoId As Guid, ByVal claimID As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(PartsInfoDAL.COL_NAME_PARTS_INFO_ID) = partsInfoId.ToByteArray
        row(PartsInfoDAL.COL_NAME_PARTS_DESCRIPTION_ID) = Guid.Empty.ToByteArray
        row(PartsInfoDAL.COL_NAME_IN_STOCK_ID) = Guid.Empty.ToByteArray
        row(PartsInfoDAL.COL_NAME_CLAIM_ID) = claimID.ToByteArray
        row(PartsInfoDAL.COL_NAME_PARTS_INFO_ID) = partsInfoId.ToByteArray
        row(PartsInfoDAL.COL_NAME_COST) = 0
        dt.Rows.Add(row)

        Return (dv)

    End Function

    Public Function AddTransactionLogHeader(ByVal tranLogHeaderId As Guid) As TransactionLogHeader
        Dim objTranLogHeader As TransactionLogHeader

        If Not tranLogHeaderId.Equals(Guid.Empty) Then
            objTranLogHeader = New TransactionLogHeader(tranLogHeaderId, Me.Dataset)
        Else
            objTranLogHeader = New TransactionLogHeader(Me.Dataset)
        End If

        Return objTranLogHeader
    End Function

    Public Shared Sub AddNewRowToPartsInfoSearchDV(ByRef dv As PartsInfoDV, ByVal NewPartsInfoBO As PartsInfo)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewPartsInfoBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(PartsInfoDV.COL_NAME_PARTS_INFO_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(PartsInfoDV.COL_NAME_PARTS_DESCRIPTION, GetType(String))
                dt.Columns.Add(PartsInfoDV.COL_NAME_IN_STOCK_DESCRIPTION, GetType(String))
                dt.Columns.Add(PartsInfoDV.COL_NAME_COST, GetType(Decimal))
                dt.Columns.Add(PartsInfoDV.COL_NAME_CLAIM_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(PartsInfoDV.COL_NAME_PARTS_DESCRIPTION_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(PartsInfoDV.COL_NAME_IN_STOCK_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(PartsInfoDV.COL_NAME_CODE, GetType(String))

            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(PartsInfoDV.COL_NAME_PARTS_INFO_ID) = NewPartsInfoBO.Id.ToByteArray
            row(PartsInfoDV.COL_NAME_PARTS_DESCRIPTION) = ""
            row(PartsInfoDV.COL_NAME_IN_STOCK_DESCRIPTION) = NewPartsInfoBO.InStockID.ToByteArray
            row(PartsInfoDV.COL_NAME_COST) = 0
            row(PartsInfoDV.COL_NAME_CLAIM_ID) = NewPartsInfoBO.ClaimId.ToByteArray
            row(PartsInfoDV.COL_NAME_PARTS_DESCRIPTION_ID) = NewPartsInfoBO.PartsDescriptionId.ToByteArray
            row(PartsInfoDV.COL_NAME_IN_STOCK_ID) = NewPartsInfoBO.InStockID.ToByteArray
            row(PartsInfoDV.COL_NAME_CODE) = ""
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New PartsInfoDV(dt)
        End If
    End Sub
#End Region

End Class


