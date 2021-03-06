'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/14/2007)  ********************

Public Class VSCClassCode
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New VSCClassCodeDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New VSCClassCodeDAL
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
            If Row(VSCClassCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCClassCodeDAL.COL_NAME_CLASS_CODE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(VSCClassCodeDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCClassCodeDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VSCClassCodeDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
      Public Property Active() As Guid
        Get
            CheckDeleted()
            If Row(VSCClassCodeDAL.COL_NAME_ACTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCClassCodeDAL.COL_NAME_ACTIVE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCClassCodeDAL.COL_NAME_ACTIVE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
        Public Property CompanyGroup() As Guid
        Get
            CheckDeleted()
            If Row(VSCClassCodeDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCClassCodeDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCClassCodeDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set

    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCClassCodeDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

    '#Region "DataView Retrieveing Methods"
    '    Public Shared Function Getlist(ByVal CodeMask As String, _
    '                                               ByVal ActiveMask As boolean, _
    '                                               ByVal companygroupId As Guid) As DataView
    '        Try
    '            Dim dal As New VSCClassCodeDAL
    '            Dim ds As Dataset

    '            ds = dal.LoadList(CodeMask, ActiveMask, companygroupId)

    '            Return ds.Tables(VSCClassCodeDAL.TABLE_NAME).DefaultView

    '        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '            Throw New DataBaseAccessException(ex.ErrorType, ex)
    '        End Try
    '    End Function
    '#End Region

#Region "DataView Retrieveing Methods"

#Region "ClassCodeSearchDV"
    Public Class ClassCodeSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_VSC_CLASS_CODE_ID As String = VSCClassCodeDAL.COL_NAME_CLASS_CODE_ID
        Public Const COL_NAME_COMPANY_GROUP_ID As String = VSCClassCodeDAL.COL_NAME_COMPANY_GROUP_ID
        Public Const COL_NAME_CODE As String = VSCClassCodeDAL.COL_NAME_CODE
        Public Const COL_NAME_ACTIVE As String = VSCClassCodeDAL.COL_NAME_ACTIVE
        Public Const COL_NAME_DESCRIPTION As String = VSCClassCodeDAL.COL_NAME_DESCRIPTION

#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal CodeIdMask As Guid, _
                                                 ByVal ActiveMask As Guid, _
                                                       ByVal companygroupId As Guid) As DataView
        Try
            Dim dal As New VSCClassCodeDAL

            Return New ClassCodeSearchDV(dal.LoadList(CodeIdMask, ActiveMask, companygroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As DataView
        Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        Dim companygroupId As Guid = company.CompanyGroupId
        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(VSCClassCodeDAL.COL_NAME_CLASS_CODE_ID) = id.ToByteArray
        row(VSCClassCodeDAL.COL_NAME_CODE) = String.Empty
        row(VSCClassCodeDAL.COL_NAME_ACTIVE) = Guid.Empty.ToByteArray
        row(VSCClassCodeDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(VSCClassCodeDAL.COL_NAME_COMPANY_GROUP_ID) = companygroupId.ToByteArray


        dt.Rows.Add(row)

        Return (dv)

    End Function


#End Region



End Class


