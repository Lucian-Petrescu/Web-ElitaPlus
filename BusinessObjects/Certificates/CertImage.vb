'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/29/2016)  ********************

Public Class CertImage
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
    Public Sub New(ByVal familyDS As DataSet, ByVal parentId As Guid)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
        Me.Initialize(parentId)
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CertImageDAL
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
            Dim dal As New CertImageDAL
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
    Private Sub Initialize(ByVal parentId As Guid)
        Me.CertId = parentId
    End Sub

    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CertImageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertImageDAL.COL_NAME_CERT_IMAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertImageDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ImageId() As Guid
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_IMAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertImageDAL.COL_NAME_IMAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_IMAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property DocumentTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_DOCUMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertImageDAL.COL_NAME_DOCUMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_DOCUMENT_TYPE_ID, Value)
        End Set
    End Property



    Public Property ScanDate() As DateType
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_SCAN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertImageDAL.COL_NAME_SCAN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_SCAN_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100), ValidateDuplicateFileName("")>
    Public Property FileName() As String
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_FILE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertImageDAL.COL_NAME_FILE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_FILE_NAME, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property FileSizeBytes() As LongType
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_FILE_SIZE_BYTES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertImageDAL.COL_NAME_FILE_SIZE_BYTES), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_FILE_SIZE_BYTES, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)>
    Public Property Comments() As String
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertImageDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property UserName() As String
        Get
            CheckDeleted()
            If Row(CertImageDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertImageDAL.COL_NAME_USER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertImageDAL.COL_NAME_USER_NAME, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertImageDAL
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

#Region "Certificate Image List Selection View"
    Public Class CertImagesList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As Certificate)
            MyBase.New(LoadTable(parent), GetType(CertImage), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, CertImage).CertId.Equals(CType(Parent, Certificate).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As Certificate) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(CertImagesList)) Then
                    Dim dal As New CertImageDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(CertImagesList))
                End If
                Return parent.Dataset.Tables(CertImageDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateDuplicateFileNameAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.DUPLICATE_FILE_NAME)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertImage = CType(objectToValidate, CertImage)

            For Each dr As DataRow In obj.Row.Table.Rows
                Try

                    If (New Guid(CType(dr(CertImageDAL.COL_NAME_CERT_IMAGE_ID), Byte())) <> obj.Id) AndAlso
                        (CType(dr(CertImageDAL.COL_NAME_FILE_NAME), String).ToUpperInvariant() = obj.FileName.ToUpperInvariant()) Then
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                End Try

            Next

            Return True

        End Function
    End Class

End Class


