'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/11/2004)  ********************
Imports Assurant.Common.Types
Public Class Question
    Inherits BusinessObjectBase
    Implements IExpirable

   

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
            Dim dal As New QuestionDAL
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
            Dim dal As New QuestionDAL
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

    'DEF-2846
    Private Const AnswerValue As String = "ANSWER_VALUE"


#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        Me.Effective = Date.Now
        Me.Expiration = New Date(2499, 12, 31, 23, 59, 59)
        Me.ParentId = Guid.Empty
        Me.ChildOrder = 0
    End Sub

    Private Const Question As String = "QUESTION"
    Private Const Answer As String = "ANSWER"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid Implements IExpirable.ID
        Get
            If Row(QuestionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(QuestionDAL.COL_NAME_SOFT_QUESTION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property SoftQuestionGroupId() As Guid
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_SOFT_QUESTION_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(QuestionDAL.COL_NAME_SOFT_QUESTION_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_SOFT_QUESTION_GROUP_ID, Value)
        End Set
    End Property



    Public Property ParentId() As Guid
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_PARENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(QuestionDAL.COL_NAME_PARENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_PARENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ChildOrder() As LongType
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_CHILD_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(QuestionDAL.COL_NAME_CHILD_ORDER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_CHILD_ORDER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(QuestionDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    Public ReadOnly Property TranslatedDescription() As String
        Get
            If Row(QuestionDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return LookupListNew.GetTranslatedQuestionFromCode(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Code)
            End If
        End Get
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=255), ValidateDuplicateCode("")> _
    Public Property Code() As String Implements IExpirable.Code
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(QuestionDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property QuestionTypeId() As Guid
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_QUESTION_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(QuestionDAL.COL_NAME_QUESTION_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_QUESTION_TYPE_ID, Value)
        End Set
    End Property
    <ValidateImpactsClaimId("")> _
    Public Property ImpactsClaimId() As Guid
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_IMPACTS_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(QuestionDAL.COL_NAME_IMPACTS_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_IMPACTS_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AnswerTypeId() As Guid
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_ANSWER_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(QuestionDAL.COL_NAME_ANSWER_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_ANSWER_TYPE_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2000)> _
    Public Property CustomerMessage() As String
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_CUSTOMER_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(QuestionDAL.COL_NAME_CUSTOMER_MESSAGE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_CUSTOMER_MESSAGE, Value)
        End Set
    End Property



    Public Property EntityAttributeId() As Guid
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_ENTITY_ATTRIBUTE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(QuestionDAL.COL_NAME_ENTITY_ATTRIBUTE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_ENTITY_ATTRIBUTE_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)> _
    Public Property SearchTags() As String
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_SEARCH_TAGS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(QuestionDAL.COL_NAME_SEARCH_TAGS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_SEARCH_TAGS, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE)> _
    Public Property Effective() As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Date.Now
            Else
                Return New DateTimeType(CType(Row(QuestionDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)> _
    Public Property Expiration() As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(QuestionDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(QuestionDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(QuestionDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property


    'Following is a dummy property just implemented to handle interface constraint
    Private Property parent_id As Guid Implements IExpirable.parent_id
        Get
            Return Guid.Empty
        End Get
        Set(ByVal value As Guid)
            'do nothing
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New QuestionDAL
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

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As Question)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Question")
        End If
        MyBase.CopyFrom(original)
        Me.Effective = Date.Now
        Me.Expiration = New Date(2499, 12, 31, 23, 59, 59)
        'copy the childrens        
        For Each detail As Answer In original.AnswerChildren
            Dim newDetail As Answer = Me.AnswerChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.QuestionId = Me.Id
            newDetail.Effective = Me.Effective
            newDetail.Expiration = Me.Expiration
            newDetail.Save()
        Next
    End Sub

    Public Function GetSoftQuestionGroup() As SoftQuestionGroup
        Try
            If Me.IsNew Then
                Return New SoftQuestionGroup(Me.Dataset)
            ElseIf Not Me.IsNew Then
                Return New SoftQuestionGroup(Me.SoftQuestionGroupId, Me.Dataset)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Overrides ReadOnly Property IsNew() As Boolean Implements IExpirable.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property

    Public Shared Function getQuestionList(ByVal code As String, ByVal description As String, ByVal questionTypeID As Guid, ByVal searchTags As String, ByVal issue As String, ByVal activeOn As String) As QuestionSearchDV
        'Purpose:                    Load QuestionList data into dataview
        'Author:                     Arnie Lugo
        'Date:                       03/14/2012
        'Modification History:       REQ-860
        Try
            Dim dal As New QuestionDAL

            Return New QuestionSearchDV(dal.LoadQuestionList(code, description, questionTypeID, searchTags, issue, activeOn, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function IsQuestionAssignedtoIssue() As Boolean
        Try
            Return (New QuestionDAL).IsQuestionAssignedtoIssue(Me.Id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)

        End Try
    End Function

#End Region

#Region "SoftQuestionDV"
    Public Class SoftQuestionDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_SOFT_QUESTION_ID As String = QuestionDAL.COL_NAME_SOFT_QUESTION_ID
        Public Const COL_NAME_SOFT_QUESTION_GROUP_ID As String = QuestionDAL.COL_NAME_SOFT_QUESTION_GROUP_ID
        Public Const COL_NAME_PARENT_ID As String = QuestionDAL.COL_NAME_PARENT_ID
        Public Const COL_NAME_CHILD_ORDER As String = QuestionDAL.COL_NAME_CHILD_ORDER
        Public Const COL_NAME_DESCRIPTION As String = QuestionDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CHILD_COUNT As String = "ChildrenCount"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class QuestionSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_SOFT_QUESTION_ID As String = QuestionDAL.COL_NAME_SOFT_QUESTION_ID
        Public Const COL_NAME_CODE As String = QuestionDAL.COL_NAME_CODE
        Public Const COL_NAME_DESCRIPTION As String = QuestionDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_QUESTION_TYPE As String = QuestionDAL.COL_NAME_QUESTION_TYPE   '"question_type"
        Public Const COL_NAME_SEARCH_TAGS As String = QuestionDAL.COL_NAME_SEARCH_TAGS
        Public Const COL_NAME_ISSUE As String = QuestionDAL.COL_NAME_ISSUE
        Public Const COL_NAME_EFFECTIVE As String = QuestionDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = QuestionDAL.COL_NAME_EXPIRATION

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property QuestionId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_SOFT_QUESTION_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_CODE), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property QuestionType(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_QUESTION_TYPE).ToString
            End Get
        End Property
        Public Shared ReadOnly Property Effective(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EFFECTIVE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EXPIRATION).ToString
            End Get
        End Property

    End Class

#End Region

#Region "Answers Selection View"
    <ValidateMinanswerRequired("")> _
    Public ReadOnly Property AnswerChildren() As Answer.AnswerList
        Get
            Dim Activeon As DateTime = DateTime.Now
            If Me.Expiration.Value < DateTime.Now Then
                Activeon = Me.Expiration.Value
            End If
            Return New Answer.AnswerList(Me, Activeon)
        End Get
    End Property


    Public Function GetAnswerSelectionView() As AnswerSelectionView
        Dim t As DataTable = AnswerSelectionView.CreateTable
        Dim detail As Answer
        Dim filteredTable As DataTable

        Try

            For Each detail In Me.AnswerChildren
                Dim row As DataRow = t.NewRow
                row(AnswerSelectionView.COL_NAME_ANSWER_ID) = detail.Id.ToByteArray
                row(AnswerSelectionView.COL_NAME_CODE) = detail.Code
                row(AnswerSelectionView.COL_NAME_ORDER) = detail.AnswerOrder
                row(AnswerSelectionView.COL_NAME_VALUE) = detail.AnswerValue
                row(AnswerSelectionView.COL_NAME_DESCRIPTION) = detail.Description
                row(AnswerSelectionView.COL_NAME_SUPPORTS_CLAIM) = LookupListNew.GetDescriptionFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), detail.SupportsClaimId)
                row(AnswerSelectionView.COL_NAME_SCORE) = detail.Score
                row(AnswerSelectionView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
                row(AnswerSelectionView.COL_NAME_EXPIRATION) = detail.Expiration.ToString
                row(AnswerSelectionView.COL_NAME_LIST_ITEM_ID) = detail.ListItemId.ToByteArray

                t.Rows.Add(row)

            Next

            ''DEF-2285
            Dim answerQuery = From filteredRow In t.AsEnumerable() Where CType(filteredRow.Field(Of String)("expiration"), DateType) > DateTime.Now Select filteredRow
            If answerQuery.Count > 0 Then
                filteredTable = answerQuery.CopyToDataTable()
                Return New AnswerSelectionView(filteredTable)
            Else
                Return New AnswerSelectionView(AnswerSelectionView.CreateTable)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Class AnswerSelectionView
        Inherits DataView
        Public Const COL_NAME_ANSWER_ID As String = AnswerDAL.COL_NAME_ANSWER_ID
        Public Const COL_NAME_ORDER As String = AnswerDAL.COL_NAME_ANSWER_ORDER
        Public Const COL_NAME_CODE As String = AnswerDAL.COL_NAME_CODE
        Public Const COL_NAME_VALUE As String = AnswerDAL.COL_NAME_ANSWER_VALUE
        Public Const COL_NAME_DESCRIPTION As String = AnswerDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_SUPPORTS_CLAIM As String = AnswerDAL.COL_NAME_SUPPORTS_CLAIM_ID
        Public Const COL_NAME_SCORE As String = AnswerDAL.COL_NAME_SCORE
        Public Const COL_NAME_EFFECTIVE As String = AnswerDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = AnswerDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_LIST_ITEM_ID As String = AnswerDAL.COL_NAME_LIST_ITEM_ID

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ANSWER_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ORDER, GetType(String))
            t.Columns.Add(COL_NAME_CODE, GetType(String))
            t.Columns.Add(COL_NAME_VALUE, GetType(String))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_SUPPORTS_CLAIM, GetType(String))
            t.Columns.Add(COL_NAME_SCORE, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            t.Columns.Add(COL_NAME_LIST_ITEM_ID, GetType(Byte()))
            Return t
        End Function
    End Class

    Public Function GetAnswerChild(ByVal childId As Guid) As Answer
        Return CType(Me.AnswerChildren.GetChild(childId), Answer)
    End Function

    Public Function GetNewAnswerChild() As Answer
        Dim newAnswer As Answer = CType(Me.AnswerChildren.GetNewChild, Answer)
        newAnswer.QuestionId = Me.Id
        Return newAnswer
    End Function

#End Region

#Region "Expiring Logic"

    Public Function ExpireOverLappingQuestions() As Boolean
        Try
            Dim overlap As New OverlapValidationVisitorDAL
            Dim ds As New DataSet
            ds = overlap.LoadList(Me.Id, Me.GetType.Name, Me.Code, Me.Effective, Me.Expiration, Guid.Empty)
            If ds.Tables(0).Rows.Count > 0 Then
                For Each dtrow As DataRow In ds.Tables(0).Rows
                    Dim qId As Guid = New Guid(CType(dtrow(QuestionDAL.COL_NAME_SOFT_QUESTION_ID), Byte()))
                    Dim ExpQuestion As New Question(qId, Me.Dataset)

                    If Me.Effective.Value < ExpQuestion.Expiration.Value Then
                        'Expire overlapping question 1 second before current question
                        ExpQuestion.Accept(New ExpirationVisitor(Me.Effective))
                    End If

                    'If ExpQuestion.IsDirty Then
                    '    ExpQuestion.Save()
                    'End If
                Next
                Return True         'expired successfully
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Visitor"
    ''' <summary>
    ''' Accept member of IElement interface
    ''' </summary>
    ''' <param name="Visitor"></param>
    ''' <returns>Returns True if Overlapping Records are found</returns>
    ''' <remarks></remarks>
    Public Function Accept(ByRef Visitor As IVisitor) As Boolean Implements IElement.Accept
        Try
            Return Visitor.Visit(Me)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateImpactsClaimId
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute
        Private _fieldDisplayName As String
        Public Const QTYP_ISSUE As String = "ISSUE"

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.IMPACTS_CLAIM_IS_REQUIRED)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
            Dim obj As Question = CType(context, Question)
            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim ImpactsClaimid As Guid = CType(objectToCheck, Guid)
            Dim QuestionTypeCode As String = LookupListNew.GetCodeFromId(LookupListNew.GetQuestionTypeLookupList(language_id), obj.QuestionTypeId)
            If QuestionTypeCode = QTYP_ISSUE Then
                If ImpactsClaimid = Guid.Empty Then Return False
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateMinanswerRequired
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute
        Private _fieldDisplayName As String

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.MIN_ANSWER_REQUIRED)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
            Dim obj As Question = CType(context, Question)
            'If there are no answers defined then validation fails
            If obj.AnswerChildren.Count < 1 Then
                Return False
            End If
            Return True
        End Function
    End Class

#End Region

#Region "Update Translation"
    Sub UpdateTranslation()
        Dim dropdownBO As New DropdownItem
        Dim retVal As Integer
        Dim DropdownId As Guid, listItemId As Guid

        DropdownId = QuestionList.GetDropdownId(Question)
        If Not DropdownId = Guid.Empty Then
            listItemId = (New QuestionDAL).GetListItembyCode(Me.Code.ToUpper, DropdownId)
            If listItemId = Guid.Empty Then
                retVal = dropdownBO.AddDropdownItem(Me.Code.ToUpper, Codes.YESNO_Y, Codes.YESNO_Y, DropdownId, Me.Description, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            Else
                retVal = dropdownBO.UpdateDropdownItem(listItemId, Me.Code.ToUpper, _
                         Codes.YESNO_Y, Codes.YESNO_Y, Me.Description, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            End If
        End If

        'answer grid list update
        DropdownId = QuestionList.GetDropdownId(Answer)
        For Each ans As Answer In Me.AnswerChildren
            If ans.Expiration.Value > DateTime.Now Then
                If Not DropdownId = Guid.Empty Then
                    listItemId = (New QuestionDAL).GetListItembyCode(ans.Code.ToUpper, DropdownId)
                    If listItemId = Guid.Empty Then
                        retVal = dropdownBO.AddDropdownItem(ans.Code.ToUpper, Codes.YESNO_Y, Codes.YESNO_Y, DropdownId, ans.Description, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                    Else
                        retVal = dropdownBO.UpdateDropdownItem(listItemId, Me.Code.ToUpper, _
                                 Codes.YESNO_Y, Codes.YESNO_Y, ans.Description, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                    End If
                End If
            End If
        Next

        'answer grid list update
        DropdownId = QuestionList.GetDropdownId(AnswerValue)
        For Each ansValue As Answer In Me.AnswerChildren
            If ansValue.Expiration.Value > DateTime.Now Then
                If Not DropdownId = Guid.Empty Then
                    listItemId = (New QuestionDAL).GetListItembyCode(ansValue.Code.ToUpper, DropdownId)
                    If listItemId = Guid.Empty Then
                        retVal = dropdownBO.AddDropdownItem(ansValue.Code.ToUpper, Codes.YESNO_Y, Codes.YESNO_Y, DropdownId, ansValue.AnswerValue, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                    Else
                        retVal = dropdownBO.UpdateDropdownItem(listItemId, Me.Code.ToUpper, Codes.YESNO_Y, Codes.YESNO_Y, ansValue.AnswerValue, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                    End If
                End If
            End If
        Next


    End Sub
#End Region

End Class


