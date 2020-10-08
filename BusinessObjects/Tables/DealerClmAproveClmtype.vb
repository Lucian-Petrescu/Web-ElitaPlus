'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/29/2015)  ********************

Public Class DealerClmAproveClmtype
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
    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet, DealerId As Guid, ClaimTypeId As Guid)
        MyBase.New(False)
        Dataset = familyDS
        LoadByDealerIdClaimTypeId(DealerId, ClaimTypeId)
    End Sub

    'New BO Load not avialable
    Public Sub New(DealerId As Guid, ClaimTypeId As Guid)
        MyBase.New()
        Dataset = New DataSet
        LoadByDealerIdClaimTypeId(DealerId, ClaimTypeId)
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New DealerClmAproveClmtypeDAL
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
            Dim dal As New DealerClmAproveClmtypeDAL
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

    Protected Sub LoadByDealerIdClaimTypeId(DealerId As Guid, claimTypeId As Guid)
        Try
            Dim dal As New DealerClmAproveClmtypeDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(claimTypeId, dal.COL_NAME_CLAIM_TYPE_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByDealerIdClaimTypeId(Dataset, DealerId, claimTypeId)
                Row = FindRow(claimTypeId, dal.COL_NAME_CLAIM_TYPE_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            'If Me.Row Is Nothing Then
            '    Throw New DataNotFoundException
            'End If
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
            If row(DealerClmAproveClmtypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DealerClmAproveClmtypeDAL.COL_NAME_DEALER_CLAIM_APROVE_CLMTYPE_ID), Byte()))
            End If
        End Get
    End Property


    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(DealerClmAproveClmtypeDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DealerClmAproveClmtypeDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerClmAproveClmtypeDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property



    Public Property ClaimTypeId As Guid
        Get
            CheckDeleted()
            If row(DealerClmAproveClmtypeDAL.COL_NAME_CLAIM_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DealerClmAproveClmtypeDAL.COL_NAME_CLAIM_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerClmAproveClmtypeDAL.COL_NAME_CLAIM_TYPE_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerClmAproveClmtypeDAL
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

#End Region

    '#Region "List Methods"
    '    Public Class DealerClaimTypeList
    '        Inherits BusinessObjectListBase
    '        Public Sub New(ByVal parent As CoverageType)
    '            MyBase.New(GetCovLossList(parent), GetType(CoverageLoss), parent)
    '        End Sub
    '        Public Sub New(ByVal parent As CoverageType, ByVal coverageLossId As Guid)
    '            MyBase.New(GetCovLoss(parent, coverageLossId), GetType(CoverageLoss), parent)
    '        End Sub
    '        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
    '            Return True
    '        End Function

    '        Public Function FindById(ByVal CauseOfLossId As Guid) As CoverageLoss
    '            Dim bo As CoverageLoss
    '            For Each bo In Me
    '                If bo.CauseOfLossId.Equals(CauseOfLossId) Then Return bo
    '            Next
    '            Return Nothing
    '        End Function

    '        Public Function FindDefault() As CoverageLoss
    '            Dim bo As CoverageLoss
    '            For Each bo In Me
    '                If bo.DefaultFlag = DEFAULT_FLAG Then
    '                    Return bo
    '                End If
    '            Next
    '            Return Nothing
    '        End Function
    '    End Class

    '#End Region
End Class


