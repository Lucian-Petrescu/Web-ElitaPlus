'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/18/2014)  ********************

Public Class AcctEventDetailIncExc
    Inherits BusinessObjectBase

#Region "Constructors"
    Private _DealerDescription As String = String.Empty
    Private _CoverageTypeDescription As String = String.Empty
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
            Dim dal As New AcctEventDetailIncexcDAL
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
            Dim dal As New AcctEventDetailIncexcDAL
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
            If Row(AcctEventDetailIncexcDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailIncexcDAL.COL_NAME_ACCT_EVENT_DETAIL_INCEXC_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AcctEventDetailId() As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailIncexcDAL.COL_NAME_ACCT_EVENT_DETAIL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailIncexcDAL.COL_NAME_ACCT_EVENT_DETAIL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctEventDetailIncexcDAL.COL_NAME_ACCT_EVENT_DETAIL_ID, Value)
        End Set
    End Property



    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailIncexcDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailIncexcDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctEventDetailIncexcDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <Config_Criteria_Valid(""), Duplicate_Config_Exists("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailIncexcDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailIncexcDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctEventDetailIncexcDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property CoverageTypeDescription() As String
        Get
            If _CoverageTypeDescription = String.Empty AndAlso Me.CoverageTypeId <> Guid.Empty Then
                _CoverageTypeDescription = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COVERAGE_TYPES, Me.CoverageTypeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If
            Return _CoverageTypeDescription
        End Get
    End Property

    Public ReadOnly Property DealerDescription() As String
        Get
            If _DealerDescription = String.Empty AndAlso Me.DealerId <> Guid.Empty Then
                Dim objDealer As New Dealer(Me.DealerId)
                Dim objComp As New Company(objDealer.CompanyId)
                _DealerDescription = objComp.Code & " - " & objDealer.Dealer & " (" & objDealer.DealerName & ")"
            End If
            Return _DealerDescription
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctEventDetailIncexcDAL
                dal.Update(Me.Row)
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

    'Save to database even not the dataset creator
    Public Sub SaveWithoutCheckDSCreator()
        Try
            MyBase.Save()
            If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctEventDetailIncexcDAL
                dal.Update(Me.Row)
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

    'Verify that value allows in one and only one of the 3 fields
    Public Function AtLeastOneFieldHasValue() As Boolean
        If Me.CoverageTypeId = Guid.Empty AndAlso Me.DealerId = Guid.Empty Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function DuplicateExists(ByVal ListToCheck As Collections.Generic.List(Of AcctEventDetailIncExc)) As Boolean
        Dim blnDup As Boolean = False
        If Me.DealerId <> Guid.Empty AndAlso CoverageTypeId = Guid.Empty Then 'only dealer configured
            If ListToCheck.Exists(Function(r) r.DealerId = Me.DealerId AndAlso r.Id <> Me.Id) Then
                blnDup = True
            End If
        ElseIf Me.DealerId <> Guid.Empty AndAlso Me.CoverageTypeId <> Guid.Empty Then 'both dealer and coverage type configured
            If ListToCheck.Exists(Function(r) r.DealerId = Me.DealerId AndAlso r.CoverageTypeId = Me.CoverageTypeId AndAlso r.Id <> Me.Id) Then
                blnDup = True
            End If
        ElseIf Me.DealerId = Guid.Empty AndAlso Me.CoverageTypeId <> Guid.Empty Then 'only coverage type configured
            If ListToCheck.Exists(Function(r) r.CoverageTypeId = Me.CoverageTypeId AndAlso r.Id <> Me.Id) Then
                blnDup = True
            End If
        End If
        Return blnDup
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetInclusionExclusionConfigByAcctEventDetail(ByVal AcctEventDetailID As Guid) As Collections.Generic.List(Of AcctEventDetailIncexc)
        Dim dal As New AcctEventDetailIncexcDAL
        Dim ds As DataSet = dal.LoadListByAcctEventDetailID(AcctEventDetailID)
        Dim IEList As New Collections.Generic.List(Of AcctEventDetailIncexc)
        For Each dr As DataRow In ds.Tables(0).Rows
            IEList.Add(New AcctEventDetailIncexc(dr))
        Next
        Return IEList
    End Function

    Public Shared Function GetDealerList(ByVal AcctEventID As Guid) As DataView
        Dim dal As New AcctEventDetailIncexcDAL
        Dim ds As DataSet = dal.LoadDealerListByAcctEventDetailID(AcctEventID, Authentication.CurrentUser.Id)
        Return ds.Tables(0).DefaultView
    End Function
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Config_Criteria_Valid
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "AT_LEAST_ONE_FIELD_REQUIRED")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AcctEventDetailIncExc = CType(objectToValidate, AcctEventDetailIncExc)
            Return obj.AtLeastOneFieldHasValue
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Duplicate_Config_Exists
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "DUPLICATE_RECORD")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AcctEventDetailIncExc = CType(objectToValidate, AcctEventDetailIncExc)
            Dim mylist As Collections.Generic.List(Of AcctEventDetailIncExc) = obj.GetInclusionExclusionConfigByAcctEventDetail(obj.AcctEventDetailId)
            Return (Not obj.DuplicateExists(mylist))
        End Function
    End Class
#End Region
End Class


