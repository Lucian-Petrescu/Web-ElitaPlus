'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/24/2008)  ********************

Public Class ClaimStatusLetter
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimStatusLetterDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ClaimStatusLetterDAL
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

    Public Shared Function getList(ByVal dealerId As Guid, ByVal claimStatusByGroupId As Guid) As ClaimStatusLetterSearchDV
        Try
            Dim dal As New ClaimStatusLetterDAL
            Return New ClaimStatusLetterSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, _
                                                            dealerId, claimStatusByGroupId, _
                                                            ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, _
                                                            ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

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
            If row(ClaimStatusLetterDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusLetterDAL.COL_NAME_STATUS_LETTER_ID), Byte()))
            End If
        End Get
    End Property

    <ValidateClaimStatusByGroup("")> _
    Public Property ClaimStatusByGroupId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusLetterDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusLetterDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusLetterDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property LetterType As String
        Get
            CheckDeleted()
            If row(ClaimStatusLetterDAL.COL_NAME_LETTER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimStatusLetterDAL.COL_NAME_LETTER_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_LETTER_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=999999, Min:=0)> _
    Public Property NumberOfDays As LongType
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_NUMBER_OF_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimStatusLetterDAL.COL_NAME_NUMBER_OF_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_NUMBER_OF_DAYS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property EmailSubject As String
        Get
            CheckDeleted()
            If row(ClaimStatusLetterDAL.COL_NAME_EMAIL_SUBJECT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimStatusLetterDAL.COL_NAME_EMAIL_SUBJECT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_EMAIL_SUBJECT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property EmailText As String
        Get
            CheckDeleted()
            If row(ClaimStatusLetterDAL.COL_NAME_EMAIL_TEXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimStatusLetterDAL.COL_NAME_EMAIL_TEXT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_EMAIL_TEXT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100), EmailAddressFrom("")> _
    Public Property EmailFrom As String
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_EMAIL_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusLetterDAL.COL_NAME_EMAIL_FROM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_EMAIL_FROM, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UseServiceCenterEmail As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_USE_SERVICE_CENTER_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusLetterDAL.COL_NAME_USE_SERVICE_CENTER_EMAIL), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_USE_SERVICE_CENTER_EMAIL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000), ValidEmailTo(""), EmailAddressTo("")> _
    Public Property EmailTo As String
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_EMAIL_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusLetterDAL.COL_NAME_EMAIL_TO), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_EMAIL_TO, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property IsActive As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusLetterDAL.COL_NAME_IS_ACTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusLetterDAL.COL_NAME_IS_ACTIVE), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_IS_ACTIVE, Value)
        End Set
    End Property

    <ValidateNotificationType("")> _
    Public Property NotificationTypeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_NOTIFICATION_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusLetterDAL.COL_NAME_NOTIFICATION_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_NOTIFICATION_TYPE_ID, Value)
        End Set
    End Property

    Public Property UseClaimStatus As String
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_USE_CLAIM_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusLetterDAL.COL_NAME_USE_CLAIM_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_USE_CLAIM_STATUS, Value)
        End Set

    End Property
    <ValidateGroupOwner("")> _
    Public Property GroupOwnerId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusLetterDAL.COL_NAME_GROUP_OWNER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusLetterDAL.COL_NAME_GROUP_OWNER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusLetterDAL.COL_NAME_GROUP_OWNER_ID, Value)
        End Set
    End Property
#End Region

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class EmailAddressFrom
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusLetter = CType(objectToValidate, ClaimStatusLetter)

            If obj.EmailFrom Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.EmailFrom)

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class EmailAddressTo
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusLetter = CType(objectToValidate, ClaimStatusLetter)
            Dim bValid As Boolean = True

            If obj.EmailTo Is Nothing Or obj.EmailTo = "" Then
                bValid = True
            Else
                Dim arrEmail As String() = obj.EmailTo.Split(New Char() {","})
                Dim email As String
                For Each email In arrEmail
                    If Not (email Is Nothing Or email = "") Then
                        bValid = MiscUtil.EmailAddressValidation(email)
                        If bValid = False Then
                            Exit For
                        End If
                    End If
                Next
            End If

            Return bValid
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidEmailTo
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "RECIPIENT_REQUIRED")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusLetter = CType(objectToValidate, ClaimStatusLetter)
            Dim yesOrNo As String = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), obj.UseServiceCenterEmail)
            Dim bValid As Boolean = True

            If obj.UseServiceCenterEmail.Equals(Guid.Empty) Then
                bValid = False
            ElseIf yesOrNo = "N" AndAlso (obj.EmailTo Is Nothing Or obj.EmailTo = "") Then
                bValid = False
                Message = "RECIPIENT_REQUIRED"
            ElseIf yesOrNo = "Y" AndAlso Not obj.EmailTo Is Nothing And obj.EmailTo <> "" Then
                bValid = False
                Message = "RECIPIENT_ONLY_REQUIRED_WHEN_SERVICE_CENTER_RECIPIENT_NO"
            End If

            Return bValid

        End Function
    End Class


#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimStatusLetterDAL
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
    'Public Shared Sub AddNewRowToClaimStatusLetterSearchDV(ByRef dv As ClaimStatusLetterSearchDV, ByVal NewClaimStatusLetterBO As ClaimStatusLetter)
    '    Dim dt As DataTable, blnEmptyTbl As Boolean = False

    '    If NewClaimStatusLetterBO.IsNew Then
    '        Dim row As DataRow
    '        If dv Is Nothing Then
    '            Dim guidTemp As New Guid
    '            blnEmptyTbl = True
    '            dt = New DataTable
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_STATUS_LETTER_ID, guidTemp.ToByteArray.GetType)
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_CLAIM_STATUS_BY_GROUP_ID, guidTemp.ToByteArray.GetType)
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_LETTER_TYPE, GetType(String))
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_NUMBER_OF_DAYS, GetType(String))
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_EMAIL_SUBJECT, GetType(String))
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_EMAIL_TEXT, GetType(String))
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_EMAIL_FROM, GetType(String))
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_USE_SERVICE_CENTER_EMAIL, guidTemp.ToByteArray.GetType)
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_EMAIL_TO, GetType(String))
    '            dt.Columns.Add(ClaimStatusLetterSearchDV.COL_IS_ACTIVE, GetType(String))
    '        Else
    '            dt = dv.Table
    '        End If
    '        row = dt.NewRow
    '        row(ClaimStatusLetterSearchDV.COL_STATUS_LETTER_ID) = NewClaimStatusLetterBO.Id.ToByteArray
    '        row(ClaimStatusLetterSearchDV.COL_CLAIM_STATUS_BY_GROUP_ID) = NewClaimStatusLetterBO.ClaimStatusByGroupId.ToByteArray
    '        row(ClaimStatusLetterSearchDV.COL_DEALER_ID) = NewClaimStatusLetterBO.DealerId.ToByteArray
    '        row(ClaimStatusLetterSearchDV.COL_LETTER_TYPE) = NewClaimStatusLetterBO.LetterType
    '        row(ClaimStatusLetterSearchDV.COL_NUMBER_OF_DAYS) = NewClaimStatusLetterBO.NumberOfDays
    '        row(ClaimStatusLetterSearchDV.COL_EMAIL_SUBJECT) = NewClaimStatusLetterBO.EmailSubject
    '        row(ClaimStatusLetterSearchDV.COL_EMAIL_TEXT) = NewClaimStatusLetterBO.EmailText
    '        row(ClaimStatusLetterSearchDV.COL_EMAIL_FROM) = NewClaimStatusLetterBO.EmailFrom
    '        row(ClaimStatusLetterSearchDV.COL_USE_SERVICE_CENTER_EMAIL) = NewClaimStatusLetterBO.UseServiceCenterEmail.ToByteArray
    '        row(ClaimStatusLetterSearchDV.COL_EMAIL_TO) = NewClaimStatusLetterBO.EmailTo
    '        row(ClaimStatusLetterSearchDV.COL_IS_ACTIVE) = NewClaimStatusLetterBO.IsActive
    '        dt.Rows.Add(row)
    '        If blnEmptyTbl Then dv = New ClaimStatusLetterSearchDV(dt)
    '    End If
    'End Sub

#Region "ClaimStatusLetterSearchDV"
    Public Class ClaimStatusLetterSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_STATUS_LETTER_ID As String = "status_letter_id"
        Public Const COL_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_LETTER_TYPE As String = "letter_type"
        Public Const COL_NUMBER_OF_DAYS As String = "number_of_days"
        Public Const COL_EMAIL_SUBJECT As String = "email_subject"
        Public Const COL_EMAIL_TEXT As String = "email_text"
        Public Const COL_EMAIL_FROM As String = "email_from"
        Public Const COL_USE_SERVICE_CENTER_EMAIL As String = "use_service_center_email"
        Public Const COL_EMAIL_TO As String = "email_to"
        Public Const COL_IS_ACTIVE As String = "is_active"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_STATUS_DESCRIPTION As String = "status_description"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ClaimStatusLetterSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(ClaimStatusLetterSearchDV.COL_STATUS_LETTER_ID) = (New Guid()).ToByteArray
            dt.Rows.Add(row)
            Return New ClaimStatusLetterSearchDV(dt)
        End Function

    End Class

#End Region

#End Region
#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateGroupOwner
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusLetter = CType(objectToValidate, ClaimStatusLetter)

            If obj.UseClaimStatus = "N" Then
                Dim mandatAttr As New ValueMandatoryAttribute(DisplayName)
                Return mandatAttr.IsValid(valueToCheck, objectToValidate)
            End If

            Return True

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateClaimStatusByGroup
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusLetter = CType(objectToValidate, ClaimStatusLetter)

            If obj.UseClaimStatus = "Y" Then
                Dim mandatAttr As New ValueMandatoryAttribute(DisplayName)
                Return mandatAttr.IsValid(valueToCheck, objectToValidate)
            End If

            Return True

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateNotificationType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusLetter = CType(objectToValidate, ClaimStatusLetter)

            If obj.UseClaimStatus = "N" Then
                Dim mandatAttr As New ValueMandatoryAttribute(DisplayName)
                Return mandatAttr.IsValid(valueToCheck, objectToValidate)
            End If

            Return True

        End Function

    End Class
#End Region
End Class


