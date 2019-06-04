Imports System.Collections.Generic

Public Class BestReplacementGroup
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
            Dim dal As New BestReplacementGroupDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            SetValue(dal.COL_NAME_COMPANY_GROUP_ID, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New BestReplacementGroupDAL
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
            If Row(BestReplacementGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementGroupDAL.TABLE_KEY_NAME), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(BestReplacementGroupDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementGroupDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BestReplacementGroupDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(BestReplacementGroupDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementGroupDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BestReplacementGroupDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=30)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(BestReplacementGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BestReplacementGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BestReplacementGroupDAL
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

    Public Sub Copy(ByVal original As BestReplacementGroup)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement")
        End If
        MyBase.CopyFrom(original)
        'copy the childrens        
        Dim detail As BestReplacement
        For Each detail In original.BestReplacementChildren
            Dim newDetail As BestReplacement = Me.BestReplacementChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.MigrationPathId = Me.Id
            newDetail.Save()
        Next
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal searchCode As String, ByVal searchDesc As String) As BestReplacementGroup.BestReplacementGroupSearchDV
        Try
            Dim dal As New BestReplacementGroupDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Return New BestReplacementGroupSearchDV(dal.LoadList(oCompanyGroupIds, searchCode, searchDesc).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class BestReplacementGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CODE As String = BestReplacementGroupDAL.COL_NAME_CODE
        Public Const COL_NAME_DESCRIPTION As String = BestReplacementGroupDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_SHORT_DESC As String = BestReplacementGroupDAL.COL_NAME_CLAIM_ID
        Public Const COL_NAME_MIGRATION_PATH_ID As String = BestReplacementGroupDAL.COL_NAME_MIGRATION_PATH_ID
        Public Const COL_NAME_COMPANY_GROUP_ID As String = BestReplacementGroupDAL.COL_NAME_COMPANY_GROUP_ID
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property MigrationPathId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_MIGRATION_PATH_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CompanyGroupId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMPANY_GROUP_ID), Byte()))
            End Get
        End Property
    End Class

#End Region

#Region "Children Related"
    Public ReadOnly Property BestReplacementChildren() As BestReplacementList
        Get
            Return New BestReplacementList(Me)
        End Get
    End Property

    Public Function GetDetailSelectionView() As BestReplacementSelectionView
        Dim t As DataTable = BestReplacementSelectionView.CreateTable
        Dim detail As BestReplacement

        For Each detail In Me.BestReplacementChildren
            Dim row As DataRow = t.NewRow
            row(BestReplacementSelectionView.COL_NAME_MANUFACTURER) = detail.EquipmentManufacturer
            row(BestReplacementSelectionView.COL_NAME_MODEL) = detail.EquipmentModel
            row(BestReplacementSelectionView.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER) = detail.ReplacementEquipmentManufacturer
            row(BestReplacementSelectionView.COL_NAME_REPLACEMENT_MODEL) = detail.ReplacementEquipmentModel
            row(BestReplacementSelectionView.COL_NAME_PRIORITY) = detail.Priority.Value
            row(BestReplacementSelectionView.COL_NAME_BEST_REPLACEMENT_ID) = detail.Id.ToByteArray()
            t.Rows.Add(row)
        Next
        Return New BestReplacementSelectionView(t)
    End Function

    Public Class BestReplacementSelectionView
        Inherits DataView
        Public Const COL_NAME_MANUFACTURER As String = BestReplacementDAL.COL_NAME_EQUIPMENT_MANUFACTURER
        Public Const COL_NAME_MODEL As String = BestReplacementDAL.COL_NAME_EQUIPMENT_MODEL
        Public Const COL_NAME_BEST_REPLACEMENT_ID As String = BestReplacementDAL.COL_NAME_BEST_REPLACEMENT_ID
        Public Const COL_NAME_PRIORITY As String = BestReplacementDAL.COL_NAME_PRIORITY
        Public Const COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER As String = BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER
        Public Const COL_NAME_REPLACEMENT_MODEL As String = BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MODEL

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_BEST_REPLACEMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_MANUFACTURER, GetType(String))
            t.Columns.Add(COL_NAME_MODEL, GetType(String))
            t.Columns.Add(COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER, GetType(String))
            t.Columns.Add(COL_NAME_REPLACEMENT_MODEL, GetType(String))
            t.Columns.Add(COL_NAME_PRIORITY, GetType(Long))
            Return t
        End Function
    End Class

    Public Function GetChild(ByVal childId As Guid) As BestReplacement
        Return CType(Me.BestReplacementChildren.GetChild(childId), BestReplacement)
    End Function

    Public Function GetNewChild() As BestReplacement
        Dim newBestReplacement As BestReplacement = CType(Me.BestReplacementChildren.GetNewChild, BestReplacement)
        newBestReplacement.MigrationPathId = Me.Id
        Return newBestReplacement
    End Function

#End Region

End Class
