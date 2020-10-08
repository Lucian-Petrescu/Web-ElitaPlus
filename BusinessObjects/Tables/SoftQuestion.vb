'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/11/2004)  ********************

Public Class SoftQuestion
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
            Dim dal As New SoftQuestionDAL
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
            Dim dal As New SoftQuestionDAL
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
            If row(SoftQuestionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SoftQuestionDAL.COL_NAME_SOFT_QUESTION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property SoftQuestionGroupId As Guid
        Get
            CheckDeleted()
            If row(SoftQuestionDAL.COL_NAME_SOFT_QUESTION_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SoftQuestionDAL.COL_NAME_SOFT_QUESTION_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SoftQuestionDAL.COL_NAME_SOFT_QUESTION_GROUP_ID, Value)
        End Set
    End Property



    Public Property ParentId As Guid
        Get
            CheckDeleted()
            If row(SoftQuestionDAL.COL_NAME_PARENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SoftQuestionDAL.COL_NAME_PARENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SoftQuestionDAL.COL_NAME_PARENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ChildOrder As LongType
        Get
            CheckDeleted()
            If row(SoftQuestionDAL.COL_NAME_CHILD_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(SoftQuestionDAL.COL_NAME_CHILD_ORDER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SoftQuestionDAL.COL_NAME_CHILD_ORDER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(SoftQuestionDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SoftQuestionDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SoftQuestionDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SoftQuestionDAL
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

#Region "SoftQuestionDV"
    Public Class SoftQuestionDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_SOFT_QUESTION_ID As String = "soft_question_id"
        Public Const COL_NAME_SOFT_QUESTION_GROUP_ID As String = "soft_question_group_id"
        Public Const COL_NAME_PARENT_ID As String = "parent_id"
        Public Const COL_NAME_CHILD_ORDER As String = "child_order"
        Public Const COL_NAME_DESCRIPTION As String = "description"
        Public Const COL_NAME_CHILD_COUNT As String = "ChildrenCount"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(softQuestionGrpId As Guid, companyGroupId As Guid) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadList(softQuestionGrpId, companyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getSoftQuestionGroups(companyGroupId As Guid) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadSoftQuestionGroups(companyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getSoftQuestionGroupForRiskType(companyGroupId As Guid, riskTypeId As Guid) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadSoftQuestionGroupForRiskType(companyGroupId, riskTypeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getChildren(parentID As Guid) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadChildren(parentID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getMaxChildOrder(parentID As Guid) As Long
        Try
            Dim dal As New SoftQuestionDAL
            Return dal.GetMaxChildOrder(parentID)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getSoftQuestionId(parentID As Guid, ChildOrder As Long) As Guid
        Try
            Dim dal As New SoftQuestionDAL
            Return dal.GetSoftQuestionId(parentID, ChildOrder)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getAvailableSoftQuestionGroup(companyGroupId As Guid) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadAvailableSoftQuestionGroups(companyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    '<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>
    Public Shared Function getReOrderSoftQuestionGroup(companyGroupId As Guid) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadReOrderGroup(companyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getReOrderSoftQuestion(SOFT_QUESTION_GROUP_ID As String) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadReOrderQuestion(SOFT_QUESTION_GROUP_ID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getReOrderSoftQuestion2(SOFT_QUESTION_ID As String) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadReOrderQuestion2(SOFT_QUESTION_ID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getLastRow(soft_question_group_id As String, PARENT_ID As String, CHILD_ORDER As String, DESCRIPTION As String) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadLastRow(soft_question_group_id, PARENT_ID, CHILD_ORDER, DESCRIPTION).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getReOrderSoftQuestion3(soft_question_group_id As String) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadReOrderQuestion3(soft_question_group_id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getNullParentQuestion(SOFT_QUESTION_ID As String) As SoftQuestionDV
        Try
            Dim dal As New SoftQuestionDAL

            Return New SoftQuestionDV(dal.LoadNullParentQuestion(SOFT_QUESTION_ID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function InsertReOrderSoftQuestion(soft_question_group_id As String, parent_id As String, child_order As Integer, description As String)
        Try
            Dim dal As New SoftQuestionDAL

            dal.InsertSoftQuestions(soft_question_group_id, parent_id, child_order, description)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function DeleteReOrderSoftQuestion(SOFT_QUESTION_GROUP_ID As String)
        Try
            Dim dal As New SoftQuestionDAL

            dal.DeleteOrderQuestion(SOFT_QUESTION_GROUP_ID)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    '<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>
#End Region

End Class


