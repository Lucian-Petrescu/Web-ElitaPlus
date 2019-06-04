'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/29/2012)  ********************

Public Class VendorContact
    Inherits BusinessObjectBase
    Implements IExpirable

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
            Dim dal As New VendorContactDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
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
            Dim dal As New VendorContactDAL
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
    Public ReadOnly Property Id() As Guid Implements IExpirable.ID
        Get
            If Row(VendorContactDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorContactDAL.COL_NAME_VENDOR_CONTACT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorContactDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ContactInfoId() As Guid
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_CONTACT_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorContactDAL.COL_NAME_CONTACT_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_CONTACT_INFO_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE)> _
    Public Property Effective() As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(VendorContactDAL.COL_NAME_EFFECTIVE), DateTime))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)> _
    Public Property Expiration() As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(VendorContactDAL.COL_NAME_EXPIRATION), DateTime))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    Public Property AddressTypeId() As Guid
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_ADDRESS_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorContactDAL.COL_NAME_ADDRESS_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_ADDRESS_TYPE_ID, Value)
        End Set
    End Property

    Public Property Name() As String
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorContactDAL.COL_NAME_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_NAME, Value)
        End Set
    End Property

    Public Property Email() As String
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorContactDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property

    Public Property Company() As String
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_COMPANY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorContactDAL.COL_NAME_COMPANY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_COMPANY, Value)
        End Set
    End Property

    Public Property JobTitle() As String
        Get
            CheckDeleted()
            If Row(VendorContactDAL.COL_NAME_JOB_TITLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorContactDAL.COL_NAME_JOB_TITLE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VendorContactDAL.COL_NAME_JOB_TITLE, Value)
        End Set
    End Property



    Private _ContactInfo As ContactInfo = Nothing
    Public ReadOnly Property ContactInfo(ByVal parentDataSet As DataSet) As ContactInfo
        Get
            If Me._ContactInfo Is Nothing Then
                If Me.ContactInfoId.Equals(Guid.Empty) Then
                    Me._ContactInfo = New ContactInfo(parentDataSet, Nothing)
                    Me.ContactInfoId = Me._ContactInfo.Id
                Else
                    Me._ContactInfo = New ContactInfo(Me.ContactInfoId, parentDataSet, Nothing)
                End If
            End If
            Return Me._ContactInfo
        End Get
    End Property

    Public ReadOnly Property IsNew As Boolean Implements IElement.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New VendorContactDAL
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

#End Region

#Region "Dummy Properties Needed for Iexpirable Interface"
    'dummy method
    Private Function Accept(ByRef visitor As IVisitor) As Boolean Implements IElement.Accept

    End Function

    'dummy property
    Private Property Code As String Implements IExpirable.Code
        Get

        End Get
        Set(value As String)

        End Set
    End Property
    'dummy property
    Private Property parent_id As System.Guid Implements IExpirable.parent_id
        Get

        End Get
        Set(value As System.Guid)

        End Set
    End Property
#End Region
End Class


