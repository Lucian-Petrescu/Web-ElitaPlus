'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/21/2006)  ********************

Public Class EarningPattern
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
            Dim dal As New EarningPatternDAL
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
            Dim dal As New EarningPatternDAL
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

#Region "Constants"

    Public Const COL_EARNING_PATTERN_ID As String = "EARNING_PATTERN_ID"
    Public Const COL_DESCRIPTION As String = "DESCRIPTION"
    Public Const COL_CODE As String = "CODE"
    Public Const COL_EFFECTIVE As String = "EFFECTIVE"
    Public Const COL_EXPIRATION As String = "EXPIRATION"
    Public Const WILDCARD_CHAR As Char = "%"
    Public Const ASTERISK As Char = "*"
    Private Const DSNAME As String = "LIST"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(EarningPatternDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EarningPatternDAL.COL_NAME_EARNING_PATTERN_ID), Byte()))
            End If
        End Get
    End Property

    '***** JLR ****
    Public ReadOnly Property Description() As String
        Get
            CheckDeleted()
            If Row(EarningPatternDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EarningPatternDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
    End Property

    Public ReadOnly Property Code() As String
        Get
            CheckDeleted()
            If Row(EarningPatternDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EarningPatternDAL.COL_NAME_CODE), String)
            End If
        End Get
    End Property
    '***** JLR ****

    <ValueMandatory(""), ValidPatternDates(""), ValidPatternPeriod("")> _
    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If Row(EarningPatternDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(EarningPatternDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(EarningPatternDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If Row(EarningPatternDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(EarningPatternDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(EarningPatternDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EarningCodeId() As Guid
        Get
            CheckDeleted()
            If Row(EarningPatternDAL.COL_NAME_EARNING_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EarningPatternDAL.COL_NAME_EARNING_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EarningPatternDAL.COL_NAME_EARNING_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property EarningPatternStartsOnId() As Guid
        Get
            CheckDeleted()
            If Row(EarningPatternDAL.COL_NAME_EARNING_PATTERN_STARTS_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EarningPatternDAL.COL_NAME_EARNING_PATTERN_STARTS_ON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EarningPatternDAL.COL_NAME_EARNING_PATTERN_STARTS_ON_ID, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New EarningPatternDAL
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

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal Description As String, ByVal Code As String, ByVal CompanyGroupId As Guid) As DataView
        Try
            Dim dal As New EarningPatternDAL
            Return New DataView(dal.LoadList(Description, Code, CompanyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Function IsLastPattern() As Boolean
        Dim obj As EarningPattern = Me
        Dim ds As Dataset
        Dim dal As New EarningPatternDAL
        Dim maxExpiration As Date

        If Not (obj.EarningCodeId.Equals(Guid.Empty)) Then
            ds = dal.LoadList(obj.EarningCodeId, Guid.Empty)
            Dim recCount As Integer = 0
            If ds.Tables.Count > 0 Then
                recCount = ds.Tables(0).Rows.Count
                If recCount > 0 Then
                    maxExpiration = ds.Tables(0).Rows(recCount - 1)("EXPIRATION")
                    If Me.Expiration.Value <> maxExpiration Then
                        Return False
                    End If
                End If
            End If
        End If

        Return True

    End Function

    Public Function IsFirstPattern() As Boolean
        Dim obj As EarningPattern = Me
        Dim ds As Dataset
        Dim dal As New EarningPatternDAL
        Dim minEffective As Date

        If Not (obj.EarningCodeId.Equals(Guid.Empty)) Then
            ds = dal.LoadList(obj.EarningCodeId, Guid.Empty)
            Dim recCount As Integer = 0
            If ds.Tables.Count > 0 Then
                recCount = ds.Tables(0).Rows.Count
                If recCount > 0 Then
                    minEffective = ds.Tables(0).Rows(0)("EFFECTIVE")
                    If Me.Effective.Value <> minEffective Then
                        Return False
                    End If
                End If
            End If
        End If

        Return True

    End Function

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPatternDates
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ElitaPlus.Common.ErrorCodes.EARNING_PATTERN_DATE_IS_INVALID)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EarningPattern = CType(objectToValidate, EarningPattern)
            If Not (obj.Effective Is Nothing Or obj.Expiration Is Nothing) Then
                If obj.Effective.Value > obj.Expiration.Value Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPatternPeriod
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ElitaPlus.Common.ErrorCodes.EARNING_PATTERN_RANGE_IS_INVALID)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean

            Dim obj As EarningPattern = CType(objectToValidate, EarningPattern)
            Dim bValid As Boolean = True
            Dim dal As New EarningPatternDAL
            Dim ds As New Dataset
            Dim currRow, prevRow, nextRow As DataRow

            If obj.Effective Is Nothing Or obj.Expiration Is Nothing Then
                Return True  ' Skip validation. Rely on mandatory field validation to report exception
            End If
            If Not (obj.EarningCodeId.Equals(Guid.Empty)) Then
                ds = dal.LoadList(obj.EarningCodeId, obj.Id)
                Dim recCount As Integer = 0
                If ds.Tables.Count > 0 Then
                    recCount = ds.Tables(0).Rows.Count
                    Dim lastRowId As Guid
                    Dim currRowPos As Integer = 0
                    If recCount > 0 Then
                        lastRowId = New Guid(CType(ds.Tables(0).Rows(recCount - 1)("EARNING_PATTERN_ID"), Byte()))
                        Dim minEffective As Date = ds.Tables(0).Rows(0)("EFFECTIVE")
                        Dim maxExpiration As Date = ds.Tables(0).Rows(recCount - 1)("EXPIRATION")
                        ' Inserting at the begining
                        If obj.Expiration.Value.AddDays(1) = minEffective Then
                            Return True
                        End If
                        ' Inserting at the end
                        If obj.Effective.Value.AddDays(-1) = maxExpiration Then
                            Return True
                        End If
                        ' Find a spot in the middle
                        For Each currRow In ds.Tables(0).Rows
                            If obj.Expiration.Value = currRow("EXPIRATION") And _
                                obj.Effective.Value = currRow("EFFECTIVE") Then
                                ' Trying to insert a Duplicate - Reject!
                                Return False
                            ElseIf Not prevRow Is Nothing Then
                                ' Inserting in the middle (Allow to fix any GAPS)
                                If obj.Effective.Value.AddDays(-1) = prevRow("EXPIRATION") And _
                                   obj.Expiration.Value.AddDays(1) = currRow("EFFECTIVE") Then
                                    Return True
                                End If
                            End If
                            prevRow = currRow
                            currRowPos += 1
                        Next
                        bValid = False
                    End If
                End If
            End If

            Return bValid

        End Function
    End Class

#End Region

End Class




