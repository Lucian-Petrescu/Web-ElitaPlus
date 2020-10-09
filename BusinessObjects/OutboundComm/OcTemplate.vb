'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/15/2017)  ********************

Imports Assurant.ElitaPlus.BusinessObjectsNew

Public Class OcTemplate
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
            Dim dal As New OcTemplateDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New OcTemplateDAL
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

    Private Function CheckDuplicateCode() As Boolean
        Try
            Dim dal As New OcTemplateDAL
            Dim dataSet As DataSet = dal.GetCountOfTemplatesByCodeAndGroupExludingTemplateId(TemplateCode, Id, OcTemplateGroupId)
            If dataSet IsNot Nothing AndAlso dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
                If dataSet.Tables(0).Rows(0)(OcTemplateDAL.COL_NAME_NUMBER_OF_TEMPLATES) Is DBNull.Value Then
                    Return 0
                Else
                    Return CType(dataSet.Tables(0).Rows(0)(OcTemplateDAL.COL_NAME_NUMBER_OF_TEMPLATES), UInt64)
                End If
            Else
                Return 0
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Properties"
    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(OcTemplateDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcTemplateDAL.COL_NAME_OC_TEMPLATE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property OcTemplateGroupId As Guid
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_OC_TEMPLATE_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcTemplateDAL.COL_NAME_OC_TEMPLATE_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_OC_TEMPLATE_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1000), CheckDuplicate("")>
    Public Property TemplateCode As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_TEMPLATE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_TEMPLATE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_TEMPLATE_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1000)>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)>
    Public Property HasCustomizedParamsXcd As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_HAS_CUSTOMIZED_PARAMS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_HAS_CUSTOMIZED_PARAMS_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_HAS_CUSTOMIZED_PARAMS_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)>
    Public Property AllowManualUseXcd As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_USE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_USE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_USE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)>
    Public Property AllowManualResendXcd As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_RESEND_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_RESEND_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_RESEND_XCD, Value)
        End Set
    End Property

    <ValidEffectiveDate("")>
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(OcTemplateDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(OcTemplateDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property ParametersList As OcTemplateParamsList
        Get
            Return New OcTemplateParamsList(Me)
        End Get
    End Property

    Public ReadOnly Property RecipientsList As OcTemplateRecipientList
        Get
            Return New OcTemplateRecipientList(Me)
        End Get
    End Property

    <ValueMandatory(""),ValidStringLength("", Max:=100)> _
    Public Property TemplateTypeXcd As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_TEMPLATE_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_TEMPLATE_TYPE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_TEMPLATE_TYPE_XCD, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=100), SMSREQUIRED("SmsAppKey", "SmsAppKey")> _
    Public Property SmsAppKey As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_SMS_APP_KEY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_SMS_APP_KEY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_SMS_APP_KEY, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=100), SMSREQUIRED("SmsShortCode", "SmsShortCode")> _
    Public Property SmsShortCode As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_SMS_SHORT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_SMS_SHORT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_SMS_SHORT_CODE, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=100), SMSREQUIRED("SmsTriggerId", "SmsTriggerId")> _
    Public Property SmsTriggerId As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_SMS_TRIGGER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_SMS_TRIGGER_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateDAL.COL_NAME_SMS_TRIGGER_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso (IsDirty OrElse IsChildrenDirty) AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New OcTemplateDAL
                'dal.Update(Me.Row)
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

    Public Sub Copy(original As OcTemplate)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Template")
        End If
        'Copy myself
        CopyFrom(original)
        
        ''copy the childrens     
        For Each detail As OcTemplateParams In original.ParametersList
            Dim newDetail As OcTemplateParams = GetNewParameterChild()
            newDetail.CopyFrom(detail)
            newDetail.OcTemplateId = Id
            newDetail.Save()
        Next

        For Each detail As OcTemplateRecipient In original.RecipientsList
            Dim newDetail As OcTemplateRecipient = GetNewRecipientChild()
            newDetail.CopyFrom(detail)
            newDetail.OcTemplateId = Id
            newDetail.Save()
        Next
    End Sub

    Public Function GetParameterChild(childId As Guid) As OcTemplateParams
        Return ParametersList.Find(childId)
    End Function

    Public Function GetNewParameterChild() As OcTemplateParams
        Dim child As OcTemplateParams = ParametersList.GetNewChild
        child.OcTemplateId = Id
        Return child
    End Function

    Public Function RemoveParametersChild(childId As Guid)
        ParametersList.Delete(childId)
    End Function

    Public Function GetRecipientChild(childId As Guid) As OcTemplateRecipient
        Return RecipientsList.Find(childId)
    End Function

    Public Function GetNewRecipientChild() As OcTemplateRecipient
        Dim child As OcTemplateRecipient = RecipientsList.GetNewChild
        child.OcTemplateId = Id
        Return child
    End Function

    Public Function RemoveRecipientsChild(childId As Guid)
        RecipientsList.Delete(childId)
    End Function
#End Region

#Region "Custom Validators"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_TEMPLATE_CODE As String = "DUPLICATE_TEMPLATE_CODE"

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_TEMPLATE_CODE)
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As OcTemplate = CType(objectToValidate, OcTemplate)
            If (obj.CheckDuplicateCode()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class SMSREQUIRED
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_TEMPLATE_CODE As String = "GUI_FIELD_NUMBER_REQUIRED"
        Private _propName As String

        Public Sub New(fieldDisplayName As String, propName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_TEMPLATE_CODE)
            _propName = propName.Trim.ToUpper
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As OcTemplate = CType(objectToValidate, OcTemplate)
            If (obj.TemplateTypeXcd = "OC_TEMP_TYPE-SMS") Then 'Check required fields for SMS
                Select _propName
                    Case "SMSAPPKEY"
                        If string.IsNullOrEmpty(obj.SmsAppKey) Then
                            Return False
                        Else 
                            Return True
                        End If
                    Case "SMSSHORTCODE"
                        If string.IsNullOrEmpty(obj.SmsShortCode) Then
                            Return False
                        Else 
                            Return True
                        End If
                    Case "SMSTRIGGERID"
                        If string.IsNullOrEmpty(obj.SmsTriggerId) Then
                            Return False
                        Else 
                            Return True
                        End If
                End Select
            Else
                Return True
            End If
        End Function
    End Class
#End Region

#Region "DataView Retrieveing Methods"
#Region "TemplateSearchDV"
    Public Class TemplateSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_TEMPLATE_ID As String = "OC_TEMPLATE_ID"
        Public Const COL_TEMPLATE_CODE As String = "TEMPLATE_CODE"
        Public Const COL_TEMPLATE_DESCRIPTION As String = "TEMPLATE_DESCRIPTION"

        Public Const COL_TEMPLATE_GROUP_ID As String = "OC_TEMPLATE_GROUP_ID"
        Public Const COL_TEMPLATE_GROUP_CODE As String = "TEMPLATE_GROUP_CODE"
        Public Const COL_TEMPLATE_GROUP_DESCRIPTION As String = "TEMPLATE_GROUP_DESCRIPTION"
        Public Const COL_TEMPLATE_TYPE_XCD As String = "TEMPLATE_TYPE_XCD"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Shared Function GetTemplateSearchDV(companyList As ArrayList,
                                        dealerId As Guid,
                                        templateGroupCodeMask As String,
                                        templateCodeMask As String) As TemplateSearchDV
        Try
            Dim dal As New OcTemplateDAL
            Return New TemplateSearchDV(dal.LoadList(companyList, dealerId, templateGroupCodeMask, templateCodeMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "TemplateDV"
    Public Class TemplateDV
        Inherits DataView

#Region "Constants"
        Public Const COL_TEMPLATE_ID As String = "OC_TEMPLATE_ID"
        Public Const COL_TEMPLATE_GROUP_ID As String = "OC_TEMPLATE_GROUP_ID"
        Public Const COL_TEMPLATE_CODE As String = "TEMPLATE_CODE"
        Public Const COL_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_HAS_CUSTOMIZED_PARAMS_XCD As String = "HAS_CUSTOMIZED_PARAMS_XCD"
        Public Const COL_ALLOW_MANUAL_USE_XCD As String = "ALLOW_MANUAL_USE_XCD"
        Public Const COL_ALLOW_MANUAL_RESEND_XCD As String = "ALLOW_MANUAL_RESEND_XCD"
        Public Const COL_EFFECTIVE_DATE As String = "EFFECTIVE_DATE"
        Public Const COL_EXPIRATION_DATE As String = "EXPIRATION_DATE"
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

        Public Function AddNewRowToEmptyDV() As TemplateDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(COL_TEMPLATE_ID) = (New Guid()).ToByteArray
            row(COL_TEMPLATE_GROUP_ID) = Guid.Empty.ToByteArray
            row(COL_TEMPLATE_CODE) = DBNull.Value
            row(COL_DESCRIPTION) = DBNull.Value
            row(COL_HAS_CUSTOMIZED_PARAMS_XCD) = DBNull.Value
            row(COL_ALLOW_MANUAL_USE_XCD) = DBNull.Value
            row(COL_ALLOW_MANUAL_RESEND_XCD) = DBNull.Value
            row(COL_EFFECTIVE_DATE) = DBNull.Value
            row(COL_EXPIRATION_DATE) = DBNull.Value
            row(COL_CREATED_BY) = DBNull.Value
            row(COL_CREATED_DATE) = DBNull.Value
            row(COL_MODIFIED_BY) = DBNull.Value
            row(COL_MODIFIED_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New TemplateDV(dt)
        End Function
    End Class
#End Region

    Public Shared Function GetAssociatedMessageCount(templateId As Guid) As Integer
        Try
            Dim dal As New OcTemplateDAL
            Dim dataSet As DataSet = dal.GetAssociatedMessageCount(templateId)
            If dataSet IsNot Nothing AndAlso dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
                If dataSet.Tables(0).Rows(0)(OcTemplateDAL.COL_NAME_NUMBER_OF_MESSAGES) Is DBNull.Value Then
                    Return 0
                Else
                    Return CType(dataSet.Tables(0).Rows(0)(OcTemplateDAL.COL_NAME_NUMBER_OF_MESSAGES), UInt64)
                End If
            Else
                Return 0
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, CountryTax.INVALID_EFFECTIVE_DATE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As OcTemplate = CType(objectToValidate, OcTemplate)

            If Not obj.IsDeleted AndAlso Not IsNothing(obj.EffectiveDate) AndAlso Not IsNothing(obj.ExpirationDate) Then 'Edit or add new
                If (obj.EffectiveDate.Value >= obj.ExpirationDate.Value) Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

End Class


