'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/13/2010)  ********************

Public Class DealerTmkReconWrk
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

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
        Me.VerifyConcurrency(sModifiedDate)
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
            Dim dal As New DealerTmkReconWrkDAL
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
            Dim dal As New DealerTmkReconWrkDAL
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
            If Row(DealerTmkReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerTmkReconWrkDAL.COL_NAME_DEALER_TMK_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerfileProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerTmkReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=8)> _
    Public Property RecordType() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=12)> _
    Public Property RejectCode() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property TmkLoaded() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_TMK_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_TMK_LOADED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_TMK_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property Certificate() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Dealercode() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_DEALERCODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_DEALERCODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_DEALERCODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Firstname() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_FIRSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_FIRSTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_FIRSTNAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Lastname() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_LASTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_LASTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_LASTNAME, Value)
        End Set
    End Property


    Public Property Salesdate() As DateType
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_SALESDATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerTmkReconWrkDAL.COL_NAME_SALESDATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_SALESDATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property CampaignNumber() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Layout() As String
        Get
            CheckDeleted()
            If Row(DealerTmkReconWrkDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerTmkReconWrkDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerTmkReconWrkDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerTmkReconWrkDAL
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

#Region "Validation"

    Public Shared Sub ValidateFileName(ByVal fileLength As Integer)
        If fileLength = 0 Then
            Dim errors() As ValidationError = {New ValidationError("DEALERLOADFORM_FORM001", GetType(DealerTmkReconWrk), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(DealerTmkReconWrk).FullName)
        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New DealerTmkReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Return (ds.Tables(DealerTmkReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadList(ByVal dealerfileProcessedID As Guid, ByVal certNumberMask As String, ByVal campaignNumberMask As String, ByVal statusCodeMask As String) As DataView
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
    Public Shared Function ValidateFile(ByVal strFileName As String) As Guid
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

    Public Shared Function ProcessFile(ByVal strFileName As String) As Guid
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

    Public Shared Function DeleteFile(ByVal strFileName As String) As Guid
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


