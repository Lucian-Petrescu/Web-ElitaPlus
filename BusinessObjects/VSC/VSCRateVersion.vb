'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/11/2007)  ********************

Public Class VSCRateVersion
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const MSG_INVALID_EXPIRATION_DATE As String = "MSG_EXPIRATION_DATE_CANNOT_LOWER_THAN_EFFECTIVE_DATE"



#End Region
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
            Dim dal As New VSCRateVersionDAL
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
            Dim dal As New VSCRateVersionDAL
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
            If Row(VSCRateVersionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCRateVersionDAL.COL_NAME_VSC_RATE_VERSION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property VscPlanId() As Guid
        Get
            CheckDeleted()
            If Row(VSCRateVersionDAL.COL_NAME_VSC_PLAN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCRateVersionDAL.COL_NAME_VSC_PLAN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCRateVersionDAL.COL_NAME_VSC_PLAN_ID, Value)
        End Set
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(VSCRateVersionDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCRateVersionDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCRateVersionDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public Property DealerGroupId() As Guid
        Get
            CheckDeleted()
            If Row(VSCRateVersionDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCRateVersionDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCRateVersionDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property VersionNumber() As LongType
        Get
            CheckDeleted()
            If Row(VSCRateVersionDAL.COL_NAME_VERSION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCRateVersionDAL.COL_NAME_VERSION_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(VSCRateVersionDAL.COL_NAME_VERSION_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(VSCRateVersionDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCRateVersionDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(VSCRateVersionDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNewExpirationDate("")> _
    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(VSCRateVersionDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCRateVersionDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(VSCRateVersionDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCRateVersionDAL
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

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal SearchBy As VSCRateVersionDAL.SearchByType, ByVal companyGroupID As Guid,
                                ByVal PlanID As Guid, ByVal Code As String, ByVal Name As String,
                                ByVal EffectiveDate As Date, Optional ByVal HighestVersionOnly As Boolean = True,
                                Optional ByVal iVersionNumber As Integer = 0) As DataView
        Try
            Dim dal As New VSCRateVersionDAL
            Return New DataView(dal.LoadList(SearchBy, companyGroupID, PlanID, Code, Name, EffectiveDate, HighestVersionOnly, iVersionNumber).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function validateEffectiveDate(RateVersionId As Guid, EffectiveDate As Date) As String
        Try
            Dim dal As New VSCRateVersionDAL
            Return dal.validateEffectiveDate(RateVersionId, EffectiveDate)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Custom validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidNewExpirationDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, MSG_INVALID_EXPIRATION_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As VSCRateVersion = CType(objectToValidate, VSCRateVersion)

            If obj.EffectiveDate.Value > obj.ExpirationDate.Value Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region


End Class


