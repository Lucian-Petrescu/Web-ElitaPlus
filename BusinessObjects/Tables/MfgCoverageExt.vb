'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/24/2010)  ********************

Public Class MfgCoverageExt
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
            Dim dal As New MfgCoverageExtDAL
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
            Dim dal As New MfgCoverageExtDAL
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
            If row(MfgCoverageExtDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_EXT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(MfgCoverageExtDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(MfgCoverageExtDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MfgCoverageExtDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property MfgCoverageId() As Guid
        Get
            CheckDeleted()
            If row(MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ExtWarranty() As LongType
        Get
            CheckDeleted()
            If row(MfgCoverageExtDAL.COL_NAME_EXT_WARRANTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(MfgCoverageExtDAL.COL_NAME_EXT_WARRANTY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(MfgCoverageExtDAL.COL_NAME_EXT_WARRANTY, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New MfgCoverageExtDAL
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

#Region "Constants"

    Public Const COL_MFG_COVERAGE_EXT_ID As String = MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_EXT_ID
    Public Const COL_MFG_COVERAGE_ID As String = MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_ID
    Public Const COL_DEALER_ID As String = MfgCoverageExtDAL.COL_NAME_DEALER_ID
    Public Const COL_EXT_WARRANTY As String = MfgCoverageExtDAL.COL_NAME_EXT_WARRANTY
    Public Const COL_DEALER_NAME As String = MfgCoverageExtDAL.COL_LST_DEALER_NAME

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(ByVal MfgCoverageId As Guid) As DataView
        Try
            Dim dal As New MfgCoverageExtDAL
            Dim ds As DataSet

            ds = dal.LoadList(MfgCoverageId)
            Return (ds.Tables(MfgCoverageExtDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetAvailableDealers(ByVal MfgCoverageId As Guid, ByVal DealerId As Guid) As DataView
        Try
            Dim dal As New MfgCoverageExtDAL
            Dim ds As DataSet

            ds = dal.GetAvailableDealers(MfgCoverageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, DealerId)
            Return (ds.Tables(MfgCoverageExtDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As MfgCoverageExt) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_EXT_ID) = id.ToByteArray
            '  row(MfgCoverageExtDAL.COL_NAME_EXT_WARRANTY) = If(bo.ExtWarranty Is Nothing, DBNull.Value)
            row(MfgCoverageExtDAL.COL_NAME_DEALER_ID) = bo.DealerId.ToByteArray
            row(MfgCoverageExtDAL.COL_NAME_MFG_COVERAGE_ID) = bo.MfgCoverageId.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function


#End Region

End Class


