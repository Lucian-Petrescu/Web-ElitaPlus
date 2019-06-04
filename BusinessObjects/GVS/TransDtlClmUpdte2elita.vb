'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/12/2008)  ********************

Public Class TransDtlClmUpdte2elita
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
            Dim dal As New TransDtlClmUpdte2elitaDAL
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
            Dim dal As New TransDtlClmUpdte2elitaDAL
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
            If row(TransDtlClmUpdte2elitaDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_TRANS_DTL_CLM_UPDTE_2ELITA_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property TransactionLogHeaderId() As Guid
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ItemNumber() As LongType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_ITEM_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Response() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_RESPONSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_RESPONSE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_RESPONSE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property ResponseDetail() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_RESPONSE_DETAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_RESPONSE_DETAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_RESPONSE_DETAIL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property XmlClaimNumber() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property XmlServiceOrderNumber() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_SERVICE_ORDER_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_SERVICE_ORDER_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_SERVICE_ORDER_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property XmlExternalItemCode() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_EXTERNAL_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_EXTERNAL_ITEM_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_EXTERNAL_ITEM_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property XmlInHomeVisitDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_IN_HOME_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_IN_HOME_VISIT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_IN_HOME_VISIT_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property XmlVisitDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_VISIT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_VISIT_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property XmlDefectReason() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_DEFECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_DEFECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_DEFECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property XmlTechnicalReport() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_TECHNICAL_REPORT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_TECHNICAL_REPORT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_TECHNICAL_REPORT, Value)
        End Set
    End Property



    Public Property XmlLabor() As DecimalType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_LABOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_LABOR), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_LABOR, Value)
        End Set
    End Property



    Public Property XmlTripAmount() As DecimalType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_TRIP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_TRIP_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_TRIP_AMOUNT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property XmlExpectedRepairDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_EXPECTED_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_EXPECTED_REPAIR_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_EXPECTED_REPAIR_DATE, Value)
        End Set
    End Property



    Public Property XmlQuotationDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_QUOTATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_QUOTATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_QUOTATION_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property XmlClaimStatus() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_CLAIM_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_CLAIM_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_CLAIM_STATUS, Value)
        End Set
    End Property



    Public Property XmlRepairDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_REPAIR_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_REPAIR_DATE, Value)
        End Set
    End Property



    Public Property XmlShipping() As DecimalType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_SHIPPING) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_SHIPPING), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_SHIPPING, Value)
        End Set
    End Property



    Public Property XmlPickupDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_PICKUP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_PICKUP_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_PICKUP_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property XmlETicket() As String
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_E_TICKET) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_E_TICKET), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_E_TICKET, Value)
        End Set
    End Property



    Public Property XmlCollectDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_COLLECT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_COLLECT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlClmUpdte2elitaDAL.COL_NAME_XML_COLLECT_DATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransDtlClmUpdte2elitaDAL
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

#End Region

End Class


