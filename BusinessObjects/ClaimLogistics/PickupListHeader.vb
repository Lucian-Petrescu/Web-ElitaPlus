'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/29/2008)  ********************

Public Class PickupListHeader
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

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New PickupListHeaderDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New PickupListHeaderDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(PickupListHeaderDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PickupListHeaderDAL.COL_NAME_HEADER_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=120)> _
    Public Property PickupType() As String
        Get
            CheckDeleted()
            If row(PickupListHeaderDAL.COL_NAME_PICKUP_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PickupListHeaderDAL.COL_NAME_PICKUP_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(PickupListHeaderDAL.COL_NAME_PICKUP_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property PicklistNumber() As String
        Get
            CheckDeleted()
            If row(PickupListHeaderDAL.COL_NAME_PICKLIST_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PickupListHeaderDAL.COL_NAME_PICKLIST_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(PickupListHeaderDAL.COL_NAME_PICKLIST_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RouteId() As Guid
        Get
            CheckDeleted()
            If row(PickupListHeaderDAL.COL_NAME_ROUTE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PickupListHeaderDAL.COL_NAME_ROUTE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(PickupListHeaderDAL.COL_NAME_ROUTE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property HeaderStatusId() As Guid
        Get
            CheckDeleted()
            If row(PickupListHeaderDAL.COL_NAME_HEADER_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PickupListHeaderDAL.COL_NAME_HEADER_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(PickupListHeaderDAL.COL_NAME_HEADER_STATUS_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PickupListHeaderDAL
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

    Public Shared Function GetNewOpenClaims(ByVal RouteId As Guid) As DataSet

        Try
            Dim companies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New PickupListHeaderDAL

            Return dal.GetNewOpenClaims(RouteId, companies)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimsReadyFromSC(ByVal RouteId As Guid) As DataSet

        Try
            Dim companies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim dal As New PickupListHeaderDAL

            Return dal.GetClaimsReadyFromSC(RouteId, companies, languageId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimIDByCode(ByVal claimNumber As String, ByVal certItemCoverageCode As String) As Guid
        Dim claimID As Guid = Guid.Empty
        Dim dal As New PickupListHeaderDAL
        Dim ds As DataSet = dal.GetClaimIDByCode(ElitaPlusIdentity.Current.ActiveUser.Companies, claimNumber, certItemCoverageCode)

        If (ds.Tables.Count > 0 And ds.Tables(0).Rows.Count > 0) Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            claimID = New Guid(CType(dr(DALObjects.ClaimDAL.COL_NAME_CLAIM_ID), Byte()))
        End If

        Return claimID

    End Function

    Public Shared Function UpdatePickListStatus(ByVal PickListNumber As String, ByVal pickupBy As String) As DataSet

        Try
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim dal As New PickupListHeaderDAL

            Return dal.UpdatePickListStatus(PickListNumber, pickupBy, userId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function UpdatePickListStatus_Received(ByVal PickListNumber As String, ByVal ServiceCenterID As Guid, ByVal claimStr As String) As DataSet

        Try
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim dal As New PickupListHeaderDAL

            Return dal.UpdatePickListStatus_Received(PickListNumber, ServiceCenterID, claimStr, userId, companyGroupId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetActiveClaimsForSvc(ByVal serviceNetworkID As Guid, ByVal sortOrder As Integer, ByVal claimStatusCodeId As Guid) As DataSet

        Try
            Dim dal As New PickupListHeaderDAL

            Return dal.GetActiveClaimsForSvc(serviceNetworkID, sortOrder, claimStatusCodeId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimInfo(ByVal claimId As Guid, ByVal includeStatusHistory As String, ByVal customerName As String, ByVal customerPhone As String, ByVal AuthorizationNumber As String) As DataSet

        Try
            Dim dal As New PickupListHeaderDAL

            Return dal.GetClaimInfo(claimId, includeStatusHistory, ElitaPlusIdentity.Current.ActiveUser.Companies, ElitaPlusIdentity.Current.ActiveUser.LanguageId, customerName, customerPhone, AuthorizationNumber)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimStatusHistory(ByVal claimId As Guid) As DataSet

        Try
            Dim dal As New PickupListHeaderDAL

            Return dal.GetClaimStatusHistory(claimId, ElitaPlusIdentity.Current.ActiveUser.Companies, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimsByPickList(ByVal HeaderID As Guid, ByVal StoreServiceCenterID As Guid, ByVal ServiceCenterID As Guid) As DataSet

        Try
            Dim companies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New PickupListHeaderDAL

            Return dal.GetClaimsByPickList(HeaderID, StoreServiceCenterID, ServiceCenterID, companies, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetPicklistByDateRange(ByVal startDate As DateTime, ByVal endDate As DateTime) As DataSet

        Try
            Dim dal As New PickupListHeaderDAL

            Return dal.GetPicklistByDateRange(ElitaPlusIdentity.Current.ActiveUser.LanguageId, _
                                              ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, _
                                              startDate, endDate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetClaimsByDateRange(ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal serviceCenterId As Guid) As DataSet

        Try
            Dim dal As New PickupListHeaderDAL

            Return dal.GetClaimsByDateRange(startDate, endDate, serviceCenterId, ElitaPlusIdentity.Current.ActiveUser.Companies, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


