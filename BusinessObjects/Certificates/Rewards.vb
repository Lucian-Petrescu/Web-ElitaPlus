﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/11/2017)  ********************
Imports Common = Assurant.ElitaPlus.Common
Public Class Rewards
    Inherits BusinessObjectBase


#Region "Constants"
    Private Const SEARCH_EXCEPTION As String = "SEARCH_CRITERION_001" ' Reward List Search Exception - If no criterion(except companyid) is selected
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

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RewardsDAL
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
            Dim dal As New RewardsDAL
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
            If Row(RewardsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RewardsDAL.COL_NAME_REWARD_ID), Byte()))
            End If
        End Get
    End Property


    Public Property ReferenceId() As Guid
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RewardsDAL.COL_NAME_REFERENCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_REFERENCE_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)>
    Public Property RewardTypeXcd() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_REWARD_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_REWARD_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_REWARD_TYPE_XCD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property RewardStatusXcd() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_REWARD_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_REWARD_STATUS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_REWARD_STATUS_XCD, Value)
        End Set
    End Property


     <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)> 
    Public Property RewardAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_REWARD_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(RewardsDAL.COL_NAME_REWARD_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_REWARD_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property RewardPymtModeXcd() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_REWARD_PYMT_MODE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_REWARD_PYMT_MODE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_REWARD_PYMT_MODE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property FormSignedXcd() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_FORM_SIGNED_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_FORM_SIGNED_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_FORM_SIGNED_XCD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property SubscriptionFormSignedXcd() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_SUBSCRIPTION_FORM_SIGNED_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_SUBSCRIPTION_FORM_SIGNED_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_SUBSCRIPTION_FORM_SIGNED_XCD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property InvoiceSignedXcd() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_INVOICE_SIGNED_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_INVOICE_SIGNED_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_INVOICE_SIGNED_XCD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property RibSignedXcd() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_RIB_SIGNED_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_RIB_SIGNED_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_RIB_SIGNED_XCD, Value)
        End Set
    End Property



    'Public Property DeliveryDate() As DateType
    '    Get
    '        CheckDeleted()
    '        If Row(RewardsDAL.COL_NAME_DELIVERY_DATE) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New DateType(CType(Row(RewardsDAL.COL_NAME_DELIVERY_DATE), Date))
    '        End If
    '    End Get
    '    Set(ByVal Value As DateType)
    '        CheckDeleted()
    '        Me.SetValue(RewardsDAL.COL_NAME_DELIVERY_DATE, Value)
    '    End Set
    'End Property


    <ValidStringLength("", Max:=20)>
    Public Property SequenceNumber() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_SEQUENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_SEQUENCE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_SEQUENCE_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property CertNumber() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public ReadOnly Property IbanNumber() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_IBAN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_IBAN_NUMBER), String)
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=80)>
    Public ReadOnly Property SwiftCode() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_SWIFT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_SWIFT_CODE), String)
            End If
        End Get
    End Property

    Public Property GiftCardRequestId() As Guid
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_GIFT_CARD_REQUEST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RewardsDAL.COL_NAME_GIFT_CARD_REQUEST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RewardsDAL.COL_NAME_GIFT_CARD_REQUEST_ID, Value)
        End Set
    End Property

        <ValidStringLength("", Max:=5)>
    Public ReadOnly Property Dealer() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_DEALER), String)
            End If
        End Get
    End Property

        <ValidStringLength("", Max:=5)>
    Public ReadOnly Property Company() As String
        Get
            CheckDeleted()
            If Row(RewardsDAL.COL_NAME_COMPANY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RewardsDAL.COL_NAME_COMPANY), String)
            End If
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RewardsDAL
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

    Public Shared Function getRewardList(ByVal CompanyId As Guid, ByVal DealerId As Guid, ByVal CertificateNumber As String, ByVal RewardStatus As String) As RewardSearchDV
        Try
            Dim dal As New RewardsDAL
            Dim fromdate As Date?
            Dim todate As Date?

            Return New RewardSearchDV(dal.LoadRewardList(CompanyId, DealerId, CertificateNumber, RewardStatus).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "RewardSearchDV"
    Public Class RewardSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_REWARD_ID As String = "reward_id"
        Public Const COL_CERT_NUMBER As String = "cert_number"
        Public Const COL_REWARD_AMOUNT As String = "reward_amount"
        Public Const COL_REWARD_PAYMENT_MODE As String = "reward_pymt_mode_xcd"
        Public Const COL_REWARD_TYPE As String = "reward_type"
        Public Const COL_REWARD_STATUS_CODE As String = "reward_status"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
End Class
