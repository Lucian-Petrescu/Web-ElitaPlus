Public Class DealerReinsReconWrk
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub
    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.VerifyConcurrency(sModifiedDate)
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
            Dim dal As New DealerReinsReconWrkDAL
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
            Dim dal As New DealerReinsReconWrkDAL
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
            If Row(DealerReinsReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerReinsReconWrkDAL.COL_NAME_DEALER_REINS_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property DealerfileProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerReinsReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property ReinsuranceLoaded() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_REINSURANCE_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_REINSURANCE_LOADED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_REINSURANCE_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)>
    Public Property RecordType() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Certificate() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3)>
    Public Property RejectCode() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property



    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerReinsReconWrkDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)>
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)>
    Public Property RiskTypeEnglish() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_RISK_TYPE_ENGLISH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_RISK_TYPE_ENGLISH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_RISK_TYPE_ENGLISH, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property CoverageTypeCode() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_COVERAGE_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_COVERAGE_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_COVERAGE_TYPE_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=250)>
    Public Property ReinsuranceRejectReason() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_REINSURANCE_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_REINSURANCE_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_REINSURANCE_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3000)>
    Public Property EntireRecord() As String
        Get
            CheckDeleted()
            If Row(DealerReinsReconWrkDAL.COL_NAME_ENTIRE_RECORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerReinsReconWrkDAL.COL_NAME_ENTIRE_RECORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerReinsReconWrkDAL.COL_NAME_ENTIRE_RECORD, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerReinsReconWrkDAL
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

    Public Shared Function LoadList(ByVal dealerfileProcessedID As Guid, ByVal recordMode As String) As DataView
        Try
            Dim dal As New DealerReinsReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, recordMode)
            Return (ds.Tables(DealerReinsReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadRejectList(ByVal dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New DealerReinsReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadRejectList(dealerfileProcessedID)
            Return (ds.Tables(DealerReinsReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function



#End Region

End Class

