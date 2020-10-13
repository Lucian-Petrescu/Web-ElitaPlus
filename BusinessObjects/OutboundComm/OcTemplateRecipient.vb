'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/15/2017)  ********************
Imports System.Text.RegularExpressions

Public Class OcTemplateRecipient
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
            Dim dal As New OcTemplateRecipientDAL
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
            Dim dal As New OcTemplateRecipientDAL            
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
            If row(OcTemplateRecipientDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(OcTemplateRecipientDAL.COL_NAME_OC_TEMPLATE_RECIPIENT_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property OcTemplateId As Guid
        Get
            CheckDeleted()
            If row(OcTemplateRecipientDAL.COL_NAME_OC_TEMPLATE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(OcTemplateRecipientDAL.COL_NAME_OC_TEMPLATE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateRecipientDAL.COL_NAME_OC_TEMPLATE_ID, Value)
        End Set
    End Property

    <NewValueMandatory(""), ValidStringLength("", Max:=400)>
    Public Property RecipientSourceFieldXcd As String
        Get
            CheckDeleted()
            If row(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_SOURCE_FIELD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_SOURCE_FIELD_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_SOURCE_FIELD_XCD, Value)
        End Set
    End Property

    Public Property RecipientSourceFieldDescription As String
        Get
            CheckDeleted()
            If Row(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_SOURCE_FIELD_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_SOURCE_FIELD_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_SOURCE_FIELD_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=800)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(OcTemplateRecipientDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateRecipientDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateRecipientDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidEmailAddress("")>
    Public Property RecipientAddress As String
        Get
            CheckDeleted()
            If Row(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_ADDRESS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateRecipientDAL.COL_NAME_RECIPIENT_ADDRESS, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New OcTemplateRecipientDAL
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
    Public Shared Function GetList(templateId As Guid) As DataView
        Try
            Dim dal As New OcTemplateRecipientDAL
            Return New DataView(dal.LoadList(templateId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "TemplateRecipientDV"
    Public Class TemplateRecipientsDV
        Inherits DataView

#Region "Constants"
        Public Const COL_OC_TEMPLATE_RECIPIENT_ID As String = "OC_TEMPLATE_RECIPIENT_ID"
        Public Const COL_OC_TEMPLATE_ID As String = "OC_TEMPLATE_ID"
        Public Const COL_RECIPIENT_SOURCE_FIELD_XCD As String = "RECIPIENT_SOURCE_FIELD_XCD"
        Public Const COL_RECIPIENT_SOURCE_FIELD_DESCRIPTION As String = "RECIPIENT_SOURCE_FIELD_DESC"
        Public Const COL_RECIPIENT_ADDRESS As String = "RECIPIENT_ADDRESS"
        Public Const COL_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_CREATED_BY As String = "CREATED_BY"
        Public Const COL_CREATED_DATE As String = "CREATED_DATE"
        Public Const COL_MODIFIED_BY As String = "MODIFIED_BY"
        Public Const COL_MODIFIED_DATE As String = "MODIFIED_DATE"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As TemplateRecipientsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(COL_OC_TEMPLATE_RECIPIENT_ID) = (New Guid()).ToByteArray
            row(COL_OC_TEMPLATE_ID) = Guid.Empty.ToByteArray
            row(COL_RECIPIENT_SOURCE_FIELD_XCD) = DBNull.Value
            row(COL_RECIPIENT_SOURCE_FIELD_DESCRIPTION) = DBNull.Value
            row(COL_RECIPIENT_ADDRESS) = DBNull.Value
            row(COL_DESCRIPTION) = DBNull.Value
            row(COL_CREATED_BY) = DBNull.Value
            row(COL_CREATED_DATE) = DBNull.Value
            row(COL_MODIFIED_BY) = DBNull.Value
            row(COL_MODIFIED_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New TemplateRecipientsDV(dt)
        End Function
    End Class
#End Region
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class NewValueMandatory
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As OcTemplateRecipient = CType(objectToValidate, OcTemplateRecipient)

            If String.IsNullOrEmpty(obj.RecipientSourceFieldXcd) AndAlso String.IsNullOrEmpty(obj.RecipientAddress) Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidEmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As OcTemplateRecipient = CType(objectToValidate, OcTemplateRecipient)
            Dim emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", RegexOptions.None, Timespan.FromMilliseconds(250))

            If Not String.IsNullOrEmpty(obj.RecipientAddress) AndAlso Not emailExpression.IsMatch(obj.RecipientAddress) Then
                Return False
            End If

            Return True

        End Function
    End Class

#End Region

End Class


