﻿Imports System.Collections.Generic
Public Class BenefitProductCode
    Inherits BusinessObjectBase
    Implements IAttributable

#Region "Constructor"

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


    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    Public Sub New(ByVal dealerId As Guid, ByVal productCode As String, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(dealerId, productCode)
    End Sub
    Protected Sub Load()
        Try
            Dim dal As New BenefitProductCodeDAL
            If Me.Dataset.Tables.IndexOf(BenefitProductCodeDAL.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(BenefitProductCodeDAL.TABLE_NAME).NewRow
            Me.Dataset.Tables(BenefitProductCodeDAL.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(BenefitProductCodeDAL.TABLE_KEY_NAME, Guid.NewGuid)
            SetValue(BenefitProductCodeDAL.COL_NAME_EFFECTIVE_DATE, Date.Today)
            SetValue(BenefitProductCodeDAL.COL_NAME_EXPIRATION_DATE, Date.Today)
            SetValue(BenefitProductCodeDAL.COL_NAME_CREATED_DATE, Date.Now)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Protected Sub Load(ByVal dealerId As Guid, ByVal productCode As String)
        Try
            Dim dal As New BenefitProductCodeDAL

            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(Id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByDealerProduct(Me.Dataset, dealerId, productCode)
                Me.Row = Me.FindRow(dealerId, dal.COL_NAME_DEALER_ID, productCode, dal.COL_NAME_BEN_PRODUCT_CODE, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If

            SetValue(BenefitProductCodeDAL.COL_NAME_EFFECTIVE_DATE, System.DateTime.Today)
            SetValue(BenefitProductCodeDAL.COL_NAME_EFFECTIVE_DATE, EXPIRATION_DEFAULT)
            SetValue(BenefitProductCodeDAL.COL_NAME_CREATED_DATE, System.DateTime.Now)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New BenefitProductCodeDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(BenefitProductCodeDAL.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(BenefitProductCodeDAL.TABLE_NAME) >= 0 Then
                Me.Row = FindRow(id, BenefitProductCodeDAL.TABLE_KEY_NAME, Me.Dataset.Tables(BenefitProductCodeDAL.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = FindRow(id, BenefitProductCodeDAL.TABLE_KEY_NAME, Me.Dataset.Tables(BenefitProductCodeDAL.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If

            Me._originalEffectiveDate = Me.EffectiveDate
            Me._originalExpirationDate = Me.ExpirationDate

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

#Region "Properties Name"
    Public Class PropertiesName
        Public Const Id As String = "Id"
        Public Const DealerId As String = "DealerId"
        Public Const VendorId As String = "VendorId"
        Public Const BenefitProductCode As String = "BenefitProductCode"
        Public Const Description As String = "Description"
        Public Const CurrencyIsoCode As String = "CurrencyIsoCode"
        Public Const UnitOfMeasureXcd As String = "UnitOfMeasureXcd"
        Public Const NetPrice As String = "NetPrice"
        Public Const TaxTypeXCD As String = "TaxTypeXCD"
        Public Const DurationInMonth As String = "DurationInMonth"
        Public Const EffectiveDate As String = "EffectiveDate"
        Public Const ExpirationDate As String = "ExpirationDate"
        Public Const VendorBillablePartNum As String = "VendorBillablePartNum"
        Public Const DaysToExpireAfterEndDay As String = "DaysToExpireAfterEndDay"
    End Class
#End Region

#Region "Properties"
    Private _originalEffectiveDate As DateType
    Private _originalExpirationDate As DateType
    Private _AttributeValueList As AttributeValueList(Of IAttributable)

    Public ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable) Implements IAttributable.AttributeValues
        Get
            If (_AttributeValueList Is Nothing) Then
                _AttributeValueList = New AttributeValueList(Of IAttributable)(Me.Dataset, Me)
            End If
            Return _AttributeValueList
        End Get
    End Property

    <ValueMandatory("")>
    Public ReadOnly Property Id() As Guid Implements IAttributable.Id
        Get
            If Row(BenefitProductCodeDAL.COL_NAME_BEN_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BenefitProductCodeDAL.COL_NAME_BEN_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
    End Property


    <ValueMandatory("")>
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BenefitProductCodeDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get

        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_DEALER_ID, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property VendorId As Guid
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_VENDOR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BenefitProductCodeDAL.COL_NAME_VENDOR_ID), Byte()))
            End If
        End Get

        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_VENDOR_ID, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)>
    Public Property BenefitProductCode As String
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_BEN_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(BenefitProductCodeDAL.COL_NAME_BEN_PRODUCT_CODE)
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_BEN_PRODUCT_CODE, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(BenefitProductCodeSearchDV.COL_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(BenefitProductCodeSearchDV.COL_DESCRIPTION)
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeSearchDV.COL_DESCRIPTION, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10)>
    Public Property CurrencyIsoCode As String
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_CURRENCY_ISO_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(BenefitProductCodeDAL.COL_NAME_CURRENCY_ISO_CODE)
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_CURRENCY_ISO_CODE, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property UnitOfMeasureXcd As String
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_PRICE_UOM) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(BenefitProductCodeDAL.COL_NAME_PRICE_UOM)
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_PRICE_UOM, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property NetPrice As DecimalType
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_NET_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(BenefitProductCodeDAL.COL_NAME_NET_PRICE), Decimal))
            End If
        End Get

        Set(ByVal value As DecimalType)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_NET_PRICE, value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property TaxTypeXCD As String
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_TAX_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(BenefitProductCodeDAL.COL_NAME_TAX_TYPE_XCD)
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_TAX_TYPE_XCD, value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=9999)>
    Public Property DurationInMonth As LongType
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_DURATIONINMONTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BenefitProductCodeDAL.COL_NAME_DURATIONINMONTH), Long))
            End If
        End Get

        Set(ByVal value As LongType)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_DURATIONINMONTH, value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(BenefitProductCodeDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get

        Set(ByVal value As DateType)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_EFFECTIVE_DATE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidExpirationDate("")>
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(BenefitProductCodeDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get

        Set(ByVal value As DateType)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_EXPIRATION_DATE, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property VendorBillablePartNum As String
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_VENDOR_BILLABLE_PART_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(BenefitProductCodeDAL.COL_NAME_VENDOR_BILLABLE_PART_NUM)
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_VENDOR_BILLABLE_PART_NUM, value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=9999)>
    Public Property DaysToExpireAfterEndDay As LongType
        Get
            CheckDeleted()
            If Row(BenefitProductCodeDAL.COL_NAME_DAYSTOEXPIREAFTERENDDAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BenefitProductCodeDAL.COL_NAME_DAYSTOEXPIREAFTERENDDAY), Long))
            End If
        End Get

        Set(ByVal value As LongType)
            CheckDeleted()
            Me.SetValue(BenefitProductCodeDAL.COL_NAME_DAYSTOEXPIREAFTERENDDAY, value)
        End Set
    End Property


    Public ReadOnly Property TableName As String Implements IAttributable.TableName
        Get
            Return BenefitProductCodeDAL.TABLE_NAME
        End Get
    End Property
#End Region
#Region "Constants"
    Public ReadOnly EXPIRATION_DEFAULT As New Date(2499, 12, 31, 23, 59, 59)
#End Region
#Region "Public Members"

    Public Sub SaveWithLog()
        Try
            Dim refreshId As Guid
            MyBase.Save()
            If Me._isDSCreator AndAlso (Me.IsDirty OrElse Me.IsFamilyDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BenefitProductCodeDAL
                Dim createLogRecord As Boolean = Not (Me.IsNew Or Me._originalEffectiveDate Is Nothing Or Me._originalExpirationDate Is Nothing)

                If createLogRecord Then
                    ' the record is current
                    If Not (Me._originalEffectiveDate.Value <= DateTime.Today AndAlso Me._originalExpirationDate.Value >= DateTime.Today) Then
                        createLogRecord = False
                    End If
                End If

                If Not createLogRecord Then
                    dal.UpdateFamily(Me.Dataset)
                    refreshId = Me.Id
                Else

                    Dim currentObj As New BenefitProductCode(Me.Id)
                    currentObj.ExpirationDate = Me.EffectiveDate.Value.AddDays(-1)

                    Dim newObj As New BenefitProductCode()
                    With newObj
                        .DealerId = Me.DealerId
                        .Description = Me.Description
                        .BenefitProductCode = Me.BenefitProductCode
                        .VendorId = Me.VendorId
                        .CurrencyIsoCode = Me.CurrencyIsoCode
                        .UnitOfMeasureXcd = Me.UnitOfMeasureXcd
                        .DurationInMonth = Me.DurationInMonth
                        .DaysToExpireAfterEndDay = Me.DaysToExpireAfterEndDay
                        .EffectiveDate = Me.EffectiveDate
                        .ExpirationDate = Me.ExpirationDate
                        .NetPrice = Me.NetPrice
                        .TaxTypeXCD = Me.TaxTypeXCD
                        .VendorBillablePartNum = Me.VendorBillablePartNum
                    End With

                    dal.UpdateFamily(currentObj.Dataset, newObj.Dataset)

                    ' swap the dataset table
                    refreshId = newObj.Id

                End If

                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached AndAlso Me.Row.RowState <> DataRowState.Deleted Then
                    Dim objId As Guid = refreshId
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso (Me.IsDirty OrElse Me.IsFamilyDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BenefitProductCodeDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached AndAlso Me.Row.RowState <> DataRowState.Deleted Then
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

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property


#End Region
#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal compIds As ArrayList,
                                   ByVal dealerId As Guid,
                                   ByVal benefitProductCodeMask As String,
                                   ByVal LanguageId As Guid) As BenefitProductCodeSearchDV
        Try
            Dim dal As New BenefitProductCodeDAL
            Return New BenefitProductCodeSearchDV(dal.LoadList(compIds, dealerId, benefitProductCodeMask, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetList(ByVal dealerId As Guid,
                                   ByVal benefitProductCodeMask As String,
                                   ByVal vendorBillablePartNum As String,
                                   ByVal ben_product_code_id As Guid) As BenefitProductCodeSearchDV
        Try
            Dim dal As New BenefitProductCodeDAL
            Return New BenefitProductCodeSearchDV(dal.LoadList(dealerId, benefitProductCodeMask, vendorBillablePartNum, ben_product_code_id).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "Children Related"

#End Region
#Region "Public Methods"

#End Region
#Region "Custom Validation"

#End Region

#Region "BenefitProductCodeSearchDV"
    Public Class BenefitProductCodeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_BENEFIT_PRODUCT_CODE_ID As String = "ben_product_code_id"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_BENEFIT_PRODUCT_CODE As String = "ben_product_code"
        Public Const COL_RISK_GROUP As String = "risk_group"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_COMPANY_CODE As String = "company_code"
        Public Const COL_VENDOR_NAME As String = "vendor_name"
        Public Const COL_EFFECTIVE_DATE As String = "effective_date"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_LAYOUT As String = "layout"


#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region


#Region "Validation"

    Private Const BENEFIT_PRODUCT_CODE_FORM001 As String = "BENEFIT_PRODUCT_CODE_FORM001" ' Expiration date must be greater than or equal to Effective date
    Private Const BENEFIT_PRODUCT_CODE_FORM002 As String = "BENEFIT_PRODUCT_CODE_FORM002" ' Check if the expiration is valid and do not overlap with another range

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidExpirationDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, BENEFIT_PRODUCT_CODE_FORM001)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BenefitProductCode = CType(objectToValidate, BenefitProductCode)

            Dim bValid As Boolean = True

            If Not obj.ExpirationDate Is Nothing And Not obj.EffectiveDate Is Nothing Then
                If Convert.ToDateTime(obj.EffectiveDate.Value) > Convert.ToDateTime(obj.ExpirationDate.Value) Then
                    Me.Message = BENEFIT_PRODUCT_CODE_FORM001
                    bValid = False
                ElseIf ValidateExpirationRange(obj.EffectiveDate, obj.ExpirationDate, obj) = False Then
                    Me.Message = BENEFIT_PRODUCT_CODE_FORM002
                    bValid = False
                End If
            End If

            Return bValid

        End Function

        Private Function ValidateExpirationRange(ByVal NewEffectiveDate As Assurant.Common.Types.DateType, ByVal NewExpirationDate As Assurant.Common.Types.DateType, ByVal oBenefitProductCode As BenefitProductCode) As Boolean

            Dim bValid As Boolean = True
            Dim bChangeRec As Boolean = False
            Dim EffectiveDate, ExpirationDate As Date


            Dim dv As DataView = GetList(oBenefitProductCode.DealerId, oBenefitProductCode.BenefitProductCode, oBenefitProductCode.VendorBillablePartNum, oBenefitProductCode.Id)
            Dim dvRows As DataRowCollection = dv.Table.Rows
            Dim dataRow As DataRow

            'If dvRows.Count = 1 Then
            '    'only one record for the combination
            '    bValid = True
            'Else
            For Each dataRow In dvRows
                EffectiveDate = dataRow(BenefitProductCodeDAL.COL_NAME_EFFECTIVE_DATE)
                ExpirationDate = dataRow(BenefitProductCodeDAL.COL_NAME_EXPIRATION_DATE)

                'If NewEffectiveDate.Value = EffectiveDate Then
                '    bChangeRec = True
                'Else
                '    If bChangeRec = True And NewExpirationDate.Value >= EffectiveDate Then
                '        bValid = False
                '        Exit For
                '    End If
                'End If
                If (NewEffectiveDate.Value >= EffectiveDate AndAlso NewEffectiveDate.Value <= ExpirationDate) Or
                        (NewExpirationDate.Value >= EffectiveDate AndAlso NewExpirationDate.Value <= ExpirationDate) Then
                    bValid = False
                    Exit For
                End If
            Next
            'End If
            Return bValid
        End Function
    End Class
#End Region
End Class

