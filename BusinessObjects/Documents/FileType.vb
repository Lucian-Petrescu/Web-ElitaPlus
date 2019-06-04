'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/18/2015)  ********************
Imports Assurant.ElitaPlus.DALObjects.Documents

Namespace Documents
    Public Class FileType
        Inherits BusinessObjectBase

#Region "Constants"
        Friend Const DUPLICATE_FILE_TYPE_CODE As String = "DUPLICATE_FILE_TYPE_CODE"
        Friend Const DUPLICATE_FILE_TYPE_EXTENSION As String = "DUPLICATE_FILE_TYPE_EXTENSION"
#End Region

#Region "Constructors"

        'New BO attaching to a BO family
        Friend Sub New(ByVal pDataTable As DataTable)
            MyBase.New(False)
            Me.Dataset = pDataTable.DataSet
            Dim newRow As DataRow = pDataTable.NewRow
            pDataTable.Rows.Add(newRow)
            Me.Row = newRow
            SetValue(FileTypeDAL.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        End Sub

        Public Sub New(ByVal row As DataRow)
            MyBase.New(False)
            Me.Dataset = row.Table.DataSet
            Me.Row = row
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
                If Row(FileTypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(FileTypeDAL.COL_NAME_FILE_TYPE_ID), Byte()))
                End If
            End Get
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=50), CheckDuplicateCode("")> _
        Public Property Code() As String
            Get
                CheckDeleted()
                If Row(FileTypeDAL.COL_NAME_CODE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(FileTypeDAL.COL_NAME_CODE), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(FileTypeDAL.COL_NAME_CODE, Value)
            End Set
        End Property


        <ValueMandatory(""), ValidStringLength("", Max:=100)> _
        Public Property Description() As String
            Get
                CheckDeleted()
                If Row(FileTypeDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(FileTypeDAL.COL_NAME_DESCRIPTION), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(FileTypeDAL.COL_NAME_DESCRIPTION, Value)
            End Set
        End Property


        <ValueMandatory(""), ValidStringLength("", Max:=10), CheckDuplicateExtension("")> _
        Public Property Extension() As String
            Get
                CheckDeleted()
                If Row(FileTypeDAL.COL_NAME_EXTENSION) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(FileTypeDAL.COL_NAME_EXTENSION), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(FileTypeDAL.COL_NAME_EXTENSION, Value)
            End Set
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=100)> _
        Public Property MimeType() As String
            Get
                CheckDeleted()
                If Row(FileTypeDAL.COL_NAME_MIME_TYPE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(FileTypeDAL.COL_NAME_MIME_TYPE), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(FileTypeDAL.COL_NAME_MIME_TYPE, Value)
            End Set
        End Property


#End Region

#Region "Public Members"
        <Obsolete("Use DocumentManager.Save to make sure cache is updated properly")> _
        Public Overrides Sub Save()
            Try
                MyBase.Save()
                If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                    Dim dal As New FileTypeDAL
                    dal.Update(Me.Row)
                    'Reload the Data from the DB
                    If Me.Row.RowState <> DataRowState.Detached Then
                        Dim objId As Guid = Me.Id
                        Me.Dataset = New DataSet
                        Me.Row = Nothing
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Try
        End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Custom Validators"
        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class CheckDuplicateCode
            Inherits ValidBaseAttribute

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, DUPLICATE_FILE_TYPE_CODE)
            End Sub

            Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
                Dim obj As FileType = CType(objectToValidate, FileType)
                If (DocumentManager.Current.FileTypes.Where(Function(ft) ft.Code = obj.Code AndAlso ft.Id <> obj.Id).Count() = 0) Then
                    Return True
                Else
                    Return False
                End If
            End Function
        End Class

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class CheckDuplicateExtension
            Inherits ValidBaseAttribute

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, DUPLICATE_FILE_TYPE_EXTENSION)
            End Sub

            Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
                Dim obj As FileType = CType(objectToValidate, FileType)
                If (DocumentManager.Current.FileTypes.Where(Function(ft) ft.Extension = obj.Extension AndAlso ft.Id <> obj.Id).Count() = 0) Then
                    Return True
                Else
                    Return False
                End If
            End Function
        End Class
#End Region

    End Class

End Namespace