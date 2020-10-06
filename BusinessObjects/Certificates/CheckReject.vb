Public Class CheckReject
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
                Dim dal As New CheckPaymentDAL
                If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                    dal.LoadSchema(Dataset)
                End If
                If Dataset.Tables(dal.TABLE_NAME).Rows.Count = 1 Then
                    Dataset.Tables(dal.TABLE_NAME).Rows(0).Delete()
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
                Dim dal As New CheckPaymentDAL
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
        Private _cert As Certificate

        'Initialization code for new objects
        Private Sub Initialize()
        End Sub

#End Region

#Region "Properties"

        'Key Property
        Public ReadOnly Property Id As Guid
            Get
                If Row(CheckPaymentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(CheckPaymentDAL.COL_NAME_PAYMENT_ID), Byte()))
                End If
            End Get
        End Property

    <ValueMandatory("")>
    Public Property CertId As Guid
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CheckPaymentDAL.COL_NAME_REFERENCE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CheckPaymentDAL.COL_NAME_REFERENCE_ID, Value)
            _cert = Nothing
        End Set
    End Property

    <ValueMandatory("", Message:="SELECT_REJECT_REASON")>
    Public Property RejectReasonXcd As String
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_REJECT_REASON_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CheckPaymentDAL.COL_NAME_REJECT_REASON_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CheckPaymentDAL.COL_NAME_REJECT_REASON_XCD, Value)
        End Set
    End Property

    Public Property SelectRejectCheck As String
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_CHECK_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CheckPaymentDAL.COL_NAME_CHECK_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CheckPaymentDAL.COL_NAME_CHECK_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("", Message:="CHECK_REJECT_DATE_REQUIRED"), ValidDateRange("", Max:="31-Dec-2099", Min:="01-Jan-1901", Message:="INVALID_DATE")>
    Public Property RejectDate As DateType
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_REJECT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CheckPaymentDAL.COL_NAME_REJECT_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CheckPaymentDAL.COL_NAME_REJECT_DATE, Value)
        End Set
    End Property


    <ValidStringLength("Comments", Max:=160)>
        Public Property Comments As String
            Get
                CheckDeleted()
                If Row(CheckPaymentDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                    Return String.Empty
                Else
                    Return CType(Row(CheckPaymentDAL.COL_NAME_COMMENTS), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(CheckPaymentDAL.COL_NAME_COMMENTS, Value)
            End Set
        End Property


#End Region

#Region "Public Members"
    Public Sub AddRejectPayment(reject_date As Date,
                               payment_id As Guid,
                               reject_reason As String,
                               comments As String,
                               ByRef err_no As Integer,
                               ByRef err_msg As String)
        Try
            If _isDSCreator AndAlso IsDirty Then
                err_no = 0
                err_msg = String.Empty

                Dim dal As New CheckPaymentDAL
                dal.AddRejectPayment(reject_date,
                               payment_id,
                               reject_reason,
                               comments,
                               err_no,
                               err_msg)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub PopulateWithDefaultValues(certId As Guid, Optional ByVal claimId As Object = Nothing)
            Dim cert As New Certificate(certId)
            Me.CertId = certId
            SetValue(DALBase.COL_NAME_CREATED_BY, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        End Sub

        Public Shared Function GetNewCheckPayment(certId As Guid) As CheckPayment
            Dim c As New CheckPayment
            c.PopulateWithDefaultValues(certId)
            Return c
        End Function

#End Region

End Class
