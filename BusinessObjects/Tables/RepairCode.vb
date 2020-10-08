
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/15/2004)  ********************

Public Class RepairCode
    Inherits BusinessObjectBase

#Region "Constructors"

    'Existing BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Existing BO attaching to a BO family
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
            Dim dal As New RepairCodeDAL
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
            Dim dal As New RepairCodeDAL
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
            If row(RepairCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RepairCodeDAL.COL_NAME_REPAIR_CODE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanygroupId As Guid
        Get
            CheckDeleted()
            If Row(RepairCodeDAL.COL_NAME_COMPANY_GROUP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RepairCodeDAL.COL_NAME_COMPANY_GROUP), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RepairCodeDAL.COL_NAME_COMPANY_GROUP, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property ShortDesc As String
        Get
            CheckDeleted()
            If Row(RepairCodeDAL.COL_NAME_SHORT_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairCodeDAL.COL_NAME_SHORT_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RepairCodeDAL.COL_NAME_SHORT_DESC, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(RepairCodeDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairCodeDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RepairCodeDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RepairCodeDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(descriptionMask As String, codeMask As String, _
                                         companyGroupId As Guid) As RepairCodeSearchDV
        Try
            Dim dal As New RepairCodeDAL
            Return New RepairCodeSearchDV(dal.LoadList(descriptionMask, codeMask, companyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(RepairCodeDAL.COL_NAME_REPAIR_CODE_ID) = id.ToByteArray
        row(RepairCodeDAL.COL_NAME_SHORT_DESC) = String.Empty
        row(RepairCodeDAL.COL_NAME_DESCRIPTION) = String.Empty

        dt.Rows.Add(row)

        Return (dv)

    End Function


#Region "SearchDV"
    Public Class RepairCodeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_REPAIR_CODE_ID As String = "repair_code_id"
        Public Const COL_CODE As String = "short_desc"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_COMPANY_GROUP_CODE As String = "company_group_code"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#End Region

End Class


