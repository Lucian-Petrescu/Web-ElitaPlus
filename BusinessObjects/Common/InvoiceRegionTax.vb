Public Class InvoiceRegionTax
    Inherits BusinessObjectBase
#Region "Constant"
    Public Const COL_NAME_INVOICE_REGION_TAX_ID As String = "INVOICE_REGION_TAX_ID"
    Public Const COL_NAME_INVOICE_TRANS_ID As String = "INVOICE_TRANS_ID"
    Public Const COL_NAME_REGION As String = "REGION"
    Public Const COL_NAME_REGION_ID As String = "REGION_ID"
    Public Const COL_NAME_TAX_TYPE As String = "TAX_TYPE_XCD"
    Public Const COL_NAME_TAX_AMOUNT As String = "TAX_AMOUNT"
    Public Const COL_NAME_REGION_DESCRIPTION As String = "REGION_DESCRIPTION"


#End Region

#Region "Properties"
    Public ReadOnly Property Id() As Guid
        Get
            If Row(InvoiceRegionTaxDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceRegionTaxDAL.COL_NAME_INVOICE_REGION_TAX_ID), Byte()))
            End If
        End Get
    End Property

    Public Property InvoiceRegionTaxId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceRegionTaxDAL.COL_NAME_INVOICE_REGION_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceRegionTaxDAL.COL_NAME_INVOICE_REGION_TAX_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceRegionTaxDAL.COL_NAME_INVOICE_REGION_TAX_ID, Value)
        End Set
    End Property

    Public Property InvoiceTransactionId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceRegionTaxDAL.COL_NAME_INVOICE_TRANS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceRegionTaxDAL.COL_NAME_INVOICE_TRANS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceRegionTaxDAL.COL_NAME_INVOICE_TRANS_ID, Value)
        End Set
    End Property

    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceRegionTaxDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceRegionTaxDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceRegionTaxDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property

    Public Property TaxType() As String
        Get
            CheckDeleted()
            If Row(InvoiceRegionTaxDAL.COL_NAME_TAX_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceRegionTaxDAL.COL_NAME_TAX_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceRegionTaxDAL.COL_NAME_TAX_TYPE_XCD, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=400)>
    Public Property RegionDescription() As String
        Get
            CheckDeleted()
            If Row(InvoiceRegionTaxDAL.COL_NAME_REGION_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceRegionTaxDAL.COL_NAME_REGION_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceRegionTaxDAL.COL_NAME_REGION_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatoryIIBBtaxAmount(""), ValidNumericRange("", Min:=0, Max:=999999999.99)>
    Public Property TaxAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(InvoiceRegionTaxDAL.COL_NAME_TAX_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(InvoiceRegionTaxDAL.COL_NAME_TAX_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(InvoiceRegionTaxDAL.COL_NAME_TAX_AMOUNT, Value)
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    Public Sub New(ByVal id As Guid, ByVal key As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id, key)
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New InvoiceRegionTaxDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New InvoiceRegionTaxDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then '
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
    Protected Sub Load(ByVal searchid As Guid, ByVal key As Guid)
        Try
            Dim dal As New InvoiceRegionTaxDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(key, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                dal.Load(Me.Dataset, searchid)
                Me.Row = Me.FindRow(key, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceRegionTaxDAL
                dal.SaveInvoiceRegionTax(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim searchid As Guid = Me.InvoiceTransactionId
                    Dim lookupkey As Guid = Me.InvoiceRegionTaxId
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(searchid, lookupkey)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function GetInvoiceRegionTax() As InvoiceRegionTaxDV
        Dim IIBBRegionTaxesDAL As New InvoiceRegionTaxDAL

        If Not (Me.InvoiceTransactionId.Equals(Guid.Empty)) Then
            Return New InvoiceRegionTaxDV(IIBBRegionTaxesDAL.LoadInvoiceRegionTax(Me.InvoiceTransactionId).Tables(0))
        End If

    End Function

    'Public Function ValidateNewRiskTypeTolerance(ByVal DealerInflations As InvoiceRegionTaxDV) As Boolean

    '    Dim dealerInflation() = DealerInflations.ToTable().Select(COL_NAME_RISK_TYPE & "=" & "'" & Me.RiskType & "'")

    '    If dealerInflation.Length > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

#End Region

#Region "Invoice Region Tax Search Dataset"
    Public Class InvoiceRegionTaxDV
        Inherits DataView

        Public Const COL_INVOICE_REGION_TAX_ID As String = "INVOICE_REGION_TAX_ID"
        Public Const COL_INVOICE_TRANS_ID As String = "INVOICE_TRANS_ID"
        'Public Const COL_REGION As String = "REGION"
        Public Const COL_REGION_ID As String = "REGION_ID"
        Public Const COL_TAX_AMOUNT As String = "TAX_AMOUNT"
        Public Const COL_REGION_DESCRIPTION As String = "REGION_DESCRIPTION"
        Public Const COL_INVOICE_TYPE As String = "TAX_TYPE_XCD"
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Grid Data Related"
    Public Shared Function GetEmptyList(ByVal dv As DataView) As System.Data.DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(InvoiceRegionTaxDV.COL_INVOICE_REGION_TAX_ID) = Guid.NewGuid.ToByteArray
            row(InvoiceRegionTaxDV.COL_INVOICE_TRANS_ID) = Guid.NewGuid.ToByteArray
            row.Item(InvoiceRegionTaxDV.COL_REGION_ID) = String.Empty
            row(InvoiceRegionTaxDV.COL_TAX_AMOUNT) = 0D
            row(InvoiceRegionTaxDV.COL_REGION_DESCRIPTION) = String.Empty
            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As InvoiceRegionTaxDV, ByVal NewBO As InvoiceRegionTax)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        'If NewBO.IsNew Then
        Dim row As DataRow
        If dv Is Nothing Then
            Dim guidTemp As New Guid
            blnEmptyTbl = True
            dt = New DataTable
            dt.Columns.Add(InvoiceRegionTaxDV.COL_INVOICE_REGION_TAX_ID, guidTemp.ToByteArray.GetType)
            dt.Columns.Add(InvoiceRegionTaxDV.COL_INVOICE_TRANS_ID, guidTemp.ToByteArray.GetType)
            dt.Columns.Add(InvoiceRegionTaxDV.COL_REGION_ID, guidTemp.ToByteArray.GetType)
            'dt.Columns.Add(InvoiceRegionTaxDV.COL_REGION, GetType(String))
            dt.Columns.Add(InvoiceRegionTaxDV.COL_TAX_AMOUNT, GetType(String))
            dt.Columns.Add(InvoiceRegionTaxDV.COL_INVOICE_TYPE, GetType(String))
            dt.Columns.Add(InvoiceRegionTaxDV.COL_REGION_DESCRIPTION, GetType(String))

        Else
            dt = dv.Table
        End If
        If NewBO.IsNew Then
            row = dt.NewRow
            row(InvoiceRegionTaxDV.COL_INVOICE_REGION_TAX_ID) = NewBO.Id.ToByteArray
            row(InvoiceRegionTaxDV.COL_REGION_ID) = NewBO.RegionId.ToByteArray
            dt.Rows.Add(row)

            If blnEmptyTbl Then dv = New InvoiceRegionTaxDV(dt)
            dv.Sort = COL_NAME_TAX_AMOUNT & " DESC"

        End If
    End Sub

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValueMandatoryIIBBtaxAmount
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.VALUE_REQUIRED_IIBB_TAX_AMOUNT)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As InvoiceRegionTax = CType(objectToValidate, InvoiceRegionTax)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
#End Region


End Class
