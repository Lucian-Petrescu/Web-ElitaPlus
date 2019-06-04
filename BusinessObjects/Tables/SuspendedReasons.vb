'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/17/2013)  ********************

Public Class SuspendedReasons
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const COL_NAME_ID As String = SuspendedReasonsDAL.COL_NAME_ID
    Public Const COL_NAME_DEALER_ID As String = SuspendedReasonsDAL.COL_NAME_DEALER_ID
    Public Const COL_NAME_DESCRIPTION As String = SuspendedReasonsDAL.COL_NAME_DESCRIPTION
    Public Const COL_NAME_CODE As String = SuspendedReasonsDAL.COL_NAME_CODE
    Public Const COL_NAME_CLAIM_ALLOWED As String = SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED
    Public Const COL_NAME_DEALER_NAME As String = SuspendedReasonsDAL.COL_NAME_DEALER_NAME
    Public Const COL_NAME_CLAIM_ALLOWED_STR As String = SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED_STR

#End Region


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

    Public Sub New(ByVal id As Guid, ByVal inDv As DataSet)
        MyBase.New(False)
        Me.Dataset = New DataSet
        Me.Load(id, inDv)
    End Sub

    'Exiting BO attaching to a BO family
    'Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
    '    MyBase.New(False)
    '    Me.Dataset = familyDS
    '    Me.Load(id)
    'End Sub

    ''New BO attaching to a BO family
    'Public Sub New(ByVal familyDS As DataSet)
    '    MyBase.New(False)
    '    Me.Dataset = familyDS
    '    Me.Load()
    'End Sub



    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub
  

    'Public Shared Sub Load(ByVal Id As System.Guid, ByRef NewBO As SuspendedReasons, ByVal dv As SuspendedReasons.SearchDV)
    '    '        Dim dt As DataTable

    '    For Each drv As DataRowView In CType(dv, DataView)
    '        Dim cguid As Guid = New Guid(CType(drv.Item(SuspendedReasons.COL_NAME_ID), Byte()))

    '        If cguid = Id Then
    '            For Each col As DataColumn In drv.Row.Table.Columns
    '                NewBO.Row(col.ColumnName) = drv.Row(col.ColumnName)
    '            Next

    '            Exit For
    '        End If

    '    Next

    'End Sub
    Protected Sub Load(ByVal Id As System.Guid, ByVal inDv As DataSet)

        If Me.Dataset Is Nothing OrElse Me.Dataset.Tables.Count() = 0 Then
            Dim dal As New SuspendedReasonsDAL
            dal.LoadSchema(Me.Dataset)
        End If

        Dim newRow As DataRow = Me.Dataset.Tables(0).NewRow

        For Each drv As DataRow In inDv.Tables(0).Rows
            Dim cguid As Guid = New Guid(CType(drv.Item(SuspendedReasons.COL_NAME_ID), Byte()))

            If cguid = Id Then
                Me.Dataset.Tables(0).Rows.Add(newRow)
                Me.Row = newRow

                For Each col As DataColumn In drv.Table.Columns
                    newRow(col.ColumnName) = drv(col.ColumnName)
                Next

                Me.Row.AcceptChanges()  '** Remove the isNew from the BO Row

                Exit For
            End If
        Next
    End Sub
    Protected Sub Load()
        Try
            Dim dal As New SuspendedReasonsDAL

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
            Dim dal As New SuspendedReasonsDAL

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
                dal.Load(Me.Dataset, id, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
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
    Public Property Suspended_Reasons_Id() As Guid
        Get
            If Row(SuspendedReasonsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SuspendedReasonsDAL.COL_NAME_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SuspendedReasonsDAL.COL_NAME_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(SuspendedReasonsDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SuspendedReasonsDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SuspendedReasonsDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property
    Public Property Dealer_Name() As String
        Get
            CheckDeleted()
            If Row(SuspendedReasonsDAL.COL_NAME_DEALER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SuspendedReasonsDAL.COL_NAME_DEALER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SuspendedReasonsDAL.COL_NAME_DEALER_NAME, Value)
        End Set
    End Property

    Public Property Claim_Allowed_Str() As String
        Get
            CheckDeleted()
            If Row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED_STR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED_STR), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED_STR, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Claim_Allowed() As String
        Get
            CheckDeleted()
            If Row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()

            Me.SetValue(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED, Value.Replace("0", ""))
        End Set

    End Property

    Public ReadOnly Property Claim_Allowed_True() As Boolean
        Get
            CheckDeleted()
            If Not (Row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED) Is DBNull.Value) Then
                If CType(Row(SuspendedReasonsDAL.COL_NAME_CLAIM_ALLOWED), String) = "Y" Then
                    Return True
                End If
            End If

            Return False
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(SuspendedReasonsDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SuspendedReasonsDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SuspendedReasonsDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=250)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(SuspendedReasonsDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SuspendedReasonsDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SuspendedReasonsDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Sub Save(ByVal LanguageId As Guid, ByVal NewRec As Boolean)
        Try
            MyBase.Save()

            If (Me._isDSCreator OrElse Me.IsDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then

                Dim dal As New SuspendedReasonsDAL
                Dim RowId As Guid

                If NewRec Then
                    dal.InserRow(Me.Row, ElitaPlusIdentity.Current.ActiveUser.NetworkId, RowId)
                Else
                    dal.UpdateRow(Me.Row, RowId)
                End If

                Me.Row.AcceptChanges()
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region


#Region "DataView Retrieveing Methods"

    Public Shared Sub AddNewRowToSearchDV(ByRef inDV As SuspendedReasons.SearchDV, ByVal NewBO As SuspendedReasons)
        Dim newDS As DataSet, dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow

            If inDV Is Nothing Then
                blnEmptyTbl = True

                '** Copy the Structure from BO to SerachDV **    
                newDS = NewBO.Dataset.Clone
                dt = newDS.Tables(0)
            Else
                dt = inDV.Table
            End If

            row = dt.NewRow

            '*** Copy data from BO to SearchDV ***
            Dim BODataRow As DataRow = NewBO.Dataset.Tables(0).Rows(0)

            For Each col As DataColumn In NewBO.Dataset.Tables(0).Columns
                row(col.ColumnName) = BODataRow.Item(col.ColumnName)
            Next

            dt.Rows.Add(row)

            If blnEmptyTbl Then inDV = New SearchDV(dt)
        End If
    End Sub

    Public Shared Function LoadSearchData(ByVal SearchValues As SuspendedReasons.SearchDV.Values) As SuspendedReasons.SearchDV

        Dim dal As New SuspendedReasonsDAL
        Try
            Return New SearchDV(dal.LoadList(SearchValues.CompanyGroupID _
                                             , SearchValues.Dealer _
                                             , SearchValues.Code _
                                             , SearchValues.Description _
                                             , SearchValues.Claim_Allowed _
                                             , ElitaPlusIdentity.Current.ActiveUser.NetworkId).Tables(0))


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "RouteSearchDV"

    Public Class SearchDV
        Inherits DataView

        Structure Values
            Dim CompanyGroupID As Guid
            Dim CompanyID As Guid
            Dim Dealer As Guid
            Dim LanguageID As Guid
            Dim ID As Guid
            Dim Code As String
            Dim Description As String
            Dim ClaimAllowedID As Guid
            Dim Claim_Allowed As String
            Dim Claim_Allowed_Str As String
        End Structure


        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
        Public Shared ReadOnly Property Suspended_reason_Id(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Dealer_Id(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_DEALER_ID), Byte()))
            End Get
        End Property
        Public Shared ReadOnly Property Dealer_Name(ByVal row) As String
            Get
                Return row(COL_NAME_DEALER_NAME).ToString
            End Get
        End Property
        Public Shared ReadOnly Property routeId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_ID), Byte()))
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

        Public Shared ReadOnly Property CLAIM_ALLOWED(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CLAIM_ALLOWED).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CLAIM_ALLOWED_STR(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CLAIM_ALLOWED_STR).ToString
            End Get
        End Property

    End Class
#End Region

End Class


