'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/17/2007)  ********************
Imports System.Globalization
Imports System.Collections.Generic

Public Class RegionTax
    Inherits BusinessObjectBase

#Region "Constants"
    
#End Region

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

    'New with DataRow
    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RegionTaxDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New RegionTaxDAL
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

            'load existing detail records
            Dim detailDAL As New RegionTaxDetailDAL, detailBO As New RegionTaxDetail
            detailDAL.LoadList(Me.Id, Me.Dataset)

            Dim detailRow As DataRow
            If _detailRecords Is Nothing Then _detailRecords = New List(Of RegionTaxDetail)

            For Each detailRow In Me.Dataset.Tables(RegionTaxDetailDAL.TABLE_NAME).Rows
                detailBO = New RegionTaxDetail(detailRow)
                _detailRecords.Add(detailBO)
            Next

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private _detailRecords As List(Of RegionTaxDetail) = New List(Of RegionTaxDetail)
#End Region

#Region "Properties"
    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(RegionTaxDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RegionTaxDAL.COL_NAME_REGION_TAX_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(RegionTaxDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RegionTaxDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RegionTaxDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property TaxTypeId() As Guid
        Get
            CheckDeleted()
            If Row(RegionTaxDAL.COL_NAME_TAX_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RegionTaxDAL.COL_NAME_TAX_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RegionTaxDAL.COL_NAME_TAX_TYPE_ID, Value)
        End Set
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CountryTaxDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidEffectiveDate(""), ValidNewEffectiveDate("")> _
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(RegionTaxDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(RegionTaxDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(RegionTaxDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidLastEntryForDelete("")> _
    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(RegionTaxDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(RegionTaxDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(RegionTaxDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(RegionTaxDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionTaxDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegionTaxDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ProductTaxTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_PRODUCT_TAX_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_PRODUCT_TAX_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CountryTaxDAL.COL_NAME_PRODUCT_TAX_TYPE_ID, Value)
        End Set
    End Property

    Public Property RegionTaxDetail(ByVal taxBucket As Long) As RegionTaxDetail
        Get
            CheckDeleted()

            Dim oRGD As RegionTaxDetail
            For Each oRGD In _detailRecords
                If oRGD.TaxBucket = taxBucket Then
                    Return oRGD
                End If
            Next
            Return Nothing
        End Get

        Set(ByVal value As RegionTaxDetail)
            CheckDeleted()

            Dim oRGD As RegionTaxDetail, blnExisting As Boolean = False
            For Each oRGD In _detailRecords
                If oRGD.TaxBucket = taxBucket Then
                    blnExisting = True
                    Exit For
                End If
            Next

            If Not value Is Nothing Then
                If Not blnExisting Then 'create a new one
                    oRGD = New RegionTaxDetail(Me.Dataset)
                    _detailRecords.Add(oRGD)

                    oRGD.RegionTaxId = Id
                    oRGD.TaxBucket = taxBucket
                End If
                oRGD.Percent = value.Percent
                oRGD.NonTaxable = value.NonTaxable
                oRGD.MinimumTax = value.MinimumTax
                oRGD.GlAccountNumber = value.GlAccountNumber
            End If
        End Set
    End Property

    Public ReadOnly Property RegionTaxDetailList() As List(Of RegionTaxDetail)
        Get
            Return _detailRecords
        End Get
    End Property
#End Region

#Region "Public Members"

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsFamilyDirty()
        End Get
    End Property

    Public Overrides Sub Save()
        Try
            MyBase.Save()

            'validate region tax detail BO
            Dim oRTD As RegionTaxDetail
            For Each oRTD In RegionTaxDetailList
                If oRTD.IsDirty AndAlso (Not Me.IsDeleted) Then oRTD.Validate()
            Next

            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RegionTaxDAL
                dal.UpdateFamily(Me.Dataset)
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

    Public Overrides Sub DeleteChildren()
        'delete the detail children first
        Dim oRGD As RegionTaxDetail
        For Each oRGD In _detailRecords
            oRGD.Delete()
        Next

        MyBase.DeleteChildren()
    End Sub

    Public Sub SetEffectiveExpirationDates()

        Dim myCal As Calendar = CultureInfo.InvariantCulture.Calendar
        Dim MinEffDate As Date, MaxExpDate As Date, RecCnt As Integer
        Me.GetMinEffDateAndMaxExpDate(MinEffDate, MaxExpDate, RecCnt)

        Dim NewEffectivedate As Date
        Dim NewExpirationDate As Date

        If RecCnt = 0 Then 'no existing record, start date default to today
            NewEffectivedate = Date.Now.ToLongDateString
        Else 'new effective date will be last expiration date + 1 day
            NewEffectivedate = MaxExpDate.AddDays(1).ToLongDateString
        End If

        NewExpirationDate = myCal.AddYears(myCal.AddDays(NewEffectivedate, -1), 1)

        Me.EffectiveDate = NewEffectivedate
        Me.ExpirationDate = NewExpirationDate
    End Sub

    Public Sub Copy(ByVal original As RegionTax)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing region tax")
        End If
        'Copy content
        CopyProperties(original)
    End Sub

    Public Sub Clone(ByVal original As RegionTax)
        'Copy include unique key
        CopyProperties(original, True)
    End Sub

    Public Sub DeleteAndSave()
        Me.CheckDeleted()
        Me.BeginEdit()
        Try
            Me.Delete()
            Me.Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Me.cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "Private Members"

    Private Sub CopyProperties(ByVal original As RegionTax, Optional ByVal KeyIncluded As Boolean = False)
        Dim oRTD As RegionTaxDetail
        With original
            If KeyIncluded Then Me.SetValue(RegionTaxDAL.COL_NAME_REGION_ID, .Id)
            Me.RegionId = .RegionId
            Me.TaxTypeId = .TaxTypeId
            Me.EffectiveDate = .EffectiveDate
            Me.ExpirationDate = .ExpirationDate
            Me.Description = .Description
            Me.ProductTaxTypeId = .ProductTaxTypeId
            Me.DealerId = .DealerId
            Me._detailRecords.Clear()
            For Each oRTD In .RegionTaxDetailList
                Me.RegionTaxDetail(oRTD.TaxBucket) = oRTD
            Next
        End With
    End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal RegionID As Guid, ByVal TaxTypeId As Guid, _
                                   ByVal oProductTaxTypeId As Guid, ByVal DealerId As Guid) As System.Data.DataView
        Try
            Dim dal As New RegionTaxDAL
            Return New System.Data.DataView(dal.LoadList(RegionID, TaxTypeId, oProductTaxTypeId, DealerId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Sub GetMinEffDateAndMaxExpDate(ByRef MinEffDate As Date, ByRef MaxExpDate As Date, ByRef RcdCount As Integer)
        Try
            Dim dal As New RegionTaxDAL
            dal.LoadMinEffDateMaxExpDate(MinEffDate, MaxExpDate, RcdCount, Me.RegionId, Me.TaxTypeId, _
                                         Me.ProductTaxTypeId, Me.DealerId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "Custom validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidLastEntryForDelete
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, CountryTax.LAST_ENTRY_ONLY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As RegionTax = CType(objectToValidate, RegionTax)

            If obj.IsDeleted Then 'when deleting
                Dim minEffdt As Date, maxExpdt As Date, recCnt As Integer
                obj.GetMinEffDateAndMaxExpDate(minEffdt, maxExpdt, recCnt)

                If (obj.ExpirationDate.Value <> maxExpdt) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidNewEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, CountryTax.EFFECTIVE_DATE_NOT_GREATER)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As RegionTax = CType(objectToValidate, RegionTax)

            If obj.IsNew Then ' when add new 
                Dim minEffdt As Date, maxExpdt As Date, recCnt As Integer
                obj.GetMinEffDateAndMaxExpDate(minEffdt, maxExpdt, recCnt)

                If (maxExpdt <> Date.Parse(CountryTaxDAL.INFINITE_DATE_STR, System.Globalization.CultureInfo.InvariantCulture)) _
                    AndAlso (obj.EffectiveDate <> maxExpdt.AddDays(1)) Then
                    Return False
                End If
            End If

            Return True
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, CountryTax.INVALID_EFFECTIVE_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As RegionTax = CType(objectToValidate, RegionTax)

            If Not obj.IsDeleted Then 'Edit or add new
                If (obj.EffectiveDate.Value >= obj.ExpirationDate.Value) Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

#End Region

End Class


