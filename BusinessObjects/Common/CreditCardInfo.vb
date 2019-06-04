'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/29/2010)  ********************

Imports Assurant.Elita.PciSecure.Attributes

Public Class CreditCardInfo
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
            Dim dal As New CreditCardInfoDAL
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
            Dim dal As New CreditCardInfoDAL
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


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CreditCardInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CreditCardInfoDAL.COL_NAME_CREDIT_CARD_INFO_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CreditCardFormatId() As Guid
        Get
            CheckDeleted()
            If Row(CreditCardInfoDAL.COL_NAME_CREDIT_CARD_FORMAT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CreditCardInfoDAL.COL_NAME_CREDIT_CARD_FORMAT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CreditCardInfoDAL.COL_NAME_CREDIT_CARD_FORMAT_ID, Value)
        End Set
    End Property


    <PciProtect(PciDataType.CreditCardNumber)>
    Public Property CreditCardNumber() As String
        Get
            CheckDeleted()
            If Row(CreditCardInfoDAL.COL_NAME_CREDIT_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CreditCardInfoDAL.COL_NAME_CREDIT_CARD_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CreditCardInfoDAL.COL_NAME_CREDIT_CARD_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property NameOnCreditCard() As String
        Get
            CheckDeleted()
            If Row(CreditCardInfoDAL.COL_NAME_NAME_ON_CREDIT_CARD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CreditCardInfoDAL.COL_NAME_NAME_ON_CREDIT_CARD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CreditCardInfoDAL.COL_NAME_NAME_ON_CREDIT_CARD, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=28)> _
    Public Property ExpirationDate() As String
        Get
            CheckDeleted()
            If Row(CreditCardInfoDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CreditCardInfoDAL.COL_NAME_EXPIRATION_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CreditCardInfoDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=16)> _
    Public Property Last4Digits() As String
        Get
            CheckDeleted()
            If Row(CreditCardInfoDAL.COL_NAME_LAST_4_DIGITS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CreditCardInfoDAL.COL_NAME_LAST_4_DIGITS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CreditCardInfoDAL.COL_NAME_LAST_4_DIGITS, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CreditCardInfoDAL
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

    Public Shared Sub DeleteNewChildCreditCardInfo(ByVal parentCertInstallment As CertInstallment)
        Dim row As DataRow
        If parentCertInstallment.Dataset.Tables.IndexOf(CreditCardInfoDAL.TABLE_NAME) >= 0 Then
            Dim rowIndex As Integer
            For rowIndex = 0 To parentCertInstallment.Dataset.Tables(CreditCardInfoDAL.TABLE_NAME).Rows.Count - 1
                row = parentCertInstallment.Dataset.Tables(CreditCardInfoDAL.TABLE_NAME).Rows.Item(rowIndex)
                If Not (row.RowState = DataRowState.Deleted) Or (row.RowState = DataRowState.Detached) Then
                    Dim cc As CreditCardInfo = New CreditCardInfo(row)
                    If parentCertInstallment.CreditCardInfoId.Equals(cc.Id) And cc.IsNew Then
                        cc.Delete()
                    End If
                End If
            Next

        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Custom Validation"


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidCreditCardNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.WS_INVALID_CREDIT_CARD_NUMBER)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CreditCardInfo = CType(objectToValidate, CreditCardInfo)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            End If

            Dim dv As DataView = LookupListNew.GetCompanyCreditCardsFormatLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim CreditCardTypeCode As String = LookupListNew.GetCodeFromId(dv, obj.CreditCardFormatId)
            If obj.IsNew AndAlso Not CreditCardFormat.IsCreditCardValid(CreditCardTypeCode, obj.CreditCardNumber) Then
                Return False
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditional
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CreditCardInfo = CType(objectToValidate, CreditCardInfo)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf obj.IsNew AndAlso valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
#End Region

End Class


