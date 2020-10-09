'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/11/2008)  ********************

Public Class ClaimStatus
    Inherits BusinessObjectBase

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
            Dim dal As New ClaimStatusDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
            Dim dal As New ClaimStatusDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
    Dim _claimBO As ClaimBase
    Dim _isTimeZoneForClaimExtStatusDateDone As Boolean = False
    ''testing TFS2008
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(ClaimStatusDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimStatusByGroupId As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1200)> _
    Public Property Comments As String
        Get
            CheckDeleted()
            If row(ClaimStatusDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimStatusDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property


    Public ReadOnly Property StatusOrder As LongType
        Get
            If Row(ClaimStatusDAL.COL_NAME_STATUS_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimStatusDAL.COL_NAME_STATUS_ORDER), Long))
            End If
        End Get
    End Property

    Public ReadOnly Property StatusCode As String
        Get
            If Row(ClaimStatusDAL.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_STATUS_CODE), String)
            End If
        End Get
    End Property

    Public ReadOnly Property StatusDescription As String
        Get
            If Row(ClaimStatusDAL.COL_NAME_STATUS_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_STATUS_DESCRIPTION), String)
            End If
        End Get
    End Property

    Public ReadOnly Property Owner As String
        Get
            If Row(ClaimStatusDAL.COL_NAME_OWNER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_OWNER), String)
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property StatusDate As DateTimeType
        Get
            CheckDeleted()
            If Row(ClaimStatusDAL.COL_NAME_STATUS_DATE_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_STATUS_DATE_1), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusDAL.COL_NAME_STATUS_DATE_1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property ExternalUserName As String
        Get
            CheckDeleted()
            If Row(ClaimStatusDAL.COL_NAME_EXTERNAL_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStatusDAL.COL_NAME_EXTERNAL_USER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusDAL.COL_NAME_EXTERNAL_USER_NAME, Value)
        End Set
    End Property

    Public Property IsTimeZoneForClaimExtStatusDateDone As Boolean
        Get
            Return _isTimeZoneForClaimExtStatusDateDone
        End Get
        Set
            _isTimeZoneForClaimExtStatusDateDone = value
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso Row.RowState <> DataRowState.Detached Then
                If Row.RowState <> DataRowState.Deleted Then
                    HandelTimeZoneForClaimExtStatusDate()
                End If
                Dim dal As New ClaimStatusDAL
                UpdateFamily(Dataset)
                Dim blnGVSCall As Boolean = False
                If Row.RowState <> DataRowState.Deleted Then
                    blnGVSCall = True
                End If
                dal.UpdateFamily(Dataset)
                If blnGVSCall = True Then
                    ' Create transaction log header if the service center is integrated with GVS
                    Dim objClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId, Dataset)
                    If objClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Single Then CType(objClaimBO, Claim).HandleGVSTransactionCreation(Guid.Empty, Nothing)
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

    Public Shared Function GetLatestClaimStatus(claimID As Guid) As ClaimStatus
        Dim oClaimStatus As ClaimStatus = Nothing

        Try
            Dim dal As New ClaimStatusDAL
            Dim dtClaim As DataTable = dal.GetLatestClaimStatus(claimID, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)

            If dtClaim IsNot Nothing AndAlso dtClaim.Rows.Count > 0 Then
                oClaimStatus = New ClaimStatus(New Guid(CType(dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte())))
            End If

            Return oClaimStatus
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimStatusList(claimID As Guid) As ClaimStatusSearchDV

        Try
            Dim dal As New ClaimStatusDAL
            Return New ClaimStatusSearchDV(dal.LoadList(claimID, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function UpdateExtendedMV(claimId As System.Guid) As ClaimStatus
        If claimId <> Guid.Empty Then
            Dim dal As New ClaimStatusDAL
            Dim dtClaim As DataTable = dal.GetLatestClaimStatus(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            If dtClaim IsNot Nothing AndAlso dtClaim.Rows.Count > 0 Then
                Dim dtTable As DataTable = dal.GetExtendedMVStatus(claimId).Tables(0)
                If dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_STATUS_CODE) <> dtTable.Rows(0)(ClaimStatusDAL.COL_NAME_CLAIM_STATUS) Then
                    dal.UpdateExtendedMV(claimId, New Guid(CType(dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte())), dtClaim.Rows(0)(ClaimStatusDAL.COL_NAME_STATUS_CODE))
                End If
            End If
        End If
    End Function


    Public Shared Function GetClaimStatus(claimId As Guid) As DataView
        Try
            Dim dal As New ClaimStatusDAL
            Return New DataView(dal.GetClaimStatus(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimStatusByUserRole(claimId As Guid) As DataView
        Try
            Dim dal As New ClaimStatusDAL
            Return New DataView(dal.GetClaimStatusByUserRole(claimId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimStatusHistoryOnly(claimId As Guid) As DataView
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

    Public Function AddClaimStatus(claimStatusId As Guid) As ClaimStatus
        Dim objClaimStatus As ClaimStatus

        If Not claimStatusId.Equals(Guid.Empty) Then
            objClaimStatus = New ClaimStatus(claimStatusId, Dataset)
        Else
            objClaimStatus = New ClaimStatus(Dataset)
        End If

        Return objClaimStatus
    End Function

    Public Function ClaimBO(claimId As Guid) As ClaimBase

        If _claimBO Is Nothing Then
            _claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimId)
        End If
        Return _claimBO

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, claimStatusId As Guid, claimID As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID) = claimStatusId.ToByteArray
        dt.Rows.Add(row)

        Return (dv)

    End Function

    Public Sub HandelTimeZoneForClaimExtStatusDate(Optional ByVal objClaimBO As ClaimBase = Nothing)

        If objClaimBO Is Nothing Then
            objClaimBO = ClaimBO(ClaimId)
        End If
        If Not objClaimBO.Company.TimeZoneNameId.Equals(Guid.Empty) Then
            Dim strTimeZonename As String = LookupListNew.GetDescriptionFromId(LookupListCache.LK_TIME_ZONE_NAMES, objClaimBO.Company.TimeZoneNameId)
            UpdateStatusDateOnFamily(strTimeZonename)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub UpdateStatusDateOnFamily(strTimeZoneName)
        Dim dt As DataTable = Dataset.Tables(ClaimStatusDAL.TABLE_NAME)



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

            IsTimeZoneForClaimExtStatusDateDone = True
        Next
    End Sub

    Public Shared Sub AddClaimToNewPickList(claim_id As Guid, claim_status_by_groupID As Guid, external_user_name As String, comments As String)

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

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

End Class


