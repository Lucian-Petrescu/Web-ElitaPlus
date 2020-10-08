'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/15/2017)  ********************

Public Class OcTemplateParams
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
            Dim dal As New OcTemplateParamsDAL
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
            Dim dal As New OcTemplateParamsDAL            
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
    Public ReadOnly Property Id As Guid
        Get
            If row(OcTemplateParamsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(OcTemplateParamsDAL.COL_NAME_OC_TEMPLATE_PARAMS_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property OcTemplateId As Guid
        Get
            CheckDeleted()
            If row(OcTemplateParamsDAL.COL_NAME_OC_TEMPLATE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(OcTemplateParamsDAL.COL_NAME_OC_TEMPLATE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_OC_TEMPLATE_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=600)> _
    Public Property ParamName As String
        Get
            CheckDeleted()
            If row(OcTemplateParamsDAL.COL_NAME_PARAM_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateParamsDAL.COL_NAME_PARAM_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_PARAM_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)>
    Public Property ParamValueSourceXcd As String
        Get
            CheckDeleted()
            If row(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE_SOURCE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE_SOURCE_XCD, Value)
        End Set
    End Property

    Public Property ParamValueSourceDescription As String
        Get
            CheckDeleted()
            If Row(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE_SOURCE_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE_SOURCE_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE_SOURCE_DESCRIPTION, Value)
        End Set
    End Property

    <ValidDataType("")>
    Public Property ParamValue As String
        Get
            CheckDeleted()
            If Row(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_PARAM_VALUE, Value)
        End Set
    End Property

    Public Property ParamType As String
        Get
            CheckDeleted()
            If Row(OcTemplateParamsDAL.COL_NAME_PARAM_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateParamsDAL.COL_NAME_PARAM_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_PARAM_TYPE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=400)>
    Public Property ParamDataTypeXcd As String
        Get
            CheckDeleted()
            If row(OcTemplateParamsDAL.COL_NAME_PARAM_DATA_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateParamsDAL.COL_NAME_PARAM_DATA_TYPE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_PARAM_DATA_TYPE_XCD, Value)
        End Set
    End Property

    Public Property ParamDataTypeDescription As String
        Get
            CheckDeleted()
            If Row(OcTemplateParamsDAL.COL_NAME_PARAM_DATA_TYPE_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateParamsDAL.COL_NAME_PARAM_DATA_TYPE_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_PARAM_DATA_TYPE_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)> _
    Public Property DateFormatString As String
        Get
            CheckDeleted()
            If row(OcTemplateParamsDAL.COL_NAME_DATE_FORMAT_STRING) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateParamsDAL.COL_NAME_DATE_FORMAT_STRING), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_DATE_FORMAT_STRING, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)>
    Public Property AllowEmptyValueXcd As String
        Get
            CheckDeleted()
            If row(OcTemplateParamsDAL.COL_NAME_ALLOW_EMPTY_VALUE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateParamsDAL.COL_NAME_ALLOW_EMPTY_VALUE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_ALLOW_EMPTY_VALUE_XCD, Value)
        End Set
    End Property

    Public Property AllowEmptyValueDescription As String
        Get
            CheckDeleted()
            If Row(OcTemplateParamsDAL.COL_NAME_ALLOW_EMPTY_VALUE_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateParamsDAL.COL_NAME_ALLOW_EMPTY_VALUE_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateParamsDAL.COL_NAME_ALLOW_EMPTY_VALUE_DESCRIPTION, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New OcTemplateParamsDAL
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
            Dim dal As New OcTemplateParamsDAL
            Return New DataView(dal.LoadList(templateId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "TemplateParamsDV"
    Public Class TemplateParamsDV
        Inherits DataView

#Region "Constants"
        Public Const COL_OC_TEMPLATE_PARAMS_ID As String = "OC_TEMPLATE_PARAMS_ID"
        Public Const COL_OC_TEMPLATE_ID As String = "OC_TEMPLATE_ID"
        Public Const COL_PARAM_NAME As String = "PARAM_NAME"
        Public Const COL_PARAM_VALUE_SOURCE_XCD As String = "PARAM_VALUE_SOURCE_XCD"
        Public Const COL_PARAM_VALUE_SOURCE_DESCRIPTION As String = "PARAM_VALUE_SOURCE_DESCRIPTION"
        Public Const COL_PARAM_VALUE As String = "PARAM_VALUE"
        Public Const COL_PARAM_TYPE As String = "PARAM_TYPE"
        Public Const COL_PARAM_DATA_TYPE_XCD As String = "PARAM_DATA_TYPE_XCD"
        Public Const COL_PARAM_DATA_TYPE_DESCRIPTION As String = "PARAM_DATA_TYPE_DESCRIPTION"
        Public Const COL_DATE_FORMAT_STRING As String = "DATE_FORMAT_STRING"
        Public Const COL_ALLOW_EMPTY_VALUE_XCD As String = "ALLOW_EMPTY_VALUE_XCD"
        Public Const COL_ALLOW_EMPTY_VALUE_DESCRIPTION As String = "ALLOW_EMPTY_VALUE_DESCRIPTION"
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

        Public Function AddNewRowToEmptyDV() As TemplateParamsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(TemplateParamsDV.COL_OC_TEMPLATE_PARAMS_ID) = (New Guid()).ToByteArray
            row(TemplateParamsDV.COL_OC_TEMPLATE_ID) = Guid.Empty.ToByteArray
            row(TemplateParamsDV.COL_PARAM_NAME) = DBNull.Value
            row(TemplateParamsDV.COL_PARAM_VALUE_SOURCE_XCD) = DBNull.Value
            row(TemplateParamsDV.COL_PARAM_VALUE_SOURCE_DESCRIPTION) = DBNull.Value
            row(TemplateParamsDV.COL_PARAM_VALUE) = DBNull.Value
            row(TemplateParamsDV.COL_PARAM_TYPE) = DBNull.Value
            row(TemplateParamsDV.COL_PARAM_DATA_TYPE_XCD) = DBNull.Value
            row(TemplateParamsDV.COL_PARAM_DATA_TYPE_DESCRIPTION) = DBNull.Value
            row(TemplateParamsDV.COL_DATE_FORMAT_STRING) = DBNull.Value
            row(TemplateParamsDV.COL_ALLOW_EMPTY_VALUE_XCD) = DBNull.Value
            row(TemplateParamsDV.COL_CREATED_BY) = DBNull.Value
            row(TemplateParamsDV.COL_CREATED_DATE) = DBNull.Value
            row(TemplateParamsDV.COL_MODIFIED_BY) = DBNull.Value
            row(TemplateParamsDV.COL_MODIFIED_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New TemplateParamsDV(dt)
        End Function
    End Class
#End Region
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidDataType
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.BO_INVALID_DATA)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As OcTemplateParams = CType(objectToValidate, OcTemplateParams)
            Dim emailExpression As New System.Text.RegularExpressions.Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")

            If Not String.IsNullOrEmpty(obj.ParamDataTypeXcd) AndAlso Not String.IsNullOrEmpty(obj.ParamValue) Then
                Select Case obj.ParamDataTypeXcd
                    Case "OCTPT-GUID" ' GUID
                        Dim outputval_GUID As Guid = Guid.Empty
                        Return Guid.TryParse(obj.ParamValue, outputval_GUID)
                    Case "OCTPT-DATE" ' Date
                        Dim outputval_DATE As Date = Date.Today
                        Return Date.TryParse(obj.ParamValue, outputval_DATE)
                    Case "OCTPT-NUM" ' Number
                        Dim outputval_DECIMAL As Decimal
                        Return Decimal.TryParse(obj.ParamValue, outputval_DECIMAL)
                    Case "OCTPT-STR" ' String
                        Return True
                End Select
            End If

            Return True

        End Function
    End Class

#End Region

End Class


