Public Class CheckReject
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
                Dim dal As New CheckPaymentDAL
                If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                    dal.LoadSchema(Me.Dataset)
                End If
                If Me.Dataset.Tables(dal.TABLE_NAME).Rows.Count = 1 Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows(0).Delete()
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

        Protected Sub Load(ByVal id As Guid)
            Try
                Dim dal As New CheckPaymentDAL
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
        Private _cert As Certificate

        'Initialization code for new objects
        Private Sub Initialize()
        End Sub

#End Region

#Region "Properties"

        'Key Property
        Public ReadOnly Property Id() As Guid
            Get
                If Row(CheckPaymentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(CheckPaymentDAL.COL_NAME_PAYMENT_ID), Byte()))
                End If
            End Get
        End Property

    <ValueMandatory("")>
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CheckPaymentDAL.COL_NAME_REFERENCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CheckPaymentDAL.COL_NAME_REFERENCE_ID, Value)
            Me._cert = Nothing
        End Set
    End Property

    <ValueMandatory("", Message:="SELECT_REJECT_REASON")>
    Public Property RejectReasonXcd() As String
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_REJECT_REASON_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CheckPaymentDAL.COL_NAME_REJECT_REASON_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CheckPaymentDAL.COL_NAME_REJECT_REASON_XCD, Value)
        End Set
    End Property

    Public Property SelectRejectCheck() As String
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_CHECK_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CheckPaymentDAL.COL_NAME_CHECK_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CheckPaymentDAL.COL_NAME_CHECK_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("", Message:="CHECK_REJECT_DATE_REQUIRED"), ValidDateRange("", Max:="31-Dec-2099", Min:="01-Jan-1901", Message:="INVALID_DATE")>
    Public Property RejectDate() As DateType
        Get
            CheckDeleted()
            If Row(CheckPaymentDAL.COL_NAME_REJECT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CheckPaymentDAL.COL_NAME_REJECT_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CheckPaymentDAL.COL_NAME_REJECT_DATE, Value)
        End Set
    End Property


    <ValidStringLength("Comments", Max:=160)>
        Public Property Comments() As String
            Get
                CheckDeleted()
                If Row(CheckPaymentDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                    Return String.Empty
                Else
                    Return CType(Row(CheckPaymentDAL.COL_NAME_COMMENTS), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(CheckPaymentDAL.COL_NAME_COMMENTS, Value)
            End Set
        End Property


#End Region

#Region "Public Members"
    Public Sub AddRejectPayment(ByVal reject_date As Date,
                               ByVal payment_id As Guid,
                               ByVal reject_reason As String,
                               ByVal comments As String,
                               ByRef err_no As Integer,
                               ByRef err_msg As String)
        Try
            If Me._isDSCreator AndAlso Me.IsDirty Then
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

    Public Sub PopulateWithDefaultValues(ByVal certId As Guid, Optional ByVal claimId As Object = Nothing)
            Dim cert As New Certificate(certId)
            Me.CertId = certId
            Me.SetValue(DALBase.COL_NAME_CREATED_BY, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        End Sub

        Public Shared Function GetNewCheckPayment(ByVal certId As Guid) As CheckPayment
            Dim c As New CheckPayment
            c.PopulateWithDefaultValues(certId)
            Return c
        End Function

#End Region

End Class
