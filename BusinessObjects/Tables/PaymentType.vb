'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/6/2006)  ********************

Public Class PaymentType
    Inherits BusinessObjectBase

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
            Dim dal As New PaymentTypeDAL
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
            Dim dal As New PaymentTypeDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
            If row(PaymentTypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PaymentTypeDAL.COL_NAME_PAYMENT_TYPE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(PaymentTypeDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PaymentTypeDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PaymentTypeDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(PaymentTypeDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PaymentTypeDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PaymentTypeDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CollectionMethodId As Guid
        Get
            CheckDeleted()
            If Row(PaymentTypeDAL.COL_NAME_COLLECTION_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PaymentTypeDAL.COL_NAME_COLLECTION_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PaymentTypeDAL.COL_NAME_COLLECTION_METHOD_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), PayPalPaymentInstruValidator("")>
    Public Property PaymentInstrumentId As Guid
        Get
            CheckDeleted()
            If Row(PaymentTypeDAL.COL_NAME_PAYMENT_INSTRUMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PaymentTypeDAL.COL_NAME_PAYMENT_INSTRUMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PaymentTypeDAL.COL_NAME_PAYMENT_INSTRUMENT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(PaymentTypeDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PaymentTypeDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PaymentTypeDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PaymentTypeDAL
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
#End Region

#Region "DataView Retrieveing Methods"


    Public Shared Function getList(CompGrpId As Guid, LanguageId As Guid) As PaymentTypeSearchDV

        Try
            Dim dal As New PaymentTypeDAL
            Dim ds As New DataSet
            dal.LoadList(ds, CompGrpId, LanguageId)
            Return New PaymentTypeSearchDV(ds.Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListForQuoteEngine(CompGrpId As Guid, LanguageId As Guid) As System.Data.DataView

        Try
            Dim dal As New PaymentTypeDAL
            Dim ds As New DataSet
            dal.LoadListForQouteEngine(ds, CompGrpId)
            Return New System.Data.DataView(ds.Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getCollectionMethodsList(ByRef ds As DataSet, CompGrpId As Guid, LanguageId As Guid) As DataSet

        Try
            Dim dal As New PaymentTypeDAL
            If ds Is Nothing Then ds = New DataSet
            dal.LoadCollectionMethodsList(ds, CompGrpId, LanguageId)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getPaymentInstrumentsList(ByRef ds As DataSet, CompGrpId As Guid, LanguageId As Guid) As DataSet

        Try
            Dim dal As New PaymentTypeDAL
            If ds Is Nothing Then ds = New DataSet
            dal.LoadPaymentInstrumentsList(ds, CompGrpId, LanguageId)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getPaymentTypesList(ByRef ds As DataSet, CompGrpId As Guid, LanguageId As Guid) As DataSet

        Try
            Dim dal As New PaymentTypeDAL
            If ds Is Nothing Then ds = New DataSet
            dal.LoadPaymentTypesList(ds, CompGrpId, LanguageId)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As PaymentType) As DataView

    '    Dim dt As DataTable
    '    dt = dv.Table

    '    If bo.IsNew Then
    '        Dim row As DataRow = dt.NewRow

    '        row(PaymentTypeDAL.COL_NAME_COLLECTION_METHOD_ID) = bo.CollectionMethodId.ToByteArray
    '        row(PaymentTypeDAL.COL_NAME_PAYMENT_INSTRUMENT_ID) = bo.PaymentInstrumentId.ToByteArray
    '        row(PaymentTypeDAL.COL_NAME_PAYMENT_TYPE_ID) = bo.Id.ToByteArray
    '        'row(PaymentTypeDAL.COL_NAME_COMPANY_GROUP_ID) = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id.ToByteArray
    '        dt.Rows.Add(row)
    '    End If

    '    Return (dv)

    'End Function
    Public Shared Sub AddNewRowToSearchDV(ByRef dv As PaymentTypeSearchDV, NewBO As PaymentType)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(PaymentTypeSearchDV.COL_PAYMENT_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(PaymentTypeSearchDV.COL_COLLECTION_METHOD, GetType(String))
                dt.Columns.Add(PaymentTypeSearchDV.COL_COLLECTION_METHOD_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT, GetType(String))
                dt.Columns.Add(PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT_ID, guidTemp.ToByteArray.GetType)

            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(PaymentTypeSearchDV.COL_PAYMENT_TYPE_ID) = NewBO.Id.ToByteArray
            row(PaymentTypeSearchDV.COL_COLLECTION_METHOD_ID) = NewBO.CollectionMethodId.ToByteArray
            row(PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT_ID) = NewBO.PaymentInstrumentId.ToByteArray
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New PaymentTypeSearchDV(dt)
        End If
    End Sub
#End Region
#Region "SearchDV"
    Public Class PaymentTypeSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PAYMENT_TYPE_ID As String = PaymentTypeDAL.COL_NAME_PAYMENT_TYPE_ID
        Public Const COL_PAYMENT_INSTRUMENT_ID As String = PaymentTypeDAL.COL_NAME_PAYMENT_INSTRUMENT_ID
        Public Const COL_COLLECTION_METHOD_ID As String = PaymentTypeDAL.COL_NAME_COLLECTION_METHOD_ID
        Public Const COL_PAYMENT_INSTRUMENT As String = PaymentTypeDAL.COL_NAME_PAYMENT_INSTRUMENT
        Public Const COL_COLLECTION_METHOD As String = PaymentTypeDAL.COL_NAME_COLLECTION_METHOD
        Public Const COL_COMPANY_GROUP_ID As String = PaymentTypeDAL.COL_NAME_COMPANY_GROUP_ID
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class PayPalPaymentInstruValidator
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PAY_PAL_VALIDATOR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As PaymentType = CType(objectToValidate, PaymentType)

            If Not obj.CollectionMethodId = Guid.Empty Then
                If LookupListNew.GetCodeFromId(LookupListCache.LK_PAYMENT_INSTRUMENT, obj.PaymentInstrumentId) = Codes.PAYMENT_INSTRUMENT__PAYPAL Then
                    If Not LookupListNew.GetCodeFromId(LookupListCache.LK_COLLECTION_METHODS, obj.CollectionMethodId) = Codes.COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH Then
                        Return False
                    End If
                End If
            End If

            Return True

        End Function
    End Class

#End Region
End Class



