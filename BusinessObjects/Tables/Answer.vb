﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/30/2012)  ********************

Public Class Answer
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
            Dim dal As New AnswerDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New AnswerDAL
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


    ''DEF-2285
    Public Sub Load(ByVal ds As DataSet, ByVal id As Guid)
        Try
            Dim dal As New AnswerDAL
            dal.Load(ds, id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region


#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        Me.Effective = Date.Now
        Me.Expiration = New Date(2499, 12, 31, 23, 59, 59)
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid Implements IExpirable.ID
        Get
            If Row(AnswerDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AnswerDAL.COL_NAME_ANSWER_ID), Byte()))
            End If
        End Get
    End Property

    Public Property QuestionId As Guid Implements IExpirable.parent_id
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_SOFT_QUESTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AnswerDAL.COL_NAME_SOFT_QUESTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_SOFT_QUESTION_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020), ValidateUniqueAnswerCode("")> _
    Public Property Code As String Implements IExpirable.Code
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AnswerDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AnswerDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidateUniqueAnswerOrder("")> _
    Public Property AnswerOrder As LongType
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_ANSWER_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(AnswerDAL.COL_NAME_ANSWER_ORDER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_ANSWER_ORDER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1020)> _
    Public Property AnswerValue As String
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_ANSWER_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AnswerDAL.COL_NAME_ANSWER_VALUE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_ANSWER_VALUE, Value)
        End Set
    End Property

    Public Property SupportsClaimId As Guid
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_SUPPORTS_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AnswerDAL.COL_NAME_SUPPORTS_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_SUPPORTS_CLAIM_ID, Value)
        End Set
    End Property

    Public Property Score As DecimalType
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_SCORE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AnswerDAL.COL_NAME_SCORE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_SCORE, Value)
        End Set
    End Property

    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE)> _
    Public Property Effective As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(AnswerDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)> _
    Public Property Expiration As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(AnswerDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    Public Property ListItemId As Guid
        Get
            CheckDeleted()
            If Row(AnswerDAL.COL_NAME_LIST_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AnswerDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AnswerDAL.COL_NAME_LIST_ITEM_ID, Value)
        End Set
    End Property

    Public Overrides ReadOnly Property IsNew As Boolean Implements IExpirable.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            Dim dal As New AnswerDAL
            'if the answer code is blank then get new answer code
            If Not Me.Row.RowState = DataRowState.Detached AndAlso Not Me.Row.RowState = DataRowState.Deleted Then
                If String.IsNullOrEmpty(Me.Code) Then
                    Me.Code = dal.GetNewAnswerCode()
                End If
            End If
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                dal.Update(Me.Row)
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

    Public Sub Copy(ByVal original As Answer)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    Public Function GetAnswerList(ByVal SoftQuestinoId As Guid) As DataView
        Try
            Dim ANSdal As AnswerDAL
            Return ANSdal.GetAnswerList(QuestionId).Tables(0).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function GetAnswerCodebyValue(ByVal AnswerValue As String) As String
        Try
            Dim ansDAL As New AnswerDAL
            Return ansDAL.GetAnswerCodeByValue(AnswerValue)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function GetAnswerDataByCode(ByVal AnswerCode As String) As DataSet
        Try
            Dim ansDAL As New AnswerDAL
            Return ansDAL.GetAnswerDataByCode(AnswerCode)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Answer List Selection View"
    Public Class AnswerList
        Inherits BusinessObjectListBase
        ReadOnly const_today As DateTime = DateTime.Now
        Public Sub New(ByVal parent As Question)
            MyBase.New(LoadTable(parent, DateTime.Now), GetType(Answer), parent)
        End Sub
        Public Sub New(ByVal parent As Question, ByVal ActiveOn As DateTime)
            MyBase.New(LoadTable(parent, ActiveOn), GetType(Answer), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, Answer).QuestionId.Equals(CType(Parent, Question).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As Question, ByVal ActiveOn As DateTime) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(AnswerList)) Then
                    Dim dal As New AnswerDAL
                    dal.LoadList(parent.Dataset, parent.Id, ActiveOn)
                    parent.AddChildrenCollection(GetType(AnswerList))
                End If
                Return parent.Dataset.Tables(AnswerDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region

#Region "Visitor"
    Public Function Accept(ByRef Visitor As IVisitor) As Boolean Implements IExpirable.Accept
        Try
            Return Visitor.Visit(Me)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Custom validations"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateUniqueAnswerCode
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.INVALID_CODE)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
            Dim obj As Answer = CType(context, Answer)
            For Each dtrow As DataRow In obj.Dataset.Tables(AnswerDAL.TABLE_NAME).Rows
                If dtrow.RowState <> DataRowState.Deleted OrElse dtrow.RowState <> DataRowState.Detached Then
                    Dim answerId As New Guid(CType(dtrow(AnswerDAL.TABLE_KEY_NAME), Byte()))
                    If answerId <> obj.Id Then
                        Dim ansCode As String = CType(dtrow(AnswerDAL.COL_NAME_CODE), String)
                        Dim expDate As DateTime = CType(dtrow(AnswerDAL.COL_NAME_EXPIRATION), DateTime)
                        If ansCode = obj.Code And expDate > DateTime.Now Then Return False
                    End If
                End If
            Next
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateUniqueAnswerOrder
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute

        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.INVALID_ORDER)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
            Dim obj As Answer = CType(context, Answer)
            For Each dtrow As DataRow In obj.Dataset.Tables(AnswerDAL.TABLE_NAME).Rows
                If dtrow.RowState <> DataRowState.Deleted OrElse dtrow.RowState <> DataRowState.Detached Then
                    Dim answerId As New Guid(CType(dtrow(AnswerDAL.TABLE_KEY_NAME), Byte()))
                    If answerId <> obj.Id Then
                        Dim ansOrder As Long = CType(dtrow(AnswerDAL.COL_NAME_ANSWER_ORDER), Long)
                        Dim expDate As DateTime = CType(dtrow(AnswerDAL.COL_NAME_EXPIRATION), DateTime)
                        If ansOrder = obj.AnswerOrder And expDate > DateTime.Now Then Return False
                    End If
                End If
            Next
            Return True
        End Function
    End Class
#End Region
End Class


