'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/10/2007)  ********************

Public Class CoverageByCompanyGroup
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
            Dim dal As New CoverageByCompanyGroupDAL
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
            Dim dal As New CoverageByCompanyGroupDAL
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

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CoverageByCompanyGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageByCompanyGroupDAL.COL_NAME_COVERAGE_BY_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageByCompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageByCompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageByCompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageByCompanyGroupDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageByCompanyGroupDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageByCompanyGroupDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageByCompanyGroupDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "Children Related"

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal compGrpId As Guid) As SearchDV
        Try
            Dim dal As New CoverageByCompanyGroupDAL
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New SearchDV(dal.LoadList(oLanguageId, compGrpId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Private Shared Function GetCovCompGrpList(ByVal parent As CompanyGroup) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(CovCompGrpList)) Then
                Dim dal As New CoverageByCompanyGroupDAL
                dal.LoadList(parent.Id, parent.Dataset)
                parent.AddChildrenCollection(GetType(CovCompGrpList))
            End If
            'Return New CovLossList(parent)
            Return parent.Dataset.Tables(CoverageByCompanyGroupDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Shared Function GetUsedCompanyGroupDv() As DataView
        Dim dal As New CoverageByCompanyGroupDAL
        Dim ds As Dataset

        ds = dal.GetUsedCompanyGroups()
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Shared Function GetUsedCompanyGroup() As ArrayList
        Dim oUsedCompGrpDv As DataView = GetUsedCompanyGroupDv()
        Dim oUsedCompGrpArr = New ArrayList

        If oUsedCompGrpDv.Table.Rows.Count > 0 Then
            Dim index As Integer

            ' Create Array
            For index = 0 To oUsedCompGrpDv.Table.Rows.Count - 1
                If Not oUsedCompGrpDv.Table.Rows(index)(CoverageByCompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID) Is System.DBNull.Value Then
                    oUsedCompGrpArr.Add(New Guid(CType(oUsedCompGrpDv.Table.Rows(index)(CoverageByCompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID), Byte())))
                End If
            Next
        End If

        Return oUsedCompGrpArr
    End Function
#End Region

#Region "SearchDV"

    Public Class SearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_DESCRIPTION As String = "description"
        Public Const COL_NAME_CODE As String = "code"
        Public Const COL_NAME_COVERAGE_BY_COMPANY_GROUP_ID As String = CoverageByCompanyGroupDAL.COL_NAME_COVERAGE_BY_COMPANY_GROUP_ID
        Public Const COL_NAME_COMPANY_GROUP_ID As String = CoverageByCompanyGroupDAL.COL_NAME_COMPANY_GROUP_ID
        Public Const COL_NAME_COVERAGE_TYPE_ID As String = CoverageByCompanyGroupDAL.COL_NAME_COVERAGE_TYPE_ID
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CoverageByCompanyGroup_id(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COVERAGE_BY_COMPANY_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CompanyGroupGd(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMPANY_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CoverageTypeId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End Get
        End Property

    End Class

#End Region
#Region "List Methods"
    Public Class CovCompGrpList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As CompanyGroup)
            MyBase.New(GetCovCompGrpList(parent), GetType(CoverageByCompanyGroup), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(ByVal covTypeId As Guid) As CoverageByCompanyGroup
            Dim bo As CoverageByCompanyGroup
            For Each bo In Me
                If bo.CoverageTypeId.Equals(covTypeId) Then Return bo
            Next
            Return Nothing
        End Function
    End Class

#End Region
End Class



