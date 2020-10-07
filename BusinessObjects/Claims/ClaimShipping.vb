'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/22/2015)  ********************

Public Class ClaimShipping
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
           Dim dal As New ClaimShippingDAL
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
            Dim dal As New ClaimShippingDAL            
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
            If row(ClaimShippingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimShippingDAL.COL_NAME_CLAIM_SHIPPING_ID), Byte()))
            End If
        End Get
    End Property
          
    <ValueMandatory("")> _
    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If row(ClaimShippingDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimShippingDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimShippingDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property
          
          
    <ValueMandatory("")> _
    Public Property ShippingTypeId As Guid
        Get
            CheckDeleted()
            If row(ClaimShippingDAL.COL_NAME_SHIPPING_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimShippingDAL.COL_NAME_SHIPPING_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimShippingDAL.COL_NAME_SHIPPING_TYPE_ID, Value)
        End Set
    End Property
          
          
    <ValueMandatory("")> _
    Public Property ShippingDate As DateType
        Get
            CheckDeleted()
            If row(ClaimShippingDAL.COL_NAME_SHIPPING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ClaimShippingDAL.COL_NAME_SHIPPING_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimShippingDAL.COL_NAME_SHIPPING_DATE, Value)
        End Set
    End Property
          
          
    <ValidStringLength("", Max:=120)> _
    Public Property TrackingNumber As String
        Get
            CheckDeleted()
            If row(ClaimShippingDAL.COL_NAME_TRACKING_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimShippingDAL.COL_NAME_TRACKING_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimShippingDAL.COL_NAME_TRACKING_NUMBER, Value)
        End Set
    End Property

    

    Public Property ReceivedDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimShippingDAL.COL_NAME_RECEIVED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimShippingDAL.COL_NAME_RECEIVED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimShippingDAL.COL_NAME_RECEIVED_DATE, Value)
        End Set
    End Property

    Private _claim As ClaimBase

    Public Property Claim As ClaimBase
        Get
            If (_claim Is Nothing) Then
                If Not ClaimId.Equals(Guid.Empty) Then
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId, Dataset)
                End If
            End If
            Return _claim
        End Get
        Private Set
            _claim = value
        End Set
    End Property

    Private ReadOnly Property HasReceivedDateChanged
        Get
            If (Row.HasVersion(DataRowVersion.Original)) Then
                If (Row(ClaimShippingDAL.COL_NAME_RECEIVED_DATE, DataRowVersion.Original) Is DBNull.Value) Then
                    Return Not ReceivedDate Is Nothing
                Else
                    Return ReceivedDate <> DirectCast(Row(ClaimShippingDAL.COL_NAME_RECEIVED_DATE, DataRowVersion.Original), Date)
                End If
            Else
                Return Not ReceivedDate Is Nothing
            End If

        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                If HasReceivedDateChanged Then
                    With Claim
                        PublishedTask.AddEvent(companyGroupId:=.Company.CompanyGroupId, _
                                               companyId:=.CompanyId, _
                                               countryId:=.Company.CountryId, _
                                               dealerId:=.Dealer.Id, _
                                               productCode:=.Certificate.ProductCode, _
                                               coverageTypeId:=.CertificateItemCoverage.CoverageTypeId, _
                                               sender:="Claim Shipping", _
                                               arguments:="ClaimId:" & DALBase.GuidToSQLString(.Id), _
                                               eventDate:=TimeZoneInfo.ConvertTimeToUtc(ReceivedDate), _
                                               eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CUST_DEVICE_RECEPTION_DATE), _
                                               eventArgumentId:=Nothing)
                    End With
                End If
                Dim dal As New ClaimShippingDAL
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

    Public Function LoadClaimShippingData()
        Try
            Dim dal As New ClaimShippingDAL

            If (Not ClaimId.Equals(Guid.Empty)) Then
                Return New ClaimShippingDV(dal.LoadClaimShippingData(ClaimId).Tables(0))
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetLatestClaimShippingInfo(claimId As Guid, shipTypeId As Guid) As ClaimShippingDV
        Try
            Dim dal As New ClaimShippingDAL
            
            If (Not ClaimId.Equals(Guid.Empty)) Then
                Return New ClaimShippingDV(dal.GetLatestClaimShippingInfo(claimId, shipTypeId).Tables(0))
            End If

        Catch ex As Exception
            
        End Try
    End Function

    Public Shared Sub UpdateClaimShippingInfo(claimShippingId As Guid, comments As String)
        Dim dal As New ClaimShippingDAL
        dal.UpdateClaimShippingInfo(claimShippingId, comments)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region


    Public Class ClaimShippingDV
        Inherits DataView

        Public Const COL_CLAIM_SHIPPING_ID As String = "claim_shipping_id"
        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_SHIPPING_TYPE_ID As String = "shipping_type_id"
        Public Const COL_SHIPPING_DATE As String = "shipping_date"
        Public Const COL_TRACKING_NUMBER As String = "tracking_number"
        Public Const COL_RECEIVED_DATE As String = "received_date"
        Public Const COL_CARRIER_CODE As String = "carrier_code"
        Public Const COL_CARRIER_NAME As String = "carrier_name"
        
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As ClaimShippingDV, NewBO As ClaimShipping)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(ClaimShippingDV.COL_CLAIM_SHIPPING_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimShippingDV.COL_CLAIM_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimShippingDV.COL_SHIPPING_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimShippingDV.COL_TRACKING_NUMBER, GetType(String))
                dt.Columns.Add(ClaimShippingDV.COL_SHIPPING_DATE, GetType(DateType))
                dt.Columns.Add(ClaimShippingDV.COL_RECEIVED_DATE, GetType(DateType))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(ClaimShippingDV.COL_CLAIM_SHIPPING_ID) = NewBO.Id.ToByteArray
            row(ClaimShippingDV.COL_CLAIM_ID) = NewBO.ClaimId.ToByteArray
            row(ClaimShippingDV.COL_SHIPPING_TYPE_ID) = NewBO.ShippingTypeId.ToByteArray
            row(ClaimShippingDV.COL_TRACKING_NUMBER) = NewBO.TrackingNumber
            row(ClaimShippingDV.COL_SHIPPING_DATE) = NewBO.ShippingDate
            row(ClaimShippingDV.COL_RECEIVED_DATE) = NewBO.ReceivedDate

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New ClaimShippingDV(dt)
        End If
    End Sub
    Public Class ClaimShippingList
        Inherits BusinessObjectListBase

        Public Sub New(parent As ClaimBase)
            MyBase.New(LoadTable(parent), GetType(ClaimShipping), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimShipping).ClaimId.Equals(CType(Parent, ClaimBase).Id)
        End Function

        Private Shared Function LoadTable(parent As ClaimBase) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ClaimShippingList)) Then
                    Dim dal As New ClaimShippingDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ClaimShippingList))
                End If
                Return parent.Dataset.Tables(ClaimShippingDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

End Class


