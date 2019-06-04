Public Class ListPrice
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
            Dim dal As New ListPriceDAL
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
            Dim dal As New ListPriceDAL
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

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal dealerId As Guid, ByVal strManufacturerName As String, _
                             ByVal strModelNumber As String, ByVal strSku As String, ByVal fromDate As String, _
                             ByVal toDate As String, ByVal amountTypeId As Guid) As ListPriceSearchDV
        Try
            Dim dal As New ListPriceDAL
            Return New ListPriceSearchDV(dal.LoadListAll(ElitaPlusIdentity.Current.ActiveUser.LanguageId, _
                                                         dealerId, strManufacturerName, strModelNumber, strSku, _
                                                         fromDate, toDate, amountTypeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Public Methods"
    Public Shared Function GetSKU(ByVal dealerId As Guid, ByVal manufacturerName As String, _
                             ByVal modelNumber As String) As String
        Try
            Dim dal As New ListPriceDAL
            Dim dv As ListPriceSearchDV = New ListPriceSearchDV(dal.LoadPriceList(dealerId, manufacturerName, modelNumber, String.Empty, _
                String.Empty, String.Empty).Tables(0))
            Dim sku As String = String.Empty
            For Each dvr As DataRowView In dv
                If (sku = String.Empty) Then
                    sku = dv.SKU(dvr.Row)
                Else
                    If (sku <> dv.SKU(dvr.Row)) Then
                        ' We got 2 SKUs for same Make and Model so do not resolve
                        Return String.Empty
                    End If
                End If
            Next
            Return sku
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function IsSKUValid(ByVal dealerId As Guid, ByVal strSKU As String, ByVal dtEffective As Date, ByRef dListPrice As DecimalType) As Boolean
        Dim blnValid As Boolean = False
        dListPrice = ListPrice.GetListPrice(dealerId, strSKU, dtEffective.ToString("yyyyMMdd"))
        If (dListPrice <> Nothing) Then
            blnValid = True
        End If
        Return blnValid
    End Function

    Public Shared Function GetListPrice(ByVal dealerId As Guid, ByVal strSku As String, _
                         ByVal dateOfLoss As String) As DecimalType
        Try
            Dim dal As New ListPriceDAL
            Dim dv As ListPriceSearchDV = New ListPriceSearchDV(dal.LoadPriceList(dealerId, String.Empty, String.Empty, strSku, _
                dateOfLoss, dateOfLoss).Tables(0))
            If (dv.Count = 1) Then
                Return New DecimalType(dv.Amount(dv(0).Row))
            Else
                Return Nothing
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Removed as a part of REQ-1244 . List price is available through Price List (REQ-1106) functionality
    'Public Shared Function GetRepairAuthAmount(ByVal dealerId As Guid, ByVal strSku As String, ByVal dtEffective As Date) As DecimalType
    '    Try
    '        If strSku.Trim = String.Empty Then
    '            Return Nothing
    '        Else
    '            Dim dal As New ListPriceDAL
    '            Dim ds As DataSet = dal.LoadRepairAuthAmount(dealerId, strSku, dtEffective)

    '            If (ds.Tables(0).Rows.Count = 1) Then
    '                Return New DecimalType(CType(ds.Tables(0).Rows(0).Item(ListPriceSearchDV.COL_NAME_AMOUNT), Decimal))
    '            Else
    '                Return Nothing
    '            End If
    '        End If

    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    'End Function
#End Region

#Region "ListPriceSearchDV"

    Public Class ListPriceSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_LIST_PRICE_ID As String = ListPriceDAL.COL_NAME_LIST_PRICE_ID
        Public Const COL_NAME_DEALER_NAME As String = ListPriceDAL.COL_NAME_DEALER_NAME
        Public Const COL_NAME_DEALER_CODE As String = ListPriceDAL.COL_NAME_DEALER_CODE
        Public Const COL_NAME_SKU_NUMBER As String = ListPriceDAL.COL_NAME_SKU_NUMBER
        Public Const COL_NAME_MODEL_NUMBER As String = ListPriceDAL.COL_NAME_MODEL_NUMBER
        Public Const COL_NAME_MANUFACTURER_NAME As String = ListPriceDAL.COL_NAME_MANUFACTURER_NAME
        Public Const COL_NAME_AMOUNT As String = ListPriceDAL.COL_NAME_AMOUNT
        Public Const COL_NAME_EFFECTIVE As String = ListPriceDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = ListPriceDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_AMOUNT_TYPE_ID As String = ListPriceDAL.COL_NAME_AMOUNT_TYPE_ID
        Public Const COL_NAME_AMOUNT_TYPE_DESC As String = ListPriceDAL.COL_NAME_AMOUNT_TYPE_DESC
#End Region

#Region "Properties"
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ListPriceId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_LIST_PRICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property DealerName(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DEALER_NAME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property DealerCode(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DEALER_CODE).ToString
            End Get
        End Property


        Public Shared ReadOnly Property SKU(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_SKU_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ModelNumber(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_MODEL_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ManufacturerName(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_MANUFACTURER_NAME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Effective(ByVal row As DataRow) As Date
            Get
                Return row(COL_NAME_EFFECTIVE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(ByVal row As DataRow) As Date
            Get
                Return row(COL_NAME_EXPIRATION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Amount(ByVal row As DataRow) As Decimal
            Get
                Return CType(row(COL_NAME_AMOUNT), Decimal)
            End Get
        End Property

        Public Shared ReadOnly Property AmountTypeId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_AMOUNT_TYPE_ID), Byte()))
            End Get
        End Property
#End Region
    End Class
#End Region
End Class
