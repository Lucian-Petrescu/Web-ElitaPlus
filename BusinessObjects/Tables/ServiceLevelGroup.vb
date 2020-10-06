'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/27/2012)  ********************

Public Class ServiceLevelGroup
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
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
            Dim dal As New ServiceLevelGroupDAL
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
            Dim dal As New ServiceLevelGroupDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(ServiceLevelGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceLevelGroupDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(ServiceLevelGroupDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ServiceLevelGroupDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ServiceLevelGroupDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(ServiceLevelGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ServiceLevelGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ServiceLevelGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If row(ServiceLevelGroupDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceLevelGroupDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ServiceLevelGroupDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceLevelGroupDAL
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

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal searchCode As String, ByVal searchDesc As String, ByVal oCountryId As Guid, Optional ByVal fromDate As String = Nothing, Optional ByVal toDate As String = Nothing) As ServiceLevelGroupSearchDV
        Try
            Dim dal As New ServiceLevelGroupDAL
            Dim oCountryIds As ArrayList

            If DALBase.IsNothing(oCountryId) Then
                ' Get All User Countries
                oCountryIds = ElitaPlusIdentity.Current.ActiveUser.Countries
            Else
                oCountryIds = New ArrayList
                oCountryIds.Add(oCountryId)
            End If

            Return New ServiceLevelGroupSearchDV(dal.LoadList(oCountryIds, searchCode, searchDesc, fromDate, toDate).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function



    Public Shared Function GetNewDataViewRow(ByVal dv As ServiceLevelGroupSearchDV, ByVal bo As ServiceLevelGroup) As ServiceLevelGroupSearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(ServiceLevelGroupSearchDV.COL_NAME_CODE) = bo.Code 'String.Empty
            row(ServiceLevelGroupSearchDV.COL_NAME_DESCRIPTION) = bo.Description 'String.Empty
            row(ServiceLevelGroupSearchDV.COL_NAME_SERVICE_LEVEL_GROUP_ID) = bo.Id.ToByteArray
            row(ServiceLevelGroupSearchDV.COL_NAME_COUNTRY_ID) = bo.CountryId.ToByteArray
            'row(ServiceLevelGroupSearchDV.COL_DEALER_GROUP_ID) = bo.DealerGroupId.ToByteArray
            'row(BillingPlanDAL.COL_NAME_ACCTING_BY_GROUP_DESC) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_YESNO, bo.AcctingByGroupId)
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function
#End Region

    Public Class ServiceLevelGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COUNTRY_DESC As String = ServiceLevelGroupDAL.COL_NAME_COUNTRY_DESC
        Public Const COL_NAME_DESCRIPTION As String = ServiceLevelGroupDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = ServiceLevelGroupDAL.COL_NAME_CODE
        Public Const COL_NAME_SERVICE_LEVEL_GROUP_ID As String = ServiceLevelGroupDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID
        Public Const COL_NAME_COUNTRY_ID As String = ServiceLevelGroupDAL.COL_NAME_COUNTRY_ID
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ServiceLevelGroupId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property


    End Class

End Class


