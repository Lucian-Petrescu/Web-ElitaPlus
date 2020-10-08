'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/11/2012)  ********************
Imports Microsoft.SqlServer.Server

Public Class IssueType
    Inherits BusinessObjectBase

#Region "Constructors"

    'properties
    Public MyDropDownBO As DropdownItem
    Public MyDropDownParentId As Guid
    Public MyDropDownParentCode As String
    Public MyDropDownListItemId As Guid = Guid.Empty
    Public MyDropDownOldCode As String
    Public MyDropDownNewItemCode As String
    Public MyDropDownNewItemDesc As String

    Public IssueTypeCode As String = "ISSTYP"

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
            Dim dal As New IssueTypeDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New IssueTypeDAL
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
            If Row(IssueTypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueTypeDAL.COL_NAME_ISSUE_TYPE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(IssueTypeDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueTypeDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueTypeDAL.COL_NAME_CODE, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(IssueTypeDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueTypeDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueTypeDAL.COL_NAME_DESCRIPTION, value)
        End Set
    End Property

    Public Property IsSystemGenerated As Guid
        Get
            CheckDeleted()
            If Row(IssueTypeDAL.COL_NAME_IS_SYSTEM_GENERATED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueTypeDAL.COL_NAME_IS_SYSTEM_GENERATED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueTypeDAL.COL_NAME_IS_SYSTEM_GENERATED_ID, value)
        End Set
    End Property

    Public Property IsSelfCleaning As Guid
        Get
            CheckDeleted()
            If Row(IssueTypeDAL.COL_NAME_IS_SELF_CLEANING_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueTypeDAL.COL_NAME_IS_SELF_CLEANING_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueTypeDAL.COL_NAME_IS_SELF_CLEANING_ID, value)
        End Set
    End Property

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid, bo As IssueType) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow
            row(IssueTypeDAL.COL_NAME_ISSUE_TYPE_ID) = bo.Id.ToByteArray
            row(IssueTypeDAL.COL_NAME_DESCRIPTION) = bo.Description
            row(IssueTypeDAL.COL_NAME_CODE) = bo.Code
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then

                If MyDropDownParentCode.Equals(String.Empty) Or MyDropDownParentCode.Equals(Nothing) Then
                    MyDropDownParentCode = IssueTypeCode
                End If

                MyDropDownParentId = GetDropdownId(MyDropDownParentCode)

                Dim dal As New IssueTypeDAL

                If (Not MyDropDownParentId.Equals(Guid.Empty)) Then
                    dal.MyDropDownNewItemCode = MyDropDownNewItemCode
                    dal.MyDropDownParentId = MyDropDownParentId
                    dal.MyDropDownNewItemDesc = MyDropDownNewItemDesc
                    dal.MyDropDownUser = ElitaPlusIdentity.Current.ActiveUser.NetworkId

                    If IsDeleted Then
                        dal.MyDropDownAction = "Delete"
                        dal.MyDropDownListItemId = GetDropdownItemId(MyDropDownParentId, MyDropDownOldCode.ToUpper())
                    ElseIf MyDropDownListItemId.Equals(Guid.Empty) AndAlso IsNew Then
                        If Not GetDropdownItemId(MyDropDownParentId, MyDropDownNewItemCode.ToUpper()).Equals(Guid.Empty) Then
                            Dim err As New ValidationError(Common.ErrorCodes.DUPLICATE_ISSUE_TYPE_ERR, [GetType], Nothing, "CodeLabel", Nothing)
                            Throw New BOValidationException(New ValidationError() {err}, [GetType].Name, UniqueId)
                        End If
                        dal.MyDropDownAction = "Add"
                    ElseIf Not IsDeleted AndAlso IsDirty AndAlso Not MyDropDownListItemId.Equals(Guid.Empty) Then
                        dal.MyDropDownListItemId = GetDropdownItemId(MyDropDownParentId, MyDropDownOldCode.ToUpper())
                        dal.MyDropDownAction = "Update"
                    End If

                    dal.UpdateFamily(Dataset)
                Else
                    Throw New DataException("No corrosponding Dropdown list item found")
                End If

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

#Region "DataView Retrieveing Methods"

    Public Shared Function GetDropdownId(listCode As String) As Guid
        Dim issueType As New IssueTypeDAL
        Return New Guid(issueType.GetDropdownId(listCode).ToString)
    End Function

    Public Shared Function GetDropdownItemId(dropdownId As Guid, itemCode As String) As Guid
        Dim issueType As New IssueTypeDAL
        Return New Guid(issueType.GetDropdownItemId(dropdownId, itemCode).ToString)
    End Function

    Public Shared Function GetList(code As String, _
                                      description As String) As IssueType.IssueTypeSearchDV

        Try
            Dim dal As New IssueTypeDAL

            If (description.Contains(DALBase.WILDCARD_CHAR) Or description.Contains(DALBase.ASTERISK)) Then
                description = description & DALBase.ASTERISK
            End If
            If (code.Contains(DALBase.WILDCARD_CHAR) Or code.Contains(DALBase.ASTERISK)) Then
                code = code & DALBase.ASTERISK
            End If

            Return New IssueTypeSearchDV(dal.LoadList(code, description, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class IssueTypeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_ISSUE_TYPE_ID As String = IssueTypeDAL.COL_NAME_ISSUE_TYPE_ID
        Public Const COL_NAME_CODE As String = IssueTypeDAL.COL_NAME_CODE
        Public Const COL_NAME_DESCRIPTION As String = IssueTypeDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_IS_SYSTEM_GENERATED_DESC As String = IssueTypeDAL.COL_NAME_IS_SYSTEM_GENERATED_DESC
        Public Const COL_NAME_IS_SELF_CLEANING_DESC As String = IssueTypeDAL.COL_NAME_IS_SELF_CLEANING_DESC
        Public Const COL_NAME_IS_SYSTEM_GENERATED_ID As String = IssueTypeDAL.COL_NAME_IS_SYSTEM_GENERATED_ID
        Public Const COL_NAME_IS_SELF_CLEANING_ID As String = IssueTypeDAL.COL_NAME_IS_SELF_CLEANING_ID
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

End Class


