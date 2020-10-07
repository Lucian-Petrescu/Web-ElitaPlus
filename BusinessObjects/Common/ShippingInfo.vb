'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/31/2006)  ********************

Public Class ShippingInfo
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
            Dim dal As New ShippingInfoDAL
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
            Dim dal As New ShippingInfoDAL
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
    Private _claimIDHasBeenObtained As Boolean = False
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(ShippingInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ShippingInfoDAL.COL_NAME_SHIPPING_INFO_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property CreditCardNumber As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_CREDIT_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_CREDIT_CARD_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_CREDIT_CARD_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10, Min:=6)> _
    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProcessingFee As DecimalType
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_PROCESSING_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ShippingInfoDAL.COL_NAME_PROCESSING_FEE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_PROCESSING_FEE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TotalCharge As DecimalType
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_TOTAL_CHARGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ShippingInfoDAL.COL_NAME_TOTAL_CHARGE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_TOTAL_CHARGE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ShippingInfoDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ShippingInfoDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=25)> _
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(ShippingInfoDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ShippingInfoDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ShippingInfoDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property

    Public Property ClaimIDHasBeenObtained As Boolean
        Get
            Return _claimIDHasBeenObtained
        End Get
        Set
            _claimIDHasBeenObtained = Value
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ShippingInfoDAL
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
    Public Sub PrePopulate(ByVal objCertItemCoverage As CertItemCoverage, ByVal objServiceCenter As ServiceCenter)
        Dim objCert As New Certificate(objCertItemCoverage.CertId)
        Dim objAddress As New Address(objCert.AddressId)
        Address1 = objAddress.Address1
        Address2 = objAddress.Address2
        City = objAddress.City
        RegionId = objAddress.RegionId
        CountryId = objAddress.CountryId
        PostalCode = objAddress.ZipLocator
        ProcessingFee = objServiceCenter.ProcessingFee
        TotalCharge = New DecimalType(objServiceCenter.ProcessingFee.Value + objCertItemCoverage.Deductible.Value)

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
        Address1 = objShippingInfo.Address1
        Address2 = objShippingInfo.Address2
        City = objShippingInfo.City
        RegionId = objShippingInfo.RegionId
        CountryId = objShippingInfo.CountryId
        PostalCode = objShippingInfo.PostalCode
        ProcessingFee = objShippingInfo.ProcessingFee
        TotalCharge = objShippingInfo.TotalCharge
        CreditCardNumber = objShippingInfo.CreditCardNumber
        AuthorizationNumber = objShippingInfo.AuthorizationNumber

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region
End Class


