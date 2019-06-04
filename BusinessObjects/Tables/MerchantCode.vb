'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/13/2010)  ********************

Public Class MerchantCode
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
            Dim dal As New MerchantCodeDAL
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
            Dim dal As New MerchantCodeDAL
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

#Region "MerchantCodeSearchDV"
    Public Class MerchantCodeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_MERCHANT_CODE_ID As String = "merchant_code_id"
        Public Const COL_COMPANY_CREDIT_CARD_ID As String = "company_credit_card_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_COMPANY_CREDIT_CARD_TYPE As String = "company_credit_card_type"
        Public Const COL_MERCHANT_CODE As String = "merchant_code"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As MerchantCodeSearchDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(MerchantCodeSearchDV.COL_MERCHANT_CODE_ID) = (New Guid()).ToByteArray
            row(MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_ID) = Guid.Empty.ToByteArray
            row(MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE) = ""
            row(MerchantCodeSearchDV.COL_MERCHANT_CODE) = DBNull.Value
            dt.Rows.Add(row)
            Return New MerchantCodeSearchDV(dt)
        End Function
    End Class
#End Region

    Private _creditCardFormatId As Guid = Guid.Empty

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(MerchantCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(MerchantCodeDAL.COL_NAME_MERCHANT_CODE_ID), Byte()))
            End If
        End Get
    End Property

    '<ValueMandatory(""), ValidateDuplicateCreditCardType("")> _
    <ValueMandatory("")> _
    Public Property CompanyCreditCardId() As Guid
        Get
            CheckDeleted()
            If Row(MerchantCodeDAL.COL_NAME_COMPANY_CREDIT_CARD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MerchantCodeDAL.COL_NAME_COMPANY_CREDIT_CARD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MerchantCodeDAL.COL_NAME_COMPANY_CREDIT_CARD_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(MerchantCodeDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(MerchantCodeDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MerchantCodeDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property MerchantCode() As String
        Get
            CheckDeleted()
            If Row(MerchantCodeDAL.COL_NAME_MERCHANT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(MerchantCodeDAL.COL_NAME_MERCHANT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(MerchantCodeDAL.COL_NAME_MERCHANT_CODE, Value)
        End Set
    End Property

    Public ReadOnly Property CreditCardFormatId() As Guid
        Get
            If (Me._creditCardFormatId.Equals(Guid.Empty)) Then
                Dim objCompanyCreditCard As New CompanyCreditCard(Me.CompanyCreditCardId)
                _creditCardFormatId = objCompanyCreditCard.CreditCardFormatId
            End If

            Return _creditCardFormatId
            
        End Get
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New MerchantCodeDAL
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal dealerId As Guid) As MerchantCodeSearchDV
        Try
            Dim dal As New MerchantCodeDAL
            Return New MerchantCodeSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, dealerId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As MerchantCodeSearchDV, ByVal NewMerchantCodedBO As MerchantCode)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        dv.Sort = ""
        If NewMerchantCodedBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(MerchantCodeSearchDV.COL_MERCHANT_CODE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(MerchantCodeSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(MerchantCodeSearchDV.COL_MERCHANT_CODE, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(MerchantCodeSearchDV.COL_MERCHANT_CODE_ID) = NewMerchantCodedBO.Id.ToByteArray
            row(MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE) = NewMerchantCodedBO.CompanyCreditCardId.ToByteArray
            row(MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_ID) = Guid.Empty.ToByteArray
            row(MerchantCodeSearchDV.COL_DEALER_ID) = NewMerchantCodedBO.dealerId.ToByteArray
            row(MerchantCodeSearchDV.COL_MERCHANT_CODE) = Nothing
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New MerchantCodeSearchDV(dt)
        End If
    End Sub

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As MerchantCode) As MerchantCodeSearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow
            row(MerchantCodeDAL.COL_NAME_MERCHANT_CODE_ID) = bo.Id.ToByteArray
            row(MerchantCodeDAL.COL_NAME_COMPANY_CREDIT_CARD_ID) = bo.Id.ToByteArray
            'row(MerchantCodeDAL.COL_NAME_CONTRACT_ID) = bo.ContractId.ToByteArray
            row(MerchantCodeDAL.COL_NAME_MERCHANT_CODE) = Nothing
            dt.Rows.Add(row)
        End If

        'Return (dv)
        Return New MerchantCodeSearchDV(dt)
    End Function

    '<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    'Public NotInheritable Class ValidateDuplicateCreditCardType
    '    Inherits ValidBaseAttribute
    '    Implements IValidatorAttribute
    '    Private _fieldDisplayName As String

    '    Public Sub New(ByVal fieldDisplayName As String)
    '        MyBase.New(fieldDisplayName, Messages.DUPLICATE_CREDIT_CARD_TYPE_FOUND)
    '        _fieldDisplayName = fieldDisplayName
    '    End Sub

    '    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
    '        Dim dal As New MerchantCodeDAL
    '        Dim obj As MerchantCode = CType(context, MerchantCode)
    '        Dim merchantCodeDataset As DataSet
    '        merchantCodeDataset = dal.VerifyDuplicateCreditCardType(obj.CompanyCreditCardId, obj.ContractId)
    '        If (merchantCodeDataset.Tables(0).Rows.Count > 0) Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    End Function
    'End Class

#End Region

End Class


