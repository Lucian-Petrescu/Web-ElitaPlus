'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/13/2010)  ********************

Public Class DealerTmkReconWrk
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

    'Exiting BO
    Public Sub New(id As Guid, sModifiedDate As String)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
        VerifyConcurrency(sModifiedDate)
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
            Dim dal As New DealerTmkReconWrkDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New DealerTmkReconWrkDAL
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
            If Row(DealerTmkReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerTmkReconWrkDAL.COL_NAME_DEALER_TMK_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerfileProcessedId As Guid
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerTmkReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=8)> _
    Public Property RecordType As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=12)> _
    Public Property RejectCode As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property RejectReason As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property TmkLoaded As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_TMK_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_TMK_LOADED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_TMK_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property Certificate As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Dealercode As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_DEALERCODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_DEALERCODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_DEALERCODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Firstname As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_FIRSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_FIRSTNAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_FIRSTNAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Lastname As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_LASTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_LASTNAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_LASTNAME, Value)
        End Set
    End Property


    Public Property Salesdate As DateType
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_SALESDATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerTmkReconWrkDAL.COL_NAME_SALESDATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_SALESDATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property CampaignNumber As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Layout As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DealerTmkReconWrkDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerTmkReconWrkDAL
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

#Region "Validation"

    Public Shared Sub ValidateFileName(fileLength As Integer)
        If fileLength = 0 Then
            Dim errors() As ValidationError = {New ValidationError("DEALERLOADFORM_FORM001", GetType(DealerTmkReconWrk), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(DealerTmkReconWrk).FullName)
        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New DealerTmkReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Return (ds.Tables(DealerTmkReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadList(dealerfileProcessedID As Guid, certNumberMask As String, campaignNumberMask As String, statusCodeMask As String) As DataView
        Try
            Dim dal As New DealerTmkReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, certNumberMask, campaignNumberMask, statusCodeMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Return (ds.Tables(DealerTmkReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "StoreProcedures Control"
    Public Shared Function ValidateFile(strFileName As String) As Guid
        Try
            Dim oData As New DealerTmkReconWrkDAL.TeleMrktFileProcessedData
            oData.filename = strFileName
            oData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)

            Dim dal As New DealerTmkReconWrkDAL
            dal.ValidateFile(oData)
            Return oData.interfaceStatus_id
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ProcessFile(strFileName As String) As Guid
        Try
            Dim oData As New DealerTmkReconWrkDAL.TeleMrktFileProcessedData
            oData.filename = strFileName
            oData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_PROCESS)

            Dim dal As New DealerTmkReconWrkDAL
            dal.ProcessFile(oData)
            Return oData.interfaceStatus_id
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function DeleteFile(strFileName As String) As Guid
        Try
            Dim oData As New DealerTmkReconWrkDAL.TeleMrktFileProcessedData
            oData.filename = strFileName
            oData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_DELETE)

            Dim dal As New DealerTmkReconWrkDAL
            dal.DeleteFile(oData)
            Return oData.interfaceStatus_id
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
End Class


