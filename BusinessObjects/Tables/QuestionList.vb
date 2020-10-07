'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/27/2012)  ********************

Public Class QuestionList
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
            Dim dal As New QuestionListDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            SetValue(dal.COL_NAME_EFFECTIVE, QuestionList.GetCurrentDateTime())
            SetValue(dal.COL_NAME_EXPIRATION, QUESTION_EXPIRATION_DEFAULT)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New QuestionListDAL
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
            If row(QuestionListDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(QuestionListDAL.COL_NAME_QUESTION_LIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(QuestionListDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(QuestionListDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(QuestionListDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(QuestionListDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(QuestionListDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(QuestionListDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Effective As DateType
        Get
            CheckDeleted()
            If row(QuestionListDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(QuestionListDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(QuestionListDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Expiration As DateType
        Get
            CheckDeleted()
            If row(QuestionListDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(QuestionListDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(QuestionListDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property




#End Region

#Region "Constants"
    Private ReadOnly QUESTION_EXPIRATION_DEFAULT As New DateTime(2499, 12, 31, 23, 59, 59)
    Friend Const EQUIPMENT_FORM004 As String = "EQUIPMENT_FORM004" ' Invalid List code since same effective
    Friend Const EQUIPMENT_FORM005 As String = "EQUIPMENT_FORM005" ' Question List Assigned To Dealer Cannt Be Deleted.
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New QuestionListDAL
                'dal.Update(Me.Row)
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

    Public Sub Copy(ByVal original As QuestionList)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Detail List")
        End If
        MyBase.CopyFrom(original)
        'copy the childrens        
        Dim detail As IssueQuestionList
        For Each detail In original.BestQuestionListChildren
            Dim newDetail As IssueQuestionList = BestQuestionListChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.QuestionListId = Id
            newDetail.Effective = QuestionList.GetCurrentDateTime()
            newDetail.Save()
        Next
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal code As String, _
                                        ByVal description As String, ByVal activeOn As DateTimeType) As QuestionList.QuestionSearchDV

        Try
            Dim dal As New QuestionListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If (description.Contains(DALBase.WILDCARD_CHAR) Or description.Contains(DALBase.ASTERISK)) Then
                description = description & DALBase.ASTERISK
            End If
            If (code.Contains(DALBase.WILDCARD_CHAR) Or code.Contains(DALBase.ASTERISK)) Then
                code = code & DALBase.ASTERISK
            End If

            Return New QuestionSearchDV(dal.LoadList(code, description, activeOn, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class QuestionSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_QUESTION_LIST_ID As String = QuestionListDAL.COL_NAME_QUESTION_LIST_ID
        Public Const COL_NAME_DESCRIPTION As String = QuestionListDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = QuestionListDAL.COL_NAME_CODE
        Public Const COL_NAME_EFFECTIVE As String = QuestionListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = QuestionListDAL.COL_NAME_EXPIRATION
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property QuestionListId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_QUESTION_LIST_ID), Byte()))
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

    ''------------------

    Public ReadOnly Property BestQuestionListChildren As IssueQuestionListDetail
        Get
            Return New IssueQuestionListDetail(Me)
        End Get
    End Property

    Public ReadOnly Property BestDealerListChildren As DealerList
        Get
            Return New DealerList(Me)
        End Get
    End Property


    ''------------------

    Public Function GetChild(ByVal childId As Guid) As IssueQuestionList
        Return CType(BestQuestionListChildren.GetChild(childId), IssueQuestionList)
    End Function

    Public Function GetNewChild() As IssueQuestionList
        Dim newQuestionListDetail As IssueQuestionList = CType(BestQuestionListChildren.GetNewChild, IssueQuestionList)
        newQuestionListDetail.QuestionListId = Id
        newQuestionListDetail.Effective = Effective
        newQuestionListDetail.Expiration = Expiration
        Return newQuestionListDetail
    End Function

    Public Function GetDealerChild(ByVal childId As Guid) As Dealer
        Return CType(BestDealerListChildren.GetChild(childId), Dealer)
    End Function

    ''------------------

#End Region

#Region "Public Methods"

    Public Shared Function CheckListCodeForOverlap(ByVal code As String, ByVal effective As DateType, _
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New QuestionListDAL
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
            Dim dal As New QuestionListDAL
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
            Dim dal As New QuestionListDAL
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

    Public Function GetSelectedQuestionList(ByVal QuestionListID As Guid) As DataView
        Dim eqListDal As New IssueQuestionListDAL
        Return eqListDal.GetSelectedQuestionList(QuestionListID).Tables(0).DefaultView

    End Function

    Public Function GetSelectedDealertList(ByVal QuestionListCode As String) As DataView
        Dim eqListDal As New IssueQuestionListDAL
        Return eqListDal.GetSelectedDealerList(QuestionListCode).Tables(0).DefaultView

    End Function

    Public Shared Function GetCurrentDateTime() As DateTime
        Try
            Dim dal As New QuestionListDAL
            Return dal.GetCurrentDateTime()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
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
            Dim obj As QuestionList = CType(objectToValidate, QuestionList)
            If (obj.CheckDuplicateQuestionListCode()) Then
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
            Dim obj As QuestionList = CType(objectToValidate, QuestionList)
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
            Dim obj As QuestionList = CType(objectToValidate, QuestionList)
            If (obj.CheckListCodeDatesForOverlap()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Protected Function CheckDuplicateQuestionListCode() As Boolean
        Dim EquipDal As New QuestionListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty And Not Effective Is Nothing Then
            Dim dv As QuestionList.QuestionSearchDV = New QuestionList.QuestionSearchDV(EquipDal.LoadList(Code, String.Empty, Effective, _
                    oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If Not Code Is Nothing And Not Effective Is Nothing Then
                For Each dr As DataRow In dv.Table.Rows
                    If ((dr(QuestionListDAL.COL_NAME_CODE).ToString.ToUpper = Code.ToUpper) And _
                        (dr(QuestionListDAL.COL_NAME_EFFECTIVE) = Date.Parse(Effective).ToString("dd-MMM-yyyy")) And _
                        Not DirectCast(dr(QuestionListDAL.COL_NAME_QUESTION_LIST_ID), Byte()).SequenceEqual(Id.ToByteArray)) Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function


    Protected Function CheckListCodeDatesForOverlap() As Boolean
        Dim EquipDal As New QuestionListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty And Not Description Is String.Empty And Not Effective Is Nothing And Nothing And Not Expiration Is Nothing Then
            Dim dv As QuestionList.QuestionSearchDV = New QuestionList.QuestionSearchDV(EquipDal.LoadList(Code, String.Empty, Effective, _
                    oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            For Each dr As DataRow In dv.Table.Rows
                If ((Not dr(QuestionListDAL.COL_NAME_CODE) = Code) And _
                    (Not dr(QuestionListDAL.COL_NAME_EFFECTIVE) >= Equals(Effective)) And _
                    (Not dr(QuestionListDAL.COL_NAME_EXPIRATION) <= Equals(Expiration))) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Protected Function CheckIfListIsAssignedToDealer() As Boolean
        Dim EquipDal As New QuestionListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty Then
            If EquipDal.IsListToDealer(Code, Id).Tables(0).Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function CheckIfListIsAssignedToDealer(ByVal vCode As String, ByVal vId As Guid) As Boolean
        Dim EquipDal As New QuestionListDAL
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

    Public Shared Function CheckDuplicateQuestionListCode(ByVal vCode As String, ByVal vEffective As String, ByVal vId As Guid) As Boolean
        Dim EquipDal As New QuestionListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not vCode Is String.Empty And Not vEffective Is Nothing Then
            Dim dv As QuestionList.QuestionSearchDV = New QuestionList.QuestionSearchDV(EquipDal.LoadUniqueList(vCode, String.Empty, vEffective, _
                    oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If Not vCode Is Nothing And Not vEffective Is Nothing Then
                For Each dr As DataRow In dv.Table.Rows
                    If ((dr(QuestionListDAL.COL_NAME_CODE).ToString.ToUpper = vCode.ToUpper) And _
                        (dr(QuestionListDAL.COL_NAME_EFFECTIVE) = vEffective) And _
                        Not DirectCast(dr(QuestionListDAL.COL_NAME_QUESTION_LIST_ID), Byte()).SequenceEqual(vId.ToByteArray)) Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

    Public Shared Function GetDropdownId(ByVal listCode As String) As Guid
        Dim EquipDal As New QuestionListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        Return New Guid(EquipDal.GetDropdownId(listCode).ToString)

    End Function

    Public Shared Function GetDropdownItemId(ByVal DropdownId As Guid, ByVal itemCode As String) As Guid
        Dim EquipDal As New QuestionListDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        Return New Guid(EquipDal.GetDropdownItemId(DropdownId, itemCode).ToString)

    End Function

#End Region


End Class



