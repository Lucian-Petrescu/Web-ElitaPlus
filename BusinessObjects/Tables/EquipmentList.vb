Imports Assurant.Common.Localization

Public Class EquipmentList
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

    Protected Sub Load()
        Try
            Dim dal As New EquipmentListDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            SetValue(dal.COL_NAME_EFFECTIVE, EquipmentListDetail.GetCurrentDateTime())
            SetValue(dal.COL_NAME_EXPIRATION, EQUIPMENT_EXPIRATION_DEFAULT)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New EquipmentListDAL
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
    <ValueMandatory("")> _
    Public ReadOnly Property Id As Guid
        Get
            If Row(EquipmentListDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentListDAL.COL_NAME_EQUIPMENT_LIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=30), ValueMandatory(""), CheckListCodeDatesOverlaped("")> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentListDAL.COL_NAME_CODE)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=500)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentListDAL.COL_NAME_DESCRIPTION)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=500)> _
    Public Property Comments As String
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentListDAL.COL_NAME_COMMENTS)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Effective As DateType
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(EquipmentListDAL.COL_NAME_EFFECTIVE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Expiration As DateType
        Get
            CheckDeleted()
            If Row(EquipmentListDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(EquipmentListDAL.COL_NAME_EXPIRATION).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDAL.COL_NAME_EXPIRATION, Value)
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
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New EquipmentListDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
                dal.UpdateFamily(Dataset) 'New Code Added Manually
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

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(original As EquipmentList)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Detail List")
        End If

        CopyFrom(original)
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
            If detail.Expiration < DateHelper.GetDateValue(Effective.ToString()) Then
                Continue For
            End If

            Dim newDetail As EquipmentListDetail = BestEquipmentListChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.EquipmentListId = Id

            '' Set Effective date, Equipment list or Equipment, which ever is future date
            SelectedEquipmetEffective = EquipmentListDetail.GetEquipmentEffective(newDetail.EquipmentId)
            If SelectedEquipmetEffective < Effective Then
                newDetail.Effective = Effective
            Else
                newDetail.Effective = SelectedEquipmetEffective
            End If

            '' Set Expiration date, Equipment list or Equipment, which ever is earlier
            SelectedEquipmetExpiration = EquipmentListDetail.GetEquipmentExpiration(newDetail.EquipmentId)
            SelectedEquipmetListExpiration = Expiration
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

    Public Shared Function GetList(code As String, _
                                        description As String, effective As String, _
                                        expiration As String) As EquipmentList.EquipmentSearchDV
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

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property EquipmentListId(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EQUIPMENT_LIST_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Code(row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Description(row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Comments(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMENTS), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Effective(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EFFECTIVE), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EXPIRATION), Byte()))
            End Get
        End Property

    End Class

#End Region

#Region "Children Related"

    Public ReadOnly Property BestEquipmentListChildren As EquipmentListDetailList
        Get
            Return New EquipmentListDetailList(Me)
        End Get
    End Property

    Public Function GetChild(childId As Guid) As EquipmentListDetail
        Return CType(BestEquipmentListChildren.GetChild(childId), EquipmentListDetail)
    End Function

    Public Function GetNewChild() As EquipmentListDetail
        Dim newEquipmentListDetail As EquipmentListDetail = CType(BestEquipmentListChildren.GetNewChild, EquipmentListDetail)
        newEquipmentListDetail.EquipmentListId = Id
        newEquipmentListDetail.Effective = Effective
        newEquipmentListDetail.Expiration = Expiration
        Return newEquipmentListDetail
    End Function

#End Region

#Region "Public Methods"

    Public Shared Function CheckListCodeForOverlap(code As String, effective As DateType, _
                                        expiration As DateType, listId As Guid) As Boolean

        Try
            Dim dal As New EquipmentListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If code IsNot String.Empty Then
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


    Public Shared Function CheckListCodeDurationOverlap(code As String, effective As DateType, _
                                        expiration As DateType, listId As Guid) As Boolean

        Try
            Dim dal As New EquipmentListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If code IsNot String.Empty Then
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


    Public Shared Function ExpirePreviousList(code As String, effective As DateType, _
                                        expiration As DateType, listId As Guid) As Boolean

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

    Public Function GetSelectedEquipmentList(EquipmentListID As Guid) As DataView
        Dim eqListDal As New EquipmentListDAL
        Return eqListDal.GetSelectedEquipmentList(EquipmentListID).Tables(0).DefaultView

    End Function

    Public Shared Function ExpireList(expiration As DateType, listId As Guid) As Boolean

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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM004)
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM005)
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM005)
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
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

        If Code IsNot String.Empty And Effective IsNot Nothing Then
            Dim dv As EquipmentList.EquipmentSearchDV = New EquipmentList.EquipmentSearchDV(EquipDal.LoadList(Code, String.Empty, Effective, _
                   String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If Code IsNot Nothing And Effective IsNot Nothing Then
                For Each dr As DataRow In dv.Table.Rows
                    If ((dr(EquipmentListDAL.COL_NAME_CODE).ToString.ToUpper = Code.ToUpper) And _
                        (dr(EquipmentListDAL.COL_NAME_EFFECTIVE) = DateHelper.GetDateValue(Effective).ToString("dd-MMM-yyyy")) And _
                        Not DirectCast(dr(EquipmentListDAL.COL_NAME_EQUIPMENT_LIST_ID), Byte()).SequenceEqual(Id.ToByteArray)) Then
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

        If Code IsNot String.Empty And Description IsNot String.Empty And Effective IsNot Nothing And Nothing And Expiration IsNot Nothing Then
            Dim dv As EquipmentList.EquipmentSearchDV = New EquipmentList.EquipmentSearchDV(EquipDal.LoadList(Code, String.Empty, Effective, _
                   String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            For Each dr As DataRow In dv.Table.Rows
                If ((Not dr(DALBase.COL_NAME_CODE) = Code) And _
                    (Not dr(EquipmentDAL.COL_NAME_EFFECTIVE) >= Equals(Effective)) And _
                    (Not dr(EquipmentDAL.COL_NAME_EXPIRATION) <= Equals(Expiration))) Then
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

        If Code IsNot String.Empty Then
            If EquipDal.IsListToDealer(Code, Id).Tables(0).Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function CheckIfListIsAssignedToDealer(vCode As String, vId As Guid) As Boolean
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

    Public Shared Function CheckDuplicateEquipmentListCode(vCode As String, vEffective As String, vId As Guid) As Boolean
        Dim EquipDal As New EquipmentListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If vCode IsNot String.Empty And vEffective IsNot Nothing Then
            Dim dv As EquipmentList.EquipmentSearchDV = New EquipmentList.EquipmentSearchDV(EquipDal.LoadList(vCode, String.Empty, vEffective, _
                   String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If vCode IsNot Nothing And vEffective IsNot Nothing Then
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


