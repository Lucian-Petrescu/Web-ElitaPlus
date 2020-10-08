'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/21/2008)  ********************

Public Class BranchStandardization
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const WILDCARD_CHAR As Char = "%"
    Public Const ASTERISK As Char = "*"

#End Region


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
            Dim dal As New BranchStandardizationDAL
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
            Dim dal As New BranchStandardizationDAL
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
            If Row(BranchStandardizationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchStandardizationDAL.COL_NAME_BRANCH_STANDARDIZATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(BranchStandardizationDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchStandardizationDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchStandardizationDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property DealerBranchCode As String
        Get
            CheckDeleted()
            If Row(BranchStandardizationDAL.COL_NAME_DEALER_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchStandardizationDAL.COL_NAME_DEALER_BRANCH_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchStandardizationDAL.COL_NAME_DEALER_BRANCH_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BranchId As Guid
        Get
            CheckDeleted()
            If Row(BranchStandardizationDAL.COL_NAME_BRANCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchStandardizationDAL.COL_NAME_BRANCH_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchStandardizationDAL.COL_NAME_BRANCH_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New BranchStandardizationDAL
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

#Region "DataView Retrieveing Methods"

    Public Shared Sub AddNewRowToBranchStandardizationSearchDV(ByRef dv As BranchStandardizationSearchDV, NewBranchStandardizationBO As BranchStandardization)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBranchStandardizationBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(BranchStandardizationSearchDV.COL_BRANCH_STANDARDIZATION_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(BranchStandardizationSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(BranchStandardizationSearchDV.COL_BRANCH_ALIAS, GetType(String))
                dt.Columns.Add(BranchStandardizationSearchDV.COL_BRANCH_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(BranchStandardizationSearchDV.COL_DEALER_NAME, GetType(String))
                dt.Columns.Add(BranchStandardizationSearchDV.COL_BRANCH_CODE, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(BranchStandardizationSearchDV.COL_BRANCH_STANDARDIZATION_ID) = NewBranchStandardizationBO.Id.ToByteArray
            row(BranchStandardizationSearchDV.COL_DEALER_ID) = NewBranchStandardizationBO.DealerId.ToByteArray
            row(BranchStandardizationSearchDV.COL_BRANCH_ALIAS) = NewBranchStandardizationBO.DealerBranchCode
            row(BranchStandardizationSearchDV.COL_BRANCH_ID) = NewBranchStandardizationBO.BranchId.ToByteArray
            row(BranchStandardizationSearchDV.COL_DEALER_NAME) = ""
            row(BranchStandardizationSearchDV.COL_BRANCH_CODE) = ""

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New BranchStandardizationSearchDV(dt)

        End If
    End Sub

    Public Shared Function GetBranchAliasList(descriptionMask As String, _
                                                branchIdForSearch As Guid, _
                                                dealerId As Guid) As BranchStandardizationSearchDV
        Try
            Dim companyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New BranchStandardizationDAL
            Dim ds As DataSet
            Return New BranchStandardizationSearchDV(dal.GetBranchAliasList(descriptionMask, branchIdForSearch, dealerId, companyIds).Tables(0))
            'ds = dal.GetBranchAliasList(descriptionMask, branchIdForSearch, dealerId, companyIds)

            'Return (ds.Tables(dal.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "Memebers"

    Public Function MyGetFormattedSearchStringForSQL(str As String) As String
        If (Not IsNothing(str)) Then
            str = str.Trim
            str = str.ToUpper
            If (str.IndexOf(ASTERISK) > -1) Then
                str = str.Replace(ASTERISK, WILDCARD_CHAR)
            End If
        Else
            str &= WILDCARD_CHAR
        End If
        Return (str)
    End Function

#End Region

#Region "SearchDV"
    Public Class BranchStandardizationSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_BRANCH_STANDARDIZATION_ID As String = BranchStandardizationDAL.COL_NAME_BRANCH_STANDARDIZATION_ID
        Public Const COL_BRANCH_ID As String = BranchStandardizationDAL.COL_NAME_BRANCH_ID
        Public Const COL_DEALER_ID As String = BranchStandardizationDAL.COL_NAME_DEALER_ID
        Public Const COL_BRANCH_ALIAS As String = BranchStandardizationDAL.COL_NAME_DEALER_BRANCH_CODE
        Public Const COL_BRANCH_CODE As String = BranchStandardizationDAL.COL_NAME_BRANCH_CODE
        Public Const COL_DEALER_NAME As String = BranchStandardizationDAL.COL_NAME_DEALER_NAME
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        'Public Function AddNewRowToEmptyDV() As BranchStandardizationSearchDV
        '    Dim dt As DataTable = Me.Table.Clone()
        '    Dim row As DataRow = dt.NewRow
        '    row(BranchStandardizationSearchDV.COL_BRANCH_STANDARDIZATION_ID) = (New Guid()).ToByteArray
        '    dt.Rows.Add(row)
        '    Return New BranchStandardizationSearchDV(dt)
        'End Function
    End Class

#End Region
End Class


