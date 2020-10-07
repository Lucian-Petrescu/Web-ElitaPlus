'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/11/2007)  ********************

Public Class QuoteEngineData1

    Public ReadOnly Property QEData As QuoteEngineData
        Get
            If _QEData Is Nothing Then
                _QEData = New QuoteEngineData
            End If
            Return _QEData
        End Get
    End Property
    Private _QEData As QuoteEngineData

End Class

Public Class VSCQuote
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New VSCQuoteDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New VSCQuoteDAL
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
            If Row(VSCQuoteDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCQuoteDAL.COL_NAME_QUOTE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property QuoteNumber As LongType
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_QUOTE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCQuoteDAL.COL_NAME_QUOTE_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_QUOTE_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ManufacturerId As Guid
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCQuoteDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property VscModelId As Guid
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_VSC_MODEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCQuoteDAL.COL_NAME_VSC_MODEL_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_VSC_MODEL_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCQuoteDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ModelYear As LongType
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_MODEL_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCQuoteDAL.COL_NAME_MODEL_YEAR), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_MODEL_YEAR, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property VscClassCodeId As Guid
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_VSC_CLASS_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCQuoteDAL.COL_NAME_VSC_CLASS_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_VSC_CLASS_CODE_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property Vin As String
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_VIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCQuoteDAL.COL_NAME_VIN), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_VIN, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Odometer As LongType
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCQuoteDAL.COL_NAME_ODOMETER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_ODOMETER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=480)> _
    Public Property VehicleLicenseTag As String
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_VEHICLE_LICENSE_TAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCQuoteDAL.COL_NAME_VEHICLE_LICENSE_TAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_VEHICLE_LICENSE_TAG, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property EngineVersion As String
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_ENGINE_VERSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCQuoteDAL.COL_NAME_ENGINE_VERSION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_ENGINE_VERSION, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InServiceDate As DateType
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_IN_SERVICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCQuoteDAL.COL_NAME_IN_SERVICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_IN_SERVICE_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=32)> _
    Public Property NewUsed As String
        Get
            CheckDeleted()
            If Row(VSCQuoteDAL.COL_NAME_NEW_USED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCQuoteDAL.COL_NAME_NEW_USED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCQuoteDAL.COL_NAME_NEW_USED, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCQuoteDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
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
    Public Shared Function GetQuote(ByVal oQuoteEngineData As QuoteEngineData) As Dataset
        Try
            Dim dal As New VSCQuoteDAL
            Return dal.GetQuote(oQuoteEngineData)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


