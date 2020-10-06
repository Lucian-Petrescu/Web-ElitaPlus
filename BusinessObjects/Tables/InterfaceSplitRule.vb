Public Class InterfaceSplitRule
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

    Protected Sub Load()
        Try
            Dim dal As New InterfaceSplitRuleDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New InterfaceSplitRuleDAL
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

    Public Property HeaderSourceCode As String
        Get
            For Each dr As DataRow In Row.Table.Rows
                If (Not dr(InterfaceSplitRuleDAL.COL_NAME_SOURCE_CODE).ToString() Is Nothing) Then
                    Return dr(InterfaceSplitRuleDAL.COL_NAME_SOURCE_CODE).ToString()
                End If
            Next

            Return String.Empty
        End Get
        Set(ByVal value As String)
            For Each dr As DataRow In Row.Table.Rows
                dr(InterfaceSplitRuleDAL.COL_NAME_SOURCE_CODE) = value
            Next
        End Set
    End Property

    Public Property HeaderSource As String
        Get
            For Each dr As DataRow In Row.Table.Rows
                If (Not dr(InterfaceSplitRuleDAL.COL_NAME_SOURCE).ToString() Is Nothing) Then
                    Return dr(InterfaceSplitRuleDAL.COL_NAME_SOURCE).ToString()
                End If
            Next

            Return String.Empty
        End Get
        Set(ByVal value As String)
            For Each dr As DataRow In Row.Table.Rows
                dr(InterfaceSplitRuleDAL.COL_NAME_SOURCE) = value
            Next
        End Set
    End Property

    ''Public Property DefaultNewSourceCode As String
    ''Get
    'For Each dr As DataRow In Me.Row.Table.Rows
    '            If (dr(InterfaceSplitRuleDAL.COL_NAME_FIELD_NAME).ToString() Is Nothing) Then
    '                Return dr(InterfaceSplitRuleDAL.COL_NAME_NEW_SOURCE_CODE).ToString()
    '            End If
    '        Next

    '        Return String.Empty
    '    End Get
    '    Set(ByVal value As String)
    '        Dim defaultRow As DataRow
    '        For Each dr As DataRow In Me.Row.Table.Rows
    '            If (dr(InterfaceSplitRuleDAL.COL_NAME_FIELD_NAME).ToString() = String.Empty) Then
    '                defaultRow = dr
    '            End If
    '        Next

    '        Dim defaultRule As InterfaceSplitRule
    '        If (defaultRow Is Nothing) Then
    '            If ((value Is Nothing) OrElse (value = String.Empty)) Then Return
    '            defaultRule = New InterfaceSplitRule(Me.Row.Table.DataSet)
    '            defaultRule.Source = Me.Source
    '            defaultRule.SourceCode = Me.SourceCode
    '        Else
    '            defaultRule = New InterfaceSplitRule(New Guid(DirectCast(defaultRow(InterfaceSplitRuleDAL.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte())), Me.Row.Table.DataSet)
    '            If ((value Is Nothing) OrElse (value = String.Empty)) Then
    '                defaultRule.Active = "N"
    '                defaultRule.Save()
    '                Return
    '            End If
    '        End If

    '        defaultRule.NewSourceCode = value
    '    End Set
    'End Property

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"
    <ValueMandatory("")> _
    Public ReadOnly Property Id() As Guid
        Get
            If Row(InterfaceSplitRuleDAL.COL_NAME_INTERFACE_SPLIT_RULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InterfaceSplitRuleDAL.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Source() As String
        Get
            CheckDeleted()
            If Row(InterfaceSplitRuleDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(InterfaceSplitRuleDAL.COL_NAME_SOURCE)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(InterfaceSplitRuleDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property SourceCode() As String
        Get
            CheckDeleted()
            If Row(InterfaceSplitRuleDAL.COL_NAME_SOURCE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(InterfaceSplitRuleDAL.COL_NAME_SOURCE_CODE)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(InterfaceSplitRuleDAL.COL_NAME_SOURCE_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property NewSourceCode() As String
        Get
            CheckDeleted()
            If Row(InterfaceSplitRuleDAL.COL_NAME_NEW_SOURCE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(InterfaceSplitRuleDAL.COL_NAME_NEW_SOURCE_CODE)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(InterfaceSplitRuleDAL.COL_NAME_NEW_SOURCE_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property Active() As String
        Get
            CheckDeleted()
            If Row(InterfaceSplitRuleDAL.COL_NAME_ACTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(InterfaceSplitRuleDAL.COL_NAME_ACTIVE)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(InterfaceSplitRuleDAL.COL_NAME_ACTIVE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)> _
    Public Property FieldName() As String
        Get
            CheckDeleted()
            If Row(InterfaceSplitRuleDAL.COL_NAME_FIELD_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(InterfaceSplitRuleDAL.COL_NAME_FIELD_NAME)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(InterfaceSplitRuleDAL.COL_NAME_FIELD_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property FieldOperator() As String
        Get
            CheckDeleted()
            If Row(InterfaceSplitRuleDAL.COL_NAME_FIELD_OPERATOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(InterfaceSplitRuleDAL.COL_NAME_FIELD_OPERATOR)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(InterfaceSplitRuleDAL.COL_NAME_FIELD_OPERATOR, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2000)> _
    Public Property FieldValue() As String
        Get
            CheckDeleted()
            If Row(InterfaceSplitRuleDAL.COL_NAME_FIELD_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(InterfaceSplitRuleDAL.COL_NAME_FIELD_VALUE)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(InterfaceSplitRuleDAL.COL_NAME_FIELD_VALUE, Value)
        End Set
    End Property
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal source As String, ByVal sourceCode As String) As InterfaceSplitRule.InterfaceSplitRuleSearchDV
        Try
            Dim dal As New InterfaceSplitRuleDAL

            If (Not (sourceCode.Contains(DALBase.WILDCARD_CHAR) Or sourceCode.Contains(DALBase.ASTERISK)) And String.IsNullOrEmpty(sourceCode.Trim)) Then
                sourceCode = sourceCode & DALBase.ASTERISK
            End If

            If (source Is Nothing) Then source = String.Empty

            If (Not (source.Contains(DALBase.WILDCARD_CHAR) Or source.Contains(DALBase.ASTERISK)) And String.IsNullOrEmpty(source.Trim)) Then
                source = sourceCode & DALBase.ASTERISK
            End If
            Return New InterfaceSplitRuleSearchDV(dal.LoadList(source, sourceCode).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

    Public Class InterfaceSplitRuleSearchDV
        Inherits DataView

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property InterfaceSplitRuleId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(InterfaceSplitRuleDAL.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Source(ByVal row) As String
            Get
                Return row(InterfaceSplitRuleDAL.COL_NAME_SOURCE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property SourceCode(ByVal row) As String
            Get
                Return row(InterfaceSplitRuleDAL.COL_NAME_SOURCE_CODE).ToString
            End Get
        End Property

    End Class

    Public Class InterfaceSplitRuleSelectionView
        Inherits DataView
        Public Const COL_NAME_INTERFACE_SPLIT_RULE_ID As String = InterfaceSplitRuleDAL.COL_NAME_INTERFACE_SPLIT_RULE_ID
        Public Const COL_NAME_FIELD_NAME As String = InterfaceSplitRuleDAL.COL_NAME_FIELD_NAME
        Public Const COL_NAME_FIELD_OPERATOR As String = InterfaceSplitRuleDAL.COL_NAME_FIELD_OPERATOR
        Public Const COL_NAME_FIELD_VALUE As String = InterfaceSplitRuleDAL.COL_NAME_FIELD_VALUE
        Public Const COL_NAME_NEW_SOURCE_CODE As String = InterfaceSplitRuleDAL.COL_NAME_NEW_SOURCE_CODE
        Public Const COL_NAME_IS_NEW As String = "IS_NEW"

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_INTERFACE_SPLIT_RULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_FIELD_NAME, GetType(String))
            t.Columns.Add(COL_NAME_FIELD_OPERATOR, GetType(String))
            t.Columns.Add(COL_NAME_FIELD_VALUE, GetType(String))
            t.Columns.Add(COL_NAME_NEW_SOURCE_CODE, GetType(String))
            t.Columns.Add(COL_NAME_IS_NEW, GetType(Boolean))
            Return t
        End Function
    End Class

    Public Function GetChildSelectionView() As InterfaceSplitRuleSelectionView
        Dim dt As DataTable = InterfaceSplitRuleSelectionView.CreateTable()
        Dim vdr As DataRow
        For Each dr As DataRow In Row.Table.Rows()
            If ((Not dr(InterfaceSplitRuleDAL.COL_NAME_ACTIVE) Is Nothing) AndAlso (dr(InterfaceSplitRuleDAL.COL_NAME_ACTIVE).ToString() = "Y")) Then
                vdr = dt.NewRow()
                vdr(InterfaceSplitRuleSelectionView.COL_NAME_INTERFACE_SPLIT_RULE_ID) = dr(InterfaceSplitRuleDAL.COL_NAME_INTERFACE_SPLIT_RULE_ID)
                vdr(InterfaceSplitRuleSelectionView.COL_NAME_FIELD_NAME) = dr(InterfaceSplitRuleDAL.COL_NAME_FIELD_NAME)
                vdr(InterfaceSplitRuleSelectionView.COL_NAME_FIELD_OPERATOR) = dr(InterfaceSplitRuleDAL.COL_NAME_FIELD_OPERATOR)
                vdr(InterfaceSplitRuleSelectionView.COL_NAME_FIELD_VALUE) = dr(InterfaceSplitRuleDAL.COL_NAME_FIELD_VALUE)
                vdr(InterfaceSplitRuleSelectionView.COL_NAME_NEW_SOURCE_CODE) = dr(InterfaceSplitRuleDAL.COL_NAME_NEW_SOURCE_CODE)
                vdr(InterfaceSplitRuleSelectionView.COL_NAME_IS_NEW) = False
                dt.Rows.Add(vdr)
            End If
        Next

        Return New InterfaceSplitRuleSelectionView(dt)

    End Function

    Public Function GetRuleChild(ByVal SelectedChildId As Guid) As InterfaceSplitRule
        For Each dr As DataRow In Row.Table.Rows
            Dim interfaceSplitRuleId As Guid = New Guid(DirectCast(dr(InterfaceSplitRuleDAL.COL_NAME_INTERFACE_SPLIT_RULE_ID), Byte()))
            If (interfaceSplitRuleId = SelectedChildId) Then
                Return New InterfaceSplitRule(interfaceSplitRuleId, Dataset)
            End If
        Next

        Throw New BOInvalidOperationException()
    End Function

    Public Function GetNewRuleChild(ByVal Source As String, ByVal SourceCode As String) As InterfaceSplitRule
        Dim returnValue = New InterfaceSplitRule(Row.Table.DataSet)
        returnValue.Source = UCase(Source)  'If(String.IsNullOrEmpty(Me.HeaderSource), "XXX", Me.HeaderSource)
        returnValue.SourceCode = UCase(SourceCode) 'If(String.IsNullOrEmpty(Me.HeaderSourceCode), "XXX", Me.HeaderSourceCode)
        returnValue.Active = "Y"
        Return returnValue
    End Function

    Public Overrides Sub Delete()
        Row.BeginEdit()
        Active = "N"
        Save(Row)
        Row.EndEdit()
    End Sub

    Public Overrides Sub Save()
        Dim splitRule As InterfaceSplitRule
        Dim dal As New InterfaceSplitRuleDAL

        For index = Row.Table.Rows.Count - 1 To 0 Step -1
            If (Me.Row.Table(index).RowState = DataRowState.Added) AndAlso (Row.Table(index)(InterfaceSplitRuleDAL.COL_NAME_ACTIVE).ToString() = "") Then
                Row.Table(index).Delete()
            Else
                Save(Row.Table(index))
            End If
        Next

        dal.Update(Row.Table)
    End Sub
End Class
