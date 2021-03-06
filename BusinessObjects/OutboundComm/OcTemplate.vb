﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/15/2017)  ********************

Imports Assurant.ElitaPlus.BusinessObjectsNew

Public Class OcTemplate
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
            Dim dal As New OcTemplateDAL
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
            Dim dal As New OcTemplateDAL
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

    Private Function CheckDuplicateCode() As Boolean
        Try
            Dim dal As New OcTemplateDAL
            Dim dataSet As DataSet = dal.GetCountOfTemplatesByCodeAndGroupExludingTemplateId(Me.TemplateCode, Me.Id, Me.OcTemplateGroupId)
            If Not dataSet Is Nothing AndAlso dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(OcTemplateDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcTemplateDAL.COL_NAME_OC_TEMPLATE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property OcTemplateGroupId() As Guid
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_OC_TEMPLATE_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcTemplateDAL.COL_NAME_OC_TEMPLATE_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_OC_TEMPLATE_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1000), CheckDuplicate("")>
    Public Property TemplateCode() As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_TEMPLATE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_TEMPLATE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_TEMPLATE_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1000)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)>
    Public Property HasCustomizedParamsXcd() As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_HAS_CUSTOMIZED_PARAMS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_HAS_CUSTOMIZED_PARAMS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_HAS_CUSTOMIZED_PARAMS_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)>
    Public Property AllowManualUseXcd() As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_USE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_USE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_USE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)>
    Public Property AllowManualResendXcd() As String
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_RESEND_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_RESEND_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_ALLOW_MANUAL_RESEND_XCD, Value)
        End Set
    End Property

    <ValidEffectiveDate("")>
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(OcTemplateDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(OcTemplateDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(OcTemplateDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property ParametersList() As OcTemplateParamsList
        Get
            Return New OcTemplateParamsList(Me)
        End Get
    End Property

    Public ReadOnly Property RecipientsList() As OcTemplateRecipientList
        Get
            Return New OcTemplateRecipientList(Me)
        End Get
    End Property

    <ValueMandatory(""),ValidStringLength("", Max:=100)> _
    Public Property TemplateTypeXcd() As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_TEMPLATE_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_TEMPLATE_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_TEMPLATE_TYPE_XCD, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=100), SMSREQUIRED("SmsAppKey", "SmsAppKey")> _
    Public Property SmsAppKey() As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_SMS_APP_KEY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_SMS_APP_KEY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_SMS_APP_KEY, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=100), SMSREQUIRED("SmsShortCode", "SmsShortCode")> _
    Public Property SmsShortCode() As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_SMS_SHORT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_SMS_SHORT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_SMS_SHORT_CODE, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=100), SMSREQUIRED("SmsTriggerId", "SmsTriggerId")> _
    Public Property SmsTriggerId() As String
        Get
            CheckDeleted()
            If row(OcTemplateDAL.COL_NAME_SMS_TRIGGER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateDAL.COL_NAME_SMS_TRIGGER_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcTemplateDAL.COL_NAME_SMS_TRIGGER_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso (Me.IsDirty OrElse Me.IsChildrenDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New OcTemplateDAL
                'dal.Update(Me.Row)
                MyBase.UpdateFamily(Me.Dataset)
                dal.UpdateFamily(Me.Dataset)
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

    Public Sub Copy(ByVal original As OcTemplate)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Template")
        End If
        'Copy myself
        MyBase.CopyFrom(original)
        
        ''copy the childrens     
        For Each detail As OcTemplateParams In original.ParametersList
            Dim newDetail As OcTemplateParams = Me.GetNewParameterChild()
            newDetail.CopyFrom(detail)
            newDetail.OcTemplateId = Me.Id
            newDetail.Save()
        Next

        For Each detail As OcTemplateRecipient In original.RecipientsList
            Dim newDetail As OcTemplateRecipient = Me.GetNewRecipientChild()
            newDetail.CopyFrom(detail)
            newDetail.OcTemplateId = Me.Id
            newDetail.Save()
        Next
    End Sub

    Public Function GetParameterChild(ByVal childId As Guid) As OcTemplateParams
        Return Me.ParametersList.Find(childId)
    End Function

    Public Function GetNewParameterChild() As OcTemplateParams
        Dim child As OcTemplateParams = Me.ParametersList.GetNewChild
        child.OcTemplateId = Me.Id
        Return child
    End Function

    Public Function RemoveParametersChild(ByVal childId As Guid)
        Me.ParametersList.Delete(childId)
    End Function

    Public Function GetRecipientChild(ByVal childId As Guid) As OcTemplateRecipient
        Return Me.RecipientsList.Find(childId)
    End Function

    Public Function GetNewRecipientChild() As OcTemplateRecipient
        Dim child As OcTemplateRecipient = Me.RecipientsList.GetNewChild
        child.OcTemplateId = Me.Id
        Return child
    End Function

    Public Function RemoveRecipientsChild(ByVal childId As Guid)
        Me.RecipientsList.Delete(childId)
    End Function
#End Region

#Region "Custom Validators"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_TEMPLATE_CODE As String = "DUPLICATE_TEMPLATE_CODE"

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_TEMPLATE_CODE)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
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

        Public Sub New(ByVal fieldDisplayName As String, ByVal propName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_TEMPLATE_CODE)
            _propName = propName.Trim.ToUpper
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Shared Function GetTemplateSearchDV(ByVal companyList As ArrayList,
                                        ByVal dealerId As Guid,
                                        ByVal templateGroupCodeMask As String,
                                        ByVal templateCodeMask As String) As TemplateSearchDV
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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As TemplateDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(TemplateDV.COL_TEMPLATE_ID) = (New Guid()).ToByteArray
            row(TemplateDV.COL_TEMPLATE_GROUP_ID) = Guid.Empty.ToByteArray
            row(TemplateDV.COL_TEMPLATE_CODE) = DBNull.Value
            row(TemplateDV.COL_DESCRIPTION) = DBNull.Value
            row(TemplateDV.COL_HAS_CUSTOMIZED_PARAMS_XCD) = DBNull.Value
            row(TemplateDV.COL_ALLOW_MANUAL_USE_XCD) = DBNull.Value
            row(TemplateDV.COL_ALLOW_MANUAL_RESEND_XCD) = DBNull.Value
            row(TemplateDV.COL_EFFECTIVE_DATE) = DBNull.Value
            row(TemplateDV.COL_EXPIRATION_DATE) = DBNull.Value
            row(TemplateDV.COL_CREATED_BY) = DBNull.Value
            row(TemplateDV.COL_CREATED_DATE) = DBNull.Value
            row(TemplateDV.COL_MODIFIED_BY) = DBNull.Value
            row(TemplateDV.COL_MODIFIED_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New TemplateDV(dt)
        End Function
    End Class
#End Region

    Public Shared Function GetAssociatedMessageCount(ByVal templateId As Guid) As Integer
        Try
            Dim dal As New OcTemplateDAL
            Dim dataSet As DataSet = dal.GetAssociatedMessageCount(templateId)
            If Not dataSet Is Nothing AndAlso dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
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

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, CountryTax.INVALID_EFFECTIVE_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
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


