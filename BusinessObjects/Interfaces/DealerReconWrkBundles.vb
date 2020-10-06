Public Class DealerReconWrkBundles
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const COL_ITEM_MANUFACTURER As String = "ITEM_MANUFACTURER"
    Public Const COL_ITEM_MODEL As String = "ITEM_MODEL"
    Public Const COL_ITEM_SERIAL_NUMBER As String = "ITEM_SERIAL_NUMBER"
    Public Const COL_ITEM_DESCRIPTION As String = "ITEM_DESCRIPTION"
    Public Const COL_ITEM_PRICE As String = "ITEM_PRICE"
    Public Const COL_ITEM_BUNDLE_VAL As String = "ITEM_BUNDLE_VAL"
    Public Const COL_ITEM_MAN_WARRANTY As String = "ITEM_MAN_WARRANTY"

#End Region

#Region "Constructors"

    'New BO
    Public Sub New()
        MyBase.New()
    End Sub
    'New BO
    Public Sub New(ByVal dealerProcessedID As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(dealerProcessedID)
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal oRow As DataRow, ByVal ds As DataSet)
        MyBase.New()
        Dataset = ds
        Row = oRow
    End Sub


    Protected Sub Load(ByVal dealerFileProcessedID As Guid)
        Try
            Dim dal As New DealerReconWrkBundlesDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset, dealerFileProcessedID)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            Initialize()
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

    <ValidStringLength("", Max:=2)> _
    Public Property ItemNumber() As String
        Get
            CheckDeleted()
            If Row(COL_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(COL_ITEM_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property ItemManufacturer() As String
        Get
            CheckDeleted()
            If Row(COL_ITEM_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_MANUFACTURER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(COL_ITEM_MANUFACTURER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property ItemModel() As String
        Get
            CheckDeleted()
            If Row(COL_ITEM_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(COL_ITEM_MODEL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property ItemSerialNumber() As String
        Get
            CheckDeleted()
            If Row(COL_ITEM_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(COL_ITEM_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property ItemDescription() As String
        Get
            CheckDeleted()
            If Row(COL_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(COL_ITEM_DESCRIPTION, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99999999.99)> _
    Public Property ItemPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(COL_ITEM_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_PRICE), Decimal)
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(COL_ITEM_PRICE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property ItemBundleValue() As String
        Get
            CheckDeleted()
            If Row(COL_ITEM_BUNDLE_VAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_BUNDLE_VAL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(COL_ITEM_BUNDLE_VAL, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99999)> _
    Public Property ItemManWarranty() As LongType
        Get
            CheckDeleted()
            If Row(COL_ITEM_MAN_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ITEM_MAN_WARRANTY), Long)
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(COL_ITEM_MAN_WARRANTY, Value)
        End Set
    End Property
#End Region

#Region "External Properties"

    Shared ReadOnly Property CompanyId(ByVal DealerfileProcessedId As Guid) As Guid
        Get
            Dim oDealerfileProcessed As New DealerFileProcessed(DealerfileProcessedId)
            Dim oDealer As New Dealer(oDealerfileProcessed.DealerId)
            Dim oCompanyId As Guid = oDealer.CompanyId
            Return oCompanyId
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Validate()
        'Only validating
        MyBase.Validate()
    End Sub
    Public Function SaveBundles(ByVal ds As DataSet) As Integer
        Try
            Dim dal As New DealerReconWrkBundlesDAL
            Return dal.SaveBundles(ds)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(ByVal dealerReconWrkId As Guid) As DataSet
        Try
            Dim dal As New DealerReconWrkBundlesDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerReconWrkId)
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

End Class
