'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/8/2009)  ********************

Public Class ClaimTax
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const TAX_TYPE_CLAIM As String = "7"
#End Region
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
            Dim dal As New ClaimTaxDAL
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
            Dim dal As New ClaimTaxDAL
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
            If row(ClaimTaxDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimTaxDAL.COL_NAME_CLAIM_TAX_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimInvoiceId As Guid
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_CLAIM_INVOICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimTaxDAL.COL_NAME_CLAIM_INVOICE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_CLAIM_INVOICE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DisbursementId As Guid
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_DISBURSEMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimTaxDAL.COL_NAME_DISBURSEMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_DISBURSEMENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TaxTypeId As Guid
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimTaxDAL.COL_NAME_TAX_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX_TYPE_ID, Value)
        End Set
    End Property



    Public Property Tax1Amount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimTaxDAL.COL_NAME_TAX1_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimTaxDAL.COL_NAME_TAX1_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX1_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Tax1Description As String
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX1_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimTaxDAL.COL_NAME_TAX1_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX1_DESCRIPTION, Value)
        End Set
    End Property



    Public Property Tax2Amount As DecimalType
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX2_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ClaimTaxDAL.COL_NAME_TAX2_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX2_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Tax2Description As String
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX2_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimTaxDAL.COL_NAME_TAX2_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX2_DESCRIPTION, Value)
        End Set
    End Property



    Public Property Tax3Amount As DecimalType
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX3_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ClaimTaxDAL.COL_NAME_TAX3_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX3_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Tax3Description As String
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX3_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimTaxDAL.COL_NAME_TAX3_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX3_DESCRIPTION, Value)
        End Set
    End Property



    Public Property Tax4Amount As DecimalType
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX4_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ClaimTaxDAL.COL_NAME_TAX4_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX4_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Tax4Description As String
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX4_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimTaxDAL.COL_NAME_TAX4_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX4_DESCRIPTION, Value)
        End Set
    End Property



    Public Property Tax5Amount As DecimalType
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX5_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ClaimTaxDAL.COL_NAME_TAX5_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX5_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Tax5Description As String
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX5_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimTaxDAL.COL_NAME_TAX5_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX5_DESCRIPTION, Value)
        End Set
    End Property



    Public Property Tax6Amount As DecimalType
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX6_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ClaimTaxDAL.COL_NAME_TAX6_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX6_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Tax6Description As String
        Get
            CheckDeleted()
            If row(ClaimTaxDAL.COL_NAME_TAX6_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimTaxDAL.COL_NAME_TAX6_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimTaxDAL.COL_NAME_TAX6_DESCRIPTION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimTaxDAL
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

    Public Shared Function GetClaimTaxList(claimInvoiceID As Guid) As ClaimTaxSearchDV

        Try
            Dim dal As New ClaimTaxDAL
            Return New ClaimTaxSearchDV(dal.LoadListByClaimInvoice(claimInvoiceID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimTaxCountry(claimID As Guid) As Guid
        Try
            Dim dal As New ClaimTaxDAL
            Dim ds As DataSet, guidCountryID As Guid, drResult As DataRow
            ds = dal.LoadClaimTaxCountry(claimID)
            If ds.Tables(0).Rows.Count = 1 Then
                drResult = ds.Tables(0).Rows(0)
                guidCountryID = New Guid(CType(drResult("COUNTRY_ID"), Byte()))
            Else
                guidCountryID = Guid.Empty
            End If
            Return guidCountryID
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region
#Region "ClaimTaxSearchDV"
    Public Class ClaimTaxSearchDV
        Inherits DataView

        Public Const COL_CLAIM_TAX_ID As String = ClaimTaxDAL.COL_NAME_CLAIM_TAX_ID
        Public Const COL_CLAIM_INVOICE_ID As String = ClaimTaxDAL.COL_NAME_CLAIM_INVOICE_ID
        Public Const COL_DISBURSEMENT_ID As String = ClaimTaxDAL.COL_NAME_DISBURSEMENT_ID
        Public Const COL_TAX_TYPE_ID As String = ClaimTaxDAL.COL_NAME_TAX_TYPE_ID
        Public Const COL_TAX1_AMOUNT As String = ClaimTaxDAL.COL_NAME_TAX1_AMOUNT
        Public Const COL_TAX1_DESCRIPTION As String = ClaimTaxDAL.COL_NAME_TAX1_DESCRIPTION
        Public Const COL_TAX2_AMOUNT As String = ClaimTaxDAL.COL_NAME_TAX2_AMOUNT
        Public Const COL_TAX2_DESCRIPTION As String = ClaimTaxDAL.COL_NAME_TAX2_DESCRIPTION
        Public Const COL_TAX3_AMOUNT As String = ClaimTaxDAL.COL_NAME_TAX3_AMOUNT
        Public Const COL_TAX3_DESCRIPTION As String = ClaimTaxDAL.COL_NAME_TAX3_DESCRIPTION
        Public Const COL_TAX4_AMOUNT As String = ClaimTaxDAL.COL_NAME_TAX4_AMOUNT
        Public Const COL_TAX4_DESCRIPTION As String = ClaimTaxDAL.COL_NAME_TAX4_DESCRIPTION
        Public Const COL_NAME_TAX5_AMOUNT As String = ClaimTaxDAL.COL_NAME_TAX5_AMOUNT
        Public Const COL_TAX5_DESCRIPTION As String = ClaimTaxDAL.COL_NAME_TAX5_DESCRIPTION
        Public Const COL_TAX6_AMOUNT As String = ClaimTaxDAL.COL_NAME_TAX6_AMOUNT
        Public Const COL_TAX6_DESCRIPTION As String = ClaimTaxDAL.COL_NAME_TAX6_DESCRIPTION

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ClaimInvoiceId(row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_INVOICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property TaxTypeId(row As DataRow) As String
            Get
                Return row(COL_TAX_TYPE_ID).ToString
            End Get
        End Property


        Public Shared ReadOnly Property Desc1(row As DataRow) As String
            Get
                Return row(COL_TAX1_DESCRIPTION).ToString
            End Get
        End Property
    End Class
#End Region
End Class



