'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/7/2010)  ********************
Imports System.Text.RegularExpressions

Public Class CreditCardFormat
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
            Dim dal As New CreditCardFormatDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize(True)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New CreditCardFormatDAL
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
            Initialize(False)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private _regularExpression As RegularExpression
    Private Sub Initialize(blnNew As Boolean)
        If _regularExpression Is Nothing Then
            If blnNew Then
                _regularExpression = New RegularExpression(Dataset)
            Else
                _regularExpression = New RegularExpression(RegularExpressionId, Dataset)
            End If
            RegularExpressionId = _regularExpression.Id
        End If
    End Sub

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(CreditCardFormatDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CreditCardFormatDAL.COL_NAME_CREDIT_CARD_FORMAT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CreditCardTypeId As Guid
        Get
            CheckDeleted()
            If Row(CreditCardFormatDAL.COL_NAME_CREDIT_CARD_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CreditCardFormatDAL.COL_NAME_CREDIT_CARD_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CreditCardFormatDAL.COL_NAME_CREDIT_CARD_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RegularExpressionId As Guid
        Get
            CheckDeleted()
            If Row(CreditCardFormatDAL.COL_NAME_REGULAR_EXPRESSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CreditCardFormatDAL.COL_NAME_REGULAR_EXPRESSION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CreditCardFormatDAL.COL_NAME_REGULAR_EXPRESSION_ID, Value)
        End Set
    End Property

    Public Function RegularExpressionBO() As RegularExpression

        If _regularExpression Is Nothing Then
            _regularExpression = New RegularExpression(RegularExpressionId)
        End If
        Return _regularExpression

    End Function

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If IsFamilyDirty Then RegularExpressionBO.Save()
            If _isDSCreator AndAlso (IsDirty OrElse IsFamilyDirty) AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CreditCardFormatDAL
                dal.UpdateFamily(Dataset)
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

#Region "CCFORMATSearchDV"
    Public Class CreditCardFormatSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CREDIT_CARD_FORMAT_ID As String = "credit_card_format_id"
        Public Const COL_CREDIT_CARD_TYPE_ID As String = "credit_card_type_id"
        Public Const COL_CREDIT_CARD_TYPE As String = "Credit_Card_Type"
        Public Const COL_FORMAT As String = "format"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As CreditCardFormatSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(COL_CREDIT_CARD_FORMAT_ID) = (New Guid()).ToByteArray
            row(COL_CREDIT_CARD_TYPE_ID) = Guid.Empty.ToByteArray
            row(COL_CREDIT_CARD_TYPE) = ""
            row(COL_FORMAT) = ""
            dt.Rows.Add(row)
            Return New CreditCardFormatSearchDV(dt)
        End Function
    End Class
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(CreditCardTypeId As Guid) As CreditCardFormatSearchDV
        Try
            Dim dal As New CreditCardFormatDAL
            Return New CreditCardFormatSearchDV(dal.LoadList(CreditCardTypeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadByCode(CreditCardTypeCode As String) As DataView
        Try
            Dim dal As New CreditCardFormatDAL
            Return dal.LoadByCode(CreditCardTypeCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As CreditCardFormatSearchDV, NewCreditCardFormatBO As CreditCardFormat)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        dv.Sort = ""
        If NewCreditCardFormatBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(CreditCardFormatSearchDV.COL_CREDIT_CARD_FORMAT_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CreditCardFormatSearchDV.COL_CREDIT_CARD_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CreditCardFormatSearchDV.COL_FORMAT, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(CreditCardFormatSearchDV.COL_CREDIT_CARD_FORMAT_ID) = NewCreditCardFormatBO.Id.ToByteArray
            row(CreditCardFormatSearchDV.COL_CREDIT_CARD_TYPE_ID) = NewCreditCardFormatBO.CreditCardTypeId.ToByteArray
            row(CreditCardFormatSearchDV.COL_FORMAT) = String.Empty
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New CreditCardFormatSearchDV(dt)
        End If
    End Sub

    Public Shared Function IsCreditCardValid(CreditCardTypeCode As String, CreditCardNumber As String) As Boolean
        Dim dv As DataView = LoadByCode(CreditCardTypeCode)
        If dv Is Nothing OrElse dv.Count <= 0 OrElse dv.Item(0).Item(CreditCardFormatSearchDV.COL_FORMAT).Equals(String.Empty) Then
            Throw New StoredProcedureGeneratedException("Credit Card Format Error", Common.ErrorCodes.WS_CREDIT_CARD_FORMAT_NOT_FOUND)
        End If
        Dim format As String = dv.Item(0).Item(CreditCardFormatSearchDV.COL_FORMAT)
        Dim tempRegEx As Regex = New Regex(format)
        Dim M As Match = tempRegEx.Match(CreditCardNumber.Trim())
        Return M.Success
    End Function
#End Region

End Class


