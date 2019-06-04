'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/29/2011)  ********************
Imports Assurant.ElitaPlus.Common
Imports Assurant.Common

Public Class CertCancelRequest
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
            Dim dal As New CertCancelRequestDAL
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
            Dim dal As New CertCancelRequestDAL
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

#Region "Attributes"
    Dim cancellationReasonDesc As String
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(CertCancelRequestDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertCancelRequestDAL.COL_NAME_CERT_CANCEL_REQUEST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If row(CertCancelRequestDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertCancelRequestDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancelRequestDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CancellationReasonId() As Guid
        Get
            CheckDeleted()
            If row(CertCancelRequestDAL.COL_NAME_CANCELLATION_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertCancelRequestDAL.COL_NAME_CANCELLATION_REASON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancelRequestDAL.COL_NAME_CANCELLATION_REASON_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CancellationRequestDate() As DateType
        Get
            CheckDeleted()
            If row(CertCancelRequestDAL.COL_NAME_CANCELLATION_REQUEST_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertCancelRequestDAL.COL_NAME_CANCELLATION_REQUEST_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertCancelRequestDAL.COL_NAME_CANCELLATION_REQUEST_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CancellationDate() As DateType
        Get
            CheckDeleted()
            If row(CertCancelRequestDAL.COL_NAME_CANCELLATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertCancelRequestDAL.COL_NAME_CANCELLATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertCancelRequestDAL.COL_NAME_CANCELLATION_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property Status() As String
        Get
            CheckDeleted()
            If Row(CertCancelRequestDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(CertCancelRequestDAL.COL_NAME_STATUS), String))
            End If
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            CheckDeleted()
            If Row(CertCancelRequestDAL.COL_NAME_STATUS_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(CertCancelRequestDAL.COL_NAME_STATUS_DESCRIPTION), String))
            End If
        End Get
    End Property

    Public ReadOnly Property StatusDate() As DateType
        Get
            CheckDeleted()
            If Row(CertCancelRequestDAL.COL_NAME_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertCancelRequestDAL.COL_NAME_STATUS_DATE), Date))
            End If
        End Get
    End Property

    Public Property ProofOfDocumentation() As String
        Get
            CheckDeleted()
            If Row(CertCancelRequestDAL.COL_NAME_PROOF_OF_DOCUMENTATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(CertCancelRequestDAL.COL_NAME_PROOF_OF_DOCUMENTATION), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertCancelRequestDAL.COL_NAME_PROOF_OF_DOCUMENTATION, Value)
        End Set
    End Property

    Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancelRequestDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancelRequestDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancelRequestDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertCancelRequestDAL
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
    Public Shared Function GetCertCancelRequestData(ByVal certId As Guid) As Dataset
        Try
            Dim dal As New CertCancelRequestDAL
            Dim ds As DataSet
            ds = dal.LoadByCertId(certId)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Shared Methods"

    Public Shared Sub SetProcessCancelRequestData(ByVal oCertCancelRequestBO As CertCancelRequest, ByVal useExistingBankInfo As String, ByVal oCRequestBankInfoBO As BankInfo, ByVal oCancReqCommentBO As Comment, ByVal oCertCancelRequestData As CertCancelRequestData)
        With oCertCancelRequestData
            .cancellationReasonId = oCertCancelRequestBO.CancellationReasonId
            .cancelRequestDate = oCertCancelRequestBO.CancellationRequestDate
            .cancellationDate = oCertCancelRequestBO.CancellationDate
            .certCancelRequestId = oCertCancelRequestBO.Id
            .proofOfDocumentation = oCertCancelRequestBO.ProofOfDocumentation
            .useExistingBankInfo = useExistingBankInfo
            .bankInfoId = oCertCancelRequestBO.BankInfoId
            .ibanNumber = oCRequestBankInfoBO.IbanNumber
            .accountNumber = oCRequestBankInfoBO.Account_Number
            .callerName = oCancReqCommentBO.CallerName
            .commentTypeId = oCancReqCommentBO.CommentTypeId
            .comments = oCancReqCommentBO.Comments
            .created_by = ElitaPlusIdentity.Current.ActiveUser.UserName
            .status_description = oCertCancelRequestBO.StatusDescription
        End With
    End Sub

#End Region

#Region "StoreProcedures Control"

    Public Shared Function CertCancelRequest(ByVal oCertCancelRequestData As CertCancelRequestData, ByRef dblRefundAmount As Double, ByRef strMsg As String) As CertCancelRequestData
        Try
            Dim dal As New CertCancelRequestDAL

            dal.ExecuteCancelRequestSP(oCertCancelRequestData, dblRefundAmount, strMsg)
            Return oCertCancelRequestData

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
End Class


