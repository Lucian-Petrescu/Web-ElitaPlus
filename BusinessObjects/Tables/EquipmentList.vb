﻿Imports Assurant.Common.Localization

Public Class EquipmentList
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

    Protected Sub Load()
        Try
            Dim dal As New EquipmentListDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            SetValue(dal.COL_NAME_EFFECTIVE, EquipmentListDetail.GetCurrentDateTime())
            SetValue(dal.COL_NAME_EXPIRATION, EQUIPMENT_EXPIRATION_DEFAULT)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New EquipmentListDAL
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
    <ValueMandatory("")> _
    Public ReadOnly Property Id() As Guid
        Get
            If Row(EquipmentListDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentListDAL.COL_NAME_EQUIPMENT_LIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=30), ValueMandatory(""), CheckListCodeDatesOverlaped("")> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentListDAL.COL_NAME_CODE)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(EquipmentListDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=500)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentListDAL.COL_NAME_DESCRIPTION)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(EquipmentListDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=500)> _
    Public Property Comments() As String
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentListDAL.COL_NAME_COMMENTS)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(EquipmentListDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(EquipmentListDAL.COL_NAME_EFFECTIVE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(EquipmentListDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(EquipmentListDAL.COL_NAME_EXPIRATION).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(EquipmentListDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property
#End Region

#Region "Constants"
    Private ReadOnly EQUIPMENT_EXPIRATION_DEFAULT As New DateTime(2499, 12, 31, 23, 59, 59)
    Friend Const EQUIPMENT_FORM004 As String = "EQUIPMENT_FORM004" ' Invalid List code since same effective
    Friend Const EQUIPMENT_FORM005 As String = "EQUIPMENT_FORM005" ' Equipment List Assigned To Dealer Cannt Be Deleted.
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New EquipmentListDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
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

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As EquipmentList)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Detail List")
        End If

        MyBase.CopyFrom(original)
        'copy the childrens 

        Dim SelectedEquipmetExpiration As DateTime
        Dim SelectedEquipmetListExpiration As DateTime
        Dim SelectedEquipmetEffective As DateTime
        Dim detail As EquipmentListDetail

        For Each detail In original.BestEquipmentListChildren
            '' Avoid expired equipments to be copied from the old list
            If detail.Expiration < EquipmentListDetail.GetCurrentDateTime() Then
                Continue For
            End If

            '' Check Equipment is valid and lies with in the new future lsit
            If detail.Expiration < DateHelper.GetDateValue(Me.Effective.ToString()) Then
                Continue For
            End If

            Dim newDetail As EquipmentListDetail = Me.BestEquipmentListChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.EquipmentListId = Me.Id

            '' Set Effective date, Equipment list or Equipment, which ever is future date
            SelectedEquipmetEffective = EquipmentListDetail.GetEquipmentEffective(newDetail.EquipmentId)
            If SelectedEquipmetEffective < Me.Effective Then
                newDetail.Effective = Me.Effective
            Else
                newDetail.Effective = SelectedEquipmetEffective
            End If

            '' Set Expiration date, Equipment list or Equipment, which ever is earlier
            SelectedEquipmetExpiration = EquipmentListDetail.GetEquipmentExpiration(newDetail.EquipmentId)
            SelectedEquipmetListExpiration = Me.Expiration
            If SelectedEquipmetExpiration < SelectedEquipmetListExpiration Then
                newDetail.Expiration = SelectedEquipmetExpiration
            Else
                newDetail.Expiration = SelectedEquipmetListExpiration
            End If

            newDetail.Effective = EquipmentListDetail.GetCurrentDateTime()
            newDetail.Save()

        Next
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal code As String, _
                                        ByVal description As String, ByVal effective As String, _
                                        ByVal expiration As String) As EquipmentList.EquipmentSearchDV
        Try
            Dim dal As New EquipmentListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If (description.Contains(DALBase.WILDCARD_CHAR) Or description.Contains(DALBase.ASTERISK)) Then
                description = description & DALBase.ASTERISK
            End If
            If (code.Contains(DALBase.WILDCARD_CHAR) Or code.Contains(DALBase.ASTERISK)) Then
                code = code & DALBase.ASTERISK
            End If

            Return New EquipmentSearchDV(dal.LoadList(code, description, effective, _
                expiration, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class EquipmentSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_EQUIPMENT_LIST_ID As String = EquipmentListDAL.COL_NAME_EQUIPMENT_LIST_ID
        Public Const COL_NAME_DESCRIPTION As String = EquipmentListDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = EquipmentListDAL.COL_NAME_CODE
        Public Const COL_NAME_COMMENTS As String = EquipmentListDAL.COL_NAME_COMMENTS
        Public Const COL_NAME_EFFECTIVE As String = EquipmentListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = EquipmentListDAL.COL_NAME_EXPIRATION
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property EquipmentListId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EQUIPMENT_LIST_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Comments(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMENTS), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Effective(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EFFECTIVE), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EXPIRATION), Byte()))
            End Get
        End Property

    End Class

#End Region

#Region "Children Related"

    Public ReadOnly Property BestEquipmentListChildren() As EquipmentListDetailList
        Get
            Return New EquipmentListDetailList(Me)
        End Get
    End Property

    Public Function GetChild(ByVal childId As Guid) As EquipmentListDetail
        Return CType(Me.BestEquipmentListChildren.GetChild(childId), EquipmentListDetail)
    End Function

    Public Function GetNewChild() As EquipmentListDetail
        Dim newEquipmentListDetail As EquipmentListDetail = CType(Me.BestEquipmentListChildren.GetNewChild, EquipmentListDetail)
        newEquipmentListDetail.EquipmentListId = Me.Id
        newEquipmentListDetail.Effective = Me.Effective
        newEquipmentListDetail.Expiration = Me.Expiration
        Return newEquipmentListDetail
    End Function

#End Region

#Region "Public Methods"

    Public Shared Function CheckListCodeForOverlap(ByVal code As String, ByVal effective As DateType, _
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New EquipmentListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If Not code Is String.Empty Then
                Dim ds As DataSet = dal.CheckOverlap(code, effective, _
                    expiration, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, listId)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        Return False
    End Function


    Public Shared Function CheckListCodeDurationOverlap(ByVal code As String, ByVal effective As DateType, _
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New EquipmentListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If Not code Is String.Empty Then
                Dim ds As DataSet = dal.CheckDurationOverlap(code, effective, _
                    expiration, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, listId)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        Return False
    End Function


    Public Shared Function ExpirePreviousList(ByVal code As String, ByVal effective As DateType, _
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New EquipmentListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Return dal.ExpireList(dal.CheckOverlapToExpire(code, effective, _
                expiration, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, listId), effective.ToString)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        Return False
    End Function

    Public Function GetSelectedEquipmentList(ByVal EquipmentListID As Guid) As DataView
        Dim eqListDal As New EquipmentListDAL
        Return eqListDal.GetSelectedEquipmentList(EquipmentListID).Tables(0).DefaultView

    End Function

    Public Shared Function ExpireList(ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New EquipmentListDAL
            Dim oCompanyGroupIds As ArrayList
            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim ds As DataSet = New DataSet()

            dal.LoadEQ(ds, listId)
            Return dal.ExpireList(ds, CType(expiration, DateTime).AddMinutes(1).ToString)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        Return False
    End Function

#End Region

#Region "Custom Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM004)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EquipmentList = CType(objectToValidate, EquipmentList)
            If (obj.CheckDuplicateEquipmentListCode()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckListAssignedToDealer
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM005)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EquipmentList = CType(objectToValidate, EquipmentList)
            If (obj.CheckIfListIsAssignedToDealer()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckListCodeDatesOverlaped
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM005)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EquipmentList = CType(objectToValidate, EquipmentList)
            If (obj.CheckListCodeDatesForOverlap()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Protected Function CheckDuplicateEquipmentListCode() As Boolean
        Dim EquipDal As New EquipmentListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty And Not Effective Is Nothing Then
            Dim dv As EquipmentList.EquipmentSearchDV = New EquipmentList.EquipmentSearchDV(EquipDal.LoadList(Code, String.Empty, Effective, _
                   String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If Not Me.Code Is Nothing And Not Me.Effective Is Nothing Then
                For Each dr As DataRow In dv.Table.Rows
                    If ((dr(EquipmentListDAL.COL_NAME_CODE).ToString.ToUpper = Me.Code.ToUpper) And _
                        (dr(EquipmentListDAL.COL_NAME_EFFECTIVE) = DateHelper.GetDateValue(Me.Effective).ToString("dd-MMM-yyyy")) And _
                        Not DirectCast(dr(EquipmentListDAL.COL_NAME_EQUIPMENT_LIST_ID), Byte()).SequenceEqual(Me.Id.ToByteArray)) Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function


    Protected Function CheckListCodeDatesForOverlap() As Boolean
        Dim EquipDal As New EquipmentListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty And Not Description Is String.Empty And Not Effective Is Nothing And Nothing And Not Expiration Is Nothing Then
            Dim dv As EquipmentList.EquipmentSearchDV = New EquipmentList.EquipmentSearchDV(EquipDal.LoadList(Code, String.Empty, Effective, _
                   String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            For Each dr As DataRow In dv.Table.Rows
                If ((Not dr(EquipmentDAL.COL_NAME_CODE) = Me.Code) And _
                    (Not dr(EquipmentDAL.COL_NAME_EFFECTIVE) >= Equals(Me.Effective)) And _
                    (Not dr(EquipmentDAL.COL_NAME_EXPIRATION) <= Equals(Me.Expiration))) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Protected Function CheckIfListIsAssignedToDealer() As Boolean
        Dim EquipDal As New EquipmentListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty Then
            If EquipDal.IsListToDealer(Code, Me.Id).Tables(0).Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function CheckIfListIsAssignedToDealer(ByVal vCode As String, ByVal vId As Guid) As Boolean
        Dim EquipDal As New EquipmentListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not String.IsNullOrEmpty(vCode) Then
            If EquipDal.IsListToDealer(vCode, vId).Tables(0).Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function CheckDuplicateEquipmentListCode(ByVal vCode As String, ByVal vEffective As String, ByVal vId As Guid) As Boolean
        Dim EquipDal As New EquipmentListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not vCode Is String.Empty And Not vEffective Is Nothing Then
            Dim dv As EquipmentList.EquipmentSearchDV = New EquipmentList.EquipmentSearchDV(EquipDal.LoadList(vCode, String.Empty, vEffective, _
                   String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If Not vCode Is Nothing And Not vEffective Is Nothing Then
                For Each dr As DataRow In dv.Table.Rows
                    If ((dr(EquipmentListDAL.COL_NAME_CODE).ToString.ToUpper = vCode.ToUpper) And _
                        (dr(EquipmentListDAL.COL_NAME_EFFECTIVE) = vEffective) And _
                        Not DirectCast(dr(EquipmentListDAL.COL_NAME_EQUIPMENT_LIST_ID), Byte()).SequenceEqual(vId.ToByteArray)) Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

#End Region

End Class


