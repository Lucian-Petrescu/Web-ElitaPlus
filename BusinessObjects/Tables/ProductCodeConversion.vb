'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/22/2004)  ********************

Public Class ProductCodeConversion
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

    Protected Sub Load()
        Try
            Dim dal As New ProductCodeConversionDAL
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
            Dim dal As New ProductCodeConversionDAL
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

#Region "Variables"

    '  Private moProductConversionData As ProductConversionData

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ProductCodeConversionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ProductCodeConversionDAL.COL_NAME_PRODUCT_CONVERSION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeConversionDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property ExternalProdCode() As String
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_EXTERNAL_PROD_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeConversionDAL.COL_NAME_EXTERNAL_PROD_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_EXTERNAL_PROD_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProductCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeConversionDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property

    'Public ReadOnly Property TheProductConversionData() As ProductConversionData
    '    Get
    '        If moProductConversionData Is Nothing Then
    '            moProductConversionData = New ProductConversionData
    '        End If
    '        Return moProductConversionData
    '    End Get
    'End Property

    Public Shared ReadOnly Property TheProductConversionData() As ProductConversionData
        Get
            Return New ProductConversionData
        End Get
    End Property

    <ValidNumericRange("", MAX:=999)> _
    Public Property CertificateDuration() As Short
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_CERTIFICATE_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeConversionDAL.COL_NAME_CERTIFICATE_DURATION), Short)
            End If
        End Get
        Set(ByVal Value As Short)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_CERTIFICATE_DURATION, Value)
        End Set
    End Property

    <ValidNumericRange("", MAX:=99)> _
    Public Property ManufacturerWarranty() As Short
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_MANUFACTURER_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeConversionDAL.COL_NAME_MANUFACTURER_WARRANTY), Short)
            End If
        End Get
        Set(ByVal Value As Short)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_MANUFACTURER_WARRANTY, Value)
        End Set
    End Property

    <ValidNumericRange("", MAX:=NEW_MAX_DOUBLE)> _
   Public Property GrossAmount() As Double
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_GROSS_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeConversionDAL.COL_NAME_GROSS_AMOUNT), Double)
            End If
        End Get
        Set(ByVal Value As Double)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_GROSS_AMOUNT, Value)
        End Set
    End Property

    <ValidStringLength("", MAX:=255), Valid_DealerProdCodeMfgCombination(""), Valid_DealerProdCodeCombination("")> _
    Public Property Manufacturer() As String
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeConversionDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeConversionDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)>
    Public Property SalesPrice() As Double
        Get
            CheckDeleted()
            If Row(ProductCodeConversionDAL.COL_NAME_SALES_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductCodeConversionDAL.COL_NAME_SALES_PRICE), Double)
            End If
        End Get
        Set(ByVal Value As Double)
            CheckDeleted()
            Me.SetValue(ProductCodeConversionDAL.COL_NAME_SALES_PRICE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductCodeConversionDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then Me.Load(Me.Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub Copy(ByVal original As ProductCodeConversion)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Product")
        End If
        'Copy myself
        Me.CopyFrom(original)

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function ProductConversionList(ByVal oData As Object) As DataView
        Try
            Dim oProductConversionData As ProductConversionData = CType(oData, ProductConversionData)
            Dim dal As New ProductCodeConversionDAL
            Dim ds As DataSet

            ds = dal.LoadList(oProductConversionData)
            '    Return ds.Tables(ProductCodeConversionDAL.DSNAME).DefaultView
            Return ds.Tables(ProductCodeConversionDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetExternalProdCodeListWithDesc(ByVal DealerID As Guid) As ExternalProdCodeWithDescDV
        Try
            Dim dal As New ProductCodeConversionDAL
            Dim ds As DataSet

            ds = dal.LoadListWithDescByDealer(DealerID)
            Return New ExternalProdCodeWithDescDV(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
 

#End Region

#Region "ExternalProdCodeWithDescDV"
    Public Class ExternalProdCodeWithDescDV
        Inherits DataView

        Public Const COL_PRODUCT_CODE_ID As String = "Product_code_id"
        Public Const COL_EXTERNATL_PRODUCT_CODE As String = "EXTERNAL_PROD_CODE"
        Public Const COL_PRODUCT_CODE As String = "PRODUCT_CODE"
        Public Const COL_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_ALL_PRODUCT_CODE As String = "All_PROD_CODE"
        Public Const COL_BUNDLED_ITEM As String = "BUNDLED_ITEM"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Valid_DealerProdCodeMfgCombination
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ERR_BO_INVALID_MFG_DEALER_EXTPRODCODE_COMBINATION_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCodeConversion = CType(objectToValidate, ProductCodeConversion)

            Dim dal As New ProductCodeConversionDAL
            Dim odealer As New Dealer(obj.DealerId)

            If LookupListNew.GetCodeFromId(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_PROD_CONV_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), odealer.ConvertProductCodeId) = "EXT" Then

                If dal.CheckForDealerProdCodeMfgCombination(obj.DealerId, obj.ExternalProdCode, obj.Manufacturer, obj.Id).Tables(0).Rows.Count = 1 Then
                    Return False
                Else
                    Return True
                End If
            End If

            Return True
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Valid_DealerProdCodeCombination
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ERR_BO_INVALID_DEALER_EXTPRODCODE_COMBINATION_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductCodeConversion = CType(objectToValidate, ProductCodeConversion)

            Dim dal As New ProductCodeConversionDAL
            Dim odealer As New Dealer(obj.DealerId)

            If (LookupListNew.GetCodeFromId(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_PROD_CONV_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), odealer.ConvertProductCodeId) = "EXT") Or
                LookupListNew.GetCodeFromId(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_PROD_CONV_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), odealer.ConvertProductCodeId) <> "EXT" Then

                'If LookupListNew.GetCodeFromId(LookupListNew.LK_USE_FULLFILE_PROCESS, odealer.UseFullFileProcessId) <> Codes.FULLFILE_NONE Then

                If dal.CheckForDealerProdCodeMfgCombination(obj.DealerId, obj.ExternalProdCode, obj.Manufacturer, obj.Id).Tables(0).Rows.Count = 1 Then
                    Return False
                Else
                    Return True
                End If
            End If

            Return True
        End Function

    End Class


#End Region

End Class


