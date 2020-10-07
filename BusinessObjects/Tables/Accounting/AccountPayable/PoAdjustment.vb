Imports Assurant.ElitaPlus.DALObjects.Tables.Accounting.AccountPayable

Namespace Tables.Accounting.AccountPayable
    Public Class PoAdjustment
        Inherits BusinessObjectBase

#Region "Constants"

        Public Const PO_LINE_ID_COL As String = "po_line_id"
        Public Const VENDOR_COL As String = "Vendor"
        Public Const PO_NUMBER_COL As String = "PO_Number"
        Public Const LINE_NUMBER_COL As String = "line_number"
        Public Const ITEM_CODE_COL As String = "Item_Code"
        Public Const DESCRIPTION_COL As String = "Description"
        Public Const QUANTITY_COL As String = "Quantity"
        Public Const UNIT_PRICE_COL As String = "Unit_Price"
        Public Const EXTENDED_PRICE_COL As String = "Extende_Price" 
      
    
#End Region

#Region " Constructors "

        Public Sub New()

            MyBase.New()
            Dataset = New DataSet
            Load()

        End Sub
        Public Sub New(ByVal vendorCode As String , ByVal poNumber As string, ByVal poLineId As Guid,ByVal companyId As Guid)

            MyBase.New()
            Dataset = New DataSet
            Load(vendorCode,poNumber,poLineId,companyId)

        End Sub
        Public Sub New(ByVal vendorCode As String , ByVal poNumber As string, ByVal poLineId As Guid, ByVal companyId As Guid, ByVal familyDs As DataSet)
            MyBase.New(False)
            Dataset = familyDS
            Load(vendorCode,poNumber,poLineId,companyId)
        End Sub
        
        Public Sub New(ByVal familyDs As DataSet)
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
            Dim dal As PoAdjustmentDAL = New PoAdjustmentDAL
            Dim ds As DataSet = New DataSet

            Dataset = dal.LoadSchema(ds)

            Dim newRow As DataRow = Dataset.Tables(PoAdjustmentDAL.PO_LINES_TABLE_NAME).NewRow
            Dataset.Tables(PoAdjustmentDAL.PO_LINES_TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(PoAdjustmentDAL.PO_LINE_ID_COL, Guid.NewGuid)
        End Sub

      Protected Sub Load(ByVal vendorCode As String , ByVal apPoNumber As string, ByVal apPoLineId As Guid,ByVal poCompany As Guid)

            Try
                Dim dal As New PoAdjustmentDAL
                If _isDSCreator Then
                    If Not Row Is Nothing Then
                        Dataset.Tables(PoAdjustmentDAL.PO_LINES_TABLE_NAME).Rows.Remove(Row)
                    End If
                End If
                Row = Nothing
                If Not Dataset Is Nothing Then
                    If Dataset.Tables.IndexOf(PoAdjustmentDAL.PO_LINES_TABLE_NAME) >= 0 Then
                        Row = FindRow(apPoLineId.ToByteArray(), PoAdjustmentDAL.PO_LINE_ID_COL, Dataset.Tables(PoAdjustmentDAL.PO_LINES_TABLE_NAME))
                    End If
                End If
                If Row Is Nothing Then 
                    dal.Load(Dataset, vendorCode,apPoNumber,poCompany)
                    Row = FindRow(apPoLineId, PoAdjustmentDAL.PO_LINE_ID_COL, Dataset.Tables(PoAdjustmentDAL.PO_LINES_TABLE_NAME))
                End If
                If Row Is Nothing Then
                    Throw New DataNotFoundException
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try

        End Sub



#End Region

#Region "BO Properties"


        <ValueMandatory("")>
        Public Property PoLineId() As Guid
            Get
                If Row(PoAdjustmentDAL.PO_LINE_ID_COL) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(Row(PoAdjustmentDAL.PO_LINE_ID_COL), Byte()))
            End Get
            Set(ByVal value As Guid)
               SetValue(PoAdjustmentDAL.PO_LINE_ID_COL, value)
            End Set
        End Property

        <ValidStringLength("", Max:=250)>
        Public Property Description() As String
            Get
                If Row(PoAdjustmentDAL.DESCRIPTION_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.DESCRIPTION_COL)
            End Get
            Set(ByVal value As String)
                SetValue(PoAdjustmentDAL.DESCRIPTION_COL, value)
            End Set
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=10)>
        Public Property Vendor() As String
            Get
                If Row(PoAdjustmentDAL.VENDOR_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.VENDOR_COL)
            End Get
            Set(ByVal value As String)
                SetValue(PoAdjustmentDAL.VENDOR_COL, value)
            End Set
        End Property
        <ValueMandatory(""), ValidStringLength("", Max:=100)>
        Public Property PoNumber() As String
            Get
                If Row(PoAdjustmentDAL.PO_NUMBER_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.PO_NUMBER_COL)
            End Get
            Set(ByVal value As String)
                SetValue(PoAdjustmentDAL.PO_NUMBER_COL, value)
            End Set
        End Property
        <ValueMandatory(""),ValidStringLength("", Max:=100)>
        Public Property LineNumber() As String
            Get
                If Row(PoAdjustmentDAL.LINE_NUMBER_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.LINE_NUMBER_COL)
            End Get
            Set(ByVal value As String)
                SetValue(PoAdjustmentDAL.LINE_NUMBER_COL, value)
            End Set
        End Property
        <ValueMandatory(""),ValidStringLength("", Max:=100)>
        Public Property ItemCode() As String
            Get
                If Row(PoAdjustmentDAL.ITEM_CODE_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.ITEM_CODE_COL)
            End Get
            Set(ByVal value As String)
                SetValue(PoAdjustmentDAL.ITEM_CODE_COL, value)
            End Set
        End Property
      Public Property Quantity() As Decimal
            Get
                If Row(PoAdjustmentDAL.QUANTITY_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.QUANTITY_COL)
            End Get
            Set(ByVal value As Decimal)
                SetValue(PoAdjustmentDAL.QUANTITY_COL, value)
            End Set
        End Property
        Public Property UnitPrice() As Decimal
            Get
                If Row(PoAdjustmentDAL.UNIT_PRICE_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.UNIT_PRICE_COL)
            End Get
            Set(ByVal value As Decimal)
                SetValue(PoAdjustmentDAL.UNIT_PRICE_COL, value)
            End Set
        End Property
        Public Property ExtendedPrice() As Decimal
            Get
                If Row(PoAdjustmentDAL.EXTENDED_PRICE_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.EXTENDED_PRICE_COL)
            End Get
            Set(ByVal value As Decimal)
                SetValue(PoAdjustmentDAL.EXTENDED_PRICE_COL, value)
            End Set
        End Property
        Public Property CompanyId() As Guid
            Get
                If Row(PoAdjustmentDAL.COMPANY_ID_COL) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(Row(PoAdjustmentDAL.COMPANY_ID_COL), Byte()))
            End Get
            Set(ByVal value As Guid)
                SetValue(PoAdjustmentDAL.COMPANY_ID_COL, value)
            End Set
        End Property
        <ValidStringLength("", Max:=30)>
        Public Property ModifiedBy() As String
            Get
                If Row(PoAdjustmentDAL.MODIFIED_BY_COL) Is DBNull.Value Then Return Nothing
                Return Row(PoAdjustmentDAL.MODIFIED_BY_COL)
            End Get
            Set(ByVal value As String)
                SetValue(PoAdjustmentDAL.MODIFIED_BY_COL, value)
            End Set
        End Property

#End Region

#Region "Public Methods"
        Public Overrides Sub Save()
            Try
                MyBase.Save()
                If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                    Dim dal As New PoAdjustmentDAL
                    dal.UpdateApLine(PoNumber,PoLineId,CompanyId,Quantity,ModifiedBy)
                    'Reload the Data from the DB
                    If Row.RowState <> DataRowState.Detached Then
                        Load(Vendor,PoNumber,PoLineId,CompanyId)
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Try
        End Sub

       Public Function GetApPoLines( ByVal vendorCode As String , ByVal apPoNumber As string,ByVal companyGroupId As Guid) As DataView
           Try
               Dim dal As New PoAdjustmentDAL
               Dim ds As Dataset = new DataSet

               ds = dal.Load(ds, vendorCode, apPoNumber, CompanyGroupId)
               Return ds.Tables(PoAdjustmentDAL.PO_LINES_TABLE_NAME).DefaultView

           Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
               Throw New DataBaseAccessException(ex.ErrorType, ex)
           End Try
       End Function


#End Region

    End Class
End Namespace