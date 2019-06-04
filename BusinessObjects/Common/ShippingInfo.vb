'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/31/2006)  ********************

Public Class ShippingInfo
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
            Dim dal As New ShippingInfoDAL
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
            Dim dal As New ShippingInfoDAL
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
    Private _claimIDHasBeenObtained As Boolean = False
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ShippingInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ShippingInfoDAL.COL_NAME_SHIPPING_INFO_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property CreditCardNumber() As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_CREDIT_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_CREDIT_CARD_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_CREDIT_CARD_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10, Min:=6)> _
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProcessingFee() As DecimalType
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_PROCESSING_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ShippingInfoDAL.COL_NAME_PROCESSING_FEE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_PROCESSING_FEE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TotalCharge() As DecimalType
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_TOTAL_CHARGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ShippingInfoDAL.COL_NAME_TOTAL_CHARGE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_TOTAL_CHARGE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ShippingInfoDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property City() As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ShippingInfoDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=25)> _
    Public Property PostalCode() As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ShippingInfoDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property

    Public Property ClaimIDHasBeenObtained() As Boolean
        Get
            Return _claimIDHasBeenObtained
        End Get
        Set(ByVal Value As Boolean)
            _claimIDHasBeenObtained = Value
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ShippingInfoDAL
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
    Public Sub PrePopulate(ByVal objCertItemCoverage As CertItemCoverage, ByVal objServiceCenter As ServiceCenter)
        Dim objCert As New Certificate(objCertItemCoverage.CertId)
        Dim objAddress As New Address(objCert.AddressId)
        Me.Address1 = objAddress.Address1
        Me.Address2 = objAddress.Address2
        Me.City = objAddress.City
        Me.RegionId = objAddress.RegionId
        Me.CountryId = objAddress.CountryId
        Me.PostalCode = objAddress.ZipLocator
        Me.ProcessingFee = objServiceCenter.ProcessingFee
        Me.TotalCharge = New DecimalType(objServiceCenter.ProcessingFee.Value + objCertItemCoverage.Deductible.Value)

    End Sub
    Public Shared Sub DeleteNewChildShippingInfo(ByVal parentClaim As Claim)
        Dim row As DataRow
        If parentClaim.Dataset.Tables.IndexOf(ShippingInfoDAL.TABLE_NAME) >= 0 Then
            Dim rowIndex As Integer
            For rowIndex = 0 To parentClaim.Dataset.Tables(ShippingInfoDAL.TABLE_NAME).Rows.Count - 1
                row = parentClaim.Dataset.Tables(ShippingInfoDAL.TABLE_NAME).Rows.Item(rowIndex)
                If Not (row.RowState = DataRowState.Deleted) Or (row.RowState = DataRowState.Detached) Then
                    Dim si As ShippingInfo = New ShippingInfo(row)
                    If parentClaim.ShippingInfoId.Equals(si.Id) And si.IsNew Then
                        si.Delete()
                    End If
                End If
            Next

        End If
    End Sub

    Public Sub CopyFromThis(ByVal objShippingInfo As ShippingInfo)
        Me.Address1 = objShippingInfo.Address1
        Me.Address2 = objShippingInfo.Address2
        Me.City = objShippingInfo.City
        Me.RegionId = objShippingInfo.RegionId
        Me.CountryId = objShippingInfo.CountryId
        Me.PostalCode = objShippingInfo.PostalCode
        Me.ProcessingFee = objShippingInfo.ProcessingFee
        Me.TotalCharge = objShippingInfo.TotalCharge
        Me.CreditCardNumber = objShippingInfo.CreditCardNumber
        Me.AuthorizationNumber = objShippingInfo.AuthorizationNumber

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region
End Class


