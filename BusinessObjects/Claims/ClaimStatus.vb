'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/11/2008)  ********************

Public Class ClaimStatus
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
            Dim dal As New ClaimStatusDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
            Dim dal As New ClaimStatusDAL
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
                dal.Load(Me.Dataset, id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
    Dim _claimBO As ClaimBase
    Dim _isTimeZoneForClaimExtStatusDateDone As Boolean = False
    ''testing TFS2008
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ClaimStatusDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimStatusByGroupId() As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1200)> _
    Public Property Comments() As String
        Get
            CheckDeleted()
            If row(ClaimStatusDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimStatusDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimStatusDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property


    Public ReadOnly Property StatusOrder() As LongType
        Get
            If Row(ClaimStatusDAL.COL_NAME_STATUS_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimStatusDAL.COL_NAME_STATUS_ORDER), Long))
            End If
        End Get
    End Property

    Public ReadOnly Property StatusCode() As String
        Get
            If Row(ClaimStatusDAL.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_STATUS_CODE), String)
            End If
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            If Row(ClaimStatusDAL.COL_NAME_STATUS_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_STATUS_DESCRIPTION), String)
            End If
        End Get
    End Property

    Public ReadOnly Property Owner() As String
        Get
            If Row(ClaimStatusDAL.COL_NAME_OWNER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_OWNER), String)
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property StatusDate() As DateTimeType
        Get
            CheckDeleted()
            If Row(ClaimStatusDAL.COL_NAME_STATUS_DATE_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_STATUS_DATE_1), DateTime)
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(ClaimStatusDAL.COL_NAME_STATUS_DATE_1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property ExternalUserName() As String
        Get
            CheckDeleted()
            If Row(ClaimStatusDAL.COL_NAME_EXTERNAL_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_EXTERNAL_USER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimStatusDAL.COL_NAME_EXTERNAL_USER_NAME, Value)
        End Set
    End Property

    Public Property IsTimeZoneForClaimExtStatusDateDone() As Boolean
        Get
            Return Me._isTimeZoneForClaimExtStatusDateDone
        End Get
        Set(ByVal value As Boolean)
            Me._isTimeZoneForClaimExtStatusDateDone = value
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.Row.RowState <> DataRowState.Detached Then
                If Me.Row.RowState <> DataRowState.Deleted Then
                    Me.HandelTimeZoneForClaimExtStatusDate()
                End If
                Dim dal As New ClaimStatusDAL
                Me.UpdateFamily(Me.Dataset)
                Dim blnGVSCall As Boolean = False
                If Me.Row.RowState <> DataRowState.Deleted Then
                    blnGVSCall = True
                End If
                dal.UpdateFamily(Me.Dataset)
                If blnGVSCall = True Then
                    ' Create transaction log header if the service center is integrated with GVS
                    Dim objClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.ClaimId, Me.Dataset)
                    If objClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Single Then CType(objClaimBO, Claim).HandleGVSTransactionCreation(Guid.Empty, Nothing)
                End If
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

    Public Shared Function GetLatestClaimStatus(ByVal claimID As Guid) As ClaimStatus
        Dim oClaimStatus As ClaimStatus = Nothing

        Try
            Dim dal As New ClaimStatusDAL
            Dim dtClaim As DataTable = dal.GetLatestClaimStatus(claimID, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)

            If Not dtClaim Is Nothing AndAlso dtClaim.Rows.Count > 0 Then
                oClaimStatus = New ClaimStatus(New Guid(CType(dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte())))
            End If

            Return oClaimStatus
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimStatusList(ByVal claimID As Guid) As ClaimStatusSearchDV

        Try
            Dim dal As New ClaimStatusDAL
            Return New ClaimStatusSearchDV(dal.LoadList(claimID, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function UpdateExtendedMV(ByVal claimId As System.Guid) As ClaimStatus
        If claimId <> Guid.Empty Then
            Dim dal As New ClaimStatusDAL
            Dim dtClaim As DataTable = dal.GetLatestClaimStatus(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            If Not dtClaim Is Nothing And dtClaim.Rows.Count > 0 Then
                Dim dtTable As DataTable = dal.GetExtendedMVStatus(claimId).Tables(0)
                If dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_STATUS_CODE) <> dtTable.Rows(0)(ClaimStatusDAL.COL_NAME_CLAIM_STATUS) Then
                    dal.UpdateExtendedMV(claimId, New Guid(CType(dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte())), dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_STATUS_CODE))
                End If
            End If
        End If
    End Function


    Public Shared Function GetClaimStatus(ByVal claimId As Guid) As DataView
        Try
            Dim dal As New ClaimStatusDAL
            Return New DataView(dal.GetClaimStatus(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimStatusByUserRole(ByVal claimId As Guid) As DataView
        Try
            Dim dal As New ClaimStatusDAL
            Return New DataView(dal.GetClaimStatusByUserRole(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimStatusHistoryOnly(ByVal claimId As Guid) As DataView
        Try
            Dim dal As New ClaimStatusDAL
            Return New DataView(dal.GetClaimStatusHistoryOnly(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetExtendedStatusForCompanyGroup() As DataView
        Try
            Dim dal As New ClaimStatusDAL
            Return New DataView(dal.GetExtendedStatusForCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function AddClaimStatus(ByVal claimStatusId As Guid) As ClaimStatus
        Dim objClaimStatus As ClaimStatus

        If Not claimStatusId.Equals(Guid.Empty) Then
            objClaimStatus = New ClaimStatus(claimStatusId, Me.Dataset)
        Else
            objClaimStatus = New ClaimStatus(Me.Dataset)
        End If

        Return objClaimStatus
    End Function

    Public Function ClaimBO(ByVal claimId As Guid) As ClaimBase

        If Me._claimBO Is Nothing Then
            Me._claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimId)
        End If
        Return Me._claimBO

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal claimStatusId As Guid, ByVal claimID As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID) = claimStatusId.ToByteArray
        dt.Rows.Add(row)

        Return (dv)

    End Function

    Public Sub HandelTimeZoneForClaimExtStatusDate(Optional ByVal objClaimBO As ClaimBase = Nothing)

        If objClaimBO Is Nothing Then
            objClaimBO = ClaimBO(Me.ClaimId)
        End If
        If Not objClaimBO.Company.TimeZoneNameId.Equals(System.Guid.Empty) Then
            Dim strTimeZonename As String = LookupListNew.GetDescriptionFromId(LookupListNew.LK_TIME_ZONE_NAMES, objClaimBO.Company.TimeZoneNameId)
            Me.UpdateStatusDateOnFamily(strTimeZonename)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub UpdateStatusDateOnFamily(ByVal strTimeZoneName)
        Dim dt As DataTable = Me.Dataset.Tables(ClaimStatusDAL.TABLE_NAME)



        Dim rowIdx As Integer
        For rowIdx = 0 To dt.Rows.Count - 1
            Dim rowState As DataRowState = dt.Rows(rowIdx).RowState
            'Dim dr As DataRow = dt.Rows(rowIdx)
            'Dim dc As DataColumn = dt.Columns(.Item(ClaimStatusDAL.COL_NAME_STATUS_DATE_1)

            If rowState = DataRowState.Added Then   ' New Row
                dt.Rows(rowIdx)(ClaimStatusDAL.COL_NAME_STATUS_DATE_1) = LoadConvertedTime_From_DB_ServerTimeZone(dt.Rows(rowIdx)(ClaimStatusDAL.COL_NAME_STATUS_DATE_1), strTimeZoneName)
            ElseIf rowState = DataRowState.Modified Then ' Modified Row

                If dt.Rows(rowIdx)(ClaimStatusDAL.COL_NAME_STATUS_DATE_1, DataRowVersion.Current) <> dt.Rows(rowIdx)(ClaimStatusDAL.COL_NAME_STATUS_DATE_1, DataRowVersion.Original) Then
                    dt.Rows(rowIdx)(ClaimStatusDAL.COL_NAME_STATUS_DATE_1) = LoadConvertedTime_From_DB_ServerTimeZone(dt.Rows(rowIdx)(ClaimStatusDAL.COL_NAME_STATUS_DATE_1), strTimeZoneName)
                End If
            End If

            Me.IsTimeZoneForClaimExtStatusDateDone = True
        Next
    End Sub

    Public Shared Sub AddClaimToNewPickList(ByVal claim_id As Guid, ByVal claim_status_by_groupID As Guid, ByVal external_user_name As String, ByVal comments As String)

        Try
            Dim dal As New ClaimStatusDAL

            dal.AddClaimToNewPickList(claim_id, claim_status_by_groupID, external_user_name, comments)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "ClaimStatusSearchDV"
    Public Class ClaimStatusSearchDV
        Inherits DataView

        Public Const COL_CLAIM_STATUS_ID As String = "claim_status_id"
        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_COMMENTS As String = "comments"
        Public Const COL_STATUS_ORDER As String = "status_order"
        Public Const COL_STATUS_CODE As String = "status_code"
        Public Const COL_STATUS_DESCRIPTION As String = "status_description"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

End Class


