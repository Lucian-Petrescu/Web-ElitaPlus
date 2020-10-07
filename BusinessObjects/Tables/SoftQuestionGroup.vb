'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/11/2004)  ********************

Public Class SoftQuestionGroup
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
        'Me.SetValue(SoftQuestionGroupDAL.COL_NAME_COMPANY_ID, ElitaPlusIdentity.Current.ActiveUser.CompanyId)
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
            Dim dal As New SoftQuestionGroupDAL
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
            Dim dal As New SoftQuestionGroupDAL
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
            LoadRiskType()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Private Sub LoadRiskType()
        GetSelectedRiskTypes(Id, Dataset)
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        SetValue(SoftQuestionGroupDAL.COL_NAME_COMPANY_GROUP_ID, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(SoftQuestionGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SoftQuestionGroupDAL.COL_NAME_SOFT_QUESTION_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    '<ValueMandatory("")> _
    'Public ReadOnly Property CompanyId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(SoftQuestionGroupDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(SoftQuestionGroupDAL.COL_NAME_COMPANY_ID), Byte()))
    '        End If
    '    End Get
        'Set(ByVal Value As Guid)
        '    CheckDeleted()
        '    Me.SetValue(SoftQuestionGroupDAL.COL_NAME_COMPANY_ID, Value)
        'End Set
    'End Property

    <ValueMandatory("")> _
    Public ReadOnly Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(SoftQuestionGroupDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SoftQuestionGroupDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(SoftQuestionGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SoftQuestionGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SoftQuestionGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"

    Public Overrides Sub Delete()
        CheckDeleted()

        'clear the softquestionsgroup id from the selected risktypes
        'use from Me.dataset
        'If Me.Dataset.Tables.IndexOf(RiskTypeDAL.RISK_TYPE_TABLE_NAME) = -1 Then
        '    'attach everything selected..
        '    Dim ds As Dataset = Me.GetSelectedRiskTypes(Me.Id)
        '    If ds.Tables.Count > 0 Then
        '        Dim dt As DataTable = New DataTable
        '        dt = ds.Tables(RiskTypeDAL.RISK_TYPE_TABLE_NAME).Copy
        '        ds.Tables.Remove(RiskTypeDAL.RISK_TYPE_TABLE_NAME)
        '        Me.Dataset.Tables.Add(dt)
        '    End If
        'End If

        If Dataset.Tables.IndexOf(RiskTypeDAL.RISK_TYPE_TABLE_NAME) <> -1 Then
            For Each row As DataRow In Dataset.Tables(RiskTypeDAL.RISK_TYPE_TABLE_NAME).Rows
                row(RiskTypeDAL.COL_NAME_SOFT_QUESTION_GROUP_ID) = DBNull.Value
            Next
        End If

        row.Delete()
    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsFamilyDirty()
        End Get
    End Property
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SoftQuestionGroupDAL
                dal.UpdateFamily(Dataset)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "Children Related"

    'METHODS ADDED MANUALLY. BEGIN

#Region "RiskType"


    'Public Sub UpdateRiskType(ByVal selectedRiskTypeGuidStrCollection As Hashtable)
    '    If selectedRiskTypeGuidStrCollection.Count > 0 Then

    '        'first Pass
    '        Dim bo As RiskType
    '        For Each bo In Me.RiskTypeChildren
    '            If Not selectedRiskTypeGuidStrCollection.Contains(bo.RiskTypeId.ToString) Then
    '                'remove the GUID entry
    '                bo.SoftQuestionGroupId = Guid.Empty
    '                bo.Save()
    '            End If
    '        Next
    '        'Second Pass
    '        Dim entry As DictionaryEntry
    '        For Each entry In selectedRiskTypeGuidStrCollection
    '            If Me.RiskTypeChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
    '                'update to new soft question GUID
    '                Dim newBO As RiskType = RiskTypeChildren.GetChild(New Guid(entry.Key.ToString))
    '                newBO.SoftQuestionGroupId = Me.Id
    '                newBO.Save()
    '            End If
    '        Next
    '    End If
    'End Sub

    Public Sub AttachRiskTypes(ByVal selectedRiskTypeGuidStrCollection As ArrayList)
        Dim riskTypeID As String
        For Each riskTypeID In selectedRiskTypeGuidStrCollection
            'update to new soft question GUID
            Dim newBO As RiskType = New RiskType(New Guid(riskTypeID), Dataset)
            If Not newBO Is Nothing Then
                newBO.SoftQuestionGroupId = Id
                newBO.Save()
            End If
        Next
    End Sub

    Public Sub DetachRiskTypes(ByVal selectedRiskTypeGuidStrCollection As ArrayList)
        Dim riskTypeID As String
        For Each riskTypeID In selectedRiskTypeGuidStrCollection
            'update to new soft question GUID
            Dim newBO As RiskType = New RiskType(New Guid(riskTypeID), Dataset)
            If Not newBO Is Nothing Then
                newBO.SoftQuestionGroupId = Guid.Empty
                newBO.Save()
            End If
        Next
    End Sub

    Public Shared Function GetAvailableRiskTypes(ByVal companyGroupId As Guid) As Dataset
        Dim ds As Dataset = New Dataset
        Dim rtDAL As RiskTypeDAL = New RiskTypeDAL
        rtDAL.LoadAvailableRiskTypeForSoftQuestion(ds, companyGroupId)
        Return ds
    End Function

    Public Shared Function GetSelectedRiskTypes(ByVal softQuestionGrpID As Guid, Optional ByVal ds As Dataset = Nothing) As Dataset
        If ds Is Nothing Then
            ds = New Dataset
        End If
        Dim rtDAL As RiskTypeDAL = New RiskTypeDAL
        rtDAL.LoadRiskTypeForSoftQuestion(ds, softQuestionGrpID)
        Return ds
    End Function



#End Region

    'METHODS ADDED MANUALLY. END


#End Region


#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal descriptionMask As String, ByVal companyGroupId As Guid) As SoftQuestionGroupDV
        Try
            Dim dal As New SoftQuestionGroupDAL

            Return New SoftQuestionGroupDV(dal.LoadList(descriptionMask, companyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "SoftQuestionGroupDV"
    Public Class SoftQuestionGroupDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_COMPANY_GROUP_ID As String = "company_group_id"
        Public Const COL_SOFT_QUESTION_GROUP_ID As String = "soft_question_group_id"
        Public Const COL_DESCRIPTION As String = "description"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#End Region

End Class


