Public Class FileProcessed
    Inherits BusinessObjectBase
#Region "Constant"
    Public Const COL_NAME_FILE_PROCESSED_ID As String = "file_processed_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_FILENAME As String = "file_name"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_BYPASSED As String = "bypassed"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_STATUS_DESC As String = "status_desc"
#End Region

#Region "Constructors"
    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub
#End Region
#Region "Load"
    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New FileProcessedDAL
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

    Public Shared Function LoadList(ByVal oFileProcessedData As FileProcessedData) As DataView
        Try
            Dim dal As New FileProcessedDAL
            Dim ds As DataSet
            ds = dal.LoadList(oFileProcessedData)
            Return (ds.Tables(FileProcessedDAL.TABLE_NAME).DefaultView)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
#Region "Properties"
    <ValueMandatory("")> _
    Public Property FileProcessId As Guid
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_FILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(FileProcessedDAL.COL_NAME_FILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_FILE_PROCESSED_ID, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property FileName As String
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_FILE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FileProcessedDAL.COL_NAME_FILE_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_FILE_NAME, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(FileProcessedDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then


                Return Nothing
            Else
                Return New Guid(CType(Row(FileProcessedDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(FileProcessedDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    Public Property Received As LongType
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(FileProcessedDAL.COL_NAME_RECEIVED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_RECEIVED, Value)
        End Set
    End Property



    Public Property Counted As LongType
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_COUNTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(FileProcessedDAL.COL_NAME_COUNTED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_COUNTED, Value)
        End Set
    End Property

    Public Property Bypassed As LongType
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_BYPASSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(FileProcessedDAL.COL_NAME_BYPASSED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_BYPASSED, Value)
        End Set
    End Property

    Public Property Rejected As LongType
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_REJECTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(FileProcessedDAL.COL_NAME_REJECTED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_REJECTED, Value)
        End Set
    End Property



    Public Property Validated As LongType
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_VALIDATED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(FileProcessedDAL.COL_NAME_VALIDATED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_VALIDATED, Value)
        End Set
    End Property



    Public Property Loaded As LongType
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(FileProcessedDAL.COL_NAME_LOADED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Layout As String
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FileProcessedDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerFileProcessedDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property

    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(FileProcessedDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(FileProcessedDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property
    Public ReadOnly Property Status As String
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FileProcessedDAL.COL_NAME_STATUS), String)
            End If
        End Get
    End Property
    Public ReadOnly Property StatusDescription As String
        Get
            CheckDeleted()
            If Row(FileProcessedDAL.COL_NAME_STATUS_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FileProcessedDAL.COL_NAME_STATUS_DESC), String)
            End If
        End Get
    End Property
#End Region
#Region "Properties External BOs"

    Public ReadOnly Property DealerCode As String
        Get
            If FileProcessId.Equals(Guid.Empty) Then Return Nothing
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerId)
        End Get
    End Property

    Public ReadOnly Property DealerNameLoad As String
        Get
            If FileProcessId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_DEALERS)
            Return LookupListNew.GetDescriptionFromId(dv, DealerId)
        End Get
    End Property
    Public ReadOnly Property CompanyGroupCode As String
        Get
            If FileProcessId.Equals(Guid.Empty) Then Return Nothing
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_GROUP, CompanyGroupId)
        End Get
    End Property

    Public ReadOnly Property CompanyGroupLoad As String
        Get
            If FileProcessId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_COMPANY_GROUP)
            Return LookupListNew.GetDescriptionFromId(dv, CompanyGroupId)
        End Get
    End Property
    Public ReadOnly Property CompanyCode As String
        Get
            If FileProcessId.Equals(Guid.Empty) Then Return Nothing
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY, CompanyId)
        End Get
    End Property

    Public ReadOnly Property CompanyLoad As String
        Get
            If FileProcessId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_COMPANY)
            Return LookupListNew.GetDescriptionFromId(dv, CompanyId)
        End Get
    End Property
    Public ReadOnly Property CountryName As String
        Get
            If FileProcessId.Equals(Guid.Empty) OrElse CountryId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_COUNTRIES)
            Return LookupListNew.GetDescriptionFromId(dv, CountryId)
        End Get
    End Property
#End Region
End Class
