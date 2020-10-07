'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/4/2012)  ********************

Public Class DealerRejectCode
    Inherits BusinessObjectBase


#Region "Constants"
    Private Const SEARCH_EXCEPTION As String = "DEALER IS REQUIRED" ' Dealer Reject Code Search Exception
    Public Const NO_DEALER_SELECTED = "--"
    Public Const NO_RECORDS_FOUND = "NO RECORDS FOUND."
#End Region

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
            Dim dal As New DealerRejectCodeDAL
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
            Dim dal As New DealerRejectCodeDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(DealerRejectCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerRejectCodeDAL.COL_NAME_DEALER_REJECT_CODE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(DealerRejectCodeDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerRejectCodeDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(DealerRejectCodeDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property MsgCodeId() As Guid
        Get
            CheckDeleted()
            If Row(DealerRejectCodeDAL.COL_NAME_MSG_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerRejectCodeDAL.COL_NAME_MSG_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(DealerRejectCodeDAL.COL_NAME_MSG_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RecordTypeId() As Guid
        Get
            CheckDeleted()
            If Row(DealerRejectCodeDAL.COL_NAME_RECORD_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerRejectCodeDAL.COL_NAME_RECORD_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(DealerRejectCodeDAL.COL_NAME_RECORD_TYPE_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Function AddDealerRejectCode(ByVal dealerRejectCodeId As Guid) As DealerRejectCode
        Dim objDealerRejectCode As DealerRejectCode

        If Not dealerRejectCodeId.Equals(Guid.Empty) Then
            objDealerRejectCode = New DealerRejectCode(dealerRejectCodeId, Dataset)
        Else
            objDealerRejectCode = New DealerRejectCode(Dataset)
        End If

        Return objDealerRejectCode
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerRejectCodeDAL
                ''''dal.Update(Me.Row)
                UpdateFamily(Dataset)
                dal.UpdateFamily(Dataset)
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
    Public Shared Function GetList(ByVal languageId As Guid,
                                ByVal recordTypeId As Guid,
                                ByVal dealerId As Guid,
                                ByVal rejectCodeMask As String,
                                ByVal rejectReasonMask As String,
                                ByVal rejectMsgTypeId As Guid) As DealerRejectCodeSearchDV
        Try
            Dim dal As New DealerRejectCodeDAL

            If (dealerId = Guid.Empty) Then
                Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Certificate), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(DealerRejectCode).FullName)
            End If

            Return New DealerRejectCodeSearchDV(dal.LoadList(languageId, recordTypeId, dealerId, rejectCodeMask, rejectReasonMask, rejectMsgTypeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#Region "DealerRejectCodeSearchDV"

    Public Class DealerRejectCodeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_DEALER_ID As String = "DEALER_ID"
        Public Const COL_REJECT_CODE As String = "REJECT_CODE"
        Public Const COL_REJECT_REASON As String = "REJECT_REASON"
        Public Const COL_RECORD_TYPE_ID As String = "RECORD_TYPE_ID"
        Public Const COL_LANGUAGE_ID As String = "LANGUAGE_ID"
        Public Const COL_DEALER_REJECT_CODE_ID As String = "DEALER_REJECT_CODE_ID"

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


